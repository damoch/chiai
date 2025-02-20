using chiai.Server.Data;
using chiai.Server.Data.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace chiai.Server.Messages.Commands
{
    public record StartNewChatCommand(int userId) : IRequest<ChatDto>;

    public class StartNewChatCommandHandler : IRequestHandler<StartNewChatCommand, ChatDto>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<StartNewChatCommandHandler> _logger;
        public StartNewChatCommandHandler(ApplicationDbContext dbContext, ILogger<StartNewChatCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<ChatDto> Handle(StartNewChatCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == request.userId);

            if (user == null)
            {
                _logger.LogError($"User with id {request.userId} not found");
                throw new ArgumentException("User not found");
            }


            int nextChatNumber = (await _dbContext.Chats.MaxAsync(x => (int?)x.Id) ?? 0) + 1;

            var chat = new Chat
            {
                UserId = request.userId,
                Title = $"Chat no. {nextChatNumber}"
            };

            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.Chats.Add(chat);
                await _dbContext.SaveChangesAsync(); 
                await transaction.CommitAsync();

                return ChatDto.FromChat(chat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start new chat");
                await transaction.RollbackAsync();
                throw new InvalidOperationException("Failed to start new chat", ex);
            }
        }
    }
}
