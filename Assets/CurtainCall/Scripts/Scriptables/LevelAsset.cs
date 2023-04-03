using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;


[CreateAssetMenu(menuName = "Create New Level")]
public class LevelAsset : ScriptableObject
{
    public List<RoundAsset> rounds;

    public LevelAsset()
    {

    }

    private void OnEnable()
    {
        EditorUtility.SetDirty(this);
    }

    static public void CreateAsset()
    {
        var levelAsset = CreateInstance<LevelAsset>();
        AssetDatabase.CreateAsset(levelAsset, "Assets/CurtainCall/Level/newLevel.asset");
        AssetDatabase.Refresh();
    }


}
