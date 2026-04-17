using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.Include(c => c.Stock).ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync([FromRoute] int id)
        {
            return await _context.Comments.Include(c => c.Stock).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
           var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            _context.Comments.Update(existingComment);
            await _context.SaveChangesAsync();
            return existingComment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentModel == null)
            {
                return null;
            }
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }
    }
}