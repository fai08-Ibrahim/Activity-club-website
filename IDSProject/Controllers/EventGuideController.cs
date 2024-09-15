using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IDSProject.Models;
using IDSProject.services.Services;
using IDSProject.core.Dtos;
using IDSProject.core.Services;
using AutoMapper;

namespace IDSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventGuideController : ControllerBase
    {
        private readonly IEventGuideService _eventGuideService;
        private readonly IMapper _mapper;

        public EventGuideController(IEventGuideService eventGuideService, IMapper mapper)
        {
            _eventGuideService = eventGuideService;
            _mapper = mapper;
        }

        // POST: api/EventGuide/AddEventGuide
        [HttpPost("AddEventGuide")]
        public async Task<ActionResult> AddEventGuide(EventGuideDto eventGuideDto)
        {
            try
            {
                var eventGuide = _mapper.Map<EventGuide>(eventGuideDto);
                await _eventGuideService.AddEventGuideAsync(eventGuide);
                return Ok($"Guide ID: {eventGuide.GuideId} successfully registered for Event ID: {eventGuide.EventId}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/EventGuide/GetAllByGuideId/{guideId}
        [HttpGet("GetAllByGuideId/{guideId}")]
        public async Task<ActionResult<IEnumerable<EventGuideDto>>> GetAllByGuideId(int guideId)
        {
            var eventGuides = await _eventGuideService.GetAllByGuideIdAsync(guideId);
            if (eventGuides == null || !eventGuides.Any())
            {
                return NotFound($"No events found for Guide with ID: {guideId}");
            }
            var eventGuideDtos = _mapper.Map<IEnumerable<EventGuideDto>>(eventGuides);
            return Ok(eventGuideDtos);
        }

        // GET: api/EventGuide/GetAllByEventId/{eventId}
        [HttpGet("GetAllByEventId/{eventId}")]
        public async Task<ActionResult<IEnumerable<EventGuideDto>>> GetAllByEventId(int eventId)
        {
            var eventGuides = await _eventGuideService.GetAllByEventIdAsync(eventId);
            if (eventGuides == null || !eventGuides.Any())
            {
                return NotFound($"No guides found for Event with ID: {eventId}");
            }
            var eventGuideDtos = _mapper.Map<IEnumerable<EventGuideDto>>(eventGuides);
            return Ok(eventGuideDtos);
        }

        // DELETE: api/EventGuide/DeleteByGuideIdAndEventName/{guideId}/{eventName}
        [HttpDelete("DeleteByGuideIdAndEventName/{guideId}/{eventName}")]
        public async Task<ActionResult> DeleteByGuideIdAndEventName(int guideId, string eventName)
        {
            var result = await _eventGuideService.DeleteByGuideIdAndEventNameAsync(guideId, eventName);
            if (result == null)
            {
                return NotFound($"No registration found for Guide ID: {guideId} in Event: {eventName}");
            }
            return Ok($"Guide ID: {guideId} was successfully removed from Event: {eventName}");
        }

        // DELETE: api/EventGuide/DeleteAllByEventName/{eventName}
        [HttpDelete("DeleteAllByEventName/{eventName}")]
        public async Task<ActionResult<IEnumerable<EventGuide>>> DeleteAllByEventName(string eventName)
        {
            var result = await _eventGuideService.DeleteAllByEventNameAsync(eventName);
            if (result == null)
            {
                return NotFound($"No registrations found for Event: {eventName}");
            }
            return Ok($"All registrations for Event: {eventName} were successfully deleted.");
        }
    }
}
