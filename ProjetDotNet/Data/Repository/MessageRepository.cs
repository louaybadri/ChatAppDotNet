using ProjetDotNet.Data.Context;
using ProjetDotNet.Data.Repository;
using ProjetDotNet.Models;
using System.Linq.Expressions;

namespace ProjetDotNet.Data
{
	public class MessageRepository : Repository<Message>, IMessageRepository
	{
		public MessageRepository(ChatAppContext _applicationDbContext) : base(_applicationDbContext)
		{

		}
	}
}