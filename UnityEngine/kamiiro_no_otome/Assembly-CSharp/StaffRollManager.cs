using UnityEngine;
using UnityEngine.Playables;
using Utage;

public class StaffRollManager : MonoBehaviour
{
	public PlayableDirector playableDirector;

	public GameObject staffRollCanvasGo;

	public AdvEngine advEngine;

	public void StartStaffRoll(AdvCommandSendMessageByName command)
	{
		playableDirector.Play();
		Debug.Log("スタッフロールを開始");
	}

	public void EndStaffRoll()
	{
		playableDirector.Stop();
		staffRollCanvasGo.SetActive(value: false);
		PlayerNonSaveDataManager.isEndGame = true;
		advEngine.ResumeScenario();
		Debug.Log("スタッフロールを終了");
	}

	public void SetTimelinePlaySpeed(float speed)
	{
		playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(speed);
	}
}
