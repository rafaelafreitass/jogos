using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Configurações de Vida")]
    [Tooltip("Vida máxima inicial do inimigo.")]
    [SerializeField]
    private int maxHealth = 1;

    // A vida atual do inimigo (privada, mas acessível pelo Inspector)
    private int currentHealth;

    void Start()
    {
        // Inicializa a vida atual com a vida máxima
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Aplica dano ao inimigo e checa se ele deve morrer.
    /// Chamado pelo SonicController.
    /// </summary>
    /// <param name="damageAmount">Quantidade de dano a ser aplicada.</param>
    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida
        currentHealth -= damageAmount;

        Debug.Log(gameObject.name + " levou " + damageAmount + " de dano. Vida restante: " + currentHealth);

        // Verifica se a vida chegou a zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Executa as ações de morte do inimigo (animação, som, destruição do objeto).
    /// </summary>
    public void Die()
    {
        // 1. Lógica de Recompensa (Ex: soltar um anel, dar pontos)
        // ... (Insira o código de recompensa aqui) ...

        // 2. Efeitos Visuais/Sonoros (Ex: animação de explosão)
        // ... (Instanciar partículas, tocar som de explosão) ...

        Debug.Log(gameObject.name + " foi destruído!");

        // 3. Destruição do GameObject
        Destroy(gameObject);
    }
}