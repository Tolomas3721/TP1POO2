using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Poo2
{
    // moins lourd a lire de cette facon
    using DistancesList = List<Tuple<double, TypeDeGrain>>;

    internal interface ClassifieurKNN
    {
        
        public List<TypeDeGrain> ClassifierGrains(List<Grain> training, List<Grain> test, uint k, double distanceMax, double degreDistance)
        {
            List<TypeDeGrain> results = new List<TypeDeGrain>();

            foreach(Grain grain in test)
            {
                results.Add(
                    PredictionKNN(
                        ChooseKClosest(
                            CalculerDistances(training, grain, degreDistance), k, distanceMax
                        )
                    )
                );
            }

            return results;
        }

        private TypeDeGrain PredictionKNN(DistancesList distances)
        {
            int nombresTypes = Enum.GetValues<TypeDeGrain>().Length;

            List<int> counts = Enumerable.Repeat(0, nombresTypes).ToList();

            foreach(var grain in distances)
            {
                counts[((int)grain.Item2)]++;
            }

            int max = counts.Max();
            List<int> tieIndices = counts
               .Select((value, index) => new { value, index })
               .Where(item => item.value == max)
               .Select(item => item.index)
               .ToList();

            bool tie = tieIndices.Count >= 2;
            if (tie)
            {
                return TieBreakerLevel1(distances);
            }

            // il est garanti que le max apparait au moins une fois
            // les index sont directement les valeurs de l'enum (voir le foreach juste au-dessus)
            // on fait juste la conversion inverse
            return (TypeDeGrain)tieIndices[0];
        }

        private TypeDeGrain TieBreakerLevel1(DistancesList distances)
        {
            // on trouve le grain le plus proche, si il y a deux grains a la meme distance
            // on calcul quels grains sont les plus proches en moyenne et on prend la plus petite distance
            // si égalité, on choisi aléatoirement entre les deux choix

            Tuple<double, TypeDeGrain> closest = new Tuple<double, TypeDeGrain>(double.MaxValue, (TypeDeGrain)0);

            closest = distances.MinBy(e => (
                e.Item1 < closest.Item1
            ))!; // on sait qu'il y a au moins une distance < infinity

            bool tie = false;

            foreach(var grain in distances)
            {
                // si les deux grains sont de même type, alors le résultat sera le même anyway
                if(grain.Item1 == closest.Item1 && grain.Item2 != closest.Item2)
                {
                    tie = true;
                }
            }

            if (!tie) return closest.Item2;

            return TieBreakerLevel2(distances);
        }

        private TypeDeGrain TieBreakerLevel2(DistancesList distances)
        {
            // on calcul quels grains sont les plus proches en moyenne et on prend la plus petite distance
            // si égalité, on choisi aléatoirement entre les deux choix


            int nombresTypes = Enum.GetValues<TypeDeGrain>().Length;

            List<int> counts = Enumerable.Repeat(0, nombresTypes).ToList();

            List<double> distancesPerType = Enumerable.Repeat(0.0, nombresTypes).ToList();

            foreach (var grain in distances)
            {
                counts[(int)grain.Item2]++;
                distancesPerType[(int)grain.Item2]++;  
            }

            for (int i = 0; i < distancesPerType.Count; i++)
            {
                distancesPerType[i] /= (double)counts[i];
            }

            int min = counts.Min();
            List<int> tieIndices = counts
               .Select((value, index) => new { value, index })
               .Where(item => item.value == min)
               .Select(item => item.index)
               .ToList();

            bool tie = tieIndices.Count >= 2;
            if (tie)
            {
                Random rng = new Random();
                int index = rng.Next(tieIndices.Count);
                return (TypeDeGrain)tieIndices[index];
            }

            // il est garanti que le max apparait au moins une fois
            // les index sont directement les valeurs de l'enum (voir le foreach juste au-dessus)
            // on fait juste la conversion inverse
            return (TypeDeGrain)tieIndices[0];
        }
    
        private DistancesList CalculerDistances(List<Grain> training, Grain test, double degreDistance)
        {
            DistancesList distances = new DistancesList();
            foreach(var grain in training)
            {
                distances.Add(
                    new Tuple<double, TypeDeGrain>(
                        test.CalculerDistance(grain, degreDistance), grain.GetVariety()
                    )
                );
            }
            return distances;
        }
    
        private DistancesList ChooseKClosest(DistancesList distances, uint k, double distanceMax)
        {
            DistancesList closest = new DistancesList();

            // tri d'insertion, parce qu'on garde seulement les k plus proches (pas beaucoup de comparaisons)
            // O(n * k) où n est le nombre de grains dans le training
            // plus efficace qu'un tri rapide si k est petit
            foreach(var distance in distances)
            {
                if(distance.Item1 > distanceMax) continue;

                closest.Add(distance);

                // i fait parti de [0, k - 1] ici
                int i = closest.Count - 2;
                if (i < 0) continue;

                while (closest[i].Item1 > closest[i + 1].Item1)
                {
                    i--;
                    if (i < 0) break;
                    // swap the two
                    var temp = closest[i];
                    closest[i] = closest[i + 1];
                    closest[i + 1] = temp;
                }

                // we have more than k elements, remove the furthest one
                if (closest.Count > k) closest.RemoveAt(closest.Count - 1);
            }

            return closest;
        }
    }
}
