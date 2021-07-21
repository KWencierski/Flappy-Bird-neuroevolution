using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float force = 900f;
    [SerializeField] float gravitationRotationSpeed = 90f;
    [SerializeField] float jumpRotationSpeed = 40f;
    [HideInInspector] public float fitness;
    public float rotationSpeed = 0;
    public event System.Action OnPlayerDeath;
    Vector3 initPos;
    Quaternion initRot;
    public float dt = 0f;
    [SerializeField] Brain brain;
    SpriteRenderer sprite;
    [HideInInspector] public bool done = false;
    //public float score;
    void Start()
    {
        initPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        initRot = transform.rotation;
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        dt += Time.deltaTime;
        //transform.position = new Vector3(-1.31f, transform.position.y, transform.position.z);
        transform.position = new Vector3(initPos.x, transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            //Vector2 up = new Vector2(0, force);
            //rb.AddForce(up);
            //rotationSpeed += jumpRotationSpeed;
            //print(up.y);
        }

        if (transform.position.y > 7)
        {
            transform.position = new Vector3(transform.position.x, 6, transform.position.z);
        }

    }

    private void FixedUpdate()
    {
        rotationSpeed -= gravitationRotationSpeed;

        rotationSpeed = Mathf.Clamp(rotationSpeed, -gravitationRotationSpeed * 3, jumpRotationSpeed);
        transform.Rotate(0, 0, transform.rotation.z + rotationSpeed);
        //transform.Rotate(Vector3.forward * -rotationSpeed);

        if (transform.eulerAngles.z > 45 && transform.eulerAngles.z < 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
           
        }
            
        else if (transform.eulerAngles.z < 315 && transform.eulerAngles.z > 180)
            transform.eulerAngles = new Vector3(0, 0, 315);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (OnPlayerDeath != null)
        //{
        //    OnPlayerDeath();
        //}
        if(!done)
        {
            brain.incrementBird(this);
            done = true;
            sprite.enabled = false;
        }
    }

    public void Jump()
    {
        Vector2 up = new Vector2(0, force);
        rb.AddForce(up, ForceMode2D.Force);
        rotationSpeed += jumpRotationSpeed;
        dt = 0f;
    }

    public void ResetGame()
    {
        //Debug.Log("Reset");
        done = false;
        fitness = 0;
        sprite.enabled = true;
        rotationSpeed = 0;
        transform.position = initPos;
        transform.rotation = initRot;
        rb.velocity = Vector2.zero;
    }

    public float getVelocity()
    {
        return rb.velocity.y;
    }
}
