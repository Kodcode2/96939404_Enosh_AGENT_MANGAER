using System.ComponentModel.DataAnnotations;

namespace Agents_Client.ViewModel
{
    public class AgentVM
    {
        public enum Status
        {
            dormant,
            Activate
        }

        public int Id { get; set; }
        public required string NickName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Location_X { get; set; } = -1;
        public int Location_Y { get; set; } = -1;
        public Status AgentStatus { get; set; } 
        public double TimeLeft { get; set; } = 0;
        public List<MissionVM> MissionActivate { get; set; }
        public int NumOfKills { get; set;}

    }
}
