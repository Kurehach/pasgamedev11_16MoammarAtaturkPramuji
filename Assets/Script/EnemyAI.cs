using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 3f;
    public int damage = 10;
    public int hp = 20;

    [Header("Loot Drop")]
    public GameObject lootPrefab;

    private Transform player;
    private SpriteRenderer spriteRenderer; // Tambahan komponen visual

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
        
        spriteRenderer = GetComponent<SpriteRenderer>(); // Inisialisasi
    }

    void Update()
    {
        if (player != null)
        {
            // Gerak ke arah player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Logika Flip Sprite Musuh menghadap Player
            if (spriteRenderer != null)
            {
                if (player.position.x > transform.position.x)
                {
                    spriteRenderer.flipX = false; // Hadap kanan
                }
                else
                {
                    spriteRenderer.flipX = true; // Hadap kiri
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            // Destroy(gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            if (lootPrefab != null) Instantiate(lootPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}