using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtistsBackend.Entities;
using ArtistsBackend.Models;
using ArtistsBackend.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ArtistsBackend.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IArtistsRepository _repository;
        private IMapper _mapper;
        public EventController(IArtistsRepository repository)
        {
            _repository = repository;
            var config = new MapperConfiguration(cfg =>
            {
                Startup.MapperInit()(cfg);
                cfg.CreateMap<Entities.Part, Models.PartDto>().
                ForMember(dest => dest.User,
                    opts => opts.MapFrom(src => _repository.GetUser(src.UserId)));
            });
            _mapper = config.CreateMapper();
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            var eventEntities = _repository.GetEventsWithoutPerformances();

            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(eventEntities);

            return Ok(eventDtos);
        }

        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult GetEvent(int id)
        {
            var ev = _repository.GetEvent(id);
            if (ev == null)
            {
                return NotFound();
            }

            var eventDto = _mapper.Map<EventDto>(ev);
            return Ok(eventDto);
        }

        [HttpPost]
        public IActionResult AddEvent([FromBody] EventForUpdateDto eventForCreation)
        {
            if(eventForCreation == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var finalEvent = _mapper.Map<Event>(eventForCreation);
            _repository.AddEvent(finalEvent);

            if (!_repository.Save())
            {
                return StatusCode(500, "An error occurred while handling your request.");
            }

            var createdEvent = _mapper.Map<EventDto>(finalEvent);

            return CreatedAtRoute("GetEvent",
                new {id = finalEvent.Id }, createdEvent);
        }

        [HttpPatch("{eventId}")]
        public IActionResult PartiallyUpdateEvent(int eventId, [FromBody] JsonPatchDocument<EventForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            if (!_repository.EventExists(eventId))
            {
                return NotFound();
            }
            Event eventEntity = _repository.GetEvent(eventId);
            EventForUpdateDto eventToPatch = _mapper.Map<EventForUpdateDto>(eventEntity);
            patchDoc.ApplyTo(eventToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(eventToPatch, eventEntity);
            if (!_repository.Save())
            {
                return StatusCode(500, "A problem has occurred while handling your request.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            if (!_repository.EventExists(id))
            {
                return NotFound();
            }

            var eventEntity = _repository.GetEvent(id);
            _repository.DeleteEvent(eventEntity);
            if (!_repository.Save())
            {
                return StatusCode(500, "A problem has occurred while handling your request.");
            }
            return NoContent();
        }
    }
}