using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppNavTest
{
    public class CMSApiServiceExtension
    {
        private static string NewReadToken = "4119874060001";
        private static string AnalyticTokenUri = "https://oauth.brightcove.com";
        private static string AnalyticTokenContentType = "application/x-www-form-urlencoded";
        private static string AnalyticTokenAuthType = "Basic";
        private static string AnalyticApiAuth = "NzA2MmNmNzEtZDIzOS00YTQ3LTlhNjUtZTE0N2FkNmI4YTE3OjA2U0ZZNlYwQ0Y0eGd3MWxNWGFELWxleGxZNkUxSW5xSlRKZHk0TFZKUWFETldBN3Y2WElzR1RFSHhIN19nQkYzU3pSTlhrWlFhbHNQRXJhN0FBekpB";
        private static string AnalyticTokenRequestUri = "/v3/access_token?grant_type=client_credentials";

        private static string AnalyticApiUri = "https://analytics.api.brightcove.com";
        private static string AnalyticApiRequestUriTemplate = "/v1/alltime/accounts/{0}/videos/{1}";
        private static string TokenTypeKey = "token_type";
        private static string AnalyticApiContentType = "application/json";
        private static string AccessTokenKey = "access_token";
        private static string AllTimeVideoViews = "alltime_video_views";
    


        private static string CMSApiUri = "https://cms.api.brightcove.com";

        private static AuthDetails LastAuthDetails = null;

        #region public methods

        public static async Task<Dictionary<string, string>> GetAuthToken()
        {
            if (!IsNewTokenRequired())
            {
                return LastAuthDetails.LastAuthTokenResponse;
            }
            DateTime currtime = DateTime.UtcNow;
            string accountid = NewReadToken;
            try
            {
                return await ExecuteAuthToken(currtime);
            }
            catch (Exception ex)
            {
                //execute it once again 
                try
                {
                    currtime = DateTime.UtcNow;
                    return await ExecuteAuthToken(currtime);
                }
                catch (Exception exx)
                {
                    //execute it once again and give up
                    try
                    {
                        currtime = DateTime.UtcNow;
                        return await ExecuteAuthToken(currtime);
                    }
                    catch (Exception exxx)
                    {
                        return new Dictionary<string, string>();
                    }
                }
            }

        }
        private static async Task<Dictionary<string, string>> ExecuteAuthToken(DateTime currtime)
        {
            var tokenResponse = await SendPostRequest(AnalyticTokenUri, AnalyticTokenContentType, AnalyticTokenAuthType, AnalyticApiAuth, AnalyticTokenRequestUri);
            var tokenDict = DeserializeJsonStringToDictiionary(tokenResponse);
            SetNewAuthTokenDetails(tokenDict, currtime);
            return tokenDict;
        }

        private static bool IsNewTokenRequired()
        {
            bool retval = false;
            if (LastAuthDetails == null)
            {
                return true;
            }
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int secondsSinceEpoch = (int)t.TotalSeconds;

            if ((secondsSinceEpoch - LastAuthDetails.LastAccessTimeInSeconds) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void SetNewAuthTokenDetails(Dictionary<string, string> tokenDict, DateTime currenttime)
        {
            try
            {


                if (tokenDict == null || (tokenDict != null && tokenDict.Count == 0))
                {
                    LastAuthDetails = null;
                    return;
                }

                if (LastAuthDetails == null)
                {
                    LastAuthDetails = new AuthDetails();
                }

                long expires;
                bool tresult = Int64.TryParse(tokenDict["expires_in"], out expires);

                TimeSpan t = currenttime - new DateTime(1970, 1, 1);
                long secondsSinceEpoch = (long)t.TotalSeconds + expires - 8; //reduce 8 seconds 

                LastAuthDetails.LastAccessTimeInSeconds = secondsSinceEpoch;
                LastAuthDetails.LastAuthTokenResponse = tokenDict;
            }
            catch (Exception ex)
            {
                //log.Error("Unexpected error in SetNewAuthTokenDetails  :" + ex.Message, ex);
                LastAuthDetails = null;
            }
        }



        public static async Task<CMSItemCollection<CMSApiVideo>> SetVideoViewCountForListofVideosFromApi1(CMSItemCollection<CMSApiVideo> vdos1)
        {
            CMSApiVideo vdo = null;
            CMSItemCollection<CMSApiVideo> vdos = null;
            try
            {
                vdos = vdos1;
                string accountid = NewReadToken;
                string basereluriforvideocountresult = @"/v1/data?accounts={0}&dimensions=video&where=video=={1}&from=alltime";
                string strvdoids = "";

                if (vdos != null && vdos.Count > 0)
                {

                    strvdoids = string.Join(",", vdos.Select(r => r.id));
                }
                else
                {
                    return new CMSItemCollection<CMSApiVideo>();
                }

                string finalreluriforvideocountresult = String.Format(basereluriforvideocountresult, accountid, strvdoids);

                var tokenDict = await GetAuthToken();

                var apiResponse = await SendGetRequest(AnalyticApiUri, AnalyticApiContentType, tokenDict[TokenTypeKey], tokenDict[AccessTokenKey], finalreluriforvideocountresult);

                var vdoidscount = DeserializeJsonStringToAnalyticsViewCount(apiResponse);

                if (vdoidscount != null && vdoidscount.items != null && vdoidscount.items.Count > 0)
                {
                    foreach (var item in vdos)
                    {
                        var countitem = vdoidscount.items.Where(s => s.video == item.id.ToString()).FirstOrDefault();
                        if (countitem != null)
                        {
                            item.PlaysTotal = countitem.video_view == null ? 0 : (long)countitem.video_view;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ///log.Error("Unexpected error in SetVideoViewCountForListofVideosFromApi:" + ex.Message, ex);
                //swallow exception
            }
            return vdos;
        }



        public static async void SetVideoViewCountForListofVideosFromApi( CMSItemCollection<CMSApiVideo> vdos)
        {
            CMSApiVideo vdo = null;
            try
            {
                string accountid = NewReadToken;
                string basereluriforvideocountresult = @"/v1/data?accounts={0}&dimensions=video&where=video=={1}&from=alltime";
                string strvdoids = "";

                if (vdos != null && vdos.Count > 0)
                {

                    strvdoids = string.Join(",", vdos.Select(r => r.id));
                }
                else
                {
                    return;
                }

                string finalreluriforvideocountresult = String.Format(basereluriforvideocountresult, accountid, strvdoids);

                var tokenDict = await GetAuthToken();

                var apiResponse = await SendGetRequest(AnalyticApiUri, AnalyticApiContentType, tokenDict[TokenTypeKey], tokenDict[AccessTokenKey], finalreluriforvideocountresult);

                var vdoidscount = DeserializeJsonStringToAnalyticsViewCount(apiResponse);

                if (vdoidscount != null && vdoidscount.items != null && vdoidscount.items.Count > 0)
                {
                    foreach (var item in vdos)
                    {
                        var countitem = vdoidscount.items.Where(s => s.video == item.id.ToString()).FirstOrDefault();
                        if (countitem != null)
                        {
                            item.PlaysTotal = countitem.video_view == null ? 0 : (long)countitem.video_view;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ///log.Error("Unexpected error in SetVideoViewCountForListofVideosFromApi:" + ex.Message, ex);
                //swallow exception
            }
            return;
        }

        private static string PrepareSearchVideosURLForFeaturedVideos(List<FieldValuePair> allField, int pageSize, int pageNum, CMSSortBy sortBy, SortOrder sortOrder, string relativeURI)
        {

            string finalurl = relativeURI + "?q=tags:feature,featured+";
            string allmatch = "";  //include all items that are exactly matching by using double quote + %2B
            string anymatch = ""; //include any items that are  matching by NOT using double quote + %2B
            string nonematch = ""; // exclude the item from the result using double quote + %2D
            string accountid = NewReadToken;
            int cnt = 0;
            if (allField != null && allField.Count > 0)
            {
                foreach (var item in allField)
                {
                    if (!(string.IsNullOrEmpty(item.Field) || string.IsNullOrEmpty(item.Value)))
                    {
                        if (cnt == 0)
                        {
                            allmatch = "%2B" + (item.Field == "tag" ? "tags" : item.Field) + ":" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                        }
                        else
                        {
                            allmatch = allmatch + "+" + "%2B" + (item.Field == "tag" ? "tags" : item.Field) + ":" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                        }
                        cnt = cnt + 1;
                    }
                }

            }


            if (allmatch.Length > 0 || anymatch.Length > 0 || nonematch.Length > 0)
            {

                if (allmatch.Length > 0)
                {
                    finalurl = finalurl + allmatch;
                }
            }

            if (sortOrder == SortOrder.Ascending)
            {
                finalurl = finalurl + "&sort=";
            }
            else
            {
                finalurl = finalurl + "&sort=-";
            }
            finalurl = finalurl + sortBy;

            finalurl = finalurl + "&limit=" + pageSize + "&offset=" + (pageNum * pageSize);
            finalurl = String.Format(finalurl, accountid);
            return finalurl;
        }

        public static async Task<CMSItemCollection<CMSApiVideo>> SearchVideos(List<FieldValuePair> allField, List<FieldValuePair> anyField, List<FieldValuePair> noneField, int pageSize, int pageNum, SortBy sortBy, SortOrder sortOrder, bool IncludePlaysTotal = false, bool IncludeVideoTotal = false)
        {
            CMSSortBy tsrtby = CMSSortBy.published_at;
            CMSItemCollection<CMSApiVideo> ret = new CMSItemCollection<CMSApiVideo>();
            switch ((int)sortBy)
            {
                case 1:
                    tsrtby = CMSSortBy.created_at;
                    break;
                case 2:
                    tsrtby = CMSSortBy.updated_at;
                    break;
                case 3:
                    tsrtby = CMSSortBy.published_at;
                    break;
                case 4:
                    tsrtby = CMSSortBy.plays_total;
                    break;
                case 5:
                    tsrtby = CMSSortBy.plays_trailing_week;
                    break;
            }


            try
            {
                string basereluriforsearchresult = @"/v1/accounts/{0}/videos/";
                string basereluriforsearchresulttotalcount = @"/v1/accounts/{0}/counts/videos/";

                string finalreluriforsearchresult = PrepareSearchVideosURL(allField, anyField, noneField, pageSize, pageNum, tsrtby, sortOrder, basereluriforsearchresult);
                string finalreluriforsearchresulttotalcount = PrepareSearchVideosURL(allField, anyField, noneField, pageSize, pageNum, tsrtby, sortOrder, basereluriforsearchresulttotalcount);


                var tokenDict = await GetAuthToken();

                var apiResponse = await SendGetRequest(CMSApiUri, AnalyticApiContentType, tokenDict[TokenTypeKey], tokenDict[AccessTokenKey], finalreluriforsearchresult);
                var videoSearchResult = DeserializeJsonStringToVdoSearchResult(apiResponse);
                if (videoSearchResult != null)
                {
                    ret = videoSearchResult;
                }

                if (IncludeVideoTotal)
                {
                    var apicountResponse = await SendGetRequest(CMSApiUri, AnalyticApiContentType, tokenDict[TokenTypeKey], tokenDict[AccessTokenKey], finalreluriforsearchresulttotalcount);
                    var videoSearchTotalCountResult = DeserializeJsonStringToDictiionary(apicountResponse);
                    if (videoSearchTotalCountResult != null && videoSearchTotalCountResult.Count > 0)
                    {
                        ret.TotalCount = ConvertToInt(videoSearchTotalCountResult["count"]);
                    }
                }


                if (ret.TotalCount == 0)
                {
                    ret.PageNumber = 0;
                    ret.PageNumber = pageSize;
                }
                else
                {
                    ret.PageNumber = pageNum;
                    ret.PageSize = pageSize;
                }

                if (IncludePlaysTotal && ret.Count > 0)
                {
                    //SetVideoViewCountForListofVideosFromFile( ret);
                    CMSItemCollection<CMSApiVideo> ret1 = await  SetVideoViewCountForListofVideosFromApi1(ret);
                    if (ret1 != null)
                        ret = ret1;
                }

            }
            catch (Exception ex)
            {
                //log.Error("Unexpected error in SearchVideos :" + ex.Message, ex);
                string s = ex.Message.ToString();
                ret.IsError = true;
            }

            return ret;
        }
        
        public static async Task<CMSItemCollection<CMSApiVideo>> SearchVideosFeatured(List<FieldValuePair> allField, List<FieldValuePair> anyField, List<FieldValuePair> noneField, int pageSize, int pageNum, SortBy sortBy, SortOrder sortOrder, bool IncludePlaysTotal = false, bool IncludeVideoTotal = false)
        {
            CMSSortBy tsrtby = CMSSortBy.published_at;
            CMSItemCollection<CMSApiVideo> ret = new CMSItemCollection<CMSApiVideo>();
            switch ((int)sortBy)
            {
                case 1:
                    tsrtby = CMSSortBy.created_at;
                    break;
                case 2:
                    tsrtby = CMSSortBy.updated_at;
                    break;
                case 3:
                    tsrtby = CMSSortBy.published_at;
                    break;
                case 4:
                    tsrtby = CMSSortBy.plays_total;
                    break;
                case 5:
                    tsrtby = CMSSortBy.plays_trailing_week;
                    break;
            }


            try
            {
                string basereluriforsearchresult = @"/v1/accounts/{0}/videos/";
                string basereluriforsearchresulttotalcount = @"/v1/accounts/{0}/counts/videos/";



                string finalreluriforsearchresult = PrepareSearchVideosURLForFeaturedVideos(allField, pageSize, pageNum, tsrtby, sortOrder, basereluriforsearchresult);
                string finalreluriforsearchresulttotalcount = PrepareSearchVideosURLForFeaturedVideos(allField, pageSize, pageNum, tsrtby, sortOrder, basereluriforsearchresulttotalcount);


                var tokenDict = GetAuthToken();

                var apiResponse = await SendGetRequest(CMSApiUri, AnalyticApiContentType, TokenTypeKey, AccessTokenKey, finalreluriforsearchresult);
                var videoSearchResult = DeserializeJsonStringToVdoSearchResult(apiResponse);
                if (videoSearchResult != null)
                {
                    ret = videoSearchResult;
                }

                if (IncludeVideoTotal)
                {
                    var apicountResponse = await SendGetRequest(CMSApiUri, AnalyticApiContentType, TokenTypeKey, AccessTokenKey, finalreluriforsearchresulttotalcount);
                    var videoSearchTotalCountResult = DeserializeJsonStringToDictiionary(apicountResponse);
                    if (videoSearchTotalCountResult != null && videoSearchTotalCountResult.Count > 0)
                    {
                        ret.TotalCount = ConvertToInt(videoSearchTotalCountResult["count"]);
                    }
                }


                if (ret.TotalCount == 0)
                {
                    ret.PageNumber = 0;
                    ret.PageNumber = pageSize;
                }
                else
                {
                    ret.PageNumber = pageNum;
                    ret.PageSize = pageSize;
                }

                if (IncludePlaysTotal && ret.Count > 0)
                {
                    //SetVideoViewCountForListofVideosFromApi(ret);
                }

            }
            catch (Exception ex)
            {
                
                string s = ex.Message.ToString();
            }

            return ret;
        }


        #endregion


        #region PRIVATE STATIC METHODS

        private static int ConvertToInt(object val)
        {
            int num = 0;
            try
            {
                if (val != null)
                {
                    bool result = Int32.TryParse(val.ToString(), out num);
                }
            }
            catch (Exception ex)
            {
                //swallow this exception
            }
            return num;
        }

        private static string PrepareSearchVideosURL(List<FieldValuePair> allField, List<FieldValuePair> anyField, List<FieldValuePair> noneField, int pageSize, int pageNum, CMSSortBy sortBy, SortOrder sortOrder, string relativeURI)
        {

            string finalurl = relativeURI + "?";
            string allmatch = "";  //include all items that are exactly matching by using double quote + %2B
            string anymatch = ""; //include any items that are  matching by NOT using double quote + %2B
            string nonematch = ""; // exclude the item from the result using double quote + %2D
            string accountid = NewReadToken;
            int cnt = 0;
            if (allField != null && allField.Count > 0)
            {
                foreach (var item in allField)
                {
                    if (!(string.IsNullOrEmpty(item.Field) || string.IsNullOrEmpty(item.Value)))
                    {
                        if (cnt == 0)
                        {
                            allmatch = "%2B" + (item.Field == "tag" ? "tags" : item.Field) + ":" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                        }
                        else
                        {
                            allmatch = allmatch + "+" + "%2B" + (item.Field == "tag" ? "tags" : item.Field) + ":" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                        }
                        cnt = cnt + 1;
                    }
                }

            }

            if (anyField != null && anyField.Count > 0)
            {
                foreach (var item in anyField)
                {
                    if (!(string.IsNullOrEmpty(item.Field) || string.IsNullOrEmpty(item.Value)))
                    {
                        if (cnt == 0)
                        {
                            anymatch = "%2B" + WebUtility.UrlEncode(item.Value);
                        }
                        else
                        {
                            anymatch = anymatch + "+" + "%2B" + WebUtility.UrlEncode(item.Value);
                        }
                        cnt = cnt + 1;
                    }
                    else
                    {
                        if (!(string.IsNullOrEmpty(item.Value)))
                        {
                            if (cnt == 0)
                            {
                                anymatch = "%2B" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                            }
                            else
                            {
                                anymatch = anymatch + "+" + "%2B" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                            }
                            cnt = cnt + 1;
                        }
                    }
                }
            }

            if (noneField != null && noneField.Count > 0)
            {
                foreach (var item in noneField)
                {
                    if (!(string.IsNullOrEmpty(item.Field) || string.IsNullOrEmpty(item.Value)))
                    {
                        if (cnt == 0)
                        {
                            nonematch = "%2D" + (item.Field == "tag" ? "tags" : item.Field) + ":" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                        }
                        else
                        {
                            nonematch = allmatch + "+" + "%2D" + (item.Field == "tag" ? "tags" : item.Field) + ":" + "\"" + WebUtility.UrlEncode(item.Value) + "\"";
                        }
                        cnt = cnt + 1;
                    }
                }
            }

            if (allmatch.Length > 0 || anymatch.Length > 0 || nonematch.Length > 0)
            {
                finalurl = finalurl + "q=";
                if (allmatch.Length > 0)
                {
                    finalurl = finalurl + allmatch;
                }
                if (anymatch.Length > 0)
                {
                    finalurl = finalurl + anymatch;
                }
                if (nonematch.Length > 0)
                {
                    finalurl = finalurl + nonematch;
                }
            }

            if (sortOrder == SortOrder.Ascending)
            {
                finalurl = finalurl + "&sort=";
            }
            else
            {
                finalurl = finalurl + "&sort=-";
            }
            finalurl = finalurl + sortBy;

            finalurl = finalurl + "&limit=" + pageSize + "&offset=" + (pageNum * pageSize);
            finalurl = String.Format(finalurl, accountid);
            return finalurl;
        }

        //private static HttpResponseMessage SendPostRequest(string uri, string contentType, string authorizationType, string authorization, string requestUri)
        //{
        //    using (var client = new HttpClient())
        //    {

        //        client.BaseAddress = new Uri(uri);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationType, authorization);

        //        var response = client.PostAsync(requestUri, null);

        //        response.Wait();

        //        return response.Result.IsSuccessStatusCode ? response.Result : null;
        //    }
        //}
        public static async Task<HttpResponseMessage> SendPostRequest(string uri, string contentType, string authorizationType, string authorization, string requestUri)
        {
            HttpResponseMessage res = await SendPostRequestAction(uri, contentType, authorizationType, authorization, requestUri, 1);
            if (res != null && res.StatusCode == HttpStatusCode.BadRequest)
            {
                res = await SendPostRequestAction(uri, contentType, authorizationType, authorization, requestUri, 2);
            }

            return res;

        }
       


        private static async Task<HttpResponseMessage>  SendPostRequestAction(string uri, string contentType, string authorizationType, string authorization, string requestUri, int executioncount)
        {
            HttpResponseMessage response = null;
            WebResponse webresponse = null;
            Stream stream = null;
            StreamReader streamreader = null;
            try
            {
              


                HttpWebRequest client = (HttpWebRequest)WebRequest.Create(uri + requestUri) as HttpWebRequest;

               
                WebHeaderCollection headers = new WebHeaderCollection();

                //headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0";
                // client.UserAgent = "Mozilla/5.0";
                headers[HttpRequestHeader.Authorization] = authorizationType + " " + authorization;
                //client.Headers.Add("Authorization", authorizationType + " " + authorization);

                client.Headers = headers;

                client.ContentType = contentType;
                client.Method = "POST";


                //client.PreAuthenticate = true;
               
                client.Accept = contentType;



                webresponse =  await client.GetResponseAsync();
               // if (tskwebresponse == null) return null;

                //webresponse = tskwebresponse.Result;
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
                if (executioncount == 2)
                {
                    //log.Error("Unexpected error in SendPostRequest :" + ex.Message, ex);
                }

                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            finally
            {
               
               //try { if (streamreader != null) { streamreader.Dispose(); } } catch (Exception ex) { }
                try { if (streamreader != null) { ((IDisposable)streamreader).Dispose(); } } catch (Exception ex) { }

               //try { if (stream != null) { stream.Dispose(); } } catch (Exception ex) { }
                try { if (stream != null) { ((IDisposable)stream).Dispose(); } } catch (Exception ex) { }

              // try { if (webresponse != null) { webresponse.Dispose(); } } catch (Exception ex) { }
                try { if (webresponse != null) { ((IDisposable)webresponse).Dispose(); } } catch (Exception ex) { }

            }
            return response;


        }



        public static async Task< HttpResponseMessage> SendGetRequest(string uri, string contentType, string authorizationType, string authorization, string requestUri)
        {
            HttpResponseMessage res = await SendGetRequestAction(uri, contentType, authorizationType, authorization, requestUri, 1);
            if (res != null && res.StatusCode == HttpStatusCode.BadRequest || res.StatusCode == HttpStatusCode.Unauthorized)
            {
                LastAuthDetails = null;
                Dictionary<string, string> token = await GetAuthToken();
                res = await SendGetRequestAction(uri, contentType, token[TokenTypeKey], token[AccessTokenKey], requestUri, 2);
            }
            return res;
        }

        public static async Task<HttpResponseMessage> SendGetRequestAction(string uri, string contentType, string authorizationType, string authorization, string requestUri, int executioncount)
        {
            HttpResponseMessage response = null;
            WebResponse webresponse = null;
            Stream stream = null;
            StreamReader streamreader = null;
            try
            {



                HttpWebRequest client = (HttpWebRequest)WebRequest.Create(uri + requestUri);


                WebHeaderCollection headers = new WebHeaderCollection();

                //headers[HttpRequestHeader.UserAgent] = "Mozilla/5.0";
                // client.UserAgent = "Mozilla/5.0";
                headers[HttpRequestHeader.Authorization] = authorizationType + " " + authorization;
                //client.Headers.Add("Authorization", authorizationType + " " + authorization);

                client.Headers = headers;

                //vinod
                //client.ContentType = contentType;


                client.Method = "GET";


                //client.PreAuthenticate = true;

                client.Accept = contentType;
             
                // client.PreAuthenticate = true;

                 webresponse = await client.GetResponseAsync();
                //if (tskwebresponse == null) return null;

                //webresponse = tskwebresponse.Result;
                if (webresponse == null) return null;

                //webresponse = client.GetResponse();
                stream = webresponse.GetResponseStream();
                if (stream == null) return null;

                streamreader = new StreamReader(stream, Encoding.UTF8);
                var json = streamreader.ReadToEnd();

                response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");

            }
            catch (Exception ex)
            {
                if (executioncount == 2)
                {
                   // log.Error("Unexpected error in SendGetRequest URL:" + uri + requestUri);
                    //log.Error("Unexpected error in SendGetRequest :" + ex.Message, ex);
                }

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

        public static Dictionary<string, string> DeserializeJsonString(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            //var jss = new JavaScriptSerializer();
            //return jss.Deserialize<Dictionary<string, string>>(responseContent);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

        }

        public static AnalyticsViewCountResponse DeserializeJsonStringToAnalyticsViewCount(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            //var jss = new JavaScriptSerializer();
            //return jss.Deserialize<AnalyticsViewCountResponse>(responseContent);
            return JsonConvert.DeserializeObject<AnalyticsViewCountResponse>(responseContent);
        }



        public static Dictionary<string, string> DeserializeJsonStringToDictiionary(HttpResponseMessage response)
        {
           
            var responseContent = response.Content.ReadAsStringAsync().Result;
            //var jss = new JavaScriptSerializer();
            //return jss.Deserialize<Dictionary<string, string>>(responseContent);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
        }



        public static CMSItemCollection<CMSApiVideo> DeserializeJsonStringToVdoSearchResult(HttpResponseMessage response)
        {
            CMSItemCollection<CMSApiVideo> ret = new CMSItemCollection<CMSApiVideo>();
            var responseContent = response.Content.ReadAsStringAsync().Result;

            //var jss = new JavaScriptSerializer();
            //return jss.Deserialize<CMSItemCollection<CMSApiVideo>>(responseContent);
            return JsonConvert.DeserializeObject<CMSItemCollection<CMSApiVideo>> (responseContent);

        }


        public static CMSItemCollection<CMSApiPlayList> DeserializeJsonStringToPlaylistSearchResult(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            //var jss = new JavaScriptSerializer();
            //return jss.Deserialize<CMSItemCollection<CMSApiPlayList>>(responseContent);
            return JsonConvert.DeserializeObject<CMSItemCollection<CMSApiPlayList>>(responseContent);
        }

        public static CMSApiVideo DeserializeJsonStringToVdoResult(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            //var jss = new JavaScriptSerializer();
            //return jss.Deserialize<CMSApiVideo>(responseContent);
            return JsonConvert.DeserializeObject<CMSApiVideo>(responseContent);
        }
        public  static MediaVideo DeserializeJsonStringToMediaVdoResult(HttpResponseMessage response)
        {
            var recontent = response.Content;
            var responseContent =  recontent.ReadAsStringAsync().Result; 
            return JsonConvert.DeserializeObject<MediaVideo>(responseContent);
        }
        
        private static async void SetPlaysTotal(CMSApiVideo vdo, Dictionary<string, string> tokendict)
        {
            int retcount = 0;
            try
            {
                var relativeUri = string.Format(AnalyticApiRequestUriTemplate, vdo.account_id, vdo.id);

                var apiResponse = await SendGetRequest(AnalyticApiUri, AnalyticApiContentType, tokendict[TokenTypeKey], tokendict[AccessTokenKey], relativeUri);
                var videoCountDict = DeserializeJsonStringToDictiionary(apiResponse);
                retcount = ConvertToInt(videoCountDict[AllTimeVideoViews]);

            }
            catch (Exception ex)
            {
               // log.Error("Unexpected error in SetPlaysTotal :" + ex.Message, ex);
                //swallow the errors
            }
            vdo.PlaysTotal = retcount;
        }

       

        private static async Task<int> GetCountofAllVideos()
        {

            string accountid = NewReadToken;
            string baseurlforvideocount = @"/v1/accounts/{0}/counts/videos/";

            int retcount = 0;

            Dictionary<string, string> tokenDict = null;
            try
            {
                tokenDict = await GetAuthToken();

                string finalurlforvideolistcount = String.Format(baseurlforvideocount, accountid);

                var apiResponse = await SendGetRequest(CMSApiUri, AnalyticApiContentType, tokenDict[TokenTypeKey], tokenDict[AccessTokenKey], finalurlforvideolistcount);
                var videoCountDict = DeserializeJsonStringToDictiionary(apiResponse);
                retcount = ConvertToInt(videoCountDict["count"]);
            }
            catch (Exception ex)
            {
               // log.Error("Unexpected error in GetCountofAllVideos :" + ex.Message, ex);
                //swallow exception
            }
            return retcount;
        }

        #endregion
    }
}
