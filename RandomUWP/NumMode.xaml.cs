using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
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

namespace RandomUWP
{

    public sealed partial class NumMode : Page
    {
        public int count = 1;
        public int count_left = 1;
        public int[] chosen = new int[1000];
        public int chosenNum;
        public NumMode()
        {
            this.InitializeComponent();
        }


        public void hide_settings_ui()
        {
            ui_numbox_min.Visibility = Visibility.Collapsed;
            ui_numbox_max.Visibility = Visibility.Collapsed;
            ui_numbox_count.Visibility = Visibility.Collapsed;
            ui_text_min.Visibility = Visibility.Collapsed;
            ui_text_max.Visibility = Visibility.Collapsed;
            ui_text_count.Visibility = Visibility.Collapsed;
            ui_text_min_mini.Visibility = Visibility.Collapsed;
            ui_text_max_mini.Visibility = Visibility.Collapsed;
            ui_text_count_mini.Visibility = Visibility.Collapsed;
            ui_min_border.Visibility = Visibility.Collapsed;
            ui_max_border.Visibility = Visibility.Collapsed;
            ui_count_border.Visibility = Visibility.Collapsed;
            start_button.Visibility = Visibility.Collapsed;
        }

        public void show_settings_ui()
        {
            ui_numbox_min.Visibility = Visibility.Visible;
            ui_numbox_max.Visibility = Visibility.Visible;
            ui_numbox_count.Visibility = Visibility.Visible;
            ui_text_min.Visibility = Visibility.Visible;
            ui_text_max.Visibility = Visibility.Visible;
            ui_text_count.Visibility = Visibility.Visible;
            ui_text_min_mini.Visibility = Visibility.Visible;
            ui_text_max_mini.Visibility = Visibility.Visible;
            ui_text_count_mini.Visibility = Visibility.Visible;
            ui_min_border.Visibility = Visibility.Visible;
            ui_max_border.Visibility = Visibility.Visible;
            ui_count_border.Visibility = Visibility.Visible;
            start_button.Visibility = Visibility.Visible;
        }

        private void start_button_Click(object sender, RoutedEventArgs e)
        {
            start_button.Visibility = Visibility.Collapsed;
            count = count_left = Convert.ToInt32(ui_numbox_count.Text);
            random();
        }

        public void random()
        {
            hide_settings_ui();
            if (count_left == 0 && count != 1)
            {
                next_button.Visibility = Visibility.Collapsed;
                finish_button.Visibility = Visibility.Visible;
                result_text.Text = "本轮被抽中的有：\n";
                for (int i = 1; i <= count; i++)
                {
                    result_text.Text += chosen[i-1] + "、";
                    if (i % 4 == 0)
                    {
                        result_text.Text += "\n";
                    }
                }
                result_text.Text = result_text.Text.Substring(0, result_text.Text.Length - 1);
                if(count % 4 ==0)
                    result_text.Text = result_text.Text.Substring(0, result_text.Text.Length - 1);
                result_text.Text += "。";
            }
            else if (count == 1)
            {
                next_button.Visibility = Visibility.Collapsed;
                finish_button.Visibility = Visibility.Visible;
                Random random = new Random();
                result_text.Text = "被抽中的是:";
                if (Convert.ToInt32(ui_numbox_min.Text) > Convert.ToInt32(ui_numbox_max.Text))
                {
                    int temp = Convert.ToInt32(ui_numbox_min.Text);
                    ui_numbox_min.Text = ui_numbox_max.Text;
                    ui_numbox_max.Text = temp.ToString();
                }
                chosenNum = random.Next(Convert.ToInt32(ui_numbox_min.Text), Convert.ToInt32(ui_numbox_max.Text));
                result_text.Text += chosenNum.ToString();
                chosen[count - count_left] = chosenNum;
            }
            else
            {
                next_button.Visibility = Visibility.Visible;
                finish_button.Visibility = Visibility.Collapsed;
                Random random = new Random();
                result_text.Text = "被抽中的是:";
                if (Convert.ToInt32(ui_numbox_min.Text) > Convert.ToInt32(ui_numbox_max.Text))
                {
                    int temp = Convert.ToInt32(ui_numbox_min.Text);
                    ui_numbox_min.Text = ui_numbox_max.Text;
                    ui_numbox_max.Text = temp.ToString();
                }
                chosenNum = random.Next(Convert.ToInt32(ui_numbox_min.Text), Convert.ToInt32(ui_numbox_max.Text));
                result_text.Text += chosenNum.ToString();
                chosen[count - count_left] = chosenNum;
            }
            result_text.Visibility = Visibility.Visible;
            count_left--;
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
