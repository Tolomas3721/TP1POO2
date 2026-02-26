// See https://aka.ms/new-console-template for more informati
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using System.IO;



namespace Tp1Poo2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string chemintest = "base_de_données/seeds_dataset_test.csv";
            string chemintraining = "base_de_données/seeds_dataset_training.csv";
            IFileManager file = new IFileManager();
            List<Grain> test = file.ReadFile(chemintest);
            foreach (Grain grain in test) {
                Console.WriteLine(grain.GetVariety());
            }
    }
}
}