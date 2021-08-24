using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FBS.XF.Toolkit.Interfaces;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Win32;
using Xamarin.Essentials;

namespace FBS.XF.Toolkit.WPF.Controls
{
	/// <summary>
	/// FilePickerDialog.
	/// </summary>
	public class FilePickerDialog : IFilePickerDialog
	{
		#region Public methods
		/// <summary>
		/// Picks the file.
		/// </summary>
		/// <returns>Task&lt;System.String&gt;.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public Task<FileResult> PickFile()
		{
			var taskCompletionSource = new TaskCompletionSource<FileResult>();
			var openFileDialog = new OpenFileDialog();
			var result = openFileDialog.ShowDialog();

			if (result.HasValue && result == true)
			{
				var provider = new FileExtensionContentTypeProvider();

				if (!provider.TryGetContentType(openFileDialog.FileName, out var contentType))
				{
					contentType = "application/octet-stream";
				}

				taskCompletionSource.SetResult(new FileResult(openFileDialog.FileName, contentType));
			}

			return taskCompletionSource.Task;
		}
		#endregion
	}
}
