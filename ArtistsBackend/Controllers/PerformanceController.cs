using ArtistsBackend.Entities;
using ArtistsBackend.Models;
using ArtistsBackend.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private IArtistsRepository _repository;
        private IMapper _mapper;
        public PerformanceController(IArtistsRepository repository)
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

        [HttpGet("{eventId}/performances")]
        public IActionResult GetPerformances(int eventId)
        {
            if (!_repository.EventExists(eventId))
            {
                return NotFound();
            }

            var performancesForEvent = _repository.GetPerformancesForEvent(eventId);
            var performancesForEventResults = _mapper.Map<IEnumerable<PerformanceDto>>(performancesForEvent);

            return Ok(performancesForEventResults);
        }

        [HttpGet("{eventId}/performances/{id}", Name = "GetPerformance")]
        public IActionResult GetPointOfInterest(int eventId, int id)
        {
            if (!_repository.EventExists(eventId))
            {
                return NotFound();
            }

            var performance = _repository.GetPerformanceForEvent(eventId, id);
            if (performance == null)
            {
                return NotFound();
            }

            var performanceResult = _mapper.Map<PerformanceDto>(performance);

            return Ok(performanceResult);
        }

        [HttpPost("{eventId}/performances")]
        public IActionResult CreatePerformance(int eventId,
            [FromBody] PerformanceForCreationDto performanceDto)
        {
            if (performanceDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.EventExists(eventId))
            {
                return NotFound();
            }

            var finalPerformance = _mapper.Map<Performance>(performanceDto);

            _repository.AddPerformanceForEvent(eventId, finalPerformance);

            if (!_repository.Save())
            {
                return StatusCode(500, "An error occurred while handling your request.");
            }

            var createdPerformance = _mapper.Map<PerformanceDto>(finalPerformance);

            return CreatedAtRoute("GetPerformance",
                new { eventId = eventId, id = finalPerformance.Id }, createdPerformance);
        }

        [HttpPatch("{eventId}/performances/{id}")]
        public IActionResult PartiallyUpdatePointofInterest(int eventId, int id,
            [FromBody] JsonPatchDocument<PerformanceForCreationDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_repository.EventExists(eventId))
            {
                return NotFound();
            }

            var performanceEntity = _repository.GetPerformanceForEvent(eventId, id);

            if (performanceEntity == null)
            {
                return NotFound();
            }

            var performanceToPatch = _mapper.Map<PerformanceForCreationDto>(performanceEntity);

            patchDoc.ApplyTo(performanceToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _mapper.Map(performanceToPatch, performanceEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem has occurred while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{eventId}/performances/{id}")]
        public IActionResult DeletePerformances(int eventId, int id)
        {
            if (!_repository.EventExists(eventId))
            {
                return NotFound();
            }

            var performanceEntity = _repository.GetPerformanceForEvent(eventId, id);

            if (performanceEntity == null)
            {
                return NotFound();
            }

            _repository.DeletePerformance(performanceEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "A problem has occurred while handling your request.");
            }

            return NoContent();
        }
    }
}
