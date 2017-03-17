//-----------------------------------------------------------------------
// <copyright file="TestFragment.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Util.Logging;
using Microsoft.Intune.Mam.Client.App;
using Microsoft.Intune.Mam.Client.Identity;
using Microsoft.Intune.Mam.Client;

namespace IntuneMAMSampleAndroid
{
    /// <summary>
    /// A MAMFragment which holds the ContentProvider buttons.
    /// </summary>
    public class TestFragment : MAMFragment, View.IOnClickListener
    {
        public const string DUMMY_IMAGE_GUID = "27792a3d-2afc-4733-bcae-e02f7925bc2e";

        public override View OnMAMCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnMAMCreateView(inflater, container, savedInstanceState);

            var fragView = inflater.Inflate(Resource.Layout.fragment_testfragment, container, false);

            // Set onclick listeners for ContentProvider buttons
            fragView.FindViewById<Button>(Resource.Id.hideFragmentButton).SetOnClickListener(this);
            fragView.FindViewById<Button>(Resource.Id.copyCPButton).SetOnClickListener(this);
            fragView.FindViewById<Button>(Resource.Id.pasteImageButton).SetOnClickListener(this);
            fragView.FindViewById<Button>(Resource.Id.dbOpsButton).SetOnClickListener(this);

            return fragView;
        }

        private void OnClickDBOps(View v)
        {
            var clipboard = (ClipboardManager)Activity.GetSystemService(Context.ClipboardService);
            var clip = clipboard.PrimaryClip;
            var resolver = Activity.ContentResolver;
            var target = clip.GetItemAt(0).Uri;
            var cp = resolver.AcquireContentProviderClient(target);

            var val1 = "valueInField1";
            var val2 = "valueInField2";
            var val3 = "value2InField2";

            // Initialize DB operation arguments
            var insertVals = new ContentValues();
            insertVals.Put(TestContentProvider.FIELD1, val1);
            insertVals.Put(TestContentProvider.FIELD2, val2);
            var updateVals = new ContentValues();
            updateVals.Put(TestContentProvider.FIELD1, val1);
            updateVals.Put(TestContentProvider.FIELD2, val3);

            // Set up result reporting
            var resultText = Activity.FindViewById<TextView>(Resource.Id.dbResultText);
            resultText.Text = "";
            var result = true;

            try
            {
                // Insert and verify
                var c = cp.Query(target, null, null, null, null);
                cp.Insert(target, insertVals);
                c = cp.Query(target, null, null, null, null);
                result = result && (c.Count == 1);
                result = result && (c.ColumnCount == 2);
                c.MoveToFirst();
                result = result && (c.GetString(0).Equals(val1));
                result = result && (c.GetString(1).Equals(val2));

                // Update and verify
                cp.Update(target, updateVals, null, null);
                c = cp.Query(target, null, null, null, null);
                result = result && (c.Count == 1);
                result = result && (c.ColumnCount == 2);
                c.MoveToFirst();
                result = result && (c.GetString(0).Equals(val1));
                result = result && (c.GetString(1).Equals(val3));

                // Delete and verify
                cp.Delete(target, val1, null);
                c = cp.Query(target, null, null, null, null);
                result = result && (c.Count == 0);
                result = result && (c.ColumnCount == 2);
            }
            catch (RemoteException e)
            {
                e.PrintStackTrace();
            }

            if (result)
            {
                resultText.Text = "DB Ops Pass";
            }
        }

        private void OnClickPasteImage(View v)
        {
            var imageView = Activity.FindViewById<ImageView>(Resource.Id.image1);
            imageView.SetImageBitmap(null);
            imageView.ContentDescription = "NoImage";

            var clipboard = (ClipboardManager)Activity.GetSystemService(Context.ClipboardService);
            var clip = clipboard.PrimaryClip;
            var resolver = Activity.ContentResolver;
            var target = clip.GetItemAt(0).Uri;
            var cp = resolver.AcquireContentProviderClient(target);
            ParcelFileDescriptor pfd = null;
            try
            {
                pfd = cp.OpenFile(target, "r");
            }
            catch (RemoteException e)
            {
                e.PrintStackTrace();
            }
            catch (FileNotFoundException e)
            {
                e.PrintStackTrace();
            }
            var image = BitmapFactory.DecodeFileDescriptor(pfd.FileDescriptor);
            imageView.SetImageBitmap(image);

            // Set ImageView to pass state
            imageView.ContentDescription = "YesImage";
        }

        private void OnClickCopyCP(View v)
        {
            var uriString = string.Format("content://{0}", TestContentProvider.AUTHORITY);
            var providerUri = Android.Net.Uri.WithAppendedPath(Android.Net.Uri.Parse(uriString), DUMMY_IMAGE_GUID);
            var clip = ClipData.NewUri(Activity.ContentResolver, "AContentProvider", providerUri);
            var clipboard = (ClipboardManager)Activity.GetSystemService(Context.ClipboardService);
            clipboard.PrimaryClip = clip;
        }

        private void OnClickHideFragment(View v)
        {
            var ft = FragmentManager.BeginTransaction();
            ft.Remove(this);
            ft.Commit();
        }

        /// <summary>
        /// Handle clicks passed from the parent view.
        /// </summary>
        /// <param name="v">The clicked view.</param>
        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.hideFragmentButton)
            {
                this.OnClickHideFragment(v);
            }
            else if (v.Id == Resource.Id.copyCPButton)
            {
                this.OnClickCopyCP(v);
            }
            else if (v.Id == Resource.Id.pasteImageButton)
            {
                this.OnClickPasteImage(v);
            } 
            else if (v.Id == Resource.Id.dbOpsButton)
            {
                this.OnClickDBOps(v);
            }
        }
    }
}