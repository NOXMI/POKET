using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randommove : MonoBehaviour
{
    public GameObject MONOO;
    private float UR,DR;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UR = Random.Range(-1f, 1f);
        DR = Random.Range(-1f, 1f);
        MONOO.transform.Translate(new Vector3(UR, 0, DR) *Time.deltaTime*3);
    }
}
