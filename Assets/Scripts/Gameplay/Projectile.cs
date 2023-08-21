using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    public void Shoot()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // change "Ground" to "Wall" if needed
        int layerMask = 1 << LayerMask.NameToLayer("Wall");

        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 normal = contact.normal;

            if (normal == Vector2.left)
            {
                var obj = makeBouncePad(contact.collider.gameObject);
                obj.transform.Rotate(0f, 180f, 0f);
                ObjectPool.ReturnGameObject(gameObject);
            }

            else if (normal == Vector2.right)
            {
                makeBouncePad(contact.collider.gameObject);
                ObjectPool.ReturnGameObject(gameObject);
            }

            collision.collider.enabled = false;
        }

        else
            ObjectPool.ReturnGameObject(gameObject);

        collision.gameObject.layer = 0;
    }

    public GameObject makeBouncePad(GameObject go)
    {
        GameObject bouncePad = new();
        bouncePad.transform.localScale = new Vector3(4, 4, 1.1006f);
        bouncePad.transform.position = transform.position;
        bouncePad.transform.parent = go.transform;
        bouncePad.layer = 3;
        Bouncer bouncer = bouncePad.AddComponent<Bouncer>();
        bouncer.setVals(true, Vector2.up * 15);
        BoxCollider2D boxCollider = bouncePad.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(1f/4f, .1f/4f);
        boxCollider.offset = Vector2.up * .5f/4f;
        boxCollider.isTrigger = true;

        var animator = bouncePad.AddComponent<Animator>();
        bouncePad.AddComponent<SpriteRenderer>();
        animator.runtimeAnimatorController = FindAnyObjectByType<PlayerController>().Animator.runtimeAnimatorController;
        animator.SetTrigger("DieWall");

        return bouncePad;
    }
}