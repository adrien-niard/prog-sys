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
using System.Resources;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Navigation;

namespace EasySavev2
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public static ResourceManager rm = new ResourceManager("EasySavev2.English", Assembly.GetExecutingAssembly());
        public static ResourceManager rm2 = new ResourceManager("EasySavev2.Francais", Assembly.GetExecutingAssembly());
        public Settings()
        {
            InitializeComponent();

            ConfigurationManager.RefreshSection("appSettings");
            try
            {
                if (ConfigurationManager.AppSettings.Get("langue") == "EN")
                {

                    Language.Content = rm.GetString("Language");
                    EncExt.Content = rm.GetString("EncExt");
                    PrioExt.Content = rm.GetString("PrioExt");
                    SizeMax.Content = rm.GetString("SizeMax");
                    BusinessTool.Content = rm.GetString("BusinessTool");
                    Save.Content = rm.GetString("SaveSettings");
                    Back.Content = rm.GetString("Back");
                }

                if (ConfigurationManager.AppSettings.Get("langue") == "FR")
                {
                    Language.Content = rm2.GetString("Language");
                    EncExt.Content = rm2.GetString("EncExt");
                    PrioExt.Content = rm2.GetString("PrioExt");
                    SizeMax.Content = rm2.GetString("SizeMax");
                    BusinessTool.Content = rm2.GetString("BusinessTool");
                    Save.Content = rm2.GetString("SaveSettings");
                    Back.Content = rm2.GetString("Back");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //NameValueCollection test = new NameValueCollection();
            string[] keys = ConfigurationManager.AppSettings.AllKeys;
            
            foreach (string key in keys)
            {
                
                if (keys != null)
                {
                    if (key.IndexOf(".") != -1 && key.Substring(0, key.IndexOf(".")) == "ext")
                    {                            
                        ListEncrypt.Items.Add(key.Substring(3 , key.Length - 3));                            
                    }

                    if (key.IndexOf(".") != -1 && key.Substring(0, key.IndexOf(".")) == "pri")
                    {
                        ListPriority.Items.Add(key.Substring(3, key.Length - 3));
                    }

                    SizeKo.Text = ConfigurationManager.AppSettings.Get("size");
                    LanguageBox.Text = ConfigurationManager.AppSettings.Get("langue");
                    Logicielm.Text = ConfigurationManager.AppSettings.Get("job");
                }
            }
           
        }
        
        public void startSet()
        {
            try
            {
                string[] keys2 = ConfigurationManager.AppSettings.AllKeys;
                Settings settings = new Settings();

                foreach (string key in keys2)
                {

                    if (keys2 != null)
                    {
                        if (key.IndexOf(".") != -1 && key.Substring(0, key.IndexOf(".")) == "ext")
                        {
                            settings.ListEncrypt.Items.Add(key.Substring(3, key.Length - 3));
                        }

                        settings.SizeKo.Text = ConfigurationManager.AppSettings.Get("size");
                        settings.LanguageBox.Text = ConfigurationManager.AppSettings.Get("langue");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
}
        

        private void ButAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool Exist = false;
                if (ListEncrypt != null && TextEncrypt.Text != "")
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

                if (Exist == false && TextEncrypt.Text != "")
                {

                    ListEncrypt.Items.Add(TextEncrypt.Text);
                }
                TextEncrypt.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ButDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                object selectobj = ListEncrypt.SelectedItem;
                if (ListEncrypt.SelectedItem != null)
                {
                    string selectstring = selectobj.ToString();
                    conf.AppSettings.Settings.Remove("ext" + selectstring);
                    ListEncrypt.Items.Remove(selectobj);
                    TextEncrypt.Clear();
                }

                conf.Save(ConfigurationSaveMode.Modified);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
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
                            if (keys[i] == "ext" + Element)
                            {
                                Exist = true;
                                break;
                            }
                        }

                        if (Exist == false)
                        {
                            conf.AppSettings.Settings.Add("ext" + Element, Element);
                        }
                    }
                }

                foreach (string Element2 in ListPriority.Items)
                {
                    bool Exist2 = false;

                    //var ext = ConfigurationManager.AppSettings["ext"];
                    if (Element2 != null)
                    {
                        for (int i = 0; i < keys.Length; i++)
                        {
                            if (keys[i] == "pri" + Element2)
                            {
                                Exist2 = true;
                                break;
                            }
                        }

                        if (Exist2 == false)
                        {
                            conf.AppSettings.Settings.Add("pri" + Element2, Element2);
                        }
                    }
                }

                string size = SizeKo.Text;
                conf.AppSettings.Settings.Remove("size");
                conf.AppSettings.Settings.Add("size", size);

                string job = Logicielm.Text;
                conf.AppSettings.Settings.Remove("job");
                conf.AppSettings.Settings.Add("job", job);

                string language = LanguageBox.Text;
                if (language != "")
                {
                    conf.AppSettings.Settings.Remove("langue");
                    conf.AppSettings.Settings.Add("langue", language);
                   
                }
                conf.Save(ConfigurationSaveMode.Modified);

                if (ConfigurationManager.AppSettings.Get("langue") == "EN")
                {
                    MessageBox.Show("Settings saved !");
                }

                if (ConfigurationManager.AppSettings.Get("langue") == "FR")
                {
                    MessageBox.Show("Paramètres sauvegardés !");
                }

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

            this.Visibility = Visibility.Hidden;
            
        }

        private void SizeKo_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void TextPriority_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButAdd_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                bool Exist = false;
                if (ListPriority != null && TextPriority.Text != "")
                {
                    foreach (string i in ListPriority.Items)
                    {
                        if (TextPriority.Text == i)
                        {
                            Exist = true;
                            break;
                        }
                    }
                }

                if (Exist == false && TextPriority.Text != "")
                {
                    ListPriority.Items.Add(TextPriority.Text);
                }
                TextPriority.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ButDelete_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                object selectobj = ListPriority.SelectedItem;
                if (ListPriority.SelectedItem != null)
                {
                    string selectstring = selectobj.ToString();
                    conf.AppSettings.Settings.Remove("pri" + selectstring);
                    ListPriority.Items.Remove(selectobj);
                    TextPriority.Clear();
                    conf.Save(ConfigurationSaveMode.Modified);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
