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
    public class MessagesController : ApiController
    {
        private DataContext db = new DataContext();

        //Get User's messages
        [Route("api/UserMessages")]
        [HttpGet]
        public IHttpActionResult GetUserMessages(int id)
        {
            //IQueryable<Message> allMessages = db.Messages;

            //var convId = db.Conversations.Select(c => c.ConversationId).Where(c => c.ReceiverUserId == id);

            var allMessages = from d in db.Conversations
                              where d.ReceiverUserId == id
                              || d.SenderUserId == id
                              select new
                              {
                                  conversation = d.Messages,
                                  conversationId = d.ConversationId, 
                                  sender = d.Sender.FirstName + " " + d.Sender.LastName,
                                  senderId = d.SenderUserId,
                                  senderPic  = d.Sender.ProfilePic,
                                  receiver = d.Receiver.FirstName + " " + d.Receiver.LastName,
                                  receiverId = d.ReceiverUserId,
                                  receiverPic = d.Receiver.ProfilePic
                              };
                              //select d.Messages;
                                                


            //allMessages = allMessages.Contains(m => m.ConversationId == convIds);

            //allMessages = from m in db.Messages
            //              where( from d in db.Conversations select d.ConversationId)
            //              .Contains( d.ReceiverUserId == id)
            //              select m;

            return Ok(allMessages);
        }

        // GET: api/Messages
        public IQueryable<Message> GetMessages()
        {
            return db.Messages;
        }

        // GET: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.MessageId)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = message.MessageId }, message);
        }

        // DELETE: api/Messages/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Messages.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Messages.Count(e => e.MessageId == id) > 0;
        }
    }
}