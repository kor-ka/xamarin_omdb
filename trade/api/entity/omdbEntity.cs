using System;
using System.Collections.Generic;
using System.Text;

namespace trade.api.entity
{
    class FilmData
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbID { get; set; }
        public string Type { get; set; }
        public string Plot { get; set; }
        public bool Touchable { get; set; }

        public FilmData()
        {
            Touchable = true;
        }
    }
}
