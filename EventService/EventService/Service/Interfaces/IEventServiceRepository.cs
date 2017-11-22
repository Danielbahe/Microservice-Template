﻿using System;
using System.Collections.Generic;
using EventService.Models;
using RabbitCommunications.Models;

namespace EventService.Service.Interfaces
{
    public interface IEventServiceRepository
    {
        Response<List<Event>> GetWeekEvents(DateTime date);
        Response<Event> SaveEvent(Event Event);
        Response<Event> CreateEvent(Event Event);
        Response<Event> DeleteEvent(Event Event);
        Response<Event> AsistToEvent(Event eventData);
        Response<Event> CancelAsistToEvent(Event eventData);
        Response<Event> AsistBusToEvent(Event eventData);
        Response<Event> CancelAsistBusToEvent(Event eventData);
        Response<Event> AsistDietToEvent(Event eventData);
        Response<Event> CancelAsistDietToEvent(Event eventData);
        Response<Event> AsistVeganToEvent(Event eventData);
        Response<Event> CancelAsistVeganToEvent(Event Event);
        Response<Event> GetEventAssistance(Event Event);
        Response<Event> UpdatePersonsDatabase(Person person);
        Response<Event> InsertPersonsDatabase(Person person);
        Response<Event> DeletePerson(Person person);
    }
}