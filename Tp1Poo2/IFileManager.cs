using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Poo2
{
    internal interface IFileManager
    {
        public List<Grain> ReadFile(string path);

        public void WriteFile(string path, List<TypeDeGrain> prediction, int[,] çonfusionMatrix, double precision, uint k, double maxDistance, double minkowskiValue);
    }
}
