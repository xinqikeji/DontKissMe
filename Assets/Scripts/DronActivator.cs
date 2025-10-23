using UnityEngine;

public class DronActivator : MonoBehaviour
{
    [SerializeField] private Drone _drone;

    private void OnTriggerEnter(Collider other) => _drone.Activate();
}