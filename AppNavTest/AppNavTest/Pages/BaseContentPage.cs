using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace AppNavTest
{
    public interface IPageBase
    {
        Task<bool> PopToRoot();
    }
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            

        }



        protected async void btnmenubase_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PopupTransparent());

        }

        protected async void btnbackbase_clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PopAsync();
            }
            catch(Exception ex)
            {
                //swallow exception
            }
            
        }

        protected override void OnAppearing()
        {
            if (Navigation != null && Navigation.NavigationStack != null && (Navigation.NavigationStack.Count == 1 || Navigation.NavigationStack.Count == 0))
            {
                var xidbackbutton = this.FindByName<global::Xamarin.Forms.Image>("idbackbutton");
                if (xidbackbutton !=null)
                 xidbackbutton.IsVisible = false;

                var xidmenubutton = this.FindByName<global::Xamarin.Forms.Image>("idmenubutton");
                if (xidmenubutton != null)
                    xidmenubutton.IsVisible = true;
            }

            //if (this.Title != "Video Gallery" && Navigation != null && Navigation.NavigationStack != null && (Navigation.NavigationStack.Count > 1))
            //{
            //    var xidmenubutton = this.FindByName<global::Xamarin.Forms.Image>("idmenubutton");
            //    if (xidmenubutton != null)
            //        xidmenubutton.IsVisible = false;
            //}
            base.OnAppearing();

           
        }
       
     

        public Label GetLabelByName(string name)
        {
            return this.FindByName<global::Xamarin.Forms.Label>(name);
        }
        
        
    }
}
