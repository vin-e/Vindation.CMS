using System;

namespace Vin.Core.MultiTenancy
{
    public class RequestIdentificationStrategies
    {
        public static RequestIdentificationStrategy FromHostname
        {
            get
            {
                return req => req.Uri.Host.ToLowerInvariant();
            }
        }

        public static RequestIdentificationStrategy FromUrlFragment
        {
            get
            {
                return req => req.Uri.AbsolutePath.ToLowerInvariant();
            }
        }

        public static RequestIdentificationStrategy FromSubDomain
        {
            get
            {
                return req =>
                {
                    if (req.Uri.HostNameType == UriHostNameType.Dns)
                    {
                        var host = req.Uri.Host.ToLowerInvariant();
                        if (host.Split('.').Length > 2)
                        {
                            int lastIndex = host.LastIndexOf(".");
                            int index = host.LastIndexOf(".", lastIndex - 1);
                            return host.Substring(0, index);
                        }
                    }

                    return null;
                };
            }
        }
    }
}