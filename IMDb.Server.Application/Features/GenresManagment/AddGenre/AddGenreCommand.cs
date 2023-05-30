using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.GenresManagment.AddGenre;

public record AddGenreCommand(
   string Genre
) : IRequest<Result<AddGenreCommandResponse>>;