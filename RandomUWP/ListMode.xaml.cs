using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Composition.Interactions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RandomUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListMode : Page
    {


        public ListMode()
        {
            this.InitializeComponent();
        }

        public int count_left = 1;
        
        public int listCount = 0;
        public int listReadingCount = 0;
        public string[] listName = new string[100];
        public int[] itemCount = new int[100];
        public int itemReadingCount = 0;
        public string[,] itemName = new string[100, 1000];

        public int count = 0;
        public string[] chosenItem = new string[1000];

        public void hide_settings_ui()
        {
            ui_combobox_list.Visibility = Visibility.Collapsed;
            ui_numbox_count.Visibility = Visibility.Collapsed;
            ui_text_count.Visibility = Visibility.Collapsed;
            ui_text_list.Visibility = Visibility.Collapsed;
            ui_list_border.Visibility = Visibility.Collapsed;
            ui_count_border.Visibility = Visibility.Collapsed;
            start_button.Visibility = Visibility.Collapsed;
        }

        public void show_settings_ui()
        {
            ui_combobox_list.Visibility = Visibility.Visible;
            ui_numbox_count.Visibility = Visibility.Visible;
            ui_text_count.Visibility = Visibility.Visible;
            ui_text_list.Visibility = Visibility.Visible;
            ui_list_border.Visibility = Visibility.Visible;
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
            int chosenListIndex= ui_combobox_list.SelectedIndex+1;
            if (count_left == 0 && count != 1)
            {

                next_button.Visibility = Visibility.Collapsed;
                finish_button.Visibility = Visibility.Visible;
                result_text.Text = "本轮被抽中的有：\n";
                for (int i = 1; i <= count; i++)
                {
                    result_text.Text += chosenItem[i-1] + "、";
                    if (i % 4 == 0)
                    {
                        result_text.Text += "\n";
                    }
                }
                result_text.Text = result_text.Text.Substring(0, result_text.Text.Length - 1);
                if (count % 4 == 0)
                    result_text.Text = result_text.Text.Substring(0, result_text.Text.Length - 1);
                result_text.Text += "。";
                result_text.Visibility = Visibility.Visible;
            }
            else if (count == 1)
            {
                next_button.Visibility = Visibility.Collapsed;
                finish_button.Visibility = Visibility.Visible;
                Random random = new Random();
                result_text.Text = "被抽中的是:";
                int chosenItemIndex = random.Next(1, itemCount[chosenListIndex] + 1);
                result_text.Text += itemName[chosenListIndex, chosenItemIndex];
                chosenItem[count - count_left] = itemName[chosenListIndex, chosenItemIndex];
                result_text.Visibility = Visibility.Visible;
            }
            else
            {
                next_button.Visibility = Visibility.Visible;
                finish_button.Visibility = Visibility.Collapsed;
                Random random = new Random();
                result_text.Text = "被抽中的是:";
                int chosenItemIndex = random.Next(1, itemCount[chosenListIndex] + 1);
                result_text.Text += itemName[chosenListIndex, chosenItemIndex];
                chosenItem[count - count_left] = itemName[chosenListIndex, chosenItemIndex];
                result_text.Visibility = Visibility.Visible;
            }
            


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

        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                string listFilePath = UserDataPaths.GetDefault().Documents + @"\dev\RandomUWP\list.txt";
                StorageFile listFile = await StorageFile.GetFileFromPathAsync(listFilePath);
                IList<string> list = await FileIO.ReadLinesAsync(listFile);
                listCount = Convert.ToInt16(list[0]);
                int current_line = 0;
                for (listReadingCount = 1; listReadingCount <= listCount; listReadingCount++)
                {
                    listName[listReadingCount] = list[++current_line];
                    ui_combobox_list.Items.Add(listName[listReadingCount]);
                    itemCount[listReadingCount] = Convert.ToInt32(list[++current_line]);
                    for (itemReadingCount = 1; itemReadingCount <= itemCount[listReadingCount]; itemReadingCount++)
                    {
                        itemName[listReadingCount, itemReadingCount] = list[++current_line];
                    }
                }

            }
            catch(FileNotFoundException)
            {
                hide_settings_ui();
                result_text.Text = "未找到列表文件";
                result_text.Visibility = Visibility.Visible;
            }
            catch (FormatException)
            {
                hide_settings_ui();
                result_text.Text = "列表文件格式错误";
                result_text.Visibility = Visibility.Visible;
            }

        }
    }

}
