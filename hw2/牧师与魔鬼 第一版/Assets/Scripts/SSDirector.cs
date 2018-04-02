using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  

public interface ISceneController  
{  
	void LoadResources();
	void Pause (); 
	void Resume ();
}  
//暂停、恢复、退出
public class SSDirector : System.Object  
{  
	private static SSDirector _instance;  

	public ISceneController currentScenceController { get; set; }  
	public bool running { get; set; }  

	public static SSDirector getInstance()  
	{  
		if (_instance == null)  
		{  
			_instance = new SSDirector();  
		}  
		return _instance;  
	}  

	public int getFPS()  
	{  
		return Application.targetFrameRate;  
	}  

	public void setFPS(int fps)  
	{
		Application.targetFrameRate = fps;  
	}
}  