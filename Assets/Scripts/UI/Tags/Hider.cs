using UnityEngine;

public class Hider : MonoBehaviour
{
    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);
    public void HideAndStopTime()
    {
        Hide();
        Time.timeScale = 0;
    }

    public void ShowAndStopTime()
    {
        Show();
        Time.timeScale = 0;
    }
}
