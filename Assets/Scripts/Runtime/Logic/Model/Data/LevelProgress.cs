using System;
using Scripts.Model.Definitions.Player;

namespace Scripts.Model.Data
{
    [Serializable]
    public class LevelProgress
    {
        public StatId Id;
        public int Level;

        public LevelProgress(StatId id, int level)
        {
            Id = id;
            Level = level;
        }
    }
}