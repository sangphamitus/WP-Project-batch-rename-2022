using IRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChangeExtension
{
    public class EditChangeExtension : Window, IRuleEdit
    {

        private Canvas canvas = new Canvas();
        private Label label = new Label();
        private Button submitBtn = new Button();
        private Button cancelBtn = new Button();
        private TextBox editTxtBox = new TextBox();

        IRules currentRule = null;
        public EditChangeExtension(IRules current)
        {
            this.currentRule = current;


            this.Title = "Parameter Editor for Chang Extension Rule";
            this.Width = 415;
            this.Height = 235;
            this.ResizeMode = ResizeMode.NoResize;


            label.Content = "Please type characters you want to Chang Extension";
            label.Margin = new Thickness(20, 10, 0, 0);
            label.FontSize = 16;

            editTxtBox.Width = 360;
            editTxtBox.Height = 80;
            editTxtBox.TextWrapping = TextWrapping.WrapWithOverflow;
            editTxtBox.Margin = new Thickness(20, 50, 0, 0);
            editTxtBox.Text = ((ChangeExtension)currentRule).getPrefix();

            submitBtn.Content = "Submit";
            submitBtn.Name = "buttonSubmit";
            submitBtn.IsDefault = true;
            submitBtn.Click += this.OnSubmitButtonClick;
            submitBtn.Width = 170;
            submitBtn.Height = 40;
            submitBtn.Margin = new Thickness(20, 145, 0, 0);
            submitBtn.FontSize = 15;

            cancelBtn.Click += this.OnCancelButtonClick;
            cancelBtn.IsCancel = true;
            cancelBtn.Content = "Cancel";
            cancelBtn.Width = 170;
            cancelBtn.Height = 40;
            cancelBtn.Margin = new Thickness(210, 145, 0, 0);
            cancelBtn.FontSize = 15;

            canvas.Children.Add(label);
            canvas.Children.Add(editTxtBox);
            canvas.Children.Add(submitBtn);
            canvas.Children.Add(cancelBtn);

            this.AddChild(canvas);
            this.ShowDialog();
        }
        private void OnSubmitButtonClick(object sender, RoutedEventArgs e)
        {
            string str = editTxtBox.Text;
            Regex reg = new Regex("\\W+");
            if (str.Length != 0 )
            { if (reg.Match(str).Success)
                {
                    MessageBox.Show("Invalid extention!");
                }
                else
                {
                    ((ChangeExtension)currentRule).setPrefix(str);
                    DialogResult = true;
                }
            }
        }
        public IRules getCurrentRule()
        {
            return currentRule;
        }
        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {

        }

    }

    public class ChangeExtension : IRules
    {

        private string _extension;

        public void setPrefix(string pre)
        {
            _extension = pre;
        }
        public string getPrefix()
        {
            return _extension;
        }
        public ChangeExtension()
        {
            this._extension = "";

        }
        public ChangeExtension(string pre)
        {
            this._extension = pre;
        }

        public string applyRule(string filename)
        {
            int index = filename.LastIndexOf('.');
            if (index != -1)
            {
                string result = filename.Replace(filename.Substring(index+1),_extension);
                return result;
            }
            return filename;
        }

        public void reset()
        {
            return;
        }

        public override string ToString()

        {
            return "Change extention: \"" + this._extension + "\"";
        }

        public IRules? parse(string data)
        {
            return new ChangeExtension(data);
        }
        public IRules? EditRule()
        {


            return (new EditChangeExtension(this)).getCurrentRule();
        }

        public bool isEditatble()
        {
            return true;
        }


        public static string ruleName { get => "Chang Extension"; }


    }
}