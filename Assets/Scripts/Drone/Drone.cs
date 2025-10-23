using UnityEngine;

[RequireComponent(typeof(DroneMove))]
public class Drone : MonoBehaviour
{
    private DroneMove _droneMove;

    public void Activate()
    {
        _droneMove = GetComponent<DroneMove>();
        gameObject.SetActive(true);
        _droneMove.enabled = true;
    }
}