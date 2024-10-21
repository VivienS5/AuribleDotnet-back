using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aurible.Models;
using Aurible.Services;

[ApiController]
[Route("book")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        return Ok(_bookService.GetAllBooks());
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }
}
