
using Fluent;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using MessageBox = System.Windows.Forms.MessageBox;

namespace Project_batch_rename_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :RibbonWindow
    {
        BindingList<string> itemTypes;
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<FileInOS> _files = new ObservableCollection<FileInOS>();
        ObservableCollection<FolderInOS> _folders = new ObservableCollection<FolderInOS>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemTypes = new BindingList<string>()
            {
                "File", "Folder"
            };
            _files = new ObservableCollection<FileInOS>()
            {
  
            };
            _folders = new ObservableCollection<FolderInOS>()
            {
      
            };

            typeComboBox.ItemsSource = itemTypes;
            ItemListView.ItemsSource = _files;
          
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
          ItemListView.ItemsSource = _files;
          
        }

        private void typeComboBox_DropDownClosed(object sender, EventArgs e)
        {

            if (typeComboBox.SelectedItem == null)
                return;
            if (typeComboBox.SelectedItem == "File")
                ItemListView.ItemsSource = _files;
            if (typeComboBox.SelectedItem == "Folder")
                ItemListView.ItemsSource = _folders;
        }

        private void ItemsDrop(object sender, System.Windows.DragEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void addItems(object sender, RoutedEventArgs e)
        {
            if (typeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please choose item type (files or folders)", "Error");
                return;
            }
            if (typeComboBox.SelectedItem.ToString() == "File")
            {
                ItemListView.ItemsSource = _files;
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
                            NewFilename = "",
                            Pathname = screen.FileNames.ToArray()[i],
                            Error = "",
                            Status = 0
                        });
                    }

                }
              

                MessageBox.Show(screen.FileNames.Count() + " file(s) Added Successfully", "Success");
            }
            else if (typeComboBox.SelectedItem.ToString() == "Folder")
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();

                int counter = 0;
                if (System.Windows.Forms.DialogResult.OK == result)
                {
                    ItemListView.ItemsSource = _folders;

                    string path = dialog.SelectedPath + "\\";
                    string[] folders = Directory.GetDirectories(path);
                    List<FolderInOS> newFoldernames = new List<FolderInOS>();

                    foreach (var folder in folders)
                    {
                        bool isExisted = false;
                        string currentName = folder.Remove(0, path.Length);

                        foreach (var foldername in _folders)
                        {
                            if (foldername.Filename == currentName && foldername.Pathname == path)
                            {
                                isExisted = true;
                                break;
                            }
                        }

                        if (!isExisted)
                        {
                            newFoldernames.Add(new FolderInOS() { Filename = currentName, Pathname = path });
                            counter++;
                        }
                    }
                    foreach (var newFoldername in newFoldernames)
                        _folders.Add(newFoldername);

                    MessageBox.Show(counter + " folder(s) Added Successfully", "Success");
                }

            }
        }
    }
}
