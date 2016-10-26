using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using DOL.WHD.Section14c.Domain.Models.Submission;
using DOL.WHD.Section14c.Domain.Models.Submission.Dto;

namespace DOL.WHD.Section14c.Api
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
                
                // many to many relationships
                cfg.CreateMap<ApplicationSubmissionDto, ApplicationSubmission>()
                    .AfterMap(
                        (src, dest) =>
                            dest.EstablishmentType =
                                src.EstablishmentTypeId.Select(
                                    x => new ApplicationSubmissionEstablishmentType {EstablishmentTypeId = x, ApplicationSubmissionId = dest.Id}).ToList())
                    .AfterMap(
                        (src, dest) =>
                            dest.ProvidingFacilitiesDeductionType =
                                src.ProvidingFacilitiesDeductionTypeId.Select(
                                    x => new ApplicationSubmissionProvidingFacilitiesDeductionType { ProvidingFacilitiesDeductionTypeId = x, ApplicationSubmissionId = dest.Id }).ToList());

                cfg.CreateMap<WorkSiteDto, WorkSite>()
                    .AfterMap(
                        (src, dest) =>
                            dest.WorkSiteType =
                                src.WorkSiteTypeId.Select(
                                    x => new WorkSiteWorkSiteType() { WorkSiteTypeId = x, WorkSiteId = dest.Id }).ToList());
            });
        }
    }
}