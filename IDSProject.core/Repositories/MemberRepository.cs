using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IDSProject.core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Http.HttpResults;
using IDSProject.Models;
using IDSProject.core.Models;

namespace IDSProject.core.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DatabaseServerContext _context;
        public MemberRepository(DatabaseServerContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _context.Set<Member>()
                .Include(m => m.EventMembers)  // Include the EventMember relationship
                .ThenInclude(em => em.Event)   // Then include the Event details
                .ToListAsync();
        }
        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Set<Member>()
                .Include(m => m.EventMembers)  // Include the EventMember relationship
                .ThenInclude(em => em.Event)   // Then include the Event details
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task AddOrUpdateMemberAsync(Member member)
        {
            // Check if the email is already used by another member
            var existingMemberWithEmail = await _context.Set<Member>()
                .FirstOrDefaultAsync(m => m.Email == member.Email && m.Id != member.Id);

            if (existingMemberWithEmail != null)
            {
                throw new InvalidOperationException("A member with the same email already exists.");
            }

            if (member.Id == 0)
            {
                // Add a new member
                await _context.Set<Member>().AddAsync(member);
            }
            else
            {
                // Find the existing member by Id
                var existingMember = await _context.Set<Member>().FindAsync(member.Id);
                if (existingMember != null)
                {
                    // Update the existing member's details
                    _context.Entry(existingMember).CurrentValues.SetValues(member);
                }
                else
                {
                    throw new InvalidOperationException("Member not found.");
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMemberByIdAsync(int memberId)
        {
            // Find the member
            var member = await _context.Set<Member>().FindAsync(memberId);

            if (member != null)
            {
                // Find all EventMember entries related to this member
                var eventMembers = await _context.Set<EventMember>()
                    .Where(em => em.MemberId == memberId)
                    .ToListAsync();

                // Remove all related EventMember entries
                if (eventMembers.Any())
                {
                    _context.Set<EventMember>().RemoveRange(eventMembers);
                }

                // Remove the member
                _context.Set<Member>().Remove(member);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Member?> GetMemberByEmailAsync(string email)
        {
            return await _context.Set<Member>().SingleOrDefaultAsync(m => m.Email == email);
        }

    }
}
