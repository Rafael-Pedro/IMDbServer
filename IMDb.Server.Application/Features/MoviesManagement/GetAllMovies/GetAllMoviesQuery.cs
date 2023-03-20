using FluentResults;
using MediatR;

namespace IMDb.Server.Application.Features.MoviesManagement.GetAllMovies;

public record GetAllMoviesQuery() : IRequest<Result>;