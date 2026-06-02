using System.Globalization;
using System.Xml.Linq;
using FastEndpoints;
using TcmbForexApi.WriteAPI.Business.Abstract;
using TcmbForexApi.WriteAPI.Business.DTOs;
using TcmbForexApi.WriteAPI.Core;
using TcmbForexApi.WriteAPI.Core.Requests;

namespace TcmbForexApi.WriteAPI.Endpoints
{
    public class ForexWithDateEndpoint(IForexService forexService) : Endpoint<ForexWithDateRequest, BaseResponse>
    {
        public override void Configure()
        {
            Post("/write/forex-with-date");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ForexWithDateRequest req, CancellationToken ct)
        {
            if (req.Date >= DateOnly.FromDateTime(DateTime.Now))
            {
                await SendAsync(new BaseResponse
                {
                    Success = false,
                    Message = "Date cannot be in the future."
                }, statusCode: 400, cancellation: ct);
                return;
            }
            string xmlUrl = $"https://www.tcmb.gov.tr/kurlar/{req.Date:yyyyMM}/{req.Date:ddMMyyyy}.xml";

            var doc = await CallTcmbForexEndpoint(xmlUrl);

            await SaveToDb(doc);

            await SendAsync(new BaseResponse
            {
                Success = true,
                Data = "Forex data inserted successfully."
            }, cancellation: ct);
        }

        async Task SaveToDb(XDocument doc)
        {
            foreach (var currency in doc.Descendants("Currency"))
            {
                string? code = currency.Attribute("Kod")?.Value;
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

                var currencyInsertDto = new CurrencyInsertDto
                {
                    Code = code!,
                    Name = name!,
                    Unit = unit,
                    ForexBuying = forexBuying,
                    ForexSelling = forexSelling,
                    BanknoteBuying = banknoteBuying,
                    BanknoteSelling = banknoteSelling,
                    CrossRateUSD = crossRateUSD
                };

                await forexService.AddForexAsync(currencyInsertDto);
            }
        }

        static async Task<XDocument> CallTcmbForexEndpoint(string url)
        {
            using HttpClient httpClient = new();

            string xmlContent = await httpClient.GetStringAsync(url);

            return XDocument.Parse(xmlContent);            
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