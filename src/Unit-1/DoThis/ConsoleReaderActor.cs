using System;
using Akka.Actor;

namespace WinTail
{
    /// <summary>
    /// Actor responsible for reading FROM the console. 
    /// Also responsible for calling <see cref="ActorSystem.Terminate"/>.
    /// </summary>
    class ConsoleReaderActor : UntypedActor
    {
        public const string ExitCommand = "exit";
        public const String StartCommand = "start";

        //private IActorRef _consoleWriterActor;
        //private readonly IActorRef _validationActor;

        //public ConsoleReaderActor(IActorRef validationActor)
        //{
        //    _validationActor = validationActor;
        //}

        protected override void OnReceive(object message)
        {
            //var read = Console.ReadLine();
            //if (!string.IsNullOrEmpty(read) && String.Equals(read, ExitCommand, StringComparison.OrdinalIgnoreCase))
            //{
            //    // shut down the system (acquire handle to system via
            //    // this actors context)
            //    Context.System.Terminate();
            //    return;
            //}

            //// send input to the console writer to process and print
            //// YOU NEED TO FILL IN HERE
            //_consoleWriterActor.Tell(read);

            //// continue reading messages from the console
            //// YOU NEED TO FILL IN HERE
            //Self.Tell("continue");

            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }

            GetAndValidateInput();
        }

        #region Internal methods

        private void DoPrintInstructions()
        {
            //Console.WriteLine("Write whatever you want into the console!");
            //Console.WriteLine("Some entries will pass validation, and some won't...\n\n");
            //Console.WriteLine("Type 'exit' to quit this application at any time.\n");
            Console.WriteLine("Please provide the URI of a log file on disk.\n");
        }

        /// <summary>
        /// Reads input from console, validates it, then signals appropriate response
        /// (continue processing, error, success, etc.).
        /// </summary>
        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();

            if (!String.IsNullOrEmpty(message) && String.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // If user typed ExitCommand, shut down the entire actor
                // system (allows the process to exit)
                Context.System.Terminate();

                return;
            }

            // Otherwise, just hand message off to validation actor
            // (by telling its actor ref)
            //_validationActor.Tell(message);
            Context.ActorSelection("akka://MyActorSystem/user/validationActor").Tell(message);
        }

        #endregion Internal methods
    }
}