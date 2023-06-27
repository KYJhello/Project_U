using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    Dictionary<string, Object> resources = new Dictionary<string, Object>();

    public T Load<T>(string path) where T : Object
    {
        // Ÿ�԰� ��η� Ű�� ����
        string key = $"{typeof(T)}.{path}";

        // as�� ���̳��� ĳ��Ʈ, ����ƽ ĳ��Ʈ ����Ϸ��� (T) ����ȯ ���
        if (resources.ContainsKey(key))
            return resources[key] as T;

        T resource = Resources.Load<T>(path);
        resources.Add(key, resource);
        return resource;
    }
}
