using AutoMapper;
using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.UserData.LocalUserData;
using Medical_Center_Common.Models.DTO.UserData.LoginData;
using Medical_Center_Common.Models.DTO.UserData.RegistrationData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medical_Center.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private string secretKey;

        public UserRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user = _db.LocalUsers.FirstOrDefault(user => user.UserName == username);

            if(user != null)
            {
                return false;
            }

            return true;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await _db.LocalUsers.FirstOrDefaultAsync(user => user.UserName.ToLower() == loginRequestDTO.UserName.ToLower()
                                                             && user.Password == loginRequestDTO.Password);

            if(user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            var userDTO = _mapper.Map<LocalUserDTO>(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = userDTO
            };

            return loginResponseDTO;
        }

        public async Task<LocalUser> RegisterAsync(RegistrationRequestDTO registrationRequestDTO)
        {
            var user = _mapper.Map<LocalUser>(registrationRequestDTO);
            await _db.LocalUsers.AddAsync(user);
            await _db.SaveChangesAsync();

            user.Password = "";

            return user; 
        }
    }
}
