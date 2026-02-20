using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Poo2
{

    public class KAMA : Grain
    {
        public KAMA(double area, double perimeter, double compactness,
          double kernelLength, double kernelWidth,
          double asymmetryCoefficient, double grooveLength)
          : base(area, perimeter, compactness, kernelLength, kernelWidth, asymmetryCoefficient, grooveLength)
        {

        }

        public override TypeDeGrain GetVariety() => TypeDeGrain.KAMA;
    }
}
