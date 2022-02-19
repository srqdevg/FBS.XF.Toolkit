namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// IForce Orientation
	/// </summary>
	/// <remarks>
	/// All rights belong to 
	/// https://monolara.blogspot.com/2016/11/xamarinforms-force-page-orientation-and.html
	/// MIT License
	/// </remarks>
	public interface IForceOrientation
	{
		#region Public methods
		/// <summary>
		/// Forces the landscape.
		/// </summary>
		void ForceLandscape();

		/// <summary>
		/// Forces the portrait.
		/// </summary>
		void ForcePortrait();

		/// <summary>
		/// Sets the oriention free.
		/// </summary>
		void SetOrientionFree();
		#endregion
	}
}