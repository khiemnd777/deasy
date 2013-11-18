using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Deasy
{
    public static class CommonHelper
    {
        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (var obj in array)
                action.Invoke(obj);
        }

        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            ForEach<T>(list.ToArray(), action);
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            ForEach<T>(list.ToArray(), action);
        }

        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }

        private static AspNetHostingPermissionLevel? _trustLevel = null;

        /// <summary>
        /// Finds the trust level of the running application (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
        /// </summary>
        /// <returns>The current trust level.</returns>
        public static AspNetHostingPermissionLevel GetTrustLevel()
        {
            if (!_trustLevel.HasValue)
            {
                //set minimum
                _trustLevel = AspNetHostingPermissionLevel.None;

                //determine maximum
                foreach (AspNetHostingPermissionLevel trustLevel in
                        new AspNetHostingPermissionLevel[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal 
                            })
                {
                    try
                    {
                        new AspNetHostingPermission(trustLevel).Demand();
                        _trustLevel = trustLevel;
                        break; //we've set the highest permission we can
                    }
                    catch (System.Security.SecurityException)
                    {
                        continue;
                    }
                }
            }
            return _trustLevel.Value;
        }
    }
}
