using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Poo2
{
    public enum TypeDeGrain
    {
        ROSA = 0,   
        KAMA = 1,
        CANADIAN= 2,  

    }


    public abstract class Grain
    {
        // acces au classe fille
        protected double Area { get; set; }
        protected double Perimeter { get; set; }
        protected double Compactness { get; set; }
        protected double Kernel_Length { get; set; }
        protected double Kernel_Width { get; set; }
        protected double Asymmetry_Coefficient { get; set; }
        protected double Groove_Length { get; set; }
        // les constructeurs
        public Grain(double area, double perimeter, double compactness,
                     double kernelLength, double kernelWidth, double grooveLength,
                     double asymmetryCoefficient)

        {
            this.Area = area;
            this.Perimeter = perimeter;
            this.Compactness = compactness;
            this.Kernel_Length = kernelLength;
            this.Kernel_Width = kernelWidth;
            this.Groove_Length = grooveLength;
            this.Asymmetry_Coefficient = asymmetryCoefficient;
        }

        // Methodes 
        public abstract TypeDeGrain GetVariety();
    }
}
