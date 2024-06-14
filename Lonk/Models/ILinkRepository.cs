namespace Lonk.Models
{
    public interface ILinkRepository
    {
        Task<LongLink?> GetByLongIdAsync(string longId);
        Task SaveLinkAsync(LongLink link);
        Task DeleteAsync(string longId);
    }
}
