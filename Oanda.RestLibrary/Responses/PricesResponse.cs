using System.Text;

namespace Oanda.RestLibrary.Responses
{
    public class PricesResponse
    {
        public Account[] accounts { get; set; }

        public override string ToString()
        {
            var resp = new StringBuilder();
            foreach (var account in accounts)
            {
                resp.Append("id: ");
                resp.Append(account.id);
                resp.Append(", ");
                resp.Append("tags: ");
                resp.Append(string.Concat(account.tags));
            }

            return resp.ToString();
        }
    }

}