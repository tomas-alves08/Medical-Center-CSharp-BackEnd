using Medical_Center_Common.Models.DTO.EmailData;

namespace Medical_Center_Services.Services
{
    public interface IEmailService
{
        void SendEmail(EmailDTO request);
}
}
