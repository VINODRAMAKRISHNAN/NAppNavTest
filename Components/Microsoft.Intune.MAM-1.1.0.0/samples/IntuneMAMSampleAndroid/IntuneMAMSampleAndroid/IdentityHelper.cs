//-----------------------------------------------------------------------
// <copyright file="IdentityHelper.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Android.Content;
using Java.Util.Logging;
using Microsoft.Intune.Mam.Client.Identity;
using Microsoft.Intune.Mam.Client.App;
using Microsoft.Intune.Mam.Policy;
using Microsoft.Intune.Mam.Client;

namespace IntuneMAMSampleAndroid
{
    /// <summary>
    /// Helper for information on MAM User Info and Identity.
    /// </summary>
    sealed class IdentityHelper
    {
        private static readonly Logger LOGGER = Logger.GetLogger(typeof(IdentityHelper).Name);

        /// <summary>
        /// Constructor.
        /// </summary>
        private IdentityHelper()
        {
        }

        /// <summary>
        /// Gets the current identity.
        /// </summary>
        /// <param name="context">UI context to use.</param>
        /// <returns>The effective MAM identity.</returns>
        internal static string GetCurrentIdentity(Context context)
        {
            return TestIdentityResolver.getCurrentIdentity(context);
        }

        ///<summary>
        /// Resolves the identity using the MAMPolicyManager.
        /// </summary>
        static class TestIdentityResolver
        {
            private static readonly Logger LOGGER = Logger.GetLogger(typeof(TestIdentityResolver).Name);

            /// <summary>
            /// Take the thread identity, UI Policy identity (if context is not null), and process identity into account.
            /// </summary>
            /// <param name="Context">Context for UI Identity.</param>
            ///<returns>Identity string.</returns>
            public static string getCurrentIdentity(Context context)
            {
                string identity = null;
                if (MAMPolicyManager.CurrentThreadIdentity != null)
                {
                    identity = MAMPolicyManager.CurrentThreadIdentity;
                    LOGGER.Info("Resolved identity (Thread): [" + identity + "]");
                }
                else if (MAMPolicyManager.GetUIPolicyIdentity(context) != null)
                {
                    identity = MAMPolicyManager.GetUIPolicyIdentity(context);
                    LOGGER.Info("Resolved identity (UIPolicy): [" + identity + "]");
                }
                else if (MAMPolicyManager.ProcessIdentity != null)
                {
                    identity = MAMPolicyManager.ProcessIdentity;
                    LOGGER.Info("Resolved identity (Process): [" + identity + "]");
                }
                else
                {
                    LOGGER.Info("Resolved identity (None).");
                }
                return identity;
            }
        }

        ///<summary>
        /// Gets the UserPrincipalName for the enrolled MAM User.
        /// </summary>
        ///<returns>The User Principal Name</returns>
        internal static string GetUPN()
        {
            IMAMUserInfo info = MAMComponents.GetUserInfo();
            string upn;
            if (null == info)
            {
                upn = null;
                LOGGER.Log(Level.Severe, "MAMUserInfo is null");
            }
            else if (null == info.PrimaryUser)
            {
                upn = null;
                LOGGER.Log(Level.Severe, "MAMUserInfo getPrimaryUser is null");
            }
            else
            {
                upn = info.PrimaryUser;
                LOGGER.Log(Level.Info, "UPN is: " + upn);
            }
            return upn;
        }


        /// <summary>
        /// Sets the UI identity to an activity.
        /// </summary>
        /// <param name="activity">activity</param>
        /// <param name="identity">identity</param>
        public static void SetUIIdentity(Context activity, string identity)
        {
            MAMPolicyManager.SetUIPolicyIdentity(activity, identity, new SetUIIdentityCallback(activity, identity));
        }

        private class SetUIIdentityCallback : Java.Lang.Object, IMAMSetUIIdentityCallback
        {
            private Context activity;
            private string identity;

            public SetUIIdentityCallback(Context activity, string identity)
            {
                this.identity = identity;
                this.activity = activity;
            }

            public void NotifyIdentityResult(MAMIdentitySwitchResult identitySwitchResult)
            {
                LOGGER.Info("Set UI identity to [" + identity + "] result: " + identitySwitchResult.ToString());
                LOGGER.Info("After setUIPolicyIdentity, identity is:[" + MAMPolicyManager.GetUIPolicyIdentity(activity) + "]");
            }
        }
    }
}