using System.Collections;
using UnityEngine;

public class EnemiesHome : EnemiesBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if(this.gameObject.activeSelf){
        StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.enemies.movement.SetDirection(-this.enemies.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.enemies.movement.SetDirection(Vector2.up, true);
        this.enemies.movement.rigidbody.isKinematic = true;
        this.enemies.movement.enabled = false;

        Vector3 position = this.transform.position;
        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.enemies.transform.position = newPosition;
            elapsed += Time.deltaTime;

            yield return null;
        }

        elapsed =  0.0f;

          while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.enemies.transform.position = newPosition;
            elapsed += Time.deltaTime;

            yield return null;
        }
        this.enemies.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.enemies.movement.rigidbody.isKinematic = false;
        this.enemies.movement.enabled = true;
    }   
}