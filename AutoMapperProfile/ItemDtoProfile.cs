using System.Collections.Generic;
using AutoMapper;
using Models;

namespace Tickets.Services.AutoMapperProfiles
{
    public class ItemDtoProfile:Profile
    {
        public ItemDtoProfile()
        {
            CreateMap<PlaneTicket, Item>();
            CreateMap<PlaneTicket, Item>()
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.passenger.name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.passenger.name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.passenger.surname))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.passenger.patronymic))
                .ForMember(dest => dest.DocType, opt => opt.MapFrom(src => src.passenger.doc_type))
                .ForMember(dest => dest.DocNumber, opt => opt.MapFrom(src => src.passenger.doc_number))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.passenger.birthdate))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.passenger.gender))
                .ForMember(dest => dest.PassengerType, opt => opt.MapFrom(src => src.passenger.passenger_type))
                .ForMember(dest => dest.TicketNumber, opt => opt.MapFrom(src => src.passenger.ticket_number))
                .ForMember(dest => dest.TicketType, opt => opt.MapFrom(src => src.passenger.ticket_type));
            CreateMap<Models.Route, Item>()
                .ForMember(p => p.AirlineCode, s => s.MapFrom(s => s.airline_code))
                .ForMember(p => p.FlightNum, s => s.MapFrom(s => s.flight_num))
                .ForMember(p => p.DepartPlace, s => s.MapFrom(s => s.depart_place))
                .ForMember(p => p.DepartDatetime, s => s.MapFrom(s => s.depart_datetime))
                .ForMember(p => p.ArrivePlace, s => s.MapFrom(s => s.arrive_place))
                .ForMember(p => p.ArriveDatetime, s => s.MapFrom(s => s.arrive_datetime))
                .ForMember(p => p.PnrId, s => s.MapFrom(s => s.pnr_id));
        }
    }
}