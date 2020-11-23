using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAN : MonoBehaviour
{

    public List<GameObject> pixels = new List<GameObject>();
    public GameObject image;
    Renderer rend;
    public PixelScript ps;

    public float dis_threshold = -3.0f;
    public float dis_threshold_sigmoid = 0.5f;
    public float dis_sumOfPixels;
    public float dis_sigmoidApplied;
    public float dis_bias = -1.0f;

    public float weight = 1;

    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.FindGameObjectWithTag("image");
        //Fill list with children
        foreach (Transform pixel in image.transform)
        {
            pixels.Add(pixel.gameObject);
        }

    }

    void colourPixels()
    {
        foreach (GameObject pixel in pixels)
        {
            float colour = Random.Range(0.0f,2.0f);
            rend = pixel.GetComponent<Renderer>();
            rend.material.color = new Color(colour,colour,colour);    
        }
    }

    void loadNotMountain()
    {
        rend = pixels[0].GetComponent<Renderer>();
        ps = pixels[0].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        rend = pixels[1].GetComponent<Renderer>();
        ps = pixels[1].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        rend = pixels[2].GetComponent<Renderer>();
        ps = pixels[2].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        rend = pixels[3].GetComponent<Renderer>();
        ps = pixels[3].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        rend = pixels[4].GetComponent<Renderer>();
        ps = pixels[4].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        ps.setIsCenter(true);
        rend = pixels[5].GetComponent<Renderer>();
        ps = pixels[5].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        rend = pixels[6].GetComponent<Renderer>();
        ps = pixels[6].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        rend = pixels[7].GetComponent<Renderer>();
        ps = pixels[7].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        rend = pixels[8].GetComponent<Renderer>();
        ps = pixels[8].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
    }

    void loadMountain()
    {
        rend = pixels[0].GetComponent<Renderer>();
        ps = pixels[0].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        rend = pixels[1].GetComponent<Renderer>();
        ps = pixels[1].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        rend = pixels[2].GetComponent<Renderer>();
        ps = pixels[2].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        rend = pixels[3].GetComponent<Renderer>();
        ps = pixels[3].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        rend = pixels[4].GetComponent<Renderer>();
        ps = pixels[4].GetComponent<PixelScript>();
        rend.material.color = new Color(1,1,1);
        ps.setValue(rend.material.color.r);
        ps.setIsCenter(true);
        rend = pixels[5].GetComponent<Renderer>();
        ps = pixels[5].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        rend = pixels[6].GetComponent<Renderer>();
        ps = pixels[6].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        rend = pixels[7].GetComponent<Renderer>();
        ps = pixels[7].GetComponent<PixelScript>();
        rend.material.color = new Color(0,0,0);
        ps.setValue(rend.material.color.r);
        rend = pixels[8].GetComponent<Renderer>();
        ps = pixels[8].GetComponent<PixelScript>();
        rend.material.color = Color.black;
        ps.setValue(rend.material.color.r);
    }

    void discriminate()
    {
        //Debug.Log("Discriminate");

        dis_sumOfPixels = 0;

        foreach (GameObject pixel in pixels)
        {
            ps = pixel.GetComponent<PixelScript>();
            if(ps.getIsCenter())
            {
                dis_sumOfPixels += (ps.getValue() * 1);
            }

            else
            {
                dis_sumOfPixels += (ps.getValue() * -1);
            }
        }

        dis_sumOfPixels += dis_bias;

        dis_sigmoidApplied = sigmoid(dis_sumOfPixels);

        if(dis_sigmoidApplied >= dis_threshold_sigmoid)
        {
            Debug.Log("Discriminator - This is a mountain");
        }

        else
        {
            Debug.Log("Discriminator - This is NOT a mountain");
        }
    }

    void generate()
    {
        //float z = Random.Range(0.0f,1.0f);
        //float z = 1.0f;
        //Debug.Log(z);

        foreach(GameObject pixel in pixels)
        {
            //Debug.Log("pixel[" + i + "]");
            //Color c = new Color(z,z,z);
            float z = Random.Range(0.0f,1.0f);
            ps = pixel.GetComponent<PixelScript>();
            rend = pixel.GetComponent<Renderer>();
            float value;

            if(ps.getIsCenter())
            {
                value = ((weight * z) + weight);
                Debug.Log("isCenter - " + value);
            }
            else
            {
                value = ((-weight * z) - weight);
                Debug.Log("notCenter - " + value);
            }

            float sigmoidApplied = sigmoid(value);
            ps.setValue(sigmoidApplied);
            rend.material.color = new Color(sigmoidApplied,sigmoidApplied,sigmoidApplied);
        }
    }

    public float sigmoid(float value)
    {
        // 1 over 1 plus the exponential of negative value passed in
        return 1.0f / (1.0f + (float)Mathf.Exp(-value));
    }

    public float logLossFor0(float prediction)
    {
        return -Mathf.Log(1 - prediction);
    }

    public float logLossFor1(float prediction)
    {
        return -Mathf.Log(prediction);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m"))
        {
            loadMountain();
        }

        else if(Input.GetKeyDown("n"))
        {
            loadNotMountain();
        }

        else if(Input.GetKeyDown("d"))
        {
            discriminate();
        }

        else if(Input.GetKeyDown("g"))
        {
            generate();
        }
    }
}
