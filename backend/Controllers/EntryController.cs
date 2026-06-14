using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NutriFacts.Controllers;
[Authorize]
[ApiController]
[Route("api/entry")]
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

    [HttpPut("{entryId:guid}")]
    public async Task<IActionResult> Update(Guid entryId, [FromBody] UpdateEntry updateEntry)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _entryApplicationService.UpdateAsync(entryId, userId, updateEntry.Quantity);
        return Ok();
    }

    [HttpDelete("{entryId:guid}")]
    public async Task<IActionResult> Delete(Guid entryId)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _entryApplicationService.DeleteAsync(entryId, userId);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEntry createEntry)
    {
        var userId = GetUserId();
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _entryApplicationService.AddAsync(createEntry.ProductId, userId, createEntry.Quantity);
        return Ok();
    }

    [HttpGet]
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

    [HttpGet("history")]
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

    [HttpGet("today-facts")]
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