using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlumSak_ShaderCountViewer_Plugin.Core
{
    internal class Shader
    {
        public static string GetShaderCount(string srcFile)
        {
            string tocPath = srcFile;

            if (File.Exists(tocPath))
            {
                FileInfo rileInfo = new FileInfo(tocPath);
                long fileSize = rileInfo.Length;
                long shaderCount = Math.Max(+((fileSize - 32) / 8), 0);

                return shaderCount.ToString();
            }

            return "0";
        }
    }
}