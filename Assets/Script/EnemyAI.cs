using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 3f;
    public int damage = 10;
    public int hp = 20;

    [Header("Loot Drop")]
    public GameObject lootPrefab; // Tarik prefab item drop ke sini

    private Transform player;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject); // Hancur setelah nyerang player
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            if (lootPrefab != null) Instantiate(lootPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // Hancur karena dibunuh
        }
    }
}