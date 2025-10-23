using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _scale = .7f;

    public void Hide() => gameObject.SetActive(false);
}