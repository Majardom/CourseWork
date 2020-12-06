using Authentication.Core;
 
namespace Authentication.Services.Intefaces
{
	public interface IAuthenticationService
	{
		User Authorize(string email, string password);

		void SaveTokenIdentity(TokenIdentity tokenIdentity);

		bool Validate(string token);
	}
}
