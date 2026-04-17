using System.Security.Cryptography;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllStocks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepository.GetAllStocksAsync();
            var stockDtos = stocks.Select(s => s.ToStockDto());
            return Ok(stockDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToSTockFromCreateDto();
            await _stockRepository.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepository.UpdateStockAsync(id, updateStockRequestDto);
            if (stockModel == null)
            {
                return NotFound();
            }


            return Ok(stockModel.ToStockDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRepository.DeleteStockAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}