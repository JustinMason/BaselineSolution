namespace Core.Domain.DTO
{
    public class ResetPasswordDto
    {
        public string ActivationUrl { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
