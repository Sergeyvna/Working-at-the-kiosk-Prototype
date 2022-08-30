using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public bool canMove;
    public int sceneIndx;

    [SerializeField] 
    private float movespeed = 5.0f;
    
    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        canMove = true;
    }

    void Update()
    {
        //Changing the size of player based on their position
        float scaleValue = 1 - ( transform.position.y / 10);
        transform.localScale =  new Vector3 ( scaleValue , scaleValue , scaleValue );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if(canMove)
        {
            var xPos = Input.GetAxis("Horizontal");
            var yPos = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(xPos * movespeed * Time.deltaTime, yPos * movespeed * Time.deltaTime);

            if(xPos != 0 || yPos != 0)
                animator.SetBool("isWalking",true);
            else
                animator.SetBool("isWalking",false);

           
            if(xPos < 0)
                GetComponent<SpriteRenderer>().flipX = true;
            else if(xPos > 0)
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else
            animator.SetBool("isWalking",false);

    }
}
