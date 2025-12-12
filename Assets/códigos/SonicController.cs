using UnityEngine;

public class SonicController : MonoBehaviour
{
    [Header("Configurações de Combate")]
    [Tooltip("Força vertical aplicada ao Sonic após derrotar um inimigo.")]
    [SerializeField]
    private float bounceForce = 10f; 

    // Referência ao Rigidbody2D do Sonic
    private Rigidbody2D rb; 

    void Start()
    {
        // Pega o componente Rigidbody2D ao iniciar
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("O SonicController requer um Rigidbody2D no mesmo GameObject.");
        }
    }

    // Chamado quando o colisor do Sonic entra em contato com um **TRIGGER** (KillZone)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. LÓGICA DE MORTE DO INIMIGO (KILLZONE)
        if (other.CompareTag("EnemyKillZone"))
        {
            // O KillZone é um filho, o Inimigo é o pai
            EnemyController enemy = other.transform.parent.GetComponent<EnemyController>();
            
            if (enemy != null)
            {
                // Chama o método de morte do script do inimigo
                enemy.Die(); 
                
                // Aplica o quique (bounce)
                ApplyBounce();
            }
        }
    }

    // Chamado quando o colisor do Sonic entra em contato com um **COLISOR SÓLIDO** (Hurtbox)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 2. LÓGICA DE DANO DO SONIC (HURTBOX / LADOS)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Se chegou aqui, não foi um acerto na KillZone, então o Sonic leva dano.
            // Para maior robustez, você pode adicionar uma checagem de velocidade vertical, 
            // mas a separação de colisores já garante que é um acerto lateral/inferior.
            
            TakeDamage();
        }
    }

    // --- Métodos de Ação ---

    private void ApplyBounce()
    {
        if (rb != null)
        {
            // Zera a velocidade Y para garantir que o quique seja limpo
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            // Aplica a força para cima (o 'bounce' clássico do Sonic)
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
        }
    }

    private void TakeDamage()
    {
        // *** ESTE É ONDE VOCÊ DESENVOLVE SUA LÓGICA DE DANO ***
        
        Debug.Log("Sonic levou dano! Lógica de perda de anéis/morte deve ser executada aqui.");
        
        // Exemplo: 
        // if (ringCount > 0)
        // {
        //     DropRings();
        //     StartInvincibilityFrames();
        // }
        // else
        // {
        //     Die();
        // }
    }
}