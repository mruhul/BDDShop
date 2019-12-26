using System.Threading.Tasks;
using BddShop.Features.Shared;
using Bolt.RequestBus;

namespace BddShop.Features.Registration
{
    internal sealed class RegisterUserHandler : RequestHandlerAsync<RegisterUser>
    {
        private readonly UserService _userService;
        private readonly RegistrationEmailSender _emailSender;
        private readonly IUserAuthenticator _authenticator;

        public RegisterUserHandler(UserService userService, 
            RegistrationEmailSender emailSender, 
            IUserAuthenticator authenticator)
        {
            _userService = userService;
            _emailSender = emailSender;
            _authenticator = authenticator;
        }

        protected override async Task Handle(IExecutionContextReader context, RegisterUser request)
        {
            var id = await _userService.Create(request);

            await _emailSender.SendAsync(request);

            await _authenticator.Authenticate(new UserAuthRequest
            {
                Email = request.Email,
                Id = id
            });
        }
    }

    public class RegisterUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
