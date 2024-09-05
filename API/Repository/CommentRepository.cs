using API.Data;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CommentRepository : ICommentRepository
    {
       
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context) 
        { 
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comment.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comment.FindAsync(id) ;
            if (comment == null)
            {
                return null;
            }
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetALlAsync()
        {
            return await _context.Comment.ToListAsync(); 
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null) {
                return null;
                    }
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);

            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }

    

    }
}
