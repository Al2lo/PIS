var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/LAD", (string ParmA,string ParmB) => $"GET-Http-XYZ:ParmA = {ParmA}, ParmB = {ParmB}");
app.MapPost("/LAD", (string ParmA, string ParmB) => $"GET-Http-XYZ:ParmA = {ParmA}, ParmB = {ParmB}");
app.MapPut("/LAD", (string ParmA, string ParmB) => $"GET-Http-XYZ:ParmA = {ParmA}, ParmB = {ParmB}");

app.MapPost("/sum", async (context) =>
{
    int x = Int32.Parse(context.Request.Query["X"]);
    int y = Int32.Parse(context.Request.Query["Y"]);
    Console.WriteLine(x + y);
    await context.Response.WriteAsync($"{x + y}");

});

app.MapGet("/xml", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("./wwwroot/XMLMult.html"); 
});

app.MapGet("/form", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html";
    await context.Response.SendFileAsync("./wwwroot/formMylt.html");
});

app.MapPost("/mult", async (HttpContext context) =>
{
    int x = 0;
    int y = 0;
    using (StreamReader reader = new StreamReader(context.Request.Body))
    {
        string requestBody = await reader.ReadToEndAsync();

        x = Int32.Parse(requestBody.Split("\"")[1]);
        y = Int32.Parse(requestBody.Split("\"")[3]);
    }
    await context.Response.WriteAsJsonAsync(x * y);
});

app.Run();