using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessObject;

namespace TodoListNetWeb.Controllers
{
    public class ListController : Controller
    {
        TodoListDBEntities db = new TodoListDBEntities();
        // GET: List
        public ActionResult Index()
        {
            var Lists = db.Lists.ToList();
            return View(Lists);
        }
    }
}