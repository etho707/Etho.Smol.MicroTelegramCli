// See https://aka.ms/new-console-template for more information
using Etho.Smol.MicroTelegramCli;
using System.Text;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

Console.WriteLine("Enter Bot Token");
var token = Console.ReadLine();


var Bot = new TelegramBotClient(token);

User me = await Bot.GetMeAsync();
Console.Title = me.Username ?? "My awesome Bot";

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new() { AllowedUpdates = { } };
Bot.StartReceiving(Handlers.HandleUpdateAsync,
                   Handlers.HandleErrorAsync,
                   receiverOptions,
                   cts.Token);

Console.WriteLine($"Start listening for @{me.Username}");

while (true)
{
    Console.Write("etho:~$ ");
    var msg = Console.ReadLine();
    await Handlers.Say("etho: " + HttpUtility.UrlEncode(msg) + " url");
}

// Send cancellation request to stop bot
cts.Cancel();