using BilgeAdam___Sumo.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgeAdam___Sumo.Tools
{
    public static class Tool
    {
        public static string TimeToString(DateTime time)
        {
            TimeSpan time2 = DateTime.Now - time;
            if (time2.Days > 365)
            {
                return (time2.Days / 365) + " Yıl önce";
            }
            else if (time2.Days > 30)
            {
                return (time2.Days / 30) + " Ay önce";
            }
            else if (time2.Days > 0)
            {
                return time2.Days + " Gün önce";
            }
            else if (time2.Hours > 0)
            {
                return time2.Hours + " Saat önce";
            }
            else if (time2.Minutes > 0)
            {
                return time2.Minutes + " Dakika önce";
            }
            else
            {
                return "Az Önce";
            }

        }
        public static void WhichPage<T>(int PageNumber, List<T> list, ref int counter, ref int pageCount, T item)
        {
            if ((15 * (PageNumber - 1)) <= counter && counter < 15 * PageNumber)
            {
                list.Add(item);
                counter++;
                pageCount++;
            }
            else
            {
                pageCount++;
                if (pageCount <= (15 * (PageNumber - 1)))
                {
                    counter++;
                }
            }
        }
        public static List<Tag> GetTag(string tags, List<Tag> allTag)
        {
            string[] userTag = tags.Split(new char[] { ',', '/', '.' });
            List<Tag> questiyonTag = new List<Tag>();

            foreach (string userValue in userTag)
            {
                foreach (Tag dsb in allTag)
                {
                    if (dsb.TagName.ToLower() == userValue.ToLower())
                    {
                        questiyonTag.Add(dsb);
                        break;
                    }
                }
            }

            return questiyonTag;
        }
    }
}