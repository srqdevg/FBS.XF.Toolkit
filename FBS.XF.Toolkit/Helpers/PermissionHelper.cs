using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FBS.XF.Toolkit.Helpers
{
	/// <summary>
	/// Permission Helper.
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
		#endregion
	}
}
