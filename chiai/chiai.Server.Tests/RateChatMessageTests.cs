using chiai.Server.Data.Enums;
using chiai.Server.Data;
using chiai.Server.Messages.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace chiai.Server.Tests
{
    internal class RateChatMessageTests
    {
        private ApplicationDbContext _dbContext;
        private Mock<ILogger<RateChatMessageCommandHandler>> _loggerMock;
        private RateChatMessageCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _loggerMock = new Mock<ILogger<RateChatMessageCommandHandler>>();

            var chat = new Chat { Id = 1, UserId = 1, Title = "Test Chat", };
            var message = new ChatMessage { Id = 1, ChatId = 1, Content = "Test Message", Rating = RatingType.None, Author="ChiAI" };

            _dbContext.Chats.Add(chat);
            _dbContext.ChatMessages.Add(message);
            _dbContext.SaveChanges();

            _handler = new RateChatMessageCommandHandler(_dbContext, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public void Handle_InvalidChatId_ThrowsArgumentException()
        {
            var command = new RateChatMessageCommand(chatId: 999, messageId: 1, rating: (int)RatingType.Like);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_InvalidMessageId_ThrowsArgumentException()
        {
            var command = new RateChatMessageCommand(chatId: 1, messageId: 999, rating: (int)RatingType.Like);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _handler.Handle(command, CancellationToken.None));
        }
    }
}
