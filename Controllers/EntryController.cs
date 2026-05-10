using Microsoft.AspNetCore.Mvc;

namespace NutriFacts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EntryController : ControllerBase
{
    private readonly EntryApplicationService _entryApplicationService;

    public EntryController(EntryApplicationService entryApplicationService)
    {
        _entryApplicationService = entryApplicationService;
    }

    [HttpPost("/set")]
    public IActionResult SetEntry(string code, double quantity)
    {
        _entryApplicationService.AddEntry(code, quantity);
        return Ok();
    }
    [HttpGet("/all")]
    public IActionResult GetAllEntries()
    {
        var entries = _entryApplicationService.GetAllEntries();
        return Ok(entries);
    }

    [HttpGet("/all-facts")]
    public IActionResult GetTodayFacts()
    {
        var facts = _entryApplicationService.GetTodayFacts();
        return Ok(facts);
    }

}