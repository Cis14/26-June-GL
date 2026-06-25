namespace Creatio.Copilot
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Common.Logging;
	using Terrasoft.Core;
	using Terrasoft.Core.Factories;

	#region Interface: IToolFilterResolver

	/// <summary>
	/// Resolves serializable filter descriptors into runtime tool filters.
	/// </summary>
	public interface IToolFilterResolver
	{

		#region Methods: Public

		IList<IAIToolFilter> Resolve(IEnumerable<AIToolFilterDescriptor> descriptors);

		#endregion

	}

	#endregion

	#region Class: DefaultToolFilterResolver

	/// <summary>
	/// Default implementation of <see cref="IToolFilterResolver"/>.
	/// </summary>
	[DefaultBinding(typeof(IToolFilterResolver))]
	internal class DefaultToolFilterResolver : IToolFilterResolver
	{

		#region Constants: Private

		private const string FromDescriptorMethodName = "FromDescriptor";

		#endregion

		#region Fields: Private

		private readonly UserConnection _userConnection;
		private readonly ILog _log = LogManager.GetLogger("Copilot");

		#endregion

		#region Constructors: Public

		public DefaultToolFilterResolver(UserConnection userConnection) {
			_userConnection = userConnection;
		}

		#endregion

		#region Methods: Private

		private static MethodInfo FindFromDescriptorMethod(Type filterType, Type[] parameterTypes) {
			return filterType.GetMethod(FromDescriptorMethodName, BindingFlags.Public | BindingFlags.Static, null,
				parameterTypes, null);
		}

		private IAIToolFilter Resolve(AIToolFilterDescriptor descriptor) {
			if (descriptor == null) {
				return null;
			}
			if (string.IsNullOrWhiteSpace(descriptor.TypeName)) {
				_log.Warn("Skipping tool filter descriptor because TypeName is empty.");
				return null;
			}
			try {
				var filterType = Type.GetType(descriptor.TypeName, throwOnError: false);
				if (filterType == null) {
					_log.Warn($"Skipping tool filter descriptor because type '{descriptor.TypeName}' was not found.");
					return null;
				}
				MethodInfo fromDescriptorMethod = FindFromDescriptorMethod(filterType,
					new[] { typeof(AIToolFilterDescriptor), typeof(UserConnection) });
				if (fromDescriptorMethod != null) {
					return fromDescriptorMethod.Invoke(null,
						new object[] { descriptor, _userConnection }) as IAIToolFilter;
				}
				fromDescriptorMethod = FindFromDescriptorMethod(filterType, new[] { typeof(AIToolFilterDescriptor) });
				if (fromDescriptorMethod != null) {
					return fromDescriptorMethod.Invoke(null, new object[] { descriptor }) as IAIToolFilter;
				}
				return Activator.CreateInstance(filterType) as IAIToolFilter;
			} catch (Exception exception) {
				_log.Warn($"Skipping tool filter '{descriptor.TypeName}' because it could not be resolved.", exception);
				return null;
			}
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc />
		public IList<IAIToolFilter> Resolve(IEnumerable<AIToolFilterDescriptor> descriptors) {
			return descriptors?
				.Select(Resolve)
				.Where(filter => filter != null)
				.ToList() ?? new List<IAIToolFilter>();
		}

		#endregion

	}

	#endregion

}

