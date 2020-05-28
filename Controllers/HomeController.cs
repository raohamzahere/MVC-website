using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JqeryMVCLoadExample.Models;
namespace JqeryMVCLoadExample.Controllers
{
    public class HomeController : Controller
    {
         DataClasses1DataContext d = new DataClasses1DataContext();
        // GET: /Home/
        string temp;
        public ActionResult Index()
        {


            return View(d.c_names.ToList());
        }
        public ActionResult Delete(int id)
        {
            var s = d.bookinfos.First(x => x.Id == id);
            d.bookinfos.DeleteOnSubmit(s);
            d.SubmitChanges();

            return  RedirectToAction("Index");
        }





        public ActionResult Edit(int id)
        {
            Session["id"] = id;
            return View();
        }
        public ActionResult EditDone()
        {
            int id = Int32.Parse(Request["_id"]);

            var a = d.bookinfos.First(s => s.Id == id);
            if (Request["title"] != null && Request["title"] != "")
            {
                a.title = Request["title"];
            }
            if (Request["author"] != null && Request["author"] != "")
            {
                a.author = Request["author"];
            }
            if (Request["price"] != null && Request["price"] != "")
            {
                a.price = Request["price"];
            }
            if (Request["date"] != null && Request["date"] != "")
            {
                a.date = Request["date"];
            }
            d.SubmitChanges();


            return RedirectToAction("index");
        }
        public ActionResult datashow()
        {
            

            string g = Request["cat"];
            Session["catogory"] = g;
            var obj = d.bookinfos.Where(d => d.c_name == g);
            return View(obj.ToList());
        }
        public ActionResult Add_category()
        {
            string name = Request["cat"];


            c_name p = new c_name();
            if (name != "" && name != null)
            {

                p.c_name1 = name;

                d.c_names.InsertOnSubmit(p);
                d.SubmitChanges();
            }
            return RedirectToAction("Index");
        }








        public ActionResult Add()
        {
            string name = Request["title"];
            string author = Request["author"];
            string price = Request["price"];
            string date= Request["date"];
            string cname = Request["catogory"];
            int temp = (from  t in d.bookinfos
                        orderby t.Id descending
                        select t.Id).First()  ;
            int value=temp;
            bookinfo p = new bookinfo();
            p.title = name;
            p.price = price;
            p.author = author;
            p.c_name = cname;
            p.date = date;
            p.Id = value+1;
            d.bookinfos.InsertOnSubmit(p);
            d.SubmitChanges();

            return RedirectToAction("Index");
        }
    }
}
