using System.ComponentModel.DataAnnotations;

namespace Agents_Client.ViewModel
{
    public class MissionActiveVM
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public int Location_X_Agent { get; set; } = -1;
        public int Location_Y_Agent { get; set; } = -1;

        public string TargetName { get; set; }

        public string TargetPosition { get; set; }
        public int Location_X_Target { get; set; } = -1;
        public int Location_Y_Target { get; set; } = -1;
        public double? TimeLeft { get; set; }
        public double? Distance { get; set;}

    }
}
