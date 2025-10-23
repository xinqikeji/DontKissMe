using System.Collections.Generic;
using UnityEngine;

public class ClotherUser : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
    [Range(0, 100)]
    [SerializeField] private float _chanceClotherOff;
    [SerializeField] private List<Material> _materials;

    private void Start()
    {
        var randomNumber = Random.Range(1, 101);
        if (randomNumber <= _chanceClotherOff)
            _skinnedMesh.enabled = false;

        var randomMaterialNumber = Random.Range(0, _materials.Count);
        _skinnedMesh.material = _materials[randomMaterialNumber];   
    }
}
