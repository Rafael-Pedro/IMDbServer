using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.MoviesManagement.GetMoviesById;

public record GetMoviesByIdQuery() : IRequest<Result>;