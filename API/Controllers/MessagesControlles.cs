using API.DOTs;
using API.Entities;
using API.Extenstions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class MessagesController(IMessageRepository messageRepository , IUserRepository userRepository
, IMapper mapper) : BaseApiController
{
    [HttpPost]
    public async Task<ActionResult<MemberDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var username = User.GetUserName();

        if(username == createMessageDto.RecipientUsername.ToLower())
            return BadRequest("You cannot message your self");

        var sender = await userRepository.GetUserByUserNameAsync(username);

        var recipient = await userRepository.GetUserByUserNameAsync(createMessageDto.RecipientUsername);

        if(sender == null || recipient == null) return BadRequest("cannot send message at this time");
        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SendUserName = sender.Name,
            RecipientUserName = recipient.Name,
            Content = createMessageDto.Content


        };

        messageRepository.AddMessage(message);

        if(await messageRepository.SaveAllAsync()) return Ok(mapper.Map<MessageDto>(message));

        return BadRequest("Faild to save message");
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>>GetMessagesForUser(
             [FromQuery] MessageParams messageParams)
        {
            messageParams.UserName = User.GetUserName();

            var messages = await messageRepository.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages);

            return messages;
        }

    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
    {
        var currentUsername = User.GetUserName();

        return Ok(await messageRepository.GetMessageThread(currentUsername , username));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        var username = User.GetUserName();
        
        var message = await messageRepository.GetMessage(id);

        if(message == null) return BadRequest("this message cannot deleted");

        if(message.SendUserName != username && message.RecipientUserName != username)
            return Forbid();

        if(message.SendUserName == username) message.SenderDeleted = true;
        if(message.RecipientUserName == username) message.RecipientDeleted = true;

        if(message is {SenderDeleted: true , RecipientDeleted: true})
        {
            messageRepository.DeleteMessage(message);
        }

        if(await messageRepository.SaveAllAsync()) return Ok();

        return BadRequest("problem deleting the message");
    }

}