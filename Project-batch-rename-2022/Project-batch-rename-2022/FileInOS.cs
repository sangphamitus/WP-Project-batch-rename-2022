using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_batch_rename_2022
{
   interface FileChange
    {
        public string getType();
        public int getStatus();

    }

    internal class FileInOS:INotifyPropertyChanged,ICloneable,FileChange
    {
        public string Filename { get; set; }
        
        public string NewFilename { get; set; }
        public string Pathname { get; set; }
        public string Result { get; set; }

        public string Type { get; set; }
        public int Status { get; set;}

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string getType()
        {
            return "File";
        }
        public int getStatus()
        {
            return this.Status;
        }
    }
}
