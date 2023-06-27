using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    Dictionary<string, Object> resources = new Dictionary<string, Object>();

    public T Load<T>(string path) where T : Object
    {
        // 타입과 경로로 키값 설정
        string key = $"{typeof(T)}.{path}";

        // as는 다이나믹 캐스트, 스태틱 캐스트 사용하려면 (T) 형변환 사용
        if (resources.ContainsKey(key))
            return resources[key] as T;

        T resource = Resources.Load<T>(path);
        resources.Add(key, resource);
        return resource;
    }
}
