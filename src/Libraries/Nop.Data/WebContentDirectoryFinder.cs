using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Data
{
    public static class WebContentDirectoryFinder
    {
        public static string CalculateContentRootFolder()
        {
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(AppContext.BaseDirectory);
            if (coreAssemblyDirectoryPath == null)
            {
                throw new Exception("Could not find location of MultipleDbContextEfCoreDemo.Core assembly!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "NopCommerce.sln"))
            {
                if (directoryInfo.Parent == null)
                {
                    throw new Exception("Could not find content root folder!");
                }

                directoryInfo = directoryInfo.Parent;
            }
            //throw new Exception("Testing!" + directoryInfo.FullName + $"{Path.DirectorySeparatorChar}Nop.Web");
            return directoryInfo.FullName + $"{Path.DirectorySeparatorChar}Presentation{Path.DirectorySeparatorChar}Nop.Web";
        }

        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
