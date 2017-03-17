using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace AppNavTest
{
    class Model
    {
    }

    public class AuthDetails
    {
        public long LastAccessTimeInSeconds { get; set; }
        public Dictionary<string, string> LastAuthTokenResponse { get; set; }
    }


    public class FieldValuePair
    {
        private string _field, _value;
        public FieldValuePair()
        {
        }
        public FieldValuePair(string field, string value)
        {
            _field = field;
            _value = value;
        }

        public string Field
        {
            get
            {
                return _field;
            }

            set
            {
                _field = value;
            }

        }
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }

        }

        public string ToBrightcoveString()
        {
            return "";
        }
    }

    public enum SortBy
    {
        None = 0,
        CreationDate = 1,
        ModifiedDate = 2,
        PublishDate = 3,
        PlaysTotal = 4,
        PlaysTrailingWeek = 5,
    }

    public enum SortOrder
    {
        None = 0,
        Ascending = 1,
        Descending = 2,
    }

    public enum CMSSortBy
    {
        None = 0,
        created_at = 1,
        updated_at = 2,
        published_at = 3,
        plays_total = 4,
        plays_trailing_week = 5,
    }


    public class CMSItemCollection<T> : List<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool IsError { get; set; }
    }


    public static class CustomListExtensions
    {
        public static CMSItemCollection<T> ToCMSItemCollection<T>(this List<T> list)
        {
            CMSItemCollection<T> collection = new CMSItemCollection<T>();

            foreach (var item in list)
            {
                collection.Add((T)item);
            }

            return collection;
        }
    }




    #region CMSApiVideo Entities



    //public class CustomFields
    //{
    //    public string relatedlink1url { get; set; }
    //    public string relatedlink1text { get; set; }
    //}



    public class Source
    {
        public string src { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }
    }


    public class Thumbnail
    {
        public string asset_id { get; set; }
        public bool? remote { get; set; }
        public string src { get; set; }
        public List<Source> sources { get; set; }
    }

    public class Source2
    {
        public string src { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }
    }

    public class Poster
    {
        public string asset_id { get; set; }
        public bool? remote { get; set; }
        public string src { get; set; }
        public List<Source2> sources { get; set; }
    }

    public class Images
    {
        public Thumbnail thumbnail { get; set; }
        public Poster poster { get; set; }
    }

    public class Link
    {
        public string text { get; set; }
        public string url { get; set; }
    }

    public class Schedule
    {
        public string ends_at { get; set; }
        public string starts_at { get; set; }
    }

    public class Sharing
    {
        public bool? by_external_acct { get; set; }
        public object by_id { get; set; }
        public object source_id { get; set; }
        public bool? to_external_acct { get; set; }
        public bool? by_reference { get; set; }
    }

    public class Source3
    {
        public string src { get; set; }
    }

    public class TextTrack
    {
        public string id { get; set; }
        public string src { get; set; }
        public string srclang { get; set; }
        public string label { get; set; }
        public string kind { get; set; }
        public string mime_type { get; set; }
        public string asset_id { get; set; }
        public List<Source3> sources { get; set; }
        public bool? @default { get; set; }
    }



    public class CMSApiVideo
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public object ad_keys { get; set; }
        public bool? complete { get; set; }
        public string created_at { get; set; }
        public List<object> cue_points { get; set; }
        public Dictionary<string, string> custom_fields { get; set; }
        public string description { get; set; }
        public string digital_master_id { get; set; }
        public int? duration { get; set; }
        public string economics { get; set; }
        public object folder_id { get; set; }
        public object geo { get; set; }
        public Images images { get; set; }
        public Link link { get; set; }
        public string long_description { get; set; }
        public string name { get; set; }
        public string original_filename { get; set; }
        public string published_at { get; set; }
        public string reference_id { get; set; }
        public Schedule schedule { get; set; }
        public Sharing sharing { get; set; }
        public string state { get; set; }
        public List<string> tags { get; set; }
        public List<TextTrack> text_tracks { get; set; }
        public string updated_at { get; set; }


        private long _playstotal = 0;

        [JsonIgnore]

        public string SeoName
        {
            get
            {
                string ret = "";

                try
                {
                    if (!string.IsNullOrEmpty(this.name))
                    {
                        ret = Regex.Replace(this.name, "[^0-9a-zA-Z]+", "");
                        ret = ret.Replace(" ", "-");
                    }

                }
                catch (Exception ex)
                {
                    //swallow this error
                }
                return ret;
            }
        }

        [JsonIgnore]

        public DateTime PublishedDate
        {
            get
            {
                DateTime ret = DateTime.MinValue;
                try
                {
                    ret = DateTime.Parse(this.published_at);
                }
                catch (Exception ex)
                {
                    //swallow this error
                }
                return ret;
            }
        }

        [JsonIgnore]

        public long IdT
        {
            get
            {

                long ret = 0;
                try
                {
                    ret = long.Parse(this.id);
                }
                catch (Exception ex)
                {
                    //swallow this error
                }
                return ret;
            }
        }

        [JsonIgnore]

        public long PlaysTotal
        {
            get
            {
                return _playstotal;
            }
            set
            {
                long val = 0;
                try
                {
                    val = long.Parse(value.ToString());
                }
                catch (Exception ex)
                {
                    //swallow this error
                }
                _playstotal = val;
            }
        }

        [JsonIgnore]

        public string ThumbnailUrl
        {
            get
            {
                return this.images == null ? "" : (this.images.thumbnail == null ? "" : (this.images.thumbnail == null ? "" : this.images.thumbnail.src));
            }

        }

        [JsonIgnore]

        public long Length
        {
            get
            {

                long ret = 0;
                try
                {
                    if (this.duration != null)
                    {
                        ret = long.Parse(this.duration.ToString());
                    }

                }
                catch (Exception ex)
                {
                    //swallow this error
                }
                return ret;
            }
        }

        [JsonIgnore]

        public List<string> TagsT
        {
            get
            {
                return this.tags;
            }
        }

        [JsonIgnore]

        public string NameT
        {
            get
            {
                return this.name;
            }
        }

        [JsonIgnore]

        public string ShortDescription
        {
            get
            {
                return this.description;
            }
        }
        [JsonIgnore]

        public long AccountId
        {
            get
            {
                long ret = 0;
                try
                {
                    if (this.duration != null)
                    {
                        ret = long.Parse(this.account_id);
                    }

                }
                catch (Exception ex)
                {
                    //swallow this error
                }
                return ret;
            }
        }

    }

    #endregion

    #region CMSPlayList Entities

    public class CMSApiPlayList
    {
        public string id { get; set; }
        public string account_id { get; set; }
        public string created_at { get; set; }
        public object description { get; set; }
        public bool? favorite { get; set; }
        public string name { get; set; }
        public object reference_id { get; set; }
        public string type { get; set; }
        public string updated_at { get; set; }
        public List<string> video_ids { get; set; }
        public List<CMSApiVideo> videos { get; set; }

        [JsonIgnore]
        public long IdT
        {
            get
            {

                long ret = 0;
                try
                {
                    ret = long.Parse(this.id);
                }
                catch (Exception ex)
                {
                    //swallow this error
                }
                return ret;
            }
        }


        public string NameTT
        {
            get
            {
                return this.name;
            }
        }
    }

   
    #endregion


    #region analytics entities

}

    public class ViewCountItem
    {
        public string video { get; set; }
        int video_view_t=0;
        public int? video_view 
        {
            get
            {
               return video_view_t;
            } 
            set
            {
                if (value == null){
                    value=0;
                }
                video_view_t=(int)value;
            }
        }
        
    }

    public class ASummary
    {
        public int video_view { get; set; }
    }

    public class AnalyticsViewCountResponse
    {
        int item_count_t=0;
        public int item_count 
        { 
            
            get
            {
               return item_count_t;
            } 
            set
            {
                if (value == null){
                    value=0;
                }
                item_count_t = (int)value;
            }
        }
        public List<ViewCountItem> items { get; set; }
        public ASummary summary { get; set; }

    #endregion
}
