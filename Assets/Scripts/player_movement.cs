using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    Rigidbody player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveBy = transform.right * x + transform.forward * z;
        player.MovePosition(transform.position + moveBy.normalized * speed * Time.deltaTime);

    }
}
