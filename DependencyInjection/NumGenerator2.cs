using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class NumGenerator2: INumGenerator2
    {
        public int RandomValue { get;  }


        private readonly INumGenerator numGenerator;

        public NumGenerator2(INumGenerator NumGenerator)
        {
            numGenerator = NumGenerator;

            RandomValue = new Random().Next(1000);
        }

        public int GetNumGeneratorRandomNumber()
        {
            return numGenerator.RandomValue;
        }
    }

    public interface INumGenerator2
    {
        public int RandomValue { get; }

        public int GetNumGeneratorRandomNumber();
    }
}
