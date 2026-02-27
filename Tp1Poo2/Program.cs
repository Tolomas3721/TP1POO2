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
            Interface_utilisateur interface_Utilisateur = new Interface_utilisateur();
            interface_Utilisateur.MenuPrincipal();
        }
}
    }
