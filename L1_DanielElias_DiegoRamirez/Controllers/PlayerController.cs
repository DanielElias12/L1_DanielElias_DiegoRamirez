using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using L1_DanielElias_DiegoRamirez.Models.Data;
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

        //POST READ FILE
        //[HttpPost]
        //public ActionResult ReadFile()
        //{
        //    return View();
        //}

        // GET: PlayerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlayerController/Create
        public ActionResult dotnetManualCreate()
        {
            return View();
        }

        //.NET LIST
        public ActionResult dotnetList()
        {

            return View(Singleton.Instance.PlayersList);

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

        public ActionResult dotnetEdit(int id)
        {

            var editPlayer = Singleton.Instance.PlayersList.Find(x => x.Id == id);
            return View(editPlayer);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult dotnetEdit( int id, IFormCollection collection)
        {
            try
            {
                var editPlayer = Singleton.Instance.PlayersList.Find(x => x.Id == id);
             
                editPlayer.Salary = Convert.ToDouble(collection["Salary"]);
                editPlayer.Club = collection["Club"];
                Singleton.Instance.PlayersList[id] = editPlayer;
                return RedirectToAction(nameof(dotnetList));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlayerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlayerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlayerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
