namespace Memos.Contracts;

public record GetMemoRequest(string? Search, string? SortItem, string? SortOrder);