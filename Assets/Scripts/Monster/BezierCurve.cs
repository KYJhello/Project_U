using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform player;
    public Vector3 target;
    public Vector3 p1, p2, p3;
    public float duration;

    private void Start()
    {
        p1 = this.transform.position;
        p2 = ((player.position + this.transform.position) / 2) + Vector3.up * 5f;
        p3 = player.position;
        StartCoroutine(VezierCurve());
    }

    IEnumerator VezierCurve(float duration = 1.0f)
    {
        float time = 0f;
        while (true)
        {
            if (time > 1f)
            {
                time = 0f;
            }
            Vector3 p4 = Vector3.Lerp(p1, p2, time);
            Vector3 p5 = Vector3.Lerp(p2, p3, time);
            target = Vector3.Lerp(p4, p5, time);

            transform.position = target;
            
            time += Time.deltaTime / duration;
            yield return null;
        }
    }
}
