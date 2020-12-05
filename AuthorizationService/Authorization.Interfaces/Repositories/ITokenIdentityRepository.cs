using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core;

namespace Authentication.Interfaces
{
	public interface ITokenIdentityRepository: IGenericRepository<TokenIdentity>
	{
	}
}
