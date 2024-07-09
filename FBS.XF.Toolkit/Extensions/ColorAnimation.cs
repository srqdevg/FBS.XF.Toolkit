using System;
using Xamarin.Forms;

namespace FBS.XF.Toolkit.Extensions
{
	/// <summary>
	/// Color Animation.
	/// Implements the <see cref="Animation" />
	/// </summary>
	/// <seealso cref="Animation" />
	public class ColorAnimation : Animation
	{
		readonly Easing _easing;
		readonly Action _finished;
		readonly Action<double> _step;

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorAnimation"/> class.
		/// </summary>
		public ColorAnimation()
		{
			_easing = Easing.Linear;
			_step = f => { };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorAnimation"/> class.
		/// </summary>
		/// <param name="callback">The callback.</param>
		/// <param name="fromColor">From color.</param>
		/// <param name="toColor">To color.</param>
		/// <param name="easing">The easing.</param>
		/// <param name="finished">The finished.</param>
		public ColorAnimation(Action<Color> callback, Color fromColor, Color toColor, Easing easing = null, Action finished = null)
		{
			_easing = easing ?? Easing.Linear;
			_finished = finished;

			//Func<double, double> transform = AnimationExtensions.Interpolate(start, end);
			Color transform(double t) => Color.FromRgba(
				fromColor.R + t * (toColor.R - fromColor.R),
				fromColor.G + t * (toColor.G - fromColor.G),
				fromColor.B + t * (toColor.B - fromColor.B),
				fromColor.A + t * (toColor.A - fromColor.A));

			_step = f => callback(transform(f));
		}

		/// <summary>
		/// Commits the specified owner.
		/// </summary>
		/// <param name="owner">The owning animation that will be animated.</param>
		/// <param name="name">The name, or handle, that is used to access and track the animation and its state.</param>
		/// <param name="rate">The time, in milliseconds, between frames.</param>
		/// <param name="length">The number of milliseconds over which to interpolate the animation.</param>
		/// <param name="easing">The easing function to use to transision in, out, or in and out of the animation.</param>
		/// <param name="finished">An action to call when the animation is finished.</param>
		/// <param name="repeat">A function that returns true if the animation should continue.</param>
		/// <remarks>To be added.</remarks>
		public new void Commit(IAnimatable owner, string name, uint rate = 16, uint length = 250, Easing easing = null, Action<double, bool> finished = null,
			Func<bool> repeat = null)
		{
			owner.Animate(name, this, rate, length, easing, finished, repeat);
		}

		/// <summary>
		/// Gets the callback.
		/// </summary>
		/// <returns>A callback that recursively runs the eased animation step on this <see cref="T:Xamarin.Forms.Animation" /> object and those of its children that have begun and not finished.</returns>
		/// <remarks>To be added.</remarks>
		public new Action<double> GetCallback()
		{
			Action<double> result = f =>
			{
				_step(_easing.Ease(f));
				////foreach (Animation animation in _children)
				////{
				////	if (animation._finishedTriggered)
				////		continue;

				////	double val = Math.Max(0.0f, Math.Min(1.0f, (f - animation._beginAt) / (animation._finishAt - animation._beginAt)));

				////	if (val <= 0.0f) // not ready to process yet
				////		continue;

				////	Action<double> callback = animation.GetCallback();
				////	callback(val);

				////	if (val >= 1.0f)
				////	{
				////		animation._finishedTriggered = true;
				////		if (animation._finished != null)
				////			animation._finished();
				////	}
				////}
			};
			return result;
		}
	}
}
