namespace Dto.Users
{
    public class ResetPasswordDto
    {
        public int Id { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        public string Email { get; set; }
    }
}
