using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// Interface IFilePickerDialog
	/// </summary>
	public interface IFilePickerDialog
	{
		#region Methods
		/// <summary>
		/// Picks the file.
		/// </summary>
		/// <returns>Task&lt;System.String&gt;.</returns>
		Task<FileResult> PickFile(string filetypes);
		#endregion
	}
}