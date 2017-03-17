//-----------------------------------------------------------------------
// <copyright file="TestContentProvider.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.IO;
using Android.Content;
using Android.Database;
using Android.Graphics;
using Android.OS;
using Java.Util.Logging;
using Microsoft.Intune.Mam.Client.App;
using Microsoft.Intune.Mam.Client.Content;
using Microsoft.Intune.Mam.Client.Identity;
using Android.App;

namespace IntuneMAMSampleAndroid
{
    [ContentProvider(new string[] { TestContentProvider.AUTHORITY })]
    public class TestContentProvider : MAMContentProvider
    {
        public static readonly Logger LOGGER = Logger.GetLogger(typeof(TestContentProvider).Name);

        public const string AUTHORITY = "com.microsoft.IntuneMAMSampleApplication.provider";

        public const string FIELD1 = "Field1";
        public const string FIELD2 = "Field2";

        /// <summary>
        /// Subdirectory under which served files live.
        /// </summary>
        public const string FILES_SUBDIR = "testContentProviderShared";

        /// <summary>
        /// Name of the extra which contains the primary result value in the bundle returned by callMAM().
        /// </summary>
        public const string BUNDLE_RESULT_KEY = "result";

        private const string TABLE_NAME = "t1";
        private const int BITMAP_COMPRESS_MAX_QUALITY = 100;

        /// <summary>
        /// Method to check isProvideContentAllowed on the passed in identity.
        /// </summary>
        private const string METHOD_SET_ID_SWITCH_SUCCESS = "SetIdentitySwitchSuccess";

        private static AppIdentitySwitchResult identitySwitchResult = AppIdentitySwitchResult.Success;

        private TestContentDatabase db;

        public override int DeleteMAM(Android.Net.Uri arg0, string arg1, string[] arg2)
        {
            try
            {
                db.ReadableDatabase.ExecSQL("DELETE FROM " + TABLE_NAME + " WHERE " + FIELD1 + "='" + arg1 + "';");
            }
            catch (SQLException e)
            {
                e.PrintStackTrace();
            }

            return 1;
        }

        public override string GetType(Android.Net.Uri uri)
        {
            return "text/plain";
        }

        public override Android.Net.Uri InsertMAM(Android.Net.Uri uri, ContentValues values)
        {
            // This log mesage is read by BVT code, do not midify it without modifying the corresponding BVTs
            LOGGER.Info("TestContentProvider.insertMAM with identity " + MAMPolicyManager.CurrentThreadIdentity);
            try
            {
                db.ReadableDatabase.ExecSQL("INSERT INTO " + TABLE_NAME + " VALUES ('" + values.GetAsString(FIELD1) + "','" + values.GetAsString(FIELD2) + "');");
            }
            catch (SQLException e)
            {
                e.PrintStackTrace();
            }

            return null;
        }

        public override bool OnCreate()
        {
            db = new TestContentDatabase(this.Context);
            return true;
        }

        public override ICursor QueryMAM(Android.Net.Uri uri, string[] projection, string selection, string[] selectionArgs, string sortOrder)
        {
            var cursor = db.ReadableDatabase.RawQuery("SELECT * FROM " + TABLE_NAME + ";", null);
            return cursor;
        }

        public override int UpdateMAM(Android.Net.Uri uri, ContentValues values, string selection, string[] selectionArgs)
        {
            try
            {
                db.ReadableDatabase.ExecSQL("UPDATE " + TABLE_NAME + " SET " + FIELD2 + "='" + values.GetAsString(FIELD2) + "' WHERE " + FIELD1 + "='" + values.GetAsString(FIELD1) + "';");
            }
            catch (SQLException e)
            {
                e.PrintStackTrace();
            }

            return 1;
        }

        public override ParcelFileDescriptor OpenFileMAM(Android.Net.Uri uri, string mode)
        {
            var filesDir = Application.Context.FilesDir.ToString();
            if (!uri.Path.Contains(TestFragment.DUMMY_IMAGE_GUID))
            {
                return ParcelFileDescriptor.Open(
                    new Java.IO.File(System.IO.Path.Combine(filesDir, FILES_SUBDIR, uri.Path)),
                    ParcelFileDescriptor.ParseMode(mode));
            }

            var path = System.IO.Path.Combine(filesDir, "icon.png");
            var bmap = BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.Icon);
            using (var output = File.OpenWrite(path))
            {
                bmap.Compress(Bitmap.CompressFormat.Png, BITMAP_COMPRESS_MAX_QUALITY, output);
            }

            return ParcelFileDescriptor.Open(
                new Java.IO.File(path), 
                ParcelFileDescriptor.ParseMode(mode));
        }
    }
}