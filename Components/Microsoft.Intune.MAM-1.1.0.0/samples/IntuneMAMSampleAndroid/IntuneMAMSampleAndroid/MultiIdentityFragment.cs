//-----------------------------------------------------------------------
// <copyright file="MultiIdentityFragment.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using Android.Content;
using Java.Util.Logging;
using Microsoft.Intune.Mam.Client.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Microsoft.Intune.Mam.Client.Identity;
using Microsoft.Intune.Mam.Client;

namespace IntuneMAMSampleAndroid
{
    /// <summary>
    /// Fragment containing logic for MultiIdentity inspection and manipulation.
    /// </summary>
    public class MultiIdentityFragment : MAMFragment, View.IOnClickListener
    {
        private static readonly Logger LOGGER = Logger.GetLogger(typeof(MultiIdentityFragment).Name);
        
        public override void OnMAMCreate(Bundle savedInstanceState)
        {
            base.OnMAMCreate(savedInstanceState);
        }

        public override View OnMAMCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnMAMCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.fragment_multiidentityfragment, container, false);

            RefreshCurrentIdentity(view.RootView, Activity);

            return view;
        }

        /// <summary>
        /// Called when "Current Identity" button is clicked.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickCurrentIdentityRefresh(View view)
        {
            RefreshCurrentIdentity(view.RootView, Activity);
        }

        /// <summary>
        /// Called when threading option is changed.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickThreadedIdentityOption(View view)
        {
            RefreshCurrentIdentity(view.RootView, Activity);
        }

        /// <summary>
        /// Refresh the current identity.
        /// </summary>
        /// <param name="rootView">The root view of UI elements to refresh the UI.</param>
        /// <param name="activityContext">The context from which the identity should be drawn.</param>
        private void RefreshCurrentIdentity(View rootView, Context activityContext)
        {
            string foundIdentity = IdentityHelper.GetCurrentIdentity(activityContext);

            TextView identityTextView = rootView.FindViewById<TextView>(Resource.Id.currentIdentityText);
            if (identityTextView != null)
            {
                if (foundIdentity != null)
                {
                    identityTextView.Text = foundIdentity;
                }
                else
                {
                    identityTextView.Text = "Identity is NULL.";
                }
            }
            LOGGER.Info("=== Identity ===");
            LOGGER.Info("Effective Identity:   [" + foundIdentity + "]");
            LOGGER.Info("CurrentThreadIdentity:[" + MAMPolicyManager.CurrentThreadIdentity + "]");
            LOGGER.Info("UIPolicyIdentity:     [" + MAMPolicyManager.GetUIPolicyIdentity(activityContext) + "]");
            LOGGER.Info("ProcessIdentity:      [" + MAMPolicyManager.ProcessIdentity + "]");
        }

        /// <summary>
        /// Gets the current identity selected in the UI.
        /// </summary>
        /// <param name="rootView">The root view of UI elements to inspect the UI.</param>
        /// <returns>A string representation of the current identity selected in the radio button group.</returns>
        private string GetSelectedIdentity(View rootView)
        {
            RadioGroup newIdentityRadioGroup = rootView.FindViewById<RadioGroup>(Resource.Id.newIdentityRadioGroup);
            int identityRadioButtonID = newIdentityRadioGroup.CheckedRadioButtonId;

            RadioButton b = rootView.FindViewById<RadioButton>(identityRadioButtonID);

            EditText newIdentityInput = rootView.FindViewById<EditText>(Resource.Id.newIdentity_text);

            string newIdentity = IdentityHelper.GetUPN();
            if (b.Id == Resource.Id.identityPrimaryRadioButton)
            {
                newIdentity = IdentityHelper.GetUPN();
            }

            if (b.Id == Resource.Id.identityNewRadioButton)
            {
                newIdentity = newIdentityInput.Text.Trim();
            }

            return newIdentity;
        }

        /// <summary>
        /// Sets the UI identity to {@link Resource.Id.newIdentityRadioGroup} on button click.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickSetUIIdentity(View view)
        {
            string newIdentity = GetSelectedIdentity(view.RootView);

            IdentityHelper.SetUIIdentity(Activity, newIdentity);

            LOGGER.Info("Pending set UI identity to [" + newIdentity + "]");
        }

        /// <summary>
        /// Clears the UI identity on button click.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickClearUIIdentity(View view)
        {
            var receiver = new ClearUIIdentityCallbackReceiver(LOGGER);
            MAMPolicyManager.SetUIPolicyIdentity(Activity, null, receiver);
            LOGGER.Info("Cleared UI Identity.");
        }

        class ClearUIIdentityCallbackReceiver : Java.Lang.Object, IMAMSetUIIdentityCallback
        {
            public Logger LOGGER;

            public ClearUIIdentityCallbackReceiver(Logger logger)
            {
                LOGGER = logger;
            }

            public void NotifyIdentityResult(MAMIdentitySwitchResult identitySwitchResult)
            {
                LOGGER.Info("Clear UI Identity [null] result: " + identitySwitchResult);
            }
        }

        /// <summary>
        /// Sets the Thread identity to {@link Resource.Id.newIdentityRadioGroup} on button click.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickSetThreadIdentity(View view)
        {
            string newIdentity = GetSelectedIdentity(view.RootView);
            MAMIdentitySwitchResult identitySwitchResult = MAMPolicyManager.SetCurrentThreadIdentity(newIdentity);
            LOGGER.Info("Set thread Identity to [" + newIdentity + "] result: " + identitySwitchResult.ToString());
        }

        /// <summary>
        /// Clears the thread identity on button click.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickClearThreadIdentity(View view)
        {
            MAMPolicyManager.SetCurrentThreadIdentity(null);
            LOGGER.Info("Cleared thread Identity.");
        }

        /// <summary>
        /// Sets the Process identity to {@link Resource.Id.newIdentityRadioGroup} on button click.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickSetProcessIdentity(View view)
        {
            string newIdentity = GetSelectedIdentity(view.RootView);
            MAMIdentitySwitchResult identitySwitchResult = MAMPolicyManager.SetProcessIdentity(newIdentity);
            LOGGER.Info("Set process Identity to [" + newIdentity + "] result: " + identitySwitchResult.ToString());
        }

        /// <summary>
        /// Clears the thread identity on button click.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickClearProcessIdentity(View view)
        {
            MAMPolicyManager.SetProcessIdentity(null);
            LOGGER.Info("Cleared process Identity.");
        }

        /// <summary>
        /// Launches a new activity as the identity specified in {@link Resource.Id.newIdentityRadioGroup}.
        /// </summary>
        /// <param name="view">The clicked button.</param>
        public void OnClickLaunchIdentity(View view)
        {
            // Cook up an intent for this activity.
            Intent launchSelfIntent = new Intent(Activity, Activity.Class);
            // MAM will send the current effective identity attached to the outgoing intent.
            string newIdentity = GetSelectedIdentity(view.RootView);
            string oldIdentity = MAMPolicyManager.CurrentThreadIdentity;
            // Temporarily set the current identity to the selected identity.
            MAMPolicyManager.SetCurrentThreadIdentity(newIdentity);
            try
            {
                StartActivity(launchSelfIntent);
            }
            finally
            {
                MAMPolicyManager.SetCurrentThreadIdentity(oldIdentity);
            }

        }

        /// <summary>
        /// Handle clicks passed from the parent view.
        /// </summary>
        /// <param name="v">The clicked view.</param>
        public void OnClick(View v)
        {
            if (Resource.Id.identitySetUIButton == v.Id)
            {
                OnClickSetUIIdentity(v);
            }
            else if (Resource.Id.identityClearUIButton == v.Id)
            {
                OnClickClearUIIdentity(v);
            }
            else if (Resource.Id.identityLaunchButton == v.Id)
            {
                OnClickLaunchIdentity(v);
            }
            else if (Resource.Id.identitySetThreadButton == v.Id)
            {
                OnClickSetThreadIdentity(v);
            }
            else if (Resource.Id.identityClearThreadButton == v.Id)
            {
                OnClickClearThreadIdentity(v);
            }
            else if (Resource.Id.identitySetProcessButton == v.Id)
            {
                OnClickSetProcessIdentity(v);
            }
            else if (Resource.Id.identityClearProcessButton == v.Id)
            {
                OnClickClearProcessIdentity(v);
            }
            else if (Resource.Id.currentIdentityRefresh == v.Id)
            {
                OnClickCurrentIdentityRefresh(v);
            }
        }

    }

}