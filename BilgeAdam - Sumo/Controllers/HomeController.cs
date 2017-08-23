using BilgeAdam___Sumo.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using BilgeAdam___Sumo.Tools;

namespace BilgeAdam___Sumo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request["FilterId"] == null && Request["PageNumber"] == null)
            {
                using (SOFModel dc = new SOFModel())
                {
                    List<Question> questionList = new List<Question>();
                    List<Question> list = dc.Questions.Include(x => x.User).Include(x => x.Tags).Include(x => x.Answers).Include(x => x.QuestionVotes).OrderByDescending(x => x.CreateTime).ToList();
                    int counter = 0, pageCount = 0;
                    foreach (Question item in list)
                    {
                        if (item.IsActive == true)
                        {
                            Tool.WhichPage(1, questionList, ref counter, ref pageCount, item);
                        }
                    }
                    TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
                    TempData["PageNumber"] = 1;
                    TempData["Filter"] = 0;
                    TempData["PopulerTags"] = dc.Tags.Include(x => x.TagFollows).OrderByDescending(x => x.TagFollows.Count).ToList();
                    TempData["LastTenRosette"] = dc.UserRosettes.Include(x=>x.User).Include(x=>x.Rosette).OrderByDescending(x => x.Date).Take(10).ToList();
                    return View(questionList);

                }
            }
            else
            {
            
                    return Index(int.Parse(Request["FilterId"]), int.Parse(Request["PageNumber"]));
                
            }
        }
        [HttpPost]
        public ActionResult Index(int FilterId = 0, int PageNumber = 1)
        {
            using (SOFModel dc = new SOFModel())
            {
                List<Question> questionList = new List<Question>();
                List<Question> list = dc.Questions.Include(x => x.User).Include(x => x.Tags).Include(x => x.Answers).Include(x => x.QuestionVotes).ToList();
                int counter = 0, pageCount = 0;
                if (FilterId == 0) // Etkin
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.IsActive == true)
                        {
                            Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (FilterId == 1) // Yeni
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                    }
                }
                else if (FilterId == 2) // Popüler
                {
                    List<Question> newList = list.OrderByDescending(x => x.ViewsCount).ToList();
                    foreach (Question item in newList)
                    {
                        Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                    }
                }
                else if (FilterId == 3) // En Fazla Oy
                {
                    List<Question> newList = list.OrderByDescending(x => x.QuestionVotes.Count).ToList();
                    foreach (Question item in newList)
                    {
                        Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                    }
                }
                else if (FilterId == 4) // Cevaplanmamış
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.Answers.Count == 0)
                        {
                            Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (FilterId == 5) // Zorluk Derecesi - Zor
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.LevelId == 1)
                        {
                            Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (FilterId == 6) // Zorluk Derecesi - Orta
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.LevelId == 2)
                        {
                            Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (FilterId == 7) // Zorluk Derecesi - Kolay
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.LevelId == 3)
                        {
                            Tool.WhichPage(PageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
                TempData["PageNumber"] = PageNumber;
                TempData["Filter"] = FilterId;
                TempData["PopulerTags"] = dc.Tags.Include(x => x.TagFollows).OrderByDescending(x => x.TagFollows.Count).ToList();
                TempData["LastTenRosette"] = dc.UserRosettes.Include(x => x.User).Include(x => x.Rosette).OrderByDescending(x => x.Date).Take(10).ToList();
                return View(questionList);
            }
        }
        public ActionResult SaveUser(string userMail, string password, string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(userMail) && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                using (SOFModel dc = new SOFModel())
                {
                    bool flag = false;
                    foreach (User item in dc.Users.ToList())
                    {
                        if (item.Mail == userMail)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        try
                        {
                            DataAccess.User u = new DataAccess.User();
                            u.Mail = userMail;
                            u.Password = password;
                            u.FirstName = firstName;
                            u.LastName = lastName;
                            u.RoleId = 3;
                            u.AccountCreateDate = DateTime.Today;
                            u.RepPoint = 10;
                            dc.Users.Add(u);
                            dc.SaveChanges();
                            string control = "";
                            TempData["IsLogged"] = 1;
                            TempData["UserName"] = firstName + " " + lastName;
                            TempData["UserId"] = u.Id;
                            return Json(control);
                        }
                        catch
                        {
                            string control = "Kayıt sırasında bir hata çıktı. Lütfen daha sonra tekrar deneyiniz.";
                            return Json(control);
                        }
                    }
                    else
                    {
                        string control = "Bu Mail Başka Bir Kullanıcı Tarafından Kullanılmaktadır.";
                        return Json(control);
                    }
                }
            }
            else
            {
                string control = "Hiç bir alan boş geçilemez.";
                return Json(control);
            }
        }
        public ActionResult ControlUserInformation(string userMail, string password)
        {
            if (!string.IsNullOrWhiteSpace(userMail) && !string.IsNullOrWhiteSpace(password))
            {
                using (SOFModel dc = new SOFModel())
                {
                    bool flag = false;
                    foreach (User item in dc.Users.ToList())
                    {
                        if (item.Mail.ToLower() == userMail.ToLower() && item.Password == password)
                        {
                            flag = true;
                            TempData["UserName"] = item.FirstName + " " + item.LastName;
                            TempData["UserId"] = item.Id;
                            break;
                        }
                    }
                    if (flag == true)
                    {
                        TempData["IsLogged"] = 1;
                        string control = "";
                        return Json(control);
                    }
                    else
                    {
                        string control = "Kullanıcı Adı veya Şifresi Yanlıştı.";
                        return Json(control);
                    }
                }
            }
            else
            {
                string control = "Kullanıcı Adı ve Şifresi Boş Geçilemez.";
                return Json(control);
            }
        }

        public ActionResult SSS()
        {
            return View();
        }
    }
}