using Agents_Client.ViewModel;
using System.Text.Json;

namespace Agents_Client.Services
{
    public class MissionService(IHttpClientFactory clientFactory): IMissionService
    {
        private readonly string baseUrl = "http://localhost:5226/missions";
        public async Task<List<MissionVM>> GetAllMissions()
        {
            var httpClient = clientFactory.CreateClient();
            var result = await httpClient.GetAsync(baseUrl);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<MissionVM>? users = JsonSerializer.Deserialize<List<MissionVM>>
                    (content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return users;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }
    }
}
