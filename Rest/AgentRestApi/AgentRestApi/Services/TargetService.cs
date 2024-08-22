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

        public async Task<TargetModel> PinTarget(LocationDto location, int id)
        {
            var model = await context.Targets.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"This target with {id} isn't exists");
            
            if (location == null ||
                location.X > 1000 || location.X < 0
                || location.Y > 1000 || location.Y < 0)
            {
                throw new Exception("No location");
            }
            model.Location_Y = location.Y;
            model.Location_X = location.X;
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }

        private readonly Dictionary<string, (int x, int y)> directions = new()
        {
            {"n",(0,1) },
            {"e",(1,0) },
            {"w",(-1,0) },
            {"s",(0,-1) },
            {"ne",(1,1) },
            {"nw",(-1,1) },
            {"se",(1,-1) },
            {"sw",(-1,-1) }
        };


        public async Task<TargetModel> MoveTarget(DirectionDto direction, int id)
        {
            var model = await context.Targets.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"This target with {id} isn't exists");

            bool IsExist = directions.TryGetValue(direction.Movment , out var result);
            if (!IsExist) 
            { 
                throw new Exception("Not exists");
            }
            if(model.TargetStatus == TargetModel.Status.eliminated)
            {
                throw new Exception("Is eliminated");
            }

            model.Location_X += directions[direction.Movment].x;
            model.Location_Y += directions[direction.Movment].y;

            await context.SaveChangesAsync();
            return model;

        }



    }
}
