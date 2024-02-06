using Microsoft.Win32;
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

        public MainWindow()
        {
            InitializeComponent();
            myMediaPlayer = new MediaPlayer();
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //example of instantiating open fie dialog
            OpenFileDialog ofd = new OpenFileDialog();

            //show onscreen to usr
            if (ofd.ShowDialog() == true)
            {
                mediaMenuItem.Visibility = Visibility.Visible;

                currentFile = TagLib.File.Create(ofd.FileName);
                MediaPlayerUC media = new MediaPlayerUC(currentFile.Tag.Title, currentFile.Tag.FirstPerformer, currentFile.Tag.Album, currentFile.Tag.Year, null);

                MediaContent.Children.Add(media);
            }
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Open(new Uri(currentFile.Name));
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
    }
}