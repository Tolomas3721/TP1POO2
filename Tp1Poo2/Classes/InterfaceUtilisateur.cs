using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Tp1Poo2
{
    internal class InterfaceUtilisateur
    {
      public void MenuPrincipal ()
      {
            AnsiConsole.Write(
                new FigletText("Bienvenue dans le classifieur KNN").Centered().Color(Color.Green)
            );
            uint k = ChooseK();
            double minkowskiValue = chooseDistanceType();
            double maxDistance = chooseMaxDistance();

            Directory.SetCurrentDirectory("../../../../Tp1Poo2");

            List<Grain> test = CSVFileManager.ReadFile("data/seeds_dataset_test");
            List<Grain> train = CSVFileManager.ReadFile("data/seeds_dataset_training");


            ClassifieurKNN classifieur = new ClassifieurKNN();
            List<TypeDeGrain>? resultats = classifieur.ClassifierGrains(train, test, k, minkowskiValue, maxDistance);
            if(resultats == null)
            {
                resultats = new List<TypeDeGrain>();
            }

            int[,] confusionMatrix = classifieur.CalculateConfusionMatrix(resultats, test);
            double accuracy = classifieur.CalculateAccuracy(confusionMatrix);

            AfficherResultat(resultats, test, confusionMatrix, accuracy);

            JSONFileManager.WriteFile(
                "data/resultats",
                resultats,
                test,
                k,
                maxDistance,
                minkowskiValue,
                confusionMatrix,
                accuracy
            );
        }
        private void AfficherResultat(List<TypeDeGrain>prediction, List<Grain>test, int[,] confusionMatrix, double accuracy)
        {
            AnsiConsole.Write(new Rule("[yellow]Résultats de classification[/]").Centered());
            // Tableau de prediction vs réalité
            var tableDetails = new Table().Border(TableBorder.Rounded);

            tableDetails.Title("[blue] le Détails par grain[/]");
            tableDetails.AddColumn("Index");
            tableDetails.AddColumn("Réel");
            tableDetails.AddColumn("Prédit");

            for (int i = 0; i < test.Count && i < prediction.Count; i++)
            {
                string reel = test[i].GetVariety().ToString();
                string predit = prediction[i].ToString();

                string couleur = reel == predit ? "green" : "red";

                tableDetails.AddRow(
                    i.ToString(),
                    reel,
                    $"[{couleur}]{predit}[/]");
            }

            AnsiConsole.Write(tableDetails);
            // 3. Tableau matrice de confusion (si tu veux aussi)
            var tableConf = new Table()
                .Border(TableBorder.Rounded);

            tableConf.Title("[blue]Matrice de confusion[/]");
            tableConf.AddColumn("Prédite \\ Réelle");
            tableConf.AddColumn("Kama");
            tableConf.AddColumn("Rosa");
            tableConf.AddColumn("Canadian");

            tableConf.AddRow("Kama",
                confusionMatrix[0, 0].ToString(),
                confusionMatrix[0, 1].ToString(),
                confusionMatrix[0, 2].ToString());

            tableConf.AddRow("Rosa",
                confusionMatrix[1, 0].ToString(),
                confusionMatrix[1, 1].ToString(),
                confusionMatrix[1, 2].ToString());

            tableConf.AddRow("Canadian",
                confusionMatrix[2, 0].ToString(),
                confusionMatrix[2, 1].ToString(),
                confusionMatrix[2, 2].ToString());

            AnsiConsole.Write(tableConf);

            // 2 decimal pour l'affichage de l'accuracy
            AnsiConsole.MarkupLine($"Exactitude : [bold green]{accuracy * 100:F2}%[/]");
        }

        private uint ChooseK()
        {
            // Demande k avec texte en gris + validation
            uint k = AnsiConsole.Prompt(
                new TextPrompt<uint>("Veuillez saisir le nombre de voisins [green]k[/] :")
                    .PromptStyle("green")
                    .Validate(value => value == 0
                            ? ValidationResult.Error("[red]k doit être supérieur à 0[/]")
                            : ValidationResult.Success()));

            AnsiConsole.WriteLine();
            return k;
        }
        private double chooseDistanceType()
        {
            int choose;
            double minkowskiResult;
            while (true)
            {
                AnsiConsole.MarkupLine(
                     "[green]veiller choisir le type de distance : 1 pour Manhattan, 2 pour Euclidienne , 3 Minkowski[/]");

                string typeDistance = Console.ReadLine();
                if (int.TryParse(typeDistance, out choose))
                {
                    if (choose == 1)
                    {
                        return 1.0; // Manhattan
                    }
                    else if (choose == 2)
                    {
                        return 2.0; // Euclidienne
                    }
                    else if (choose == 3)
                    {
                        //  Rentrer la valeur de Minkowski
                        AnsiConsole.MarkupLine("[red]veuiller saisir la valeur Minkowski à utiliser [/]");
                        string minkowskiValue = Console.ReadLine();
                        if (double.TryParse(minkowskiValue, out minkowskiResult) && minkowskiResult > 0)
                        {
                            return minkowskiResult;
                        }
                        AnsiConsole.MarkupLine("[green]choix invalide, veiller choisir entre 1 , 2 ou 3 [/]");
                    }
                }
            }   
        }
        private double chooseMaxDistance()
        {
            float maxDistance;
            while (true)
            {
                AnsiConsole.MarkupLine("[Blue]veiller saisir la distance maximale à utiliser[/]");
                string maxDistanceStr = Console.ReadLine();
                if (float.TryParse(maxDistanceStr, out maxDistance))
                {
                    if (maxDistance <= 0)
                    {
                        AnsiConsole.MarkupLine("[red]la distance maximale doit être supérieure à 0");
                    }
                    else
                    {
                        return maxDistance;
                    }
                }
            }
        }
    }
}
   