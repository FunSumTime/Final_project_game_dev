using UnityEngine;

public class FollowPlayer3rd : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
// this will be a third person camera follow script 
    public Vector3 offset = new Vector3(0, 5, -10);
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
     // make the camera follow the player with an offset
     transform.position = player.transform.position + offset;

    }
}
