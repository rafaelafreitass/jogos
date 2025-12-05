using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        // aqui você pode causar dano no inimigo
        Destroy(gameObject);
    }

    void Start()
    {
        Destroy(gameObject, 3f); // auto-destruir depois de 3s
    }
}
