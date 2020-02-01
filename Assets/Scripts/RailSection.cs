using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSection : MonoBehaviour
{
    [SerializeField]
    public Transform leftHitPosition;

    [SerializeField]
    public Transform rightHitPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (leftHitPosition == null || rightHitPosition == null)
        {
            Debug.LogError("hit position reference is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
