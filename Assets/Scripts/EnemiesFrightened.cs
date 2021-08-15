using UnityEngine;

public class EnemiesFrightened : EnemiesBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer vulnerable;
    public SpriteRenderer white;
    public SpriteRenderer dead;


    public bool killed { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        this.body.enabled = false;
        this.vulnerable.enabled = true;
        this.white.enabled = false;
        this.dead.enabled = false;

        Invoke(nameof(Flash), duration / 2.0f);

    }

    public override void Disable()
    {
        base.Disable();
        
        this.body.enabled = true;
        this.vulnerable.enabled = false;
        this.white.enabled = false;
        this.dead.enabled = false;

    }
    private void Flash()
    {
        if(!this.killed)
        {
        this.vulnerable.enabled = false;
        this.white.enabled = true;
        this.white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Killed()
    {
        this.killed = true;
        
        Vector3 position = this.enemies.home.inside.position;
        position.z = this.enemies.home.inside.position.z;
        this.enemies.transform.position = position;

        this.enemies.home.Enable(this.duration);

        this.body.enabled = false;
        this.vulnerable.enabled = false;
        this.white.enabled = false;
        this.dead.enabled = true;
    }
    private void OnEnable()
    {
        this.enemies.movement.speedMultiplier = 0.5f;
        this.killed = false;
    }

    private void OnDisable()
    {
        this.enemies.movement.speedMultiplier = 1.0f;
        this.killed = false;
    }
      private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (this.enabled) {
                Killed();
             }
         }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirections in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirections.x, availableDirections.y, 0.0f);
                float distance = (this.enemies.target.position - newPosition).sqrMagnitude;

                if(distance > maxDistance)
                {
                    direction = availableDirections;
                    maxDistance = distance;
                }
             }

             this.enemies.movement.SetDirection(direction);
        }
}
}
