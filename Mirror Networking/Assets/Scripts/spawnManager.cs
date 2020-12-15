using UnityEngine;
using Mirror;

public class Example : NetworkBehaviour
{
    //Assign the prefab in the Inspector
    public GameObject m_MyGameObject;
    GameObject m_MyInstantiated;

    void Start()
    {
        //Instantiate the prefab
        m_MyInstantiated = Instantiate(m_MyGameObject);
        //Spawn the GameObject you assign in the Inspector
        NetworkServer.Spawn(m_MyInstantiated);
    }
}
