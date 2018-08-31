namespace DevCut.Contracts.Interfaces
{
	public interface IProgramManager
	{
		void CreateNewProgram(IProgram create);
		void EditProgram(IProgram edit);
		void DeleteExistingProgram(IProgram delete);
	}
}
