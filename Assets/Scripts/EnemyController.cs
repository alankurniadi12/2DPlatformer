using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyController : MonoBehaviour
{
    public bool isGrounded = false; //UNTUK MENGECEK KARAKTER BERADA DI GROUND
    public bool isFacingRight = false;
    public Transform batas1; // BATAS GERAK KIRI
    public Transform batas2; // BATAS GERAK KE KANAN
    float speed = 2; // KECEPATAN ENEMY

    Rigidbody2D rigid;
    Animator anim;
    public int HP = 1;
    bool isDie = false;
    public static int EnemyKilled = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            if (isFacingRight)
                MoveRight();
            else
                MoveLeft();

            if (transform.position.x >= batas2.position.x && isFacingRight)
                Flip();
            else if (transform.position.x <= batas1.position.x && !isFacingRight)
                Flip();
        }

        void MoveRight()
        {
            Vector3 pos = transform.position;
            pos.x += speed * Time.deltaTime;
            transform.position = pos;
            if (!isFacingRight)
            {
                Flip();
            }
        }
        void MoveLeft()
        {
            Vector3 pos = transform.position;
            pos.x -= speed * Time.deltaTime;
            transform.position = pos;
            if (isFacingRight)
            {
                Flip();
            }
        }
        void Flip()
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            isFacingRight = !isFacingRight;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            isDie = true;
            rigid.velocity = Vector2.zero;
            anim.SetBool("IsDie", true);
            Destroy(this.gameObject, 2);
            Data.score += 20;
            EnemyKilled++;

            if (EnemyKilled == 3)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

}
