//using AutoMapper;
using System.Linq;
using AutoMapper;
using Google.Apis.Calendar.v3.Data;
using GoogleCalendarApis.DTOS;
using GoogleCalendarApis.Models;
namespace GoogleCalendarApis.Profiles
{
    public class MapProfile:Profile 
     {
         public MapProfile()
         {
            CreateMap<Event, EventToDto>()
                     .ForMember(ed => ed.Id, e => e.MapFrom(S => S.Id))
                     .ForMember(ed => ed.Summary, e => e.MapFrom(S => S.Summary))
                     .ForMember(ed => ed.Description, e => e.MapFrom(S => S.Description))
                     .ForMember(ed => ed.Start, e => e.MapFrom(S => S.Start.DateTime))
                     .ForMember(ed => ed.End, e => e.MapFrom(S => S.End.DateTime))
                     .ForMember(ed => ed.Location, e => e.MapFrom(S => S.Location));


        }

    }
}
