using BilgeAdam___Sumo.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using BilgeAdam___Sumo.Tools;
using BilgeAdam___Sumo.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SOF_BilgeAdam.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index(int questionId, int pageNumber = 1)
        {
            using (SOFModel dc = new SOFModel())
            {
                Question que = dc.Questions.Include(x => x.User).Include(x => x.Tags).Include(x => x.Answers).Include(x => x.QuestionVotes).Include(x => x.QuestionFollows).FirstOrDefault(x => x.Id == questionId);
                List<Answer> list = dc.Answers.Include(x => x.User).Include(x => x.AnswerVotes).Where(x => x.QuestionId == que.Id).ToList();
                List<Answer> aList = new List<Answer>();
                int counter = 0, pageCount = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    Tool.WhichPage(pageNumber, aList, ref counter, ref pageCount, list[i]);
                }
                TempData["Count"] = (int)Math.Ceiling((decimal)pageCount / (decimal)15);
                TempData["PageNumber"] = pageNumber;
                TempData["Answers"] = aList;



                Question whoAsked = dc.Questions.Where(x => x.Id == questionId).First();
                TempData["OtherQuestions"] = dc.Questions.Where(x => x.UserId == whoAsked.UserId && x.Id != whoAsked.Id).ToList();
                return View(que);
            }
        }
        public ActionResult Create()
        {
            return View(new Question());
        }
        public ActionResult SaveQuestion(string title, string content, int userId, string tags)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(content) && userId > 0)
                {
                    using (SOFModel dc = new SOFModel())
                    {
                        Question que = new Question();
                        que.Title = title;
                        que.Content = content;
                        que.CreateTime = DateTime.Now;
                        que.IsActive = true;
                        que.IsDeleted = false;
                        que.ViewsCount = 0;
                        que.UserId = userId;
                        que.Tags = Tool.GetTag(tags, dc.Tags.ToList());
                        dc.Questions.Add(que);
                        dc.SaveChanges();
                        return RedirectToAction("Index", new { questionId = que.Id, pageNumber = 1 });
                    }
                }
                else
                {
                    TempData["CreateErrorMessage"] = "Başlık ve İçerik Alanları Boş Geçilemez.";
                    return RedirectToAction("Create");
                }
            }
            catch
            {
                TempData["CreateErrorMessage"] = "Sorunuz Kaydedilemedi. Lütfen Bilgilerinizi Kontrol Ediniz.";
                return RedirectToAction("Create");
            }
        }
        public ActionResult SaveAnswer(int questionId, string content, int userId, int pageNumber)
        {
            List<Answer> list = new List<Answer>();
            try
            {
                if (questionId > 0 && !string.IsNullOrWhiteSpace(content) && userId > 0 && pageNumber > 0)
                {
                    using (SOFModel dc = new SOFModel())
                    {
                        Answer q = new Answer();
                        q.Content = content;
                        q.UserId = userId;
                        q.QuestionId = questionId;
                        q.Accepted = false;
                        q.CreateTime = DateTime.Now;
                        dc.Answers.Add(q);
                        dc.SaveChanges();
                        list = dc.Answers.Where(x => x.QuestionId == questionId).ToList();
                        return RedirectToAction("Index", new { questionId = questionId, pageNumber = (int)Math.Ceiling((decimal)list.Count / (decimal)15) });
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Lütfen Cevabınız Kısmını Boş Geçmeyiniz.";
                    return RedirectToAction("Index", new { questionId = questionId, pageNumber = pageNumber });
                }

            }
            catch
            {
                TempData["ErrorMessage"] = "Mesajınız Gönderilemedi. Lütfen Bilgilerinizi Kontrol Ediniz.";
                return RedirectToAction("Index", new { questionId = questionId, pageNumber = pageNumber });
            }


        }
        public ActionResult SaveQuestionVote(int questionId, int userId, bool vote)
        {
            using (SOFModel dc = new SOFModel())
            {   
                try
                {
                    QuestionVote qv;
                    if (dc.QuestionVotes.Where(x => x.QuestionId == questionId && x.UserId == userId).FirstOrDefault() != null)
                    {
                        qv = dc.QuestionVotes.Where(x => x.QuestionId == questionId && x.UserId == userId).First();
                        qv.Vote = vote;
                    }
                    else
                    {
                        qv = new QuestionVote();
                        qv.QuestionId = questionId;
                        qv.UserId = userId;
                        qv.Vote = vote;
                        dc.QuestionVotes.Add(qv);
                    }
                    dc.SaveChanges();
                }
                catch
                {
                }

                int count = 0;

                foreach (QuestionVote item in dc.QuestionVotes.Where(x => x.QuestionId == questionId).ToList())
                {
                    count += item.Vote == true ? 1 : -1;
                }
                return Json(count);
            }

        }
        public ActionResult SaveAnswerVote(object answerId, int userId, bool vote)
        {
            using (SOFModel dc = new SOFModel())
            {
                string[] array = (string[])answerId;
                int aID = Convert.ToInt32(array[0]);
                try
                {
                    AnswerVote av;
                    if (dc.AnswerVotes.Where(x => x.AnswerId == aID && x.UserId == userId).FirstOrDefault() != null)
                    {
                        av = dc.AnswerVotes.Where(x => x.AnswerId == aID && x.UserId == userId).First();
                        av.Vote = vote;
                    }
                    else
                    {
                        av = new AnswerVote();
                        av.AnswerId = aID;
                        av.UserId = userId;
                        av.Vote = vote;
                        dc.AnswerVotes.Add(av);
                    }
                    dc.SaveChanges();
                }
                catch (Exception e)
                {
                    string a = e.Message;
                }
                int count = 0;
                foreach (AnswerVote item in dc.AnswerVotes.Where(x => x.AnswerId == aID).ToList())
                {
                    count += item.Vote == true ? 1 : -1;
                }
                return Json(count);
            }

        }
        [HttpPost]
        public JsonResult Accepted(int answerId)
        {

            List<AnswerVM> answerVM;
            List<Answer> list;


            using (SOFModel dc=new BilgeAdam___Sumo.DataAccess.SOFModel ())
            {
                try
                {
                    var answer = dc.Answers.Where(x => x.Id == answerId).FirstOrDefault();
                  
                    if (answer.Accepted)
                        answer.Accepted = false;

                    else
                        answer.Accepted = true;
                    
                    dc.SaveChanges();
                     list= dc.Answers.AsNoTracking().Where(x => x.QuestionId == answer.QuestionId).ToList();
                    //answerVM = new List<AnswerVM>();
                    //foreach (Answer item in list)
                    //{
                    //    AnswerVM newAnswer = new AnswerVM();
                    //    newAnswer.Answer1 = item.Answer1;
                    //    newAnswer.AnswerVotes = item.AnswerVotes;
                    //    newAnswer.Question = item.Question;
                    //    newAnswer.User = item.User;
                    //    answerVM.Add(newAnswer);
                    //}
                    

                }
                catch (Exception e)
                {
                    throw;
                }
                
            }
          // var data = JsonConvert.SerializeObject(list);
            return Json("Onaylandı", JsonRequestBehavior.AllowGet);

        }

    }
}