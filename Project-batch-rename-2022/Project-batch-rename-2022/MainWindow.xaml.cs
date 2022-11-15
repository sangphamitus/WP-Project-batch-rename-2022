
using Fluent;
using IRule;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Xps;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using MessageBox = System.Windows.Forms.MessageBox;
using Path = System.IO.Path;

namespace Project_batch_rename_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {

        //project handle
        private string currentProjectName = "Unsaved Project";
        private string currentProjectPath = "autosave.proj";
        private string currentPresetPath = "";

        //end project handle


        BindingList<string> itemTypes;
        BindingList<string> ruleNames;
        BindingList<string> conflictHandle;
        //  BindingList<Rules> _rulesList= new BindingList<Rules>();
        BindingList<IRules> _rulesList = new BindingList<IRules>();
        RuleFactory _ruleFactory = RuleFactory.NewInstance();

        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<FileInOS> _files = new ObservableCollection<FileInOS>();
        ObservableCollection<FolderInOS> _folders = new ObservableCollection<FolderInOS>();
        ObservableCollection<FileChange> _fullList = new ObservableCollection<FileChange>();
        ObservableCollection<FileChange> _fullListResult = new ObservableCollection<FileChange>();
        /// <summary>
        /// rules goes here
        /// </summary>


        ObservableCollection<object> _ruleList = new ObservableCollection<object>();


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemTypes = new BindingList<string>()
            {
                "File",
                "Folder",
                "All Files in Folder (Recursive)",
                "All Folders in Folder (Recursive)",
                "All File&Folders in Folder (Recursive)"

            };
            conflictHandle = new BindingList<string>()
            {
                "None",
                "Override",
                "Skip"

            };

            this.Title = currentProjectName;
            //ruleNames = new BindingList<string>()
            //{
            //    "Change extension",
            //     "Add Counter",
            //     "Trim",
            //     "Remove white space",
            //     "Replace character",
            //     "Add a prefix",
            //     "Add a subffix",
            //     "Lowercase",
            //     "PascalCase"

            //};

            _files = new ObservableCollection<FileInOS>()
            {

            };
            _folders = new ObservableCollection<FolderInOS>()
            {

            };

            _ruleFactory.Inject(AddCounter.AddCounterAsSuffix.ruleName, new AddCounter.AddCounterAsSuffix());
            _ruleFactory.Inject(AddCounter.AddCounterAsPrefix.ruleName, new AddCounter.AddCounterAsPrefix());
            _ruleFactory.Inject(AddPrefix.AddPrefix.ruleName, new AddPrefix.AddPrefix());
            _ruleFactory.Inject(AddSuffix.AddSuffix.ruleName, new AddSuffix.AddSuffix());
            _ruleFactory.Inject(ChangeExtension.ChangeExtension.ruleName, new ChangeExtension.ChangeExtension());
            _ruleFactory.Inject(DeleteWhiteSpace.DeleteWhiteSpace.ruleName, new DeleteWhiteSpace.DeleteWhiteSpace());
            _ruleFactory.Inject(LowerCase.LowerCase.ruleName, new LowerCase.LowerCase());
            _ruleFactory.Inject(PascalCase.PascalCase.ruleName, new PascalCase.PascalCase());
            _ruleFactory.Inject(RemoveSpace.RemoveSpace.ruleName, new RemoveSpace.RemoveSpace());
            _ruleFactory.Inject(ReplaceCharaters.ReplaceCharacters.ruleName, new ReplaceCharaters.ReplaceCharacters());


            ruleNames = _ruleFactory.listKeys();
            typeComboBox.ItemsSource = itemTypes;
            //  ItemListView.ItemsSource = _files;
            ItemListView.ItemsSource = _fullList;
            rulesComboBox.ItemsSource = ruleNames;
            chosenRulesListView.ItemsSource = _rulesList;
            conflictComboBox.ItemsSource = conflictHandle;


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
                    Type = "Folder",
                    Result = "",
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
            else
            {
                ItemListView.ItemsSource = _fullList;
            }
            //if (typeComboBox.SelectedItem == "File")
            //    ItemListView.ItemsSource = _files;
            //if (typeComboBox.SelectedItem == "Folder")
            //    ItemListView.ItemsSource = _folders;
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
                // ItemListView.ItemsSource = _files;
                var screen = new CommonOpenFileDialog();
                screen.IsFolderPicker = false;
                screen.Multiselect = true;

