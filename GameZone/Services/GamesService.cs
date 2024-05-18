using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;

        //Best way to save image in server is use interface IWebHostEnvironment
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FilesSettings.Imagespath}";
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                 .Include(g => g.category)
                 .Include(g => g.Devices)
                 .ThenInclude(d => d.Device)
                 .AsNoTracking()
                 .ToList();
        }

        public Game? GetById(int id)
        {
            return _context.Games
                  .Include(g => g.category)
                  .Include(g => g.Devices)
                  .ThenInclude(d => d.Device)
                  .AsNoTracking()
                  .SingleOrDefault(g => g.Id == id);
        }

        public async Task Create(CreateGameFormVM model)
        {

            var CoverName = await SaveCover(model.Cover);

            //  Save game in database

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = CoverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditGameFormVM model)
        {
            var game = _context.Games.
                Include(g => g.Devices)
                .SingleOrDefault(g => g.Id == model.Id);
            if (game == null) return null;

            var hasnewCover = model.Cover is not null; 
            var OldCover = game.Cover; 
            
            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices=model.SelectedDevices.Select(d => new GameDevice{DeviceId=d }).ToList();

            if (hasnewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRoews =_context.SaveChanges();

            if (effectedRoews > 0)
            {
                if (hasnewCover)
                {
                    var cover=Path.Combine(_imagesPath,OldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
                return null;
            }

        }

        public bool Delete(int id)
        {
            var isdeleted = false;
            var game = _context.Games.Find(id);
            if (game == null)
            {
                return isdeleted;
            }

            _context.Remove(game);

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                isdeleted=true;
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }
            return isdeleted;

        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagesPath, CoverName);

            //Save Cover in Server
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return CoverName;

        }

    
    }

  
}
