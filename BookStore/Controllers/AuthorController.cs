using BookStoreBL.ModelView;
using BookStoreBL.Repositery;
using BookStoreDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        // GET: AuthorController
        private readonly IBookStoreRepositery<Author> authorrep;
        public AuthorController(IBookStoreRepositery<Author> authorrep)
        {
            this.authorrep = authorrep;

        }
        public ActionResult Index()
        {
            var model = authorrep.List();
            return View(model);
        }


        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorMW model)
        {
            if (ModelState.IsValid)
            {
                try
            {
                
                    var author = new Author()
                    {
                        FullName = model.FullName
                    };
                    authorrep.Add(author);

                    return RedirectToAction(nameof(Index));

                
            }
            catch
            {
                return View();
                }
            }
            
                ModelState.AddModelError("", "You have to fill all the required fields!");
                return View();

            
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetAuthorVM(id));
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AuthorMW authorMW )
        {
            if (ModelState.IsValid)
            {
                var model = new Author()
                {
                    Id= authorMW.Id,
                    FullName = authorMW.FullName
                };
                authorrep.Update(model);
                try
                {

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View();
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View(GetAuthorVM(id));
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deleteauthor(int id)
        {
              try
                {
                authorrep.Delete(id);

                return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
           
        }

        //maping
       private AuthorMW GetAuthorVM(int id)
        {
            var model = authorrep.Find(id);
            var authorVM = new AuthorMW()
            {
                Id = model.Id,
                FullName = model.FullName
            };
            return authorVM;
        }

    }
}
