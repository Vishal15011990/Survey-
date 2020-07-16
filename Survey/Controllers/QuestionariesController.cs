using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Survey.Models;

namespace Survey.Controllers
{
    public class QuestionariesController : Controller
    {
        private SurveyEntities db = new SurveyEntities();

        // GET: Questionaries
        public ActionResult Index()
        {
            var questionarie = db.Questionarie.Include(q => q.Country_Info).Include(q => q.State_Info);
            return View(questionarie.ToList());
        }

        // GET: Questionaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionarie questionarie = db.Questionarie.Find(id);
            if (questionarie == null)
            {
                return HttpNotFound();
            }
            return View(questionarie);
        }

        // GET: Questionaries/Create
        public ActionResult Credentials()
        {
            //ViewBag.City = new SelectList(db.Cities, "City_Id", "City_Name");
            ViewBag.Country = new SelectList(db.Country_Info, "Country_Id", "Country_Name");
            ViewBag.State = new SelectList(db.State_Info, "State_Id", "State_Name");
            return View();
        }


        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Questionarie questionarie)
        {
            ViewBag.Country = new SelectList(db.Country_Info, "Country_Id", "Country_Name", questionarie.Country);
            ViewBag.State = new SelectList(db.State_Info, "State_Id", "State_Name", questionarie.State);
            questionarie.IsActive = true;
            questionarie.CreatedBy = Guid.NewGuid();
            questionarie.CreatedOn = DateTime.Now;
            db.Questionarie.Add(questionarie);
            db.SaveChanges();
            return Json(questionarie.Id);

        }



        #region Dropdownlist
        public JsonResult GetState(int Cid)
        {
            List<State_Info> statlist = db.State_Info.Where(x => x.Country_RefId == Cid).ToList();
            var Statelist = statlist.Select(m => new SelectListItem()
            {
                Text = m.State_Name.ToString(),
                Value = m.State_Id.ToString()
            });
            return Json(Statelist, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetCity(int Sid)
        //{
        //    List<Cities> cityList = db.Cities.Where(x => x.State_RefId == Sid).ToList();
        //    var Citylist = cityList.Select(m => new SelectListItem()
        //    {
        //        Text = m.City_Name,
        //        Value = m.City_Id.ToString()
        //    });
        //    return Json(Citylist, JsonRequestBehavior.AllowGet);
        //}
        #endregion

        #region Check Name EmailID Phone Availability
        public JsonResult Nameavailability(string name)
        {
            //bool result = !db.Questionarie.ToList().Exists(model => model.User_Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            var result = db.Questionarie.Where(x => x.User_Name == name).SingleOrDefault();
            if (result != null)
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult Emailavailability(string emailId)
        {
            bool result = !db.Questionarie.ToList().Exists(model => model.EmailId.Equals(emailId, StringComparison.CurrentCultureIgnoreCase));
            if (result != true)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("success", JsonRequestBehavior.AllowGet);

            }

        }
        public ActionResult Phoneavailability(string phone)
        {
            bool result = !db.Questionarie.ToList().Exists(model => model.Phone.Equals(phone, StringComparison.CurrentCultureIgnoreCase));
            if (result != true)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("success", JsonRequestBehavior.AllowGet);

            }
        }
        #endregion

        #region Login Logout
        [HttpPost]
        public ActionResult Login(Questionarie emp)
        {
            bool Isresult = db.Questionarie.Any(x => x.User_Name == emp.User_Name && x.Password == emp.Password);
            if(Isresult)
            {
                Session["Id"] = emp.Id;
                Session["Username"] = emp.User_Name;
                return Json("success");
            }
            
            return Json("error");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Credentials","Questionaries");

        }
        #endregion


    }
}

#region
//    // GET: Questionaries/Edit/5
//    public ActionResult Edit(int? id)
//    {
//        if (id == null)
//        {
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        }
//        Questionarie questionarie = db.Questionarie.Find(id);
//        if (questionarie == null)
//        {
//            return HttpNotFound();
//        }
//        ViewBag.City = new SelectList(db.CityMaster, "ID", "Name", questionarie.City);
//        ViewBag.Country = new SelectList(db.CountryMaster, "ID", "Name", questionarie.Country);
//        ViewBag.State = new SelectList(db.StateMaster, "ID", "Name", questionarie.State);
//        return View(questionarie);
//    }

//    // POST: Questionaries/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public ActionResult Edit([Bind(Include = "Id,Company_Name,User_Name,Password,First_Name,Last_Name,Country,State,City,Address_1,Address_2,Pincode,EmailId,Phone,IsActive,IsDelete,CreatedOn,CreatedBy")] Questionarie questionarie)
//    {
//        if (ModelState.IsValid)
//        {
//            db.Entry(questionarie).State = EntityState.Modified;
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        ViewBag.City = new SelectList(db.CityMaster, "ID", "Name", questionarie.City);
//        ViewBag.Country = new SelectList(db.CountryMaster, "ID", "Name", questionarie.Country);
//        ViewBag.State = new SelectList(db.StateMaster, "ID", "Name", questionarie.State);
//        return View(questionarie);
//    }

//    // GET: Questionaries/Delete/5
//    public ActionResult Delete(int? id)
//    {
//        if (id == null)
//        {
//            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        }
//        Questionarie questionarie = db.Questionarie.Find(id);
//        if (questionarie == null)
//        {
//            return HttpNotFound();
//        }
//        return View(questionarie);
//    }

//    // POST: Questionaries/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public ActionResult DeleteConfirmed(int id)
//    {
//        Questionarie questionarie = db.Questionarie.Find(id);
//        db.Questionarie.Remove(questionarie);
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }

//    protected override void Dispose(bool disposing)
//    {
//        if (disposing)
//        {
//            db.Dispose();
//        }
//        base.Dispose(disposing);
//    }
//}
#endregion

