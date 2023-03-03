// See https://aka.ms/new-console-template for more information

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
            Context.Sender.Tell($"Pong {msg.Message}");
        });
    }
}