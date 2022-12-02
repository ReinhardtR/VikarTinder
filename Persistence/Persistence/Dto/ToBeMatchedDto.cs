namespace Persistence.Dto;

public class ToBeMatchedDto
{
    public int UserId { get; init; }
    public int MatchId { get; init; }
    public bool WantsToMatch { get; init; }
    
    public ToBeMatchedDto()
    {
        
    }
}