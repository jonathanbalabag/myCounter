using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCounter.Models;

namespace MyCounter.Controllers
{
    public class CounterController : Controller
    {
        // GET: Counter
        public ActionResult Index(Counter counter)
        {

            //get count from database
            myDBEntities db = new myDBEntities();
            var cnt =  db.tblCounts.Where(n => n.Id == 1).Select(n => new Counter{count=n.count }).ToList();

            //if the button is triggered
            if (counter.isIncrease)
            {
                //if count is less 10 --> increase the count
                if (cnt[0].count < 10)
                {
                    //update the database
                    tblCount tbl = new tblCount();
                    tbl.Id = 1;
                    tbl.count = cnt[0].count + 1;
                    db.tblCounts.Attach(tbl);
                    var entry = db.Entry(tbl);
                    entry.Property(e => e.count).IsModified = true;
                    db.SaveChanges();
                    cnt[0].count++;
                }
            }
            //create view object 
            ViewBag.myCounter = new Counter {count =cnt[0].count };

            return View();
        }
        //this resets the counter
        public ActionResult Reset(Counter counter)
        {
                    //updates the db counter to zero
                    myDBEntities db = new myDBEntities();
                    tblCount tbl = new tblCount();
                    tbl.Id = 1;
                    tbl.count = 0;
                    db.tblCounts.Attach(tbl);
                    var entry = db.Entry(tbl);
                    entry.Property(e => e.count).IsModified = true;
                    db.SaveChanges();
                    //redirects to  action 'Index '
                    return RedirectToAction("Index", counter);
        }
    }
}