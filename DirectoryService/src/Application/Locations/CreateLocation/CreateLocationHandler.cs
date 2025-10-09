using Application.Abstractions;
using CSharpFunctionalExtensions;
using Domain.Entity;
using Domain.VO;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Locations.CreateLocation
{
    public class CreateLocationHandler : ICommandHandler<Guid, CreateLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ILogger<CreateLocationHandler> _logger;

        public CreateLocationHandler(
            ILocationRepository locationRepository,
            ILogger<CreateLocationHandler> logger)
        {
            _locationRepository = locationRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Errors>> Handle(
            CreateLocationCommand command,
            CancellationToken cancellationToken)
        {
            var resultName = LocationName.Create(command.Name);

            if (resultName.IsFailure) return GeneralErrors.Failure().ToErrors();

            var resultAddress = LocationAddress.Create(
                command.Address.Country,
                command.Address.Region,
                command.Address.City,
                command.Address.Street,
                command.Address.House,
                command.Address.Apartment);

            if (resultAddress.IsFailure) return GeneralErrors.Failure().ToErrors();

            var resultTimezone = LocationTimezone.Create(command.Timezone.IanaCode);

            if (resultTimezone.IsFailure) return GeneralErrors.Failure().ToErrors();

            var resultLocation = Location.Create(
                resultName.Value,
                resultAddress.Value,
                resultTimezone.Value,
                command.IsActive);

            if (resultLocation.IsFailure) return GeneralErrors.Failure().ToErrors();

            var resultCreate = await _locationRepository.CreateAsync(resultLocation.Value, cancellationToken);

            if (resultCreate.IsFailure) return GeneralErrors.Failure().ToErrors();

            _logger.LogInformation("Локация успешно создалась с id - {Id}", resultCreate.Value);

            return resultCreate.Value;
        }
    }
}