﻿using System;
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


namespace ClientsProject.Controllers
{
    
    public class ClientsController : Controller
    {
        private ClientDBContext db = new ClientDBContext();

        

        // GET: Clients
        public ActionResult Index(string searchString)
        {
            var clients = from m in db.Clients
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.Phone.Contains(searchString));
            }
            
            if (TempData["Buffer"] == null)
            {
                ViewBag.progress = 100;
            }
            else
            {
                ViewBag.progress = TempData["Buffer"];
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
                TempData["Buffer"] = 100;
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
            TempData["Buffer"] = 100 - 100 / db.Clients.Count();
            db.Clients.Remove(client);
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
