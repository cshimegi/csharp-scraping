using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Scraper.Services;
using Scraper.Models;
using System.Threading.Tasks;

namespace Scraper
{
    class Program
    {
        static readonly char[] alphabets = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();

        static void Main(string[] args)
        {
            Task t = MainAsync(args);
            Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            Log.info("Start to get data from NBA web page");

            List<Task> tasks = new List<Task>();

            foreach (char alphabet in alphabets)
            {
                Task t = Task.Run(async () =>
                {
                    var data = Crawler.getPlayerBasicInfoByAlphabet(alphabet);
                    var playerLinks = data.Result;

                    await getPlayerCareerData(alphabet, playerLinks);
                    Log.info("Got players whose last name starts with [" + alphabet + "]");
                });
                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());
        }


        static async Task getPlayerCareerData (char alphabet, List<string> links)
        {

            List<Task> tasks = new List<Task>();
            List<NBA> careers = new List<NBA>();

            foreach (string link in links)
            {
                Task t = Task.Run(() =>
                {
                    NBA playerCareer = Crawler.getPlayerCareerData(link).Result;
                    careers.Add(playerCareer);
                    Log.debug("Got player's career => " + playerCareer.player);
                });
                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());

            using (CsvWriter cw = new CsvWriter(alphabet + ".csv"))
            {
                cw.savePlayerCareerData(careers);
            }
        }
    }
}
