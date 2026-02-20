using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

   //  class filles 
      public class ROSA : Grain
      {
        public ROSA(double area, double perimeter, double compactness,
                double kernelLength, double kernelWidth,
                double asymmetryCoefficient, double grooveLength):
            base (area, perimeter , compactness,kernelLength, kernelWidth, asymmetryCoefficient, grooveLength)
        {
           
        }
        public override TypedeGrain GetVariety() => TypedeGrain.ROSA;
      }