using Assets.Scripts.Enemy.Eat;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EatEater : MonoBehaviour
{

    [SerializeField] private HotDogEater _hotDogEater;
    [SerializeField] private DonatEater _donatEater;
    [SerializeField] private PizzaEater _pizzaEater;

    private List<Eater> _foodEaters = new List<Eater>();
    private Eater _temEater;

    private void Awake()
    {
        _foodEaters.Add(_hotDogEater);
        _foodEaters.Add(_donatEater);
        _foodEaters.Add(_pizzaEater);
        _foodEaters.ForEach(x => x.Init());
    }
    public void SetCurrentEats(Food food) => _temEater = _foodEaters.First(x => food.GetType() == x.Food.GetType());

    public void StartEat(float time) => _temEater.StartEat(time);

    public void StopEat() => _temEater.StopEat();

    public void Show() => _temEater.Show();

    public void Hide() => _temEater.Hide();
}
