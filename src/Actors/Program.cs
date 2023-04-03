// See https://aka.ms/new-console-template for more information

using System.Globalization;

using Akka.Actor;

var system = ActorSystem.Create("tets");

var actor = system.ActorOf<PingActor>();

var response = await actor.Ask<string>(new PingActor.Msg("Test"));

Console.WriteLine(response);

class PingActor : ReceiveActor
{
    public class Msg
    {
        public string Message { get; }

        public Msg(string message)
        {
            Message = message;
        }
    }


    public PingActor()
    {
        Receive<Msg>(msg =>
        {
            var date = new DateOnly(2002, 1, 1);
            var strt = date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            Context.Sender.Tell($"Pong {msg.Message} {strt}");
        });
    }
}