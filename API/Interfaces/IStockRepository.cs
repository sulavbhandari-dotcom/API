using API.Dtos.Stock;
using API.Models;

namespace API.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetALlAsync();
        Task<Stock?> GetByIdAsync(int id); //FirstOrDefault CAN BE NULL
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock> DeleteAsync(int id);

        Task<bool> StockExists(int id);
    }
}
