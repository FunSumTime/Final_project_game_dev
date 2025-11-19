using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    private float horizontalInput;
    private float forwardInput;
    //anim = GetComponent<Animator>();

    void Start()
    {

    }

    // Update is called once per frame
    void walk()
    {
        
    }
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Debug.Log("Horizontal Input: " + horizontalInput + " Forward Input: " + forwardInput);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
    }
}
