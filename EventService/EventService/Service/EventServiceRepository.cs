using System.Collections.Generic;
using EventService.Models;
using EventService.Service.Interfaces;
using RabbitCommunications.Models;

namespace EventService.Service
{
    public class EventServiceRepository : IEventServiceRepository
    {
        public Response<List<Event>> GetWeekEvents(string date)
        {
            var weekEvents = new List<Event>();
            for (int i = 0; i < 7; i++)
            {
                weekEvents.Add(CreateMockEvent());
            }

            var response = new Response<List<Event>>
            {
                Data = weekEvents,
                Succes = true
            };
            return response;
        }

        public Response<Event> SaveEvent(Event Event)
        {
            throw new System.NotImplementedException();
        }

        public Response<Event> CreateEvent(Event Event)
        {
            throw new System.NotImplementedException();
        }

        public Response<Event> DeleteEvent(Event Event)
        {
            throw new System.NotImplementedException();
        }

        private Event CreateMockEvent()
        {
            return new Event
            {
                CollaId = 1,
                Address = "https://www.google.es/maps/place/Pla%C3%A7a+dOctavi%C3%A0,+08172+Sant+Cugat+del+Vall%C3%A8s,+Barcelona/data=!4m2!3m1!1s0x12a496c2dcbe74a7:0x4c5ea5ef7e711774?sa=X&ved=0ahUKEwipqsLk2LHXAhUJvRQKHcxeB2kQ8gEIezAO",
                Date = "12/11/2017",
                Title = "Assaig bla",
                Description = "bla bla bla",
                InitialHour = "20:00",
                EndHour = "22:00",
                Day = 5
            };
        }
    }
}