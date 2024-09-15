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
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }
        [HttpGet("Get All Members")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();
            var memberDtos = _mapper.Map<IEnumerable<MemberDto>>(members);
            return Ok(memberDtos);
        }

        [HttpGet("Get Member by Id")]
        public async Task<ActionResult<MemberDto?>> GetMemberById(int memberId)
        {
            var member = await _memberService.GetByMemberIdAsync(memberId);
            var memberDto = _mapper.Map<MemberDto>(member);
            if (memberDto == null)
            {
                return NotFound("A Member with this Id may not exist");
            }
            else
            {
                return Ok(memberDto);
            }
        }
        [HttpGet("Get Member by email (unique property)")]
        public async Task<ActionResult<MemberDto?>> GetMemberByEmail(string email)
        {
            var member = await _memberService.GetMemberByEmail(email);
            var memberDto = _mapper?.Map<MemberDto>(member);
            if (memberDto == null)
            {
                return NotFound($"There does not exist a member with this email: {email}");
            }
            else
            {
                return Ok(memberDto);
            }
        }
        [HttpDelete("Delete Member by Id if exists")]
        public async Task<ActionResult> DeleteMemberById(int memberId)
        {
            var member = await _memberService.GetByMemberIdAsync(memberId);
            if (member != null)
            {
                await _memberService.DeleteMemberById(memberId);
                return Ok($"Member with id = {memberId} was deleted successfully.");
            }
            else
            {
                return NotFound($"There is no such member with id = {memberId}");
            }
        }
        [HttpPost("AddOrUpdateMember")]
        public async Task<ActionResult> AddOrUpdateMember(MemberDto memberDto)
        {
            try
            {
                var member = _mapper.Map<Member>(memberDto);

                if (memberDto.Id == 0)
                {
                    // Add a new member
                    await _memberService.AddOrUpdateMember(member);
                    return Ok("Member was added successfully.");
                }
                else
                {
                    // Check if the member exists
                    var existingMember = await _memberService.GetByMemberIdAsync(memberDto.Id);
                    if (existingMember == null)
                    {
                        return NotFound("You're trying to update a member that does not exist.");
                    }
                    else
                    {
                        // Update the existing member
                        await _memberService.AddOrUpdateMember(member);
                        return Ok($"Member with ID = {memberDto.Id} was updated successfully.");
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Handle cases like duplicate email or member not found
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

    }
}
