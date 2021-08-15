using UnityEngine;

public class Banana : Coins
{
    public float duration = 8.0f;

protected override void Eat()
{
    FindObjectOfType<GameManager>().BananaEaten(this);
}

}


