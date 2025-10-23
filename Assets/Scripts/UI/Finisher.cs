using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Events;

public class Finisher : MonoBehaviour
{
    private SplineFollower _car;

    public event UnityAction FinishLevel;

    public bool IsFinish { get; private set; } = false;

    private void OnEnable()
    {
        _car = FindObjectOfType<Car>().GetComponent<SplineFollower>();
        _car.onEndReached += UseEvent;
    }
    private void OnDisable() => _car.onEndReached += UseEvent;

    private void UseEvent(double obj)
    {
        if (IsFinish == false)
            FinishLevel?.Invoke();
        else
            Debug.LogAssertion("Не должно приходить 2 ивента победы");
        IsFinish = true;
    }

}
