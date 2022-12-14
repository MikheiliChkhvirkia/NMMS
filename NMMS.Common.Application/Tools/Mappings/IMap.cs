using AutoMapper;

namespace NMMS.Common.Application.Tools.Mappings
{
    public interface IMap<T>
    {
        void Mapping(Profile profile);
    }
}
