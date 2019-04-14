using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PiSecurity.Models
{
    public class DbConnection
    {
        //null instance of connection
        private static DbConnection myDbConnection = null;
        private string DbConnectString = @"Data Source=DESKTOP-O5D8V8I\SQLEXPRESS;Initial Catalog=PiSecurity;Integrated Security=SSPI;";
        List<Image> ImageList = new List<Image>();

        //private constructor
        private DbConnection() { }

        public static DbConnection GetInstance()
        {
            if (myDbConnection == null)
            {
                myDbConnection = new DbConnection();
            }
            return myDbConnection;
        }

        public void InsertImage(string CameraName, string ImgDate, string Picture)
        {
            SqlConnection conn = new SqlConnection(DbConnectString);

            conn.Open();

            SqlCommand sqlComm = conn.CreateCommand();

            string sql = "INSERT INTO PiPictures(CameraName, ImgDate, Picture) VALUES('" + CameraName + "', '" + ImgDate + "', '" + Picture + "')";
            

            sqlComm.CommandText = sql;
            sqlComm.ExecuteNonQuery();

            conn.Close();
        }

        public List<Image> GetImages()
        {
            Image anImage = null;
            ImageList = new List<Image>(); //reset the imageList

            using (SqlConnection conn = new SqlConnection(DbConnectString))
            {
                SqlCommand command = new SqlCommand("SELECT ImgId, CameraName, ImgDate, Picture FROM PiPictures", conn);
                conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string imgId = reader["ImgId"] == DBNull.Value ? "n/a" : reader["ImgId"].ToString();
                        string cameraName = reader["CameraName"] == DBNull.Value ? "n/a" : reader["CameraName"].ToString();
                        string date = reader["ImgDate"] == DBNull.Value ? "n/a" : reader["ImgDate"].ToString();
                        string path = reader["Picture"] == DBNull.Value ? "n/a" : reader["Picture"].ToString();

                        string fileName = path.Split('\\')[8]; //Get the image name from the last entry in the array after splitting

                        anImage = new Image(imgId, cameraName, date, fileName);

                        ImageList.Add(anImage);
                    }
                }
            }

            return ImageList;
        }
    }
}