//-----------------------------------------------------------------------
// <copyright file="EntryActivity.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Intune.Mam.Client.App;

namespace IntuneMAMSampleAndroid
{
    [Activity(Label ="XamarinMAM Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class EntryActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_entry);

            Button button = FindViewById<Button>(Resource.Id.EntryActivityStartButton);

            button.Click += OnClickStart;
        }

        public void OnClickStart(object sender, EventArgs e)
        {
            this.StartActivity(IntentFactory.NewIntent(this, typeof(MainActivity)));
        }
    }
}

