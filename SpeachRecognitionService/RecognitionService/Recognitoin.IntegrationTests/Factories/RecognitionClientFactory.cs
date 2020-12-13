using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Recognitoin.IntegrationTests.Factories
{
	public class RecognitionClientFactory
	{
        public static Process ServeApplication()
        {
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var iisProcess = new Process();
            var applicationPath = GetApplicationPath();

            iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, 8080);
            iisProcess.Start();

            return iisProcess;
        }

        private static string GetApplicationPath()
        {
            return Path.GetFullPath(
                Path.Combine(
                    GetCurrentDirectory(),
                    "../../../Recognition.Web"
                )
            );
        }

        private static string GetCurrentDirectory()
        {
            return Path.GetDirectoryName(
                Uri.UnescapeDataString(
                    new UriBuilder(
                        Assembly.GetExecutingAssembly().CodeBase
                    ).Path
                )
            );
        }
    }
}
