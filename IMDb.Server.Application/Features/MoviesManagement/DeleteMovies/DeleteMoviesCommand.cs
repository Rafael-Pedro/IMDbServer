using MediatR;
using FluentResults;

namespace IMDb.Server.Application.Features.MoviesManagement.DeleteMovies;

public record DeleteMoviesCommand(int Id) : IRequest<Result>;
