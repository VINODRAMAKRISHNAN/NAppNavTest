//-----------------------------------------------------------------------
// <copyright file="IntentFactory.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Android.Content;

namespace IntuneMAMSampleAndroid
{
    /// <summary>
    /// Produce Intents that have the potential to break the WG SDK.
    /// </summary>
    sealed class IntentFactory
    {
        private static readonly string EXTRA_EVIL_SERIALIZABLE = typeof(IntentFactory).Name + ".EvilSerializable";

        /// <summary>
        /// Class that requires deserialization in the app's ClassLoader.
        /// </summary>
        [Serializable]
        private class EvilSerializable : Java.Lang.Object, Java.IO.ISerializable
        {
            private const long serialVersionUID = -9061855460227146504L;
        }

        /// <summary>
        /// Disabled constructor.
        /// </summary>
        private IntentFactory()
        {
        }

        /// <summary>
        /// See <see cref="Intent.Intent()">Intent.ctor()</see>
        /// </summary>
        ///<returns>New <see cref="Intent">Intent</code></returns>
        public static Intent NewIntent()
        {
            return new Intent();
        }

        /// <summary>
        /// See <see cref="Intent.Intent(Context, Class)">Intent.ctor(Context, Class)</see>
        /// </summary>
        /// <param name="packageContext">See <see cref="Intent.Intent(Context, Class)">Intent.Intent(Context, Class)</see>.</param>
        /// <param name="cls">See <see cref="Intent.Intent(Context, Class)">Intent.Intent(Context, Class)</see></param>
        /// <returns>New <see cref="Intent">Intent</see>.</returns>
        public static Intent NewIntent(Context packageContext, Type cls)
        {
            return AddSerializableExtras(new Intent(packageContext, cls));
        }

        /// <summary>
        /// See <see cref="Intent.Intent(string)">Intent.Intent(String)</see>.
        /// </summary>
        /// <param name="action">See <see cref="Intent.Intent(string)">Intent.Intent(String)</see>.</param>
        /// <returns>New <see cref="Intent">Intent</see>.</returns>
        public static Intent NewIntent(string action)
        {
            return new Intent(action);
        }

        /// <summary>
        /// See <see cref="Intent.Intent(string, Uri)">Intent.Intent(String, Uri)</see>.
        /// </summary>
        /// <param name="action">See <see cref="Intent.Intent(string, Uri)">Intent.Intent(String, Uri)</see>.</param>
        /// <param name="uri">See <see cref="Intent.Intent(string, Uri)">Intent.Intent(String, Uri)</see>.</param>
        /// <returns>new <see cref="Intent">Intent</see>.</returns>
        public static Intent NewIntent(string action, Android.Net.Uri uri)
        {
            return new Intent(action, uri);
        }

        /// <summary>
        /// See <see cref="Intent.createChooser(Intent, CharSequence)">Intent.createChooser(Intent, CharSequence)</see>.
        /// </summary>
        /// <param name="target">See <see cref="Intent.createChooser(Intent, CharSequence)">Intent.createChooser(Intent, CharSequence)</see>.</param>
        /// <param name="title">See <see cref="Intent.createChooser(Intent, CharSequence)">Intent.createChooser(Intent, CharSequence)</see>.</param>
        /// <returns>New <see cref="Intent">Intent</see>.</returns>
        public static Intent CreateChooser(Intent target, string title)
        {
            return Intent.CreateChooser(target, title);
        }

        /// <summary>
        /// Add the evil <see cref="Serializable">Serializable</see> extras to the <see cref="Intent">Intent</see>.
        /// </summary>
        /// <param name="intent"><see cref="Intent">Intent</see> to modify.</param>
        /// <returns>Modified <see cref="Intent">Intent</see>.</returns>
        private static Intent AddSerializableExtras(Intent intent)
        {
            intent.PutExtra(EXTRA_EVIL_SERIALIZABLE, new EvilSerializable());
            return intent;
        }
    }
}