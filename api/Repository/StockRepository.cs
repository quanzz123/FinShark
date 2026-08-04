using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockExiting = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stockExiting == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockExiting);
            await _context.SaveChangesAsync();
            return stockExiting;

        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockExiting = await _context.Stocks
                .FirstOrDefaultAsync(s => s.Id == id);
            if (stockExiting == null)
            {
                return null;
            }

            stockExiting.Symbol = stockDto.Symbol;
            stockExiting.CompanyName = stockDto.CompanyName;
            stockExiting.Purchase = stockDto.Purchase;
            stockExiting.LastDiv = stockDto.LastDiv;
            stockExiting.Industry = stockDto.Industry;
            stockExiting.MarketCap = stockDto.MarketCap;
            await _context.SaveChangesAsync();
            return stockExiting;
        }
    }
}