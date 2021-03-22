using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Scraper.Models;

namespace Scraper.Services
{
    class CsvWriter: IDisposable
    {
        private static string[] PATHS = { Environment.CurrentDirectory, "..", "data" };
        private static readonly string BASE_PATH = Path.Combine(PATHS);
        private static readonly string NBA_HEADER = string.Format("{0}, {1}", "Player,G,PTS,TRB,AST,FG(%),FG3(%),FT(%),eFG(%),PER,WS", Environment.NewLine);

        private string filename;
        protected bool Disposed { get; private set; }

        public CsvWriter(string filename)
        {
            this.filename = filename;

            if (!Directory.Exists(BASE_PATH))
            {
                Directory.CreateDirectory(BASE_PATH);
            }
        }

        public void savePlayerCareerData(List<NBA> rows)
        {
            Log.debug("Start to write all player careers");

            try
            {
                string filepath = Path.Combine(BASE_PATH, this.filename);

                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(NBA_HEADER);

                        foreach (NBA row in rows)
                        {
                            var rowData = string.Format("{0}, {1}", String.Join(',', row.toList()), Environment.NewLine);
                            sw.WriteLine(rowData);
                            Log.debug("Done to write player's career => " + row.player);
                        }
                    }
                } else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        foreach (NBA row in rows)
                        {
                            var rowData = string.Format("{0}, {1}", String.Join(',', row.toList()), Environment.NewLine);
                            sw.WriteLine(rowData);
                            Log.debug("Done to write player's career => " + row.player);
                        }
                    }
                }

                Log.debug("Done to write all player careers");
            } catch (Exception ex)
            {
                Log.error("[Csv write error] " + ex.Message);
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            this.Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    // Perform managed cleanup here.

                }

                // Perform unmanaged cleanup here.

                this.Disposed = true;
            }
        }
    }
}
