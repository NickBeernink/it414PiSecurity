using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PiSecurity.Models;

namespace PiSecurity.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LiveFeed()
        {
            //TODO: figure out how to implement a live feed
            return View();
        }

        public ActionResult TakePicture()
        {
            PiCam cam = new PiCam();

            string imgPath = cam.TakePicture(); //Take a picture and get the image path returned as a string

            //Split the imgPath apart and grab the file name from the last entry
            string[] fileNameParts = imgPath.Split('\\');
            string fileName = fileNameParts[fileNameParts.Length - 1];

            ViewBag.FileName = fileName; //File name used to see image in the view

            return View();
        }

        public ActionResult ViewLog()
        {
            DbConnection dbc = DbConnection.GetInstance();

            List<Image> imgList = dbc.GetImages(); //Get all images in the database

            ViewBag.ImageList = imgList;

            return View();
        }

        public ActionResult ShowImage(string fileName)
        {
            //Show the image the user clicked
            ViewBag.FileName = fileName;

            return View();
        }
    }
}