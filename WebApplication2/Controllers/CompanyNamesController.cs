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
    public class CompanyNamesController : Controller
    {
        private SubsidiariesEntities db = new SubsidiariesEntities();

        // GET: CompanyNames
        public ActionResult Index(string CompanyTypeID, string ExchangeCode, string searchString, string searchBy)
        {

           // string searchby = "Company Name" + "Short Code" + "CountryID" + "BusinessSectorID";


            var GenreLst = new List<string>();

            var GenreQry = from d in db.CompanyNames
                           orderby d.ExchangeCode
                           select d.ExchangeCode;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.ExchangeCode = new SelectList(db.Exchanges, "ExchangeCode", "ExchangeName");


            var TypLst = new List<string>();

            var TypQry = from d in db.CompanyNames
                         orderby d.CompanyTypeID
                         select d.CompanyTypeID;

            TypLst.AddRange(TypQry.Distinct());
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "CompanyTypeID", "CompanyTypeDesc");

            ViewBag.searchBy = new SelectList(db.CompanyNames, "searchby");


            var companyNames = from m in db.CompanyNames
                               select m;



            if (!String.IsNullOrEmpty(searchString))
            {
                companyNames = companyNames.Where(s => s.CompanyName1.Contains(searchString));


            }


            if (!string.IsNullOrEmpty(ExchangeCode))
            {
                companyNames = companyNames.Where(x => x.ExchangeCode == ExchangeCode);
            }

            if (!string.IsNullOrEmpty(CompanyTypeID))
            {
                companyNames = companyNames.Where(x => x.CompanyTypeID == CompanyTypeID);
            }

            return View(companyNames);
        }

        // GET: CompanyNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyName companyName = db.CompanyNames.Find(id);
            if (companyName == null)
            {
                return HttpNotFound();
            }
            return View(companyName);
        }

        // GET: CompanyNames/Create
        public ActionResult Create()
        {
            ViewBag.ExchangeCode = new SelectList(db.Exchanges, "ExchangeCode", "ExchangeName");
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "CompanyTypeID", "CompanyTypeDesc");
            return View();
        }

        // POST: CompanyNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,ExchangeCode,CompanyName1,ShortCode,CorpInfo,CountryID,BusinessSectorID,CompanyTypeID,UpdateDate")] CompanyName companyName)
        {
            if (ModelState.IsValid)
            {
                db.CompanyNames.Add(companyName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExchangeCode = new SelectList(db.Exchanges, "ExchangeCode", "ExchangeName", companyName.ExchangeCode);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "CompanyTypeID", "CompanyTypeDesc", companyName.CompanyTypeID);
            return View(companyName);
        }

        // GET: CompanyNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyName companyName = db.CompanyNames.Find(id);
            if (companyName == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExchangeCode = new SelectList(db.Exchanges, "ExchangeCode", "ExchangeName", companyName.ExchangeCode);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "CompanyTypeID", "CompanyTypeDesc", companyName.CompanyTypeID);
            return View(companyName);
        }

        // POST: CompanyNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,ExchangeCode,CompanyName1,ShortCode,CorpInfo,CountryID,BusinessSectorID,CompanyTypeID,UpdateDate")] CompanyName companyName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExchangeCode = new SelectList(db.Exchanges, "ExchangeCode", "ExchangeName", companyName.ExchangeCode);
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "CompanyTypeID", "CompanyTypeDesc", companyName.CompanyTypeID);
            return View(companyName);
        }

        // GET: CompanyNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyName companyName = db.CompanyNames.Find(id);
            if (companyName == null)
            {
                return HttpNotFound();
            }
            return View(companyName);
        }

        // POST: CompanyNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyName companyName = db.CompanyNames.Find(id);
            db.CompanyNames.Remove(companyName);
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
