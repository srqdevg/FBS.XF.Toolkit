﻿using System.Windows.Input;
using FBS.XF.Toolkit.Controls;
using FBS.XF.Toolkit.WPF.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(CustomCheckBox), typeof(CustomCheckBoxRenderer))]
namespace FBS.XF.Toolkit.WPF.Renderers
{
	/// <summary>
	/// CustomCheckBoxRenderer.
	/// Implements the <see cref="Xamarin.Forms.Platform.WPF.CheckBoxRenderer" />
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.WPF.CheckBoxRenderer" />
	public class CustomCheckBoxRenderer : CheckBoxRenderer
	{
		#region UI methods
		/// <summary>
		/// Controls the on mouse enter.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void ControlOnMouseEnter(object sender, MouseEventArgs e)
		{
			prevCursor = Control.Cursor;
			Control.Cursor = Cursors.Hand;
		}

		/// <summary>
		/// Controls the on mouse leave.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void ControlOnMouseLeave(object sender, MouseEventArgs e)
		{
			Control.Cursor = prevCursor;
		}
		#endregion

		#region Override methods
		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			if (Control != null && Device.RuntimePlatform == Device.WPF)
			{
				Control.MouseEnter -= ControlOnMouseEnter;
				Control.MouseLeave -= ControlOnMouseLeave;
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control != null && Device.RuntimePlatform == Device.WPF)
				{
					Control.MouseEnter += ControlOnMouseEnter;
					Control.MouseLeave += ControlOnMouseLeave;
				}
			}
		}
		#endregion

		#region Fields
		private Cursor prevCursor;
		#endregion
	}
}
