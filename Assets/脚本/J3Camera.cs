using UnityEngine;
using System.Collections;

public class J3Camera : MonoBehaviour
{
    public Transform target;

    public float defaultDistance = 50.0f;

    public float defaultAngle = 45.0f;

    public float SpeedDeltaX = 5.0f;

    public float SpeedDeltaY = 5.0f;

    public float SpeedDeltaZ = 1.0f;

    public float MinDistance = 1.0f;
	
	public float MaxDistance = 300.0f;

    private Vector3 respectVector;
	private Vector3 lastPos;
	private Vector3 lastDir;

    // Use this for initialization
    void Start ()
    {
        if (defaultAngle < 0)
            defaultAngle = 0;
        else if (defaultAngle > 90.0f)
            defaultAngle = 90.0f;
        respectVector = -target.forward;
        respectVector.y = 0;
        var x = Vector3.Cross(respectVector, Vector3.up);
        respectVector = Quaternion.AngleAxis(defaultAngle, x) * respectVector;
        respectVector *= defaultDistance;
	}
	
	void saveLast()
	{
		lastPos = target.position;
		lastDir = target.forward;
	}
	
	void setAsDefault()
	{
		var x = Random.value;
		var z = Random.value;
		respectVector = new Vector3(x, 0, z);
		respectVector.Normalize();
		respectVector = Vector3.RotateTowards(Vector3.up, respectVector, (90 - defaultAngle) / 180.0f * Mathf.PI, 0);
		respectVector *= defaultDistance;
		saveLast();
	}
	
	// Update is called once per frame
	void Update ()
    {
        var dz = Input.GetAxis("Mouse ScrollWheel");
        if (dz != 0)
        {
			dz *= -SpeedDeltaZ;
			var d = respectVector.magnitude + dz;
			if (d < MinDistance)
				d = MinDistance;
			else if (d > MaxDistance)
				d = MaxDistance;
			respectVector.Normalize();
			respectVector *= d;
        }

        if (Input.GetMouseButton(0))
        {
            var dx = Input.GetAxis("Mouse X");
            if (dx != 0)
                respectVector = Quaternion.AngleAxis(dx * SpeedDeltaX, Vector3.up) * respectVector;

            var dy = Input.GetAxis("Mouse Y");
            if (dy != 0)
            {
				dy *= -SpeedDeltaY;
				var a = Vector3.Angle(Vector3.up, respectVector);
				var tend = a + dy;
				if (tend <= 0.0f)
					tend = 0.1f;
				else if (tend >= 180.0f)
					tend = 179.9f;
				var realDy = tend - a;
				var axis = Vector3.Cross(Vector3.up, respectVector);
				respectVector = Quaternion.AngleAxis(realDy, axis) * respectVector;
            }
        }

        if (!Input.GetMouseButton(0) && (lastPos != target.position || lastDir != target.forward))
            followBehind();

        transform.position = target.position + respectVector;
        transform.LookAt(target);
    }

    void followBehind()
    {
		
    }
}
