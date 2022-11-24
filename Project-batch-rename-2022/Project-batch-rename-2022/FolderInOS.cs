using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public string toJSON()
        {
            var obj = new
            {
                Filename = this.Filename,
                NewFilename = this.NewFilename,
                Pathname = this.Pathname,
                Result = this.Result,
                Type = this.Type,
                Status = this.Status
            };
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(obj, options);
            return jsonString;
        }
    }
}
