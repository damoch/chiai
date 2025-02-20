using chiai.Server.Data;
using chiai.Server.Messages.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace chiai.Server.Tests;

[TestFixture]
public class GetChatHistoryTest
{
    private ApplicationDbContext _dbContext;
    private Mock<ILogger<GetUserChatHistoryQueryHandler>> _loggerMock;
    private GetUserChatHistoryQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        _dbContext = new ApplicationDbContext(options);
        _loggerMock = new Mock<ILogger<GetUserChatHistoryQueryHandler>>();
        var user = new User { Id = 1, Username = "Test User" };
        _dbContext.Users.Add(user);

        _dbContext.Chats.AddRange(new List<Chat>
            {
                new Chat { Id = 1, UserId = 1, Title = "Chat 1" },
                new Chat { Id = 2, UserId = 1, Title = "Chat 2" }
            });

        _dbContext.SaveChanges();
        _handler = new GetUserChatHistoryQueryHandler(_dbContext, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ValidUserId_ReturnsChats()
    {
        var query = new GetUserChatHistoryQuery(userId: 1);
        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(2));
        Assert.That(result.First().Title, Is.EqualTo("Chat 1"));
    }

    [Test]
    public void Handle_InvalidUserId_ThrowsArgumentException()
    {
        var query = new GetUserChatHistoryQuery(userId: 999);
        var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            await _handler.Handle(query, CancellationToken.None));

    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }
}
