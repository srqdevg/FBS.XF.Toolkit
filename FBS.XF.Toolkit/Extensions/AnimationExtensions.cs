﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Extensions
{
	/// <summary>
	/// Animation Extensions.
	/// </summary>
	public static class AnimationExtensions
	{
		#region Public methods
		/// <summary>
		/// Changes the background color to.
		/// </summary>
		/// <param name="self">The self.</param>
		/// <param name="newColor">The new color.</param>
		/// <param name="length">The length.</param>
		/// <param name="easing">The easing.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		public static Task<bool> ChangeBackgroundColorTo(this VisualElement self, Color newColor, uint length = 250, Easing easing = null)
		{
			var result = new Task<bool>(() => false);

			if (!self.AnimationIsRunning(nameof(ChangeBackgroundColorTo)))
			{
				Color fromColor = self.BackgroundColor;

				try
				{
					Color Transform(double t) => Color.FromRgba(
						fromColor.R + t * (newColor.R - fromColor.R),
						fromColor.G + t * (newColor.G - fromColor.G),
						fromColor.B + t * (newColor.B - fromColor.B),
						fromColor.A + t * (newColor.A - fromColor.A));

					result = TransmuteColorAnimation(self, nameof(ChangeBackgroundColorTo), Transform, length, easing);
				}
				catch (Exception ex)
				{
					// To supress animation overlapping errors 
					self.BackgroundColor = fromColor;
				}
			}

			return result;
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Transmutes the color animation.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <param name="name">The name.</param>
		/// <param name="transform">The transform.</param>
		/// <param name="length">The length.</param>
		/// <param name="easing">The easing.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		private static Task<bool> TransmuteColorAnimation(VisualElement element, string name, Func<double, Color> transform, uint length, Easing easing)
		{
			easing ??= Easing.Linear;
			var taskCompletionSource = new TaskCompletionSource<bool>();

			element.Animate(name, transform, color => { element.BackgroundColor = color; }, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));
			return taskCompletionSource.Task;
		}
		#endregion


		 public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
		 {
			 Color Transform(double t) => Color.FromRgba(
				 fromColor.R + t * (toColor.R - fromColor.R), 
				 fromColor.G + t * (toColor.G - fromColor.G), 
				 fromColor.B + t * (toColor.B - fromColor.B), 
				 fromColor.A + t * (toColor.A - fromColor.A));

			 return ColorAnimation(self, "ColorTo", Transform, callback, length, easing);
		 }

    public static void CancelAnimation(this VisualElement self)
    {
        self.AbortAnimation("ColorTo");
    }

    public static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
    {
        easing ??= Easing.Linear;
        var taskCompletionSource = new TaskCompletionSource<bool>();

        element.Animate(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

        return taskCompletionSource.Task;
    }
	}
}
