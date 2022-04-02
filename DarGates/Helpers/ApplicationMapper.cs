using AutoMapper;
using DarGates.DB;
using DarGates.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Gate,GateDTO>().ReverseMap();
            CreateMap<GateLog, ParkedDTO>()
                .ForMember(dest => dest.Car, src => src.MapFrom(p => p.ParkType))
                .ReverseMap();
            CreateMap<GardUser, UserDTO.Response>()
                .ForMember(dest => dest.GateName, src => src.MapFrom(u => u.Gate.Name))
                .ForMember(dest => dest.PrinterMac, src => src.MapFrom(u => u.Gate.PrinterMac));
                //.ForMember(dest => dest.Balance, src => src.MapFrom(u => u.GateLogs
                    //.Where(g => !g.IsPayed && !g.IsDeleted).Sum(g => g.Total) + u.PrinterLogs.Where(p => !p.IsPayed).Sum(p => p.Price)));
            CreateMap<GardUser, DarUserDTO>().ReverseMap();

            CreateMap<GateLog, GateLogDTO>()
                .ForMember(dest => dest.InDate, src => src.MapFrom(g => g.InDate.Value.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.OutDate, src => src.MapFrom(g => g.OutDate.Value.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.RePrintFines, src => src.MapFrom(g => g.PrinterLogs.Sum(p => p.Price)))
                .ReverseMap();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<Owner, ParkTypeCountDTO>()
                .ForMember(dest => dest.ParkType, src => src.MapFrom(o => o.Type))
                .ReverseMap();
            CreateMap<RePrintReason, ReasonDTO>().ReverseMap();
            CreateMap<OfficialHoliday, OfficialHolidayDTO>()
                .ForMember(dest => dest.StartDate, src => src.MapFrom(o => o.StartDate.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(o => o.EndDate.ToString("dd-MM-yyyy")))
                .ReverseMap();
            CreateMap<WeeklyHolidayDTO, WeeklyHoliday>().ReverseMap();
            CreateMap<PrinterLog, PrinterLogDTO>()
                .ForMember(dest => dest.PrintDate, src => src.MapFrom(s => s.PrintDate.Value.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.RePrintReason, src => src.MapFrom(s => s.RePrintReason.Reason))
                .ReverseMap();
            CreateMap<Invitation, InvitationDTO.AddInvitation>().ReverseMap();
            CreateMap<InvitationType, InvitationDTO.InvitationType>().ReverseMap();
            CreateMap<Invitation, GetInvitationDTO>()
                .ForMember(dest => dest.InvitationType, src => src.MapFrom(g => g.InvitationType.Type))
                .ForMember(dest => dest.CreationDate, src => src.MapFrom(g => g.CreationDate.Value.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.InTime, src => src.MapFrom(g => g.GateLog != null ? g.GateLog.InDate.Value.ToString("dd-MM-yyyy") + "/" + g.GateLog.InTime : ""))
                .ForMember(dest => dest.OutTime, src => src.MapFrom(g => g.GateLog != null && g.GateLog.OutDate != null ? g.GateLog.OutDate.Value.ToString("dd-MM-yyyy") + "/" + g.GateLog.OutTime : ""))
                .ForMember(dest => dest.Status, src => src.MapFrom(g => g.CreationDate < DateTime.Today && g.GateLog == null ? 0 : g.GateLog == null ? 1 : 2))
                .ReverseMap();
            CreateMap<InvitationType, ParkTypeCountDTO>()
                .ForMember(dest => dest.ParkType, src => src.MapFrom(i => i.Type))
                .ReverseMap();
        }
    }
}
