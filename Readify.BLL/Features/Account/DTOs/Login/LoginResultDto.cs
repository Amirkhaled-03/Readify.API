namespace Readify.BLL.Features.Account.DTOs.Login
{
    public class LoginResultDto
    {
        public string Token { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public int LoginCode { get; set; }
    }
}
