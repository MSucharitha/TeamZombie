using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(SteamVR_LaserPointer))]
public class VRUIInput : MonoBehaviour
{
	private SteamVR_LaserPointer laserPointer;
	private SteamVR_TrackedController trackedController;
//	private SteamVR_TrackedObject trackedObject;
//	private SteamVR_Controller.Device device;

	private Button button_hovered;

	private void OnEnable()
	{
		laserPointer = GetComponent<SteamVR_LaserPointer>();
		laserPointer.PointerIn -= HandlePointerIn;
		laserPointer.PointerIn += HandlePointerIn;
		laserPointer.PointerOut -= HandlePointerOut;
		laserPointer.PointerOut += HandlePointerOut;

		trackedController = GetComponent<SteamVR_TrackedController>();
		if (trackedController == null)
		{
			trackedController = GetComponentInParent<SteamVR_TrackedController>();
		}
		trackedController.TriggerClicked -= HandleTriggerClicked;
		trackedController.TriggerClicked += HandleTriggerClicked;

//		trackedObject = GetComponent<SteamVR_TrackedObject> ();
//		if (trackedObject == null) {
//			trackedObject = GetComponentInParent<SteamVR_TrackedObject> (); 
//		}
	}

//	private void FixedUpdate() {
//		device = SteamVR_Controller.Input ((int)trackedObject.index);
//
//		bool triggerPressed = device.GetTouchUp (SteamVR_Controller.ButtonMask.Grip);
//		Debug.Log ("Trigger Pressed? " + triggerPressed.ToString ());
//
//		if (triggerPressed) {
//			Debug.Log ("Pressing Trigger on Menu");
//			clickHoveredButton ();
//		}
//	}

	private void clickHoveredButton() {
		Debug.Log (button_hovered);
		if (button_hovered != null) {
			button_hovered.onClick.Invoke ();
		}
	}

	private void HandleTriggerClicked(object sender, ClickedEventArgs e)
	{
		Debug.Log ("Trigger Pressed");
		clickHoveredButton ();

		if (EventSystem.current.currentSelectedGameObject != null)
		{
			ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
		}
	}

	private void HandlePointerIn(object sender, PointerEventArgs e)
	{
		
		var button = e.target.GetComponent<Button>();
		button_hovered = button;

		if (button_hovered != null)
		{
			button.Select();
			Debug.Log("HandlePointerIn", e.target.gameObject);
		}
	}

	private void HandlePointerOut(object sender, PointerEventArgs e)
	{
		
		var button = e.target.GetComponent<Button>();
		button_hovered = null;

//		if (button != null)
//		{
//			EventSystem.current.SetSelectedGameObject(null);
//			Debug.Log("HandlePointerOut", e.target.gameObject);
//		}
	}
}