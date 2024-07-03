using AutoMapper;

namespace JEX_backend.API;

public class JobBoardProfiles : Profile
{
    public JobBoardProfiles()
    {
        CreateMap<Entities.Company, Models.CompanyWithoutJobOpeningsDto>();
        CreateMap<Entities.Company, Models.CompanyDto>();
        CreateMap<Entities.JobOpening, Models.JobOpeningDto>();
    }
}
