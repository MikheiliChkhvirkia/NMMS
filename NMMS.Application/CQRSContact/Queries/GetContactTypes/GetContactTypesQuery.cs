using MediatR;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSContact.Queries.GetContactTypes
{
    public class GetContactTypesQuery : IRequest<List<NamedData<int>>> { }
}
