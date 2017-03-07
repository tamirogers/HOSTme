using GroupProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace GroupProject.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext() : base("RoomMe")
        {

        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Conversation> Conversations { get; set; }
        public IDbSet<Favorite> Favorites { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        //configure relationships
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //Compound Key
            modelBuilder.Entity<Favorite>()
                .HasKey(a => new { a.UserId, a.RoomId });

            //A User has many Favorites
            modelBuilder.Entity<User>()
                .HasMany(r => r.Favorites)
                .WithRequired(f => f.User)
                .HasForeignKey(f => f.UserId);


            //One to Many User to Rooms
            modelBuilder.Entity<User>()
                .HasMany(u => u.Rooms)
                .WithRequired(r => r.User)
                .HasForeignKey(r => r.UserId);


            modelBuilder.Entity<Conversation>()
                .HasRequired(c => c.Sender)
                .WithMany(s => s.Conversations)
                .HasForeignKey(s => s.SenderUserId);

            modelBuilder.Entity<Conversation>()
               .HasRequired(c => c.Receiver)
               .WithMany()
               .HasForeignKey(s => s.ReceiverUserId);


            //One to Many Conversation to Messages
            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Messages)
                .WithRequired(m => m.Conversation)
                .HasForeignKey(m => m.ConversationId);

            base.OnModelCreating(modelBuilder);
        }
    }
}