using IRule;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics;

namespace AddCounter
{
    public class EditCounterAsPrefix : Window, IRuleEdit
    {

        private Canvas canvas = new Canvas();
        private Label label = new Label();
        private Button submitBtn = new Button();
        private Button cancelBtn = new Button();
        private TextBox editTxtBox = new TextBox();

        private Label label2 = new Label();
        private TextBox editTxtBox2 = new TextBox();
        private Label label3 = new Label();
        private TextBox editTxtBox3 = new TextBox();

        IRules currentRule = null;
        public EditCounterAsPrefix(IRules current)
        {
            this.currentRule = current;


            this.Title = "Parameter Editor for Add Counter";
            this.Width = 415;
            this.Height = 365;
            this.ResizeMode = ResizeMode.NoResize;


            label.Content = "Please type number of digit";
            label.Margin = new Thickness(20, 10, 0, 0);
            label.FontSize = 16;

            editTxtBox.Width = 120;
            editTxtBox.Height = 30;
            editTxtBox.TextWrapping = TextWrapping.WrapWithOverflow;
            editTxtBox.Margin = new Thickness(20, 50, 0, 0);
            editTxtBox.Text = ((AddCounterAsPrefix)this.currentRule).getNoD();

            label2.Content = "Please type step of counter";
            label2.Margin = new Thickness(20, 75, 0, 0);
            label2.FontSize = 16;

            editTxtBox2.Width = 120;
            editTxtBox2.Height = 30;
            editTxtBox2.TextWrapping = TextWrapping.WrapWithOverflow;
            editTxtBox2.Margin = new Thickness(20, 110, 0, 0);
            editTxtBox2.Text = ((AddCounterAsPrefix)this.currentRule).getStep();

            label3.Content = "Please type start value";
            label3.Margin = new Thickness(20, 140, 0, 0);
            label3.FontSize = 16;

            editTxtBox3.Width = 120;
            editTxtBox3.Height = 30;
            editTxtBox3.TextWrapping = TextWrapping.WrapWithOverflow;
            editTxtBox3.Margin = new Thickness(20, 170, 0, 0);
            editTxtBox3.Text = ((AddCounterAsPrefix)this.currentRule).StartValue.ToString();

            submitBtn.Content = "Submit";
            submitBtn.Name = "buttonSubmit";
            submitBtn.IsDefault = true;
            submitBtn.Click += this.OnSubmitButtonClick;
            submitBtn.Width = 170;
            submitBtn.Height = 40;
            submitBtn.Margin = new Thickness(20, 215, 0, 0);
            submitBtn.FontSize = 15;

            cancelBtn.Click += this.OnCancelButtonClick;
            cancelBtn.IsCancel = true;
            cancelBtn.Content = "Cancel";
            cancelBtn.Width = 170;
            cancelBtn.Height = 40;      
            cancelBtn.Margin = new Thickness(210, 215, 0, 0);
            cancelBtn.FontSize = 15;

            canvas.Children.Add(label);
            canvas.Children.Add(editTxtBox);
            canvas.Children.Add(label2);
            canvas.Children.Add(editTxtBox2);
            canvas.Children.Add(label3);
            canvas.Children.Add(editTxtBox3);
            canvas.Children.Add(submitBtn);
            canvas.Children.Add(cancelBtn);

            this.AddChild(canvas);
            this.ShowDialog();
        }
        private void OnSubmitButtonClick(object sender, RoutedEventArgs e)
        {
            string str = editTxtBox.Text;
            string str2 = editTxtBox2.Text;
            string str3 = editTxtBox3.Text;


            if (str.Length != 0)
            {
                Regex reg = new Regex("^[0-9]$");

                if (reg.Match(str).Success&&reg.Match(str2).Success && reg.Match(str3).Success)
                {
                    int n = Convert.ToInt32(str);
                    ((AddCounterAsPrefix)this.currentRule).setNoD(n);
                    int m = Convert.ToInt32(str2);
                    ((AddCounterAsPrefix)this.currentRule).setStep( m);
                    int p = Convert.ToInt32(str3);
                    ((AddCounterAsPrefix)this.currentRule).StartValue=p;
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
            return this.currentRule;
        }
        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {

        }

        
    }
    public class AddCounterAsPrefix : IRules,ICloneable
    {
        private int NumberOfDigit;
        public AddCounterAsPrefix()
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
        public void setStep(int num)
        {
            this.Step = num;
        }
        public string getStep()
        {
            return this.Step.ToString();
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
           
            string result =  generateCounter()+"_"+filename ;

            return result;

        }
        public override string ToString()
        {
            return "Counter Prefix: " + this.NumberOfDigit + " num, step: " + this.Step +", start at: "+this.StartValue;
        }

        public void reset()
        {

         
            this._counter = 0;
        }

        public IRules? parse(string data)
        {
            return new AddCounterAsPrefix();
        }


        public bool isEditatble()
        {
            return true;
        }


        public IRules? EditRule()
        {


            return (new EditCounterAsPrefix(this)).getCurrentRule();
        }

        public object Clone()
        {
           return MemberwiseClone();
        }

        public string toJSON()
         {
            

            var obj = new
            {
                ruleName = ruleName,
                NumberOfDigit = this.NumberOfDigit,
                Step = this.Step,
                StartValue = this.StartValue,
                _counter = this._counter,
            };
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(obj, options);
            return jsonString;
        }

        public bool importPreset(JSONruleFile preset)
        {
            try
            {
                this.NumberOfDigit = preset.NumberOfDigit;
                this.Step = preset.Step;
                this.StartValue = preset.StartValue;
                this._counter = preset._counter;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public static string ruleName { get => "Add Counter As Prefix"; }

    }
}

