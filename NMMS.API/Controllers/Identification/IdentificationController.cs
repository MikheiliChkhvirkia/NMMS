using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMMS.Application.CQRSIdentification.Commands.UpdateIdentification;
using NMMS.Application.CQRSIdentification.Queries.GetDocumentTypes;
using NMMS.Common.API.Controllers;
using NMMS.Common.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace NMMS.API.Controllers.Identification
{
    public class IdentificationController : ApiControllerBase
    {
        public IdentificationController(IMediator mediator)
            : base(mediator) { }

        [HttpGet("GetDocumentTypes")]
        [SwaggerOperation("Get documentTypes")]
        public Task<List<NamedData<int>>> Get() => mediator.Send(new GetDocumentTypesQuery());

        [HttpPatch("UpdateIdentification")]
        [SwaggerOperation("Update identification")]
        public Task Update(int id, [FromBody] UpdateIdentificationCommandModel request)
             => mediator.Send(new UpdateIdentificationCommand { Id = id, Model = request });
    }
}
