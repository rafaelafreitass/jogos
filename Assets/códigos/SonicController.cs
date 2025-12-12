using UnityEngine;

public class SonicController : MonoBehaviour
{
    [Header("Configurações de Combate")]
    [Tooltip("Força vertical aplicada ao Sonic após derrotar um inimigo.")]
    [SerializeField]
    private float bounceForce = 10f; 

    [Tooltip("Dano que o Sonic inflige ao acertar um inimigo por cima.")]
    [SerializeField]
    private int damageToEnemy = 1;

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
        // 1. LÓGICA DE MORTE DO INIMIGO (KILLZONE - Acerto por Cima)
        if (other.CompareTag("EnemyKillZone"))
        {
            // O KillZone é um filho, o Inimigo (que tem o EnemyController) é o pai
            EnemyController enemy = other.transform.parent.GetComponent<EnemyController>();
            
            if (enemy != null)
            {
                // Chama o método TakeDamage no script do inimigo.
                // Se o inimigo tiver 1 de vida (ou menos), ele irá morrer dentro do seu próprio script.
                enemy.TakeDamage(damageToEnemy); 
                
                // Aplica o quique (bounce) para dar a sensação de Sonic
                ApplyBounce();
            }
        }
    }

    // Chamado quando o colisor do Sonic entra em contato com um **COLISOR SÓLIDO** (Hurtbox)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 2. LÓGICA DE DANO DO SONIC (HURTBOX / LADOS ou Por Baixo)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // A colisão ocorreu fora da KillZone (Trigger), 
            // logo, o Sonic acertou o corpo do inimigo e leva dano.
            
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
        // *** LÓGICA DE DANO AO SONIC ***
        
        Debug.Log("Sonic levou dano! O EnemyController tinha a Tag 'Enemy'.");
        
        // Aqui deve entrar a lógica para:
        // 1. Verificar se o Sonic tem anéis.
        // 2. Se tiver, soltar os anéis e dar invencibilidade temporária.
        // 3. Se não tiver anéis (ou vida), rodar a animação de morte do Sonic.
    }
}