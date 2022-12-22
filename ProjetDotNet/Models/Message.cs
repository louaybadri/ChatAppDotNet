using System.ComponentModel.DataAnnotations;

namespace ProjetDotNet.Models
{
	public class Message
	{
		[Key]
		public int Id { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public String MessageText { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastUpdatedAt { get; set; }
		public Message(int SenderId, int ReceiverId, string MessageText, DateTime CreatedAt, DateTime LastUpdatedAt)
		{
			this.SenderId = SenderId;
			this.ReceiverId = ReceiverId;
			this.MessageText = MessageText;
			this.CreatedAt = CreatedAt;
			this.LastUpdatedAt = LastUpdatedAt;
		}

	}
}
