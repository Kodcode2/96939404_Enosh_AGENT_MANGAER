using static Agents_Client.ViewModel.TargetVM;

namespace Agents_Client.ViewModel
{
    public class MissionVM
    {
        public enum Status
        {
            proposal,
            OnMission,
            MissionEnd
        }
      
            public int AgentId { get; set; }
            public int TargetId { get; set; }
            public AgentVM? AgentModel { get; set; }
            public TargetVM? TargetModel { get; set; }
            public double? TimeLeft { get; set; }
            public string? ExecuteTime { get; set; }
            public DateTime StartTime { get; set; }
            public Status MissionStatus { get; set; }
        }
    }

