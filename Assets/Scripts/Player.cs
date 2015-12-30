using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;
    public int fallBoundary = -10;
    bool alive;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = .25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    Controller2D controller;
    Animator animator;
    private DeathCounter deathCounter;

    public GUIText textDied;

    void Start()
    {
        textDied.text = "";
        alive = true;
        GameObject deathObjectCounter = GameObject.FindWithTag("DeathCounter");
        if (deathObjectCounter != null)
        {
            deathCounter = deathObjectCounter.GetComponent<DeathCounter>();
        }
        if (deathCounter == null)
        {
            Debug.Log("Ne moze naci'GameController' skriptu");
        }
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
        animator = GetComponent<Animator>();
        animator.SetBool("heDead", false);
    }

    void Update()
    {
        animator.SetBool("uZraku", false);
        animator.SetBool("uZrakuLijevo", false);
        animator.SetBool("wallStickDesno", false);
        animator.SetBool("wallStickLijevo", false);
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        int wallDirX = (controller.collisions.left) ? -1 : 1;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

        bool wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;
            if(controller.collisions.right)
                animator.SetBool("wallStickDesno", true);
            if (controller.collisions.left)
                animator.SetBool("wallStickLijevo", true);
            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (input.x != wallDirX && input.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space)&&moveSpeed!=0)
        {
            if (wallSliding)
            {
                if (wallDirX == input.x)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime, input);

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        if (velocity.y != 0&&input.x>0)
        {
            animator.SetBool("uZraku", true);
        }
        if (velocity.y != 0 && input.x < 0)
        {
            animator.SetBool("uZrakuLijevo", true);
        }
        if (transform.position.y <= fallBoundary)
        {
            moveSpeed = 0;
            if (alive)
            {
                textDied.text = "#Mrtav";
                StartCoroutine(Die());
            }
            alive = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            moveSpeed = 0;
            if (alive)
            {
                textDied.text = "#Mrtav";
                StartCoroutine(Die());
            }
            alive = false;
        }
    }
    IEnumerator Die()
    {
        animator.SetBool("heDead", true);
        yield return new WaitForSeconds(1);
        deathCounter.Count(1);
        animator.SetBool("heDead", false);
        moveSpeed = 6;
        textDied.text = "";
        alive = true;
    }

}
