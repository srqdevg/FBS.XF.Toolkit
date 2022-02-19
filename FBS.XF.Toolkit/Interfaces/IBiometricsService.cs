namespace FBS.XF.Toolkit.Interfaces
{
	/// <summary>
	/// IBiometric Service
	/// </summary>
	public interface IBiometricService
	{
		#region Methods
		/// <summary>
		/// Authenticates this instance.
		/// </summary>
		/// <returns><c>true</c> if authenticated, <c>false</c> otherwise.</returns>
		bool Authenticate();

		/// <summary>
		/// Determines whether this instance is available.
		/// </summary>
		/// <returns><c>true</c> if this instance is available; otherwise, <c>false</c>.</returns>
		bool IsAvailable();
		#endregion
	}
}
