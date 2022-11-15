using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_batch_rename_2022
{
    internal class FolderInOS : INotifyPropertyChanged, ICloneable,FileChange
    {
        public string Filename { get; set; }

        public string NewFilename { get; set; }
        public string Pathname { get; set; }
        public string Result { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }

        public string getType()
        {
            return "Folder";
        }
        public int getStatus()
        {
            return this.Status;
        }
    }
}
