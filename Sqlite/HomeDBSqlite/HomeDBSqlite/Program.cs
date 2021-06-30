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
            using (var dbContext = new MyDbContext())
            {
                var gen = new Generator(dbContext);
                dbContext.Database.EnsureCreated();

                //gen.LoadData();

                /*MeasureTime(() =>
                {
                    var areaOfWindows = dbContext.Homes.Sum(x => x.Rooms.Sum(r => r.Windows.Sum(w => w.Area)));
                }, "o powierzchnię okien");

                MeasureTime(() =>
                {
                    var nhomeWithMaxNorthWindows = dbContext.Homes.OrderByDescending(x => x.Rooms.Sum(r => r.Windows.Where(w => w.Side == "północ")
                                    .Sum(a => a.Area))).FirstOrDefault();
                }, "o dom z największą powierzchnią okien na północ"); */

                var two = new List<Home>();
                MeasureTime(() =>
                {
                    two = dbContext.Homes.Where(x => x.Rooms.Count(r => r.Name == "sypialnia") > 1).ToList();
                }, "z dwoma złączeniami");
                foreach (var item in two)
                {
                    Console.WriteLine($"ID: {item.OID}");
                }

                /*
                var three = new List<Home>();
                MeasureTime(() =>
                {
                     three = dbContext.Homes.Where(x => x.Occupants.Any(x => x.PersonOccupant.Name == "James")
                                                && x.Rooms.Count(r => r.Name == "sypialnia") > 1).ToList();
                }, "z trzema złączeniami");
                foreach (var item in three)
                {
                    Console.WriteLine($"ID: {item.OID}");
                }
                MeasureTime(() =>
                {
                    var four = dbContext.Homes.Where(x => x.Occupants.Any(x => x.PersonOccupant.Name == "James")
                                                && x.Rooms.Count(r => r.Name == "sypialnia") > 1
                                                && x.Rooms.Any(f => f.Furniture.Count() > 3
                                                && f.Name == "sypialnia"))
                                                .ToList();
                }, "z czterema złączeniami");*/


                /*MeasureTime(() =>
                {
                    var five = dbContext.Homes.Where(x => x.Owners.Any(o => o.CompanyOwner.Headquaters.City.Contains("Lake")) 
                                                 && x.Rooms.Any(w => w.Windows.Any(s => s.Side == "wschód")
                                                 && x.Occupants.Count() >= 2)).ToList();
                }, "z pięcioma złączeniami");

                MeasureTime(() =>
                {
                    var six = dbContext.Homes.Where(x => x.Owners.Any(o => o.CompanyOwner.Headquaters.City.Contains("Lake") && o.Share > 30)
                                            && x.Rooms.Any(w => w.Windows.Any(s => s.Side == "wschód")
                                            && w.Furniture.Any(t => t.Name == "komoda")
                                            && x.Occupants.Count() >= 2)
                                            && x.Address.City.Contains("Lake"))
                                            .ToList();
                }, "z ośmioma złączeniami");*/
            }
            Console.ReadLine();
        }
        private static void MeasureTime(Action func, string message)
        {
            var stopwatch = new Stopwatch();
            long sum = 0;
            for (int i = 0; i < 10; i++)
            {
                stopwatch.Reset();
                stopwatch.Start();
                func();
                stopwatch.Stop();
                sum += stopwatch.ElapsedMilliseconds;
                if (i == 0)
                {
                    Console.WriteLine($"Pierwsze zapytanie: {sum}");
                }
            }

            Console.WriteLine($"Zapytanie {message} średnio zajęło: {sum / 10}");
        }
    }
}
