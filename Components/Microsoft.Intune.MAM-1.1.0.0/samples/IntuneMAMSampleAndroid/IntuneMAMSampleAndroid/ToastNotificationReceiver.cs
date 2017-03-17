//-----------------------------------------------------------------------
// <copyright file="ToastNotificationReceiver.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Android.Content;
using Microsoft.Intune.Mam.Client.Notification;
using Microsoft.Intune.Mam.Policy.Notification;
using Android.OS;
using Android.Widget;

namespace IntuneMAMSampleAndroid
{
    /// <summary>
    /// Receives notifications and displays toast.
    /// </summary>
    public class ToastNotificationReceiver : Java.Lang.Object, IMAMNotificationReceiver
    {
        private readonly Context context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">the context of the receiver.</param>
        public ToastNotificationReceiver(Context context) {
            this.context = context;
        }

        /// <summary>
        /// Handles Receive.
        /// </summary>
        /// <param name="MAMNotification">Incoming notification.</param>
        /// <returns>True.</returns>
        public bool OnReceive(IMAMNotification notification) {
            Handler handler = new Handler(this.context.MainLooper);
            handler.Post(() => { Toast.MakeText(context, "Received MDMNotification of type " + notification.Type, ToastLength.Short).Show(); });
            return true;
        }
    }
}