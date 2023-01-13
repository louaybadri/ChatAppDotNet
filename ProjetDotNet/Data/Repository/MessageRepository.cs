using ProjetDotNet.Data.Context;
using ProjetDotNet.Data.Repository;
using ProjetDotNet.Models;


namespace ProjetDotNet.Data
{
	public class MessageRepository : Repository<Message>, IMessageRepository
	{
		public MessageRepository(ChatAppContext _applicationDbContext) : base(_applicationDbContext)
		{
		}

		public IEnumerable<Message> GetAllMessages(User currentUser, int contactId)
		{
			return Find(c => (c.ReceiverId == currentUser.Id
								  && c.SenderId == contactId) ||
								  (c.ReceiverId == contactId
								  && c.SenderId == currentUser.Id))
								  .OrderBy(c => c.CreatedAt).ToList();
		}

		public Message UpdateMessage(int id, string newMessage)
		{
			Message msgToUpdate = Get(id);
			msgToUpdate.MessageText = newMessage;
			_applicationDbContext.Message.Update(msgToUpdate);
			return msgToUpdate;
		}

	}
}