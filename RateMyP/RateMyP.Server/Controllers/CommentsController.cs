using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RateMyP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateMyP.Server.Controllers
    {
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
        {
        private readonly RateMyPDbContext m_context;

        public CommentsController(RateMyPDbContext context)
            {
            m_context = context;
            }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
            {
            return await m_context.Comments.ToListAsync();
            }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(Guid id)
            {
            var comment = await m_context.Comments.FindAsync(id);

            if (comment == null)
                return NotFound();

            return comment;
            }

        // PUT: api/Comments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(Guid id, Comment comment)
            {
            if (id != comment.Id)
                return BadRequest();

            m_context.Entry(comment).State = EntityState.Modified;

            try
                {
                await m_context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!CommentExists(id))
                    return NotFound();
                else
                    throw;
                }

            return NoContent();
            }

        // POST: api/Comments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
            {
            m_context.Comments.Add(comment);
            await m_context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
            }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(Guid id)
            {
            var comment = await m_context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            m_context.Comments.Remove(comment);
            await m_context.SaveChangesAsync();

            return comment;
            }

        private bool CommentExists(Guid id)
            {
            return m_context.Comments.Any(e => e.Id == id);
            }
        }
    }
