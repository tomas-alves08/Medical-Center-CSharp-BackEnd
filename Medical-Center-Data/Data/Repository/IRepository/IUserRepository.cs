using Medical_Center_Data.Data.Models;
using Medical_Center_Common.Models.DTO.UserData.LoginData;
using Medical_Center_Common.Models.DTO.UserData.RegistrationData;

namespace Medical_Center_Data.Data.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> RegisterAsync(RegistrationRequestDTO registrationRequestDTO);
    }
}
