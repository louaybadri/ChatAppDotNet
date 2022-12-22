using ProjetDotNet.Models;

namespace ProjetDotNet.Data.Repository
{
	public interface IMessageRepository : IRepository<Message>
	{
		IEnumerable<Message> GetAllMessages(User currentUser, int contact);
	}
}