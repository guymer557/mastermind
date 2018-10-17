using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Mindmaster
{
    public class GameBoardForm : Form
    {     
        private const string k_ApprovalButtonText = "-->>";
        private const string k_FormTitel = "Bool Pgia";
        private const string k_DuplicateColorMessage = "You have already chosen that color, Please choose again";
        private const short k_HiddenSquaresRow = 0;
        private const short k_ButtonBaseSize = 45;
        private const short k_FirstInnerRow = 2;

        private readonly short r_MaxLengthOfInput;
        private readonly short r_ColumnOfApprovalButton;
        private readonly short r_ColumnOfMatchResults;
        private readonly GameManager r_TheGame;
        
        private ChooseColorForm m_ChooseColorForm = new ChooseColorForm();
        private TableLayoutPanel m_GameBoard = new TableLayoutPanel();
        private int[] m_UserGuessInput;
        private short m_CurrentUserInputLength = 0;
        
        public void InitializeGameBoardForm()
        {
            r_TheGame.StartGame();
            buildGameBoard();
        }

        private TableLayoutPanelCellPosition getSquarePosition(Button i_Button)
        {
            return m_GameBoard.GetCellPosition(i_Button);
        }

        private void buildGameBoard()
        {
            m_ChooseColorForm.BuildColorsTable();
            m_GameBoard.AutoSize = true;
            m_GameBoard.Top = 6;
            m_GameBoard.Left = 6;
            m_GameBoard.ColumnCount = r_MaxLengthOfInput + 2; // Plus 2 because the column of the approval button and the results square
            m_GameBoard.RowCount = r_TheGame.MaxRounds + 2; // Plus 2 because the first line of black squares and the blank line

            // Those methods create the first two lines, the loop of inner lines start from line 3 (index 2)
            buildFirstLineOfBoard(0, r_MaxLengthOfInput - 1);
            addSpaceLineToTableLayoutPanel(k_HiddenSquaresRow + 1);
            for (int row = k_FirstInnerRow; row < m_GameBoard.RowCount; row++)
            {
                buildInnerLineBoard(row);
            }

            Controls.Add(m_GameBoard);
        }

        private void buildFirstLineOfBoard(int i_Row, int i_MaxColumn)
        {
            for (int col = 0; col <= i_MaxColumn; col++)
            {
                Button buttonToAdd = new Button();
                buttonToAdd.Size = new Size(k_ButtonBaseSize, k_ButtonBaseSize);
                buttonToAdd.BackColor = Color.Black;
                buttonToAdd.Enabled = false;
                m_GameBoard.Controls.Add(buttonToAdd, col, i_Row);
            }
        }

        private void addSpaceLineToTableLayoutPanel(int i_Row)
        {
            for (int col = 0; col < m_GameBoard.ColumnCount; col++)
            {
                Label spaceLabel = new Label();
                spaceLabel.AutoSize = true;
                spaceLabel.Text = string.Empty;
                m_GameBoard.Controls.Add(spaceLabel, col, i_Row);
            }
        }

        private void buildInnerLineBoard(int i_Row)
        {
            for (int col = 0; col < m_GameBoard.ColumnCount; col++)
            {
                Button buttonToAdd = new Button();
                if (col >= 0 && col < r_MaxLengthOfInput)
                {
                    if (i_Row == k_FirstInnerRow)
                    {
                        buttonToAdd.Enabled = true;
                    }
                    else
                    {
                        buttonToAdd.Enabled = false;
                    }

                    buttonToAdd.Size = new Size(k_ButtonBaseSize, k_ButtonBaseSize);
                    buttonToAdd.Name = col.ToString();
                    buttonToAdd.Click += buttonGuessColor_Click;
                }
                else if (col == r_ColumnOfApprovalButton)
                {
                    buttonToAdd.Text = k_ApprovalButtonText;
                    buttonToAdd.Size = new Size(k_ButtonBaseSize, k_ButtonBaseSize / 2);
                    buttonToAdd.Anchor = AnchorStyles.None;
                    buttonToAdd.Enabled = false;
                    buttonToAdd.Click += buttonAprroval_Click;
                }
                else
                {
                    buildGuessingResultsSquare(col, i_Row);
                    return;
                }

                m_GameBoard.Controls.Add(buttonToAdd, col, i_Row);
            }
        }

        private void buildGuessingResultsSquare(int i_Column, int i_Row)
        {
            const short k_NumOfSquersInRow = 2;
            TableLayoutPanel resultsTable = new TableLayoutPanel();
            resultsTable.ColumnCount = k_NumOfSquersInRow;
            resultsTable.RowCount = k_NumOfSquersInRow;
            resultsTable.ClientSize = new Size(k_ButtonBaseSize, k_ButtonBaseSize);
            for (int i = 0; i < k_NumOfSquersInRow; i++)
            {
                for (int j = 0; j < k_NumOfSquersInRow; j++)
                {
                    Button buttonToAdd = new Button();
                    buttonToAdd.ClientSize = new Size((k_ButtonBaseSize / 2) - resultsTable.Margin.Vertical, (k_ButtonBaseSize / 2) - resultsTable.Margin.Vertical);
                    buttonToAdd.Enabled = false;
                    resultsTable.Controls.Add(buttonToAdd);
                }
            }

            m_GameBoard.Controls.Add(resultsTable, i_Column, i_Row);
        }

        private void enableApprovalButton(int i_Row)
        {
            m_GameBoard.GetControlFromPosition(r_ColumnOfApprovalButton, i_Row).Enabled = true;
        }

        private void enabledLineOfGussingButtons(int i_Row, bool i_SetValue)
        {
            for(int col = 0; col < r_MaxLengthOfInput; col++)
            {
                m_GameBoard.GetControlFromPosition(col, i_Row).Enabled = i_SetValue;
            }
        }

        private void updateMatchResults(int[] i_MatchResults, int i_Row)
        {
            TableLayoutPanel resultTable = (TableLayoutPanel)m_GameBoard.GetControlFromPosition(r_ColumnOfMatchResults, i_Row);
            TableLayoutControlCollection listOfSquares = resultTable.Controls;
            int squareIndex = 0;

            for(int j = 0; j < i_MatchResults[0]; j++)
            {
                listOfSquares[squareIndex].BackColor = Color.Black;
                squareIndex++;
            }

            for (int j = 0; j < i_MatchResults[1]; j++)
            {
                listOfSquares[squareIndex].BackColor = Color.Yellow;
                squareIndex++;
            }
        }

        private void buttonGuessColor_Click(object sender, EventArgs e)
        {
            m_ChooseColorForm.ShowDialog();
            if (m_ChooseColorForm.DialogResult == DialogResult.OK)
            {
                int colorNum = GameColors.GetColorNumber(m_ChooseColorForm.UserColorChoice);
                int totalOfCurrentColor = m_UserGuessInput.Count(element => element.Equals(colorNum));
                if (totalOfCurrentColor > 0)
                {
                    MessageBox.Show(k_DuplicateColorMessage, "Duplicate Choice");
                }
                else
                {
                    (sender as Button).BackColor = m_ChooseColorForm.UserColorChoice;
                    if (m_UserGuessInput[getSquarePosition((Button)sender).Column] == 0)
                    {
                        m_CurrentUserInputLength++;
                    }

                    m_UserGuessInput[getSquarePosition((Button)sender).Column] = colorNum;
                }
            }

            if (m_CurrentUserInputLength == r_MaxLengthOfInput)
            {
                enableApprovalButton(getSquarePosition((Button)sender).Row);
            }
        }

        private void buttonAprroval_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;     
            int buttRow = getSquarePosition((Button)sender).Row;
            int[] matchResults = r_TheGame.HandleUserMove(m_UserGuessInput);
            updateMatchResults(matchResults, buttRow);
            enabledLineOfGussingButtons(buttRow, false);

            if (r_TheGame.IsGameOver)
            {
                exposeHiddenFirstLine(r_TheGame.HiddenSequence);
            }
            else
            {
                m_CurrentUserInputLength = 0;
                Array.Clear(m_UserGuessInput, 0, m_UserGuessInput.Length);
                enabledLineOfGussingButtons(buttRow + 1, true);
            }
        }

        private void exposeHiddenFirstLine(int[] i_HiddenSequence)
        {
            for(int col = 0; col < r_MaxLengthOfInput; col++)
            {
                m_GameBoard.GetControlFromPosition(col, k_HiddenSquaresRow).BackColor = GameColors.GetColorName(i_HiddenSequence[col] - 1);
            }
        }

        public GameBoardForm(short i_GameRounds)
        {
            Text = k_FormTitel;
            AutoSize = true;
            StartPosition = FormStartPosition.CenterScreen;
            r_TheGame = new GameManager(i_GameRounds);
            r_MaxLengthOfInput = (short)r_TheGame.LengthOfSequence;
            r_ColumnOfApprovalButton = r_MaxLengthOfInput;
            r_ColumnOfMatchResults = (short)(r_ColumnOfApprovalButton + 1);
            m_UserGuessInput = new int[r_MaxLengthOfInput];
        }
    }
}