using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        /*
            Calcule la distance Minkowski entre les deux grains

            pour la distance Manhattan:     degreDistance = 1
            pour la distance Euclidienne:   degreDistance = 2
         */
        public double MinkowskiDistance(Grain autre, double p)
        {
            double dist = 0.0;

            dist += MinkowskiStep(this.Area,                    autre.Area,                     p);
            dist += MinkowskiStep(this.Perimeter,               autre.Perimeter,                p);
            dist += MinkowskiStep(this.Compactness,             autre.Compactness,              p);
            dist += MinkowskiStep(this.Kernel_Length,           autre.Kernel_Length,            p);
            dist += MinkowskiStep(this.Kernel_Width,            autre.Kernel_Width,             p);
            dist += MinkowskiStep(this.Groove_Length,           autre.Groove_Length,            p);
            dist += MinkowskiStep(this.Asymmetry_Coefficient,   autre.Asymmetry_Coefficient,    p);

            dist = Math.Pow(dist, 1 / p);
            return dist;
        }

        private double MinkowskiStep(double x1, double x2, double p)
        {
            double ret = Math.Abs(x1 - x2);
            ret = Math.Pow(ret, p);

            return ret;
        }
    }
}
