using System;
using System.Windows.Forms;
using System.Drawing;

namespace Mindmaster
{
    public class StartForm : Form
    {
        private const short k_MinOfGuesses = 4;
        private const short k_MaxOfGuesses = 10;
        private const string k_FormTitle = "Bool Pgia";
        private const string k_CounterButtText = "Number Of Chances: {0}";

        private Button m_NumOfChancesButt = new Button();
        private Button m_StartButton = new Button();
        private short m_CurrentNumOfGuesses = k_MinOfGuesses;

        public void InitializeStartWindow()
        {
            Text = k_FormTitle;
            m_StartButton.Text = "Start";
            StartPosition = FormStartPosition.CenterScreen;
            m_NumOfChancesButt.Text = string.Format(k_CounterButtText, k_MinOfGuesses);
            Size = new Size(300, 150);
            m_NumOfChancesButt.Width = 250;
            m_StartButton.Width = m_NumOfChancesButt.Width / 3;
            m_NumOfChancesButt.Location = new Point((ClientSize.Width / 2) - (m_NumOfChancesButt.Width / 2), 20);
            m_StartButton.Location = new Point(m_NumOfChancesButt.Left + (m_NumOfChancesButt.Width - m_StartButton.Width), m_NumOfChancesButt.Height * 3);
            m_NumOfChancesButt.Click += m_NumOfChancesButt_Click;
            m_StartButton.Click += m_StartButton_Click;
            Controls.Add(m_StartButton);
            Controls.Add(m_NumOfChancesButt);
        }

        private void m_NumOfChancesButt_Click(object sender, EventArgs e)
        {
            if (m_CurrentNumOfGuesses < k_MaxOfGuesses)
            {
                m_CurrentNumOfGuesses++;
            }
            else
            {
                m_CurrentNumOfGuesses = k_MinOfGuesses;
            }

            (sender as Button).Text = string.Format(k_CounterButtText, m_CurrentNumOfGuesses);
        }

        private void m_StartButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public short CurrentNumOfGuesses
        {
            get
            {
                return m_CurrentNumOfGuesses;
            }
        }
    }
}
