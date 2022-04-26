using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour
{
	public Texture heartImage;
	private int livesLeft; 
	//private ScoreScript scoreScript;

	private void OnGUI()
	{

		// Vidas
		Rect r = new Rect(Screen.width - (heartImage.width * 7) ,0 ,Screen.width, Screen.height);
		GUILayout.BeginArea(r);
		GUILayout.BeginHorizontal();
		SetlivesLeft();
		if (livesLeft >= 0) 
		{
			ImagesForInteger(livesLeft, heartImage);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}

	private void SetlivesLeft ()
	{
		GameObject player = GameObject.Find("Spaceship");

		if (player != null) 
		{
            livesLeft = player.GetComponent<Spaceship>().livesLeft();
        }

	}
		
	
	private void ImagesForInteger(int total, Texture icon)
	{
		if( total > 0)
		for(int i=0; i < total; i++)
		{
			GUILayout.Label(icon);
		}
	}
}