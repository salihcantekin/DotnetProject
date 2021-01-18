using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class NumGenerator: INumGenerator
    {
        public int RandomValue { get; }

        public NumGenerator()
        {
            RandomValue = new Random().Next(1000);
        }

    }

    public interface INumGenerator
    {
        public int RandomValue { get; }
    }
}
