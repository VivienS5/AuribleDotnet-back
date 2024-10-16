using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Aurible.Models;
using Aurible.Services;

[ApiController]
[Route("api/[controller]")]
public class ManageController : ControllerBase
{
    private readonly IManageService _manageService;

    public ManageController(IManageService manageService)
    {
        _manageService = manageService;
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = _manageService.GetBookById(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBook(Book book)
    {
        _manageService.AddBook(book);
        return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();
        
        _manageService.UpdateBook(book);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        _manageService.DeleteBook(id);
        return NoContent();
    }
}
