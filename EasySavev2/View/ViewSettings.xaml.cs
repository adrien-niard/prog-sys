using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Collections.Specialized;
using System.Diagnostics;

namespace EasySavev2
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            //NameValueCollection test = new NameValueCollection();
            string[] keys = ConfigurationManager.AppSettings.AllKeys;
            foreach (string key in keys)
            {
                if (keys != null)
                {
                    if (key.IndexOf(".") == 0)
                    {
                        ListEncrypt.Items.Add(key);
                    }

                    SizeKo.Text = ConfigurationManager.AppSettings.Get("size");
                    LanguageBox.Text = ConfigurationManager.AppSettings.Get("langue");
                }
            }
        }
        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            bool Exist = false;
            if (ListEncrypt != null && TextEncrypt.Text != null)
            {
                foreach (string i in ListEncrypt.Items)
                {
                    if (TextEncrypt.Text == i)
                    {
                        Exist = true;
                        break;
                    }
                }
            }

            if (Exist == false && TextEncrypt.Text != null)
            {
                ListEncrypt.Items.Add(TextEncrypt.Text);
            }
            TextEncrypt.Clear();
        }

        private void ButDelete_Click(object sender, RoutedEventArgs e)
        {
            ListEncrypt.Items.Remove(ListEncrypt.SelectedItem);
            TextEncrypt.Clear();

            Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            foreach (string Element in ListEncrypt.Items)
            {
                //var ext = ConfigurationManager.AppSettings["ext"];

                conf.AppSettings.Settings.Remove(Element);
            }
            conf.Save(ConfigurationSaveMode.Modified);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListEncrypt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ListBox ListEncrypt = new ListBox();
            //Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        private void SaveSet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                string[] keys = ConfigurationManager.AppSettings.AllKeys;

                foreach (string Element in ListEncrypt.Items)
                {
                    bool Exist = false;

                    //var ext = ConfigurationManager.AppSettings["ext"];
                    if (Element != null)
                    {
                        for (int i = 0; i < keys.Length; i++)
                        {
                            if (keys[i] == Element)
                            {
                                Exist = true;
                                break;
                            }
                        }

                        if (Exist == false)
                        {
                            conf.AppSettings.Settings.Add(Element, Element);
                        }
                    }
                }

                string size = SizeKo.Text;
                conf.AppSettings.Settings.Remove("size");
                conf.AppSettings.Settings.Add("size", size);
                conf.Save(ConfigurationSaveMode.Modified);

                string language = LanguageBox.Text;
                if (language != "")
                {
                    conf.AppSettings.Settings.Remove("langue");
                    conf.AppSettings.Settings.Add("langue", language);
                    conf.Save(ConfigurationSaveMode.Modified);
                }

                System.Windows.MessageBox.Show("Settings saved");

                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void ButBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void SizeKo_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
