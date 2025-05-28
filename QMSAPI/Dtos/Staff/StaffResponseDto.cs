namespace QMSAPI.Dtos.Staff
{
    public class StaffResponseDto
    {
        public int StaffId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string SRole { get; set; }
        public string Username { get; set; }
        public string Access { get; set; }
        public string? SAddress { get; set; }
    }
}
