/*using Medical_Center_Common.Models.DTO.AppointmentData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Shared.Models;
using Stripe.Checkout;
using Microsoft.AspNetCore.Hosting.Server;
using Medical_Center.Data.Models;

namespace Medical_Center.Controllers
{
    [ApiController]
    [Route("api/payments")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentCheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string s_wasmClientURL = string.Empty;

        public PaymentCheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> PaymentOrder([FromBody] CreateAppointmentDTO appointmentDTO, [FromServices] IServiceProvider sp)
        {
            var referer = Request.Headers.Referer;
            s_wasmClientURL = referer[0];

            var server = sp.GetRequiredService<IServer>();

            var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

            string? thisApiUrl = null;
            if (serverAddressesFeature != null)
            {
                thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
            }

            if(thisApiUrl != null) 
            {
                var sessionId = await AppointmentPayment(appointmentDTO, thisApiUrl);
                var pubKey = _configuration["Stripe:PubKey"];

                var appointmentPaymentResponse = new AppointmentOrderResponse()
                {
                    SessionId = sessionId,
                    PubKey = pubKey,
                };

                return Ok(appointmentPaymentResponse);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [NonAction]
        public async Task<string> AppointmentPayment(AppointmentDTO appointmentDTO, string thisApiUrl)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{thisApiUrl}/appointmentPayment/success?sessionId=" + "{CHECKOUT_SESSION_ID}",
                CancelUrl = s_wasmClientURL + "failed",
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                { new ()
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = appointmentDTO.Price,
                                Currency = "NZD",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = appointmentDTO.Title,
                                    Description = appointmentDTO.Description,
                                    Images = new List<string>{ appointmentDTO.imageUrl}
                                },
                            },
                            Quantity = 1,
                        },
                },
                Mode = "payment"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Id;
        }

        [HttpGet("success")]
        public ActionResult CheckoutSuccess(string sessionId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            var total = session.AmountTotal.Value;
            var customerEmail = session.CustomerDetails.Email;

            return Redirect(s_wasmClientURL + "success");
        }
    }
}
*/