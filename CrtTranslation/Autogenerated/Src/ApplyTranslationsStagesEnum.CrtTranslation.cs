namespace Terrasoft.Configuration.Translation
{

	#region Enum: RecordEditMode

	public enum ApplyTranslationsStagesEnum
	{
		Initializing = 1,
		CleanUnusedReferences = 2,
		ProcessForceApply = 3,
		CheckAnyConfigurationResourcesChanged = 4,
		ApplyTranslations = 5,
		GenerateStaticContent = 6,
		VerifyTranslations = 7,
		CorrectInvalidTranslations = 8,
		Completed = 20,
	}

	#endregion

}
