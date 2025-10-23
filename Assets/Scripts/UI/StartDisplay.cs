using UnityEngine;
using UnityEngine.Events;

public class StartDisplay : MonoBehaviour
{
    private bool _isStart;

    public UnityAction LevelStarted;

    private void Update()
    {
        if (!_isStart && Input.GetMouseButtonDown(0))
        {
            LevelStarted?.Invoke();
            Hide();
            _isStart = true;
        }
    }

    private void Hide() => gameObject.SetActive(false);
}
