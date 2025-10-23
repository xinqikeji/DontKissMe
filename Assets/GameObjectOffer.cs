using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectOffer : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    private void Start()
    {
        _gameObject.transform.parent = transform.root.parent;
        _gameObject.SetActive(true);
        foreach (var mesh in _meshRenderers)
        {
            mesh.enabled = false;
        }
    }
}
