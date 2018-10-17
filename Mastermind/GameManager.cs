namespace Mindmaster
{
    public class GameManager
    {
        private readonly short r_MaxRounds;
        private Sequence m_HiddenSequence = new Sequence();
        private short m_GuessesCounter = 0;
        private bool m_IsUserWin = false;

        public void StartGame()
        {
            m_HiddenSequence.GenerateSequence();
        }

        public int[] HandleUserMove(int[] i_UserSequenceGuess)
        {
            int[] sequenceMatchResults = new int[2];
            sequenceMatchResults = m_HiddenSequence.FindMatchesInTwoSequences(i_UserSequenceGuess);
            m_IsUserWin = sequenceMatchResults[Sequence.IndexOfElementAndIndexMatch] == Sequence.MaxSequenceLength;
            m_GuessesCounter++;
            return sequenceMatchResults;           
        }

        public int LengthOfSequence
        {
            get { return Sequence.MaxSequenceLength; }
        }

        public bool IsUserWin
        {
            get
            {
                return m_IsUserWin;
            }
        }

        public short MaxRounds
        {
            get
            {
                return r_MaxRounds;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return m_IsUserWin || (r_MaxRounds == m_GuessesCounter);
            }
        }

        public int[] HiddenSequence
        {
            get
            {
                return m_HiddenSequence.TheSequence;
            }
        }

        public GameManager(short i_MaxRounds)
        {
            r_MaxRounds = i_MaxRounds;
        }
    }
}