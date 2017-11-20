using System;
using System.Collections.Generic;
using EventService.Models;
using RabbitCommunications.Models;

namespace EventService.Service.Interfaces
{
    public interface IEventService
    {
        Response<List<Event>> GetWeekEvents(DateTime date);
        Response<Event> SaveEvent(Event eventData);
        Response<Event> CreateEvent(Event eventData);
        Response<Event> DeleteEvent(Event eventData);
    }
}