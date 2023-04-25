using Microsoft.EntityFrameworkCore;
using SiSApi.Models;
using System;

namespace SiSApi.Data
{
	public class AppRepository : IAppRepository
	{
		private readonly AppDbContext _context;

		public AppRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Retrospective>> GetAll()
		{
			return await _context.Retrospectives
					.Include(r => r.Feedbacks)
				.AsNoTracking()
				.ToListAsync();
		}
		public async Task<List<Retrospective>> GetByDate(string date)
		{
				DateTime.TryParse(date, out DateTime dateChosen);
			return await _context.Retrospectives
				.AsNoTracking()
				.Where(r => r.Date.Date == dateChosen.Date)
				.ToListAsync();
		}


		private bool ItemExists(Guid id)
		{
			return _context.Retrospectives.Any(e => e.Id == id);
		}


		public async Task<bool> UpdateRetrospective(Retrospective model)
		{
			try
			{
				var item = await _context.Retrospectives.FirstOrDefaultAsync(r => r.Id == model.Id);
				_context.Entry(item).State = EntityState.Modified;
				item = model;
				await _context.SaveChangesAsync();

			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ItemExists(model.Id))
				{
					return false;
				}
				else
				{
					throw;
				}
			}
			return true;
		}

		public async Task<Retrospective> GetById(Guid guid)
		{
			return await _context.Retrospectives
				.Include(r => r.Feedbacks)
				.AsNoTracking()
				.FirstOrDefaultAsync(r => r.Id == guid);
		}

		public async Task<bool> AddRetrospective(Retrospective model)
		{
			try
			{
				await _context.Retrospectives.AddAsync(model);
				await _context.SaveChangesAsync();

			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ItemExists(model.Id))
				{
					return false;
				}
				else
				{
					throw;
				}
			}
			return true;
		}

		public async Task<bool> DeleteRetrospective(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> AddFeedback(Guid guid, Feedback model)
		{
			try
			{
				var repo = await _context.Retrospectives.AsNoTracking().FirstOrDefaultAsync(r => r.Id == guid); 
				repo.Feedbacks.Add(
				new Feedback()
				{
					RetrospectiveId = guid,
					Body = model.Body,
					FeedbackType = model.FeedbackType,
					Name = model.Name
				});
				//_context.Entry(repo).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				 repo = await _context.Retrospectives.AsNoTracking().FirstOrDefaultAsync(r => r.Id == guid);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ItemExists(model.Id))
				{
					return false;
				}
				else
				{
					throw;
				}
			}
			return true;
		}
	}
}
