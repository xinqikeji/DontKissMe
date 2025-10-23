using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Food[] _foodsPrefabs;
    [SerializeField] private int _count;
    [SerializeField] private ParticleSystem _exploidEffect;

    private List<Food> _foods = new List<Food>();

    public enum DIRECTION
    {
        RIGHT = 0,
        LEFT = 1,
        UP = 2,
        DOWN = 3
    }

    public DIRECTION DIRECTION_FORCE;

    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            Food food = Instantiate(_foodsPrefabs[Random.Range(0, _foodsPrefabs.Length - 1)], transform.position + Random.insideUnitSphere * .5f, new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), 0));
            _foods.Add(food);
            food.GetComponent<FoodMove>().SetForce(0);
            food.Hide();
            food.transform.parent = transform.root;
        }
        Activate();
    }

    public void Activate()
    {
        //gameObject.SetActive(false);
        _exploidEffect.Play();
        _exploidEffect.transform.parent = transform.root;

        foreach (var food in _foods)
        {
            food.Show();

            switch (DIRECTION_FORCE)
            {
                case DIRECTION.RIGHT:
                    food.Explode(new Vector3(-1, Random.Range(.5f, 1f), 0));
                    break;
                case DIRECTION.LEFT:
                    food.Explode(new Vector3(1, Random.Range(.5f, 1f), 0));
                    break;
                case DIRECTION.UP:
                    food.Explode(new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)));
                    break;
                case DIRECTION.DOWN:
                    food.Explode(new Vector3(Random.Range(-1f, 1f), -1, Random.Range(-1f, 1f)));
                    break;
            }
        }
    }
}