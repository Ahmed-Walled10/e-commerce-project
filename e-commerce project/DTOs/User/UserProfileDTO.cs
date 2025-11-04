namespace e_commerce_project.DTOs.User
{
    public class UserProfileDTO
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Gender { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateTime Created_at { get; set; }

    }
}
