using System.Security.Cryptography.X509Certificates;

namespace LoonieTrader.Shared.Models
{
    public interface IRequirements
    {
        int MinPoints { get; set; }

        int MaxPoints { get; set; }
    }
}