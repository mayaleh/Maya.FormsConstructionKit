using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Spa.Library.Helper
{
    public static class RouteHelper
    {
        private const string PathSeparator = "/";

        public static string ComposePath(params string[] paths)
        {
            if (paths.Length == 0)
            {
                return string.Empty;
            }

            return paths[0].StartsWith(PathSeparator)
                ? string.Join(PathSeparator, paths)
                : PathSeparator + string.Join(PathSeparator, paths);
        }
    }
}
