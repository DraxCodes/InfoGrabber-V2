using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using System.IO;

namespace Info_Generator {
    class Functions { 
        public static string GrabInfo ( int id ) {
            TMDbClient client = new TMDbClient(MainWindow.APIKey);                                              //Initialise Client
            var movie = client.GetMovie(id, MovieMethods.Undefined | MovieMethods.Credits | MovieMethods.Images);
            List<Cast> cast = movie.Credits.Cast;
            if ( cast.Count() != 0 && movie.Tagline != "" ) {
                return "Movie Name: " + movie.Title + "\nTag Line: " + movie.Tagline + "\nCast Member: " + cast[0].Name;
            } else if ( movie.Tagline == "" && cast.Count > 0 ) {
                return "Movie Name: " + movie.Title + "\nTag Line: None" + "\nCast Member: " + cast[0].Name;
            } else if ( cast.Count == 0 && movie.Tagline != "" ) {
                return "Movie Name: " + movie.Title + "\nTag Line: " + movie.Tagline + "\nCast Member: None";
            } else {
                return "Movie Name: " + movie.Title + "\nTag Line: None" + "\nCast Member: None";
            }
        }

        public static string AddScreenShot () {
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string img = "No valid image selected!";

            // Set filter options and filter index.
            openFileDialog1.Filter = "PNG Files (.png)|*.png|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            // Call the ShowDialog method to show the dialog box.
            bool? userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if ( userClickedOK == true ) {
                string text = openFileDialog1.FileName;
                {
                    // Read the first line from the file and write it the textbox.
                    img = text;
                }
            }
            return img;
        }

        public static async Task<string> UploadScreenShot ( string location ) {
            const string _secret = "29a9c0cdbda3f744c293b5654bd5dfb974b5ea58";
            const string _id = "3a282605f7d510e";
            string url;
            var client = new ImgurClient(_id, _secret);
            var imageEndpoint = new ImageEndpoint(client);
            var localImage = File.ReadAllBytes(location);
            var image = await imageEndpoint.UploadImageBinaryAsync(localImage);
            url = image.Link;
            return url;
        }
    }

}