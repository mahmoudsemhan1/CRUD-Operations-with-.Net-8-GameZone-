
namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly IcategoriesService _categoriesService;
        private readonly IDeviceServices _deviceServices;
        private readonly IGamesService _gamesService;
        public GamesController(IcategoriesService categoriesService, IDeviceServices deviceServices,
            IGamesService gamesService)
        {

            _categoriesService = categoriesService;
            _deviceServices = deviceServices;
            _gamesService = gamesService;
        }


        public IActionResult Index()
        {
            var games = _gamesService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gamesService.GetById(id);
            if (game == null) return NotFound();
            return View(game);
        }


        [HttpGet]
        public IActionResult Create()
        {

            CreateGameFormVM viewmodel = new()
            {
                Categories = _categoriesService.GetCategories(),
                Devices = _deviceServices.GetDevices(),

            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // = > for more security 
        public async Task<IActionResult> Create(CreateGameFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetCategories();
                model.Devices = _deviceServices.GetDevices();

                return View(model);
            }
            //1-Save Game ,2-save cover to server
            await _gamesService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Delete(int id)
        // {
        //     var game= _gamesService.GetById(id);
        //     if (game == null) return View("NotFound");

        //     await _gamesService.Delete(id);
        //     return RedirectToAction(nameof(Index));
        // }


    }
}
