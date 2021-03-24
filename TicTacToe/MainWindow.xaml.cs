using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members
        /// <summary>
        /// Stores the current results of the cells in the active game
        /// </summary>
        private MarkType[] mResults;
        /// <summary>
        /// True if it is player 1s turn or false if player 2s
        /// </summary>
        private bool mPlayer1Turn;
        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion
        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion
        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }
            // Make sure player 1 starts
            mPlayer1Turn = true;

            //Iterate every button on the grid...
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change all properties to default
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Starts a new game on the click after the previous one finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            // Cast the sender to a button
            var button = (Button)sender;
            // Find the buttons position in the list
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);
            
            // Dont do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.X : MarkType.O;

            // Set button text to result
            button.Content = mPlayer1Turn ? 'X' : 'O';

            // Change O to red
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggle the player turns
            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            // Check for horizontal wins
            //
            // Row - 0
            //

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                return;
            }
            //
            // Row - 1
            //

            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                return;
            }
            //
            // Row - 2
            //

            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                return;
            }
            //Check for vertical wins
            //
            // Column - 0
            //

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                return;
            }
            //
            // Column - 1
            //

            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                return;
            }
            //
            // Column - 2
            //

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                return;
            }
            // Check for diagonal winners
            //
            // Top left to bottom right
            //

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                return;
            }
            //
            // Top right to bottom left
            //

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
                return;
            }

            // Check for no winner and full game board
            if (!mResults.Any(results => results == MarkType.Free))
            {
                // Game ends
                mGameEnded = true;

                // Turns all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });

            }
        }
    }
}