                screen.Filters.Add(
                   new CommonFileDialogFilter("All files", "*.*")
                );

                if (screen.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    int count = 0;

                    for (int i = 0; i < screen.FileNames.Count(); i++)
                    {
                        String[] filenameSplit = screen.FileNames.ToArray()[i].Split('\\');
                        String filename = filenameSplit[filenameSplit.Length - 1];
                        bool isExist = false;

                        if (_fullList.Where(X => X.getType() == "File" && ((FileInOS)X)!.Pathname == screen.FileNames.ToArray()[i]).FirstOrDefault() == null)
                        {
                            count++;
                            _fullList.Add(new FileInOS
                            {
                                Filename = filename,
                                NewFilename = filename,
                                Pathname = screen.FileNames.ToArray()[i],
                                Type = "File",
                                Result = "",
                                Status = 0
                            });
                        }

                    }
                    MessageBox.Show(count + " file(s) Added Successfully", "Success");
                }

                // if(screen.FileNames!= null)
                //  MessageBox.Show(screen.FileNames.Count() + " file(s) Added Successfully", "Success");
            }
            else if (typeComboBox.SelectedItem.ToString() == "Folder")
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();

                int counter = 0;
                if (System.Windows.Forms.DialogResult.OK == result)
                {
                    // ItemListView.ItemsSource = _folders;

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
                            newFoldernames.Add(new FolderInOS()
                            {
                                Filename = currentName,
                                NewFilename = currentName,
                                Pathname = path + currentName,
                                Type = "Folder"
                            });
                            counter++;
                        }
                    }
                    foreach (var newFoldername in newFoldernames)
                    {
                        _fullList.Add(newFoldername);
                        _folders.Add(newFoldername);
                    }

                    MessageBox.Show(counter + " folder(s) Added Successfully", "Success");
                }

            }
            else if (typeComboBox.SelectedItem.ToString() == "All Files in Folder (Recursive)")
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();

                int counter = 0;
                if (System.Windows.Forms.DialogResult.OK == result)
                {

                    // ItemListView.ItemsSource = _folders;

                    string path = dialog.SelectedPath + "\\";
                    int count = RecursiveReadFileAndFolder(path, "File");
                    MessageBox.Show(count + " file(s) Added Successfully", "Success");

                }

            }
            else if (typeComboBox.SelectedItem.ToString() == "All Folders in Folder (Recursive)")
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();

                int counter = 0;
                if (System.Windows.Forms.DialogResult.OK == result)
                {

                    // ItemListView.ItemsSource = _folders;

                    string path = dialog.SelectedPath + "\\";
                    int count = RecursiveReadFileAndFolder(path, "Folder");
                    MessageBox.Show(count + " folder(s) Added Successfully", "Success");

                }

            }
            else if (typeComboBox.SelectedItem.ToString() == "All File&Folders in Folder (Recursive)")
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();

                int counter = 0;
                if (System.Windows.Forms.DialogResult.OK == result)
                {

                    // ItemListView.ItemsSource = _folders;

                    string path = dialog.SelectedPath + "\\";
                    int count = RecursiveReadFileAndFolder(path, "All");
                    MessageBox.Show(count + " folder(s) and file(s) Added Successfully", "Success");

                }

            }
            applyChangeForRules();
        }
        private int RecursiveReadFileOnly(string folder)
        {
            int count = 0;
            foreach (string f in Directory.GetFiles(folder))
            {
                String[] filenameSplit = f.Split('\\');
                String filename = filenameSplit[filenameSplit.Length - 1];
                bool isExist = false;


                if (_fullList.Where(X => X.getType() == "File" && ((FileInOS)X)!.Pathname == f).FirstOrDefault() == null)
                {
                    count++;

                    _fullList.Add(new FileInOS
                    {
                        Filename = filename,
                        NewFilename = filename,
                        Pathname = f,
                        Type = "File",
                        Result = "",
                        Status = 0
                    });
                }
            }
            foreach (string d in Directory.GetDirectories(folder))
            {
                count += RecursiveReadFileOnly(d);
            }

            return count;
        }
        private int RecursiveReadFileAndFolder(string folder, string type)
        {
            int count = 0;
            try
            {
                if (type == "All" || type == "File")
                {
                    foreach (string f in Directory.GetFiles(folder))
                    {
                        String[] filenameSplit = f.Split('\\');
                        String filename = filenameSplit[filenameSplit.Length - 1];
                        bool isExist = false;


                        if (_fullList.Where(X => X.getType() == "File" && ((FileInOS)X)!.Pathname == f).FirstOrDefault() == null)
                        {
                            count++;

                            _fullList.Add(new FileInOS
                            {
                                Filename = filename,
                                NewFilename = filename,
                                Pathname = f,
                                Type = "File",
                                Result = "",
                                Status = 0
                            });
                        }
                    }
                    if (type == "File")
                    {
                        foreach (string d in Directory.GetDirectories(folder))
                        {
                            count += RecursiveReadFileOnly(d);
                        }
                    }
                }
                if (type == "All" || type == "Folder")
                {
                    foreach (string f in Directory.GetDirectories(folder))
                    {
                        String[] filenameSplit = f.Split('\\');
                        String filename = filenameSplit[filenameSplit.Length - 1];
                        bool isExist = false;

                        if (_fullList.Where(X => X.getType() == "Folder" && ((FolderInOS)X)!.Pathname == f).FirstOrDefault() == null)
                        {
                            count++;

                            _fullList.Add(new FolderInOS
                            {
                                Filename = filename,
                                NewFilename = filename,
                                Pathname = f,
                                Type = "Folder",
                                Result = "",
                                Status = 0
                            });
                        }
                        count += RecursiveReadFileAndFolder(f, type);
                    }

                }

            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
            return count;
        }


        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void resetItems(object sender, RoutedEventArgs e)
        {
            _fullList.Clear();
        }

        private void addRuleBtnClick(object sender, RoutedEventArgs e)
        {
            if (rulesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please choose rule", "Error");
                return;
            }
            else
            {
                _rulesList.Add(_ruleFactory.rules(rulesComboBox.SelectedItem.ToString()));
            }

            applyChangeForRules();
        }

        private void ChosenRule_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditRules(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Invalid Rule");
            }
            else
            {
                if (_rulesList[index].isEditatble())
                {
                    _rulesList[index] = _rulesList[index].EditRule();
                    applyChangeForRules();
                }
                else
                {
                    MessageBox.Show("This rule does not have any parameter to edit", "Error");

                }
            }
        }

        private void resetRuleBtnClick(object sender, RoutedEventArgs e)
        {
            _rulesList.Clear();
            foreach (FileChange item in _fullList)
            {
                switch (item.getType())
                {
                    case "File":
                        ((FileInOS)item).NewFilename = ((FileInOS)item).Filename;
                        break;

                    case "Folder":
                        ((FolderInOS)item).NewFilename = ((FolderInOS)item).Filename;
                        break;
                }

            }
        }
        private string applyChangeOnFile(string filename)
        {
            string res = filename;
            foreach (IRules rule in _rulesList)
            {

                res = rule.applyRule(res);
            }
            return res;
        }
        private void applyChangeForRules()
        {
            foreach (IRules rule in _rulesList)
            {
                rule.reset();
            }

            checkValidFileAndFolder();
        }

        private void removeRuleBtnClick(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            _rulesList.RemoveAt(index);
            applyChangeForRules();
        }

        private void chosenRulesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteRules(object sender, RoutedEventArgs e)
        {

            int index = chosenRulesListView.SelectedIndex;
            _rulesList.RemoveAt(index);
            applyChangeForRules();
        }



        private void NewProjectBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OpenProjectBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void SaveProjectBtnClick(object sender, RoutedEventArgs e)
        {

        }


        private void SavePresetBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OpenPresetBtnClick(object sender, RoutedEventArgs e)
        {

        }


        private void checkValidFileAndFolder()
        {
            foreach (FileChange item in _fullList)
            {
                switch (item.getType())
                {
                    case "File":

                        ((FileInOS)item).Status = 0;
                        ((FileInOS)item).Result = "";

                        if (!File.Exists(((FileInOS)item).Pathname))
                        {
                            ((FileInOS)item).Status = -1;
                            ((FileInOS)item).Result = "File not available";
                        }

                        ((FileInOS)item).NewFilename = applyChangeOnFile(((FileInOS)item).Filename);
                        break;

                    case "Folder":
                        ((FolderInOS)item).Status = 0;
                        ((FolderInOS)item).Result = "";
                        if (!Directory.Exists(((FolderInOS)item).Pathname))
                        {
                            ((FolderInOS)item).Status = -1;
                            ((FolderInOS)item).Result = "Folder not available";
                        }

                        ((FolderInOS)item).NewFilename = applyChangeOnFile(((FolderInOS)item).Filename);
                        break;
                }

            }
        }

        private void findFileInFullList(string filePath, ref FileInOS result)
        {
            result = (FileInOS)_fullList.Where(X => X.getType() == "File" && ((FileInOS)X)!.Pathname == filePath).FirstOrDefault();
        }
        private void findFolderInFullList(string folderPath, ref FolderInOS result)
        {
            result = (FolderInOS)_fullList.Where(X => X.getType() == "Folder" && ((FolderInOS)X)!.Pathname == folderPath).FirstOrDefault();
        }
        private void CopyFileWithRecursion(string oldFolderPath, string newFolderPath)
        {
            bool Override = false;
            bool Skip = false;

            if (conflictComboBox.SelectedItem != null)
            {
                if (conflictComboBox.SelectedItem.ToString() == "Override") Override = true;
                if (conflictComboBox.SelectedItem.ToString() == "Skip") Skip = true;
            }
            FolderInOS item = null;
            findFolderInFullList((string)oldFolderPath, ref item);

            if (item == null)
            {

                string filename = Path.GetDirectoryName(oldFolderPath);

                item = new FolderInOS
                {
                    Filename = filename,
                    NewFilename = filename,
                    Pathname = oldFolderPath,
                    Type = "Folder",
                    Result = "",
                    Status = 0
                };
            }

            if (Directory.Exists(newFolderPath))
            {

                if (!Override && !Skip)
                {
                    item.Status = 2;
                    item.Result = "Please choose conflict action";

                }
                if (Skip)
                {
                    item.Status = 1;
                    item.Result = "Skipped";
                }
                if (Override)
                {
                    item.Status = 1;
                    item.Result = "Overrided";

                    Directory.Delete(newFolderPath, true);
                    Directory.CreateDirectory(newFolderPath);

                }
            }
            else
            {
                item.Status = 1;
                item.Result = "Successed";
                Directory.CreateDirectory(newFolderPath);
            }

            if (Directory.Exists(oldFolderPath))
            {
                string[] files = Directory.GetFiles(oldFolderPath);
                string[] dirs = Directory.GetDirectories(oldFolderPath);

                foreach (string f in files)
                {
                    string filename = Path.GetFileName(f);

                    string newFilename = Path.Combine(newFolderPath, filename);

                    FileInOS itemFile = null;
                    findFileInFullList(f, ref itemFile);
                    if (itemFile == null)
                    {

                        itemFile = new FileInOS
                        {
                            Filename = filename,
                            NewFilename = filename,
                            Pathname = f,
                            Type = "File",
                            Result = "",
                            Status = 0
                        };
                    }
                    if (itemFile.Status != 0) continue;
                    newFilename = Path.Combine(newFolderPath, itemFile.NewFilename);

                    if (File.Exists(newFilename))
                    {
                        if (!Override && !Skip)
                        {
                            itemFile.Status = 2;
                            itemFile.Result = "Please choose conflict action";

                        }
                        if (Skip)
                        {
                            itemFile.Status = 1;
                            itemFile.Result = "Skipped";
                        }
                        if (Override)
                        {
                            itemFile.Status = 1;
                            itemFile.Result = "Overrided";

                            File.Delete(newFilename);
                            File.Copy(f, newFilename);

                        }
                    }
                    else
                    {
                        itemFile.Status = 1;
                        itemFile.Result = "Successed";
                        File.Copy(f, newFilename);
                    }

                }

                foreach (string d in dirs)
                {
                    string foldername = Path.GetDirectoryName(d);

                    string newFoldername = Path.Combine(newFolderPath, foldername);
                    FolderInOS itemFolder = null;
                    findFolderInFullList(d, ref itemFolder);
                    if (itemFolder == null)
                    {

                        itemFolder = new FolderInOS
                        {
                            Filename = foldername,
                            NewFilename = foldername,
                            Pathname = d,
                            Type = "Folder",
                            Result = "",
                            Status = 0
                        };
                    }

                    if (itemFolder.Status != 0) continue;
                    newFoldername = Path.Combine(newFolderPath, itemFolder.NewFilename);
                    if (Directory.Exists(newFoldername))
                    {

                        if (!Override && !Skip)
                        {
                            itemFolder.Status = 2;
                            itemFolder.Result = "Please choose conflict action";

                        }
                        if (Skip)
                        {
                            itemFolder.Status = 1;
                            itemFolder.Result = "Skipped";
                        }
                        if (Override)
                        {
                            itemFolder.Status = 1;
                            itemFolder.Result = "Overrided";

                            Directory.Delete(newFoldername, true);
                            Directory.CreateDirectory(newFoldername);

                        }
                    }
                    else
                    {
                        itemFolder.Status = 1;
                        itemFolder.Result = "Successed";
                        Directory.CreateDirectory(newFoldername);
                    }
                    CopyFileWithRecursion(d, newFoldername);
                }

            }


        }
        private void RenameFolderAndFile(string folder,string Type)
        {
            bool Override = false;
            bool Skip = false;

            if (conflictComboBox.SelectedItem != null)
            {
                if (conflictComboBox.SelectedItem.ToString() == "Override") Override = true;
                if (conflictComboBox.SelectedItem.ToString() == "Skip") Skip = true;
            }

            if(Type=="File")
            {
                FileInOS itemFile = null;
                findFileInFullList(folder, ref itemFile);

                string[] FilenameSplit = folder.Split('\\');
                string Originfilename = FilenameSplit[FilenameSplit.Length - 1];
                string newFilePath = "";
                for (int i = 0; i < FilenameSplit.Length - 1; i++)
                {
                    newFilePath = Path.Combine(newFilePath, FilenameSplit[i]);

                }
                string newFileName = Path.Combine(newFilePath, itemFile.NewFilename);

                File.Move(folder, newFileName);
                itemFile.Filename = itemFile.NewFilename;
                itemFile.Pathname = newFileName;
                itemFile.Status = 1;
                itemFile.Result = "Successed";
                return;
            }
            FolderInOS itemRoot = null;
            findFolderInFullList(folder, ref itemRoot);

            string[] RootnameSplit = folder.Split('\\');
            string filename = RootnameSplit[RootnameSplit.Length - 1];
            string newPath = "";
            for (int i = 0; i < RootnameSplit.Length - 1; i++)
            {
                newPath = Path.Combine(newPath, RootnameSplit[i]);

            }
            string newName =Path.Combine( newPath, itemRoot.NewFilename);
        
            Directory.Move(folder, newName);
            itemRoot.Pathname= newName;
            itemRoot.Filename = itemRoot.NewFilename;
            itemRoot.Status = 1;
            itemRoot.Result = "Successed";

            foreach (FileChange item in _fullList)
            {
                switch (item.getType())
                {
                    case "Folder":

                        ((FolderInOS)item).Pathname=((FolderInOS)item).Pathname.Replace(folder,newName);

                        break;
                    case "File":
                        ((FileInOS)item).Pathname=((FileInOS)item).Pathname.Replace(folder, newName);
                        break;

                }
            }

            //if(Directory.Exists(newName))
            //{
            //   if (Override)
            //    {
            //        Directory.Delete(newName, true);
            //        Directory.CreateDirectory(newName);
            //        itemRoot.Status = 1;
            //        itemRoot.Result = "Overrided";
            //    }
            //   if(Skip)
            //    {
            //        itemRoot.Status = 1;
            //        itemRoot.Result = "Skipped";
            //    }

            //}
            //else
            //{
            //    Directory.CreateDirectory(newName);
            //    itemRoot.Status = 1;
            //    itemRoot.Result = "Successed";
            //}
            //if(Directory.Exists(folder))
            //{
            //    foreach (string d in Directory.GetDirectories(folder))
            //    {

            //        string newNameDirs=Path.Combine(newName,Path.GetDirectoryName(d) );

            //        FolderInOS item = null;
            //        findFolderInFullList(d, ref item);
            //        if (item == null)
            //        {
            //            if(Directory.Exists(newNameDirs))
            //            {
            //                if (Override)
            //                {
            //                    Directory.Delete(newNameDirs, true);
            //                    Directory.CreateDirectory(newNameDirs);

            //                }

            //            }
            //            else
            //            {
            //                Directory.CreateDirectory(newNameDirs);

            //            }
            //            continue;
            //        }
            //        else
            //        {

            //        }





            //    }
            //}

        }

        private void RenameFileAndFolder()
        {
            bool Override = false;
            bool Skip = false;

            if (conflictComboBox.SelectedItem != null)
            {
                if (conflictComboBox.SelectedItem.ToString() == "Override") Override = true;
                if (conflictComboBox.SelectedItem.ToString() == "Skip") Skip = true;
            }
            //ObservableCollection<FileInOS> _files = new ObservableCollection<FileInOS>();
            //ObservableCollection<FolderInOS> _folders = new ObservableCollection<FolderInOS>();
            foreach (FileChange item in _fullList)
            {
                switch (item.getType())
                {
                    case "Folder":
                        RenameFolderAndFile(((FolderInOS)item).Pathname, "Folder");
                       

                        break;
                    case "File":
                        RenameFolderAndFile(((FileInOS)item).Pathname, "File");
                        break;

                }
            }

        }

        private void StartBtnClick(object sender, RoutedEventArgs e)
        {
            applyChangeForRules();
            bool Override = false;
            bool Skip = false;
            string path = "";
            bool copyChecked = (copyToNew.IsChecked==true);
            bool moveChecked = (moveToNew.IsChecked == true);
            bool originChecked = (renameOriginal.IsChecked == true);
            if (conflictComboBox.SelectedItem != null)
            {
                if (conflictComboBox.SelectedItem.ToString() == "Override") Override = true;
                if (conflictComboBox.SelectedItem.ToString() == "Skip") Skip = true;
            }
            if (originChecked)
            {
                if( _rulesList.ToArray().Length!=0)
                {
                    RenameFileAndFolder();

                }
                else
                {
                    MessageBox.Show("Please add some rules to continue");
                }
            }
            if (copyChecked || moveChecked)
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                var result = dialog.ShowDialog();
               
               
                if (System.Windows.Forms.DialogResult.OK == result)
                {

                    // ItemListView.ItemsSource = _folders;

                    path = dialog.SelectedPath;
                    

                }

            }
            if (copyChecked || moveChecked)
            {
                foreach (FileChange item in _fullList)
                {
                    if (item.getStatus() != 0) continue;
                    switch (item.getType())
                    {
                        case "Folder":

                            string currName = ((FolderInOS)item).Pathname;
                            string newName = "";

                            newName = Path.Combine(path, ((FolderInOS)item).NewFilename);
                            if (copyChecked || moveChecked)
                            {
                                CopyFileWithRecursion(currName, newName);
                            }
                            break;
                        case "File":
                            string currFile = ((FileInOS)item).Pathname;
                            string newFile = "";

                            newFile = Path.Combine(path, ((FileInOS)item).NewFilename);
                            if (File.Exists(newFile))
                            {
                                if (!Override && !Skip)
                                {
                                    ((FileInOS)item).Status = 2;
                                    ((FileInOS)item).Result = "Please choose conflict action";

                                }
                                if (Skip)
                                {
                                    ((FileInOS)item).Status = 1;
                                    ((FileInOS)item).Result = "Skipped";
                                }
                                if (Override)
                                {
                                    ((FileInOS)item).Status = 1;
                                    ((FileInOS)item).Result = "Overrided";

                                    File.Delete(newFile);
                                    File.Copy(currFile, newFile);

                                }
                            }
                            else
                            {

                                ((FileInOS)item).Status = 1;
                                ((FileInOS)item).Result = "Successed";
                                File.Copy(currFile, newFile);
                            }

                            break;


                    }

                }
            }    
            if ( moveChecked )
            {
                foreach (FileChange item in _fullList)
                {
                    if (item.getStatus() != 1) continue;
                    switch (item.getType())
                    {
                        case "Folder":
                            if(Directory.Exists(((FolderInOS)item).Pathname))
                            Directory.Delete(((FolderInOS)item).Pathname,true);
                            break;
                        case "File":
                            if(File.Exists(((FileInOS)item).Pathname))
                            File.Delete(((FileInOS)item).Pathname);

                            break;


                    }

                }
            }
        }
      

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void removeFileFolder(object sender, RoutedEventArgs e)
        {
            _fullList.RemoveAt(ItemListView.SelectedIndex);
        }
    }
}
