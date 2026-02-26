using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Tp1Poo2
{
    internal class IFileManager
    {
        public List<Grain> ReadFile(string path)
        {
            List<Grain> listDeGrains = new List<Grain>() { };
            if (!File.Exists(path))
                Console.WriteLine("fichier introuvable");

            using (var reader = new StreamReader(path))
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string [] values = line.Split(';');
                    double convert;
                    double[] donnee = new double[values.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (double.TryParse(values[i], NumberStyles.Float, CultureInfo.InvariantCulture, out convert))
                            donnee[i] = convert;
                        
                            //Console.WriteLine("erreur de conversion");
                    }
                   
                    string variety = values[0];
                    switch (variety)
                    {
                        case "Kama":
                            listDeGrains.Add(new KAMA(donnee[1], donnee[2], donnee[3], donnee[4], donnee[5], donnee[6], donnee[7]));
                            break;
                        case "Rosa":
                            listDeGrains.Add(new ROSA(donnee[1], donnee[2], donnee[3], donnee[4], donnee[5], donnee[6], donnee[7]));
                            break;
                        case "Canadian":
                            listDeGrains.Add(new CANADIAN(donnee[1], donnee[2], donnee[3], donnee[4], donnee[5], donnee[6], donnee[7]));
                            break;
                    }
                }
            return listDeGrains;
        }

        // public void WriteFile(string path, List<TypeDeGrain> prediction, int[,] çonfusionMatrix, double precision, uint k, double maxDistance, double minkowskiValue);
    }
}
