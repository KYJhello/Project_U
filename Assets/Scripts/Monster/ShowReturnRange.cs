using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowReturnRange : MonoBehaviour
{
    private float range;
    [SerializeField] GameObject sphere;

    private void Awake()
    {
        range = GetComponentInChildren<SpiderKingData>().ReturnDistance * 2;
    }

    private void Start()
    {
        sphere.transform.localScale = new Vector3(range, 1, range);
        OnDisable();
    }

    public void OnEnable()
    {
        sphere.SetActive(true);
    }
    public void OnDisable()
    {
        sphere.SetActive(false);
    }
}
