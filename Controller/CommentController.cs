using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using api.Dtos.Comment;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace api.Controllers
{
    [ApiController]
    [Route("api/comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepository.GetAllCommentsAsync();

            var commentDtos = comments.Select(c => c.ToCommentDto());
            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockRepository.StockExistsAsync(stockId))
            {
                return NotFound($"Stock with id {stockId} not found.");
            }
            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var updatedComment = await _commentRepository.UpdateCommentAsync(id, commentDto.ToCommentFromUpdateDto());
            if (updatedComment == null)
            {
                return NotFound("Comment not found.");
            }
            return Ok(updatedComment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletedComment = await _commentRepository.DeleteCommentAsync(id);
            if (deletedComment == null)
            {
                return NotFound("Comment not found.");
            }
            return NoContent();
        }
    }
}