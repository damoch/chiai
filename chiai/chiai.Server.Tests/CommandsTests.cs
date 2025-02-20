using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using chiai.Server.Data;
using chiai.Server.Messages.Commands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace chiai.Server.Tests
{
    [TestFixture]
    public class GetChatMessagesCommandHandlerTests
    {
        private ApplicationDbContext _dbContext;
        private Mock<ILogger<GetChatMessagesCommandHandler>> _loggerMock;
        private GetChatMessagesCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique DB for each test
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _loggerMock = new Mock<ILogger<GetChatMessagesCommandHandler>>();

            var chat = new Chat { Id = 1 };
            _dbContext.Chats.Add(chat);
            _dbContext.ChatMessages.AddRange(new List<ChatMessage>
            {
                new ChatMessage { Id = 1, ChatId = 1, Content = "Hello", Timestamp = DateTime.UtcNow, Author="PNG" },
                new ChatMessage { Id = 2, ChatId = 1, Content = "How are you?", Timestamp = DateTime.UtcNow, Author="AI" }
            });
            _dbContext.SaveChanges();

            _handler = new GetChatMessagesCommandHandler(_dbContext, _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ValidChatId_ReturnsMessages()
        {
            var command = new GetChatMessagesCommand(chatId: 1);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Content, Is.EqualTo("Hello"));
        }

        [Test]
        public void Handle_InvalidChatId_ThrowsArgumentException()
        {
            var command = new GetChatMessagesCommand(chatId: 999);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _handler.Handle(command, CancellationToken.None);
            });
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
