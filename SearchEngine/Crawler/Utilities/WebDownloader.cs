using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crawler.Utilities
{
    public class WebDownloader : IDisposable
    {
        private WebClient client;
        private WebBrowser browser;
        private int eventCounter;

        public WebDownloader()
        {
            InitialWebBrowser();
            InitialWebClient();
        }
        public void InitialWebBrowser()
        {
            browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = false;
            browser.NewWindow += new System.ComponentModel.CancelEventHandler(NewWindow);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentCompleted);
            browser.Navigating += new WebBrowserNavigatingEventHandler(Navigating);
            eventCounter = 0;
        }

        public void InitialWebClient()
        {
            client = new WebClient();
        }
        public void NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            eventCounter++;
        }
        public void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            eventCounter++;
        }
        public void Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            eventCounter++;
        }

        public String Download(String url)
        {
            return client.DownloadString(url);
        }

        public String DownloadAjaxPage(String url)
        {
            if (browser.IsBusy)
            {
                browser.Dispose();
                InitialWebBrowser();
            }
            browser.Navigate(url);

            while (browser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
                //Console.WriteLine("eventCounter " + eventCounter);
            }

            while (eventCounter < 8)
            {
                Application.DoEvents();
                //Console.WriteLine("eventCounter " + eventCounter);
            }
            mshtml.HTMLDocument htmldocument = (mshtml.HTMLDocument)browser.Document.DomDocument;
            //String gethtml = wb.DocumentText;
            String content = htmldocument.documentElement.outerHTML;

            return content;
        }
        public void Dispose()
        {
            browser.Dispose();
        }
    }
}
