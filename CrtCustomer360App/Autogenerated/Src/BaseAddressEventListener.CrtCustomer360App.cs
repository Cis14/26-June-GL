namespace Terrasoft.Configuration.CrtCustomer360App
{
	using System;
	using System.Linq;
	using Terrasoft.Common;
	using Core.Entities;
	using Core.Entities.Events;

	#region Class: BaseAddressEventListener

	/// <summary>
	/// Listener for 'BaseAddress' entity events.
	/// </summary>
	/// <seealso cref="Terrasoft.Core.Entities.Events.BaseEntityEventListener"/>
	public class BaseAddressEventListener : BaseEntityEventListener
	{

		#region Methods: Private

		private void FillFullAddress(Entity entity) {
			entity.LoadLookupDisplayValues("Country", "Region", "City");
			var zip = entity.GetTypedColumnValue<string>("Zip");
			var country = GetLookupDisplayValue(entity, "Country");
			var region = GetLookupDisplayValue(entity, "Region");
			var city = GetLookupDisplayValue(entity, "City");
			var address = entity.GetTypedColumnValue<string>("Address");
			var fullAddress = new[] { zip, country, region, city, address }.Where(x => x.IsNotNullOrEmpty());
			entity.SetColumnValue("FullAddress", String.Join(", ", fullAddress));
		}

		private string GetLookupDisplayValue(Entity entity, string lookupColumnName)
		{
			var referenceSchema = entity.Schema.Columns.FindByName(lookupColumnName)?.ReferenceSchema;
			var primaryDisplayColumnName = referenceSchema?.PrimaryDisplayColumn?.Name;
			return entity.GetTypedColumnValue<string>($"{lookupColumnName}{primaryDisplayColumnName}");
		}

		#endregion

		#region Methods: Public

		/// <inheritdoc cref="BaseEntityEventListener.OnInserting"/>
		public override void OnInserting(object sender, EntityBeforeEventArgs e) {
			base.OnInserting(sender, e);
			FillFullAddress(sender as Entity);
		}

		/// <inheritdoc cref="BaseEntityEventListener.OnSaving"/>
		public override void OnSaving(object sender, EntityBeforeEventArgs e) {
			base.OnSaving(sender, e);
			FillFullAddress(sender as Entity);
		}

		/// <inheritdoc cref="BaseEntityEventListener.OnUpdating"/>
		public override void OnUpdating(object sender, EntityBeforeEventArgs e) {
			base.OnUpdating(sender, e);
			FillFullAddress(sender as Entity);
		}

		#endregion

	}

	#endregion

} 
