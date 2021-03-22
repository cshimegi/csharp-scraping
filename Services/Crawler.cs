using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using HtmlAgilityPack;
using Scraper.Models;
using System.Net.Http;
using System.Net;
using System.IO;
using AngleSharp.Html.Parser;
using AngleSharp.Html.Dom;
using AngleSharp;

namespace Scraper.Services
{
    class Crawler
    {
        const string BASE_API = "https://www.basketball-reference.com";
        const string BASE_PLAYER_API = BASE_API + "/players";

        public static async Task<NBA> getPlayerCareerData(string playerPagePath)
        {
            string url = BASE_API + playerPagePath;
            WebClient wc = new WebClient();
            string htmldocs = wc.DownloadString(url);
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(htmldocs));
            NBA nba = new NBA();
            var meta = document.QuerySelector("div[id='meta']");

            if (meta != null)
            {
                var divPerson = meta.FirstElementChild.FirstElementChild;
                var playerName = divPerson.TextContent;

                if (playerName != null)
                {
                    nba.player = playerName;
                } else
                {
                    // not found name?
                    nba.player = playerPagePath;
                }
            }

            foreach (var divTag in document.QuerySelectorAll("div[class='stats_pullout'] div[class^='p']"))
            {
                foreach (var subDivTag in divTag.Children)
                {
                    var key = subDivTag.FirstElementChild.TextContent.Replace("%", "").ToLower();
                    var value = subDivTag.LastElementChild.TextContent;

                    if (value != null)
                    {
                        typeof(NBA).GetProperty(key).SetValue(nba, value);
                    } else
                    {
                        typeof(NBA).GetProperty(key).SetValue(nba, "");
                    }
                }
            }

            return nba;
        }

        public static async Task<List<string>> getPlayerBasicInfoByAlphabet(char alphabet)
        {
            string url = BASE_PLAYER_API + "/" + alphabet + "/";
            WebClient wc = new WebClient();
            string htmldocs = wc.DownloadString(url);
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(htmldocs));
            List<string> links = new List<string>();

            foreach (var item in document.QuerySelectorAll("table[id='players'] tbody tr"))
            {
                var thTag = item.FirstElementChild;
                var aTag = thTag.FirstElementChild;
                var href = aTag.GetAttribute("href");
                //var player = aTag.TextContent;
                links.Add(href);
            }

            return links;
        }
    }
}
