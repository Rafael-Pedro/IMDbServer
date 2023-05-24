using IMDb.Server.Application.Features.Casts.AddCast;
using IMDb.Server.Infra.Database.Abstraction;
using IMDb.Server.Infra.Database.Abstraction.Respositories;
using NSubstitute;
using Xunit;

namespace IMDb.Server.Application.Tests.Features.Casts;

public class AddCastCommandHandlerTest
{
    private readonly CastCommandHandler sut;
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ICastRepository castRepository = Substitute.For<ICastRepository>();

    public AddCastCommandHandlerTest()
    => sut = new(unitOfWork, castRepository);

    [Fact]
    public async Task Handle_WhenEmailAndUsername_ShouldRegisterUser()
    {
        
    }
}
