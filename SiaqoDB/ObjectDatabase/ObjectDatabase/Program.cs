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
        private static void MeasureTime(Action func, string message)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            func();
            stopwatch.Stop();
            Console.WriteLine($"Zapytanie {message} zajęło: {stopwatch.Elapsed}");

        }

        static void Main(string[] args)
        {
            var dbPath = Path.Combine(Path.GetDirectoryName(Environment.GetCommandLineArgs().First()), "Data");
            if (Directory.Exists(dbPath))
                Directory.Delete(dbPath, true);
            Directory.CreateDirectory(dbPath);

            var db = new Siaqodb(dbPath);

            var gen = new Generator(db);

            var people = gen.RandomPeople();
            var companies = gen.RandomCompanies();
            foreach (var person in people)
            {
                db.StoreObject(person);
            }
            foreach (var company in companies)
            {
                db.StoreObject(company);
            }
            for (int i = 0; i < 1000; i++)
            {
                db.StoreObject(gen.RandomHome());
            }
            double areaOfWindows = 0;
            var homeWithMaxNorthWindows = new Home();
            var time = new Stopwatch();
            long sumw = 0;
            for (int i = 0; i < 10; i++)
            {
                time.Start();
                //MeasureTime(() =>
                // {
                areaOfWindows = db.LoadAll<Home>().Sum(x => x.Rooms.Sum(r => r.Windows.Sum(w => w.Area)));
                // }, "o powierzchnię okien");
                time.Stop();
                sumw += time.ElapsedMilliseconds;
            }
            Console.WriteLine($"Powierzchnia okien: {areaOfWindows}, czas:{sumw/10}");

            var t = new Stopwatch();
            long sum = 0;
            for (int i = 0; i < 10; i++)
            {
                t.Start();
                // MeasureTime(() =>
                //{
                homeWithMaxNorthWindows = db.LoadAll<Home>().OrderByDescending(x => x.Rooms.Sum(r => r.Windows.Where(w => w.Side == "północ").Sum(a => a.Area))).FirstOrDefault();
                // }, "o dom z największą powierzchnią okien wychodzących na północ");
                t.Stop();
                sum += t.ElapsedMilliseconds;
            }

            Console.WriteLine($"Dom z max pow. okien na północ. Wynik oid: {homeWithMaxNorthWindows.OID}, " +
                $"\npowierzchnia okien: {homeWithMaxNorthWindows.Rooms.Sum(x => x.Windows.Sum(w => w.Area))}" +
                $"\nCzas: {sum/10}");

            Console.WriteLine("OK");
            Console.ReadLine();

        }
    }
}
