using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository Context;

        public ProductManagerController()
        {
            Context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> Products = Context.List().ToList(); 
            return View(Products);
        }

        public ActionResult Create()
        {
            Product p = new Product();
            return View(p);
        }

        [HttpPost]
        public ActionResult Create(Product products)
        {
            if (!ModelState.IsValid)
            {
                return View(products);
            }
            else
            {
                Context.Insert(products);
                Context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string ID)
        {
            Product Prod = Context.Find(ID);
            if (Prod == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(Prod);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product Product, string ID)
        {
            Product ProductToEdit = Context.Find(ID);
            if (ProductToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(Product);
                }

                ProductToEdit.Category = Product.Category;
                ProductToEdit.Description = Product.Description;
                ProductToEdit.Name = Product.Name;
                ProductToEdit.Image = Product.Image;
                ProductToEdit.Prize = Product.Prize;

                Context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string ID)
        {
            Product ProductToDelete = Context.Find(ID);
            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                
                return View(ProductToDelete);
                
            }
        }

        [HttpPost]
        [ActionName("Delete")]

        public ActionResult ConfirmDelete(string ID)
        {
            Product ProductToDelete = Context.Find(ID);
            if (ProductToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(ID);
                Context.Commit();
                return RedirectToAction("Index");

            }
        }
    }
}