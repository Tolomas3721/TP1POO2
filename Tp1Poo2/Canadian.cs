using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Poo2
{
    public class CANADIAN : Grain
    {
        public CANADIAN(double area, double perimeter, double compactness, double kernelLength,
            double kernelwidth, double asymmetryCoefficient, double grooveLength)
            : base(area, perimeter, compactness, kernelLength, kernelwidth, asymmetryCoefficient, grooveLength)
        {
            
        }
        public override TypeDeGrain GetVariety() => TypeDeGrain.CANADIAN;
    }
   
}
