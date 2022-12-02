namespace Persistence.Dto;

public class ToBeMatchedDto
{
    public int UserId { get; set; }
    public int MatchId { get; set; }
    public bool WantsToMatch { get; set; }
    public ToBeMatchedDto()
    {
        
    }
}