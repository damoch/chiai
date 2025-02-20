using chiai.Server.Data;
using chiai.Server.Data.Dto;
using MediatR;

namespace chiai.Server.Messages.Commands
{
    public record GetUserChatHistoryQuery(int userId) : IRequest<IEnumerable<ChatDto>>;

    public class GetUserChatHistoryQueryHandler : IRequestHandler<GetUserChatHistoryQuery, IEnumerable<ChatDto>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<GetUserChatHistoryQueryHandler> _logger;
        public GetUserChatHistoryQueryHandler(ApplicationDbContext dbContext, ILogger<GetUserChatHistoryQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<ChatDto>> Handle(GetUserChatHistoryQuery request, CancellationToken cancellationToken)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == request.userId);
            if (user == null)
            {
                _logger.LogError($"User with id {request.userId} not found");
                throw new ArgumentException("User not found");
            }
            var chats = _dbContext.Chats.Where(c => c.UserId == request.userId).ToList();
            return chats.Select(ChatDto.FromChat).ToList();
        }
    }
}
