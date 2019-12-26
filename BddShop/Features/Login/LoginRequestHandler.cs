using System.Threading.Tasks;
using BddShop.Features.Shared;
using BddShop.Infra.Crypto;
using Bolt.RequestBus;

namespace BddShop.Features.Login
{
    internal sealed class LoginRequestHandler : IRequestHandlerAsync<LoginRequest>
    {
        private readonly UserService _userService;
        private readonly IUserAuthenticator _authenticator;

        public LoginRequestHandler(UserService userService, ICrypto crypto, IUserAuthenticator authenticator)
        {
            _userService = userService;
            _authenticator = authenticator;
        }
        
        public bool IsApplicable(IExecutionContextReader context, LoginRequest request)
        {
            return true;
        }

        async Task<IResponse> IRequestHandlerAsync<LoginRequest>.Handle(IExecutionContextReader context, LoginRequest request)
        {
            var user = await _userService.Retrieve(request);

            if (user == null) return FailedResponse();

            await _authenticator.Authenticate(new UserAuthRequest
            {
                Id = user.Id,
                Email = user.Email
            });

            return Response.Succeed();
        }

        private IResponse FailedResponse()
        {
            return Response.Failed(new [] {Error.Create("Email",
                "Failed to login. Please ensure you entered correct email and password.")});
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}