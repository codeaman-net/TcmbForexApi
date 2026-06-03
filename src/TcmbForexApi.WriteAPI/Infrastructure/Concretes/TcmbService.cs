using System.Globalization;
using System.Xml.Linq;
using TcmbForexApi.WriteAPI.Business.DTOs;
using TcmbForexApi.WriteAPI.Infrastructure.Abstracts;

namespace TcmbForexApi.WriteAPI.Infrastructure.Concretes
{
    public class TcmbService : ITcmbService
    {
        public async Task<IEnumerable<ForexRateDto>> ReadForexRates(DateOnly date)
        {
            string url = $"https://www.tcmb.gov.tr/kurlar/{date:yyyyMM}/{date:ddMMyyyy}.xml";

            using HttpClient httpClient = new();

            string xmlContent = await httpClient.GetStringAsync(url);
            
            XDocument doc = XDocument.Parse(xmlContent);

            var rates = ParseForexRates(doc);
            return rates;
        }

        IEnumerable<ForexRateDto> ParseForexRates(XDocument doc)
        {
            var rates = new List<ForexRateDto>();

            foreach (var currency in doc.Descendants("Currency"))
            {
                string? code = currency.Attribute("CurrencyCode")?.Value;
                string? name = currency.Element("CurrencyName")?.Value;
                int unit = int.Parse(currency.Element("Unit")?.Value ?? "1");

                decimal forexBuying =
                    ParseDecimal(currency.Element("ForexBuying")?.Value);

                decimal forexSelling =
                    ParseDecimal(currency.Element("ForexSelling")?.Value);

                decimal banknoteBuying =
                    ParseDecimal(currency.Element("BanknoteBuying")?.Value);

                decimal banknoteSelling =
                    ParseDecimal(currency.Element("BanknoteSelling")?.Value);

                decimal crossRateUSD =
                    ParseDecimal(currency.Element("CrossRateUSD")?.Value);

                rates.Add(new ForexRateDto
                {
                    Code = code!,
                    Name = name!,
                    Unit = unit,
                    ForexBuying = forexBuying,
                    ForexSelling = forexSelling,
                    BanknoteBuying = banknoteBuying,
                    BanknoteSelling = banknoteSelling,
                    CrossRateUSD = crossRateUSD
                });
            }

            return rates;
        }

        static decimal ParseDecimal(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0;

            if (decimal.TryParse(
                    value.Replace(",", "."),
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out decimal result))
            {
                return result;
            }

            return 0;
        }
    }
}