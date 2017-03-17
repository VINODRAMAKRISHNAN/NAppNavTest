//-----------------------------------------------------------------------------------
// <copyright company="Microsoft" file="MainActivity.cs">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------------

using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;

namespace IntuneMAMFormsSample.Droid
{
    [Activity (Label = "IntuneMAMFormsSample", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnMAMCreate(Bundle bundle)
		{
			base.OnMAMCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new IntuneMAMFormsSample.App());
		}
    }
}

