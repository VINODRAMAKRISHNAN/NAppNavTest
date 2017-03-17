using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNavTest
{
    
    public class ViewVideoVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        private bool isloading = false;
        public bool IsLoading
        {
            set
            {
                if (isloading != value)
                {
                    isloading = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("IsLoading"));

                    }
                }
            }
            get
            {
                return isloading;
            }
        }

        private bool isautoplay = false;
        public bool IsAutoPlay
        {
            set
            {
                if (isautoplay != value)
                {
                    isautoplay = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("IsAutoPlay"));

                    }
                }
            }
            get
            {
                return isautoplay;
            }
        }

        public void RaisePropertyEvent(string attributename)
        {
            PropertyChanged(this,
                       new PropertyChangedEventArgs("SelectedCategoryDisplay"));
        }



        private string selectedvideoname = "Name not available";
        public string SelectedVideoName
        {
            set
            {
                if (selectedvideoname != value)
                {
                    selectedvideoname = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedVideoName"));
                       
                    }
                }
            }
            get
            {
                return selectedvideoname;
            }
        }

        private string selectedvideourl = "";
        public string SelectedVideoURL
        {
            set
            {
                if (selectedvideourl != value)
                {
                    selectedvideourl = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedVideoURL"));

                    }
                }
            }
            get
            {
                return selectedvideourl;
            }
        }

        private string selectedvideid = "0";
        public string SelectedVideoId
        {
            set
            {
                if (selectedvideid != value)
                {
                    selectedvideid = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this,
                            new PropertyChangedEventArgs("SelectedVideoId"));

                    }
                }
            }
            get
            {
                return selectedvideid;
            }
        }

        public async Task<bool> LoadMediaURL()
        {

            try
            {

                this.IsLoading = true;

             

              
                var apiResponse = await CMSPlaylistApiServiceExtension.SendGetRequestMediaAPIAction(this.SelectedVideoId);
                MediaVideo mvideo = CMSApiServiceExtension.DeserializeJsonStringToMediaVdoResult(apiResponse);

                if (mvideo != null)
                {
                    this.SelectedVideoURL = mvideo.FLVURL;
                
                }
            }
            catch (Exception ee)
            {

                string sss = ee.Message.ToString();

            }
            finally
            {
                this.IsLoading = false;
            }

            return true;
        }



      

    }
}
