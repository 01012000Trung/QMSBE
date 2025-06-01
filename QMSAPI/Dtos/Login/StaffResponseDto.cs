namespace QMSAPI.Dtos.Login
{
    public class StaffResponseDto
    {
        public int StaffId { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string SRole { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Access { get; set; } = null!;
        public string SAddress { get; set; } = null!;
    }
}
