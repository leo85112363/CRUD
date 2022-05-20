using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.EFModel;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MovieList(ContactViewModel.QueryModel query)
        {
            ViewBag.Message = "Your contact page.";
            var model = new ContactViewModel();

            using (var dbEntities = new MovieEntities())
            {
                model.Entities = dbEntities.Movie.ToList();
                if (!string.IsNullOrEmpty(query.movieName))
                {
                    model.Entities = model.Entities.Where(c => c.MovieName.Contains(query.movieName)).ToList();
                }

             
            }

            return View(model);
        }

        public ActionResult MovieEdit(string movieSn)
        {
            var model = new MovieEditViewModel();
            var dbEntities = new MovieEntities();

            if (!string.IsNullOrEmpty(movieSn))
            {
                model.Entity = dbEntities.Movie.Where(c => c.MovieSn == movieSn).FirstOrDefault();
            }     
            
            return View(model);
        }

        public ActionResult UpdateMovieEdit(MovieEditViewModel model)
        {

            var dbEntities = new MovieEntities();
            if (!string.IsNullOrEmpty(model.Entity.MovieSn))
            {
                var entity = dbEntities.Movie.Where(c => c.MovieSn == model.Entity.MovieSn).FirstOrDefault();

                dbEntities.Entry(entity).State = EntityState.Detached;

                entity.MovieName = model.Entity.MovieName;
                entity.Rate = model.Entity.Rate;
                entity.Actor = model.Entity.Actor;

               
                dbEntities.Entry(entity).State = EntityState.Modified;
                dbEntities.SaveChanges();
            }
            else
            {
                //新增
                model.Entity.MovieSn = Guid.NewGuid().ToString();
                dbEntities.Entry(model.Entity).State = EntityState.Added;
                dbEntities.SaveChanges();
            }



            return Content("成功！");
        }

        public ActionResult MovieDelete(string movieSn)
        {
            var dbEntities = new MovieEntities();
            var entity = dbEntities.Movie.Where(c => c.MovieSn == movieSn).FirstOrDefault();


            dbEntities.Entry(entity).State = EntityState.Deleted;
            dbEntities.SaveChanges();

            return Content("刪除成功！");
        }
    }
}