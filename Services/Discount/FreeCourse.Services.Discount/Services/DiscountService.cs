using Dapper;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<ResponseDto<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

            return status > 0 ? ResponseDto<NoContent>.Success(204) : ResponseDto<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<ResponseDto<List<EntDiscount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<EntDiscount>("Select * from discount");

            return ResponseDto<List<EntDiscount>>.Success(discounts.ToList(), 200);
        }

        public async Task<ResponseDto<EntDiscount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<EntDiscount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });

            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount == null)
            {
                return ResponseDto<EntDiscount>.Fail("Discount not found", 404);
            }

            return ResponseDto<EntDiscount>.Success(hasDiscount, 200);
        }

        public async Task<ResponseDto<EntDiscount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<EntDiscount>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();

            if (discount == null)
            {
                return ResponseDto<EntDiscount>.Fail("Discount not found", 404);
            }

            return ResponseDto<EntDiscount>.Success(discount, 200);
        }

        public async Task<ResponseDto<NoContent>> Save(EntDiscount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);

            if (saveStatus > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }

            return ResponseDto<NoContent>.Fail("an error occurred while adding", 500);
        }

        public async Task<ResponseDto<NoContent>> Update(EntDiscount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });

            if (status > 0)
            {
                return ResponseDto<NoContent>.Success(204);
            }

            return ResponseDto<NoContent>.Fail("Discount not found", 404);
        }
    }
}