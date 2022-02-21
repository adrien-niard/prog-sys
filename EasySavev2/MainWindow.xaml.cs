using EasySavev2.Model;
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

namespace EasySavev2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Save> SaveList = new List<Save>();
        public MainWindow()
        {
            InitializeComponent();
        }
        //initiate the event when we click on the first browse button
        private void BrowseSrc_Click(object sender, RoutedEventArgs e)
        {
            //creating a openfilDialog instance to browse inside our file explorer
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "";
            Nullable<bool> result = openFileDialog.ShowDialog();
            //Put the file name into the source textbox
            if (result == true)
            {
                SaveSrc.Text = openFileDialog.FileName;
            }
        }
        //initiate the event when we click on the second browse button
        private void BrowseDest_Click(object sender, RoutedEventArgs e)
        {
            //creating a openfilDialog instance to browse inside our file explorer
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            if (result == true)
            {
                //Put the file name into the destination textbox
                SaveDest.Text = openFileDialog.FileName;
            }
        }

        //initiate the event when we click on the save button
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;

            foreach (Save obj in SaveList)
            {
                Log log = new Log();
                State state = new State();
                obj.Attach(log);
                obj.Attach(state);
                if (obj.Type == "Full")
                {
                    ASave ParaExec = new ParallèleExec();
                    ParaExec.ExecFull(SaveList, log, state, i);
                    MessageBox.Show("Your full save as been done");
                }
                else if (obj.Type == "Diff")
                {
                    ASave ParaExec = new ParallèleExec();
                    ParaExec.ExecDiff(SaveList, log, state, i);
                    MessageBox.Show("Your diff save as been done");
                }

                i++;
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

            MessageBox.Show("Save add in success !");

            SaveName.Clear();
            SaveDest.Clear();
            SaveSrc.Clear();
            SaveType.SelectedIndex = -1;
        }
	}
}
