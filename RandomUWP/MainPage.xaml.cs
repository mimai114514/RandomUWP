using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace RandomUWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public int times_left = 1;
        public MainPage()
        {
            this.InitializeComponent();
        }

        public void hide_settings_ui()
        {
            ui_numbox_min.Visibility = Visibility.Collapsed;
            ui_numbox_max.Visibility = Visibility.Collapsed;
            ui_numbox_times.Visibility = Visibility.Collapsed;
            ui_text_min.Visibility = Visibility.Collapsed;
            ui_text_max.Visibility = Visibility.Collapsed;
            ui_text_times.Visibility = Visibility.Collapsed;
            start_button.Visibility = Visibility.Collapsed;
        }

        public void show_settings_ui()
        {
            ui_numbox_min.Visibility = Visibility.Visible;
            ui_numbox_max.Visibility = Visibility.Visible;
            ui_numbox_times.Visibility = Visibility.Visible;
            ui_text_min.Visibility = Visibility.Visible;
            ui_text_max.Visibility = Visibility.Visible;
            ui_text_times.Visibility = Visibility.Visible;
            start_button.Visibility = Visibility.Visible; 
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            start_button.Visibility = Visibility.Collapsed;
            times_left = Convert.ToInt32(ui_numbox_times.Text);
            random();
        }

        public void random()
        {
            hide_settings_ui();
            result_text.Visibility = Visibility.Visible;
            if (times_left == 1)
            {
                next_button.Visibility = Visibility.Collapsed;
                finish_button.Visibility = Visibility.Visible;
            }
            else
            {
                next_button.Visibility = Visibility.Visible;
                finish_button.Visibility = Visibility.Collapsed;
            }
            Random random = new Random();
            result_text.Text = "被抽中的是:";
            result_text.Text += random.Next(Convert.ToInt32(ui_numbox_min.Text), Convert.ToInt32(ui_numbox_max.Text)).ToString();
            times_left--;
        }

        private void finish_button_Click(object sender, RoutedEventArgs e)
        {
            result_text.Visibility = Visibility.Collapsed;
            show_settings_ui();
            finish_button.Visibility = Visibility.Collapsed;
        }

        private void next_button_Click(object sender, RoutedEventArgs e)
        {
            random();
        }
    }


}
