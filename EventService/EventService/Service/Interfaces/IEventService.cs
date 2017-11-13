using System.Collections.Generic;
using EventService.Models;
using RabbitCommunications.Models;

namespace EventService.Service.Interfaces
{
    public interface IEventService
    {
        Response<List<Event>> GetWeekEvents(string jsonDate);
        Response<Event> SaveEvent(Event jsonEvent);
        Response<Event> CreateEvent(Event jsonEvent);
        Response<Event> DeleteEvent(Event jsonEvent);
    }
}