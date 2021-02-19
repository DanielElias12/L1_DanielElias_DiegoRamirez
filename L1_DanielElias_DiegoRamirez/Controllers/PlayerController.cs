﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using L1_DanielElias_DiegoRamirez.Models.Data;
using LIbreriaRD;
using L1_DanielElias_DiegoRamirez.Models;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;



       
namespace L1_DanielElias_DiegoRamirez.Controllers
{
    public class PlayerController : Controller
    {




        IWebHostEnvironment hostingEnvironment;
        public PlayerController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            
        }

        // GET: PlayerController
        public ActionResult Index()
        {
            return View(Singleton.Instance.PlayersList);
        }

        //READ FILE METHOD FOR .NET LIST
        public ActionResult ReadFile()
        {
         
            return View();
        }

       // POST: PlayerController/ReadFile
       [HttpPost]
        public IActionResult ReadFile(FileClass model)
        {
            if (ModelState.IsValid)
            {
                string uFileName = null;
                if (model.csv != null)
                {
                    string uploadFolder = Path.Combine(this.hostingEnvironment.WebRootPath, "CSV");
                    uFileName = Guid.NewGuid().ToString() + model.csv.FileName;
                    string filePath = Path.Combine(uploadFolder, uFileName);

                    using (FileStream fileStream = System.IO.File.Create(filePath))
                    {
                        model.csv.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    string[] lines = System.IO.File.ReadAllLines(filePath);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] fields = lines[i].Split(",");
                        var newPlayer = new Models.Player();
                        newPlayer.Id = Convert.ToInt32(fields[0]);
                        newPlayer.Name = fields[1];
                        newPlayer.LastName = fields[2];
                        newPlayer.Position = fields[3];
                        newPlayer.Salary = Convert.ToDouble(fields[4]);
                        newPlayer.Club = fields[5];
                        Singleton.Instance.PlayersList.Add(newPlayer);
                    }
                }

            }
            return RedirectToAction("dotnetList");
        }

     
        //MANUAL CREATE METHOD FOR .NET LIST
        // GET: PlayerController/Create
        public ActionResult dotnetManualCreate()
        {
            return View();
        }
        // POST: PlayerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult dotnetManualCreate(IFormCollection collection)
        {

            try
            {
                var newPlayer = new Models.Player
                {
                    Id = Convert.ToInt32(collection["Id"]),
                    Name = collection["Name"],
                    LastName = collection["LastName"],
                    Position = collection["Position"],
                    Salary = Convert.ToDouble(collection["Salary"]),
                    Club = collection["Club"]

                };


                Singleton.Instance.PlayersList.Add(newPlayer);
                return RedirectToAction(nameof(dotnetList));

            }
            catch
            {
                return View();
            }
        }


        //.NET LIST
        [HttpGet]
        public ActionResult dotnetList(string searched)
        {
            ViewData["GetPlayer"] = searched;
            var playerRequest = from x in Singleton.Instance.PlayersList select x;
            if (!String.IsNullOrEmpty(searched))
            {
                //DELEGATES
                playerRequest = playerRequest.Where(x => x.Name.Contains(searched) || x.LastName.Contains(searched) ||
                x.Position.Contains(searched) || x.Salary.ToString().Contains(searched) || x.Club.Contains(searched));
            }
            return View(playerRequest);
        }
        public ActionResult Implementedlist(string searched)
        {
            ViewData["GetPlayerImplemented"] = searched;
            var playerRequest = from x in Singleton.Instance.playeradder select x;
            if (!String.IsNullOrEmpty(searched))
            {
                //DELEGATES
                playerRequest = playerRequest.Where(x => x.Name.Contains(searched) || x.LastName.Contains(searched) ||
                x.Position.Contains(searched) || x.Salary.ToString().Contains(searched) || x.Club.Contains(searched));
            }
            return View(playerRequest);
        }

        //IMPLEMENTED LIST CREATE METHOD
        public ActionResult ImplementedListManualCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImplementedListManualCreate(IFormCollection collection)
        {

            try
            {
                var newPlayer = new Models.Player
                {
                    Id = Convert.ToInt32(collection["Id"]),
                    Name = collection["Name"],
                    LastName = collection["LastName"],
                    Position = collection["Position"],
                    Salary = Convert.ToDouble(collection["Salary"]),
                    Club = collection["Club"]

                };

                Singleton.Instance.playeradder.AddLast(newPlayer);
         
                return RedirectToAction(nameof(Implementedlist));

            }
            catch
            {
                return View();
            }

        }


        //EDIT METHODS FOR .NET LIST
        public ActionResult dotnetEdit(int id)
        {

            var editPlayer = Singleton.Instance.PlayersList.Find(x => x.Id == id);
            return View(editPlayer);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult dotnetEdit(int id, IFormCollection collection)
        {
            try
            {
                var editPlayer = Singleton.Instance.PlayersList.Find(x => x.Id == id);
                editPlayer.Salary = Convert.ToDouble(collection["Salary"]);
                editPlayer.Club = collection["Club"];
                Singleton.Instance.PlayersList[id-1] = editPlayer;
                return RedirectToAction(nameof(dotnetList));
            }
            catch
            {
                return View();
            }
        }

       

    




    }
}
