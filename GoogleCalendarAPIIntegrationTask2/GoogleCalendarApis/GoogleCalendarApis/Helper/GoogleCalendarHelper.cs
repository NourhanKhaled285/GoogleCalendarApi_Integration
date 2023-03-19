using Google.Apis.Calendar.v3.Data;
using GoogleCalendarApis.Helper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using static Google.Apis.Requests.BatchRequest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GoogleCalendarApis.Models
{
    public static class GoogleCalendarHelper
    {
        public static async Task<Event> CreateGoogleCalendarEvent(GoogleCalendarEvent request)
        {

            var services = GoogleCredintial.CreateCredintial();

            Event eventCalendar = new Event()
            {

                Summary = request.Summary,
                Location = request.Location,
                Start = new EventDateTime
                {
                 
                    DateTime = request.Start,
                    TimeZone = "GMT+02"

                },



                End = new EventDateTime
                {
             
                    DateTime = request.End,
                    TimeZone = "GMT+02"
                },
                Description = request.Description
            };
            String calendarId = "primary";
            var eventRequest = services.Events.Insert(eventCalendar, calendarId);
            var requestCreated = await eventRequest.ExecuteAsync();
            var LastEvent = await GetLastGoogleCalendarEvent();
            NotificationsHelper.SendEmail(LastEvent.Organizer.Email,LastEvent.Summary);
            return requestCreated;

        }

        public static async Task<Event> UpdateGoogleCalendarEvent(GoogleCalendarEvent request, Event _event, string eventId)
        {

            var services = GoogleCredintial.CreateCredintial();
           
            _event.Summary = request?.Summary?? _event.Summary;
            _event.Location = request?.Location?? _event.Location;
            _event.Start.DateTime = request?.Start?? _event.Start.DateTime;
            _event.End.DateTime = request?.End?? _event.End.DateTime;
            _event.Description = request?.Description?? _event.Description;
            var eventRequest = services.Events.Update(_event, "primary", eventId);
            var requestUpdated = await eventRequest.ExecuteAsync();
            return requestUpdated;


        }

        public static async Task<Event> GetGoogleCalendarEvent(string eventId)
        {
            var services = GoogleCredintial.CreateCredintial();
            var events= await GetGoogleCalendarEvents();
            var _event = events.Where(e => e.Id == eventId).FirstOrDefault();
            return _event;
        }

        public static async Task<List<Event>> SearchGoogleCalendarEvent(string summary, DateTime? date)
        {
            var services = GoogleCredintial.CreateCredintial();

            var events = await services.Events.List("primary").ExecuteAsync();
            var selectedEvents = events.Items.ToList();
            if (summary != null && date != null)
                selectedEvents = events.Items.Where(e => e.Summary?.Equals(summary) ?? false || (e.Start?.DateTime?.Equals(date) ?? false)).ToList();//e.Start.DateTime.Equals(date)
            else if (summary != null)
                selectedEvents = events.Items.Where(e => e.Summary?.Equals(summary) ?? false).ToList();
            else if (date != null)
                selectedEvents = events.Items.Where(e=>(e.Start?.DateTime?.Equals(date)) ?? false).ToList();

            return selectedEvents;
        }

        public static async Task<List<Event>> GetGoogleCalendarEvents()
        {
            var services = GoogleCredintial.CreateCredintial();

            var events = await services.Events.List("primary").ExecuteAsync();
            var selectedEvents = events.Items.ToList();
            return selectedEvents;
        }

        public static async Task<Event> GetLastGoogleCalendarEvent()
        {
            var services = GoogleCredintial.CreateCredintial();

            var events = await services.Events.List("primary").ExecuteAsync();
            var lastEventCreated = events.Items.ToList().LastOrDefault();
            return lastEventCreated;
        }


        public static async Task<string> DeleteGoogleCalendarEvent(string summary)
        {
           
            var services = GoogleCredintial.CreateCredintial();
            var events = await GetGoogleCalendarEvents();
            string response;
            try
            {
                var eventId = events.Where(e => e.Summary.Equals(summary))?.FirstOrDefault()?.Id;

                response = await services.Events.Delete("primary", eventId).ExecuteAsync();
                return response;
            }

            catch (Exception ex)
            {

                response = "This event already deleted or doesnot exist";
                return response;
            } 
         
        }



    }
}
