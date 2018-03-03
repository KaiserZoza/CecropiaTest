using AutoMapper;
using Entities;
using System;

namespace CecropiaWebApplication.Models
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<CecropiaMapperProfile>(); });
            Mapper.AssertConfigurationIsValid();
        }

        public class CecropiaMapperProfile : Profile
        {
            public CecropiaMapperProfile()
            {
                CreateMap<Exception, ExceptionRecord>()
                    .ForMember(d => d.ExceptionRecordId, o => o.Ignore())
                    .ForMember(d => d.ExceptionRecordDate, o => o.Ignore())
                    .ForMember(d => d.ExceptionMessage, o => o.MapFrom(x => x.Message))
                    .ForMember(d => d.ExceptionStackTrace, o => o.MapFrom(x => x.StackTrace))
                    .ForMember(d => d.InnerExceptionMessage, o => o.MapFrom(x => x.InnerException.Message))
                    .ForMember(d => d.InnerExceptionStackTrace, o => o.MapFrom(x => x.InnerException.StackTrace))
                    .ForMember(d => d.ExceptionTypeName, o => o.MapFrom(x => x.GetType().FullName))
                    .ForMember(d => d.InnerExceptionSource, o => o.MapFrom(x => x.InnerException.Source));

                CreateMap<PatientViewModel, Patient>()
                    .ForSourceMember(d => d.Countries, o => o.Ignore())
                    .ForSourceMember(d => d.SelectedNationality, o => o.Ignore())
                    .ForSourceMember(d => d.BloodTypes, o => o.Ignore())
                    .ForSourceMember(d => d.SelectedBloodType, o => o.Ignore())
                    .ForMember(s => s.Nationality, o => o.Ignore())
                    .ForMember(s => s.BloodType, o => o.Ignore());

                CreateMap<Patient, PatientViewModel>()
                    .ForMember(s => s.Countries, o => o.Ignore())
                    .ForMember(s => s.SelectedNationality, o => o.MapFrom(s => s.Nationality.Code))
                    .ForMember(s => s.BloodTypes, o => o.Ignore())
                    .ForMember(s => s.SelectedBloodType, o => o.MapFrom(s=>s.BloodType.ID))
                    .ForSourceMember(d => d.Nationality, o => o.Ignore())
                    .ForSourceMember(d => d.BloodType, o => o.Ignore());

                CreateMap<Patient, PatientDetailsViewModel>()
                    .ForMember(s => s.SelectedNationality, o => o.MapFrom(s => s.Nationality.Name))
                    .ForMember(s => s.SelectedBloodType, o => o.MapFrom(s => s.BloodType.Type + s.BloodType.RH))
                    .ForSourceMember(d => d.Nationality, o => o.Ignore())
                    .ForSourceMember(d => d.BloodType, o => o.Ignore());


            }
        }
    }
}