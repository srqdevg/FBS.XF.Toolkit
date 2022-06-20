using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Extensions
{
	/// <summary>
	/// Animation Extensions.
	/// </summary>
	/// <remarks>
	/// ColorTo from https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/animation/custom
	/// </remarks>
	public static class AnimationExtensions
	{
		#region Public methods
		/// <summary>
		/// Cancel boing the animation
		/// </summary>
		/// <param name="self">The self.</param>
		public static void BoingAnimationCancel(this VisualElement self)
		{
			self.AbortAnimation("Boing");
			self.Scale = 1;
		}

		/// <summary>
		/// Start boing the animation.
		/// </summary>
		/// <param name="self">The self.</param>
		public static void BoingAnimationStart(this VisualElement self)
		{
			var parentAnimation = new Animation();
			var scaleDownAnimation = new Animation(v => self.Scale = v, 1, 0.5, Easing.Linear);
			var scaleUpAnimation = new Animation(v => self.Scale = v, 0.5, 1.0, Easing.Linear);

			parentAnimation.Add(0, 0.5, scaleDownAnimation);
			parentAnimation.Add(0.5, 1, scaleUpAnimation);

			parentAnimation.Commit(self, "Boing", 16, 2000, null, repeat: () => true);
		}

		/// <summary>
		/// Cancels the color to animation.
		/// </summary>
		/// <param name="self">The self.</param>
		public static void ColorToCancel(this VisualElement self)
		{
			self.AbortAnimation("ColorTo");
		}

		/// <summary>
		/// Starts the color to.
		/// </summary>
		/// <param name="self">The self.</param>
		/// <param name="fromColor">From color.</param>
		/// <param name="toColor">To color.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="length">The length.</param>
		/// <param name="easing">The easing.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		/// <remarks>
		/// Examples:
		///    await Task.WhenAll(
		///       label.ColorTo(Color.Red, Color.Blue, c => label.TextColor = c, 5000),
		///       label.ColorTo(Color.Blue, Color.Red, c => label.BackgroundColor = c, 5000));
		///    await this.ColorTo(Color.FromRgb(0, 0, 0), Color.FromRgb(255, 255, 255), c => BackgroundColor = c, 5000);
		///    await boxView.ColorTo(Color.Blue, Color.Red, c => boxView.Color = c, 4000);
		/// </remarks>
		public static Task<bool> ColorToStart(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, 
			uint length = 250, Easing easing = null)
		{
			Color Transform(double t) => 
				Color.FromRgba(
					fromColor.R + t * (toColor.R - fromColor.R), 
					fromColor.G + t * (toColor.G - fromColor.G), 
					fromColor.B + t * (toColor.B - fromColor.B), 
					fromColor.A + t * (toColor.A - fromColor.A));

			return ColorToAnimation(self, "ColorTo", Transform, callback, length, easing);
		}

		/// <summary>
		/// Opacities the animation cancel.
		/// </summary>
		/// <param name="self">The self.</param>
		public static void OpacityAnimationCancel(this VisualElement self)
		{
			self.AbortAnimation("Opacity");
			self.Opacity = 1;
		}
		/// <summary>
		/// Starts the opacity animation.
		/// </summary>
		/// <param name="self">The self.</param>
		public static void OpacityAnimationStart(this VisualElement self)
		{
			var parentAnimation = new Animation();
			var opacityDownAnimation = new Animation(v => self.Opacity = v, 1, 0.1, Easing.Linear);
			var opacityUpAnimation = new Animation(v => self.Opacity = v, 0.1, 1.0, Easing.Linear);

			parentAnimation.Add(0, 0.5, opacityDownAnimation);
			parentAnimation.Add(0.5, 1, opacityUpAnimation);

			parentAnimation.Commit(self, "Opacity", 16, 2000, null, repeat: () => true);
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Colors to animation.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="name">The name.</param>
		/// <param name="transform">The transform.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="length">The length.</param>
		/// <param name="easing">The easing.</param>
		/// <returns>Task&lt;System.Boolean&gt;.</returns>
		private static Task<bool> ColorToAnimation(VisualElement element, string name, Func<double, Color> transform, 
			Action<Color> callback, uint length, Easing easing)
		{
			easing ??= Easing.Linear;
			var taskCompletionSource = new TaskCompletionSource<bool>();

			element.Animate(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));
			return taskCompletionSource.Task;
		}
		#endregion
	}
}
