using System.ComponentModel.DataAnnotations;

namespace AgentRestApi.Models
{
    public enum Status
    {
        proposal,
        OnMission,
        MissionEnd
    }
    public class MissionModel
    {
        public int Id { get; set; }
       
        public int AgentId { get; set; }
        public AgentModel? AgentModel { get; set; }
        public int TargetId{ get; set; }
        public TargetModel? TargetModel { get; set; }
        public double? TimeLeft { get; set; }
        public double ExecuteTime { get; set;}
        public Status Status { get; set; }

    }
}
