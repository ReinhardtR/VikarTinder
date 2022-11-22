package com.example.businessserver.dtos;

import java.util.List;

public class SubstituteDatesDTO {
    private List<SubstituteDatingDTO> dates;

    public SubstituteDatesDTO() {
    }

    public SubstituteDatesDTO(List<SubstituteDatingDTO> dates) {
        this.dates = dates;
    }

    public List<SubstituteDatingDTO> getDates() {
        return dates;
    }
}
