using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GroupProject.Infrastructure;
using GroupProject.Models;

namespace GroupProject.Controllers
{
    public class ConversationsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Conversations
        public IQueryable<Conversation> GetConversations()
        {
            return db.Conversations;
        }

        // GET: api/Conversations/5
        [ResponseType(typeof(Conversation))]
        public IHttpActionResult GetConversation(int id)
        {
            Conversation conversation = db.Conversations.Find(id);
            if (conversation == null)
            {
                return NotFound();
            }

            return Ok(conversation);
        }

        // PUT: api/Conversations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConversation(int id, Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != conversation.ConversationId)
            {
                return BadRequest();
            }

            db.Entry(conversation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConversationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Conversations
        [ResponseType(typeof(Conversation))]
        public IHttpActionResult PostConversation(Conversation conversation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var possibleCon = db.Conversations.Select(d => d.SenderUserId == conversation.SenderUserId && d.ReceiverUserId == conversation.ReceiverUserId);

            var possibleCon = from d in db.Conversations
                              where d.SenderUserId == conversation.SenderUserId
                              && d.ReceiverUserId == conversation.ReceiverUserId
                              select d.ConversationId;

            if (possibleCon.Any())
            {
                return Ok(new { conversationId = possibleCon } );
            }

            db.Conversations.Add(conversation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = conversation.ConversationId }, conversation);
        }

        // DELETE: api/Conversations/5
        [ResponseType(typeof(Conversation))]
        public IHttpActionResult DeleteConversation(int id)
        {
            Conversation conversation = db.Conversations.Find(id);
            if (conversation == null)
            {
                return NotFound();
            }

            db.Conversations.Remove(conversation);
            db.SaveChanges();

            return Ok(conversation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ConversationExists(int id)
        {
            return db.Conversations.Count(e => e.ConversationId == id) > 0;
        }
    }
}