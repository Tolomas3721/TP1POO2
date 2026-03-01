// See https://aka.ms/new-console-template for more informati
using CsvHelper;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Tp1Poo2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InterfaceUtilisateur interfaceUtilisateur = new InterfaceUtilisateur();
            interfaceUtilisateur.MenuPrincipal();
        }
    }
}
