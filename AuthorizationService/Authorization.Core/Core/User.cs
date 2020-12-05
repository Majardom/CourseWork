namespace Authentication.Core
{
	public class User : BaseObject
	{
		public string Name { get; set; }
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
