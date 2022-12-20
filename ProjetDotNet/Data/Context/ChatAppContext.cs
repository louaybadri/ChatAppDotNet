using Microsoft.EntityFrameworkCore;
using ProjetDotNet.Models;

namespace ProjetDotNet.Data.Context
{
	public class ChatAppContext : DbContext
	{
		public DbSet<User> User{ get; set; }
		public DbSet<Message> Message { get; set; }
		private static ChatAppContext? _instance;
		public static ChatAppContext Instance
		{
			get
			{
				if (_instance is null)
				{
					_instance = Instantiate_ChatAppContext();
				}
				return _instance;
			}
		}
		private ChatAppContext(DbContextOptions o) : base(o) { }
		static public ChatAppContext Instantiate_ChatAppContext()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ChatAppContext>();
			optionsBuilder.UseSqlite(@"Data Source=C:\dotnet\database4.db");
			_instance = new ChatAppContext(optionsBuilder.Options);
			return _instance;
		}
	}
}