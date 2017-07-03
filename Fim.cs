using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour {

	public void End()
	{
		if (Input.GetKeyDown(KeyCode.Return)) 
		{
			SceneManager.LoadScene("Jogo");
		}
	}
}
