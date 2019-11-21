using System;
using System.Collections.Generic;

namespace DesignPatterns_ChainOfResponsibility
{
    //original source https://refactoring.guru/design-patterns/chain-of-responsibility/csharp/example
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Design Patterns - Chain of responsibility!");

            var monkey = new MonkeyHandler();
            var squirrel = new SquirrelHandler();
            var dog = new DogHandler();

            monkey.SetNext(squirrel).SetNext(dog);

            Console.WriteLine("Chain: Monkey > Squirrel > Dog\n");
            Client.ClientCode(monkey);
            Console.WriteLine();

            Console.WriteLine("Subchain: Squirrel > Dog\n");
            Client.ClientCode(squirrel);
        }


    }

    public interface IHandler
    {
        IHandler SetNext(IHandler handler);

        object Handle(object request);
    }


    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }


    class MonkeyHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "banana")
            {
                return $"Monkey : I'll eat the {request.ToString()}\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class DogHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "meat")
            {
                return $"Dog : I'll eat the {request.ToString()}\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    class SquirrelHandler : AbstractHandler
    {
        public override object Handle(object request)
        {
            if ((request as string) == "nut")
            {
                return $"Squirrel : I'll eat the {request.ToString()}\n";
            }
            else
            {
                return base.Handle(request);
            }
        }
    }

    public class Client
    {
        public static void ClientCode(AbstractHandler handler)
        {
            var foodList = new List<string>() {"meat", "nut", "coffee" };

            foreach (var food in foodList)
            {
                Console.WriteLine($"Who wants a {food}?");

                var result = handler.Handle(food);

                if (result != null)
                {
                    Console.WriteLine($" {result}");
                }
                else
                {
                    Console.WriteLine($" Nobody wants {food}");
                }
            }
        }
    }



}
