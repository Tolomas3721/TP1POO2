using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Poo2
{

    public class KAMA : Grain
    {
        public KAMA(double Area, double Perimeter, double Compactness,
                     double Kernel_Length, double Kernel_Width, double Groove_Length,
                     double Asymmetry_Coefficient)
            : base(Area, Perimeter, Compactness, Kernel_Length, Kernel_Width, Groove_Length, Asymmetry_Coefficient)
        {

        }

        public override TypeDeGrain GetVariety() => TypeDeGrain.KAMA;
    }
}
