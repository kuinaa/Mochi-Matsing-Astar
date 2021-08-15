using UnityEngine;

public class EnemiesScatter : EnemiesBehavior
{    
    private void OnDisable()
    {
        this.enemies.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.enemies.frightened.enabled)
        {
            
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.enemies.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if (index >= node.availableDirections.Count) {
                    index = 0;
                }
            }

            this.enemies.movement.SetDirection(node.availableDirections[index]);
        }
    }

}


