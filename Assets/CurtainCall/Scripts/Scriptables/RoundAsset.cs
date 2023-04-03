using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct ReactantCondition
{
    public ReactantType type;
    public ReactantStatus status;
}

[Serializable, CreateAssetMenu(menuName = "Create New Round")]
public class RoundAsset : ScriptableObject
{
    public float duration;
    public string script;
    public List<ReactantCondition> conditionList;

    public RoundAsset()
    {

    }

    private void OnEnable()
    {
        EditorUtility.SetDirty(this);
    }
   

    static public void CreateAsset()
    {
        var roundAsset = CreateInstance<RoundAsset>();
        AssetDatabase.CreateAsset(roundAsset, "Assets/CurtainCall/Round/newRound.asset");
        AssetDatabase.Refresh();
    }
}

