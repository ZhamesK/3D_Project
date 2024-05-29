using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject MobileTrap_1;
    public GameObject MobileTrap_2;
    public float moveSpeed;

    public LayerMask playerLayerMask;

    private Vector3 originPosition_1 = new Vector3();
    private Vector3 originPosition_2 = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        originPosition_1 = MobileTrap_1.transform.position;
        originPosition_2 = MobileTrap_2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        IsSensored();
        ActivateTrap();
    }

    public void ActivateTrap()
    {
        // trapRay에 감지되면 오브젝트를 양 옆으로 이동
        if (IsSensored())
        {
            MobileTrap_1.transform.position = new Vector3(-60, 35, 130);
            MobileTrap_2.transform.position = new Vector3(-60, 35, 60);
        }
        // 감지되지 않을 때 오브젝트 원위치
        else if (!IsSensored())
        {
            MobileTrap_1.transform.position = originPosition_1;
            MobileTrap_2.transform.position = originPosition_2;
        }
    }

    private bool IsSensored()
    {
        Ray[] trapRay = new Ray[4]
        {
        new Ray(transform.position + transform.up + transform.right, Vector3.forward),
        new Ray(transform.position + transform.up - transform.right, Vector3.forward),
        new Ray(transform.position - transform.up + transform.right, Vector3.forward),
        new Ray(transform.position - transform.up - transform.right, Vector3.forward)
        };

        for (int i = 0; i < trapRay.Length; i++)
        {
            if (Physics.Raycast(trapRay[i], 60f, playerLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
