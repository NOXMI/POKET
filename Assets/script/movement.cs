using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class movement : NetworkBehaviour
{
    // Start is called before the first frame update
    public float editspeed;
    public Rigidbody zidan;
    private bool isPressed = false;
    private GameObject enemy;
    void Start()
    {
        Debug.Log("asd");
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.Find("Capsule");
        if (isLocalPlayer) { 
            move();
            attack() ;
        }
        
    }
    public override void OnStartLocalPlayer()
    {
        transform.GetComponent<Renderer>().material.color = Color.red;
    }
    public void move()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(new Vector3(-1, 0, 1) * Time.deltaTime * (editspeed / 1.4f),Space.World);
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(new Vector3(1, 0, 1) * Time.deltaTime * (editspeed / 1.4f), Space.World);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(new Vector3(-1, 0, -1) * Time.deltaTime * (editspeed / 1.4f), Space.World);
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(new Vector3(1, 0, -1) * Time.deltaTime * (editspeed / 1.4f), Space.World);
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime * editspeed, Space.World);
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.Translate(Vector3.back * Time.deltaTime * editspeed, Space.World);
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.Translate(Vector3.left * Time.deltaTime * editspeed, Space.World);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.Translate(Vector3.right * Time.deltaTime * editspeed, Space.World);
            }
        }
    }
    public void attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Rigidbody bulletCopy = (Rigidbody)Instantiate(zidan, this.transform.position, this.transform.rotation);
            Vector3 V = enemy.transform.position - transform.position;
            bulletCopy.velocity = bulletCopy.transform.TransformDirection(V * editspeed*1.5f);
        }
    }
}
