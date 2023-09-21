using Medical_Center_Common.Models.DTO.UserData.LocalUserData;

namespace Medical_Center_Common.Models.DTO.UserData.LoginData
{
    public class LoginResponseDTO
    {
        public LocalUserDTO User { get; set; }
        public string Token { get; set; }
    }
}
