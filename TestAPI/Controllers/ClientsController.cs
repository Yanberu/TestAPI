using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using ClientsProject.Models;
using Newtonsoft.Json;
using System.Web.Configuration;
using ClientsProject.Models;
using System.Threading;
using System.Threading.Tasks;


namespace ClientsProject.Controllers
{
    
    public class ClientsController : Controller
    {
        private ClientDBContext db = new ClientDBContext();
        private static IDictionary<Guid, int> _tasks = new Dictionary<Guid, int>();



        // GET: Clients
        public ActionResult Index(string searchString)
        {
            var clients = from m in db.Clients
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.Phone.Contains(searchString));
            }
            
            
            return View(clients);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            
            
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,MiddleName,LastName,Phone,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,MiddleName,LastName,Phone,Email")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
                        
        }
                
        [HttpPost]
        public ActionResult DeleteSelected(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"].Split(new char[] { ',' });
            
            foreach (string id in ids)
            {
                
                var client = this.db.Clients.Find(int.Parse(id));

                this.db.Clients.Remove(client);
                this.db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        
        public ActionResult Start()
        {

            var taskId = Guid.NewGuid();
            _tasks.Add(taskId, 0);

            Task.Factory.StartNew(() =>
            {
                for (var i = 0; i <= 100; i++)
                {
                    _tasks[taskId] = i;
                    Thread.Sleep(50);
                }
                
                _tasks.Remove(taskId);
            });
            return Json(taskId);

        }

        [HttpPost]
        public ActionResult Progress(Guid id)
        {
            return Json(_tasks.Keys.Contains(id) ? _tasks[id] : 100);
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
