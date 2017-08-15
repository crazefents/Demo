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
    public class blacc : Controller
    {
        private SubsidiariesEntities db = new SubsidiariesEntities();

        // GET: blacc
        public ActionResult Index()
        {
            var investments = db.Investments.Include(i => i.InvestmentType);
            return View(investments.ToList());
        }

        // GET: blacc/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investment investment = db.Investments.Find(id);
            if (investment == null)
            {
                return HttpNotFound();
            }
            return View(investment);
        }

        // GET: blacc/Create
        public ActionResult Create()
        {
            ViewBag.InvestType = new SelectList(db.InvestmentTypes, "InvestType", "Description");
            return View();
        }

        // POST: blacc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,InvestID,InvestType,InvPercent,DirectRel,UpdateDate,ChangeDate,NoShares_YN")] Investment investment)
        {
            if (ModelState.IsValid)
            {
                db.Investments.Add(investment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvestType = new SelectList(db.InvestmentTypes, "InvestType", "Description", investment.InvestType);
            return View(investment);
        }

        // GET: blacc/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investment investment = db.Investments.Find(id);
            if (investment == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvestType = new SelectList(db.InvestmentTypes, "InvestType", "Description", investment.InvestType);
            return View(investment);
        }

        // POST: blacc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,InvestID,InvestType,InvPercent,DirectRel,UpdateDate,ChangeDate,NoShares_YN")] Investment investment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(investment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvestType = new SelectList(db.InvestmentTypes, "InvestType", "Description", investment.InvestType);
            return View(investment);
        }

        // GET: blacc/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Investment investment = db.Investments.Find(id);
            if (investment == null)
            {
                return HttpNotFound();
            }
            return View(investment);
        }

        // POST: blacc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Investment investment = db.Investments.Find(id);
            db.Investments.Remove(investment);
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
