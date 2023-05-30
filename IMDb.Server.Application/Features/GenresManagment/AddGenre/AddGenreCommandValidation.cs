using FluentValidation;

namespace IMDb.Server.Application.Features.GenresManagment.AddGenre;

public class AddGenreCommandValidation : AbstractValidator<AddGenreCommand>
{
	public AddGenreCommandValidation()
	{
		RuleFor(rf => rf.Genre).NotEmpty().MaximumLength(50);
	}
}
