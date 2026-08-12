using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            // 1. Lấy thông tin user hiện tại
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            //2. lấy ra thông tin của stock có symbol tương ứng
            var stock  = await _stockRepository.GetStockBySymbol(symbol);
            if(stock == null)
            {
                return NotFound();
            }
            // 3. kiểm tra thông tin stock có tồn tại trong portfolio của user hay chưa
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if(userPortfolio.Any(s => s.Symbol.ToLower() == stock.Symbol.ToLower()))
            {
                return BadRequest("Stock already exists in the portfolio.");
            }

            //4. tạo mới portfolio
            var portfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioRepository.CreatePortfolio(portfolio);
            if (portfolio == null)
            {
                return BadRequest("Failed to add stock to portfolio.");
            }
            return Created();

            
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            // lấy thông tin user hiện tại
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            // lấy Portfolio của user hiện tại
            var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
            if(userPortfolio == null || !userPortfolio.Any())
            {
                return NotFound("User portfolio not found.");
            }
            // kiểm tra xem stock có tồn tại trong portfolio của user hay không
            var filterPortfolio = userPortfolio.FirstOrDefault(s => s.Symbol.ToLower() == symbol.ToLower());
            if(filterPortfolio == null)
            {
                return NotFound("Stock not found in the portfolio.");
            }

            //xóa stock khỏi portfolio
            await _portfolioRepository.DeletePortfolio(symbol, appUser);
            return Ok();
        }
    }
}