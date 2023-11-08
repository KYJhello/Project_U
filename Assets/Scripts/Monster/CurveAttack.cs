using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveAttack : MonoBehaviour
{
    Transform target;
    float time;
    int damage;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnEnable()
    {
        time = 2f;
        StartCoroutine(AttackRoutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void SetDamage(int d)
    {
        damage = d;
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        float xSpeed = (target.position.x - transform.position.x) / time;
        float zSpeed = (target.position.z - transform.position.z) / time;
        float ySpeed = -1 * (0.5f * Physics.gravity.y * time * time + transform.position.y) / time;

        float curTime = 0;
        while (curTime < time)
        {
            curTime += Time.deltaTime;
            ySpeed += Physics.gravity.y * Time.deltaTime;

            transform.position += new Vector3(xSpeed, ySpeed, zSpeed) * Time.deltaTime;

            yield return null;
        }
        GameManager.Resource.Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer == 7)
        {
            return;
        }
        if(!(collider.gameObject.layer == 3 || collider.gameObject.layer == 7))
        {
            GameManager.Resource.Destroy(this.gameObject);
        }
        IHittable hittable = collider.GetComponent<IHittable>();
        hittable?.TakeHit(damage);

        if (hittable != null)
        {
            float totalTime = Vector2.Distance(transform.position, collider.transform.position) / 10f;
            float rate = 0;
            while (rate < 1)
            {
                rate += Time.deltaTime / totalTime;
            }
        }
        //GameManager.Resource.Destroy(this.gameObject);
    }
    private Vector3[] BezierCurve(int pointCount, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3[] targetPoints = new Vector3[pointCount + 1];
        Vector3 target;

        for (int i = 0; i < pointCount + 1; i++)
        {
            Vector3 p4 = Vector3.Lerp(p1, p2, time);
            Vector3 p5 = Vector3.Lerp(p2, p3, time);
            target = Vector3.Lerp(p4, p5, time);

            targetPoints[i] = target;
        }

        return targetPoints;
    }
    IEnumerator ProjectilesMove(Vector3[] targets)
    {
        int i = 0;
        while (true)
        {
            transform.Translate(targets[i]);
        }
    }
}
