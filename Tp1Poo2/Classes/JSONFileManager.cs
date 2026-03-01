using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;

using System.Runtime.CompilerServices;

namespace Tp1Poo2
{
    internal class JSONFileManager : IFileManager
    {
        public static List<Grain> ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("fichier introuvable");
                return new List<Grain>();
            }

            List<Grain>? grains = JsonSerializer.Deserialize<List<Grain>>(path);

            if (grains != null) return grains;
            // else
            return new List<Grain>();
        }


        struct GrainData
        {
            public List<TypeDeGrain> prediction;
            public int numberOfGrains;
            public uint k;
            public double maxDistance;
            public double minkowskiValue;
            public string date;
            public int[,] confusionMatrix;
            public double predictionRate;

            public GrainData(
                List<TypeDeGrain> prediction,
                int numberOfGrains,
                uint k,
                double maxDistance,
                double minkowskiValue,
                string date,
                int[,] confusionMatrix,
                double predictionRate
            )
            {
                this.prediction = prediction;
                this.numberOfGrains = numberOfGrains;
                this.k = k;
                this.maxDistance = maxDistance;
                this.minkowskiValue = minkowskiValue;
                this.date = date;
                this.confusionMatrix = confusionMatrix;
                this.predictionRate = predictionRate;
            }
        };

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

            string currentDate = DateTime.Now.ToString("yyyy_MM_dd_HH-mm-ss");

            string data = JsonSerializer.Serialize(new GrainData(
                prediction,
                prediction.Count,
                k,
                maxDistance,
                minkowskiValue,
                currentDate,
                confusionMatrix,
                predictionRate
            ));

            if (File.Exists(name + ".json"))
            {
                name += currentDate;
            }

            while (File.Exists(name + ".json"))
            {
                name += "_0";
            }

            name += ".json";
            using (var writer = new StreamWriter(name))
            {
                writer.Write(data);
            }
        }
    }
}
