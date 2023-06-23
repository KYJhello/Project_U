using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : PlayerData
{
    [SerializeField] bool debug;
    // smg, 라이플, ar = 레이케스트
    // 샷건, 칼 = OverlapSphere
    // 최적화 위해 총알은 파티클 시스템 사용?
    [SerializeField] WeaponBase CurWeapon;
    private float cosResult;

    private void Awake()
    {
        cosResult = Mathf.Cos(CurWeapon.Angle * 0.5f * Mathf.Deg2Rad);
    }

    private void OnAttack(InputValue value)
    {
        Attack();
    }
    private void Attack()
    {
        // 1. 나이프인경우
        if(CurWeapon.GetWeaponType == WeaponType.Knife)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, CurWeapon.Range);
            foreach (Collider collider in colliders)
            {
                //2. 앞에 있는지
                Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
                if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                {
                    continue;
                }
                else
                {
                    IHittable hittable = collider.GetComponent<IHittable>();
                    hittable?.TakeHit(CurATK + CurWeapon.Damage);
                }
            }

        }
        // 2. Angle이 1인 총기의 경우 레이케스트 사용
        else if (CurWeapon.Angle == 1)
        {
            
        }
        // 3. 샷건용
        else
        {
            // 1. 범위 안에 있는지
            Collider[] colliders = Physics.OverlapSphere(transform.position, CurWeapon.Range);
            foreach (Collider collider in colliders)
            {
                //2. 앞에 있는지
                Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
                if (Vector3.Dot(transform.forward, dirTarget) < cosResult)
                {
                    continue;
                }
                else
                {
                    IHittable hittable = collider.GetComponent<IHittable>();
                    hittable?.TakeHit(CurATK + CurWeapon.Damage);
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (!debug) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, CurWeapon.Range);

        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + CurWeapon.Angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - CurWeapon.Angle * 0.5f);

        UnityEngine.Debug.DrawRay(transform.position, rightDir * CurWeapon.Range, Color.yellow);
        UnityEngine.Debug.DrawRay(transform.position, leftDir * CurWeapon.Range, Color.yellow);
    }
    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
