using Agents_Client.ViewModel;

namespace Agents_Client.Models
{
    public class MissionModel
    {
        public enum Status
        {
            proposal,
            OnMission,
            MissionEnd
        }
       

        public AgentVM? AgentModel { get; set; }
        public TargetVM? TargetModel { get; set; }
        public double? TimeLeft { get; set; }
        public string? ExecuteTime { get; set; }
        public DateTime StartTime { get; set; }
        public Status MissionStatus { get; set; }
    }
}
