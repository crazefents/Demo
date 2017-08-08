using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApplication2.Controllers
{
    public class CompanyNames1Controller : Controller
    {
        private SubsidiariesEntities db = new SubsidiariesEntities();

        // GET: CompanyNames1
        public ActionResult Index(string CountryID,string BusinessSectorID,string CompanyName1, string fields,string CompanyTypeID, string ExchangeCode, string searchString, string searchBy)
        {

            // string searchby = "Company Name" + "Short Code" + "CountryID" + "BusinessSectorID";

            var field = new List<string>();
                var allList = new object[] { "Company Names", "Country", "Business Sector" };
            

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


            
            ViewBag.fields = new SelectList(allList);


            var companyNames = from m in db.CompanyNames
                               select m;


           

            if (!String.IsNullOrEmpty(searchString))
            {
                
              

                if (fields.Equals("Company Names"))
                {
                    companyNames = companyNames.Where(s => s.CompanyName1.Contains(searchString));

                }
                if (fields.Equals("Country"))
                {
                    companyNames = companyNames.Where(s => s.CountryID.Contains(searchString));

                }


                if (fields.Equals("Business Sector"))
                {
                    companyNames = companyNames.Where(s => s.BusinessSectorID.Contains(searchString));

                }

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

        

        // GET: CompanyNames1/Details/5
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

        // GET: CompanyNames1/Create
        public ActionResult Create()
        {
            ViewBag.ExchangeCode = new SelectList(db.Exchanges, "ExchangeCode", "ExchangeName");
            ViewBag.CompanyTypeID = new SelectList(db.CompanyTypes, "CompanyTypeID", "CompanyTypeDesc");
            return View();
        }

        // POST: CompanyNames1/Create
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

        // GET: CompanyNames1/Edit/5
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

        // POST: CompanyNames1/Edit/5
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

        // GET: CompanyNames1/Delete/5
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

        // POST: CompanyNames1/Delete/5
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
