using DiscordBotTemplateNet8.Config;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace DiscordBotTemplateNet8
{
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        static async Task Main(string[] args)
        {
            var botConfig = new BotConfig();
            await botConfig.ReadJSON();

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = botConfig.DiscordBotToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(config);

            Client.Ready += Client_Ready;

            Client.MessageCreated += Client_MessageCreated;

            Console.WriteLine("Hello Cleaner bot ready, now deleting all messages from Valdura!");

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }

        private static async Task Client_MessageCreated(DiscordClient sender, MessageCreateEventArgs args)
        {
            // Ignore messages from the bot itself
            if (args.Author.IsBot)
                return;

            if (args.Author.Username == "Valdura")
            {
                await args.Message.DeleteAsync();
            }
        }
    }
}