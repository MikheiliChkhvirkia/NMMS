using MediatR;
using Microsoft.AspNetCore.Mvc;
using NMMS.Application.CQRSDistributor.Commands.CreateDistributor;
using NMMS.Application.CQRSDistributor.Commands.DeleteDistributor;
using NMMS.Application.CQRSDistributor.Commands.UpdateDistributor;
using NMMS.Application.CQRSDistributor.Queries.GetAllDistributors;
using NMMS.Application.CQRSDistributor.Queries.GetDistributorsId;
using NMMS.Common.API.Controllers;
using NMMS.Common.Application;
using NMMS.Common.Dto;
using NMMS.Domain.Entities.DistributorEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace NMMS.API.Controllers.Distribut
{
    public class DistributorController : ApiControllerBase
    {
        public DistributorController(IMediator mediator)
            : base(mediator) { }

        [HttpGet("GetDistributorsId")]
        [SwaggerOperation("Get distributorsId")]
        public Task<List<NamedData<int>>> Get() => mediator.Send(new GetDistributorsIdQuery());

        [HttpGet("GetAllDistributors")]
        [SwaggerOperation("Get all distributors")]
        public Task<PagedData<Distributor>> GetAllDistributors() => mediator.Send(new GetAllDistributorsQuery());

        [HttpPost]
        [SwaggerOperation("Create new distributor")]
        public Task Create([FromBody] CreateDistributorCommands request) => mediator.Send(request);

        [HttpPatch("{id}")]
        [SwaggerOperation("Update distributor")]
        public Task Update(int id, [FromBody] UpdateDistributorCommandModel request)
        => mediator.Send(new UpdateDistributorCommand { Id = id, Model = request });

        [HttpDelete]
        [SwaggerOperation("Delete distributor")]
        public Task Delete(int id) => mediator.Send(new DeleteDistributorCommand { Id = id, });

    }
}
