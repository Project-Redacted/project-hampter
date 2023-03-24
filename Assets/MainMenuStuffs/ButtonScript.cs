using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
	public Button yourButton;
	public string nextSceneName;
	void Start()
	{
		Button button = yourButton.GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		SceneManager.LoadScene(nextSceneName); // load the next scene
	}
}