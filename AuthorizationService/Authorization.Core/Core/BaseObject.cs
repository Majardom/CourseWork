using System.ComponentModel.DataAnnotations;

namespace Authentication.Core
{
	public class BaseObject
	{
		[Key]
		public string Id { get; set; }
	}
}
