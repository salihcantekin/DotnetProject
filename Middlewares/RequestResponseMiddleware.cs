using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseMiddleware> logger; 
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseMiddleware(RequestDelegate Next, ILogger<RequestResponseMiddleware> Logger)
        {
            next = Next;
            logger = Logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                                                   0,
                                                   readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);

            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private async Task<String> getRequestBody(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            String reqBody = ReadStreamInChunks(requestStream);

            context.Request.Body.Position = 0;

            return reqBody;
        }


        public async Task Invoke(HttpContext httpContext)
        {
            // Request
            String requestText = await getRequestBody(httpContext);



            // Response Part1

            var originalBodyStream = httpContext.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            httpContext.Response.Body = responseBody;

            // Response Part1


            await next.Invoke(httpContext); // Response Bu satırda oluşuyor



            // Response Part2

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEndAsync();
            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);

            // Response Part2

            await responseBody.CopyToAsync(originalBodyStream);

            logger.LogInformation($"Request: {requestText}");
            logger.LogInformation($"Response: {responseText}");
        }

        
    }
}
