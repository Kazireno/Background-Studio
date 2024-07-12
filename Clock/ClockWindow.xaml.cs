using PluginAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace Clock
{
    /// <summary>
    /// ClockWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ClockWindow : Window
    {
        public string Coordinate = "";
        public int Size = 100;
        public bool Time_Enable = true;
        public string Time_RGB = "";
        public bool Week_Enable = true;
        public string Week_RGB = "";
        public bool Year_Enable = true;
        public string Year_RGB = "";

        public bool CanClose = false;

        private static Label time_Label;
        private static Label week_Label;
        private static Label year_Label;

        public ClockWindow()
        {
            InitializeComponent();
            time_Label = Time_Label;
            week_Label = Week_Label;
            year_Label = Year_Label;
        }

        public void getConfig(string coordinate, int size, bool time_Enable, string time_RGB, bool week_Enable, string week_RGB, bool year_Enable, string year_RGB)
        {
            Coordinate = coordinate;
            Size = size;
            Time_Enable = time_Enable;
            Time_RGB = time_RGB;
            Week_Enable = week_Enable;
            Week_RGB = week_RGB;
            Year_Enable = year_Enable;
            Year_RGB = year_RGB;

            this.Top = Tools.GetCoordinateY(coordinate);
            this.Left = Tools.GetCoordinateX(coordinate);
           
            this.Height = 150 * (size * 0.02);
            this.Width = 834 * (size * 0.02);

            if (time_Enable) time_Label.Visibility = Visibility.Visible;
            else time_Label.Visibility = Visibility.Hidden;
            if (week_Enable) week_Label.Visibility = Visibility.Visible;
            else week_Label.Visibility = Visibility.Hidden;
            if (year_Enable) year_Label.Visibility = Visibility.Visible;
            else year_Label.Visibility = Visibility.Hidden;

            time_Label.Foreground = new SolidColorBrush(Color.FromArgb(
                (byte)Tools.StringToARGB(Time_RGB)[0],
                (byte)Tools.StringToARGB(Time_RGB)[1],
                (byte)Tools.StringToARGB(Time_RGB)[2],
                (byte)Tools.StringToARGB(Time_RGB)[3]));
            week_Label.Foreground = new SolidColorBrush(Color.FromArgb(
                (byte)Tools.StringToARGB(Week_RGB)[0],
                (byte)Tools.StringToARGB(Week_RGB)[1],
                (byte)Tools.StringToARGB(Week_RGB)[2],
                (byte)Tools.StringToARGB(Week_RGB)[3]));
            year_Label.Foreground = new SolidColorBrush(Color.FromArgb(
                (byte)Tools.StringToARGB(Year_RGB)[0],
                (byte)Tools.StringToARGB(Year_RGB)[1],
                (byte)Tools.StringToARGB(Year_RGB)[2],
                (byte)Tools.StringToARGB(Year_RGB)[3]));
        }

        static void StartShow(object sender, EventArgs e)
        {
            time_Label.Content = TimeString.GetTime();
            week_Label.Content = TimeString.GetWeek();
            year_Label.Content = TimeString.GetYear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dayTimer = new DispatcherTimer();
            dayTimer.Interval = TimeSpan.FromMilliseconds(10);
            dayTimer.Tick += new EventHandler(StartShow);
            dayTimer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!CanClose)
            {
                e.Cancel = true;
                Hide();
            }
            else 
            {
                base.OnClosing(e);
            }
        }
    }
}
