using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCheck : MonoBehaviour
{
    [SerializeField] vp_FPCamera cam;
    [SerializeField] GameObject shard;

    bool shardActivated;

    private void Update()
    {
        if (shard.activeInHierarchy && !shardActivated)
        {
            cam.RenderingFieldOfView = 45;
            shardActivated = true;
        }
        else
        {
            cam.RenderingFieldOfView = 60;
        }
    }
}
