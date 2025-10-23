using UnityEngine;

public class AniHider : MonoBehaviour
{
    [SerializeField] private GameObject _aim;

    private Finisher _finisher;
    private Starter _starter;

    private void OnEnable()
    {
        _finisher = FindObjectOfType<Finisher>();
        _starter = FindObjectOfType<Starter>();

        _finisher.FinishLevel += Hide;
        _starter.LevelStart += Show;
    }

    private void OnDisable()
    {
        _finisher.FinishLevel -= Hide;
        _starter.LevelStart -= Show;
    }

    public void Hide() => _aim.SetActive(false);

    public void Show() => _aim.SetActive(true);
}
