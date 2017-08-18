using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AssociationsController : Controller
    {
        private SubsidiariesEntities db = new SubsidiariesEntities();

        // GET: Associations
        public ActionResult Index()
        {
            var associations = db.Associations.Include(a => a.CompanyName).Include(a => a.AssociationType);
            return View(associations.ToList());
        }

        // GET: Associations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        // GET: Associations/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "CompanyName1");
            ViewBag.AssocType = new SelectList(db.AssociationTypes, "AssocType", "Description");
            return View();
        }

        // POST: Associations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,AssocID,AssocType,AssPercent,DirectRel,UpdateDate,ChangeDate,NoShares_YN")] Association association)
        {
            if (ModelState.IsValid)
            {
                db.Associations.Add(association);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "CompanyName1", association.CompanyID);
            ViewBag.AssocType = new SelectList(db.AssociationTypes, "AssocType", "Description", association.AssocType);
            return View(association);
        }

        // GET: Associations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "CompanyName1", association.CompanyID);
            ViewBag.AssocType = new SelectList(db.AssociationTypes, "AssocType", "Description", association.AssocType);
            return View(association);
        }

        // POST: Associations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,AssocID,AssocType,AssPercent,DirectRel,UpdateDate,ChangeDate,NoShares_YN")] Association association)
        {
            if (ModelState.IsValid)
            {
                db.Entry(association).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.CompanyNames, "CompanyID", "CompanyName1", association.CompanyID);
            ViewBag.AssocType = new SelectList(db.AssociationTypes, "AssocType", "Description", association.AssocType);
            return View(association);
        }

        // GET: Associations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        // POST: Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Association association = db.Associations.Find(id);
            db.Associations.Remove(association);
            db.SaveChanges();
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
