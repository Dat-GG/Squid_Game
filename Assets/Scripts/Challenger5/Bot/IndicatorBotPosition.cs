using Challenger5.UI.CountTotalAlive;
using Challenger5.UI.TimerCountdown;
using UnityEngine;

namespace Challenger5.Bot
{
    public class IndicatorBotPosition : MonoBehaviour
    {
	    public Texture2D icon; //The icon. Preferably an arrow pointing upwards.
	    public float iconSize = 50f;
	    private BotController5 _botController5;
	    private TimerCountdown5 _timerCountdown5;
	    private CountTotalAlive _countTotalAlive;
		[HideInInspector]
		public GUIStyle gooey; //GUIStyle to make the box around the icon invisible. Public so that everything has the default stats.
		private Vector2 _indRange;
		private readonly float _scaleRes = Screen.width / 500; //The width of the screen divided by 500. Will make the GUI automatically
		//scale with varying resolutions.
		private Camera _cam;
		private bool _visible = true; //Whether or not the object is visible in the camera.

		private void Start ()
		{
			_visible = GetComponent<MeshRenderer> ().isVisible;
			_botController5 = GetComponent<BotController5>();
			_timerCountdown5 = FindObjectOfType<TimerCountdown5>();
			_countTotalAlive = FindObjectOfType<CountTotalAlive>();
			_cam = Camera.main; //Don't use Camera.main in a looping method, its very slow, as Camera.main actually
			//does a GameObject.Find for an object tagged with MainCamera.

			_indRange.x = Screen.width - (Screen.width / 6);
			_indRange.y = Screen.height - (Screen.height / 7);
			_indRange /= 2f;

			gooey.normal.textColor = new Vector4 (0, 0, 0, 0); //Makes the box around the icon invisible.
		}

		private void OnGUI ()
		{
			if (_visible || _botController5.state == BotController5.States.Die ||
			    (!(_timerCountdown5.gameDuration <= 30) && _countTotalAlive.alive > 5)) return;
			var position = transform.position;
			var dir = position - _cam.transform.position;
			dir = Vector3.Normalize (dir);
			dir.y *= -1f;

			var indPos = new Vector2 (_indRange.x * dir.x, _indRange.y * dir.y);
			indPos = new Vector2 ((Screen.width / 2) + indPos.x, (Screen.height / 2) + indPos.y);

			var pdir = position - _cam.ScreenToWorldPoint(new Vector3(indPos.x, indPos.y, position.z));
			pdir = Vector3.Normalize(pdir);

			var angle = Mathf.Atan2(pdir.x, pdir.y) * Mathf.Rad2Deg;

			GUIUtility.RotateAroundPivot(angle, indPos); //Rotates the GUI. Only rotates GUI drawn after the rotate is called, not before.
			GUI.Box (new Rect (indPos.x, indPos.y, _scaleRes * iconSize, _scaleRes * iconSize), icon,gooey);
			GUIUtility.RotateAroundPivot(0, indPos); //Rotates GUI back to the default so that GUI drawn after is not rotated.
		}

		private void OnBecameInvisible()
		{
			_visible = false;
		}
		//Turns off the indicator if object is onscreen.
		private void OnBecameVisible()
		{
			_visible = true;
		}
	}
}

