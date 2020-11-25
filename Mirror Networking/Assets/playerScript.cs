using Mirror;
using UnityEngine;

public class playerScript : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 2, -2);
        //Camera.main.transform.localRotation = new Quaternion(20, 0, 0, 0);
    }

    void Update()
    {
        if (!isLocalPlayer) { return; }

        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 110.0f;
        float moveZ = Input.GetAxis("Vertical") * Time.deltaTime * 4f;

        transform.Rotate(0, moveX, 0);
        transform.Translate(0, 0, moveZ);
    }
}
