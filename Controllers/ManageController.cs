using Microsoft.AspNetCore.Mvc;
using Aurible.Models;
using Aurible.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

[ApiController]
[AuthorizeForScopes(Scopes = new[] { "profile","email"})]
[Authorize(Policy = "RequireAdminPolicy")]
[Route("[Controller]")]
public class ManageController : ControllerBase
{
    private readonly IManageService _manageService;

    public ManageController(IManageService manageService)
    {
        _manageService = manageService;
    }

    // GET /manage/{id}
    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        var book = _manageService.GetBookById(id);
        if (book == null)
            return NotFound(); // Renvoie une 404 si le livre n'existe pas
        return Ok(book); // Renvoie une 200 avec le livre si trouvé
    }

    // POST /manage
    [HttpPost]
    public IActionResult AddBook(BookDto bookDto)
    {
        Book result = _manageService.AddBook(bookDto);
        return CreatedAtAction(nameof(GetBookById), new { id = result.idBook }, result);
    }
    [HttpPost("upload/{id}")]
    public IActionResult UploadBook(IFormFile file, int id){
        var formatOK = file.FileName.EndsWith(".pdf");
        if(!formatOK) return BadRequest(new { message = "Format not supported" });
        if(_manageService.UploadBook(file,id)){
            return Ok(new { message = "Upload success" });
        }
        return NotFound();
    }

    [HttpPatch("{id}")]
    public IActionResult Update(int id, [FromBody] PatchBookDto patchBookDto)
    {
        if (patchBookDto == null)
        {
            return BadRequest();
        }

        var existingBook = _manageService.GetBookById(id);
        if (existingBook == null)
        {
            return NotFound();
        }

        // Fais en sorte que si y'a pas un champs de modifier ça ne fait pas d'erreurs
        if (!string.IsNullOrEmpty(patchBookDto.title))
            existingBook.title = patchBookDto.title;
        if (!string.IsNullOrEmpty(patchBookDto.resume))
            existingBook.resume = patchBookDto.resume;
        if (!string.IsNullOrEmpty(patchBookDto.coverURL))
            existingBook.coverURL = patchBookDto.coverURL;
        if (!string.IsNullOrEmpty(patchBookDto.audioPath))
            existingBook.audioPath = patchBookDto.audioPath;
        if (patchBookDto.maxPage.HasValue)
            existingBook.maxPage = patchBookDto.maxPage.Value;
        if (!string.IsNullOrEmpty(patchBookDto.author))
            existingBook.author = patchBookDto.author;

        // Sauvegarde les modifications
        _manageService.UpdateBook(existingBook); // Cette méthode met à jour dans la base de données

        return NoContent(); // Retourne 204 No Content si la mise à jour est réussie
    }


    // DELETE /manage/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        _manageService.DeleteBook(id);
        return NoContent();
    }
}
