using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMedia.Web.Helpers
{
    public class PagesHelper
    {
        public static PagesDto<T> Pages<T>(List<T> values,int p)
        {
            double count=values.Count*1.0;
            int pageCount =Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(count / 5)));
            PagesDto<T> page = new PagesDto<T> { PageCount=pageCount , Data=values.Skip(5*(p-1)).Take(5).ToList()};
            return page;
        }
    }
    public class PagesDto<T>
    {
        public List<T> Data { get; set; }
        public int PageCount { get; set; }
    }
}
