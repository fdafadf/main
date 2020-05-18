using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labs.Agents.Demo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DemoForm());
        }

        /*
        #region Przykłady (Pseudokod)

        class PrecalculatedAgent : IAgent<Environment2<PrecalculatedAgent, PrecalculatedAgentState>, PrecalculatedAgent, PrecalculatedAgentState>
        {
            public Action2[] PrecalculatedActions;
            public PrecalculatedAgentState State { get; }

            public PrecalculatedAgent()
            {
                State = new PrecalculatedAgentState();
            }

            public void Precalculate()
            {
                PrecalculatedActions = null; // w tym przypadku wyliczamy całą śieżkę do celu
            }

            private void Examples()
            {
                // Gdzie się znajduje agent.
                var agentX = State.Field.X;
                var agentY = State.Field.Y;
                // Czy pole (12, 34) jest puste?
                var isEmpty = State.Field.Environment[12, 34].IsEmpty;
                // Czy pole (12, 34) jest przeszkodą?
                var isObstacle = State.Field.Environment[12, 34].IsObstacle;
                // Czy pole (12, 34) jest agentem?
                var isAgent = State.Field.Environment[12, 34].IsAgent;
                // Czy pole (-1, 50) jest poza środowiskiem?
                var isOutside = State.Field.Environment[12, 34].IsOutside;
                // Czy agent został dodany do środowiska?
                // Agent który nie został dodany nie znajduje się na żadnym polu.
                var isAgentAttachedToEnvironment = State.Field != null;
            }
        }

        // Warto utworzyć sklasę dla stanu agenta, choćy po to aby pozbyć się parametrów typowych.
        // Można dodawać właściwości które opisują stan agenta i np. są przydatne są punktu widzenia 
        // zastosowanego algorytmu wyliczającego akcje.
        class PrecalculatedAgentState : AgentState2<Environment2<PrecalculatedAgent, PrecalculatedAgentState>, PrecalculatedAgent, PrecalculatedAgentState>
        {
        }

        static void AgentWithPrecalculatedActionsExample()
        {
            var random = new Random();
            // Tworzymy środowisko o rozmiarze 100x100
            var environment = new Environment2<PrecalculatedAgent, PrecalculatedAgentState>(new Random(0), 100, 100);
            // Tworzymy 100 agentów
            var agents = new PrecalculatedAgent[100];
            // Aby wykonać pojedynczy krok symulacji, potrzebujemy opisać interakcje agentów ze środowiskiem.
            // Podczas dodawania agenta do środowiska otrzymujemy obiekt który pozwala opisać interakcję agenta.
            var interactions = new List<AgentInteraction<PrecalculatedAgent, Action2, InteractionResult>>();

            for (int i = 0; i < agents.Length; i++)
            {
                agents[i] = new PrecalculatedAgent();
                var position = environment.GetRandomUnusedPosition();
                var interaction = environment.AddAgent(agents[i], position);
                interactions.Add(interaction);
            }

            // Wyliczamy z góry wszystkie akcje agentów 

            for (int i = 0; i < agents.Length; i++)
            {
                agents[i].Precalculate();
            }

            var iterationStep = 0;

            while (AllAgentsFinished() == false) // ten warunek jest tylko przykładowy
            {
                foreach (var interaction in interactions)
                {
                    // Ustawiamy akcję dla bieżacego kroku
                    interaction.Action = interaction.Agent.PrecalculatedActions[iterationStep];
                }
                
                // Przekazujemy akcję wszystkich agentów dla bieżącego kroku
                environment.Apply(interactions);
                iterationStep++;
            }
        }

        class OnlineAgent : IAgent<Environment2<OnlineAgent, OnlineAgentState>, OnlineAgent, OnlineAgentState>
        {
            public OnlineAgentState State { get; }

            public Action2 CalculateAction()
            {
                return Action2.MoveNorth;
            }
        }

        class OnlineAgentState : AgentState2<Environment2<OnlineAgent, OnlineAgentState>, OnlineAgent, OnlineAgentState>
        {
        }

        static void OnlineAgentExample()
        {
            var environment = new Environment2<OnlineAgent, OnlineAgentState>(new Random(0), 400, 400);
            var agents = new OnlineAgent[100];
            var interactions = new List<AgentInteraction<OnlineAgent, Action2, InteractionResult>>();
            var iteration = 0;

            while (AllAgentsFinished() == false)
            {
                foreach (var interaction in interactions)
                {
                    interaction.Action = interaction.Agent.CalculateAction();
                }

                environment.Apply(interactions);
                iteration++;
            }
        }

        static bool AllAgentsFinished()
        {
            return false;
        }

        #endregion
        */
    }
}
