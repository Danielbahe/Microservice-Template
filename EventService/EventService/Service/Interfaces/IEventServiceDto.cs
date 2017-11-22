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
        Response<EventDto> AsistToEvent(string jsonEvent);
        Response<EventDto> DeleteEvent(string jsonEvent);
        Response<EventDto> AsistBusToEvent(string jsonEvent);
        Response<EventDto> AsistDietToEvent(string jsonEvent);
        Response<EventDto> AsistVeganToEvent(string jsonEvent);
        Response<EventDto> CancelAsistBusToEvent(string eventData);
        Response<EventDto> CancelAsistDietToEvent(string eventData);
        Response<EventDto> CancelAsistVeganToEvent(string Event);
        Response<EventDto> GetEventAssistance(string Event);
        Response<EventDto> UpdatePerson(string person);
        Response<EventDto> AddPerson(string person);
        Response<EventDto> DeletePerson(string person);
    }
}