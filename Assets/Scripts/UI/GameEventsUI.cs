using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsUI : Singleton<GameEventsUI>
{
    #region serializable variables
    [SerializeField] GameEventButtonPrefab _gameEventButtonPrefab;
    [SerializeField] Transform _gameEventButtonParentTransform;
    #endregion

    #region local variables
    Game _game;
    List<GameEventButtonPrefab> _gameEventButtonPrefabs = new List<GameEventButtonPrefab>();
    List<GameEventInfo> _gameEventInfos = new List<GameEventInfo>();
    SceneReferences _sceneReferences;
    #endregion

    #region getters and setters
    Game Game { get { if (_game == null) { _game = Game.Instance; } return _game; } }
    SceneReferences SceneReferences { get { if (_sceneReferences == null) { _sceneReferences = SceneReferences.Instance; }return _sceneReferences; } }
    #endregion

    #region public methods
    public void ClickEventsButton()
    {
        bool buttonsActive = _gameEventButtonParentTransform.gameObject.activeSelf;
        _gameEventButtonParentTransform.gameObject.SetActive(!buttonsActive);
        if (!buttonsActive)
        {
            foreach (GameEventInfo eventInfo in _gameEventInfos)
            {
                GameEventButtonPrefab gameEventButtonPrefab = Instantiate(_gameEventButtonPrefab, _gameEventButtonParentTransform);
                gameEventButtonPrefab.Init(eventInfo);
                _gameEventButtonPrefabs.Add(gameEventButtonPrefab);
            }
        }
        else
        {
            foreach (GameEventButtonPrefab gameEventButtonPrefab in _gameEventButtonPrefabs)
            {
                Destroy(gameEventButtonPrefab.gameObject);
            }
            _gameEventButtonPrefabs.Clear();
        }
    }
    public void Init()
    {
        foreach (Seal seal in Game.Seals)
        {
            if(seal.RescueProgress == SealRescueProgress.Rescue)
            {
                GameEventInfo gameEventInfo = new GameEventInfo(
                    $"A {SceneReferences.GetDisplayName(seal.SealSpecies)} has been found and is in need of rescue.", 
                    "Seal Rescue", 
                    GameEventType.SealSpotted, seal);
                _gameEventInfos.Add(gameEventInfo);
            }
        }
    }
    #endregion
}

public class GameEventInfo
{
    #region local variables
    string _eventDescription;
    string _eventName;
    GameEventType _gameEventType;
    Seal _seal;
    #endregion

    #region getters and setters
    public string EventDescription { get { return _eventDescription; } }
    public string EventName { get { return _eventName; } }
    #endregion

    #region constructors
    public GameEventInfo(string eventDescription, string eventName, GameEventType gameEventType, Seal seal = null)
    {
        _eventDescription = eventDescription;
        _eventName = eventName;
        _gameEventType = gameEventType;
        _seal = seal;
    }
    #endregion
}
