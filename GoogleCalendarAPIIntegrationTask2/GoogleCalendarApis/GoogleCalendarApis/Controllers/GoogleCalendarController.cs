using AutoMapper;
using GoogleCalendarApis.DTOS;
using GoogleCalendarApis.Errors;
using GoogleCalendarApis.Helper;
using GoogleCalendarApis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using Google.Apis.Calendar.v3.Data;
using System.Threading.Tasks;
using System.Linq;

namespace GoogleCalendarApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleCalendarController : ControllerBase
    {
        private readonly IMapper _mapper;
        public GoogleCalendarController(IMapper mapper)
        {
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<ActionResult<EventToDto>> CreateGoogleCalender(GoogleCalendarEvent request)
        {
            return Ok(_mapper.Map<Event, EventToDto>(await GoogleCalendarHelper.CreateGoogleCalendarEvent(request)));
        }


        [HttpPut]
        public async Task<ActionResult<EventToDto>> UpdateGoogleCalender(GoogleCalendarEvent request, string eventId)
        {
            var _event = await GoogleCalendarHelper.GetGoogleCalendarEvent(eventId);
            if (_event == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Event, EventToDto>(await GoogleCalendarHelper.UpdateGoogleCalendarEvent(request, _event, eventId)));
        }


        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventToDto>> GetGoogleCalender(string eventId)
        {
            var _event = await GoogleCalendarHelper.GetGoogleCalendarEvent(eventId);
            if (_event == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Event, EventToDto>(_event));
        }

        [HttpGet("Search")]
        public async Task<ActionResult<IReadOnlyList<EventToDto>>> SearchGoogleCalendar(string summary, DateTime ?date)

        { 
            var _event = await GoogleCalendarHelper.SearchGoogleCalendarEvent(summary,date);
            if (_event == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map <IReadOnlyList<Event>,IReadOnlyList<EventToDto>>(_event));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<EventToDto>>> GetAllGoogleCalendarEvents()

        { 
            var _event = await GoogleCalendarHelper.GetGoogleCalendarEvents();
            if (_event == null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventToDto>>(_event));
        }


        [HttpDelete]
        public async Task<ActionResult<Event>> DeleteGoogleCalendarEvent(string summary)

        {
            string response = await GoogleCalendarHelper.DeleteGoogleCalendarEvent(summary);
            return Ok(response);
        }

    }
}
