 

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly IcategoriesService _categoriesService;
        private readonly IDeviceServices _deviceServices;
        public GamesController(IcategoriesService categoriesService, IDeviceServices deviceServices)
        {

            _categoriesService = categoriesService;
            _deviceServices = deviceServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            CreateGameFormVM viewmodel = new()
            {
                Categories= _categoriesService.GetCategories(),
                Devices= _deviceServices.GetDevices(),

            }; 
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // = > for more security 
        public IActionResult Create(CreateGameFormVM model)
        {
            if(!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetCategories();
                model.Devices = _deviceServices.GetDevices();

                return View(model);
            }
            //1-Save Game ,2-save cover to server


            return RedirectToAction(nameof(Index));
        }
    }
}
