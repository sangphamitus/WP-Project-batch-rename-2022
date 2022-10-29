﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_batch_rename_2022
{
    internal class FileInOS:INotifyPropertyChanged,ICloneable
    {
        public string Filename { get; set; }
        
        public string NewFilename { get; set; }
        public string Pathname { get; set; }
        public string Error { get; set; }

        public int Status { get; set;}

        public event PropertyChangedEventHandler? PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
