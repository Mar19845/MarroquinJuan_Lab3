using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
   public float force = 1;
    public float jumpForce = 1;
    private Vector3 initPos;
    private int count = 0;
    Rigidbody rb;
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Return))
            player.transform.position = respawnPoint.transform.position;

    }
    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector3(horizontal, 0, vertical) * force);
    
    }

    private void Jump()
    {
        if(rb)
            if(Mathf.Abs(rb.velocity.y) < 0.05f)
            {
                rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            }
    }

    private void OnTriggerEnter(Collider collision)
    {
 
        if (collision.gameObject.CompareTag("PowerUp"))
            collision.gameObject.SetActive(false);
            count++;
        
        if (count == 4 && collision.gameObject.CompareTag("Lava"))
            collision.gameObject.SetActive(false);
        else if(count < 4  && collision.gameObject.CompareTag("Lava"))
            Destroy(this.gameObject);

    }
}
