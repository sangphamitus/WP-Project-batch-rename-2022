
using Fluent;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
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
using IRule;

namespace Project_batch_rename_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :RibbonWindow
    {
        BindingList<string> itemTypes;
        BindingList<string> ruleNames;
        //  BindingList<Rules> _rulesList= new BindingList<Rules>();
        BindingList<IRules> _rulesList = new BindingList<IRules>();
        RuleFactory _ruleFactory = RuleFactory.NewInstance();

        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<FileInOS> _files = new ObservableCollection<FileInOS>();
        ObservableCollection<FolderInOS> _folders = new ObservableCollection<FolderInOS>();
        ObservableCollection<FileChange> _fullList= new ObservableCollection<FileChange>();
        
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
                "All Files in Folder (Recursive)"
            };
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

            _ruleFactory.Inject(AddCounter.AddCounterAsSuffix.ruleName,new AddCounter.AddCounterAsSuffix());
            _ruleFactory.Inject(AddCounter.AddCounterAsPrefix.ruleName,new AddCounter.AddCounterAsPrefix());
            _ruleFactory.Inject(AddPrefix.AddPrefix.ruleName, new AddPrefix.AddPrefix());
            _ruleFactory.Inject(AddSuffix.AddSuffix.ruleName, new AddSuffix.AddSuffix());
            _ruleFactory.Inject(ChangeExtension.ChangeExtension.ruleName, new  ChangeExtension.ChangeExtension());
            _ruleFactory.Inject(DeleteWhiteSpace.DeleteWhiteSpace.ruleName, new DeleteWhiteSpace.DeleteWhiteSpace());
            _ruleFactory.Inject(LowerCase.LowerCase.ruleName,new LowerCase.LowerCase());
            _ruleFactory.Inject(PascalCase.PascalCase.ruleName, new PascalCase.PascalCase());
            _ruleFactory.Inject(RemoveSpace.RemoveSpace.ruleName, new RemoveSpace.RemoveSpace());
            _ruleFactory.Inject(ReplaceCharaters.ReplaceCharacters.ruleName, new ReplaceCharaters.ReplaceCharacters());

         
            ruleNames = _ruleFactory.listKeys();
            typeComboBox.ItemsSource = itemTypes;
          //  ItemListView.ItemsSource = _files;
            ItemListView.ItemsSource = _fullList;
            rulesComboBox.ItemsSource = ruleNames;
            chosenRulesListView.ItemsSource = _rulesList;


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
                                Error = "",
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
                            newFoldernames.Add(new FolderInOS() { Filename = currentName, 
                                                                NewFilename=currentName,
                                                                Pathname = path, 
                                                                   Type="Folder"});
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
                    int count=RecursiveReadFile(path);
                    MessageBox.Show(count + " file(s) Added Successfully", "Success");

                }

            }
            applyChangeForRules();
        }
        private int RecursiveReadFile(string folder)
        {
            int count = 0;
            try
            {
                foreach (string f in Directory.GetFiles(folder))
                {
                    String[] filenameSplit = f.Split('\\');
                    String filename = filenameSplit[filenameSplit.Length - 1];
                    bool isExist = false;

                    //foreach (FileInOS item in _fullList)
                    //{
                    //    if (item.Pathname == f)
                    //    {
                    //        isExist = true;
                    //        break;
                    //    }
                    //}

                    if (_fullList.Where(X => X.getType() =="File" && ((FileInOS)X)!.Pathname == f).FirstOrDefault() == null )
                    {
                        count++;

                        _fullList.Add(new FileInOS
                        {
                            Filename = filename,
                            NewFilename = filename,
                            Pathname = f,
                            Type = "File",
                            Error = "",
                            Status = 0
                        });
                    }
                }
                foreach (string d in Directory.GetDirectories(folder))
                {
                   
                   return count+ RecursiveReadFile(d);
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
            //switch(rulesComboBox.SelectedItem.ToString())
            //{
            //    case "Change extension":
            //        ChangeExtension _changeExtension = new ChangeExtension();
            //        _rulesList.Add(_changeExtension);
            //        break;
            //    case "Add Counter":
            //        addCounter _addCounter = new addCounter();
            //        _rulesList.Add(_addCounter);

            //        break;
            //    case "Trim":
            //        removeSpace _removeSpace = new removeSpace();
            //        _rulesList.Add(_removeSpace);
            //        break;
            //    case "Remove white space":

            //        deleteWhiteSpace _deleteWhiteSpace = new deleteWhiteSpace();
            //        _rulesList.Add(_deleteWhiteSpace);
            //        break;

            //    case "Replace character":
            //        replaceCharacters _replaceCharacters = new replaceCharacters();
            //        _rulesList.Add(_replaceCharacters);

            //        break;
            //    case "Add a prefix":
            //        addPrefix _addPrefix = new addPrefix("");
            //        _rulesList.Add(_addPrefix);
            //        break;

            //    case "Add a subffix":
            //        addSuffix _addSuffix = new addSuffix("");
            //        _rulesList.Add(_addSuffix);
            //        break;

            //    case "Lowercase":
            //        toLowerCase _toLowerCase = new toLowerCase();
            //        _rulesList.Add(_toLowerCase);
            //        break;

            //    case "PascalCase":

            //        toPascalCase _toPascalCase = new toPascalCase();
            //        _rulesList.Add(_toPascalCase);
            //        break;

            //    default:
            //        return;
            //}
            applyChangeForRules();
        }

        private void ChosenRule_DoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void EditRules(object sender, RoutedEventArgs e)
        {
            int index=chosenRulesListView.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Invalid Rule");
            }
            else
            {
                if (_rulesList[index].isEditatble())
                {
                    _rulesList[index]= _rulesList[index].EditRule();
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
           
            foreach (FileChange item in _fullList)
            {
                switch (item.getType())
                {
                    case "File":
                        ((FileInOS)item).NewFilename =applyChangeOnFile( ((FileInOS)item).Filename);
                        break;

                    case "Folder":
                        ((FolderInOS)item).NewFilename =applyChangeOnFile( ((FolderInOS)item).Filename);
                        break;
                }

            }
        }

        private void removeRuleBtnClick(object sender, RoutedEventArgs e)
        {
            int index = chosenRulesListView.SelectedIndex;
            _rulesList.RemoveAt(index);
            applyChangeForRules();
        }
    }
}
