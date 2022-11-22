package com.example.businessserver.dtos;

import java.util.List;

public class WorkPositionDatesDTO {
    private List<WorkPositionDatingDTO> dates;

    public WorkPositionDatesDTO() {
    }

    public WorkPositionDatesDTO(List<WorkPositionDatingDTO> dates) {
        this.dates = dates;
    }

    public List<WorkPositionDatingDTO> getDates() {
        return dates;
    }
}
