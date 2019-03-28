using UnityEngine;
using System.Collections;

/** https://gist.github.com/YoungjaeKim/6045432 */
public class DragDrop : MonoBehaviour
{  
	private bool _mouseState;
	public GameObject Target;
	public Vector3 screenSpace;
	public Vector3 offset;

	private float dist = 0.25f;

	// Use this for initialization
	void Start ()
	{
		Target = gameObject;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Debug.Log(_mouseState);
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitInfo;
			if (Target == GetClickedObject (out hitInfo)) {
				_mouseState = true;
				screenSpace = Camera.main.WorldToScreenPoint (Target.transform.position);
				offset = Target.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			_mouseState = false;
		}
		var dir =Vector3.zero;
			var trans = Target.transform;
		if (_mouseState) {
			//keep track of the mouse position
			var curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

			//convert the screen mouse position to world point and adjust with offset
			var curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;

			 dir = curPosition - trans.position;
			//update the position of the object in the world
			trans.position = curPosition;
		}

        // Hover
		var asd = trans.position;
		asd.z = _mouseState ? -dist : 0;
		trans.position = asd;

		// https://answers.unity.com/questions/46845/face-forward-direction-of-movement.html#
		 trans.rotation = Quaternion.Slerp(
	        trans.rotation,
	        Quaternion.LookRotation(dir),
	        Time.deltaTime * 3f
	    );
	}
	
	
	GameObject GetClickedObject (out RaycastHit hit)
	{
		GameObject target = null;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction * 10, out hit)) {
			target = hit.collider.gameObject;
		}

		return target;
	}
}