using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Tp1Poo2
{
    internal interface IFileManager
    {
        public static abstract List<Grain> ReadFile(string path);

        public static abstract void WriteFile(
            string name,
            List<TypeDeGrain> prediction,
            List<Grain> real,
            uint k,
            double maxDistance,
            double minkowskiValue,
            int[,] confusionMatrix,
            double predictionRate
        );
    }
}
