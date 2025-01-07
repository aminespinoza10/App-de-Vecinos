using AppVecinos.API.Data;
using AppVecinos.API.Models;

namespace AppVecinos.API.Services
{
    public class FeeService : IFeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Fee>> GetFeesAsync()
        {
            return await _unitOfWork.FeeRepository.GetAllAsync();
        }

        public async Task<Fee> GetFeeByIdAsync(int id)
        {
            return await _unitOfWork.FeeRepository.GetByIdAsync(id);
        }

        public async Task<Fee> CreateFeeAsync(Fee fee)
        {
            await _unitOfWork.FeeRepository.AddAsync(fee);
            await _unitOfWork.SaveAsync();
            return fee;
        }

        public async Task<Fee> UpdateFeeAsync(Fee fee)
        {
            if (await _unitOfWork.FeeRepository.GetByIdAsync(fee.Id) == null)
            {
                throw new KeyNotFoundException($"Fee with id {fee.Id} not found.");
            }
            _unitOfWork.FeeRepository.Update(fee);
            await _unitOfWork.SaveAsync();
            return fee;
        }

        public async Task DeleteFeeAsync(int id)
        {
            if (await _unitOfWork.FeeRepository.GetByIdAsync(id) == null)
            {
                throw new KeyNotFoundException($"Fee with id {id} not found.");
            }
            await _unitOfWork.FeeRepository.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}