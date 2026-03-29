using System.Security.Cryptography;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]

        public ActionResult GetAllStocks()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public ActionResult GetStockById([FromRoute] int id)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToSTockFromCreateDto();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }

            stockModel.Symbol = updateStockRequestDto.Symbol;
            stockModel.CompanyName = updateStockRequestDto.CompanyName;
            stockModel.Purchase = updateStockRequestDto.Purchase;
            stockModel.LastDiv = updateStockRequestDto.LastDiv;
            stockModel.Industry = updateStockRequestDto.Industry;
            stockModel.MarketCap = updateStockRequestDto.MarketCap;

            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteStock([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stockModel == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return NoContent();
        }
    }
}