using UnityEngine;

public class BasketballEvent : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.AddAmmo(1);
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
         if (collision.gameObject.CompareTag("Score"))
        {
            ScorePoint Score = collision.gameObject.GetComponent<ScorePoint>();


            Score.AddScore(10);

            Destroy(gameObject);
        }
    }

}
