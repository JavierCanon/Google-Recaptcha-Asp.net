using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;


namespace StructRobotBase
{
    class RobotBase
    {
        public string Navigate(string url)
        {
            //SEND YOUR URL
            //Example: string target = "https://pje.tjdft.jus.br/consultapublica/ConsultaPublica/listView.seam";
            string target = "";

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(target);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            return responseFromServer;
        }
        public void StarterBrowser()
        {
            //SEND YOUR URL
            //Example: string target = "https://pje.tjdft.jus.br/consultapublica/ConsultaPublica/listView.seam";
            string target = "";

            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }
    }
}
