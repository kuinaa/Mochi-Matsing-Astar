using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemies[] enemies;
    
    public Player player;

    public Transform coins;

    public int enemyMultiplier {get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }



    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown) 
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(1);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform coin in this.coins)
        {
            coin.gameObject.SetActive(true);
        }

       ResetState();
    }

    private void ResetState()
    {
        ResetEnemyMultiplier();
        for (int i = 0; i < this.enemies.Length; i++) {
            this.enemies[i].ResetState();
        }

         this.player.ResetState();
    }

    private void GameOver()
    {
        for (int i = 0; i < this.enemies.Length; i++) {
            this.enemies[i].gameObject.SetActive(false);
        }

            this.player.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
         this.score = score;
    }       

    private void SetLives(int lives)
    {
            this.lives = lives;
    }   

    public void EnemiesKilled(Enemies enemy)
    {
        int points = enemy.points * this.enemyMultiplier;
        SetScore(this.score + points);
        this.enemyMultiplier++;
    }

    public void PlayerKilled()
    {
        this.player.gameObject.SetActive(false);

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void CoinCollected(Coins coin)
    {
        coin.gameObject.SetActive(false);
        SetScore(this.score + coin.points);

        if(!HasRemainingCoins())
        {
            this.player.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    public void BananaEaten(Banana coin)
    {
       
       for(int i = 0; i < this.enemies.Length; i++)
       {
           this.enemies[i].frightened.Enable(coin.duration);
       }
       
        CoinCollected(coin);
        CancelInvoke();
        Invoke(nameof(ResetEnemyMultiplier), coin.duration);
         

    }

    private bool HasRemainingCoins()
    {
         foreach (Transform coin in this.coins)
        {
            if (coin.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetEnemyMultiplier()
    {
        this.enemyMultiplier = 1;
    }
}