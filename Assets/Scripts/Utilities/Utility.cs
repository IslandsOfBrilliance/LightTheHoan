using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class Utility
{
    public static float CheckDistance(Vector3 point1, Vector3 point2)
    {
        Vector3 heading;
        float distance;
        Vector3 direction;
        float distanceSquared;

        heading.x = point1.x - point2.x;
        heading.y = point1.y - point2.y;
        heading.z = point1.z - point2.z;

        distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;
        distance = Mathf.Sqrt(distanceSquared);

        direction.x = heading.x / distance;
        direction.y = heading.y / distance;
        direction.z = heading.z / distance;
        return distance;
    }

    public static float CheckDistance(Vector2 point1, Vector2 point2)
    {
        Vector2 heading;
        float distance;
        Vector2 direction;
        float distanceSquared;

        heading.x = point1.x - point2.x;
        heading.y = point1.y - point2.y;

        distanceSquared = heading.x * heading.x + heading.y * heading.y;
        distance = Mathf.Sqrt(distanceSquared);

        direction.x = heading.x / distance;
        direction.y = heading.y / distance;
        return distance;
    }

    public static Vector3 Clamp(Vector3 vector, float clampValue)
    {
       return new Vector3(Mathf.Clamp(vector.x, -clampValue, clampValue), Mathf.Clamp(vector.y, -clampValue, clampValue), Mathf.Clamp(vector.z, -clampValue, clampValue));
    }

    public static Vector3 Clamp(Vector3 vector, float minClampValue, float maxClampValue)
    {
        return new Vector3(Mathf.Clamp(vector.x, minClampValue, maxClampValue), Mathf.Clamp(vector.y, minClampValue, maxClampValue), Mathf.Clamp(vector.z, minClampValue, maxClampValue));
    }

    public static Transform GetClosestEnemy(Collider[] enemies, Vector3 currentPos)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Collider col in enemies)
        {
            float dist = CheckDistance(col.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = col.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    public static T GetRandomItem<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> GetRandomItems<T> (this List<T> list, int amount)
    {
        List<T> origList = new List<T>();
        foreach(T item in list)
            origList.Add(item);

        List<T> tempList = new List<T>();

        for(int i = amount; i > 0; i--)
        {
            T t = GetRandomItem(origList);
            tempList.Add(t);
            origList.Remove(t);
        }

        return tempList;
    }

    public static bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public static Vector3 GetPoint(Vector3 origin, Vector3 direction, float distance, LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = new Ray(origin, direction);
        if(Physics.Raycast(ray, out hit, distance, mask))
        {
            Vector3 point = new Vector3(origin.x, origin.y, hit.point.z + (hit.normal.z * 1.25f));
            Debug.Log(point);
            return point;
        }
        else return ray.GetPoint(distance);
    }

    public static bool ConvertStringToBool(string input)
    {
        switch (input.ToLower())
        {
            case "false":
                return false;
            case "true":
                return true;
            default:
                return false;
        }
    }
}