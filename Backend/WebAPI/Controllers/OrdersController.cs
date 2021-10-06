using BusinessLogic.Models;
using BusinessLogic.Parameters;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Constants;
using WebAPI.Extensions;
using WebAPI.Parameters;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [Authorize(Policy = PolicyNames.Authorized)]
    [ApiController]
    [Route("/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            OrderModel? model = await _orderService.GetByAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost("sum")]
        public async Task<IActionResult> CalculateSum([FromBody] OrderRequest request)
        {
            int? id = HttpContext.User.Identity.GetUserId();
            if (id == null)
            {
                return Unauthorized();
            }
            OrderModel model = request.Adapt<OrderModel>();
            model.UserId = id.Value;
            PriceModel price = await _orderService.GetSumAsync(model);
            return Ok(price.Adapt<PriceResponse>());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OrderRequestSearchParameters parameters)
        {
            int? userId = HttpContext.User.Identity.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            IReadOnlyCollection<OrderModel> orders = await _orderService
                .GetAllByAsync(
                    userId.Value,
                    parameters.Adapt<OrderModelSearchParameters>()
                );
            IReadOnlyCollection<OrderResponse> response = orders.Adapt<IReadOnlyCollection<OrderResponse>>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            int? id = HttpContext.User.Identity.GetUserId();
            if (id == null)
            {
                return Unauthorized();
            }
            OrderModel model = request.Adapt<OrderModel>();
            model.UserId = id.Value;
            await _orderService.CreateAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OrderRequest request)
        {
            OrderModel model = request.Adapt<OrderModel>();
            model.Id = id;
            await _orderService.EditAsync(model);
            return Ok();
        }
    }
}
