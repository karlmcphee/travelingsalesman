using System;

/*
 * Implementation of the traveling salesman problem, using pruning to shorten the path taken.
 * 
 */

namespace TravelingSale
{
    using System.Linq;
    class TravelingSale
    {
        private static double[,] dist;
        private static double bestdist = 9999;
        static void Main(string[] args)
        {
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\karmc\source\repos\TravelingSale\TravelingSale\citydata2.txt");
            int size1 = lines.Length;
            string[] z = lines[0].Split(null);
            dist = new double[lines.Length, lines.Length];
            int len1 = 0;
            foreach (string line in lines)
            {
                z = line.Split("  ");
                int height1 = 0;
                foreach(string data in z)
                {
                    Console.WriteLine("hello " + data);
                    dist[len1, height1] = Double.Parse(data);
                    height1 += 1;
                }
                len1 += 1;

            }
           // for (int i= 0; i < dist.GetLength(0); i++)
            //    dist[i, i] = 999;
            int[] citarray = new int[5] { 1, 0, 0, 0, 0 };
            var zz = pathCalc(0, citarray, 0);
            double zz1 = zz.Item1;
            Console.WriteLine("Best path is " + zz1);
            Console.WriteLine("Best route is " + zz.Item2);
        }

        private static Tuple<double, String> pathCalc(int city, int[] citarray, double currDist)
        {
            String placestr = city.ToString();
            String beststr = "";
            double currPath = 9999;
            if(currDist > bestdist){
                return Tuple.Create(currDist, "");
            }
            double z = citarray.Sum();
            if (z >= dist.GetLength(0) - 1)
            {
                for(int i = 0; i < citarray.Length; i++)
                {
                    if (citarray[i] == 0)
                    {
                        //          Console.WriteLine("dist is " + dist[city,i]);
                        currDist = currDist + dist[city, i] + dist[i, 0];
                        if (currDist < bestdist) bestdist = currDist;
                        return Tuple.Create(dist[city, i]+dist[i,0], city.ToString()+"-"+i+"-0");
                    }
                }

            }
            for(int i = 0; i < citarray.Length; i++)
            {
                if(citarray[i]==0)
                {
                    citarray[i] = 1;
                    double newDist = currDist + dist[city, i];
                    var nn = pathCalc(i, citarray, newDist);
                    double path = dist[city, i] + nn.Item1;
                    if (path < currPath) {
                        currPath = path;
                        beststr = nn.Item2;
                        
                    }
                    citarray[i] = 0;
                }
            }
            beststr = city + "-"+beststr;
            return Tuple.Create(currPath, beststr);
        }
    }
}