using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace laba_1b
{
    public static class MessageFunc
    {
        public static ParameterizedThreadStart Recieve = async (Object? ws) =>
        {
            int k = 0;
            WebSocket? xws = (WebSocket?)ws;
            byte[] buffer = new byte[4096];
            string message = string.Empty;
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.WriteLine("Start Trace");
            while(xws != null && xws.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await xws.ReceiveAsync(buffer, CancellationToken.None);
                message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
                Trace.WriteLine(message);
            }
            Trace.WriteLine("Finish Trace");
        };

        public static ParameterizedThreadStart Send = async (Object? ws) =>
        {
            int k = 0;
            WebSocket? xws = (WebSocket?)ws;
            byte[] buffer = new byte[4096];
            while (xws != null && xws.State == WebSocketState.Open)
            {
                DateTime time = DateTime.Now;
                buffer = Encoding.ASCII.GetBytes(string.Format(time.Hour.ToString()+ ":" + time.Minute.ToString() + ":" + time.Second.ToString()));
                await xws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(2000);
            }
        };
    }
}
