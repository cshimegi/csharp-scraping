using System;
using System.Collections.Generic;
using System.Text;

namespace Scraper.Models
{
    class NBA
    {
        /**
         * Player name
         */
        public string player { set; get; }

        /**
         * G
         */
        public string g { set; get; }

        /**
         * PTS
         */
        public string pts { set; get; }

        /**
         * TRB
         */
        public string trb { set; get; }
        
        /**
         * AST 
         */
        public string ast { set; get; }
        
        /**
         * FG (%) 
         */
        public string fg { set; get; }
        
        /**
         * FG3 (%) 
         */
        public string fg3 { set; get; }
        
        /**
         * FT (%) 
         */
        public string ft { set; get; }
        
        /**
         * eFG (%) 
         */
        public string efg { set; get; }
        
        /**
         * PER 
         */
        public string per { set; get; }
        
        /**
         * WS 
         */
        public string ws { set; get; }

        /**
         * Transform player career data to List
         */
        public List<string> toList ()
        {
            List<string> result = new List<string>();

            result.Add(this.player);
            result.Add(this.g);
            result.Add(this.pts);
            result.Add(this.trb);
            result.Add(this.ast);
            result.Add(this.fg);
            result.Add(this.fg3);
            result.Add(this.ft);
            result.Add(this.efg);
            result.Add(this.per);
            result.Add(this.ws);

            return result;
        }
    }
}
