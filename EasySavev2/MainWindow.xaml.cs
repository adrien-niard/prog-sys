using EasySavev2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace EasySavev2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Save> SaveList = new List<Save>();
        public static ResourceManager rm = new ResourceManager("EasySavev2.English", Assembly.GetExecutingAssembly());
        public static ResourceManager rm2 = new ResourceManager("EasySavev2.Francais", Assembly.GetExecutingAssembly());
        public MainWindow()
        {
            InitializeComponent();
            ConfigurationManager.RefreshSection("appSettings");
            try
            {
                if (ConfigurationManager.AppSettings.Get("langue") == "EN")
                {

                    SaveNameLabel.Content = rm.GetString("Name");
                    SaveSrcLabel.Content = rm.GetString("Source");
                    SaveDestLabel.Content = rm.GetString("Destination");
                    SavetypeLabel.Content = rm.GetString("Type");
                    BrowseSrc.Content = rm.GetString("Browse");
                    BrowseDest.Content = rm.GetString("Browse");
                    AddSave.Content = rm.GetString("Add");
                    Save.Content = rm.GetString("Save");
                }

                if (ConfigurationManager.AppSettings.Get("langue") == "FR")
                {
                    SaveNameLabel.Content = rm2.GetString("Name");
                    SaveSrcLabel.Content = rm2.GetString("Source");
                    SaveDestLabel.Content = rm2.GetString("Destination");
                    SavetypeLabel.Content = rm2.GetString("Type");
                    BrowseSrc.Content = rm2.GetString("Browse");
                    BrowseDest.Content = rm2.GetString("Browse");
                    AddSave.Content = rm2.GetString("Add");
                    Save.Content = rm2.GetString("Save");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        //initiate the event when we click on the first browse button
        private void BrowseSrc_Click(object sender, RoutedEventArgs e)
        {
            //Use openFolderDialog to select a folder
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            DialogResult result = openFolderDialog.ShowDialog();

            if (result.ToString() == "OK")
            {
                SaveSrc.Text = openFolderDialog.SelectedPath + @"\";
            }
        }
        //initiate the event when we click on the second browse button
        private void BrowseDest_Click(object sender, RoutedEventArgs e)
        {
            //Use openFolderDialog to select a folder
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            DialogResult result = openFolderDialog.ShowDialog();

            if (result.ToString() == "OK")
            {
                SaveDest.Text = openFolderDialog.SelectedPath + @"\";
            }
        }

        //initiate the event when we click on the save button
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //création, initialisation et mise à jour de l'objet BackgroundWorker
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();

            int NbSave = 0;

            foreach (Save obj in SaveList)
            {
                Log log = new Log();
                State state = new State();
                obj.Attach(log);
                obj.Attach(state);

                DirectoryInfo di = new DirectoryInfo(obj.Src);
                int NbFile = di.GetFiles("*.*", SearchOption.AllDirectories).Length;

                if (obj.Type == "Full")
                {
                    ASave ParaExec = new ParallèleExec();
                    ParaExec.ExecFull(SaveList, log, state, NbFile, NbSave); ;
                    System.Windows.MessageBox.Show("Your full save as been done");
                }
                else if (obj.Type == "Diff")
                {
                    ASave ParaExec = new ParallèleExec();
                    ParaExec.ExecDiff(SaveList, log, state, NbFile, NbSave);
                    System.Windows.MessageBox.Show("Your diff save as been done");
                }

                NbSave++;
            }
            Environment.Exit(0);
        }

        private void AddSave_Click(object sender, RoutedEventArgs e)
        {
            Save save = new Save();

            save.Name = SaveName.Text;
            save.Src = SaveSrc.Text;
            save.Dest = SaveDest.Text;
            save.Type = SaveType.Text;
            save.FileSize = save.GetFileSize();
            save.Time = save.GetTime();
            SaveList.Add(save);

            System.Windows.MessageBox.Show("Save add in success !");

            SaveName.Clear();
            SaveDest.Clear();
            SaveSrc.Clear();
            SaveType.SelectedIndex = -1;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
			this.Visibility = Visibility.Hidden;
            settings.Show();
        }

        // Méthode qui initialise la barre de progression 
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //initialisation de la barre de progression avec le pourcentage de progression
            PB.Value = e.ProgressPercentage;

            //Affichage de la progression sur un label
            percent.Content = PB.Value.ToString() + "%";
        }
    }
}