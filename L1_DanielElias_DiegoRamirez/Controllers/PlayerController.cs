using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L1_DanielElias_DiegoRamirez.Models.Data;


namespace L1_DanielElias_DiegoRamirez.Controllers
{
    public class PlayerController : Controller
    {
        // GET: PlayerController
        public ActionResult Index()
        {
            return View(Singleton.Instance.PlayersList);
        }

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

            //var editPlayer = Singleton.Instance.PlayersList.Find(x => x.Id == id);
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult dotnetEdit(int id, IFormCollection collection)
        {
            try
            {
                var editPlayer = Singleton.Instance.PlayersList.Find(x => x.Id == id);
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
