﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using liuyida.Models;

namespace liuyida.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.OrderByDescending(o => o.Id).Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.Products = db.Products;
            ViewBag.ReturnUrl = Request.QueryString["r"];
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,CreationTime,DeliveryTime,DeliveryFee,DeliveryMethod,DeliveryAddress,PaymentMethod,Price,Discount,Paid,Status,Note,OrderItems")] Order order, string returnUrl)
        {
            IList<OrderItem> orderItems = order.OrderItems;
            order.OrderItems = null;
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                if (db.SaveChanges() > 0)
                {
                    foreach (OrderItem oi in orderItems)
                    {
                        if (oi.Quantity >= 1)
                        {
                            oi.OrderId = order.Id;
                            db.OrderItems.Add(oi);
                        }
                    }
                    db.SaveChanges();
                    return Redirect(returnUrl);
                }
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            ViewBag.Products = db.Products;
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            ViewBag.Products = db.Products;
            ViewBag.ReturnUrl = Request.QueryString["r"];
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,CreationTime,DeliveryTime,DeliveryFee,DeliveryMethod,DeliveryAddress,PaymentMethod,Price,Discount,Paid,Status,Note,OrderItems")] Order order, string returnUrl)
        {
            IList<OrderItem> orderItems = order.OrderItems;
            order.OrderItems = null;
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    db.OrderItems.RemoveRange(db.OrderItems.Where(oi => oi.OrderId == order.Id));
                    foreach (OrderItem oi in orderItems)
                    {
                        if (oi.Quantity >= 1)
                        {
                            oi.OrderId = order.Id;
                            db.OrderItems.Add(oi);
                        }
                    }
                    db.SaveChanges();
                    return Redirect(returnUrl);
                }
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = Request.QueryString["r"];
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            Order order = db.Orders.Find(id);
            IList<OrderItem> orderItems = db.OrderItems.Where(oi => oi.OrderId == id).ToList();
            foreach (var oi in orderItems)
            {
                db.OrderItems.Remove(oi);
            }
            db.Orders.Remove(order);
            db.SaveChanges();
            return Redirect(returnUrl);
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
