using System;
using System.IO;

namespace FBS.XF.Toolkit.Images
{
	/// <summary>
	/// StreamWrapper
	/// </summary>
	/// <seealso cref="System.IO.Stream" />
	/// <remarks>
	/// All rights are associated to https://github.com/muak/SvgImageSource
	/// MIT License
	/// </remarks>
	public class StreamWrapper : Stream
	{
		#region Constructors
		/// <summary>
		///     Initializes a new instance of the <see cref="StreamWrapper" /> class.
		/// </summary>
		/// <param name="wrapped">The wrapped.</param>
		/// <param name="additionalDisposable">The additional disposable.</param>
		/// <exception cref="ArgumentNullException">wrapped</exception>
		public StreamWrapper(Stream wrapped, IDisposable additionalDisposable = default)
		{
			this.wrapped = wrapped ?? throw new ArgumentNullException(nameof(wrapped));

			this.additionalDisposable = additionalDisposable;
		}
		#endregion

		#region Override methods
		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream"></see> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			wrapped.Dispose();

			additionalDisposable?.Dispose();
			additionalDisposable = null;

			base.Dispose(disposing);
		}

		/// <summary>
		/// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
		/// </summary>
		public override void Flush()
		{
			wrapped.Flush();
		}

		/// <summary>
		/// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset
		/// and (offset + count - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream.</param>
		/// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not
		/// currently available, or zero (0) if the end of the stream has been reached.</returns>
		public override int Read(byte[] buffer, int offset, int count)
		{
			return wrapped.Read(buffer, offset, count);
		}

		/// <summary>
		/// When overridden in a derived class, sets the position within the current stream.
		/// </summary>
		/// <param name="offset">A byte offset relative to the origin parameter.</param>
		/// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"></see> indicating the reference point used to obtain the new position.</param>
		/// <returns>The new position within the current stream.</returns>
		public override long Seek(long offset, SeekOrigin origin)
		{
			return wrapped.Seek(offset, origin);
		}

		/// <summary>
		/// When overridden in a derived class, sets the length of the current stream.
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes.</param>
		public override void SetLength(long value)
		{
			wrapped.SetLength(value);
		}

		/// <summary>
		/// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The number of bytes to be written to the current stream.</param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			wrapped.Write(buffer, offset, count);
		}
		#endregion

		#region Properties
		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
		/// </summary>
		/// <value><c>true</c> if this instance can read; otherwise, <c>false</c>.</value>
		public override bool CanRead => wrapped.CanRead;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
		/// </summary>
		/// <value><c>true</c> if this instance can seek; otherwise, <c>false</c>.</value>
		public override bool CanSeek => wrapped.CanSeek;

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
		/// </summary>
		/// <value><c>true</c> if this instance can write; otherwise, <c>false</c>.</value>
		public override bool CanWrite => wrapped.CanWrite;

		/// <summary>
		/// When overridden in a derived class, gets the length in bytes of the stream.
		/// </summary>
		/// <value>The length.</value>
		public override long Length => wrapped.Length;

		/// <summary>
		/// When overridden in a derived class, gets or sets the position within the current stream.
		/// </summary>
		/// <value>The position.</value>
		public override long Position
		{
			get => wrapped.Position;
			set => wrapped.Position = value;
		}
		#endregion

		#region Fields
		private readonly Stream wrapped;
		private IDisposable additionalDisposable;
		#endregion
	}
}