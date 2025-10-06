using Application.Abstractions;
using CSharpFunctionalExtensions;
using Domain.Entity;
using Domain.VO;
using Shared;

namespace Application.Locations.CreateLocation
{
    public class CreateLocationHandler : ICommandHandler<Guid, CreateLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public CreateLocationHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Result<Guid, Error[]>> Handle(
            CreateLocationCommand command,
            CancellationToken cancellationToken)
        {
            var resultName = LocationName.Create(command.Name);

            if (resultName.IsFailure) return new Error[] { resultName.Error };

            var resultAddress = LocationAddress.Create(
                command.Address.Country,
                command.Address.Region,
                command.Address.City,
                command.Address.Street,
                command.Address.House,
                command.Address.Apartment);

            if (resultAddress.IsFailure) return new Error[] { resultName.Error };

            var resultTimezone = LocationTimezone.Create(command.Timezone.IanaCode);

            if (resultTimezone.IsFailure) return new Error[] { resultName.Error };

            var resultLocation = Location.Create(
                resultName.Value,
                resultAddress.Value,
                resultTimezone.Value,
                command.IsActive);

            if (resultLocation.IsFailure) return new Error[] { resultName.Error };

            var resultCreate = await _locationRepository.CreateAsync(resultLocation.Value, cancellationToken);

            if (resultCreate.IsFailure) return new Error[] { resultName.Error };

            return resultCreate.Value;
        }
    }
}