using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models.ViewModel
{
    public class FileImage
    {
        public string fileName { get; set; }
        public string fileSize { get; set; }

        public FileImage(string fileName, string fileSize)
        {
            this.fileName = fileName;
            this.fileSize = fileSize;
        }

    }
}