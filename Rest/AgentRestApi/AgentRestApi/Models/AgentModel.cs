using System.ComponentModel.DataAnnotations;

namespace AgentRestApi.Models
{
    public class AgentModel
    {
        public enum StatusAgent
        {
            dormant,
            Activate
        }

        public int Id { get; set; }
        [Required, StringLength(100)]
        public required string NickName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Location_X { get; set; } = -1;
        public int Location_Y { get; set; } = -1;
        public StatusAgent AgentStatus { get; set; } = StatusAgent.dormant;
        public List<MissionModel> Mission { get; set; }


    }
}
