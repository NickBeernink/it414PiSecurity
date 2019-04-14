using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiSecurity.Models
{
    public class Image
    {
        private string imageId = "";
        private string cameraName = "";
        private string date = "";
        private string fileName = "";

        public Image(string anId, string aName, string aDate, string aFileName)
        {
            imageId = anId;
            cameraName = aName;
            date = aDate;
            fileName = aFileName;
        }

        public string ImageId
        {
            get
            {
                return imageId;
            }
        }

        public string CameraName
        {
            get
            {
                return cameraName;
            }
            set
            {
                this.cameraName = value;
            }
        }

        public string Date
        {
            get
            {
                return date;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                this.fileName = value;
            }
        }
    }
}