using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMMS.Application.CQRSAddress.Commands.UpdateAddress;
using NMMS.Application.CQRSAddress.Queries.GetAddressTypes;
using NMMS.Common.API.Controllers;
using NMMS.Common.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace NMMS.API.Controllers.Addres
{
    public class AddressController : ApiControllerBase
    {
        public AddressController(IMediator mediator)
            : base(mediator) { }

        [HttpGet("GetaddressTypes")]
        [SwaggerOperation("Get addressTypes")]
        public Task<List<NamedData<int>>> GetAddressTypes() => mediator.Send(new GetAddressTypesQuery());

        [HttpPatch("{id}")]
        [SwaggerOperation("Update distributors address")]
        public Task Update(int id, [FromBody] UpdateDistributorsCommandModel request)
            => mediator.Send(new UpdateAddressCommand { Id = id, Model = request });
    }
}
