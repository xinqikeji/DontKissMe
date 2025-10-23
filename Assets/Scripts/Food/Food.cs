using UnityEngine;

public class Food : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _weight;
    public float Weight => _weight;

    private FoodMove _foodMove;
    bool isHit;

    private void Awake()
    {
        Physics.IgnoreLayerCollision(8, 8, true);
        _foodMove = GetComponent<FoodMove>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy follower) && isHit == false)
        {
            isHit = true;
            AttackEnemy(follower);
            Destroy(gameObject);
        }
    }

    protected void AttackEnemy(Enemy follower)
    {
        follower.TakeDamage(this);
    }

    public void Explode(Vector3 direction) => _foodMove.Explode(direction);

    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);
}