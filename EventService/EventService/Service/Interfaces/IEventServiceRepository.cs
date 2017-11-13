using System.Collections.Generic;
using EventService.Models;
using RabbitCommunications.Models;

namespace EventService.Service.Interfaces
{
    public interface IEventServiceRepository
    {
        Response<List<Event>> GetWeekEvents(string date);
        Response<Event> SaveEvent(Event Event);
        Response<Event> CreateEvent(Event Event);
        Response<Event> DeleteEvent(Event Event);
    }
}