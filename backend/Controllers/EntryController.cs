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
    public async Task<IActionResult> Update(string entryId, double quantity)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _entryApplicationService.UpdateAsync(entryId, userId, quantity);
        return Ok();
    }

    [HttpDelete("/delete")]
    public async Task<IActionResult> Delete(string entryId)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _entryApplicationService.DeleteAsync(entryId, userId);
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
    public async Task<IActionResult> GetHistory()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var history = await _entryApplicationService.GetHistoryAsync(userId);
        return Ok(history);
    }

    [HttpGet("/all")]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var entries = await _entryApplicationService.GetAllAsync(userId);
        return Ok(entries);
    }

    [HttpGet("/all-facts")]
    public async Task<IActionResult> GetTodayFacts()
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var facts = await _entryApplicationService.GetTodayFactsAsync(userId);
        return Ok(facts);
    }

}