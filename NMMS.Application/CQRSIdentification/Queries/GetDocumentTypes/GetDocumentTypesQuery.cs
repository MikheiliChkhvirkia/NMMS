using MediatR;
using NMMS.Common.Dto;

namespace NMMS.Application.CQRSIdentification.Queries.GetDocumentTypes
{
    public class GetDocumentTypesQuery : IRequest<List<NamedData<int>>> { }
}
