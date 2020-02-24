using AI;
using Games.TicTacToe;
using Games.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Demos.Forms.TicTacToe.Game
{
    public partial class TicTacToeGameSettingsForm : Form
    {
        public TicTacToeGameSettingsForm()
        {
            InitializeComponent();
        }

        public TicTacToeGameSettingsForm(IDictionary<string, Func<IActionGenerator<GameState, Player, GameAction>>> engines) : this()
        {
            noughtPlayerControl.Items.Clear();
            crossPlayerControl.Items.Clear();
            var engineList = engines.Select(e => new NamedObject<Func<IActionGenerator<GameState, Player, GameAction>>>(e.Key, e.Value)).ToArray();
            noughtPlayerControl.Items.Add("Human");
            noughtPlayerControl.Items.AddRange(engineList);
            crossPlayerControl.Items.Add("Human");
            crossPlayerControl.Items.AddRange(engineList);
            noughtPlayerControl.SelectedIndex = 0;
            crossPlayerControl.SelectedIndex = 1;
        }

        public IActionGenerator<GameState, Player, GameAction> Nought;
        public IActionGenerator<GameState, Player, GameAction> Cross;

        private void noughtPlayerControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void crossPlayerControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool canBeClosed = true;

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Nought = LoadAIEngine(noughtPlayerControl);
            Cross = LoadAIEngine(crossPlayerControl);
        }

        private IActionGenerator<GameState, Player, GameAction> LoadAIEngine(ComboBox playerControl)
        {
            if ("Human" == (playerControl.SelectedItem as string))
            {
                return null;
            }
            else if (playerControl.SelectedItem is NamedObject<Func<IActionGenerator<GameState, Player, GameAction>>> item)
            {
                var engine = item.Value();

                if (engine == null)
                {
                    MessageBox.Show("Engine load failed");
                    canBeClosed = false;
                }

                return engine;
            }
            else
            {
                canBeClosed = false;
                return null;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (canBeClosed == false)
            {
                e.Cancel = true;
                canBeClosed = true;
            }
        }
    }
}
