using chiai.Server.Data;
using chiai.Server.Messages.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace chiai.Server.Tests
{
    [TestFixture]
    public class SaveChatMessageTest
    {
        private ApplicationDbContext _dbContext;
        private Mock<ILogger<SaveChatMessageCommandHandler>> _loggerMock;
        private SaveChatMessageCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _loggerMock = new Mock<ILogger<SaveChatMessageCommandHandler>>();

            var chat = new Chat { Id = 1, UserId = 1, Title = "Test Chat" };
            _dbContext.Chats.Add(chat);
            _dbContext.SaveChanges();

            _handler = new SaveChatMessageCommandHandler(_dbContext, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public void Handle_InvalidChatId_ThrowsArgumentException()
        {
            var command = new SaveChatMessageCommand(999, new ChatMessageDto
            {
                Content = "Invalid chat",
                IsFromAi = false,
                Author = "User"
            });

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _handler.Handle(command, CancellationToken.None));
        }
    }
}
