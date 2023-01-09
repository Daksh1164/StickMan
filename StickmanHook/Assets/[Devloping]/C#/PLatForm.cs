using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLatForm : MonoBehaviour
{
    public float JumpForce;
    public bool IsLeft;
    public bool IsRight;
    public bool IsUP;
    public bool IsDown;




    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D Ball = collision.gameObject.GetComponent<Rigidbody2D>();
        

        if (collision.collider.CompareTag("Player"))
        {
            Dax.SoundManager.instance.Sound.PlayOneShot(Dax.SoundManager.instance.Jump);
            transform.GetComponent<Animator>().Play("Platform");

            if (IsLeft)
            {
                Ball.velocity = new Vector2(-JumpForce , 0); 
            }

            if (IsRight)
            {
                Ball.velocity = new Vector2(JumpForce, 0);
            }

            if (IsUP)
            {
                Ball.velocity = new Vector2( 0, JumpForce);
            }

            if (IsDown)
            {
                Ball.velocity = new Vector2(0, -JumpForce);
            }

        }

    }


}
