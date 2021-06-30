using ObjectDatabase.Model;
using Sqo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs().First()), "Data");
            if (Directory.Exists(dbPath))
                Directory.Delete(dbPath, true);
            Directory.CreateDirectory(dbPath);

            var db = new Siaqodb(dbPath);

            var gen = new Generator(db);

            gen.LoadData();

            MeasureTime(() =>
            {
                var two = db.LoadAll<Home>().Where(x => x.Rooms.Count(r => r.Name == "sypialnia") > 1).ToList();
            }, "z dwoma złączeniami");

            MeasureTime(() =>
            {
                var three = db.LoadAll<Home>().Where(x => x.Occupants.Any(y => y.PersonOccupant.Name == "James")
                                            && x.Rooms.Count(r => r.Name == "sypialnia") > 1).ToList();
            }, "z trzema złączeniami");

            MeasureTime(() =>
            {
                var four = db.LoadAll<Home>().Where(x => x.Occupants.Any(y => y.PersonOccupant.Name == "James")
                                            && x.Rooms.Count(r => r.Name == "sypialnia") > 1
                                            && x.Rooms.Any(f => f.Furniture.Count() > 3
                                            && f.Name == "sypialnia"))
                                            .ToList();
            }, "z czterema złączeniami");

            MeasureTime(() =>
            {
                var five = db.LoadAll<Home>().Where(x => x.Owners.Any(o => o.CompanyOwner != null 
                                             && o.CompanyOwner.Headquaters.City.Contains("Lake")) 
                                             && x.Rooms.Any(w => w.Windows.Any(s => s.Side == "wschód")
                                             && x.Occupants.Count() >= 2)).ToList();
            }, "z pięcioma złączeniami");

            MeasureTime(() =>
            {
                var six = db.LoadAll<Home>().Where(x => x.Owners.Any(o => o.CompanyOwner != null 
                                        && o.CompanyOwner.Headquaters.City.Contains("Lake") 
                                        && o.Share > 30)
                                        && x.Rooms.Any(w => w.Windows.Any(s => s.Side == "wschód")
                                        && w.Furniture.Any(t => t.Name == "komoda")
                                        && x.Occupants.Count() >= 2)
                                        && x.Address.City.Contains("Lake"))
                                        .ToList();
            }, "z ośmioma złączeniami");

            Console.WriteLine("OK");
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
