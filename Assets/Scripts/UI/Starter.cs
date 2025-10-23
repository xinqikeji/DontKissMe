using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Starter : MonoBehaviour
{
    public event UnityAction LevelStart;

    public void Start() => StartCoroutine(CheckStartLevel());

    private IEnumerator CheckStartLevel()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        LevelStart?.Invoke();
    }
}