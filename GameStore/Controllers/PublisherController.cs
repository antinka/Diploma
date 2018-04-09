using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.ViewModels;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            _publisherService = publisherService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(PublisherViewModel publisher)
        {
            if (ModelState.IsValid)
            {
                _publisherService.AddNew(_mapper.Map<PublisherDTO>(publisher));

                return RedirectToAction("Get", new { companyName = publisher.Name });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Get(string companyName)
        {
            var publisher = _publisherService.Get(companyName);

            return View(_mapper.Map<PublisherViewModel>(publisher));
        }
    }
}