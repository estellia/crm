using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPIServices.Models;

namespace WebAPIServices.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult values()
        {
            return Json(new string[] {"value1", "value2"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMSG()
        {
            MSGBindingModels MSGBindingModels = new MSGBindingModels();
            var o = _db.OMSG.Find(1);
            var b = _db.MSG1.Find(1);
            MSGBindingModels.OMSG = o;
            MSGBindingModels.MSG1 = b;
            return Json(MSGBindingModels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddTestWithObject(MSGBindingModels MSG)
        {
            try
            {
                var o = MSG.OMSG;
                var b = MSG.MSG1;
                _db.OMSG.Add(o);
                _db.MSG1.Add(b);
                _db.SaveChanges();
                return Json("Ok", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Addtest()
        {
            try
            {
                var o = new OMSG();
                var b = new MSG1();
                o.SequenceID = 1;
                o.Comments = "a";
                o.Timestamp=DateTime.Now;
                o.Status = 0;
                o.FromSystem = "crm";
                o.FromCompany = "crm";
                o.FieldNames = "1";
                o.ObjectType = "aa";
                o.Flag = "1";
                o.TransType = "1";
                o.FieldValues = "1";
                o.UpDateTime=DateTime.Now;
                o.FieldsInKey = 1;
                b.SequenceID = 1;
                b.Content = "a";
                b.iLength = 12;
                _db.OMSG.Add(o);
                _db.MSG1.Add(b);
                _db.SaveChanges();
                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.InnerException.InnerException.Message, JsonRequestBehavior.AllowGet);

            }
          


        }
    }
}