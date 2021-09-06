using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Constants;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [Authorize(Policy = PolicyNames.Authorized)]
    [ApiController]
    [Route("orders/{orderId}/seats")]
    public class SeatOrdersController : ControllerBase
    {
        private readonly SeatOrderService _seatOrderService;

        public SeatOrdersController(SeatOrderService seatOrderService)
        {
            _seatOrderService = seatOrderService;
        }

        [HttpGet("{seatId}")]
        public async Task<IActionResult> Get(int seatId, int orderId)
        {
            SeatOrderModel? model = await _seatOrderService.GetByAsync(seatId, orderId);
            if (model == null)
            {
                return NotFound();
            }
            SeatOrderResponse response = model.Adapt<SeatOrderResponse>();
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int orderId)
        {
            IReadOnlyCollection<SeatOrderModel> services = await _seatOrderService.GetAllByAsync(orderId);
            IReadOnlyCollection<SeatOrderResponse> response = services.Adapt<IReadOnlyCollection<SeatOrderResponse>>();
            return Ok(response);
        }
    }
}
