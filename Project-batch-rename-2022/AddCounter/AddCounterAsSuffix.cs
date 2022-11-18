using IRule;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;

namespace AddCounter
{
    public class EditCounterAsSuffix : Window, IRuleEdit
    {

        private Canvas canvas = new Canvas();
        private Label label = new Label();
        private Button submitBtn = new Button();
        private Button cancelBtn = new Button();
        private TextBox editTxtBox = new TextBox();

        IRules currentRule = null;
        public EditCounterAsSuffix(IRules current)
        {
            this.currentRule = current;


            this.Title = "Parameter Editor for Add Counter";
            this.Width = 415;
            this.Height = 235;
            this.ResizeMode = ResizeMode.NoResize;


            label.Content = "Please type number of digit";
            label.Margin = new Thickness(20, 10, 0, 0);
            label.FontSize = 16;

            editTxtBox.Width = 360;
            editTxtBox.Height = 80;
            editTxtBox.TextWrapping = TextWrapping.WrapWithOverflow;
            editTxtBox.Margin = new Thickness(20, 50, 0, 0);
            editTxtBox.Text = ((AddCounterAsSuffix)currentRule).getNoD();

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
            
            
            if (str.Length != 0 )
            {
                Regex reg = new Regex("^[0-9]$");
                
                if (reg.Match(str).Success)
                {
                    int n = Convert.ToInt32(str);
                    ((AddCounterAsSuffix)currentRule).setNoD(n);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Please type digit!");
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
    public class AddCounterAsSuffix : IRules
    {
        private int NumberOfDigit;
        public AddCounterAsSuffix()
        {
            this.NumberOfDigit = 1;
            this.Step = 1;
            this.StartValue = 1;
            this._counter = 0;
        }
      
        public void setNoD(int num)
        {
            this.NumberOfDigit = num;
        }
        public string getNoD()
        {
            return this.NumberOfDigit.ToString();
        }
        public int Step { get; set; }
        public int StartValue { get; set; }
        private int _counter;
        private string generateCounter()
        {
            string result = (this.StartValue + this._counter * this.Step).ToString();

            while (result.Length < this.NumberOfDigit) { result = "0" + result; }
            this._counter++;
            return result;
        }
        public string applyRule(string filename, string type)
        {
            int index = filename.LastIndexOf('.');
            string name = "", extension = "";
            if (index != -1 && type == "File")
            {
                name = filename.Substring(0, index);
                extension = filename.Substring(index);
            }
            else name = filename;
            string result = name + "_" + generateCounter() + extension;

            return result;

        }
        public override string ToString()
        {
            return "Add Counter As Suffix: "+this.NumberOfDigit+" num" ;
        }

        public void reset()
        {
            
            this.Step = 1;
            this.StartValue = 1;
            this._counter = 0;
        }

        public IRules? parse(string data)
        {
            return new AddCounterAsSuffix();
        }


        public bool isEditatble()
        {
            return true;
        }

       
        public IRules? EditRule()
        {


            return (new EditCounterAsSuffix(this)).getCurrentRule();
        }

      

        public static string ruleName { get => "Add Counter As Suffix"; }

    }
}

