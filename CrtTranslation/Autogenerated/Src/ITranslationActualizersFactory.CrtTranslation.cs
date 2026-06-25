namespace Terrasoft.Configuration.Translation
{

	#region Interface: ITranslationActualizersFactory

	public interface ITranslationActualizersFactory
	{

		#region Methods: Public

		ITranslationActualizer GetTranslationActualizer();

		ITranslationProvider GetTranslationProvider();

		#endregion

	}

	#endregion

}

