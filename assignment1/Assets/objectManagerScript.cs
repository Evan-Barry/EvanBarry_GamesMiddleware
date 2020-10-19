using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectManagerScript : MonoBehaviour, iCollidable
{
    GameObject sphere;
    public GameObject spherePrefab;
    GameObject plane;
    public GameObject planePrefab;
    public Plane_script[] planes;
    public Sphere_script[] spheres;
    public Sphere_script s1, s2;
    public float d;
    public Vector3 s1_point_of_impact;
    public Vector3 s2_point_of_impact;
    public Vector3 s1Normal;
    public Vector3 s2Normal;
    public Vector3 s1Para;
    public Vector3 s1Perp;
    public Vector3 s2Para;
    public Vector3 s2Perp;
    private float Coefficient_of_Restitution = 0.6f;
    LineRenderer lr;
    public Vector3 defaulPos = new Vector3(0,0,0);
    //public float time_of_impact;
    public int spheresLeft;
    public Text scoreText;
    public Text gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            plane = (GameObject)Instantiate(planePrefab, defaulPos, Quaternion.identity);
            plane.name = "Plane" + i;
            sphere = (GameObject)Instantiate(spherePrefab, new Vector3(getRandomCoOrd(), 100, getRandomCoOrd()), Quaternion.identity);
            sphere.name = "Sphere" + i;
        }

        planes = FindObjectsOfType(typeof(Plane_script)) as Plane_script[];
        spheres = FindObjectsOfType(typeof(Sphere_script)) as Sphere_script[];

        planes[0].define_plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0));
        planes[1].define_plane(new Vector3(5, 0, 0), new Vector3(-1, 0, 0));
        planes[2].define_plane(new Vector3(-5, 0, 0), new Vector3(1, 0, 0));
        planes[3].define_plane(new Vector3(0, 0, 5), new Vector3(0, 0, -1));
        planes[4].define_plane(new Vector3(0, 0, -5), new Vector3(0, 0, 1));

        foreach(Sphere_script sphere in spheres)
        {
            sphere.setMass(Random.Range(1f,10f));
            sphere.setHP((int)System.Math.Round(sphere.mass * 100f));
            sphere.accelleration = new Vector3(sphere.accelleration.x, sphere.accelleration.y * sphere.mass, sphere.accelleration.z);
         }

        s1 = spheres[0];//user sphere

        s1.transform.position = new Vector3(planes[0].transform.position.x, planes[0].transform.position.y + s1.Radius_of_sphere, planes[0].transform.position.z - 2.5f);

        lr = s1.GetComponent<LineRenderer>();
        lr.positionCount = 2;

        //score = 0;
        spheresLeft = spheres.Length;
        scoreText.text = "Spheres Left - " + (spheresLeft - 1);
        gameOverText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Sphere_script s in spheres)
        {
            foreach (Plane_script plane in planes)
            {
                s.setNearestPlane(plane);
            }

            s.setNearestSphere();
            Sphere_script nearestSphere = s.getNearestSphere();

            d = Vector3.Distance(s.transform.position, nearestSphere.transform.position);

            if(d <= s.Radius_of_sphere + nearestSphere.Radius_of_sphere)
            {
                isColliding_Sphere(s, nearestSphere);
            }

            if(s.getHP() <= 0 && s.getActive())
            {
                s.transform.position = new Vector3(s.transform.position.x, 1000, s.transform.position.z);
                s.accelleration = new Vector3(0,0,0);
                spheresLeft--;
                s.setActive(false);
                scoreText.text = "Spheres Left - " + spheresLeft;
            }

            if(spheresLeft == 1)
            {
                //Game Over Text
                gameOverText.enabled = true;
            }

        }

        if(Input.GetKey(KeyCode.A))
        {
            s1.transform.RotateAround(s1.transform.position, s1.transform.up, -0.5f);
        }

        if(Input.GetKey(KeyCode.D))
        {
            s1.transform.RotateAround(s1.transform.position, s1.transform.up, 0.5f);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            s1.velocity += s1.transform.forward * 100 * Time.deltaTime;
        }

        lr.SetPosition(0, s1.transform.position);
        lr.SetPosition(1, s1.transform.forward * 20);

    }

    public Sphere_script[] GetSpheres()
    {
        return spheres;
    }

    public Vector3 newV1Parallel(float m1, float m2, Vector3 oldV1Para, Vector3 oldV2Para)
    {
        //Formula - (((m1-m2)/(m1+m2)*oldV1Para) + ((2*m2)/(m1+m2))*oldV2Para)

        float m1Minusm2 = m1 - m2;
        float m1Plusm2 = m1 + m2;
        float m2Times2 = m2 * 2;

        float firstDivision = m1Minusm2 / m1Plusm2;
        float secondDivision = m2Times2 / m1Plusm2;

        return (oldV1Para * firstDivision) + (oldV2Para * secondDivision);
        
    }

    public Vector3 newV2Parallel(float m1, float m2, Vector3 oldV1Para, Vector3 oldV2Para)
    {
        //Formula - (((m2-m1)/(m1+m2)*oldV2Para) + ((2*m1)/(m1+m2))*oldV1Para)

        float m2Minusm1 = m2 - m1;
        float m1Plusm2 = m1 + m2;
        float m1Times2 = m1 * 2;

        float firstDivision = m2Minusm1 / m1Plusm2;
        float secondDivision = m1Times2 / m1Plusm2;

        return (oldV2Para * firstDivision) + (oldV1Para * secondDivision);
    }

    public float getRandomCoOrd()
    {
        return Random.Range(-4.5f, 4.5f);
    }

    public void isColliding_Sphere(Sphere_script s1, Sphere_script s2)
    {
        Debug.Log("Spheres Colliding! " + s1.name + " " + s2.name);
        
        if(s1.name.Equals("Sphere4") || s2.name.Equals("Sphere4"))
        {
            //score++;
            
            if(s1.name.Equals("Sphere4"))
            {
                int force = (int)System.Math.Round((s1.velocity.x + s1.velocity.z));
                if(force < 0)
                {
                    force = -force;
                }
                s2.setHP(s2.getHP() - force);
                Debug.Log(s2.getHP());
            }
            else
            {
                int force = (int)System.Math.Round((s2.velocity.x + s2.velocity.z));
                if(force < 0)
                {
                    force = -force;
                }
                s1.setHP(s1.getHP() - force);
                Debug.Log(s1.getHP());
            }
        }

        //Get Normals
        s1Normal = (s2.transform.position - s1.transform.position).normalized;
        s2Normal = -s1Normal;

        //Get Para and Perp of S1 velocity at collision
        s1Para = s1.parallel_component(s1.velocity, s1Normal);
        s1Perp = s1.perpendicular_component(s1.velocity, s1Normal);

        //Get Para and Perp of S2 velocity at collision
        s2Para = s2.parallel_component(s2.velocity, s2Normal);
        s2Perp = s2.perpendicular_component(s2.velocity, s2Normal);

        //Get point of impact for both spheres
        s1_point_of_impact = s1.transform.position + (s1.Radius_of_sphere * s1Normal);
        s2_point_of_impact = s2.transform.position - (s2.Radius_of_sphere * s2Normal);

        //Get time of impact
        //time_of_impact = Time.deltaTime * (s1.distance_to_nearest_sphere1 / (s1.distance_to_nearest_sphere1 - s1.distance_to_nearest_sphere2));

        //Move spheres back
        s1.transform.position -= s1_point_of_impact * Time.deltaTime;
        s2.transform.position += s2_point_of_impact * Time.deltaTime;

        //Calculate new Para for S1 velocity after collision and aplly to the velocity
        Vector3 newV1Para = newV1Parallel(s1.mass, s2.mass, s1Para, s2Para);
        s1.velocity = s1Perp + (Coefficient_of_Restitution * newV1Para);

        //Calculate new Para for S2 velocity after collision and aplly to the velocity
        Vector3 newV2Para = newV2Parallel(s1.mass, s2.mass, s1Para, s2Para);
        s2.velocity = s2Perp + (Coefficient_of_Restitution * newV2Para);

        //Move the spheres with new velocity (Point of impact needed?)
        s1.transform.position += (s1.velocity * Time.deltaTime);
        s2.transform.position -= (s2.velocity * Time.deltaTime);
    }
}
