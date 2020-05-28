using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Todolist.Models;

namespace Todolist.Controllers
{
    public class ListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        // GET: Lists
        public ActionResult Index()
        {
            var List = new List();
            var msg = User.Identity.GetUserId();
            var data2= from s in db.Lists where s.UserId ==  msg select s;
            return View(data2.ToList());
        }

       

       

        // POST: Lists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createn([Bind(Include = "Id,UserId,Title,DueDate,Done")] List list)
        {
            
                db.Lists.Add(list);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            //return View(list);
        }

       

        // POST: Lists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editn([Bind(Include = "Id,UserId,Title,DueDate")] List list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(list);
        }


        [HttpPost]
        public JsonResult Done(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var row = db.Lists.Single(x => x.Id == id);
            row.Done = 2;
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {

            }
            return Json(row, JsonRequestBehavior.AllowGet);
            //return Redirect("Index");
        }
        [HttpGet]
        public PartialViewResult Createn()
        {
            ViewBag.id = User.Identity.GetUserId();
            return PartialView();
        }
        [HttpPost]
        public JsonResult Createnh(List list)
        {

            db.Lists.Add(list);
            db.SaveChanges();
            return Json(list, JsonRequestBehavior.AllowGet);
           // return RedirectToAction("Index");
        }


        [HttpGet]
        public PartialViewResult Editn(int?id)
        {
           
            List list = db.Lists.Find(id);


            return PartialView(list);
        }

        
        // GET: Lists/Delete/5
        //public ActionResult Delete(int? id)
       // {
           // if (id == null)
            //{
               // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //List list = db.Lists.Find(id);
            //if (list == null)
            //{
               // return HttpNotFound();
           // }
           // return View(list);
       // }

       
        [HttpPost]
        public JsonResult Deleten(int?id)
        {
            List list = db.Lists.Find(id);
            db.Lists.Remove(list);
            db.SaveChanges();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
