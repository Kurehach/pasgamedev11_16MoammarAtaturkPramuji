using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("Animation & Visuals")]
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [Header("Health")]
    public int maxHP = 100;
    private int currentHP;
    public Slider healthBar; // Tarik UI Slider HP ke sini

    [Header("Combat")]
    public float attackRange = 1.5f;
    public int attackDamage = 20;
    public LayerMask enemyLayer; // Tarik layer "Enemy" ke sini di Inspector

    void Start()
    {
        // Inisialisasi semua komponen
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Setup awal HP
        currentHP = maxHP;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHP;
            UpdateHealthBar();
        }
    }

    void Update()
    {
        // 1. Deteksi Input WASD / Panah
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // 2. Sihir Flip Gambar Kiri/Kanan
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Hadap kanan
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Hadap kiri
        }

        // 3. Trigger Transisi Animasi Idle <-> Walk
        if (anim != null)
        {
            // sqrMagnitude akan bernilai lebih dari 0 jika player menekan tombol jalan
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }

        // 4. Deteksi Input Tombol Serang (Spasi)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        // Eksekusi fisika pergerakan yang smooth
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        // Mendeteksi semua musuh dalam radius serangan berbentuk lingkaran
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
        
        if (currentHP <= 0)
        {
            Debug.Log("Player Mati!");
            // (Opsional) Tambahkan animasi mati atau game over scene di sini nanti
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null) healthBar.value = currentHP;
    }

    // Menggambar garis panduan merah di Scene Unity untuk melihat seberapa jauh jangkauan mukul
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}