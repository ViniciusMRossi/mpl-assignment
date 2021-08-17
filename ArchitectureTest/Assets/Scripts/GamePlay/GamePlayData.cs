namespace GamePlay
{
    public class GamePlayData
    {
        public uint Score
        {
            get;
            set;
        }
        public uint Lives
        {
            get;
            set;
        }

        public GamePlayData(uint lives, uint score)
        {
            Lives = lives;
            Score = score;
        }
    }
}
