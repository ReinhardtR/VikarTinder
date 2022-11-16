using Microsoft.AspNetCore.Mvc;

namespace Persistence.Logic.Interfaces;

public interface IMatchAdapter
{
    Task<Task> MatchForSub(SendSubAndWorkp request);
}