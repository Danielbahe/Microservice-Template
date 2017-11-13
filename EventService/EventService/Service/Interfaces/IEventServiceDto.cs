using System.Collections.Generic;
using EventService.Models;
using RabbitCommunications.Models;

namespace EventService.Service.Interfaces
{
    public interface IEventServiceDto
    {
        Response<List<EventDto>> GetWeekEvents(string jsonDate);
        Response<EventDto> SaveEvent(string jsonEvent);
        Response<EventDto> CreateEvent(string jsonEvent);
        Response<EventDto> DeleteEvent(string jsonEvent);
    }
}