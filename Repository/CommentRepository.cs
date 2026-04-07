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
    }
}