using CSharpFunctionalExtensions;
using Shared;

namespace Application.Abstractions;

public interface ICommandHandler<TResponse, in TCommand>
    where TCommand : ICommand
{
    Task<Result<TResponse, Error[]>> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task<UnitResult<Error[]>> Handle(TCommand command, CancellationToken cancellationToken);
}