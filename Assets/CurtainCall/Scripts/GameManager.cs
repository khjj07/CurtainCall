using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public enum ReactantType
{
    NONE
}

public enum ReactantStatus
{
    NONE
}

public class GameManager : MonoBehaviour
{
    private List<Trigger> _triggers;
    private List<Reactant> _reactants;
    [SerializeField]
    private LevelAsset _currentLevel;
    [SerializeField]
    private RoundAsset _currentRound;

    private int _timeCount=0;
    private int _roundCount=0;

    void LoadCurrentLevel()
    {
        _currentRound=_currentLevel.rounds[0];
    }

    public void FinishLevel()
    {
        StopCoroutine("RoundCoroutine");
    }

    public bool CheckRoundStatus()
    {
        foreach (var condition in _currentRound.conditionList)
        {
            if (!_reactants.Find(x => condition.type == x.type))
            {
                if (!_reactants.Find(x => condition.status == x.status.Value))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool NextRound()
    {
        ++_roundCount;
        if (_roundCount< _currentLevel.rounds.Count)
        {
            _currentRound = _currentLevel.rounds[_roundCount];
            _timeCount = 0;
        }
        else
        {
            return false;
        }
       
        return true;
    }

    public void RoundSuccess()
    {
        if(!NextRound())
        {
            FinishLevel();
        }
    }

    public void RoundFail()
    {
        if (!NextRound())
        {
            FinishLevel();
        }
    }

    public void CreateTrigger(Trigger p_trigger)
    {
        var instance = Instantiate(p_trigger);
        _triggers.Add(instance);
    }

    public void CreateReactant(Reactant p_reactant)
    {
        var instance = Instantiate(p_reactant);

        instance.status
            .Where(_ => CheckRoundStatus())
            .Subscribe(_ => { RoundSuccess(); });

        _reactants.Add(instance);
    }

    public IEnumerator RoundCoroutine(IObserver<float> observer)
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            _timeCount++;
            observer.OnNext(_timeCount);
            if (_timeCount >= _currentRound.duration)
            {
                RoundFail();
            }
        }
    }

    void Start()
    {
        LoadCurrentLevel();
        _triggers = new List<Trigger>();
        _reactants = new List<Reactant>();

        Observable.FromCoroutine<float>(RoundCoroutine)
            .Subscribe(x => { Debug.Log(x); });

    }
}
