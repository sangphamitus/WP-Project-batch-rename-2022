using IRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReplaceCharacters
{
    public class EditReplaceCharacters : Window, IRuleEdit
    {

        private Canvas canvas = new Canvas();
        private Label label = new Label();
        private Label label2 = new Label();
        private Button submitBtn = new Button();
        private Button cancelBtn = new Button();
        private TextBox editTxtBox = new TextBox();
        private TextBox replaceTxtBox = new TextBox();
        IRules currentRule = null;
        public EditReplaceCharacters(IRules current)
        {
            this.currentRule = current;


            this.Title = "Parameter Editor for change character Rule";
            this.Width = 415;
            this.Height = 235;
            this.ResizeMode = ResizeMode.NoResize;


            label.Content = "Please type characters you want to change";
            label.Margin = new Thickness(20, 10, 0, 0);
            label.FontSize = 16;

            label2.Content = "Please type new characters you want to change";
            label2.Margin = new Thickness(20, 75, 0, 0);
            label2.FontSize = 16;

            editTxtBox.Width = 360;
            editTxtBox.Height = 30;
            editTxtBox.TextWrapping = TextWrapping.WrapWithOverflow;
            editTxtBox.Margin = new Thickness(20, 50, 0, 0);
            editTxtBox.FontSize = 16;
            editTxtBox.Text = ((ReplaceCharacters)currentRule).getOld();

            replaceTxtBox.Width = 360;
            replaceTxtBox.Height = 30;
            replaceTxtBox.TextWrapping = TextWrapping.WrapWithOverflow;
            replaceTxtBox.Margin = new Thickness(20, 100, 0, 0);
            replaceTxtBox.FontSize = 16;
            replaceTxtBox.Text = ((ReplaceCharacters)currentRule).getNew();


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
            canvas.Children.Add(label2);
            canvas.Children.Add(replaceTxtBox);
            this.AddChild(canvas);
            this.ShowDialog();
        }
        private void OnSubmitButtonClick(object sender, RoutedEventArgs e)
        {
            string str = editTxtBox.Text;
            string newC = replaceTxtBox.Text;
            if (str.Length != 0 )
            {
                Regex reg = new Regex("^[ \\.\\w-$()+=[\\];#@~,&']$");

                if (reg.Match(newC).Success)
                {
                    ((ReplaceCharacters)currentRule).set(str, newC);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("New characters is invalid!");
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

    public class ReplaceCharacters : IRules
    {

        private string _newC;
        private string _oldC;
        public void set(string pre,string n)
        {
            _oldC = pre;
            _newC = n;
        }
      
        public string getOld()
        {
            return _oldC;
        }
        public string getNew()
        {
            return _newC;
        }
        public ReplaceCharacters()
        {
            this._newC = "";
            this._oldC = "";

        }
        public ReplaceCharacters(string data)
        {
            this._newC =data;
            this._oldC =data;
        }
        public string applyRule(string filename,string type)
        {
            string result = filename;
            if (this._oldC != "")

            {
                result=result.Replace(this._oldC,this._newC);
            }
            return result;
        }

        public void reset()
        {
            return;
        }

        public override string ToString()
        {
            return "Change: \"" + this._oldC+"\" to \""+this._newC+"\"";
        }

        public IRules? parse(string data)
        {
            return new ReplaceCharacters();
        }
        public IRules? EditRule()
        {


            return (new EditReplaceCharacters(this)).getCurrentRule();
        }

        public bool isEditatble() 
        {
            return true;
        }


        public static string ruleName { get => "Replace Characters"; }


    }
}