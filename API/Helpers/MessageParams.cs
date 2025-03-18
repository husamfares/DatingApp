namespace API.Helpers;

public class MessageParams : PaginaitionParams
{

    public string?  UserName { get; set; }

    public string Container { get; set; } = "Unread";
}