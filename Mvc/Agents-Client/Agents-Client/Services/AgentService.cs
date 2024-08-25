using Agents_Client.ViewModel;
using System.Text.Json;

namespace Agents_Client.Services
{
    public class AgentService(IHttpClientFactory clientFactory ):IAgentService
    {
        private readonly string baseUrl = "http://localhost:5226";
        public async Task<List<AgentVM>> GetAllMissionsPorpose()
        {
            var httpClient = clientFactory.CreateClient();
            var result = await httpClient.GetAsync(baseUrl);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<AgentVM>? agents = JsonSerializer.Deserialize<List<AgentVM>>
                    (content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return agents;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }
    }
}
