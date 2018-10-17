using System;
using System.Windows.Forms;
using System.Drawing;

namespace Mindmaster
{
    public class ChooseColorForm : Form
    {
        private const short k_NumOfColors = 8;
        private const short k_NumOfColorsInRow = 4;
        private const short k_NumOfRows = 2;
        private const string k_FormTitel = "Pick A Color:";

        private TableLayoutPanel m_TableOfColors = new TableLayoutPanel();
        private GameColors m_GameColors = new GameColors();
        private Color m_UserColorChoice;

        public void BuildColorsTable()
        {
            int currentNumColor = 0;
            for(int row = 0; row < k_NumOfRows; row++)
            {
                for(int col = 0; col < k_NumOfColorsInRow; col++)
                {
                    Button buttonToAdd = new Button();
                    buttonToAdd.Size = new Size(45, 45);
                    buttonToAdd.Click += button_Click;
                    buttonToAdd.BackColor = GameColors.GetColorName(currentNumColor);
                    currentNumColor++;
                    m_TableOfColors.Controls.Add(buttonToAdd, col, row);
                }
            }

            m_TableOfColors.AutoSize = true;
            m_TableOfColors.Left = 5;
            m_TableOfColors.Top = 5;
            Controls.Add(m_TableOfColors);
        }

        private void button_Click(object sender, EventArgs e)
        {
            UserColorChoice = (sender as Button).BackColor;
            DialogResult = DialogResult.OK;
            Close();
        }

        public Color UserColorChoice
        {
            get
            {
                return m_UserColorChoice;
            }

            set
            {
                m_UserColorChoice = value;
            }
        }

        public ChooseColorForm()
        {
            Text = k_FormTitel;
            ClientSize = new Size(220, 130);
            StartPosition = FormStartPosition.CenterParent;
            m_TableOfColors.ColumnCount = k_NumOfColorsInRow;
            m_TableOfColors.RowCount = k_NumOfRows;
        }
    }
}
