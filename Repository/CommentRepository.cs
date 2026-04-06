using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using api.Data;

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
    }
}