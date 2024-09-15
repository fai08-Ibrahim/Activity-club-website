using IDSProject.Models;
using IDSProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDSProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpController : ControllerBase
    {
        private readonly ILookUpService _lookUpService;

        public LookUpController(ILookUpService lookUpService)
        {
            _lookUpService = lookUpService;
        }

        // Get all LookUps
        [HttpGet("Get All LookUps")]
        public async Task<ActionResult<IEnumerable<LookUp>>> GetAllLookUps()
        {
            var lookUps = await _lookUpService.GetAllLookUpsAsync();
            return Ok(lookUps);
        }

        // Get LookUp by Code
        [HttpGet("Get the lookUp = {code}")]
        public async Task<ActionResult<LookUp>> GetLookUpByCode(int code)
        {
            var lookUp = await _lookUpService.GetLookUpByCodeAsync(code);
            if (lookUp == null)
            {
                return NotFound($"LookUp with code {code} not found.");
            }
            return Ok(lookUp);
        }

        [HttpPost("Add a new LookUp")]
        public async Task<ActionResult> AddLookUp(LookUp lookUp)
        {
            await _lookUpService.AddLookUpAsync(lookUp);
            return Ok("LookUp added successfully.");
        }

        [HttpPut("Update and existing lookUp = {code}")]
        public async Task<ActionResult> UpdateLookUp(int code, LookUp lookUp)
        {
            var existingLookUp = await _lookUpService.GetLookUpByCodeAsync(code);
            if (existingLookUp == null)
            {
                return NotFound($"LookUp with code {code} not found.");
            }

            lookUp.Code = code; // Ensure the code remains unchanged during the update
            await _lookUpService.UpdateLookUpAsync(lookUp);
            return Ok("LookUp updated successfully.");
        }

        // Delete LookUp by Code
        [HttpDelete("{code}")]
        public async Task<ActionResult> DeleteLookUp(int code)
        {
            var existingLookUp = await _lookUpService.GetLookUpByCodeAsync(code);
            if (existingLookUp == null)
            {
                return NotFound($"LookUp with code {code} not found.");
            }

            await _lookUpService.DeleteLookUpAsync(code);
            return Ok("LookUp deleted successfully.");
        }
    }
}
