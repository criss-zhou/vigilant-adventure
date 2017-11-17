using Crawler.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            String webUrl = "http://www.trademe.co.nz";
            var downloader = new WebDownloader();
            var s1 = downloader.Download(webUrl);
            var s2 = downloader.DownloadAjaxPage(webUrl);
            Console.WriteLine(s1.Length == s2.Length);
        }
    }
}
