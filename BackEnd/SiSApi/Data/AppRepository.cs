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
				var retro = await _context.Retrospectives.FindAsync(model.Id);
				_context.Entry(retro).State = EntityState.Modified;
				retro = model;
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
				var retro = await _context.Retrospectives.FirstOrDefaultAsync(r => r.Name == model.Name);
				if (retro == null)
					return false;

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
				var restro = await _context.Retrospectives.FindAsync(guid);
				if (restro == null)
				{
					throw new Exception();
				}

				model.Retrospective = restro;
				model.RetrospectiveId = guid;
				_context.Feedbacks.Add( model );
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
	}
}
