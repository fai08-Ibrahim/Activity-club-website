using IDSProject.Models;
using IDSProject.Repositories;
using IDSProject.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDSProject.Services
{
    public class LookUpService : ILookUpService
    {
        private readonly ILookUpRepository _lookUpRepository;

        public LookUpService(ILookUpRepository lookUpRepository)
        {
            _lookUpRepository = lookUpRepository;
        }

        // Get all LookUps
        public async Task<IEnumerable<LookUp>> GetAllLookUpsAsync()
        {
            return await _lookUpRepository.GetAllLookUpsAsync();
        }

        // Get LookUp by Code
        public async Task<LookUp?> GetLookUpByCodeAsync(int code)
        {
            return await _lookUpRepository.GetLookUpByCodeAsync(code);
        }

        // Add a new LookUp
        public async Task AddLookUpAsync(LookUp lookUp)
        {
            await _lookUpRepository.AddLookUpAsync(lookUp);
        }

        // Update an existing LookUp
        public async Task UpdateLookUpAsync(LookUp lookUp)
        {
            await _lookUpRepository.UpdateLookUpAsync(lookUp);
        }

        // Delete LookUp by Code
        public async Task DeleteLookUpAsync(int code)
        {
            await _lookUpRepository.DeleteLookUpAsync(code);
        }
    }
}
