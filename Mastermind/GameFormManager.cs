using System.Windows.Forms;

namespace Mindmaster
{
    public class GameFormManager
    {
        private StartForm m_StartGameForm;
        private GameBoardForm m_GameBoardForm;
         
        public void InitiateGameForm()
        {
            m_StartGameForm = new StartForm();
            m_StartGameForm.InitializeStartWindow();
            m_StartGameForm.ShowDialog();
            if (m_StartGameForm.DialogResult == DialogResult.OK)
            {
                m_GameBoardForm = new GameBoardForm(m_StartGameForm.CurrentNumOfGuesses);
                m_GameBoardForm.InitializeGameBoardForm();
                m_GameBoardForm.ShowDialog();
            }
        }
    }
}
