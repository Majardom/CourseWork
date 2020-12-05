namespace Authentication.Core
{
	public class TokenIdentity : BaseObject
	{
		public string Token { get; set; }
		public string UserId { get; set; }
	}
}
