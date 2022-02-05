using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using BooksApp.Models;

namespace BooksApp.Controllers
{
    [Route("api/[controller]")] // api/books
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> Get() //returns all books
        {
            try
            {
                return Ok(StaticDb.books);
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        //======

        [HttpGet("queryIndex")] //api/books/queryIndex?index=1
        public ActionResult<Book> GetByIndex(int index) 
        {
            try
            {
                if (index <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad request, the index can not be negative value or zero!");
                }
                if (index > StaticDb.books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Resource with index {index} does not exist!");
                }
                return Ok(StaticDb.books[index - 1]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        //=====

        //Add GET method that returns one book by filtering by author and title 

        [HttpGet("filter")]   //api/books/filter?author=author1&title=title1
        public ActionResult<List<Book>> FilterBooksFromQuery(string author, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "You have to send at least one filter parameter!");
                }
                if (string.IsNullOrEmpty(author))
                {
                    List<Book> booksDb = StaticDb.books.Where(x => x.Title.ToLower() == title.ToLower()).ToList();

                    if (booksDb.Count != 0)
                    {
                        return Ok(booksDb);
                    }

                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "That book doesn't exist");
                    }
                }
                if (string.IsNullOrEmpty(title))
                {
                    List<Book> booksDb = StaticDb.books.Where(x => x.Author.ToLower() == author.ToLower()).ToList();
                    return Ok(booksDb);
                }
                List<Book> booksDbb = StaticDb.books.Where(x => x.Title.ToLower() == title.ToLower() && x.Author.ToLower() == author.ToLower()).ToList();

                if (booksDbb.Count != 0)
                {
                    return Ok(booksDbb);
                }

                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "The filter you entered doesn't exist");
                }


                return Ok(booksDbb);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

    }
}
