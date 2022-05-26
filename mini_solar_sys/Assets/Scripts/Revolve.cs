using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{
    public Transform target;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = this.gameObject.transform;
            Debug.Log("Revolve target not specified. Defaulting to parent GameObject");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
    }
}
