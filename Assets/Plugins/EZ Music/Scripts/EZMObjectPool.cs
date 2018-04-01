using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZMusic
{
    public class EZMObjectPool<T> where T : Component
    {
        #region private variables
        private Queue<T> objects;
        private List<T> usedObjects;
        #endregion

        public T GetElement()
        {
            var nextElement = objects.Dequeue();
            usedObjects.Add(nextElement);
            return nextElement;
        }

        public void ReleaseElement(T element)
        {
            usedObjects.Remove(element);
            objects.Enqueue(element);
        }

        public void Initialize(int capacity, Transform parent = null)
        {
            objects = new Queue<T>();
            usedObjects = new List<T>();
            for (int i = 0; i < capacity; i++)
            {
                objects.Enqueue(CreateObject(parent));
            }
        }

        private T CreateObject(Transform parent = null)
        {
            GameObject obj = new GameObject("Pooled Object");
            obj.transform.parent = parent;
            T component = obj.AddComponent<T>();
            return component;
        }
    }
}
