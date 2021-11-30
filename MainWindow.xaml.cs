using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

namespace ModFinder_WOTR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModListData installedModList = new();

        public MainWindow()
        {
            InitializeComponent();

            installedModList.Items.Add(new ModDetails
            {
                Name = "BubbleGuns",
                Version = "0.0.1",
                ModType = "UMM",
                Source = "Local",
                CanInstall = false,
            });
            installedModList.Items.Add(new ModDetails
            {
                Name = "RespecModWrath",
                Version = "4.2.0",
                ModType = "UMM",
                Source = "Github",
                CanInstall = true,
            });
            installedModList.Items.Add(new ModDetails
            {
                Name = "Test Mod",
                Version = "1.2.3",
                ModType = "UMM",
                Source = "Nexus",
                CanInstall = false,
            });

            installedMods.DataContext = installedModList;
            showInstalledToggle.DataContext = installedModList;


            // Do magic window dragging regardless where they click
            MouseDown += (sender, e) =>
            {
                if (e.ChangedButton == MouseButton.Left)
                    DragMove();
            };

            // Close button
            closeButton.Click += (sender, e) =>
            {
                Close();
            };

            // Drag drop nonsense
            dropTarget.Drop += DropTarget_Drop;
            dropTarget.DragOver += DropTarget_DragOver;
        }

        private void DropTarget_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            if (e.Data.GetFormats().Any(f => f == DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.All(f => CheckIsMod(f)))
                {
                    e.Effects = DragDropEffects.Copy;
                }
            }
            e.Handled = true;
        }

        private void DropTarget_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string f in files)
            {
                //BARLEY CODE HERE TO INSTALL FILE
            }
        }

        public static bool CheckIsMod(string path)
        {
            if (!File.Exists(path))
                return false;

            if (System.IO.Path.GetExtension(path) != ".zip")
                return false;

            //BARLEY CODE HERE

            return true;
        }

        private void InstallOrUpdateMod(object sender, RoutedEventArgs e)
        {
            ModDetails toInstall = (sender as Button).Tag as ModDetails;
            //BARLEY CODE HERE
        }
    }

    public class ModListData : INotifyPropertyChanged
    {
        private bool _ShowInstalled;
        public bool ShowInstalled
        {
            get => _ShowInstalled;
            set
            {
                _ShowInstalled = value;
                PropertyChanged?.Invoke(this, new(nameof(ShowInstalled)));
                PropertyChanged?.Invoke(this, new(nameof(HeaderNameText)));
            }
        }
        public ObservableCollection<ModDetails> Items { get; set; } = new();
        public string HeaderNameText => ShowInstalled ? "Update" : "Install";

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ModDetails
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string ModType { get; set; }
        public string Source { get; set; }

        //BARLEY CODE HERE
        public bool CanInstall { get; set; }

        public string InstallButtonText => CanInstall ? "v0.06.11" : "up to date";

    }
}
