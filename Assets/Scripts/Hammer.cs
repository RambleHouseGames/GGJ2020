using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    public Transform HitPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (HitPosition == null)
        {
            Debug.LogError("hit position reference is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
