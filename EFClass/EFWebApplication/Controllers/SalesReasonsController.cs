using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFEntities;
using EFBusinessLogic;

namespace EFWebApplication.Controllers
{
    public class SalesReasonsController : Controller
    {
        private AdventureWorks2008Entities db = new AdventureWorks2008Entities();
        private BusinessLogic businessLogic = new BusinessLogic();
        private SalesReason salesReason = new SalesReason();

        // GET: SalesReasons
        public ActionResult Index()
        {
            return View(businessLogic.RetrieveAllRecords());
        }

        // GET: SalesReasons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            salesReason = businessLogic.RetrieveSpecificRecord((int)id);

            if (salesReason == null)
            {
                return HttpNotFound();
            }
            return View(salesReason);
        }

        // GET: SalesReasons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesReasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SalesReason salesReason)
        {
            salesReason.ModifiedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                //db.SalesReasons.Add(salesReason);
                //db.SaveChanges();

                businessLogic.AddRecord(salesReason);

                return RedirectToAction("Index");
            }

            return View(salesReason);
        }

        // GET: SalesReasons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //SalesReason salesReason = db.SalesReasons.Find(id);
            SalesReason salesReason = businessLogic.RetrieveSpecificRecord((int)id);

            if (salesReason == null)
            {
                return HttpNotFound();
            }
            return View(salesReason);
        }

        // POST: SalesReasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SalesReason salesReason)
        {
            salesReason.ModifiedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                //db.Entry(salesReason).State = EntityState.Modified;
                //db.SaveChanges();

                businessLogic.UpdateRecord(salesReason);

                return RedirectToAction("Index");
            }
            return View(salesReason);
        }

        // GET: SalesReasons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SalesReason salesReason = businessLogic.RetrieveSpecificRecord(new SalesReason() { SalesReasonID = Convert.ToInt32(id) });

            if (salesReason == null)
            {
                return HttpNotFound();
            }
            return View(salesReason);
        }

        // POST: SalesReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //SalesReason salesReason = db.SalesReasons.Find(id);
            //db.SalesReasons.Remove(salesReason);
            //db.SaveChanges();

            businessLogic.DeleteRecord(id);

            return RedirectToAction("Index");
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
