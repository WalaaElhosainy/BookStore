using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IAuthorRepository<Book> bookRepository;
        private readonly IAuthorRepository<Author> authorRepository;
        private readonly IWebHostEnvironment Hosting;


        public BookController(IAuthorRepository<Book> bookRepository,
            IAuthorRepository<Author> authorRepository,
            IWebHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            Hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            IList<Book> books = bookRepository.list();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.findOne(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            string fileName = string.Empty;
            if (ModelState.IsValid)
            {

                try
                {
                    if(model.file != null)
                    {
                        string uploads = Path.Combine(Hosting.WebRootPath,"uploads");
                        fileName = model.file.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        model.file.CopyTo(new FileStream(fullPath,FileMode.Create));

                    }
                if (model.AuthorId == -1)
                {
                    ViewBag.message = "please select an author";
                    return View(GetAllAuthors());
                }
                var author = authorRepository.findOne(model.AuthorId);
                    Book book = new Book
                    {
                        id = model.BookId,
                        title = model.Title,
                        descreption = model.Description,
                        imgUrl = fileName,
                    author = author

                };

                bookRepository.add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View(GetAllAuthors());
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.findOne(id);
            var authorId = book.author == null ? book.author.id = 0 : book.author.id;

            var viewModel = new BookAuthorViewModel
            {
                BookId = book.id,
                Title = book.title,
                Description = book.descreption,
                Authors = authorRepository.list().ToList(),
                imageUrl = book.imgUrl
            
            };

            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel viewModel)
        {
            try
            {
                string fileName = string.Empty;
                string fullOldPath= string.Empty;
                if (viewModel.file != null)
                {
                    string uploads = Path.Combine(Hosting.WebRootPath, "uploads");
                    fileName = viewModel.file.FileName;
                    string fullPath = Path.Combine(uploads, fileName);

                    //delete old file 
                    string oldFileName = viewModel.imageUrl;
                    if(oldFileName==null)
                    {
                        viewModel.file.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    else
                    {
                         fullOldPath = Path.Combine(uploads, oldFileName);
                    }       

                    if(fullOldPath!=string.Empty && fullPath != fullOldPath)
                    {
                        System.IO.File.Delete(fullOldPath);

                        //save the  new file
                        viewModel.file.CopyTo(new FileStream(fullPath, FileMode.Create));

                    }
                    var author = authorRepository.findOne(viewModel.AuthorId);
                    Book book = new Book
                    {
                        id = viewModel.BookId,
                        title = viewModel.Title,
                        descreption = viewModel.Description,
                        author = author,
                        imgUrl = fileName,

                    };

                    bookRepository.update(viewModel.BookId, book);
                    return RedirectToAction(nameof(Index));


                   }
                else { return RedirectToAction(nameof(Index)); }
             
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.findOne(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillSelectList()
        {
            var authors = authorRepository.list().ToList();
            authors.Insert(0, new Author { id = -1, fullName = "--- Please select an author ---" });

            return authors;
        }

        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };

            return vmodel;
        }
    }
}
