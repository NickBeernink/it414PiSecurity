using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Renci.SshNet;

namespace PiSecurity.Models
{
    public class PiCam
    {
        //Class variables
        private string host = ""; //ip address of the pi - not static
        private string username = "pi";
        private string password = "it414raspberry";
        private string remoteFolderPath = "/home/pi/Pictures/PyCameraPicture.jpg"; //Image folder path on pi
        private string imagesFolderPath = "C:\\Users\\Nick Beernink\\source\\repos\\PiSecurity\\PiSecurity\\Images\\"; //Images path on local computer
        private string cameraName = "Bedroom"; //Name of the camera, would change for multiple individual camera setup

        //Public constructor
        public PiCam() { }

        public string TakePicture()
        {
            //Setup the connection info to ssh into the pi
            var connectionInfo = new ConnectionInfo(host, username, new PasswordAuthenticationMethod(username, password));
            string databaseInsertPath = "";

            using (var client = new SshClient(connectionInfo)) //create the ssh client
            {
                client.Connect();

                client.RunCommand("cd /home/pi"); //cd into the directory where the python script is located
                client.RunCommand("sudo python PyCamPic.py"); //Run the python script to take a picture
            }

            using (var client = new ScpClient(host, username, password)) //Create client for file transfer
            {
                client.Connect();

                string fileName = DateTime.Now.ToFileTime() + ".jpg"; //Name the file with current datetime

                using (Stream localFile = File.Create(Path.Combine(imagesFolderPath, fileName)))
                {
                    client.Download(remoteFolderPath, localFile); //Download image from pi to local machine
                    databaseInsertPath = Path.Combine(imagesFolderPath, fileName);
                }
            }

            DbConnection dbc = DbConnection.GetInstance();

            dbc.InsertImage(cameraName, DateTime.Now.ToString(), databaseInsertPath); //Insert the new image into the database

            return databaseInsertPath;
        }

        public void LiveFeed()
        {
            //TODO: implement
        }
    }
}