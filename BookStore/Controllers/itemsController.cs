using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class itemsController : Controller
    {
        //disable automatic connection
        // private HoneyBookStorePart db = new HoneyBookStorePart();

        private IitemsMock db;
        private object items;

        //default constructor

        public itemsController()
        {
            this.db = new EFitems();
        }

        //mock constructor
        public itemsController(IitemsMock mock)
        {
            this.db = mock;
        }


        // GET: items
        public ActionResult Index()
        {
            var items = db.items.Include(a => a.item_id).Include(a => a.item_name).Include(a => a.item_price).Include(a => a.item_quantity);
            //return View(db.items.ToList());
            return View("Index", items.ToList());
        }

        // GET: items/Details/5
        [OverrideAuthorization]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                // scaffold
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            // old scafffolding does not work with the new mock
            // item item = db.items.Find(id);

            // the code which works for both EF and mock repositories is
            item item = db.items.SingleOrDefault(a => a.item_id == id);

            if (item == null)
            {
                //scaffold take out 
                // return HttpNotFound();
                return View("Error");
            }
            return View("Details", item);
        }

        // GET: items/Create
        [Authorize(Roles = "Customer")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "item_id,item_name,item_price,item_quantity")] item item)
         {
           if (ModelState.IsValid)
         {
                // db.items.Add(item);
                db.Save(item);
       // db.SaveChanges();
        return RedirectToAction("Index");
               }

               return View("Create",item);
         }

        // GET: items/Edit/5
        [Authorize(Roles = "Customer")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //item item = db.items.Find(id);
            item item = db.items.SingleOrDefault(a => a.item_id == id);
            if (item == null)
            {
                // return HttpNotFound();
                return View("Error");
            }
            return View("Edit", item);
        }

        // POST: items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "item_id,item_name,item_price,item_quantity")] item item)
        {
            if (ModelState.IsValid)
            {
                // db.Entry(item).State = EntityState.Modified;
                db.Save(item);
                //db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View("Edit", item);
        }

        // GET: items/Delete/5
        // [Authorize(Roles = "Customer")]
        // [Authorize(Roles = "Administrator")]
        //  public ActionResult Delete(int? id)
        //   {
        //       if (id == null)
        //       {
        //       return View("Error");  //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        // item item = db.items.Find(id);
        //    item item = db.items.SingleOrDefault(a => a.item_id == id);
        //    if (item == null)
        //    {
        // return HttpNotFound();
        //      return View("Error");
        //   }
        //     return View("Delete", item);
        //  }



        // POST: items/Delete/5
         [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
          public ActionResult DeleteConfirmed(int id)
        {
          //item item = db.items.Find(id);
        //db.items.Remove(item);
           //    db.SaveChanges();
           if(id == null)
                {
                return View("Error");
            }

            item item = db.items.SingleOrDefault(a => a.item_id == id);

            if(item == null)
            {
                return View("Error");
            }

            db.delete(item);

            return RedirectToAction("Index");
          }

      //  protected override void Dispose(bool disposing)
      //  {
       //   if (disposing)
       // {
      //    db.Dispose();
      //  }
     //   base.Dispose(disposing);
       //  }
    }
}

