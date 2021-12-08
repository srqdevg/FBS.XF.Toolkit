using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace FBS.XF.Toolkit.Images
{
	/// <summary>
	/// Svg image source.
	/// </summary>
	/// <remarks>
	/// All rights are associated to https://github.com/muak/SvgImageSource
	/// MIT License
	/// </remarks>
	public class SvgImageSource : ImageSource
	{
		#region Dependency properties
		/// <summary>
		/// The color property.
		/// </summary>
		public static BindableProperty ColorProperty =
			BindableProperty.Create(nameof(Color), typeof(Color), typeof(SvgImageSource), default(Color));
		
		/// <summary>
		/// The height property.
		/// </summary>
		public static BindableProperty HeightProperty =
			BindableProperty.Create(nameof(Height), typeof(double), typeof(SvgImageSource), default(double));  
		
		/// <summary>
		/// The stream function property
		/// </summary>
		public static BindableProperty StreamFuncProperty =
			BindableProperty.Create(nameof(StreamFunc), typeof(Func<CancellationToken, Task<Stream>>), typeof(SvgImageSource));

		/// <summary>
		/// The source property.
		/// </summary>
		public static BindableProperty SourceProperty =
			BindableProperty.Create(nameof(Source), typeof(string), typeof(SvgImageSource));

		/// <summary>
		/// The width property.
		/// </summary>
		public static BindableProperty WidthProperty =
			BindableProperty.Create(nameof(Width), typeof(double), typeof(SvgImageSource), default(double));
		#endregion

		#region UI methods
		/// <summary>
		/// get image stream as an asynchronous operation.
		/// </summary>
		/// <param name="userToken">The user token.</param>
		/// <returns>Stream.</returns>
		public async Task<Stream> GetImageStreamAsync(CancellationToken userToken)
		{
			OnLoadingStarted();
			userToken.Register(CancellationTokenSource.Cancel);

			try
			{
				Stream imageStream;

				using (var stream = await StreamFunc(CancellationTokenSource.Token).ConfigureAwait(false))
				{
					if (stream == null)
					{
						OnLoadingCompleted(false);
						return null;
					}

					imageStream = await SvgUtility.CreateImage(stream, Width, Height, Color);
				}

				OnLoadingCompleted(false);
				return imageStream;
			}
			catch (OperationCanceledException oex)
			{
				OnLoadingCompleted(true);
				Debug.WriteLine($"cancel exception {oex.Message}");
				throw;
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Registers the assembly.
		/// </summary>
		/// <param name="typeHavingResource">Type having resource.</param>
		public static void RegisterAssembly(Type typeHavingResource = null)
		{
			registeredAssembly = typeHavingResource == null ? Assembly.GetCallingAssembly() : typeHavingResource.GetTypeInfo().Assembly;
		}

		/// <summary>
		/// Froms the svg.
		/// </summary>
		/// <returns>The svg.</returns>
		/// <param name="resource">Resource.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="color">Color.</param>
		public static ImageSource FromSvgResource(string resource, double width, double height, Color color = default, Type resourceType = null)
		{
			var assembly = resourceType != null ? resourceType.GetTypeInfo().Assembly : registeredAssembly ?? Assembly.GetCallingAssembly();
			return new SvgImageSource { StreamFunc = GetResourceStreamFunc(resource, assembly), Source = resource, Width = width, Height = height, Color = color };
		}

		/// <summary>
		/// Froms the svg.
		/// </summary>
		/// <returns>The svg.</returns>
		/// <param name="resource">Resource.</param>
		/// <param name="color">Color.</param>
		public static ImageSource FromSvgResource(string resource, Color color = default, Type resourceType = null)
		{
			var assembly = resourceType != null ? resourceType.GetTypeInfo().Assembly : registeredAssembly ?? Assembly.GetCallingAssembly();
			return new SvgImageSource { StreamFunc = GetResourceStreamFunc(resource, assembly), Source = resource, Color = color };
		}

		/// <summary>
		/// Froms the svg URI.
		/// </summary>
		/// <returns>The svg URI.</returns>
		/// <param name="uri">URI.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="color">Color.</param>
		public static ImageSource FromSvgUri(string uri, double width, double height, Color color)
		{
			return new SvgImageSource { StreamFunc = GetHttpStreamFunc(uri), Source = uri, Width = width, Height = height, Color = color };
		}

		/// <summary>
		/// Froms the svg stream.
		/// </summary>
		/// <returns>The svg stream.</returns>
		/// <param name="streamFunc">Stream func.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="color">Color.</param>
		/// <param name="key">Key.</param>
		public static ImageSource FromSvgStream(Func<Stream> streamFunc, double width, double height, Color color, string key = null)
		{
			return new SvgImageSource { StreamFunc = token => Task.Run(streamFunc), Width = width, Height = height, Color = color };
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Gets the resource stream function.
		/// </summary>
		/// <param name="resource">The resource.</param>
		/// <returns>Func&lt;CancellationToken, Task&lt;Stream&gt;&gt;.</returns>
		private static Func<CancellationToken, Task<Stream>> GetResourceStreamFunc(string resource, Assembly assembly)
		{
			var realResource = GetRealResource(resource, assembly);
			
			if (realResource == null)
			{
				return null;
			}

			return token => Task.Run(() => assembly.GetManifestResourceStream(realResource), token);
		}

		/// <summary>
		/// Gets the HTTP stream function.
		/// </summary>
		/// <param name="uri">The URI.</param>
		/// <returns>Func&lt;CancellationToken, Task&lt;Stream&gt;&gt;.</returns>
		private static Func<CancellationToken, Task<Stream>> GetHttpStreamFunc(string uri)
		{
			return token => Task.Run(async () =>
			{
				var response = await httpClient.GetAsync(uri, token);

				if (!response.IsSuccessStatusCode)
				{
					Log.Warning("HTTP Request", $"Could not retrieve {uri}, status code {response.StatusCode}");
					return null;
				}

				// The HttpResponseMessage needs to be disposed of after the calling code is done with the stream 
				// otherwise the stream may get disposed before the caller can use it
				return new StreamWrapper(await response.Content.ReadAsStreamAsync().ConfigureAwait(false), response) as Stream;
			}, token);
		}

		/// <summary>
		/// Gets the real resource.
		/// </summary>
		/// <param name="resource">The resource.</param>
		/// <returns>System.String.</returns>
		private static string GetRealResource(string resource, Assembly assembly)
		{
			var resources = assembly.GetManifestResourceNames();
			var realResource = resources.FirstOrDefault(x => x.EndsWith(resource, StringComparison.CurrentCultureIgnoreCase));
			return realResource;
		}

		/// <summary>
		/// On the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected override void OnPropertyChanged(string propertyName = null)
		{
			if (propertyName == StreamFuncProperty.PropertyName)
			{
				OnSourceChanged();
			}
			else if (propertyName == SourceProperty.PropertyName)
			{
				if (string.IsNullOrWhiteSpace(Source) || StreamFunc != null)
				{
					return;
				}

				StreamFunc = Uri.TryCreate(Source, UriKind.Absolute, out _) ? GetHttpStreamFunc(Source) : GetResourceStreamFunc(Source, registeredAssembly);
			}

			base.OnPropertyChanged(propertyName);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color
		{
			get => (Color) GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public double Height
		{
			get => (double) GetValue(HeightProperty);
			set => SetValue(HeightProperty, value);
		}   
		
		/// <summary>
		/// Gets or sets the screen scale.
		/// </summary>
		/// <value>The screen scale.</value>
		public static float ScreenScale { get; set; }

		/// <summary>
		/// Gets or sets the stream func.
		/// </summary>
		/// <value>The stream func.</value>
		public Func<CancellationToken, Task<Stream>> StreamFunc
		{
			get => (Func<CancellationToken, Task<Stream>>) GetValue(StreamFuncProperty);
			set => SetValue(StreamFuncProperty, value);
		}

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		/// <value>The source.</value>
		public string Source
		{
			get => (string) GetValue(SourceProperty);
			set => SetValue(SourceProperty, value);
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public double Width
		{
			get => (double) GetValue(WidthProperty);
			set => SetValue(WidthProperty, value);
		}
		#endregion

		#region Fields
		private static Assembly registeredAssembly;
		private static readonly Lazy<HttpClient> lazyClient = new Lazy<HttpClient>();
		private static HttpClient httpClient => lazyClient.Value;
		#endregion
	}
}