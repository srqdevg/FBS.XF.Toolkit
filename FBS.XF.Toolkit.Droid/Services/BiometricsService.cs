using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using AndroidX.Core.Content;
//using Android.Support.V4.Content;
//using Android.Support.V4.Hardware.Fingerprint;
using AndroidX.Core.Hardware.Fingerprint;
using FBS.XF.Toolkit.Android.Services;
using FBS.XF.Toolkit.Interfaces;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(BiometricService))]
namespace FBS.XF.Toolkit.Android.Services
{
	/// <summary>
	/// BiometricsService.
	/// </summary>
	/// <seealso cref="IBiometricService" />
	public class BiometricService : IBiometricService
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BiometricService"/> class.
		/// </summary>
		public BiometricService()
		{
			// Get finger manager reference
			////fingerprintManager = FingerprintManagerCompat.From(Application.Context);
		}
		#endregion

		#region IBiometricService methods
		/// <summary>
		/// Gets the current phone number.
		/// </summary>
		/// <returns>System.String.</returns>
		public bool Authenticate()
		{
			//var manager = Android.App.Application.Context.GetSystemService(Context.TelephonyService) as TelephonyManager;
			//return true;
			var permissionResult = ContextCompat.CheckSelfPermission(Application.Context, Manifest.Permission.UseBiometric);

			if (permissionResult == Permission.Granted)
			{
				//_initialPanel.Visibility = ViewStates.Gone;
				//_authenticatedPanel.Visibility = ViewStates.Gone;
				//_errorPanel.Visibility = ViewStates.Gone;
				//_scanInProgressPanel.Visibility = ViewStates.Visible;
				//_dialogFrag.Init();
				//_dialogFrag.Show(FragmentManager, DIALOG_FRAGMENT_TAG);
			}
			//else
			//{
			//	Snackbar.Make(FindViewById(Res.Id.Content),
			//			Resource.String.missing_fingerprint_permissions,
			//			Snackbar.LengthLong)
			//		.Show();
			//}

			return false;
		}

		/// <summary>
		/// Determines whether this instance is available.
		/// </summary>
		/// <returns><c>true</c> if this instance is available; otherwise, <c>false</c>.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public bool IsAvailable()
		{
			//// Do we have the hardware?
			//if (!fingerprintManager.IsHardwareDetected)
			//{
			//	return false;
			//}

			//// Get keyguard manager
			//var keyguardManager = (KeyguardManager) Application.Context.GetSystemService(Context.KeyguardService);

			//if (keyguardManager != null && !keyguardManager.IsKeyguardSecure)
			//{
			//	return false;
			//}

			//// Do we have fingerprints to match against
			//if (!fingerprintManager.HasEnrolledFingerprints)
			//{
			//	return false;
			//}

			//return true;
			return false;
		}
		#endregion

		#region Fields
		//private readonly FingerprintManagerCompat fingerprintManager;
		#endregion
	}
}