
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game=_gamesService.GetById(id);
            if (game == null) return NotFound();
            EditGameFormVM ViewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId,
                SelectedDevices=game.Devices.Select(d=>d.DeviceId).ToList(),
                Categories=_categoriesService.GetCategories(),
                Devices=_deviceServices.GetDevices(),
                CurrentCover=game.Cover,
            };

            return View(ViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // = > for more security 
        public async Task<IActionResult> Edit(EditGameFormVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetCategories();
                model.Devices = _deviceServices.GetDevices();

                return View(model);
            }
           
            var game = await _gamesService.Update(model);
            if(game == null) return BadRequest();

            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isdeleted = _gamesService.Delete(id);

            return isdeleted ? Ok() : BadRequest();

        }
      
    }
  
    
}
