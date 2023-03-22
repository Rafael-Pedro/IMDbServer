using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.GenresManagment.AddGenre;

public record AddGenreCommand():IRequest<Result<AddGenreCommandResponse>>;