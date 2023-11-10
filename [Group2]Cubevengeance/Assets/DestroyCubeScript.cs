using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCubeScript : MonoBehaviour
{
    //#2 DESPAWN ON HIT WITH FLOOR

    //ADD A BOX COLLIDER TO THE FLOOR
    //ATTACH THIS TO PREFAB CUBE
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "Floor")
        {
            Debug.Log("Despawned");
            Destroy(this.gameObject);
        }
    }
}
