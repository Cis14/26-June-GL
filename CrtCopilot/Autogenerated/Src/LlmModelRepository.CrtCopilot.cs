namespace Creatio.Copilot
{
    using System;
	using Terrasoft.Configuration.GenAI;
	using Terrasoft.Core;
    using Terrasoft.Core.DB;
	using Terrasoft.Core.Factories;
	
    #region Interface: ILlmModelRepository

    public interface ILlmModelRepository
    {
        LlmModelReference FindByCode(string code);
    }

    #endregion

    #region Class: LlmModelRepository

	[DefaultBinding(typeof(ILlmModelRepository))]
    public class LlmModelRepository : ILlmModelRepository
    {
        private readonly UserConnection _userConnection;

        public LlmModelRepository(UserConnection userConnection) {
            _userConnection = userConnection;
        }

        public LlmModelReference FindByCode(string code) {
			if (string.IsNullOrEmpty(code)) {
				return null;
			}
            var select = new Select(_userConnection)
                .Column("Id")
                .Column("Code")
                .From("LlmModel")
                .Where("Code").IsEqual(Column.Parameter(code)) as Select;
            Guid id = select.ExecuteScalar<Guid>();
            if (id == Guid.Empty) {
                return null;
            }
            return new LlmModelReference {
				Id = id,
				Code = code
			};
        }
    }

    #endregion

}

