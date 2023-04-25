using SiSApi.Models;

namespace SiSApi.Data
{
	public interface IAppRepository
	{
		Task<List<Retrospective>> GetAll();
		Task<List<Retrospective>> GetByDate(string date);
		Task<Retrospective> GetById(Guid guid);
		Task<bool> AddRetrospective(Retrospective model);
		Task<bool> UpdateRetrospective(Retrospective model);
		Task<bool> DeleteRetrospective(Guid id);
		Task<bool> AddFeedback(Guid id, Feedback model);
	}
}