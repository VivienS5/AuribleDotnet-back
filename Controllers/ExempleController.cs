using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuribleDotnet_back.Controllers;

[ApiController]
[Route("exemple")]
public class ExempleController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ExempleController> _logger;

    public ExempleController(ILogger<ExempleController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetExemple")]
    public IEnumerable<Exemple> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Exemple
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
