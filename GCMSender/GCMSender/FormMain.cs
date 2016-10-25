using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCMSender
{
    /// <summary>
    /// GCM Sender Application
    /// 
    /// </summary>

    public partial class FormMain : Form
    {
        private const string API_URL = "https://android.googleapis.com/gcm/send";
        private const string API_KEY = "YOUR_API_KEY"; // console.developers.google.com'dan API's & auth \ Get Public API Access key from console.developer.google.com\API's & auth\Credentials

        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string json = "{ \"data\": { \"title\": \"" + textBoxTitle.Text +  "\", \"message\": \"" + textBoxMessage.Text + "\" }, \"registration_ids\": [\"" + textBoxDeviceId.Text + "\"] }";

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(API_URL);
            webRequest.ContentType = "application/json";
            webRequest.Headers.Add("Authorization", "key=" + API_KEY);
            webRequest.Method = "POST";

            byte[] data = Encoding.UTF8.GetBytes(json);
            webRequest.ContentLength = data.Length;

            Stream stream = webRequest.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Flush();

            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string content = streamReader.ReadToEnd();
            streamReader.Close();
        }
    }
}
