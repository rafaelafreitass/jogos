using UnityEngine;
using TMPro; 

public class CoinManager : MonoBehaviour
{
    
    public static CoinManager Instance;

    
    public TextMeshProUGUI coinText; 

    
    private int currentCoins = 0; 

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        UpdateCoinDisplay();
    }

    
    public void AddCoins(int amount)
    {
        currentCoins += amount;
        
        
        UpdateCoinDisplay();
    }

        private void UpdateCoinDisplay()
    {
        
        coinText.text = "Moedas: " + currentCoins.ToString();
    }
}