using EShop.Domain.Domain;
using EShop.Domain.DTO;
using EShop.Domain.Identity;
using EShop.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie_App.Service.Interface;

namespace EShop.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IConcertService _concertService;

        public AdminController(IOrderService orderService, IConcertService concertService)
        {
            _orderService = orderService;
            _concertService = concertService;
        }
        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return this._orderService.GetAllOrders();
        }
        [HttpPost("[action]")]
        public Order GetDetailsForOrder(BaseEntity id)
        {
            return this._orderService.GetDetailsForOrder(id);
        }
        [HttpPost("[action]")]
        public bool ImportAllConcerts(List<ConcertDTO> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                
                    var concert = new Concert
                    {
                        ConcertName = item.ConcertName,
                        ConcertDescription = item.ConcertDescription,
                        ConcertImage = item.ConcertImage,
                        Rating = item.Rating
                    };

                _concertService.CreateNewConcert(concert);
              
            }
            return status;
        }

    }
}
