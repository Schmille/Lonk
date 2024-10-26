namespace Lonk.Models
{
    public interface IBlocklistRepo
    {
        Task CreateAsync(string hostname);
        Task DeleteAsync(string hostname);
        Task<bool> ExistsAsync(string hostname);
        Task<List<string>> GetAllAsync();
    }
}
