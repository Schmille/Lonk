namespace Lonk.Models
{
    public interface ILinkCleaner
    {
        Task CleanAsync(DateTime before);
    }
}
