using System.ComponentModel.DataAnnotations;

public class IncomingMessageDto
{
    [Required(ErrorMessage = "Must enter User Type")]
    [EnumDataType(typeof(UserType), ErrorMessage = "Invalid value for UserType!")]
    public UserType SenderType { get; set; }
    [Required(ErrorMessage = "Must enter User Id")]
    public string UserId { get; set; }
    [Required(ErrorMessage = "Must enter Librarian ID")]
    public string LibrarianId { get; set; }
    [Required(ErrorMessage = "Must enter message content")]
    public string Content { get; set; }
    [Required(ErrorMessage = "Must enter Sent Time")]
    public DateTime SentTime { get; set; }
}