using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Info_Generator
{
    class Functions
    {
        public static string GrabInfo(int id)
        {
            TMDbClient client = new TMDbClient(MainWindow.APIKey);                                              //Initialise Client
            var movie = client.GetMovie(id, MovieMethods.Undefined | MovieMethods.Credits | MovieMethods.Images);
            List<Cast> cast = movie.Credits.Cast;
            if (cast.Count() != 0)
            {
                return "Movie Name: " + movie.Title + "\nTag Line: " + movie.Tagline + "\nCast Member: " + cast[1].Name;
            }
            else
            {
                return "Movie Name: " + movie.Title + "\nTag Line: " + movie.Tagline + "\nCast Member: ";
            }
        }
    }
}