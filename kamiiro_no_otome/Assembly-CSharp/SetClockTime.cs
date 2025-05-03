using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetClockTime : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public GameObject longHandGO;

	public GameObject weekDayImageGO;

	public Text craftRemainText;

	public bool isInitialized;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		float z = longHandGO.transform.rotation.z;
		float num = 360f - (float)PlayerDataManager.currentTimeZone * 90f;
		float num2 = z - num;
		Debug.Log("タイムゾーン：" + PlayerDataManager.currentTimeZone + "／変更後の時計の角度：" + num + "／差分の角度：" + num2);
		if (!isInitialized)
		{
			longHandGO.GetComponent<RectTransform>().DORotate(new Vector3(0f, 0f, num), 0f);
			isInitialized = true;
		}
		else if (PlayerNonSaveDataManager.isClockChangeEnable)
		{
			RectTransform component = longHandGO.GetComponent<RectTransform>();
			if (PlayerDataManager.currentTimeZone == 0)
			{
				switch (PlayerNonSaveDataManager.oldTimeZone)
				{
				case 1:
				{
					Sequence s2 = DOTween.Sequence();
					s2.Append(component.DORotate(new Vector3(0f, 0f, 180f), 0.07f));
					s2.Append(component.DORotate(new Vector3(0f, 0f, 90f), 0.07f));
					s2.Append(component.DORotate(new Vector3(0f, 0f, 0f), 0.07f));
					break;
				}
				case 2:
				{
					Sequence s = DOTween.Sequence();
					s.Append(component.DORotate(new Vector3(0f, 0f, 90f), 0.1f));
					s.Append(component.DORotate(new Vector3(0f, 0f, 0f), 0.1f));
					break;
				}
				case 3:
					component.DORotate(new Vector3(0f, 0f, 0f), 0.1f);
					break;
				}
			}
			else
			{
				int num3 = PlayerDataManager.currentTimeZone - PlayerNonSaveDataManager.oldTimeZone;
				Debug.Log("次の時間：" + PlayerDataManager.currentTimeZone + "／前の時間：" + PlayerNonSaveDataManager.oldTimeZone + "／差分の時間：" + num3);
				switch (num3)
				{
				case 1:
					component.DORotate(new Vector3(0f, 0f, -90f), 0.2f).SetRelative(isRelative: true);
					break;
				case 2:
				{
					Sequence s4 = DOTween.Sequence();
					s4.Append(component.DORotate(new Vector3(0f, 0f, -90f), 0.1f)).SetRelative(isRelative: true);
					s4.Append(component.DORotate(new Vector3(0f, 0f, -90f), 0.1f)).SetRelative(isRelative: true);
					break;
				}
				case 3:
				{
					Sequence s3 = DOTween.Sequence();
					s3.Append(component.DORotate(new Vector3(0f, 0f, -90f), 0.07f)).SetRelative(isRelative: true);
					s3.Append(component.DORotate(new Vector3(0f, 0f, -90f), 0.07f)).SetRelative(isRelative: true);
					s3.Append(component.DORotate(new Vector3(0f, 0f, -90f), 0.07f)).SetRelative(isRelative: true);
					break;
				}
				}
			}
		}
		PlayerNonSaveDataManager.oldTimeZone = PlayerDataManager.currentTimeZone;
		int num4 = PlayerDataManager.currentTotalDay / 7;
		int index = PlayerDataManager.currentTotalDay - 7 * num4;
		weekDayImageGO.GetComponent<Image>().sprite = GameDataManager.instance.itemCategoryDataBase.weekDayIconList[index];
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
