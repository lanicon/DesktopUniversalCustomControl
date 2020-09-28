using System.IO;

namespace DesktopUniversalCustomControl.ExposedMethod
{
    /// <summary>
    /// 路径方法
    /// </summary>
    public class GetPathHelper
    {
        public GetPathHelper()
        {

        }

        /// <summary>
        /// 得到bin文件所在父目录
        /// </summary>
        /// <returns></returns>
        public static string GetBinParentPath()
        {
            string path = Directory.GetCurrentDirectory();
            path = Directory.GetParent(path).Parent.FullName;
            return path;
        }
    }
}
