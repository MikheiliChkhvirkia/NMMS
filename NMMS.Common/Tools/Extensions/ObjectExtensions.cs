using NMMS.Common.Exceptions;

namespace NMMS.Common.Tools.Extensions
{
    public static class ObjectExtensions
    {
        public static void EnsureNotNull<T>(this T @object, object? objectId = null)
        {
            if (@object == null)
            {
                throw new ObjectNotFoundException(typeof(T).Name, objectId);
            }
        }
    }
}
