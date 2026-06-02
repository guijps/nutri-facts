using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    private string? GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    [HttpPost("/update")]
    public IActionResult Update(string entryId, double quantity)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        _entryApplicationService.Update(entryId, userId, quantity);
        return Ok();
    }

    [HttpDelete("/delete")]
    public IActionResult Delete(string entryId)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        _entryApplicationService.Delete(entryId, userId);
        return Ok();
    }


    [HttpPost("/set")]
    public async Task<IActionResult> Set(string code, double quantity)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _entryApplicationService.AddAsync(code, userId, quantity);
        return Ok();
    }

    [HttpGet("/history")]
    public IActionResult GetHistory()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var history = _entryApplicationService.GetHistory(userId);
        return Ok(history);
    }

    [HttpGet("/all")]
    public IActionResult GetAll()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var entries = _entryApplicationService.GetAll(userId);
        return Ok(entries);
    }

    [HttpGet("/all-facts")]
    public IActionResult GetTodayFacts()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var facts = _entryApplicationService.GetTodayFacts(userId);
        return Ok(facts);
    }

}