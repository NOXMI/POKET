using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TestScript : MonoBehaviour
{
    public GameObject genObject;
    public float frequency;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Generate), frequency, frequency);
    }

    void Generate()
    {
        Instantiate(genObject, transform.position, transform.rotation);
    }
}
