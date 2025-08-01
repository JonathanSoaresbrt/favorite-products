using Favorite.Products.API.Constants;
using Favorite.Products.API.Dtos.Response;
using Favorite.Products.Application.Interfaces.Services;
using Favorite.Products.Domain.Models.Entities;
using Favorite.Products.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Favorite.Products.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/customers")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(
            ILogger<CustomerController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();

            var response = customers.Select(c => new CustomerResponse
            {
                Name = c.Name,
                Email = c.Email.Value
            });

            return Ok(response);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound(new { error = ApiConstants.CustumerNotFound, statusCode = 404 });

            var response = new CustomerResponse
            {
                Name = customer.Name,
                Email = customer.Email.Value
            };

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CustomerRequest request)
        {
            try
            {
                request.Validate();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message, statusCode = 400 });
            }

            var customer = new Customer(
                name: request.Name,
                email: new Email(request.Email),
                dateCreated: DateTime.UtcNow,
                dateUpdated: DateTime.UtcNow);

            await _customerService.AddAsync(customer);

            var response = new CustomerResponse
            {
                Name = customer.Name,
                Email = customer.Email.Value
            };

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, response);
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long id, [FromBody] CustomerRequest request)
        {
            
            request.Validate();

            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound(new { error = ApiConstants.CustumerNotFound, statusCode = 404 });

            customer.Update(request.Name, request.Email);
            
            await _customerService.UpdateAsync(customer);

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _customerService.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new { error = ApiConstants.CustumerNotFound, statusCode = 404 });

            existing.Inactivate();
            await _customerService.DeleteAsync(id);

            return NoContent();
        }
    }
}
