using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSamples
{
    public class ParallelSearcher
    {
        internal static ConcurrentBag<string> filePaths = new ConcurrentBag<string>();

        internal void SearchFiles(string drive)
        {
            try
            {
                System.IO.Directory.SetCurrentDirectory(drive);

                var results = System.IO.Directory.EnumerateFiles(@"..\SomerelativePath");

                foreach (var result in results)
                {
                    filePaths.Add(result);
                }
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                // This volume does not have the folder you are looking for. Handle it.
            }
        }
    }
    class SearchCallExample
    {

        public SearchCallExample()
        {
            var logicalDrives = System.IO.Directory.GetLogicalDrives();

            Parallel.ForEach<string>(logicalDrives, (x) => { new ParallelSearcher { }.SearchFiles(x); });

            var files = ParallelSearcher.filePaths;


        }

    }
}
