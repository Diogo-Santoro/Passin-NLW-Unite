﻿using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCase
    {
        public ResponseRegisterEventsJson Execute(RequestEventJson request)
        {
            Validate(request);

            var dbContext = new PassinDbContext();


            var entity = new Infrastructure.Entities.Event
            {
                Title = request.Title,
                Details = request.Details,
                Maximum_Attendees = request.MaximumAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-"),
            };

            dbContext.Events.Add(entity);
            dbContext.SaveChanges();

            return new ResponseRegisterEventsJson
            {
                Id = entity.Id
            };
        }

        private void Validate(RequestEventJson request)
        {
            if(request.MaximumAttendees <= 0)
            {
                throw new PassinException("The Maximum attendes is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new PassinException("The title is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new PassinException("The details are invalid.");
            }
        }
    }
}
