using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IDSProject.Models;
using IDSProject.services.Services;
using AutoMapper;
using IDSProject.core.Dtos;

namespace IDSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly IGuideService _guideService;
        private readonly IMapper _mapper;

        public GuideController(IGuideService guideService, IMapper mapper)
        {
            _guideService = guideService;
            _mapper = mapper;
        }

        [HttpGet("Get All Guides")]
        public async Task<ActionResult<IEnumerable<GuideDto>>> GetAllGuides()
        {
            var guides = await _guideService.GetAllGuides();
            var guideDtos = _mapper.Map<IEnumerable<GuideDto>>(guides);
            return Ok(guideDtos);
        }

        [HttpGet("Get Guide by Id")]
        public async Task<ActionResult<GuideDto?>> GetGuideById(int guideId)
        {
            var guide = await _guideService.GetByGuideIdAsync(guideId);
            if (guide == null)
            {
                return NotFound("A Guide with this Id may not exist.");
            }
            else
            {
                var guideDto = _mapper?.Map<GuideDto>(guide);
                return Ok(guideDto);
            }
        }

        [HttpGet("Get Guide by Username (unique property)")]
        public async Task<ActionResult<GuideDto?>> GetGuideByUsername(string username)
        {
            var guide = await _guideService.GetGuideByUsername(username);
            if (guide == null)
            {
                return NotFound($"There does not exist a guide with this username: {username}");
            }
            else
            {
                var guideDto = _mapper.Map<GuideDto>(guide);
                return Ok(guideDto);
            }
        }

        [HttpDelete("Delete Guide by Id if exists")]
        public async Task<ActionResult> DeleteGuideById(int guideId)
        {
            var guide = await _guideService.GetByGuideIdAsync(guideId);
            if (guide != null)
            {
                await _guideService.DeleteGuideById(guideId);
                return Ok($"Guide with id = {guideId} was deleted successfully.");
            }
            else
            {
                return NotFound($"There is no such guide with id = {guideId}");
            }
        }

        [HttpPost("AddOrUpdateGuide")]
        public async Task<ActionResult> AddOrUpdateGuide(GuideDto guideDto)
        {
            try
            {
                var guide = _mapper.Map<Guide>(guideDto);
                if (guide.Id == 0)
                {
                    // Add a new guide
                    await _guideService.AddOrUpdateGuide(guide);
                    return Ok("Guide was added successfully.");
                }
                else
                {
                    // Check if the guide exists
                    var existingGuide = await _guideService.GetByGuideIdAsync(guide.Id);
                    if (existingGuide == null)
                    {
                        return NotFound("You're trying to update a guide that does not exist.");
                    }
                    else
                    {
                        // Update the existing guide
                        await _guideService.AddOrUpdateGuide(guide);
                        return Ok($"Guide with ID = {guide.Id} was updated successfully.");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handle cases like duplicate username or guide not found
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
