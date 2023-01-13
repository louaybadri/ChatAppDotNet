using ProjetDotNet.Models;

namespace ProjetDotNet.Data.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		public User UpdateUser(User user);
	}
}