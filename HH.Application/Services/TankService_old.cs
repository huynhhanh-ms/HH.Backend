using HH.Application.Common;
using HH.Domain.Common;
using HH.Domain.Dto;
using HH.Domain.Models;
using HH.Domain.Repositories.Common;
using HH.Persistence.Repositories.Common;
using Mapster;

namespace HH.Application.Services
{
    public class TankService_old : BaseService, ITankService
    {
        private readonly IGenericRepository<Tank> _repository;

        public TankService_old(IUnitOfWork unitOfWork, IGenericRepository<Tank> repository) : base(unitOfWork)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<bool>> Create(TankCreateDto request)
        {

            var tank = request.Adapt<Tank>();

            tank.CreatedBy = 0;
            tank.UpdatedBy = 0;
            tank.Id = 10;

            await _repository.CreateAsync(tank);
            await _repository.SaveChangesAsync();


            return Success<bool>("Create successfully!");
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Success<bool>("Delete successfully!");
        }

        public async Task<ApiResponse<Tank>> Get(int id)
        {
            var tank = await _repository.FindAsync(id);

            if (tank == null)
                return Failed<Tank>("Tank not found!");

            return Success<Tank>(tank);
        }

        public async Task<ApiResponse<List<Tank>>> Gets(SearchBaseRequest request)
        {
            var tanks = await _repository.GetAllAsync();
            if (tanks == null)
                return Failed<List<Tank>>("Tanks not found!");
            return Success<List<Tank>>(tanks.ToList());
        }

        public async Task<ApiResponse<bool>> Update(Tank request)
        {
            var tank = await _repository.FindAsync(request.Id);
            if (tank == null)
                return Failed<bool>("Tank not found!");

            await _repository.UpdateAsync(request);
            return Success<bool>("Update successfully!");
        }
    }
}
