using Microsoft.Win32;
using System.Diagnostics;
using System.Printing;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TagLib.File currentFile;
        MediaPlayer myMediaPlayer;

        private string openedFilePath;

        public MainWindow()
        {
            InitializeComponent();
            myMediaPlayer = new MediaPlayer();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (currentFile != null)
            {
                myMediaPlayer.Stop();
                myMediaPlayer.Close();
                currentFile.Dispose();
            }


            //example of instantiating open fie dialog
            OpenFileDialog ofd = new OpenFileDialog();

            //show onscreen to usr
            if (ofd.ShowDialog() == true)
            {
                mediaMenuItem.Visibility = Visibility.Visible;
                openedFilePath = ofd.FileName;

                // Load the media file
                myMediaPlayer.Open(new Uri(ofd.FileName));

                // get mp3 data
                getData(ofd.FileName);
            }
        }

        private void getData(string file)
        {
            currentFile = TagLib.File.Create(file);

            // Create MediaPlayerUC with tags
            MediaPlayerUC media = new MediaPlayerUC(currentFile.Tag.Title, currentFile.Tag.FirstPerformer, currentFile.Tag.Album, currentFile.Tag.Year);

            MediaContent.Children.Clear();

            // Add MediaPlayerUC to UI
            MediaContent.Children.Add(media);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Play();
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Pause();
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Stop();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = myMediaPlayer != null && myMediaPlayer.Source != null;
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = myMediaPlayer != null && myMediaPlayer.Source != null;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = myMediaPlayer != null && myMediaPlayer.Source != null;
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != null)
            {

                EditForm.Visibility = Visibility.Visible;

                TitleTextBox.Text = currentFile.Tag.Title ?? string.Empty;
                ArtistTextBox.Text = currentFile.Tag.FirstPerformer ?? string.Empty;
                AlbumTextBox.Text = currentFile.Tag.Album ?? string.Empty;
                YearTextBox.Text = currentFile.Tag.Year.ToString();

                myMediaPlayer.Stop();
                myMediaPlayer.Close();

                currentFile.Dispose();
            }
        }


        // Save file, by taking current tex box data and svaing it to my file path i got earlier
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            currentFile = TagLib.File.Create(openedFilePath);

            currentFile.Tag.Title = TitleTextBox.Text;
            currentFile.Tag.Performers = new string[] { ArtistTextBox.Text };
            currentFile.Tag.Album = AlbumTextBox.Text;

            //if they do not input a proper year, I hard coded 2024 as the year
            if (uint.TryParse(YearTextBox.Text, out uint year))
            {
                currentFile.Tag.Year = year;
            }
            else
            {
                currentFile.Tag.Year = 2024;
            }

            currentFile.Save();
            EditForm.Visibility = Visibility.Hidden;

            // Open the updated file with the existing media player instance
            myMediaPlayer.Open(new Uri(openedFilePath));
            getData(openedFilePath);
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            EditForm.Visibility = Visibility.Hidden;

            // re-open file with the existing media player instance
            myMediaPlayer.Open(new Uri(openedFilePath));
            getData(openedFilePath);
        }

        //on exit
        private void Exit_Click (object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
