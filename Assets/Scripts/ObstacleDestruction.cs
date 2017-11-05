using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestruction : MonoBehaviour
{
    public float _lifeSpan = 2.0f;
    private void OnEnable()
    {
        Invoke("Destory", _lifeSpan);
    }

    void Destory()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
