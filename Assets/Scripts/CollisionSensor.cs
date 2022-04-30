using UnityEngine;
using System.Collections;

public class CollisionSensor : MonoBehaviour {

	public delegate void OnCollideCB(string objType);

	public OnCollideCB collideCB;


	public void OnCollisionEnter(Collision collision)
	{
		if(collideCB != null)
		{
			string objType = "none";

			foreach (ContactPoint contact in collision.contacts) 
			{
				objType = contact.otherCollider.gameObject.tag;
				//Debug.DrawRay(contact.point, contact.normal, Color.white);
				
				Debug.Log("collision: "+ objType);
				collideCB.Invoke(objType);
			}
		}
	}
	public void OnTriggerEnter(Collider other) {
		if(collideCB != null)
		{
			string objType = "none";

			objType = other.gameObject.tag;
				//Debug.DrawRay(contact.point, contact.normal, Color.white);
			Debug.Log("trigger: "+ objType);

			collideCB.Invoke(objType);
		}
	}

}
