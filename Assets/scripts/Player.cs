 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{ 
    private Rigidbody2D rigid;
   private Animator anim;
    [SerializeField]
    private float speed; 
     [SerializeField]
   private bool facingRight;
    [SerializeField] 
    private float jumpForce; 
      [SerializeField] 
      private Transform []groundTransform; 
      
      [SerializeField] 
      private float groundRadius;
       [SerializeField] 
      private LayerMask groundLayer;
      private bool grounded; 
      private int score;
          [SerializeField] 
       private TMP_Text scoreText;    
     void Start()
    { 
        facingRight= true;
       rigid=GetComponent<Rigidbody2D>();
       anim=GetComponent<Animator>();
       score=0;
       scoreText.text = "Score : " + score.ToString();

    } 
    void Update() 
    { 
       if (grounded && Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) 
        {

        grounded=false; 
        anim.SetBool("groundcheck",grounded);
        rigid.AddForce(new Vector2(0,jumpForce));
       } 
       scoreText.text= "Score : " + score.ToString();

    }

   
    void FixedUpdate()
    { 
      grounded = isGrounded();
      anim.SetBool("groundcheck",grounded);
      anim.SetFloat("yAxisSpeed", rigid.velocity.y);

       float horizontal=Input.GetAxis("Horizontal");
       Movement(horizontal);
       Flip(horizontal);
      
    } 
    void Movement(float horizontal)
    { 
       rigid.velocity= new Vector2(horizontal*speed,rigid.velocity.y);
       anim.SetFloat("speed",Mathf.Abs(horizontal));
    } 
    void Flip(float horizontal) 
    { 
         if(horizontal > 0 && !facingRight || (horizontal < 0 && facingRight))
         { 
            facingRight = !facingRight;
            transform.localScale =  new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
         }
    } 
    private bool  isGrounded()
    { 
      if(rigid.velocity.y <= 0)
      { 
         foreach(Transform trans in groundTransform)
         { 
            Collider2D []colliders=Physics2D.OverlapCircleAll(trans.position,groundRadius,groundLayer);
            for(int i=0; i<colliders.Length; i++)
            { 
               if(colliders[i].gameObject!=gameObject)
               { 
                  return true;
               }
            }
         }
      } 
      return false;
    } 
    private void OnTriggerEnter2D(Collider2D other){ 
      if(other.gameObject.tag=="Coin"){ 
         Destroy(other.gameObject);
         score++;
      }
    }
}
