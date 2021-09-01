using BusinessLogic.Models;
using BusinessLogic.Parameters;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Constants;
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

        [HttpPost]
        public async Task<IActionResult> CalculateSum([FromBody] OrderRequest request)
        {
            PriceModel price = await _orderService.GetSumAsync(request.Adapt<OrderModel>());
            return Ok(price);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(OrderRequestSearchParameters parameters)
        {
            IReadOnlyCollection<OrderModel> services = await _orderService
                .GetAllByAsync(
                    parameters.Adapt<OrderModelSearchParameters>()
                );
            IReadOnlyCollection<OrderResponse> response = services.Adapt<IReadOnlyCollection<OrderResponse>>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            OrderModel model = request.Adapt<OrderModel>();
            PriceModel price = await _orderService.GetSumAsync(request.Adapt<OrderModel>());
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
