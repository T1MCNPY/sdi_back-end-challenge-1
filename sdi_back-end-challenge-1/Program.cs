using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sdi_back_end_challenge_1
{
    internal class Program
    {
        static void Main(string[] args)
        {



            // Define the car rental options
            var carSizes = new (string Size, int SeatCapacity, int Cost)[]
            {
                           ("S", 5, 5000),
                           ("M", 10, 8000),
                           ("L", 15, 12000)
            };

            // Prompt the user to input the number of seats
            Console.Write("Please input number (seat): ");
            int requiredSeats;
            if (!int.TryParse(Console.ReadLine(), out requiredSeats) || requiredSeats <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
                return;
            }

            // Calculate the optimized cost
            var result = CalculateOptimizedCost(requiredSeats, carSizes);

            // Print the results
            foreach (var item in result.Item1)
            {
                Console.WriteLine($"{item.Size} x {item.Count}");
            }
            Console.WriteLine($"Total = PHP {result.Item2}");
        }

        static (List<(string Size, int Count)>, int) CalculateOptimizedCost(int seats, (string Size, int SeatCapacity, int Cost)[] carSizes)
        {
            int minCost = int.MaxValue;
            List<(string Size, int Count)> bestCombination = null;

            // Iterate over all possible combinations of car counts
            for (int i = 0; i <= seats / carSizes[2].SeatCapacity; i++)
            {
                for (int j = 0; j <= seats / carSizes[1].SeatCapacity; j++)
                {
                    for (int k = 0; k <= seats / carSizes[0].SeatCapacity; k++)
                    {
                        int totalSeats = i * carSizes[2].SeatCapacity + j * carSizes[1].SeatCapacity + k * carSizes[0].SeatCapacity;
                        if (totalSeats >= seats) // 
                        {
                            int cost = i * carSizes[2].Cost + j * carSizes[1].Cost + k * carSizes[0].Cost;
                            if (cost < minCost)
                            {
                                minCost = cost;
                                bestCombination = new List<(string Size, int Count)>
                                           {
                                               (carSizes[2].Size, i),
                                               (carSizes[1].Size, j),
                                               (carSizes[0].Size, k)
                                           };

                                // Remove cars with a count of 0 from the best combination
                                bestCombination.RemoveAll(car => car.Count == 0);
                            }
                        }
                    }
                }
            }

            return (bestCombination, minCost);

            //////////////////////////////////
            /// for Extra points

            /*   // Define the car rental options with the ability to change capacities and costs
               var carSizes = new (string Size, int SeatCapacity, int Cost)[]
               {
               ("S", 5, 5000),
               ("M", 9, 8000),  //  Medium car with 9 seats
               ("L", 15, 11000) //  Large car with cost 11,000
               };

               // Prompt the user to input the number of seats
               Console.Write("Please input number (seat): ");
               int requiredSeats;
               if (!int.TryParse(Console.ReadLine(), out requiredSeats) || requiredSeats <= 0)
               {
                   Console.WriteLine("Invalid input. Please enter a positive integer.");
                   return;
               }

               // Calculate the optimized cost
               var result = CalculateOptimizedCost(requiredSeats, carSizes);

               // Print the results
               foreach (var item in result.Item1)
               {
                   Console.WriteLine($"{item.Size} x {item.Count}");
               }
               Console.WriteLine($"Total = PHP {result.Item2}");
           }

           static (List<(string Size, int Count)>, int) CalculateOptimizedCost(int seats, (string Size, int SeatCapacity, int Cost)[] carSizes)
           {
               int minCost = int.MaxValue;
               List<(string Size, int Count)> bestCombination = null;

               // Iterate over all possible combinations of car counts
               int[] counts = new int[carSizes.Length];

               // recursive method to explore all combinations of cars
               void TryCombination(int index, int remainingSeats, int currentCost)
               {
                   // If the used all car sizes or found a valid combination
                   if (index == carSizes.Length)
                   {
                       if (remainingSeats <= 0 && currentCost < minCost)
                       {
                           minCost = currentCost;
                           bestCombination = new List<(string Size, int Count)>();

                           for (int i = 0; i < carSizes.Length; i++)
                           {
                               if (counts[i] > 0)
                               {
                                   bestCombination.Add((carSizes[i].Size, counts[i]));
                               }
                           }
                       }
                       return;
                   }

                   // Try different numbers of cars of this type
                   for (int i = 0; i <= seats / carSizes[index].SeatCapacity + 1; i++)
                   {
                       counts[index] = i;
                       TryCombination(index + 1, remainingSeats - i * carSizes[index].SeatCapacity, currentCost + i * carSizes[index].Cost);
                   }
               }

               TryCombination(0, seats, 0);

               return (bestCombination, minCost);*/
        }
    }
}
