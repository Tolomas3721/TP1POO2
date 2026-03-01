using CsvHelper;
using CsvHelper.Configuration;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Poo2
{
    internal class CSVFileManager : IFileManager
    {
        public static List<Grain> ReadFile(string path)
        {
            path += ".csv";
            List<Grain> listDeGrains = new List<Grain>();

            if (!File.Exists(path))
            {
                Console.WriteLine("fichier introuvable");
                return listDeGrains;
            }

            using (var reader = new StreamReader(path))
            using (var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            }))
            {

                csvReader.Read();
                csvReader.ReadHeader();

                while (csvReader.Read())
                {
                    string? grainVariety = csvReader.GetField("variety");

                    switch (grainVariety)
                    {
                        case "Kama":
                            listDeGrains.Add(csvReader.GetRecord<KAMA>());
                            break;
                        case "Rosa":
                            listDeGrains.Add(csvReader.GetRecord<ROSA>());
                            break;
                        case "Canadian":
                            listDeGrains.Add(csvReader.GetRecord<CANADIAN>());
                            break;
                    }
                }
            }
            
            return listDeGrains;
        }


        // n'implémente rien
        public static void WriteFile(
            string name,
            List<TypeDeGrain> prediction,
            List<Grain> real,
            uint k,
            double maxDistance,
            double minkowskiValue,
            int[,] confusionMatrix,
            double predictionRate
            )
        {
            // faire quoi que ce soit ici ne ferait pas vraiment de sens
            // le format CSV ne permet pas vraiment de prendre en charge ceci
            // peut etre si on inclus les parametres dans le nom du fichier (date, k, etc.)
        }
    }
}
