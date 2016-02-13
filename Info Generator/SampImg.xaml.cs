using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace Info_Generator {
    /// <summary>
    /// Interaction logic for SampImg.xaml
    /// </summary>
    public partial class SampImg : Window {
        public List<string> samps = new List<string>();


        public SampImg () {
            InitializeComponent();
        }

        private async void addSamps_click ( object sender, RoutedEventArgs e ) {
            addSamples.Content = "Uploading.... Please Wait!";
            if ( samps.Count > 0 ) {
                foreach ( string img in samps ) {
                    var imgURL = await Functions.UploadScreenShot(img);
                    MainWindow.sampURLs.Add(imgURL);
                }
            } else {
                imgD.AppendText("Failed");
            }
            Thread.Sleep(1000);
            MessageBox.Show("Your Samples Are Now Uploaded. They Will be added when you click Generate.", "Upload Complete", MessageBoxButton.OK);
            this.Close();
        }

        private void addSamp1_click ( object sender, RoutedEventArgs e ) {
            var screenShot = Functions.AddScreenShot();
            imgA.Text = screenShot;
            samps.Add(screenShot);
        }

        private void addSamp2_click ( object sender, RoutedEventArgs e ) {
            var screenShot = Functions.AddScreenShot();
            imgB.Text = screenShot;
            samps.Add(screenShot);
        }

        private void addSamples_Click ( object sender, RoutedEventArgs e ) {

        }
    }
}
