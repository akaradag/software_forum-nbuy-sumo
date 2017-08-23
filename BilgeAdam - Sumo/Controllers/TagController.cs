using BilgeAdam___Sumo.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using BilgeAdam___Sumo.Tools;

namespace BilgeAdam___Sumo.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        public ActionResult Index(int tagId = 9, int filterId = 0, int pageNumber = 1)
        {
            using (SOFModel dc = new SOFModel())
            {
                List<Question> questionList = new List<Question>();
                List<Question> allQestions = dc.Questions.Include(x => x.User).Include(x => x.Tags).Include(x => x.Answers).Include(x => x.QuestionVotes).ToList();
                List<Question> list = new List<Question>();
                foreach (Question item in allQestions)
                {
                    foreach (Tag item2 in item.Tags)
                    {
                        if (item2.Id == tagId)
                        {
                            list.Add(item);
                            break;
                        }
                    }
                }
                int counter = 0, pageCount = 0;
                if (filterId == 0) // Etkin
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.IsActive == true)
                        {
                            Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (filterId == 1) // Yeni
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                    }
                }
                else if (filterId == 2) // Popüler
                {
                    List<Question> newList = list.OrderByDescending(x => x.ViewsCount).ToList();
                    foreach (Question item in newList)
                    {
                        Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                    }
                }
                else if (filterId == 3) // En Fazla Oy
                {
                    List<Question> newList = list.OrderByDescending(x => x.QuestionVotes.Count).ToList();
                    foreach (Question item  in newList)
                    {
                        Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                    }
                }
                else if (filterId == 4) // Cevaplanmamış
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.Answers.Count == 0)
                        {
                            Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (filterId == 5) // Zorluk Derecesi - Zor
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.LevelId == 1)
                        {
                            Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (filterId == 6) // Zorluk Derecesi - Orta
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.LevelId == 2)
                        {
                            Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }
                else if (filterId == 7) // Zorluk Derecesi - Kolay
                {
                    List<Question> newList = list.OrderByDescending(x => x.CreateTime).ToList();
                    foreach (Question item in newList)
                    {
                        if (item.LevelId == 3)
                        {
                            Tool.WhichPage(pageNumber, questionList, ref counter, ref pageCount, item);
                        }
                    }
                }

                TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
                TempData["PageNumber"] = pageNumber;
                TempData["Filter"] = filterId;
                TempData["TagInformation"] = dc.Tags.Include(x => x.Questions).Include(x => x.TagFollows).Where(x => x.Id == tagId).First();
                return View(questionList);
            }
        }

        public ActionResult List()
        {
            using (SOFModel dc = new SOFModel())
            {
                TempData["Tags"] = dc.Tags.Include(x => x.TagFollows).Include(x=>x.Questions).OrderByDescending(x => x.TagFollows.Count).ToList();
                List<Tag> allTags = dc.Tags.ToList();
                return View(allTags);
            }                
        }
        [HttpPost]
        public JsonResult FollowTag(int id,int userId)
        {
            // etiket takip tablosundan (TagFollow) alınan verilerden çıkan sonuca göre firstOrdefault olarak 
            //cekildiginden ya nesne gelecek ya da null değeri gelecektir.
            //Gelen veri sonucu eğer null ise demek ki ilk defa takip edilecektir.Yeni nesne oluşturulup veri tabanına eklenir,eğer null değil ise daha önce kayıtlıysa isactive değeri false ise true ya true  ise false çekilir json ile döndürülen veri buton değeri değiştirilmek için gönderilmiştir bu durum kullanıcı girişine göre etiketlerin durumu geldiğinde değiştirilecektir
            using (SOFModel dc=new DataAccess.SOFModel ())
            {
                var result = dc.TagFollows.Where(x => x.TagId == id && x.UserId == userId).FirstOrDefault();
                if (result==null)
                {
                    TagFollow tg = new TagFollow();
                    tg.TagId = id;
                    tg.UserId = userId;
                    tg.IsActive = true;
                    dc.TagFollows.Add(tg);
                    dc.SaveChanges();
                    return Json("Yeni Takip", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (result.IsActive)
                    {
                        result.IsActive = false;
                        dc.SaveChanges();
                        return Json("Takip Durdu", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        result.IsActive = true;
                        dc.SaveChanges();
                        return Json("Yeni Takip", JsonRequestBehavior.AllowGet);
                    }
                }
               
            }
        }
    }
}