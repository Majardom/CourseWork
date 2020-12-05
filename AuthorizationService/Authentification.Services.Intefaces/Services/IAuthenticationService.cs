using Authentication.Core;
 
namespace Authentication.Services.Intefaces
{
	public interface IAuthenticationService
	{
		User Authorize(string email, string password);
	}
}
