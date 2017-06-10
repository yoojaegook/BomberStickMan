using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSound : MonoBehaviour {

	// === 외부 파라미터 ======================================
	public static AppSound instance = null;

	// 배경음
	[System.NonSerialized] public audiomanager fm;
	
	
	
	[System.NonSerialized] public AudioSource BGM_TITLE;
	[System.NonSerialized] public AudioSource BGM_STAGE;


	// 효과음
	[System.NonSerialized] public AudioSource SE_EXPLOSION_1;
	[System.NonSerialized] public AudioSource SE_EXPLOSION_2;
	[System.NonSerialized] public AudioSource SE_EXPLOSION_3;
	[System.NonSerialized] public AudioSource SE_EXPLOSION_4;
	[System.NonSerialized] public AudioSource SE_EXPLOSION_5;
	[System.NonSerialized] public AudioSource SE_EXPLOSION_6;
	

	// === 내부 파라미터 ======================================
	string sceneName = "non";
    public float SoundBGMVolume = 0.7f;
    public float SoundSEVolume = 1.0f;
    float PreSoundBGMVolume = 0.0f;
    float PreSoundSEVolume = 0.0f;

	// === 코드 =============================================
	void Start () {
		// 사운드
		fm = GameObject.Find("audioManager").GetComponent<audiomanager>();

		// 배경음
		fm.CreateGroup("BGM");
		fm.SoundFolder = "Sounds/";
		BGM_TITLE 				= fm.LoadResourcesSound("BGM","explosion bgm");
		BGM_STAGE 				= fm.LoadResourcesSound("BGM","explosion bgm2");
	/*	BGM_STAGE	 	= fm.LoadResourcesSound("BGM","battle_1");
		BGM_GAMEOVER 				= fm.LoadResourcesSound("BGM","gameover_1");
	*/	

		// 효과음
		fm.CreateGroup("SE");
		fm.SoundFolder = "Sounds/";

		SE_EXPLOSION_1 = fm.LoadResourcesSound("SE", "explosion sound (8)");
        SE_EXPLOSION_2 = fm.LoadResourcesSound("SE", "explosion sound (1)");
        SE_EXPLOSION_3 = fm.LoadResourcesSound("SE", "explosion sound (5)");
        SE_EXPLOSION_4 = fm.LoadResourcesSound("SE", "explosion sound (7)");
        SE_EXPLOSION_5 = fm.LoadResourcesSound("SE", "explosion sound (5)");
        SE_EXPLOSION_6 = fm.LoadResourcesSound("SE", "explosion sound (4)");
		

		instance = this;
	}
	public void Mute()
	{
		PreSoundBGMVolume=SoundBGMVolume;
		PreSoundSEVolume=SoundSEVolume;
	}
	public void Unmute()
	{
		SoundBGMVolume = PreSoundBGMVolume;
		SoundSEVolume = PreSoundSEVolume;
	}
	void Update() {
	
        // 볼륨 설정
        fm.SetVolume("BGM", SoundBGMVolume);
        fm.SetVolume("SE", SoundSEVolume);
		// 씬이 바뀌었는지 검사
		if (sceneName != SceneManager.GetActiveScene().name) {
			sceneName = SceneManager.GetActiveScene().name;

			

			// 배경음 재생
			
			if (sceneName == "Title") {
				//fm.Stop ("BGM");
				fm.FadeOutVolumeGroup("BGM",BGM_TITLE,0.0f,1.0f,false);
				fm.FadeInVolume(BGM_TITLE,SoundBGMVolume,1.0f,true);
				BGM_TITLE.loop = true;
				BGM_TITLE.Play();
			}else
			if (sceneName == "Shoot") {
				//fm.Stop ("BGM");
				fm.FadeOutVolumeGroup("BGM",BGM_STAGE,0.0f,1.0f,false);
				fm.FadeInVolume(BGM_STAGE,SoundBGMVolume,1.0f,true);
				BGM_STAGE.loop = true;
				BGM_STAGE.Play();
			}
		}
	}
}
