using laba_1b;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseWebSockets();
app.UseStaticFiles();
app.MapGet("/tws", async (HttpContext context) =>
{
    Thread threadSend = new Thread(new System.Threading.ParameterizedThreadStart(MessageFunc.Send));
    Thread threadRecieve = new Thread(new System.Threading.ParameterizedThreadStart(MessageFunc.Recieve));
    byte[] byffer = new byte[4096];

    if(context.WebSockets.IsWebSocketRequest)
    {
        WebSocket ws =await context.WebSockets.AcceptWebSocketAsync();
        WebSocketReceiveResult result = await ws.ReceiveAsync(byffer, CancellationToken.None);
        string message = System.Text.Encoding.UTF8.GetString(byffer, 0, result.Count);
        threadSend.Start(ws);
        threadRecieve.Start(ws);
        threadSend.Join();
        threadRecieve.Join();
    }
});


app.Run();
