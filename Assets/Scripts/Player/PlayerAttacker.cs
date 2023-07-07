using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    private PlayerData data;
    [SerializeField] bool debug;
    // smg, ������, ar = �����ɽ�Ʈ
    // ����, Į = OverlapSphere
    // ����ȭ ���� �Ѿ��� ��ƼŬ �ý��� ���?
    [SerializeField] WeaponBase curWeapon;             // �� ����
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    //[SerializeField] TrailRenderer bulletTrail;
    private float cosResult;


    private void Awake()
    {
        cosResult = Mathf.Cos(curWeapon.Angle * 0.5f * Mathf.Deg2Rad);
        data = GetComponent<PlayerData>();
    }

    private void OnAttack(InputValue value)
    {
        Attack();
    }
    private void Attack()
    {
        // 1. �������ΰ��
        if(curWeapon.GetWeaponType == WeaponType.Knife)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, curWeapon.Range, targetMask);
            foreach (Collider collider in colliders)
            {
                //2. �տ� �ִ���
                Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
                if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                {
                    continue;
                }
                else
                {
                    IHittable hittable = collider.GetComponent<IHittable>();
                    hittable?.TakeHit(data.CurATK + curWeapon.Damage);
                }
            }
        }
        // 2. Angle�� 1�� �ѱ��� ��� �����ɽ�Ʈ ���
        else if (curWeapon.Angle == 1)
        {
            data.Anim.SetTrigger("Fire");
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, curWeapon.Range))
            {
                IHittable target = hit.transform.GetComponent<IHittable>();
                target?.TakeHit(data.CurATK + curWeapon.Damage);

                TrailRenderer trail = GameManager.Resource.Instantiate<TrailRenderer>("Effect/BulletTrail", curWeapon.transform.position, Quaternion.identity, true);
                trail.Clear();
                float totalTime = Vector2.Distance(curWeapon.transform.position, hit.point) / 10f;
                float rate = 0;
                while (rate < 1)
                {
                    trail.transform.position = Vector3.Lerp(curWeapon.transform.position, hit.point, rate);
                    rate += Time.deltaTime / totalTime;
                }
                GameManager.Resource.Destroy(trail.gameObject, 3f);
            }
        }
        // 3. ���ǿ�
        else
        {
            data.Anim.SetTrigger("Fire");
            // 1. ���� �ȿ� �ִ���
            Collider[] colliders = Physics.OverlapSphere(transform.position, curWeapon.Range, targetMask);
            foreach (Collider collider in colliders)
            {
                //2. �տ� �ִ���
                Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
                if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                {
                    continue;
                }
                else
                {
                    IHittable hittable = collider.GetComponent<IHittable>();
                    hittable?.TakeHit(data.CurATK + curWeapon.Damage);

                    if (hittable != null)
                    {
                        TrailRenderer trail = GameManager.Resource.Instantiate<TrailRenderer>("Effect/BulletTrail", curWeapon.transform.position, Quaternion.identity, true);
                        trail.Clear();
                        float totalTime = Vector2.Distance(curWeapon.transform.position, collider.transform.position) / 10f;
                        float rate = 0;
                        while (rate < 1)
                        {
                            trail.transform.position = Vector3.Lerp(curWeapon.transform.position, collider.transform.position, rate);
                            rate += Time.deltaTime / totalTime;
                        }
                        GameManager.Resource.Destroy(trail.gameObject, 3f);
                    }
                }
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        if (!debug) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, curWeapon.Range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + curWeapon.Angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - curWeapon.Angle * 0.5f);

        UnityEngine.Debug.DrawRay(transform.position, rightDir * curWeapon.Range, Color.yellow);
        UnityEngine.Debug.DrawRay(transform.position, leftDir * curWeapon.Range, Color.yellow);
    }
    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
