using UnityEngine;
using UnityEngine.UI;

// https://www.youtube.com/watch?v=GURPmGoAOoM
namespace UI.MainMenu
{
	public class SwipeFlagSettingUiMainMenu : MonoBehaviour
	{
		[SerializeField] private GameObject scrollBar;
		private float _scrollPos = 0f;
		private float[] _pos;
		private Scrollbar scrollbar;
		private string _flagSelected;
		protected internal string FlagSelected => _flagSelected;

		private void Awake()
		{
			scrollbar = scrollBar.GetComponent<Scrollbar>();
		}

		private void Update()
		{
			SnapFlag();
		}

		private void SnapFlag()
		{
			_pos = new float[transform.childCount];
			var distance = 1f / (_pos.Length - 1f);
			for (var i = 0; i < _pos.Length; i++)
			{
				_pos[i] = distance * i;
			}

			if (Input.GetMouseButton(0))
			{
				_scrollPos = scrollbar.value;
			}
			else
			{
				foreach (var t in _pos)
				{
					if (_scrollPos < t + distance * 0.5f && _scrollPos > t - distance * 0.5f)
					{
						scrollbar.value = Mathf.Lerp(scrollbar.value, t, 0.1f);
					}
				}
			}

			for (var i = 0; i < _pos.Length; i++)
			{
				if (!(_scrollPos < _pos[i] + distance * 0.5f) || !(_scrollPos > _pos[i] - distance * 0.5f)) continue;
				transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale,
					new Vector2(1f, 1f), 0.1f);
				_flagSelected = transform.GetChild(i).GetComponent<ElementFlagSettingUiMainMenu>().Country;
				
				for (var a = 0; a < _pos.Length; a++)
				{
					if (a != i)
					{
						transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale,
							new Vector2(0.8f, 0.8f), 0.1f);
					}
				}
			}
		}
	}
}