using System.ComponentModel.DataAnnotations;

namespace Agents_Client.Models
{
    public class AgentModel
    {
        public enum Status
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
        public Status AgentStatus { get; set; } = Status.dormant;
    }
}
