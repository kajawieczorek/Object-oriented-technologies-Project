using HomeDBSqlite.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDBSqlite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dbName = "TestDatabase.db";
            if (File.Exists(dbName))
            {
                File.Delete(dbName);
            }

            using (var dbContext = new MyDbContext())
            {
                var gen = new Generator(dbContext);

                dbContext.Database.EnsureCreated();


                /*var people = gen.RandomPeople();
                var companies = gen.RandomCompanies();
                dbContext.People.AddRange(people);
                dbContext.Companies.AddRange(companies);
                dbContext.SaveChanges();

                var homes = new List<Home>();
                for (int i = 0; i < 1000; i++)
                {
                    homes.Add(gen.RandomHome());
                }
                dbContext.Homes.AddRange(homes);
                dbContext.SaveChanges();
*/
                var t = new Stopwatch();
                long sum = 0;
                double areaOfWindows = 0;
                for (int i = 0; i < 10; i++)
                {
                    t.Start();
                    areaOfWindows = dbContext.Homes.Sum(x => x.Rooms.Sum(r => r.Windows.Sum(w => w.Area)));
                    t.Stop();
                    sum += t.ElapsedMilliseconds;
                }
                Console.WriteLine($"Powierzchnia okien: {areaOfWindows}. Czas: {sum / 10}");


                var time = new Stopwatch();
                long suma = 0;
                Tuple<int, double> homeWithMaxNorthWindows = Tuple.Create(0, 0d);
                for (int i = 0; i < 10; i++)
                {
                    time.Start();
                    homeWithMaxNorthWindows = dbContext.Homes
                        .OrderByDescending(x => x.Rooms.Sum(r => r.Windows.Where(w => w.Side == "północ")
                        .Sum(x => x.Area))).Select(h => new Tuple<int, double>(h.OID, h.Rooms.Sum(r => r.Windows.Sum(w => w.Area)))).FirstOrDefault();
                    time.Stop();
                    suma += time.ElapsedMilliseconds;
                }

                //var area = homeWithMaxNorthWindows.Rooms.Sum(x => x.Windows.Sum(a => a.Area));
                Console.WriteLine($"Dom z największą powierzchnią okien na północ: {homeWithMaxNorthWindows.Item1}. Powierzchnia: {homeWithMaxNorthWindows.Item2} Czas: {suma / 10}");
                Console.WriteLine("OK");
            }
            Console.ReadLine();
        }
    }
}
