namespace Common.Common.Api
{
    public static class DefaultDistribWeb
    {
        public static WebApplication CreateWebAplication(Action<WebApplicationBuilder>? webAppBuilder = null)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            if (webAppBuilder != null)
            {
                webAppBuilder.Invoke(builder);
            }

            return builder.Build();
        } 

    public static void RunWebAplication(WebApplication webApplication)
        {
            // Configure the HTTP request pipeline.
            if (webApplication.Environment.IsDevelopment())
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI();
            }
            webApplication.UseHttpsRedirection();
            webApplication.UseAuthorization();
            webApplication.MapControllers();
            webApplication.Run();
        }
    }
}
