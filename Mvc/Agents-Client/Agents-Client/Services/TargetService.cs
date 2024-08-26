using Agents_Client.ViewModel;
using System.Text.Json;

namespace Agents_Client.Services
{
    public class TargetService(IHttpClientFactory clientFactory):ITargetService
    {
        private readonly string baseUrl = "http://localhost:5226/targets";
        public async Task<List<TargetVM>> GetAllTargets()
        {
            var httpClient = clientFactory.CreateClient();
            var result = await httpClient.GetAsync(baseUrl);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<TargetVM>? targets = JsonSerializer.Deserialize<List<TargetVM>>
                    (content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return targets;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }

        public async Task<List<TargetVM>> GetTargets()
        {
            var targets = await GetAllTargets();
            
            int eliminated = targets.Where(t => t.TargetStatus == TargetVM.Status.eliminated).Count();

            List<TargetVM> targetsList = new();
            foreach (var target in targets)
            {
                targetsList.Add(new TargetVM
                {
                    Name = target.Name,
                    Position = target.Position,
                    Image = target.Image,
                    Location_X = target.Location_X,
                    Location_Y = target.Location_Y,
                    TargetStatus = target.TargetStatus,

                });
            }
            return targetsList;

        }



       
    }
}
