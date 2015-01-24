using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour
{

#if UNITY_EDITOR
    void Update()
    {
        if(Application.isPlaying == false)
        {
            transform.position = new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        }
    }
#endif
}
