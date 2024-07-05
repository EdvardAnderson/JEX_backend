using AutoMapper;

namespace JEX_backend.API;

public class JobBoardProfiles : Profile
{
    public JobBoardProfiles()
    {
        CreateMap<Entities.Company, Models.CompanyWithoutJobOpeningsDto>();
        CreateMap<Entities.Company, Models.CompanyDto>();
        CreateMap<Entities.JobOpening, Models.JobOpeningDto>();
        CreateMap<Entities.JobOpening, Models.JobOpeningForCreationDto>();

        // for creation API calls
        CreateMap<Models.JobOpeningForCreationDto, Entities.JobOpening>();
        CreateMap<Models.CompanyForCreationDto, Entities.Company>();
    }
}
