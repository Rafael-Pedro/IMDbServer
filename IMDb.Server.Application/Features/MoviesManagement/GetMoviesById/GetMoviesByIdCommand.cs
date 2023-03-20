using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.MoviesManagement.GetMoviesById;

public record GetMoviesByIdCommand() : IRequest<Result>;