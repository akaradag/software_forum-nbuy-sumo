using BilgeAdam___Sumo.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BilgeAdam___Sumo.Tools;
using BilgeAdam___Sumo.Models;
using System.Data.Entity.Validation;

namespace BilgeAdam___Sumo.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        
        public ActionResult Logout(User user)
        {
            TempData.Remove("UserId");
            TempData.Remove("UserName");
            TempData.Remove("IsLogged");

            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public JsonResult UpdateProfile(User user)
        {
            using (SOFModel dc = new SOFModel())
            {
                User _user = dc.Users.Find(user.Id);
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.Image = user.Image;
                _user.Mail = user.Mail;
                _user.Phone = user.Phone;
                _user.Country = user.Country;
                _user.City = user.City;
                _user.Region = user.Region;

                // !!! Burası değişmeli veritabanına date kaydedemiyoruz!
                //_user.BirthDate = ((DateTime)user.BirthDate).Date;

                int result=0;
                try
                {
                    result = dc.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }

                return Json(new { msg = result }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult MyProfile(int id)
        {
            User user;
            using (SOFModel dc = new SOFModel())
            {
                user = dc.Users.AsNoTracking().Where(x => x.Id == id).SingleOrDefault();
                user.Role = dc.Users.AsNoTracking().Where(u => u.Id == id).Select(u => u.Role).SingleOrDefault();
                TempData["FollowerCount"] = dc.UserFollows.AsNoTracking().Where(uf => uf.FollowedFromWhoId == id).Select(uf => uf.User).Count();

            }
            return View(user);

        }


        public ActionResult ShowProfile(int id)
        {
            User user;
            UserInfoVM uInfo = new UserInfoVM();
            using (SOFModel dc = new SOFModel())
            {
                user = dc.Users.Where(u => u.Id == id).SingleOrDefault();

                uInfo.Id = user.Id;
                uInfo.FirstName = user.FirstName;
                uInfo.LastName = user.LastName;
                uInfo.Mail = user.Mail;

                if (user.BirthDate != null)
                {
                    uInfo.BirthDate = ((DateTime)(user.BirthDate)).ToShortDateString();
                }

                List<string> address = new List<string>();
                address.Add(user.Region);
                address.Add(user.City);
                address.Add(user.Country);

                uInfo.Address = default(String);
                for (int i = 0; i < address.Count; i++)
                {
                    if (!string.IsNullOrEmpty(address[i]))
                    {
                        uInfo.Address += address[i];
                        if (i != (address.Count - 1))
                        {
                            uInfo.Address += ", ";
                        }
                    }
                }

                if (user.Image != null)
                {
                    string base64 = Convert.ToBase64String(user.Image);
                    uInfo.Image = string.Format("data:image/gif;base64,{0}", base64);
                }

                uInfo.Rosettes = dc.UserRosettes.Where(ur => ur.User.Id == id).Select(ur => ur.Rosette).ToList();

                uInfo.Answers = dc.Answers.Where(a => a.User.Id == id).ToList();
                foreach (Answer item in uInfo.Answers)
                {
                    item.Question = dc.Answers.Where(a => a.Id == item.Id).Select(a => a.Question).SingleOrDefault();
                }
                uInfo.Questions = dc.Questions.Where(q => q.User.Id == id).ToList();

                uInfo.Followers = dc.UserFollows.Where(uf => uf.FollowedFromWhoId == id).Select(uf => uf.User).ToList();
                uInfo.RepPoint = user.RepPoint;
                uInfo.CreateDate = user.AccountCreateDate.ToShortDateString();
                uInfo.Role = user.Role.RoleName;

            }

            return View(uInfo);
        }


        public ActionResult List()
        {

            if (Request["FilterId"] == null && Request["PageNumber"] == null)
            {
                using (SOFModel dc = new SOFModel())
                {
                    List<User> allUsers = dc.Users.Include(x => x.UserFollows).OrderByDescending(x => x.UserFollows.Count).ToList();
                    List<User> realList = new List<User>();
                    int pageCount = 0, counter = 0;

                    foreach (User item in allUsers)
                    {
                        Tool.WhichPage(1, realList, ref counter, ref pageCount, item);
                    }

                    TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
                    TempData["PageNumber"] = 1;
                    TempData["Filter"] = 0;
                    return View(realList);
                }
            }
            else
            {
                return List(int.Parse(Request["FilterId"]), int.Parse(Request["PageNumber"]));
            }

        }

        [HttpPost]
        public ActionResult List(int FilterId = 0, int PageNumber = 1)
        {
            List<User> userList;
            List<User> realList = new List<User>();
            int pageCount = 0, counter = 0;

            using (SOFModel dc = new SOFModel())
            {
                using (SOFModel db = new SOFModel())
                {
                    if (FilterId == 0) // En Son Eklenen Kullanıcı
                    {
                        userList = db.Users.Include(x => x.UserFollows).OrderByDescending(u => u.AccountCreateDate).ToList();
                        foreach (User item in userList)
                        {
                            Tool.WhichPage(PageNumber, realList, ref counter, ref pageCount, item);
                        }
                    }
                    else if (FilterId == 1)
                    {
                        userList = db.Users.Include(x => x.UserFollows).OrderByDescending(u => u.AccountCreateDate).ToList();
                        foreach (User item in userList)
                        {
                            Tool.WhichPage(PageNumber, realList, ref counter, ref pageCount, item);
                        }
                    }
                    else if (FilterId == 2) // Rep Puanı
                    {
                        userList = db.Users.Include(x => x.UserFollows).OrderByDescending(u => u.RepPoint).ToList();
                        foreach (User item in userList)
                        {
                            Tool.WhichPage(PageNumber, realList, ref counter, ref pageCount, item);
                        }

                    }
                    else if (FilterId == 3) // En Eski Kullanıcılar
                    {
                        userList = db.Users.Include(x => x.UserFollows).OrderBy(u => u.AccountCreateDate).ToList();
                        foreach (User item in userList)
                        {
                            Tool.WhichPage(PageNumber, realList, ref counter, ref pageCount, item);
                        }
                    }

                    else if (FilterId == 4) // verdiği Cevap (Top 10)
                    {
                        userList = db.Users.Include(x => x.Answers).Include(x => x.UserFollows).OrderByDescending(x => x.Answers.Count).ToList();
                        foreach (User item in userList)
                        {
                            Tool.WhichPage(PageNumber, realList, ref counter, ref pageCount, item);
                        }
                    }
                    else if (FilterId == 5) // Takipçi Sayısı
                    {
                        userList = db.Users.Include(x => x.UserFollows).OrderByDescending(x => x.UserFollows.Count).ToList();
                        foreach (User item in userList)
                        {
                            Tool.WhichPage(PageNumber, realList, ref counter, ref pageCount, item);
                        }
                    }
                    else if (FilterId == 6) // Soru
                    {
                        userList = db.Users.Include(x => x.Questions).Include(x => x.UserFollows).OrderByDescending(x => x.Questions.Count).ToList();
                        foreach (User item in userList)
                        {
                            Tool.WhichPage(PageNumber, realList, ref counter, ref pageCount, item);
                        }
                    }
                }
                TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
                TempData["PageNumber"] = PageNumber;
                TempData["Filter"] = FilterId;

                return View(realList);
            }
        }

        public ActionResult FollowList(int id)
        {
            User user;
            UserInfoVM uInfo = new UserInfoVM();
            using (SOFModel dc = new SOFModel())
            {
                user = dc.Users.Where(u => u.Id == id).SingleOrDefault();

                uInfo.Id = user.Id;
                uInfo.FirstName = user.FirstName;
                uInfo.LastName = user.LastName;

                uInfo.Answers = dc.Answers.Where(a => a.User.Id == id).ToList();
                uInfo.Questions = dc.Questions.Where(q => q.User.Id == id).ToList();
                uInfo.Followers = dc.UserFollows.Where(uf => uf.FollowedFromWhoId == id).Select(uf => uf.User).ToList();

            }

            return View(uInfo);
        }
    }
}