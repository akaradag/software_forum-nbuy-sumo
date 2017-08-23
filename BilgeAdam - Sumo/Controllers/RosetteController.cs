using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgeAdam___Sumo.DataAccess;
using System.IO;
using BilgeAdam___Sumo.Tools;

namespace BilgeAdam___Sumo.Controllers
{
    public class RosetteController : Controller
    {
        // GET: Rosette
        public ActionResult Index(int rosetteId = 0, int pageNumber = 1)
        {
            List<Rosette> listRosette;
            List<Rosette> aList = new List<Rosette>();

            int counter = 0, pageCount = 0;

            using (SOFModel db = new SOFModel())
            {
                listRosette = db.Rosettes.Include("UserRosettes").ToList();

                for (int i = 0; i < listRosette.Count; i++)
                {
                    Tool.WhichPage(pageNumber, aList, ref counter, ref pageCount, listRosette[i]);
                }
            }

            TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
            TempData["PageNumber"] = pageNumber;

            return View(aList);
        }

        public ActionResult User(int rosetteId = 0, int pageNumber = 1)
        {
            List<User> userList = new List<User>();
            List<User> aList = new List<User>();
            List<UserRosette> listUserRosette;
            int counter = 0, pageCount = 0;

            using (SOFModel db = new SOFModel())
            {

                listUserRosette = db.UserRosettes.Where(ur => ur.RosetteId == rosetteId).ToList();

                foreach (UserRosette item in listUserRosette)
                {
                    userList.Add(db.Users.Include("UserFollows").Where(u => u.Id == item.UserId).First());
                }

                for (int i = 0; i < userList.Count; i++)
                {
                    Tool.WhichPage(pageNumber, aList, ref counter, ref pageCount, userList[i]);
                }

                TempData["RosetteTitlte"] = db.Rosettes.Where(r => r.Id == rosetteId).First();
            }

            TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
            TempData["PageNumber"] = pageNumber;
            return View(aList);
        }
    }
}