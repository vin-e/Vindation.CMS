using Microsoft.Owin;

namespace Vin.Core.MultiTenancy
{
    /// <summary>
    /// Defines a strategy for obtaining a tenant identifier from the <paramref name="request"/>.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <returns></returns>
    public delegate string RequestIdentificationStrategy(IOwinRequest request);
}
