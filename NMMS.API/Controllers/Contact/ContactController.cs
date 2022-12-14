using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMMS.Application.CQRSContact.Commands.UpdateContact;
using NMMS.Application.CQRSContact.Queries.GetContactTypes;
using NMMS.Common.API.Controllers;
using NMMS.Common.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace NMMS.API.Controllers.Contact
{
    public class ContactController : ApiControllerBase
    {
        public ContactController(IMediator mediator)
            : base(mediator) { }

        [HttpGet("GetContactTypes")]
        [SwaggerOperation("Get contactTypes")]
        public Task<List<NamedData<int>>> GetContactTypes() => mediator.Send(new GetContactTypesQuery());

        [HttpPatch("{id}")]
        [SwaggerOperation("Update contact")]
        public Task Update(int id, [FromBody] UpdateContactCommandModel request)
            => mediator.Send(new UpdateContactCommand { Id = id, Model = request });

    }
}
