using IMDb.Server.Application.Extension;
using IMDb.Server.Application.Features.Casts.EditCast;
using IMDb.Server.Domain.Entities;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using NSubstitute;
using Xunit;

namespace IMDb.Server.Application.Tests.Features.Casts;

public class EditCastCommandHandlerTest
{
    private readonly EditCastCommandHandler sut;
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ICastRepository castRepository = Substitute.For<ICastRepository>();

    public EditCastCommandHandlerTest()
    => sut = new(unitOfWork, castRepository);

    [Fact]
    public async Task Handle_WhenCastIsNotNullAndUnique_ShouldSuccess()
    {
        var nameCast = "Leilany";
        var descriptionCast = "A mais bela do reino!! E a mais injuadinha também";
        var birthCast = new DateTime(1997, 12, 15);

        var cast = new Cast
        {
            Id = 1,
            Name = nameCast,
            Description = descriptionCast,
            DateBirth = birthCast
        };

        var request = new EditCastCommand(cast.Id, nameCast, descriptionCast, birthCast);

        castRepository.GetById(cast.Id, Arg.Any<CancellationToken>()).Returns(cast);
        castRepository.IsUniqueCast(cast.Name, Arg.Any<CancellationToken>()).Returns(true);

        var response = await sut.Handle(request, CancellationToken.None);

        castRepository.Received().Update(cast);
        await unitOfWork.Received().SaveChangesAsync(CancellationToken.None);
        Assert.True(response.IsSuccess);
        Assert.Empty(response.Errors);
    }

    [Fact]
    public async Task Handle_WhenNameCastIsNotNullAndNotUnique_ShouldFail()
    {
        var nameCast = "testName";
        var descriptionCast = "description";
        var birthCast = new DateTime(1111, 01, 01);

        var cast = new Cast
        {
            Id = 1,
            Name = nameCast,
            Description = descriptionCast,
            DateBirth = birthCast
        };

        castRepository.GetById(cast.Id, Arg.Any<CancellationToken>()).Returns(cast);

        var request = new EditCastCommand(1, nameCast, descriptionCast, birthCast);
        castRepository.IsUniqueCast(cast.Name, Arg.Any<CancellationToken>()).Returns(false);

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());

    }
    [Fact]
    public async Task Handle_WhenCastIsNull_ShouldFail()
    {
        var nameCast = "testName";
        var descriptionCast = "description";
        var birthCast = new DateTime(1111, 01, 01);

        var cast = new Cast
        {
            Id = 1,
            Name = nameCast,
            Description = descriptionCast,
            DateBirth = birthCast
        };

        castRepository.GetById(0, Arg.Any<CancellationToken>()).Returns(cast);

        var request = new EditCastCommand(0, nameCast, descriptionCast, birthCast);

        var response = await sut.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.NotEmpty(response.Errors);
        Assert.IsType<ApplicationError>(response.Errors.First());
    }

}
