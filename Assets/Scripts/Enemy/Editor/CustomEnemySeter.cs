using Assets.Scripts.Enemy.EnemySet;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySeter))]
public class CustomEnemySeter : Editor
{
    private EnemySeter _enemySeter;

    private void OnEnable()
    {
        _enemySeter = (EnemySeter)target;
        _enemySeter.Init();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("SetEnemyPosition"))
        {
            _enemySeter.SetPosition();
            _enemySeter.Save();
        }
        if (GUILayout.Button("SetEnemyOffcet"))
        {
            _enemySeter.SetOffcet();
            _enemySeter.Save();
        }

        _enemySeter.SetEvenly = EditorGUILayout.BeginToggleGroup("Set ByDistant", _enemySeter.SetEvenly);
        _enemySeter.Distant = EditorGUILayout.FloatField(_enemySeter.Distant);
        EditorGUILayout.EndToggleGroup();
    }

}
