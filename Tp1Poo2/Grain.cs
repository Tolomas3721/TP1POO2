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
            Area = area;
            Perimeter = perimeter;
            Compactness = compactness;
            Kernel_Length = kernelLength;
            Kernel_Width = kernelWidth;
            Groove_Length = grooveLength;
            Asymmetry_Coefficient = asymmetryCoefficient;
        }

        // Methodes 
        public abstract TypeDeGrain GetVariety();
    }
}
