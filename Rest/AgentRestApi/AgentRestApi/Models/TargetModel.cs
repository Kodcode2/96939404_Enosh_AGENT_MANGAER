﻿using System.ComponentModel.DataAnnotations;

namespace AgentRestApi.Models
{
    public class TargetModel
    {
        public enum StatusTarget
        {
            live,
            hunted,
            eliminated
        }
       
        public int Id { get; set; }
        [Required, StringLength(100)]
        public required string Name {  get; set; } = string.Empty;
        [Required,StringLength(100)]
        public string? Image {  get; set; }
        public required string Position { get; set; } = string.Empty;
        public int Location_X { get; set; } = -1;
        public int Location_Y { get; set; } = -1;
        public StatusTarget TargetStatus { get; set; } = StatusTarget.live;
    }
}
