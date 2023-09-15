using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Scripts.Model.Definitions.Player;

namespace Scripts.Model.Data
{
    [Serializable]
    public class LevelData
    {
        [SerializeField]
        private List<LevelProgress> progress;

        public int GetLevel(StatId id)
        {
            foreach (var levelProgress in progress)
            {
                if (levelProgress.Id == id)
                {
                    return levelProgress.Level;
                }
            }

            return 0;
        }

        public void LevelUp(StatId id)
        {
            var progress = this.progress.FirstOrDefault(x => x.Id == id);
            if (progress == null)
                this.progress.Add(new LevelProgress(id, 1));
            else
                progress.Level++;
        }
    }
}