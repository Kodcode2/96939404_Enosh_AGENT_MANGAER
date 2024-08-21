using AgentRestApi.Data;
using AgentRestApi.Dto;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Services
{
    public class TargetService(ApplicationDbContext context) : ITargetService
    {
        public async Task<TargetModel?> CreateTargetAsync(TargetDto targetDto)
        {
            if (targetDto == null)
            {
                throw new Exception("Not Found");
            }
            TargetModel model = new()
            {
                Name = targetDto.Name,
                Position = targetDto.Position,
                Image = targetDto.PhotoUrl
            };
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<List<TargetModel?>> GetAllTargetAsync()
        {
            return await context.Targets.ToListAsync();
        }

        public async Task<TargetModel> PinTarget(LocationDto location, TargetModel model)
        {
            if (location == null)
            {
                throw new Exception("No location");
            }
            model.Location_Y = location.Y;
            model.Location_X = location.X;
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }



    }
}
