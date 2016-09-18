using UnityEngine;
using System.Collections;

public class J3Controller : MonoBehaviour {

    NavMeshAgent navMesh;

    Camera mainCamera;

	// Use this for initialization
	void Start () {
        NavMeshHit hit;
        if (!NavMesh.SamplePosition(transform.position, out hit, 50, 1))
            throw new System.Exception("你把角色放置的实在离地形太远！");
        else
        {
            transform.position = hit.position;
            gameObject.AddComponent<NavMeshAgent>();
        }
    }
	
	// Update is called once per frame
	void Update () {

        var w = Input.GetKey(KeyCode.W);
        var s = Input.GetKey(KeyCode.S);
        var a = Input.GetKey(KeyCode.A);
        var d = Input.GetKey(KeyCode.D);

        if (w == s && a == d)
        {

        }
        else
        {
            if (a || d)
                transform.RotateAround(transform.position, Vector3.up, (a ? 1 : -1) * Time.deltaTime);
            if (w)
                GetComponent<NavMeshAgent>().Move(transform.forward * Time.deltaTime);
            else if (s)
                GetComponent<NavMeshAgent>().Move(transform.forward * Time.deltaTime * -1);
        }
    }
}
