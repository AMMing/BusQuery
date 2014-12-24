using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BusQuery.Core
{
    public class HttpProcessor
    {
        private IAsyncResult asyncResult;
        private int cmdType;
        private HttpWebRequest httpWebRequest;
        private bool needStop;
        private byte[] responseBytes;
        private ISendContent sendContent;
        private string serverUrl;

        public event GetRequestCompletedEvent RequestCompleted;

        public event RequestFaildEvent RequestFaild;

        public HttpProcessor()
        {
        }

        public HttpProcessor(int cmdType, ISendContent sendContent)
        {
            //this.serverUrl = SettingService.GetInstance().GetSettings().ServiceUrl;
            this.serverUrl = "http://mybus.xiamentd.com:8081/XMMyGoWeb/servlet/MyGoServer.HttpPool.HttpHandlerServlet";
            this.cmdType = cmdType;
            this.sendContent = sendContent;
        }

        public HttpProcessor(string serverURL, int cmdType, ISendContent sendContent)
        {
            this.serverUrl = serverURL;
            this.cmdType = cmdType;
            this.sendContent = sendContent;
        }

        public void BeginRequest()
        {
            this.needStop = false;
            this.httpWebRequest = WebRequest.Create(new Uri(this.serverUrl)) as HttpWebRequest;
            this.httpWebRequest.Method = "POST";
            this.httpWebRequest.ContentType = "application/octet-stream";
            this.httpWebRequest.Accept = "application/octet-stream";
            this.httpWebRequest.Headers["version"] = "22-Jun-2001/11:30:00-CST";
            //this.httpWebRequest.get_Headers()["mybus-phoneid"] = SettingService.GetInstance().GetSettings().PhoneNumber;
            //this.httpWebRequest.get_Headers()["handset-type"] = DeviceExtendedProperties.GetValue("DeviceName").ToString();

            this.httpWebRequest.BeginGetRequestStream(new AsyncCallback(this.RequestStreamCallback), this.httpWebRequest);
        }

        private void RequestStreamCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                Stream stream = ((HttpWebRequest)asynchronousResult.AsyncState).EndGetRequestStream(asynchronousResult);
                BinaryStream stream2 = new BinaryStream();
                stream2.writeInt(this.cmdType);
                if (this.sendContent != null)
                {
                    byte[] buffer = this.sendContent.serialize();
                    stream2.writeInt(buffer.Length);
                    if (buffer.Length > 0)
                    {
                        stream2.writeBytes(buffer);
                    }
                    else
                    {
                        stream2.writeInt(0);
                    }
                }
                stream.Write(stream2.toBytes(), 0, stream2.Length);
                stream2.close();
                stream.Close();
                this.asyncResult = this.httpWebRequest.BeginGetResponse(new AsyncCallback(this.ResponseCallback), this.httpWebRequest);
            }
            catch (Exception)
            {
            }
        }

        private void ResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest asyncState = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)asyncState.EndGetResponse(asynchronousResult);
                if (response.StatusCode == ((HttpStatusCode)((int)HttpStatusCode.OK)))
                {
                    BinaryStream stream = new BinaryStream(response.GetResponseStream());
                    if ((stream.readInt() == 1) && (stream.readInt() > 0))
                    {
                        int count = stream.readInt();
                        if (count > 0)
                        {
                            this.responseBytes = new byte[count];
                            this.responseBytes = stream.readBytes(count);
                            if (!this.needStop)
                            {
                                this.RequestCompleted(this.responseBytes);
                            }
                        }
                        else if (!this.needStop)
                        {
                            this.RequestCompleted(null);
                        }
                    }
                    stream.close();
                }
            }
            catch (Exception)
            {
                if (!this.needStop)
                {
                    this.RequestFaild();
                }
            }
        }

        public void StopRequest()
        {
            this.needStop = true;
        }
    }
}
