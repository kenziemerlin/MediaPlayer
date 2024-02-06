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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayerApplication
{
    /// <summary>
    /// Interaction logic for MediaPlayerUC.xaml
    /// </summary>
    public partial class MediaPlayerUC : UserControl
    {

        public string? SongTitle { get; set; }
        public string? Artist { get; set; }
        public string? Album { get; set; }
        public uint? Year { get; set; }

        public Image? image { get; set; }

        public MediaPlayerUC()
        {
            InitializeComponent();
        }

        public MediaPlayerUC(string songTitle, string artist, string album, uint year, Image? myImage)
        {
            SongTitle = songTitle;
            Artist = artist;
            Album = album;
            Year = year;
            image = myImage;

            InitializeComponent();

            TitleLabel.Content = this.SongTitle;
            ArtistLabel.Content = this.Artist;
            AlbumLabel.Content = this.Album;
            YearLabel.Content = this.Year;
            


        }
    }
}
