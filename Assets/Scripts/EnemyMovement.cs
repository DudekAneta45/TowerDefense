using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float movemenetPeriod = .5f;
    [SerializeField] ParticleSystem goalParticle;
    [SerializeField] AudioClip reachingPlayerBaseSFX;

    // Use this for initialization
    void Start ()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<WayPoint> path)
    {
        var lastPosition = transform.position;

        foreach (WayPoint waypoint in path)
        {
            if (lastPosition.z > waypoint.transform.position.z && lastPosition.x == waypoint.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if(lastPosition.z < waypoint.transform.position.z && lastPosition.x == waypoint.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            if (lastPosition.x < waypoint.transform.position.x && lastPosition.z == waypoint.transform.position.z)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (lastPosition.x > waypoint.transform.position.x && lastPosition.z == waypoint.transform.position.z)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            transform.position = new Vector3(waypoint.transform.position.x, waypoint.transform.position.y, waypoint.transform.position.z + 4);

            lastPosition = waypoint.transform.position;

            yield return new WaitForSeconds(movemenetPeriod);
        }
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        AudioSource.PlayClipAtPoint(reachingPlayerBaseSFX, Camera.main.transform.position);

        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();
        float destroyDelay = vfx.main.duration;
        Destroy(vfx.gameObject, destroyDelay);

        Destroy(gameObject);
    }

}
