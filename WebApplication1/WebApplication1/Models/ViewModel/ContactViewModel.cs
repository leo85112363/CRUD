using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.EFModel;

namespace WebApplication1.Models.ViewModel
{
    public class ContactViewModel
    {
       public List<Movie> Entities { get; set; }

        public QueryModel Query { get; set; }

        public class QueryModel
        {
           public string movieName { get; set; }
        
        }

    }
}