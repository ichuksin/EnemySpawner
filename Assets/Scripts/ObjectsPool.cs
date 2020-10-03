using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;

public class ObjectsPool : MonoBehaviour
{

    [SerializeField] private GameObject _container;
    [SerializeField] private int _count;

    private List<GameObject> _pool = new List<GameObject>();
    private Camera _camera;
    protected void Initialize(GameObject prefab)
    {
        _camera = Camera.main;
        for (int i = 0; i < _count; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }
    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(gameObject => gameObject.activeSelf == false);
        return result != null;
    }
    protected void DisableObjectsAbroutScreen()
    {
        foreach (var item in _pool)
        {
            if (item.activeSelf)
            {
                Vector3 position = _camera.WorldToViewportPoint(item.transform.position);
                if (position.x < 0)
                    item.SetActive(false);
            }
        }
    }
    public void ResetPool()
    {
        foreach(var item in _pool)
        {
            item.SetActive(false);
        }
    }
}
