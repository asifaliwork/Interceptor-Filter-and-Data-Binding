using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace Header.Models
{
	public class Middleware
	{
		private readonly RequestDelegate _next;

		public Middleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext Context)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			Context.Response.OnStarting( state =>
			{
                var httpContext = (HttpContext)state;
                stopwatch.Stop();
				httpContext.Response.Headers.Add("Action-Name", httpContext.Request.RouteValues["action"] as string);
				httpContext.Response.Headers.Add("HTTP-Method", httpContext.Request.Method);
				httpContext.Response.Headers.Add("HTTP-Scheme", httpContext.Request.Scheme);
			    httpContext.Response.Headers.Add("Host", httpContext.Request.Host.ToString());
				httpContext.Response.Headers.Add("Port", httpContext.Request.Host.Port.ToString());
				httpContext.Response.Headers.Add("Server-DateTime", httpContext.Request.Headers.Date);
                httpContext.Response.Headers.Remove("Content-Type");
				//httpContext.Response.Headers.Add("HTTP-Transfer-Encoding", httpContext.Request.Headers.TransferEncoding.ToString());
				//httpContext.Response.Headers.Remove("Content-Length");
               
               
                httpContext.Response.Headers["Execution-Time"] = $"{stopwatch.ElapsedMilliseconds} ms";
				return  Task.CompletedTask;
			}, Context);

			
			await  _next(Context);

		}
	}

	
	//public static class MiddlewareExtensions
	//{
	//	public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
	//	{
	//		return builder.UseMiddleware<Middleware>();
	//	}
	//}
}
