﻿namespace Persistence.Dto;

public class IdsForMatchDto
{
    public int SustituteId { get; set; }
    public int EmployerId { get; set; }
    public int GigId { get; set; }
    public bool WasAMatch { get; set; }

    public IdsForMatchDto()
    {
    }
}