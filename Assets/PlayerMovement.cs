using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public float speed = 5f;
    public float jumpForce = 200f;
    [SerializeField] private int currentAmmo = 0;
    public bool isJumping = false;

    private float moveInput;
    private Rigidbody2D rb2d;

    [SerializeField] private GameObject target;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Rigidbody2D bulletPrefab;
    
    [SerializeField] private TMP_Text ammoText;

    



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        rb2d.linearVelocity = new Vector2(moveInput * speed, rb2d.linearVelocity.y);


        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2d.AddForce(new Vector2(rb2d.linearVelocity.x, jumpForce));

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
            {
                currentAmmo--;
                UpdateAmmoUI();

                // แปลงตำแหน่งเมาส์ในจอ เป็นตำแหน่งในโลก 2D
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);


                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider != null)
                {

                    target.transform.position = new Vector2(hit.point.x, hit.point.y);
                    // สร้างกระสุนใหม่
                    Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                    // คำนวณความเร็วสำหรับการยิง
                    Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

                    // ใส่ความเร็วให้กระสุน
                    firedBullet.linearVelocity = projectileVelocity;

                }//hit.collider 
            }
        }//GetMouseButtonDown
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        return new Vector2(velocityX, velocityY);
    }//CalculateProjectileVelocity

    public void AddAmmo(int amount)
    {
        currentAmmo += amount;
        UpdateAmmoUI();
        
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"X{currentAmmo}";
        }
    }
}
