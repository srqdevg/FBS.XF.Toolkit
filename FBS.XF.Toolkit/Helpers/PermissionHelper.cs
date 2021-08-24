using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// PermissionHelper.
	/// </summary>
	public static class PermissionHelper
	{
		#region Public methods
		/// <summary>
		/// Checks the permissions.
		/// </summary>
		/// <param name="permission">The permission.</param>
		/// <returns>Task&lt;PermissionStatus&gt;.</returns>
		public static async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
			where T : Permissions.BasePermission
		{
			var status = await permission.CheckStatusAsync();
			if (status != PermissionStatus.Granted)
			{
				status = await permission.RequestAsync();
			}

			return status;
		}

		////public static async Task<PermissionStatus> CheckPermissions(Permission permission)
		////{
		////	var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
		////	bool request = false;

		////	if (permissionStatus == PermissionStatus.Denied)
		////	{
		////		if (Device.RuntimePlatform == Device.iOS)
		////		{
		////			var title = $"{permission} Permission";
		////			var question = $"To use this plugin the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
		////			var positive = "Settings";
		////			var negative = "Maybe Later";
		////			var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);

		////			if (task == null)
		////			{
		////				return permissionStatus;
		////			}

		////			var result = await task;

		////			if (result)
		////			{
		////				CrossPermissions.Current.OpenAppSettings();
		////			}

		////			return permissionStatus;
		////		}

		////		request = true;
		////	}

		////	if (request || permissionStatus != PermissionStatus.Granted)
		////	{
		////		var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);

		////		if (!newStatus.ContainsKey(permission))
		////		{
		////			return permissionStatus;					
		////		}

		////		permissionStatus = newStatus[permission];

		////		if (newStatus[permission] != PermissionStatus.Granted)
		////		{
		////			permissionStatus = newStatus[permission];
		////			var title = $"{permission} Permission";
		////			var question = $"To use the plugin the {permission} permission is required.";
		////			var positive = "Settings";
		////			var negative = "Maybe Later";
		////			var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
		////			if (task == null)
		////				return permissionStatus;

		////			var result = await task;

		////			if (result)
		////			{
		////				CrossPermissions.Current.OpenAppSettings();
		////			}

		////			return permissionStatus;
		////		}
		////	}

		////	return permissionStatus;
		////}
		#endregion
	}
}
