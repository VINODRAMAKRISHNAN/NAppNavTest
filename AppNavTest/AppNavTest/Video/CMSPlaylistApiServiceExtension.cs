using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppNavTest
{
    class CMSPlaylistApiServiceExtension
    {
        public static async Task<HttpResponseMessage> SendGetRequestAction(string param)
        {
            HttpResponseMessage response = null;
            WebResponse webresponse = null;
            Stream stream = null;
            StreamReader streamreader = null;
            try
            {
                WebRequest client = WebRequest.Create("http://h20621.azurewebsites.net/video-gallery/api/PlaylistApi/GetRelatedPlaylists?videoid=" + param);
                client.Method = "GET";
                WebHeaderCollection headers = new WebHeaderCollection();
                Random x = new Random(10000);
                
                headers[HttpRequestHeader.Authorization] = "Bearer" + " " + x.Next().ToString();

                //headers[HttpRequestHeader.Authorization] = "key";
               
                client.Headers = headers;

                client.ContentType = "application/json; charset=utf-8";

                webresponse = await client.GetResponseAsync();
              
                if (webresponse == null) return null;
               
                stream = webresponse.GetResponseStream();
                if (stream == null) return null;

                streamreader = new StreamReader(stream, Encoding.UTF8);
                var json = streamreader.ReadToEnd();

                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            }
            catch (Exception ex)
            {
                
                if (ex != null && ex.Message != null && ex.Message.Contains("404"))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

            }
            finally
            {
                // try { if (streamreader != null) { streamreader.Close(); } } catch (Exception ex) { }
                try { if (streamreader != null) { ((IDisposable)streamreader).Dispose(); } } catch (Exception ex) { }

                //try { if (stream != null) { stream.Close(); } } catch (Exception ex) { }
                try { if (stream != null) { ((IDisposable)stream).Dispose(); } } catch (Exception ex) { }

                //try { if (webresponse != null) { webresponse.Close(); } } catch (Exception ex) { }
                try { if (webresponse != null) { ((IDisposable)webresponse).Dispose(); } } catch (Exception ex) { }

            }
            return response;

        }


        public static async Task<HttpResponseMessage> SendGetRequestMediaAPIAction(string param)
        {
            HttpResponseMessage response = null;
            WebResponse webresponse = null;
            Stream stream = null;
            StreamReader streamreader = null;
            try
            {
                WebRequest client = WebRequest.Create("http://api.brightcove.com/services/library?command=find_video_by_id&video_id="+ param + "&video_fields=FLVURL&media_delivery=http&token=lZlQqFoaeD7s8HzOOWLke2tcrXeViZL6UdaHbJynnX4iK7Ghawjyjw..");
                client.Method = "GET";
                WebHeaderCollection headers = new WebHeaderCollection();
                headers[HttpRequestHeader.Authorization] = "key";

                client.Headers = headers;

                client.ContentType = "application/json; charset=utf-8";

                webresponse = await client.GetResponseAsync();

                if (webresponse == null) return null;

                stream = webresponse.GetResponseStream();
                if (stream == null) return null;

                streamreader = new StreamReader(stream, Encoding.UTF8);
                var json = streamreader.ReadToEnd();

                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            }
            catch (Exception ex)
            {

                if (ex != null && ex.Message != null && ex.Message.Contains("404"))
                {
                    response = new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                else
                {
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

            }
            finally
            {
                // try { if (streamreader != null) { streamreader.Close(); } } catch (Exception ex) { }
                try { if (streamreader != null) { ((IDisposable)streamreader).Dispose(); } } catch (Exception ex) { }

                //try { if (stream != null) { stream.Close(); } } catch (Exception ex) { }
                try { if (stream != null) { ((IDisposable)stream).Dispose(); } } catch (Exception ex) { }

                //try { if (webresponse != null) { webresponse.Close(); } } catch (Exception ex) { }
                try { if (webresponse != null) { ((IDisposable)webresponse).Dispose(); } } catch (Exception ex) { }

            }
            return response;

        }
    }
}
