using Memos.Contracts;
using Memos.DataAccess;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Memos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Memos.Controllers;

[ApiController]
[Route("/home")]
public class MemosController : ControllerBase
{
    private readonly MemosDbContex _db;

    public MemosController(MemosDbContex db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMemoRequest req, CancellationToken ct)
    {
        var memo = new Memo(req.Title, req.Description);

        await _db.Memos.AddAsync(memo, ct);
        await _db.SaveChangesAsync(ct);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetMemoRequest req, CancellationToken ct)
    {
        var memosQuery = _db.Memos.Where(m => string.IsNullOrWhiteSpace(req.Search) ||
                                                                                                m.Title.ToLower().Contains(req.Search.ToLower()));

        Expression<Func<Memo, object>> selectorKey = req.SortItem?.ToLower() switch
        {
            "date" => m => m.CreatedAt,
            "title" => m => m.Title,
            _ => m => m.Id
        };

        memosQuery = req.SortOrder == "desc" ? memosQuery.OrderByDescending(selectorKey) : memosQuery.OrderBy(selectorKey);

        var memoDtos = await memosQuery.Select(m => new MemoDto(m.Id, m.Title, m.Description, m.CreatedAt)).ToListAsync(ct);

        return Ok(new GetMemosResponse(memoDtos));
    }
}
