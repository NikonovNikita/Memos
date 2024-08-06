namespace Memos.Contracts;

public record MemoDto(Guid Id, string Title, string Description, DateTime CreatedAt);

