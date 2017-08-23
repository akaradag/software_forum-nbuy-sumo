using BilgeAdam___Sumo.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdam___Sumo.Models
{
    public class AnswerVM
    {
        public AnswerVM()
        {
            AnswerVotes = new HashSet<AnswerVote>();
            Answer1 = new HashSet<Answer>();
        }
        public Answer Answer { get; set; }
        public string QuestionTitle { get; set; }
        public ICollection<AnswerVote> AnswerVotes { get; set; }

        public ICollection<Answer> Answer1 { get; set; }
    }
}