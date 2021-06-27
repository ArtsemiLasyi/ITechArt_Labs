using BusinessLogic.Models;
using BusinessLogic.Statuses;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ServiceService
    {
        private readonly ServiceRepository _serviceRepository;

        public ServiceService(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceModel> CreateAsync(ServiceModel service)
        {
            ServiceEntity serviceEntity = service.Adapt<ServiceEntity>();
            await _serviceRepository.CreateAsync(serviceEntity);
            return serviceEntity.Adapt<ServiceModel>();
        }

        public async Task<ServiceModel?> GetByAsync(int id)
        {
            ServiceEntity? service = await _serviceRepository.GetByAsync(id);
            if (service == null)
            {
                return null;
            }
            return service.Adapt<ServiceModel>();
        }

        public async Task<IReadOnlyCollection<ServiceModel>> GetAllAsync()
        {
            IReadOnlyCollection<ServiceEntity> models = await _serviceRepository.GetAllAsync();
            return models.Adapt<IReadOnlyCollection<ServiceModel>>();
        }

        public async Task<ServiceDeletionStatus> DeleteByAsync(int id)
        {
            ServiceEntity? seatTypeEntity = await _serviceRepository.GetByAsync(id);
            if (seatTypeEntity == null)
            {
                return ServiceDeletionStatus.NotFound;
            }
            if (await _serviceRepository.IsUsedAsync(id))
            {
                return ServiceDeletionStatus.DeletionRestricted;
            }
            if (await _serviceRepository.DeleteByAsync(id))
            {
                return ServiceDeletionStatus.DeletionSuccessful;
            }
            return ServiceDeletionStatus.NotFound;
        }

        public Task EditAsync(ServiceModel model)
        {
            ServiceEntity serviceEntity = model.Adapt<ServiceEntity>();
            return _serviceRepository.UpdateAsync(serviceEntity);
        }
    }
}
