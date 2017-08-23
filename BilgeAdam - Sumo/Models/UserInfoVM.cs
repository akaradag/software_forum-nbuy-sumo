using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BilgeAdam___Sumo.DataAccess;

namespace BilgeAdam___Sumo.Models
{
    public class UserInfoVM
    {
        public UserInfoVM()
        {
            Questions = new List<Question>();
            Answers = new List<Answer>();
            Rosettes = new List<Rosette>();
            Followers = new List<User>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }

        public List<User> Followers { get; set; }

        public int RepPoint { get; set; }
        public string CreateDate { get; set; }
        public string Role { get; set; }
        public string  Image { get; set; }

        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Rosette> Rosettes { get; set; }

    }
}