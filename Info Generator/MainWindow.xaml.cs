using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace Info_Generator
{
    ///-------------------------------------------------
    /// Movie Info Grabber v2.0 - Created By Warzone22
    /// Main Purpose is to gather information on selected Movies
    /// The Produces information such as Main Movie Immage, Plot, Cast (with images) & Trailer
    /// Converts Said Information into NFO or BBCode Format for Uploads
    /// -------------------- TODO -----------------------
    /// Grab Movie Info (Main Movie Immage, Plot, Cast (with images) & Trailer)
    /// Create Local File Upload Capabilities
    /// Add Option for BBCode or NFO Output
    ///-------------------------------------------------- 

    public partial class MainWindow : Window
    {
        public static string APIKey = "630abcbc6e2239281d940e64c816e716";
        public TMDbClient client = new TMDbClient(APIKey);                                              //Initialise Client
        public List<int> id = new List<int>();                                                          //Public Id List (Used for selecting further infomation)
        public int selection, selected;

        public MainWindow()
        {
            InitializeComponent();
        }

        //----------------------------------------
        // May Remove (Used to bring up Sample Images Window)
        //----------------------------------------
        private void sampImg_Click(object sender, RoutedEventArgs e)
        {
            var addSamps = new SampImg();
            addSamps.Show();

        }

        //----------------------------------------
        // On click event action for Search button
        //----------------------------------------
        private void search_Click(object sender, RoutedEventArgs e)
        {
            try                                                                                                                 //Try search function (on click)
            {                                                                                                                   //-------------------------------------------------
                _search(textBoxSearch.Text);                                                                                    //Run Search Function Using Entered Search
            }                                                                                                                   //-------------------------------------------------
            catch (NullReferenceException)                                                                                      //Catch Error if nothing is enetered
            {                                                                                                                   //-------------------------------------------------
                MessageBox.Show(string.Format("Don't forget to search for the movie you want data for!"));                      //Throw Messagebox if nothing entered (Excetpion Triggered)
            }
        }


        //----------------------------------------
        // When combobox text changes
        // (Movie Selection is made)
        // The Selected Index of the Movie within the Combobox is saved as a variable
        // This variable is then used to select the correct ID from the ID List
        //----------------------------------------
        private void movieList_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToString(movieList.Text) == "")
            {
                id_debug.Text = "Cleared";
            }
            else
            {
                selected = movieList.SelectedIndex;
                id_debug.Text = Convert.ToString(id[selected]);
                selection = id[selected];
            }
        }

        //---------------------------------------
        // Search Function (Further Notes Within)
        //---------------------------------------
        public void _search(string searchName)
        {
            movieList.Items.Clear();
            id.Clear();                                                                                                         //Clear Id List For Renewal
            char[] del = { ' ' };                                                                                               //Spliting Chars variable (Split by white space)
            SearchContainer<SearchMovie> results = client.SearchMovie(searchName, 0, true, 0);                                  //Results list with all search results
            foreach (SearchMovie result in results.Results)                                                                     //Loop to add each list item to combobox
            {
                id.Add(result.Id);                                                                                              //Add Movie ID to List    
                string originalRelDate = Convert.ToString(result.ReleaseDate);                                                  //Variable for OriginalRelease date :: Format (04/04/1991 00:00:00)
                string[] splitRelDates = originalRelDate.Split(del);                                                            //Split OriginalDate Variable by spaces, add to array
                string relDate = splitRelDates[0];                                                                              //Select only first part of array from split
                movieList.Items.Add(result.OriginalTitle + " :: " + relDate);                                                   //Final print out in combobox :: Format (Avatar :: 08/12/2009)
            }
        }



        private void info_Grab_Click(object sender, RoutedEventArgs e)
        {
            txtTest.Text = (Functions.GrabInfo(selection));
        }
    }
}

