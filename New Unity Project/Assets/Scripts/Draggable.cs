using UnityEngine;

public class Draggable : MonoBehaviour
{
    public SteamVR_TrackedObject rightcontroller;

    public Transform minBound;

	public bool fixX;
	public bool fixY;
	public Transform thumb;	
	bool dragging;

	void FixedUpdate()
	{
        SteamVR_Controller.Device dev = SteamVR_Controller.Input((int)rightcontroller.index);

        if (dev.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			dragging = false;
            //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = new Ray(rightcontroller.transform.position, rightcontroller.transform.forward);
            RaycastHit hit;
			if (GetComponent<Collider>().Raycast(ray, out hit, 100)) {
				dragging = true;
			}
		}
		if (dev.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) dragging = false;
		if (dragging && dev.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
            Ray ray = new Ray(rightcontroller.transform.position, rightcontroller.transform.forward);
            RaycastHit hit;
            if (GetComponent<Collider>().Raycast(ray, out hit, 100))
            {
                var point = hit.point;
                //point = GetComponent<Collider>().ClosestPointOnBounds(point);
                SetThumbPosition(point);
                SendMessage("OnDrag", Vector3.one - (thumb.localPosition - minBound.localPosition) / GetComponent<BoxCollider>().size.x);
            }
		}
	}

	void SetDragPoint(Vector3 point)
	{
		point = (Vector3.one - point) * GetComponent<Collider>().bounds.size.x + GetComponent<Collider>().bounds.min;
		SetThumbPosition(point);
	}

	void SetThumbPosition(Vector3 point)
	{
        Vector3 temp = thumb.localPosition;
        thumb.position = point;

		thumb.localPosition = new Vector3(fixX ? temp.x : thumb.localPosition.x, fixY ? temp.y : thumb.localPosition.y, thumb.position.z - 1);
	}
}
