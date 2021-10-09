using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LightFeatherCodeChallenge.Data;
using Microsoft.Extensions.Options;

namespace LightFeatherCodeChallenge.Controllers
{
	public class SupervisorController : Controller
	{
		private readonly string? baseUrl;

		public SupervisorController(IOptions<DataServerOption> options)
		{
			baseUrl = options.Value.BaseUrl;
		}

		[HttpGet("api/supervisors")]
		public async Task<string> GetSupervisors()
		{ 
			using var client = new HttpClient();
			var responseStream = await client.GetStreamAsync($"{baseUrl}/api/managers");
			var supervisors = await JsonSerializer.DeserializeAsync<List<Supervisor>>(responseStream, new JsonSerializerOptions() {
				PropertyNameCaseInsensitive = true,
			});

			if (supervisors == null) {
				return String.Empty;
			}

			return String.Join(
				Environment.NewLine,
				supervisors
					.Where(x => !Int32.TryParse(x.Jurisdiction, out _))
					.OrderBy(x => x.Jurisdiction)
					.ThenBy(x => x.LastName)
					.ThenBy(x => x.FirstName)
					.Select(x => $"\"{x.Jurisdiction} - {x.LastName}, {x.FirstName}\"")
			);
		}

		[HttpPost("api/submit")]
		public IActionResult SubmitData(SubmitInfo data)
		{
			if (ModelState.IsValid) {
				Console.WriteLine($"FirstName: {data.FirstName}");
				Console.WriteLine($"LastName: {data.LastName}");
				Console.WriteLine($"Supervisor: {data.Supervisor}");
				if (data.Email != null) {
					Console.WriteLine($"Email: {data.Email}");
				}
				if (data.PhoneNumber != null) {
					Console.WriteLine($"PhoneNumber: {data.PhoneNumber}");
				}
				return Ok();
			} else {
				return BadRequest();
			}
		}
	}
}
