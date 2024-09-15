using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IDSProject.Models;
using IDSProject.services.Services;
using IDSProject.core.Dtos;
using AutoMapper;

namespace IDSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventMemberController : ControllerBase
    {
        private readonly IEventMemberService _eventMemberService;
        private readonly IMapper _mapper;
        public EventMemberController(IEventMemberService eventMemberService, IMapper mapper)
        {
            _eventMemberService = eventMemberService;
            _mapper = mapper;
        }

        // GET: api/EventMember/GetAllByMemberId/{memberId}
        [HttpGet("GetAllByMemberId/{memberId}")]
        public async Task<ActionResult<IEnumerable<EventMemberDto>>> GetAllByMemberId(int memberId)
        {
            var eventMembers = await _eventMemberService.GetAllByMemberIdAsync(memberId);
            if (eventMembers == null || !eventMembers.Any())
            {
                return NotFound($"No events found for Member with ID: {memberId}");
            }
            var eventMemberDtos = _mapper.Map<IEnumerable<EventMemberDto>>(eventMembers);
            return Ok(eventMemberDtos);
        }

        // GET: api/EventMember/GetAllByEventId/{eventId}
        [HttpGet("GetAllByEventId/{eventId}")]
        public async Task<ActionResult<IEnumerable<EventMemberDto>>> GetAllByEventId(int eventId)
        {
            var eventMembers = await _eventMemberService.GetAllByEventIdAsync(eventId);
            if (eventMembers == null || !eventMembers.Any())
            {
                return NotFound($"No members found for Event with ID: {eventId}");
            }
            var eventMemberDtos = _mapper.Map<IEnumerable<EventMemberDto>>(eventMembers); // Correctly map the collection
            return Ok(eventMemberDtos);
        }

        // DELETE: api/EventMember/DeleteByMemberIdAndEventName/{memberId}/{eventName}
        [HttpDelete("DeleteByMemberIdAndEventName/{memberId}/{eventName}")]
        public async Task<ActionResult> DeleteByMemberIdAndEventName(int memberId, string eventName, IEventMemberService _eventMemberService)
        {
            var result = await _eventMemberService.DeleteByMemberIdAndEventNameAsync(memberId, eventName);
            if (result == null)
            {
                return NotFound($"No registration found for Member ID: {memberId} in Event: {eventName}");
            }
            return Ok($"Member ID: {memberId} was successfully removed from Event: {eventName}");
        }

        // DELETE: api/EventMember/DeleteAllByEventName/{eventName}
        [HttpDelete("DeleteAllByEventName/{eventName}")]
        public async Task<ActionResult<IEnumerable<EventMember>>> DeleteAllByEventName(string eventName)
        {
            var result = await _eventMemberService.DeleteAllByEventNameAsync(eventName);
            if (result == null)
            {
                return NotFound($"No registrations found for Event: {eventName}");
            }
            return Ok($"All registrations for Event: {eventName} were successfully deleted.");
        }

        // POST: api/EventMember/AddEventMember
        [HttpPost("AddEventMember")]
        public async Task<ActionResult> AddEventMember(EventMemberDto eventMemberDto)
        {
            try
            {
                var eventMember = _mapper.Map<EventMember>(eventMemberDto);
                await _eventMemberService.AddEventMemberAsync(eventMember);
                return Ok($"Member ID: {eventMemberDto.MemberId} successfully registered for Event ID: {eventMemberDto.EventId}");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
