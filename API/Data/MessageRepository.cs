using API.DOTs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MessageRepository(DataContext context , IMapper mapper) : IMessageRepository
{
    public void AddMessage(Message message)
    {
         context.Messages.Add(message);
    }

    public void DeleteMessage(Message message)
    {
        context.Messages.Remove(message);
    }

    public async Task<Message?> GetMessage(int id)
    {
       return await context.Messages.FindAsync(id);
    }

    public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
    {
        var query = context.Messages
        .OrderByDescending(x => x.MessageSent)
        .AsQueryable();

        query = messageParams.Container switch
        {
            "Inbox" => query.Where(x => x.Recipient.Name == messageParams.UserName
             && x.RecipientDeleted == false),

            "Outbox" => query.Where(x => x.Sender.Name == messageParams.UserName
            && x.SenderDeleted == false),

            _ => query.Where(x => x.Recipient.Name == messageParams.UserName && x.DateRead == null
            && x.RecipientDeleted == false)
        };

        var messages = query.ProjectTo<MessageDto>(mapper.ConfigurationProvider);

        return await PagedList<MessageDto>.CreateAsync(messages , messageParams.PageNumber
         , messageParams.PageSize);
    }

    public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUsername)
    {
        var messages = await context.Messages
            .Include(x => x.Sender).ThenInclude(x => x.Photos)
            .Include(x => x.Recipient).ThenInclude(x => x.Photos)
            .Where(x =>
                x.RecipientUserName == currentUserName
                 && x.RecipientDeleted == false 
                && x.SendUserName == recipientUsername || 

                x.SendUserName == currentUserName 
                && x.SenderDeleted == false 
                && x.RecipientUserName == recipientUsername
            )
            .OrderBy(x => x.MessageSent)
            .ToListAsync();

        var unReadMessage = messages.Where(x => x.DateRead == null && 
        x.RecipientUserName == currentUserName).ToList();

        if(unReadMessage.Count() != 0)
        {
            unReadMessage.ForEach(x => x.DateRead = DateTime.UtcNow);
            await context.SaveChangesAsync();
        }

    return mapper.Map<IEnumerable<MessageDto>>(messages);

    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}