using Microsoft.AspNetCore.Mvc;
using IDSProject.core.Services.IServices;
using IDSProject.Models;
using AutoMapper;
using IDSProject.core.Dtos;


namespace IDSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet("Get All Events")]
        public async Task<ActionResult<IEnumerable<EventDto>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(eventDtos);
        }

        [HttpGet("Get Event by Id")]
        public async Task<ActionResult<EventDto>> GetEventById(int eventId)
        {
            //event is a reserved keyword in C#
            var eventItem = await _eventService.GetEventByIdAsync(eventId);
            if (eventItem == null)
            {
                return NotFound("An Event with this Id may not exist");
            }
            var eventDto = _mapper.Map<EventDto>(eventItem); // Updated reference to the new variable name
            return Ok(eventDto);
        }


        [HttpDelete("Delete Event by Id if exists")]
        public async Task<ActionResult> DeleteEventById(int eventId)
        {
            var eventObj = await _eventService.GetEventByIdAsync(eventId);
            if (eventObj != null)
            {
                await _eventService.DeleteEventByIdAsync(eventId);
                return Ok($"Event with id = {eventId} was deleted successfully.");
            }
            else
            {
                return NotFound($"There is no such event with id = {eventId}");
            }
        }

        [HttpPost("AddOrUpdateEvent")]
        public async Task<ActionResult> AddOrUpdateEvent(EventDto eventDto)
        {
            try
            {
                var eventObj = _mapper.Map<Event>(eventDto);
                if (eventObj.Id == 0)
                {
                    // Add a new event
                    await _eventService.AddOrUpdateEventAsync(eventObj);
                    return Ok("Event was added successfully.");
                }
                else
                {
                    // Check if the event exists
                    var existingEvent = await _eventService.GetEventByIdAsync(eventObj.Id);
                    if (existingEvent == null)
                    {
                        return NotFound("You're trying to update an event that does not exist.");
                    }
                    else
                    {
                        // Update the existing event
                        await _eventService.AddOrUpdateEventAsync(eventObj);
                        return Ok($"Event with ID = {eventObj.Id} was updated successfully.");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
