using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 15;
    [SerializeField] private float groundAcceleration = 10;
    [SerializeField] private float airAcceleration = 5;
    [SerializeField] private float jumpTakeOffForce = 12;
    [SerializeField] private float slowdownSpeed = 10;

    [SerializeField] private float friction = 1f;
    [SerializeField] private float turnAroundSpeed = 4;
    [SerializeField] private float bouncieness = 10;
    [SerializeField] private AudioClip jump1;
    [SerializeField] private AudioClip jump2;

    public enum DeathType { FloorSpike, WallSpike, CeilingSpike, Disintigrate, Buzzsaw };
    private bool isAlive = true;
    private bool isRooted = false;
    private bool isFacingRight;
    private Rigidbody2D rgbd2d;
    private Animator animator;

    public Animator Animator => animator;

    private void Start()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        rgbd2d.sharedMaterial = new PhysicsMaterial2D();
        rgbd2d.sharedMaterial.friction = friction;
        isFacingRight = true;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * .03f);

        if (IsGrounded())
        {
            //Debug.Log("Grounded");
            animator.SetBool("Jumping", false);

            if (Input.GetKeyDown(KeyCode.UpArrow) && isAlive)
            {
                animator.SetBool("Jumping", true);
                animator.SetTrigger("Jump");
                rgbd2d.velocity = Vector2.right * rgbd2d.velocity.x;
                rgbd2d.AddForce(jumpTakeOffForce * Vector2.up, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal < 0)
            {
                if (isFacingRight)
                    Flip();
            }

            else if (horizontal > 0)
            {
                if (!isFacingRight)
                    Flip();
            }

            if (horizontal > 0 && rgbd2d.velocity.x < maxSpeed)
            {
                if (rgbd2d.velocity.x < 0)
                    horizontal *= turnAroundSpeed;

                rgbd2d.velocity += Vector2.right * (IsGrounded() ? groundAcceleration : airAcceleration) * horizontal * Time.deltaTime;
                rgbd2d.velocity = new Vector2(Mathf.Clamp(rgbd2d.velocity.x, -maxSpeed, maxSpeed), rgbd2d.velocity.y);
            }

            else if (horizontal < 0 && rgbd2d.velocity.x > -maxSpeed)
            {
                if (rgbd2d.velocity.x > 0)
                    horizontal *= turnAroundSpeed;

                rgbd2d.velocity += Vector2.right * (IsGrounded() ? groundAcceleration : airAcceleration) * horizontal * Time.deltaTime;
                rgbd2d.velocity = new Vector2(Mathf.Clamp(rgbd2d.velocity.x, -maxSpeed, maxSpeed), rgbd2d.velocity.y);
            }

            if (IsGrounded())
                if (Mathf.Abs(rgbd2d.velocity.x) > maxSpeed || horizontal == 0.0f)
                    rgbd2d.velocity = new Vector2(rgbd2d.velocity.x * slowdownSpeed, rgbd2d.velocity.y);

            Debug.Log(rgbd2d.velocity.x);
            animator.SetBool("Moving", Mathf.Abs(rgbd2d.velocity.x) >= .01);
        }

        else
        {
            if (!isRooted && IsGrounded())
            {
                isRooted = true;
                rgbd2d.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public bool IsGrounded()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, Vector2.one, 0, Vector2.down, .08f);

        foreach (RaycastHit2D hit in hits)
        {
            //Debug.Log("Hit " + hit.collider.gameObject.name);

            if (!hit.collider.gameObject.Equals(this) && hit.collider.gameObject.layer == 0)
                return true;
        }

        return false;
    }

    public void BouncePlayer(Vector2 bounceVector)
    {
        if (isAlive)
            rgbd2d.velocity = bounceVector;
    }

    public void KillPlayer(DeathType typeOfDeath, GameObject skewer = null, Vector2 distFromCenter = new Vector2(), bool right = false)
    {
        if (!isAlive)
        {
            return;
        }
        Destroy(transform.GetComponentInChildren<ProjectileShooter>());
        // MusicManager.KillTheMusic(Time.deltaTime, GameManager.timeTillRespawn);
        isAlive = false;

        if (skewer && typeOfDeath != DeathType.Disintigrate)
        {
            if (typeOfDeath != DeathType.Buzzsaw)
            {
                transform.position = skewer.transform.position;
                transform.position += new Vector3(distFromCenter.x, distFromCenter.y, -1);
            }

            transform.parent = skewer.transform;
            isRooted = true;
            rgbd2d.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        Debug.Log("Death");

        switch (typeOfDeath)
        {
            case DeathType.FloorSpike:
                animator.SetTrigger("DieFloor");
                rgbd2d.velocity = Vector2.zero;
                makeBouncePad();
                break;
            case DeathType.WallSpike:
                if ((right && !isFacingRight) || !right && isFacingRight)
                {
                    Flip();
                }
                animator.SetTrigger("DieWall");
                rgbd2d.isKinematic = true;
                rgbd2d.velocity = Vector2.zero;
                makeBouncePad();
                break;
            case DeathType.CeilingSpike:
                animator.SetTrigger("DieCeiling");
                gameObject.layer = 0;
                rgbd2d.isKinematic = true;
                rgbd2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                rgbd2d.velocity = Vector2.zero;
                BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
                //Set Boxcollider in place when hanging models are implemented
                boxCollider.size = new Vector2(1, .05f);
                boxCollider.offset = new Vector2(0, -3);
                gameObject.AddComponent<Semisolid>();
                break;
            case DeathType.Buzzsaw:
                animator.SetTrigger("DieFloor");
                rgbd2d.isKinematic = true;
                rgbd2d.velocity = Vector2.zero;
                GameObject bouncepad = makeBouncePad();
                bouncepad.GetComponent<Bouncer>().setVals(true, Vector2.up * bouncieness * 2);
                break;
            case DeathType.Disintigrate:
                animator.SetTrigger("Disintegrate");
                rgbd2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                Destroy(gameObject, 1.5f);
                break;
        }

        GameManager.gameManagerReference.Respawn();
    }

    public GameObject makeBouncePad()
    {
        GameObject bouncePad = new GameObject();
        bouncePad.transform.position = this.transform.position;
        bouncePad.transform.parent = this.transform;
        bouncePad.layer = 3;
        Bouncer bouncer = bouncePad.AddComponent<Bouncer>();
        bouncer.setVals(true, Vector2.up * bouncieness);
        BoxCollider2D boxCollider = bouncePad.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(1, .1f);
        boxCollider.offset = Vector2.up * .5f;
        boxCollider.isTrigger = true;
        return bouncePad;
    }

    public void MakeJumpSound()
    {
        if ((Random.Range(0, 1)) >= .5)
        {
            GetComponent<AudioSource>().PlayOneShot(jump1);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(jump2);
        }
    }
}