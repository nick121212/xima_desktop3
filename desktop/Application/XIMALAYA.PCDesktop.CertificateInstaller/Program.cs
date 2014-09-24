using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XIMALAYA.PCDesktop.CertificateInstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                {
                    WebClient webclient = new WebClient();
                    byte[] certificatefile = webclient.DownloadData(args[0]);
                    if (certificatefile.Length > 0)
                    {
                        InstallCertificate(StoreName.AuthRoot, certificatefile);
                        InstallCertificate(StoreName.TrustedPublisher, certificatefile);
                    }
                    else
                    {
                        ShowErrorMessage("Can't download the certificate file.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        static void InstallCertificate(StoreName storageName, byte[] certificatefile)
        {
            X509Certificate2 certificate = new X509Certificate2(certificatefile);
            X509Store store = new X509Store(storageName, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadWrite);
            store.Remove(certificate);
            store.Add(certificate);
            store.Close();
        }

        static void ShowErrorMessage(string strErrorMessage)
        {
            MessageBox.Show(strErrorMessage, "Certificate Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
