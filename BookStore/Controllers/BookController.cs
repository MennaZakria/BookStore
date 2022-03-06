using BookStoreBL.Helper;
using BookStoreBL.ModelView;
using BookStoreBL.Repositery;
using BookStoreDAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Views
{
    public class BookController : Controller
    {
        // GET: BookController
        private readonly IBookStoreRepositery<Book> Bookrep;
        //private readonly IHostingEnvironment hosting;

        public BookController(IBookStoreRepositery<Book> Bookrep )
        {
            this.Bookrep = Bookrep;

        }
        public ActionResult Index()
        {
            var model = Bookrep.List();
            return View(model);
        }
        [HttpPost]
        public ActionResult Search(string SearchValue)
        {
            var model = Bookrep.Search(b => b.Title.Contains(SearchValue) || b.Description.Contains(SearchValue) || b.Author.FullName.Contains(SearchValue));
            return View("Index", model);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookMW model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string FileName =  UploadFile.UploadFiles(model.Fileupload, "images") ?? string.Empty ;
                    
                    Bookrep.Add(GetBook(model , FileName));
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

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(GetBookVM(id));
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookMW Model)
        {
            if (ModelState.IsValid)
            {
                var FileName = string.Empty;
                if(Model.ImageUrl != Model.Fileupload.FileName)
                {
                    UploadFile.RemoveFile("images", Model.ImageUrl);
                    FileName = UploadFile.UploadFiles(Model.Fileupload, "images");
                }


                Bookrep.Update(GetBook(Model, FileName));
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

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {

            return View(GetBookVM(id));
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int id)
        {
            try
            {
                Bookrep.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        //maping
        private BookMW GetBookVM(int id)
        {
            var model = Bookrep.Find(id);
            var BookVM = new BookMW()
            {
                Id = model.Id,
                Title=model.Title,
                Description = model.Description,
                ImageUrl=model.ImageUrl,
                AuthorId=model.AuthorId
            };
            return BookVM;
        }
        private Book GetBook(BookMW model, string fileName)
        {
            //string fileName = UploadFile(model.Fileupload , model.ImageUrl) ?? string.Empty;
            var book = new Book()
            {

                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                ImageUrl = fileName,
                AuthorId = model.AuthorId
            };
            return book;
        }
        //string UploadFiles(IFormFile file, string imageUrl)
        //{
        //    if (file != null)
        //    {
        //        string uploads = Path.Combine(hosting.WebRootPath, "uploads/images");

        //        string newPath = Path.Combine(uploads, file.FileName);
        //        if (string.IsNullOrEmpty(imageUrl)){
        //            file.CopyTo(new FileStream(newPath, FileMode.Create));
        //            return file.FileName;
        //        }
        //        string oldPath = Path.Combine(uploads, imageUrl);

        //        if (oldPath != newPath)
        //        {
        //            System.IO.File.Delete(oldPath);
        //            file.CopyTo(new FileStream(newPath, FileMode.Create));
        //        }

        //        return file.FileName;
        //    }

        //    return imageUrl;
        //}

    }
}
