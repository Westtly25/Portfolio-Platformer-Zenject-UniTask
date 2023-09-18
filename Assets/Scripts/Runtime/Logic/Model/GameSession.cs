using Zenject;
using UnityEngine;
using System.Linq;
using Scripts.Model.Data;
using Scripts.Model.Models;
using Scripts.Utilities.Disposables;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Scripts.Model.Definitions.Player;
using Scripts.Components.LevelManegement;

namespace Scripts.Model
{
    //TODO Refactor by Zenject Installer
    public class GameSession : MonoBehaviour
    {
        [SerializeField]
        private PlayerData data;
        [SerializeField]
        private string defaultCheckPoint;

        public PlayerData Data => data;
        private PlayerData save;

        private readonly CompositeDisposable trash = new CompositeDisposable();
        public QuickInventoryModel QuickInventory { get; private set; }
        public PerksModel PerksModel { get; private set; }
        public StatsModel StatsModel { get; private set; }

        private readonly List<string> _checkpoints = new List<string>();

        private void Awake()
        {
            var existsSession = GetExistsSession();
            if (existsSession != null)
            {
                existsSession.StartSession(defaultCheckPoint);
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(this);
                StartSession(defaultCheckPoint);
            }
        }

        private void StartSession(string defaultCheckPoint)
        {
            SetChecked(defaultCheckPoint);

            LoadHud();
            SpawnHero();
        }

        private void SpawnHero()
        {
            var checkpoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckPoint = _checkpoints.Last();
            foreach (CheckPointComponent checkPoint in checkpoints)
            {
                if (checkPoint.Id == lastCheckPoint)
                {
                    checkPoint.SpawnHero();
                    break;
                }
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(data);
            trash.Retain(QuickInventory);

            PerksModel = new PerksModel(data);
            trash.Retain(PerksModel);

            StatsModel = new StatsModel(data);
            trash.Retain(StatsModel);

            data.Hp.Value = (int) StatsModel.GetValue(StatId.Hp);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private GameSession GetExistsSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                    return gameSession;
            }

            return null;
        }

        public void Save()
        {
            save = data.Clone();
        }

        public void LoadLastSave()
        {
            data = save.Clone();

            trash.Dispose();
            InitModels();
        }

        public bool IsChecked(string id)
        {
            return _checkpoints.Contains(id);
        }

        public void SetChecked(string id)
        {
            if (!_checkpoints.Contains(id))
            {
                Save();
                _checkpoints.Add(id);
            }
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }

        private readonly List<string> _removedItems = new List<string>();

        public bool RestoreState(string id)
        {
            return _removedItems.Contains(id);
        }

        public void StoreState(string id)
        {
            if (!_removedItems.Contains(id))
                _removedItems.Add(id);
        }
    }
}