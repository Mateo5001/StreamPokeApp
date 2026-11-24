using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using PokeStream.Core.Services.AuthServices;
using PokeStream.Core.Services.Cookies;
using PokeStream.Core.Services.Http;
using PokeStream.UI.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMudServices();

// Auth: AuthStateService (Core) acts as the AuthenticationStateProvider, talking to the API.
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
// IHttpClient prefixes every request with ApiBaseUrl, so the raw HttpClient needs no BaseAddress.
builder.Services.AddScoped(_ => new HttpClient());
builder.Services.AddScoped<IHttpClient, ApiHttpClient>();
builder.Services.AddScoped<AuthStateService>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateService>());

// Cookies: plain ones via JS interop, session one HttpOnly via the endpoints mapped below.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICookieService, CookieService>();

// PokeAPI wrapper (PokeApiNet): one shared instance, it caches responses in memory.
builder.Services.AddSingleton<PokeApiNet.PokeApiClient>();

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

// Only the session cookie can be issued here (HttpOnly, so JS never sees the tokens).
app.MapPost(CookieKeys.SessionEndpoint, (SessionCookieRequest body, HttpContext ctx) =>
{
	if (string.IsNullOrEmpty(body.Value) || body.Value.Length > CookieKeys.MaxValueLength)
		return Results.BadRequest();

	ctx.Response.Cookies.Append(CookieKeys.Session, body.Value, new CookieOptions
	{
		HttpOnly = true,
		Secure = ctx.Request.IsHttps,
		SameSite = SameSiteMode.Strict,
		Path = "/",
		Expires = body.Days is > 0 ? DateTimeOffset.UtcNow.AddDays(body.Days.Value) : null
	});
	return Results.NoContent();
});
app.MapDelete(CookieKeys.SessionEndpoint, (HttpContext ctx) =>
{
	ctx.Response.Cookies.Delete(CookieKeys.Session, new CookieOptions { Path = "/" });
	return Results.NoContent();
});

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
