using IRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AddPrefix
{
    public class EditAddPrefix : Window,IRuleEdit
    {
        
        private Canvas canvas = new Canvas();
        private Label label = new Label();
        private Button submitBtn = new Button();
        private Button cancelBtn = new Button();
        private TextBox editTxtBox = new TextBox();

        IRules currentRule = null;
        public EditAddPrefix(IRules current)
        {
            this.currentRule = current;
            
            
            this.Title = "Parameter Editor for Add Suffix Rule";
            this.Width = 415;
            this.Height = 235;
            this.ResizeMode = ResizeMode.NoResize;


            label.Content = "Please type characters you want to add as suffix";
            label.Margin = new Thickness(20, 10, 0, 0);
            label.FontSize = 16;

            editTxtBox.Width = 360;
            editTxtBox.Height = 80;
            editTxtBox.TextWrapping = TextWrapping.WrapWithOverflow;
            editTxtBox.Margin = new Thickness(20, 50, 0, 0);
            editTxtBox.Text = ((AddPrefix)currentRule).getPrefix();

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
            if (str.Length != 0)
            {
               ((AddPrefix)currentRule).setPrefix( str);
                DialogResult = true;
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

    public class AddPrefix : IRules
    {

        private string _prefix;

        public void setPrefix(string pre)
        {
            _prefix= pre;
        }
        public string getPrefix()
        {
            return _prefix;
        }
        public AddPrefix()
        {
            this._prefix = "";

         }
        public AddPrefix(string pre)
        {
            this._prefix = pre;
        }
       
        public string applyRule(string filename)
        {
            filename = this._prefix + filename;
            return filename;
        }

        public void reset()
        {
            return;
        }

        public override string ToString()
        {
            return "Add Prefix: " + this._prefix;
        }
      
        public IRules? parse(string data)
        {
            return new AddPrefix(data);
        }
        public IRules? EditRule()
        {


            return (new EditAddPrefix(this)).getCurrentRule();
        }
     
        public bool isEditatble()
        {
            return true;
        }


        public static string ruleName { get => "Add Prefix"; }

       
    }
}