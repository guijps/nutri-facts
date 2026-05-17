using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NutriFacts.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EntryController : ControllerBase
{
    private readonly EntryApplicationService _entryApplicationService;

    public EntryController(EntryApplicationService entryApplicationService)
    {
        _entryApplicationService = entryApplicationService;
    }

    [HttpPost("/update")]
    public IActionResult Update(string entryId, double quantity)
    {
        _entryApplicationService.Update(entryId, quantity);
        return Ok();
    }
    [HttpDelete("/delete")]
    public IActionResult Delete(string entryId)
    {
        _entryApplicationService.Delete(entryId);
        return Ok();
    }


    [HttpPost("/set")]
    public IActionResult Set(string code, double quantity)
    {
        _entryApplicationService.Add(code, quantity);
        return Ok();
    }
    [HttpGet("/all")]
    public IActionResult GetAll()
    {
        var entries = _entryApplicationService.GetAll();
        return Ok(entries);
    }

    [HttpGet("/all-facts")]
    public IActionResult GetTodayFacts()
    {
        var facts = _entryApplicationService.GetTodayFacts();
        return Ok(facts);
    }

}