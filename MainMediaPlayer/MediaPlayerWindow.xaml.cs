using PluginAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.Xml;
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

namespace MainMediaPlayer
{
    /// <summary>
    /// MediaPlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaPlayerWindow : Window
    {
        double x1 = SystemParameters.PrimaryScreenWidth;
        double y1 = SystemParameters.PrimaryScreenHeight;


        string FileType = "";
        string FileUri = "";
        int musicValue = 100;

        public MediaPlayerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mediaElement.Visibility = Visibility.Hidden;
            image.Visibility = Visibility.Hidden;

            this.Width = x1 + 20;
            this.Height = y1 + 20;
            this.Top = -10;
            this.Left = -10;

            mediaElement.Width = this.Width;
            mediaElement.Height = this.Height;
            image.Width = this.Width;
            image.Height = this.Height;

            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Stop;

            Launch();
        }

        public void getConfig(string fileType, string fileUri,int musicValue)
        {
            FileType = fileType;
            FileUri = fileUri;
            this.musicValue = musicValue;
        }

        public void Launch()
        {
            switch (FileType)
            {
                case "mp4":
                    VideoType();
                    break;
                case "avi":
                    VideoType();
                    break;
                case "gif":
                    VideoType();
                    break;
                case "jpg":
                    ImgType();
                    break;
                case "png":
                    ImgType();
                    break;
                case "bmp":
                    ImgType();
                    break;
                default:
                    return;
            }
        }

        public void VideoType()
        {
            mediaElement.Visibility = Visibility.Visible;
            image.Visibility = Visibility.Hidden;

            mediaElement.Source = new Uri(FileUri, UriKind.Absolute);
            image.Source = null;

            mediaElement.Width = this.Width*2;
            mediaElement.Height = y1;

            mediaElement.Volume = musicValue * 0.01;

            mediaElement.Play(); 
        }

        public void ImgType()
        {
            mediaElement.Visibility = Visibility.Hidden;
            image.Visibility = Visibility.Visible;

            image.Source = new BitmapImage(new Uri(FileUri, UriKind.Absolute));
            mediaElement.Source = null;
        }

        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = new TimeSpan(0, 0, 1);
            mediaElement.Play();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Tools.TackWindowOnBackgroun(this);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            Tools.TackWindowOnBackgroun(this);
        }
    }
}
