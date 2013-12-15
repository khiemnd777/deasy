using System;
using System.Runtime.Serialization;

namespace Deasy.Infrastructure.DependencyManagement
{
    [Serializable]
    public class ComponentRegistrationException : Exception
    {
        public ComponentRegistrationException(string serviceName)
            : base(String.Format("Component {0} could not be found but is registered in the Aaron/engine/components section", serviceName))
        {
        }

        protected ComponentRegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
