using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Sprite squishedSprite;
    public float squishedSpeed = 10f;

    private bool squished;
    private bool pushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!squished && collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (player.starpower) {
                Hit();
            } else if (collision.transform.DotTest(transform, Vector2.down)) {
                GoSquished();
            }  else {
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (squished && other.CompareTag("Player"))
        {
            if (!pushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushSquished(direction);
            }
            else
            {
                Player player = other.GetComponent<Player>();

                if (player.starpower) {
                    Hit();
                } else {
                    player.Hit();
                }
            }
        }
        else if (!squished && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void GoSquished()
    {
        squished = true;

        GetComponent<SpriteRenderer>().sprite = squishedSprite;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
    }

    private void PushSquished(Vector2 direction)
    {
        pushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement movement = GetComponent<EntityMovement>();
        movement.direction = direction.normalized;
        movement.speed = squishedSpeed;
        movement.enabled = true;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

    private void OnBecameInvisible()
    {
        if (pushed) {
            Destroy(gameObject);
        }
    }

}
