using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWith : MonoBehaviour
{
    [SerializeField]
    GameObject destination;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = destination.transform.position;
        gameObject.transform.rotation = destination.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = destination.transform.position;
        gameObject.transform.rotation = destination.transform.rotation;
    }
}
