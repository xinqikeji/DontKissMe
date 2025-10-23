using UnityEngine;

[System.Serializable]
public abstract class Eater
{
    public abstract Food Food { get; protected set; }
    public abstract void Init();
    public abstract void StartEat(float time);
    public abstract void StopEat();
    public abstract void Show();
    public abstract void Hide();
}
