using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
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
            if (stockExiting == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockExiting);
            await _context.SaveChangesAsync();
            return stockExiting;

        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            // 1 chuyển DBset thành IQueryable để hoãn thực thi sql
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            // 2. lọc symbol 
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.ToLower().Contains(query.Symbol.ToLower()));
            }
            // 3, lọc theo company
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.ToLower().Contains(query.CompanyName.ToLower()));
            }


            //4. sắp xếp
            if(!string.IsNullOrWhiteSpace(query.SortBy)) {
                
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDecending
                        ? stocks.OrderByDescending(s => s.Symbol)
                        : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;



            // 4, thực thi sql
            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> StockExistingAsync(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
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