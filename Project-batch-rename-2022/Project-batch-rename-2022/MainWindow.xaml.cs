using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Project_batch_rename_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<FileInOS> _files = new ObservableCollection<FileInOS>();
        ObservableCollection<FolderInOS> _folders = new ObservableCollection<FolderInOS>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            _files = new ObservableCollection<FileInOS>()
            {
  
            };
            _folders = new ObservableCollection<FolderInOS>()
            {
      
            };

            FilesListView.ItemsSource = _files;
            FoldersListView.ItemsSource = _folders;
        }

        private void selectFilesBtn_click(object sender, RoutedEventArgs e)
        {
            var screen = new CommonOpenFileDialog();
            screen.IsFolderPicker = false;
            screen.Multiselect = true;
      
            screen.Filters.Add(
               new CommonFileDialogFilter("All files", "*.*")
            );

            if (screen.ShowDialog() == CommonFileDialogResult.Ok)
            {

                for (int i = 0; i < screen.FileNames.Count(); i++)
                {
                    String[] filenameSplit = screen.FileNames.ToArray()[i].Split('\\');
                    String filename = filenameSplit[filenameSplit.Length - 1];

                    _files.Add(new FileInOS
                    {
                        Filename = filename,
                        NewFilename = filename,
                        Pathname = screen.FileNames.ToArray()[i],
                        Error = "",
                        Status = 0
                    });
                }

            }
        }

        private void addFolders_btn_Click(object sender, RoutedEventArgs e)
        {
            var screen = new FolderBrowserDialog();
            
            // Show the FolderBrowserDialog.
            DialogResult result = screen.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string folderName = screen.SelectedPath;
                String[] filenameSplit = folderName.Split('\\');
                String foldername = filenameSplit[filenameSplit.Length - 1];
                _folders.Add(new FolderInOS
                {
                    Filename = foldername,
                    NewFilename = foldername,
                    Pathname = folderName,
                    Error = "",
                    Status = 0
                }); 
    }

        }

        private void refesh_click(object sender, RoutedEventArgs e)
        {
            _files = new ObservableCollection<FileInOS>()
            {

            };
            _folders = new ObservableCollection<FolderInOS>()
            {

            };
            FilesListView.ItemsSource = _files;
            FoldersListView.ItemsSource = _folders;
        }
    }
}
