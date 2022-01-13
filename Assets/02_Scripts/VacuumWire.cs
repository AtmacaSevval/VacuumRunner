using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Twenty.Managers;

public class VacuumWire : MonoBehaviour
{
	public Transform hoseHeadConnectionPoint;
	public PathCreator hosePath;

	public float hoseThickness = 1;
	public MeshFilter hoseMeshFilter;
	float hoseLength;

	const int numHosePoints = 40;

	public float w1;
	public float w2;

	Rigidbody rb;
	Vector3[] hosePointsLocal;
	Vector3[] hosePointsWorld;

	IK ikSolver;
	Mesh hoseMesh;

	public Transform meshPos;

	private Vector3 velocity = Vector3.zero;

	public Transform Character;
	float xOffset;

	private void Awake()
    {
		rb = GetComponent<Rigidbody>();
	}

    void Start()
	{
		ikSolver = new IK();

		hosePointsLocal = new Vector3[numHosePoints];
		hosePointsWorld = new Vector3[numHosePoints];
		for (int i = 0; i < numHosePoints; i++)
		{
			float t = i / (numHosePoints - 1f);
			Vector3 hosePointWorld = hosePath.path.GetPointAtTime(1 - t, EndOfPathInstruction.Stop);
			hosePointsLocal[i] = hoseHeadConnectionPoint.InverseTransformPoint(hosePointWorld);
		}

		for (int i = 0; i < numHosePoints - 1; i++)
		{
			hoseLength += Vector3.Distance(hosePointsLocal[i], hosePointsLocal[i + 1]);
		}

	}

	Vector3 v;
	Quaternion r;
	bool updateRot;

	void Update()
	{

		Hose();

		xOffset = Character.transform.position.x - transform.position.x;
		//Follow();
		velocity.x = xOffset * 2f;
		rb.position += velocity * Time.deltaTime;
	}
	
	void FixedUpdate()
	{
		rb.velocity = velocity;
		/*rb.AddForce(v , ForceMode.VelocityChange);
		if (updateRot)
		{
			rb.rotation = Quaternion.Slerp(rb.rotation, r, Time.deltaTime * 2);
		}*/
	}

    void Follow()
	{
		Vector3 offset = hosePointsWorld[hosePointsWorld.Length - 1] - hosePath.transform.position;
		float dst = -(offset).magnitude;
		if (Mathf.Abs(dst) > 0.01f)
		{
			rb.MovePosition(rb.position + offset);
		}

		float hoseDstBetweenBodyAndHead = Vector3.Distance(hosePath.transform.position, hoseHeadConnectionPoint.position);
		float bodyFollowHeadT = hoseDstBetweenBodyAndHead / hoseLength;
		float followStrength = Mathf.Pow(Mathf.InverseLerp(0.7f, 1, bodyFollowHeadT), 3);
		v = new Vector3(offset.x, 0, offset.z).normalized * followStrength;
		updateRot = false;
		if (followStrength > 0f)
		{
			updateRot = true;
			float moveAngle = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
			r = Quaternion.Euler(Vector3.up * moveAngle);
		}
	}

	void Hose()
	{
		for (int i = 0; i < numHosePoints; i++)
		{
			hosePointsWorld[i] = hoseHeadConnectionPoint.TransformPoint(hosePointsLocal[i]);
		}

		ikSolver.w1 = w1;
		ikSolver.w2 = w2;
		ikSolver.useDirBias = true;
		ikSolver.endBiasDir = hoseHeadConnectionPoint.up; //süpürge baþlýðýnýn olduðu yer
		ikSolver.startBiasDir = hosePath.transform.up; //fanusun üst kýsmýnýn olduðu yer
		ikSolver.Solve(hosePointsWorld, hosePath.transform.position);

		for (int i = 0; i < numHosePoints; i++)
		{
			hosePointsLocal[i] = hoseHeadConnectionPoint.InverseTransformPoint(hosePointsWorld[i]);
		}

		CylinderGenerator.CreateMesh(ref hoseMesh, hosePointsWorld, 10, hoseThickness);

		/*for(int i = 0; i<hosePointsWorld.Length; i++)
        {
			Debug.Log(hosePointsWorld[i] + " "+ i);
        }*/
		hoseMeshFilter.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity); // mesh is in worldspace
		hoseMeshFilter.mesh = hoseMesh;
	}

	void OnDrawGizmos()
	{
		if (Application.isPlaying && hosePointsLocal != null)
		{
			for (int i = 0; i < numHosePoints; i++)
			{
				Gizmos.DrawSphere(hosePointsWorld[i], 0.05f);
			}
		}
	}

    private void OnEnable()
    {
		GameManager.onLevelStart += delegate
		{
			velocity = new Vector3(0, 0, 6);
		};
		GameManager.onLevelOver += delegate
		{
			velocity = new Vector3(0, 0, 0);
		};
	}


	void OnValidate()
	{
		if (hosePath != null)
		{
			hosePath.bezierPath.SetPoint(0, Vector3.zero);
			hosePath.bezierPath.SetPoint(hosePath.bezierPath.NumPoints - 1, hosePath.transform.InverseTransformPoint(hoseHeadConnectionPoint.position));
		}
	}

}

