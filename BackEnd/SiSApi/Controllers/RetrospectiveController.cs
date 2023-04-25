using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiSApi.Data;
using SiSApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiSApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RetrospectiveController : ControllerBase
	{
		private readonly ILogger<RetrospectiveController> _logger;
		private readonly IAppRepository _appRepository;

		public RetrospectiveController(IAppRepository appRepository, ILogger<RetrospectiveController> logger)
		{
			_logger = logger;
			_appRepository = appRepository;

		}
		// GET: api/<RetrospectiveController>
		[HttpGet]
		public async Task<IEnumerable<Retrospective>> Get()
		{
			return await _appRepository.GetAll();
		}

		// GET api/<RetrospectiveController>/5
		[HttpGet("GetById/{id}")]
		public async Task<Retrospective> GetById(Guid id)
		{
			try
			{
				return await _appRepository.GetById(id);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex.StackTrace);
				throw;
			}
		}

		// GET api/<RetrospectiveController>/2023-04-20
		[HttpGet("GetByDate/{date}")]
		public async Task<IEnumerable<Retrospective>> GetByDate(string date)
		{
			return await _appRepository.GetByDate(date);
		}
		// POST api/<RetrospectiveController>
		[HttpPost]
		[ProducesResponseType(typeof(Retrospective), 200)]
		public async Task<ActionResult<Retrospective>> Post([FromBody] Retrospective model)
		{
			if (model.Date == DateTime.MinValue)
				return BadRequest("Date is required");

			if (String.IsNullOrEmpty(model.Participants.ToString()))
				return BadRequest("Atleast one Participant is required");

			await _appRepository.AddRetrospective(model);
			return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
		}

		[HttpPut("AddFeedback/{id}")]
		public async Task<IActionResult> AddFeedback(Guid id, [FromBody] Feedback model)
		{ 
			if ( await _appRepository.AddFeedback(id, model))
				return Ok();

			return BadRequest("Something went wrong");
		}


		// PUT api/<RetrospectiveController>/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id, [FromBody] Retrospective model)
		{
			await _appRepository.UpdateRetrospective(model);
			return Ok();

		}

		// DELETE api/<RetrospectiveController>/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _appRepository.DeleteRetrospective(id);
			return Ok();
		}
	}
}
