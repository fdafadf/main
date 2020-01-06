using AI.MonteCarlo;
using Demos.Forms.Utilities;
using Games.Go;
using Games.Sgf;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GoMcts = AI.MonteCarlo.MCTreeSearch<Games.Go.GoGame, Games.Go.GameState, Games.Go.FieldCoordinates, Games.Go.Stone>;

namespace Demos.Forms.Go.Game
{
    public partial class GoGameForm : Form
    {
        public GameTreeNavigator<MCTreeSearchAsGameTree<GoGame, GameState, FieldCoordinates, Stone>, GameState, FieldCoordinates, Stone, MCTreeNode<FieldCoordinates, GameState>> MainNavigator1;
        public GamePlayoutNavigator<GameState, FieldCoordinates, Stone> PlayoutNavigator;

        public GoGameForm()
        {
            InitializeComponent();

            Random random = new Random(0);

            FieldCoordinates optimizedActionRandomizer(GameState gameState)
            {
                var allowedActions = gameState.GetAllowedActionsForRandomPlayout();
                return allowedActions.Any() ? random.Next(allowedActions) : FieldCoordinates.Pass;
            }

            var rootNode = new GamePlayoutNode<GameState, FieldCoordinates>(new GameState(9));
            var playoutTree = new GamePlayoutTree<GameState, FieldCoordinates, Stone>(rootNode);
            var mcts = new GoMcts(new GoGame(), Stone.Black, new GameState(9), random, optimizedActionRandomizer);
            MainNavigator1 = new MCTreeNavigator<GoGame, GameState, FieldCoordinates, Stone>(mcts);
            PlayoutNavigator = new GamePlayoutNavigator<GameState, FieldCoordinates, Stone>(playoutTree);
            new GameTreeNavigationController<MCTreeSearchAsGameTree<GoGame, GameState, FieldCoordinates, Stone>, GoGame, GameState, FieldCoordinates, Stone, MCTreeNode<FieldCoordinates, GameState>>(MainNavigator1, mainBoardNavigationScroll);
            new GameTreeNavigationController<GamePlayoutTree<GameState, FieldCoordinates, Stone>, GoGame, GameState, FieldCoordinates, Stone, GamePlayoutNode<GameState, FieldCoordinates>>(PlayoutNavigator, playoutScrollBar);
            goBoardControl1.OnAction += MainBoardControl_OnAction;
            InitializePreparedPositionControl();
            MainNavigator1.Forwarded += MainNavigator_Forwarded;
            MainNavigator1.Navigated += MainNavigator_Navigated;
            PlayoutNavigator.Forwarded += PlayoutNavigator_Forwarded;
            PlayoutNavigator.Navigated += PlayoutNavigator_Navigated;
        }

        public void InitializePreparedPositionControl()
        {
            preparedPositionsControl.SelectedIndexChanged += PreparedPositions_SelectedIndexChanged;
            DirectoryInfo preparedPositionsDirectory = new DirectoryInfo(Settings.GoPreparedPositionsDirectoryPath);
            SgfReader sgfParser = new SgfReader();

            foreach (FileInfo file in preparedPositionsDirectory.EnumerateFiles("*.sgf"))
            {
                Collection collection = sgfParser.ReadStream(file.OpenRead());
                GameTree gameTree = collection.Games.First();
                FieldCoordinates[] position = ToPosition(gameTree);
                preparedPositionsControl.Items.Add(new NamedObject<FieldCoordinates[]>(file.Name, position));
            }
        }

        private void MainNavigator_Navigated(object sender, GameTreePath<FieldCoordinates> track)
        {
            GoBoardControlFieldFeaturesHelper.RefreshBoard(goBoardControl1, MainNavigator1);
        }

        private void MainNavigator_Forwarded(object sender, FieldCoordinates action)
        {
            GoBoardControlFieldFeaturesHelper.RefreshBoard(goBoardControl1, MainNavigator1);
        }

        private void PlayoutNavigator_Navigated(object sender, GameTreePath<FieldCoordinates> track)
        {
            GoBoardControlFieldFeaturesHelper.RefreshBoard(playoutBoardControl, PlayoutNavigator.CurrentNode);
        }

        private void PlayoutNavigator_Forwarded(object sender, FieldCoordinates action)
        {
            GoBoardControlFieldFeaturesHelper.RefreshBoard(playoutBoardControl, PlayoutNavigator.CurrentNode);
        }

        private void MainBoardControl_OnAction(object sender, Point e)
        {
            FieldCoordinates action = FieldCoordinates.Get((uint)e.X, (uint)e.Y);
            MainNavigator1.GameTree.Expand(MainNavigator1.CurrentNode, action);
            MainNavigator1.Forward(this, action);
        }

        private void PreparedPositions_SelectedIndexChanged(object sender, EventArgs e)
        {
            NamedObject<FieldCoordinates[]> selectedItem = preparedPositionsControl.SelectedItem as NamedObject<FieldCoordinates[]>;

            if (selectedItem != null)
            {
                var parentNode = MainNavigator1.GameTree.Root;

                foreach (var action in selectedItem.Value)
                {
                    var childNode = MainNavigator1.GameTree.Expand(parentNode, action);
                    parentNode = childNode;
                }

                MainNavigator1.NavigateFromRoot(sender, selectedItem.Value);
            }
        }

        private FieldCoordinates[] ToPosition(GameTree gameTree)
        {
            List<FieldCoordinates> result = new List<FieldCoordinates>();

            while (gameTree != null)
            {
                foreach (var node in gameTree.Sequence.Nodes)
                {
                    foreach (var color in new[] { "B", "W" })
                    {
                        var m = node.Properties.FirstOrDefault(p => color.Equals(p.Ident.Text, StringComparison.OrdinalIgnoreCase));

                        if (m != null)
                        {
                            result.Add(FieldCoordinates.ParseSgf(m.Values.First()));
                        }
                    }
                }

                gameTree = null;
            }

            return result.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var mcts = MainNavigator1.GameTree.Mcts;
            var rounds = mcts.RoundWithDetails((int)numberOfPlayoutsControl.Value);
            playoutListControl.Items.Clear();
            int playoutNumber = 1;
            var whiteTrigger = FieldCoordinates.Get(0, 5);

            foreach (var round in rounds)
            {
                string trigger = " ";
                var played = round.Playout?.FirstOrDefault(m => whiteTrigger.Equals(m.Item1));

                if (played != null)
                {
                    if (played.Item2.CurrentPlayer.Opposite.Color.State == FieldState.White)
                    {
                        trigger = "*";
                    }
                }

                string result = round.GetLastGameState().GetWinner().Color.State == FieldState.Black ? "B" : "W";
                playoutListControl.Items.Add(new NamedObject<MCTreeSearchRound<FieldCoordinates, GameState>>($"{result}{trigger} Playout {playoutNumber}", round));
                playoutNumber++;
            }

            GoBoardControlFieldFeaturesHelper.RefreshBoard(goBoardControl1, MainNavigator1);
        }

        private void playoutListControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            NamedObject<MCTreeSearchRound<FieldCoordinates, GameState>> selectedRound = playoutListControl.SelectedItem as NamedObject<MCTreeSearchRound<FieldCoordinates, GameState>>;
            Utilities.EnsurePathAndNavigate(sender, PlayoutNavigator, selectedRound?.Value);
        }
    }
}
