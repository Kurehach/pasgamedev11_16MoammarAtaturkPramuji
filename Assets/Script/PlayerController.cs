using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Health")]
    public int maxHP = 100;
    private int currentHP;
    public Slider healthBar; // Tarik UI Slider HP ke sini

    [Header("Combat")]
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayer; // Bikin Layer "Enemy" di Unity dan pasang ke prefab musuh, lalu pilih layer tersebut di sini

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        if (healthBar != null) healthBar.maxValue = maxHP;
        UpdateHealthBar();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Tekan Spasi untuk mukul musuh di dekat player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        // Mendeteksi semua musuh dalam radius serangan
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage);
            Debug.Log("Player mukul musuh!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        UpdateHealthBar();
        if (currentHP <= 0) Debug.Log("Player Mati!");
    }

    void UpdateHealthBar()
    {
        if (healthBar != null) healthBar.value = currentHP;
    }

    // Untuk menampilkan visual jangkauan serangan di layar Scene Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}