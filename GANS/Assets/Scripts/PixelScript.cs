using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelScript : MonoBehaviour
{

    public float value;
    public bool isCenter = false;

    public void setValue(float v)
    {
        value = v;
    }

    public float getValue()
    {
        return value;
    }

    public void setIsCenter(bool isC)
    {
        isCenter = isC;
    }

    public bool getIsCenter()
    {
        return isCenter;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
