namespace BlazorApp5.Components.Services
{
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using BlazorApp5.Components.Models;
    using System.Diagnostics;
    using static BlazorApp5.Components.Models.TestCaseData;

    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<JourneyPeriod>> FetchTestCaseDataAsync()
        {
            try
            {
                // var raw = await _http.GetStringAsync("https://raw.githubusercontent.com/ssttechdev/TestCase/refs/heads/main/Z388-25061.json");
                var raw = await _http.GetStringAsync("https://raw.githubusercontent.com/jeffry-tambari/TestCase/refs/heads/main/Z388-25061.json");

                var obj = JsonSerializer.Deserialize<Root>(raw);
                var jp = BuildJourneyPeriods(obj);

                return jp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<JourneyPeriod> BuildJourneyPeriods(Root json)
        {
            var result = new List<JourneyPeriod>();

            foreach (var port in json.PortCalls)
            {
                // Only process Function == "L"
                if (port.Function != "L")
                    continue;

                var jp = new JourneyPeriod
                {
                    Sequence = port.Sequence,
                    Name = port.Name
                };

                foreach (var act in port.Activities)
                {
                    // Only process Function == "L"
                    if (act.Function != "L")
                        continue;

                    if (act.Name == "ACTUAL TIME DEPARTURE / SAILED")
                    {
                        jp.ATD = act.Time;
                    }
                    else if (act.Name == "ACTUAL TIME ARRIVED (EOSV - END OF SEA VOYAGE)")
                    {
                        jp.ATA = act.Time;
                    }
                }

                // After filling ATD + ATA, compute difference
                if (!string.IsNullOrEmpty(jp.ATD) && !string.IsNullOrEmpty(jp.ATA))
                {
                    var atd = DateTime.Parse(jp.ATD);
                    var ata = DateTime.Parse(jp.ATA);

                    // jp.Duration = ata - atd;
                    jp.Duration = (ata - atd).Duration();
                }

                result.Add(jp);
            }

            return result;
        }


    }



}
