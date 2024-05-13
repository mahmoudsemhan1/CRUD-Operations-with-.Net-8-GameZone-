


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
        public async Task Create(CreateGameFormVM model)
        {

            var CoverName=$"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";

            var path = Path.Combine(_imagesPath,CoverName);

            //Save Cover in Server
            using var stream = File.Create(path);
            await model.Cover.CopyToAsync(stream);

            //  Save game in database

            Game game = new()
            {
                Name= model.Name,
                Description=model.Description,
                CategoryId=model.CategoryId,
                Cover=CoverName,
                Devices=model.SelectedDevices.Select(d=> new GameDevice { DeviceId =d}).ToList()
            };
            _context.Games.Add(game);
            _context.SaveChanges();
        }
    }
}
