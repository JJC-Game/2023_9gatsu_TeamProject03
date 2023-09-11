using UnityEngine;
/// <summary>
/// タップエフェクト
/// </summary>
public class TapEffectController : MonoBehaviour
{
	[SerializeField, Header("ルートオブジェクト")]
	private RectTransform m_root;
	public RectTransform root
	{
		get { return m_root; }
	}
	[SerializeField, Header("パーティクルエフェクト")]
	private ParticleSystem m_particleSystem;
	public ParticleSystem particleSystem
	{
		get { return m_particleSystem; }
	}
	/// <summary>
	/// 座標計算用カメラ
	/// </summary>
	private Camera m_targetCamera;
	public Camera targetCamera
	{
		get { return m_targetCamera ?? (m_targetCamera = Camera.current); }
	}
	/// <summary>
	/// 表示時間
	/// </summary>
	private const int DISPLAY_FRAME = 60;
	/// <summary>
	/// 表示中フレーム数
	/// 
	/// </summary>
	private int displayFrame = DISPLAY_FRAME;
	private void Start()
	{
		particleSystem.Stop();
	}
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var touchpos = Input.mousePosition;
			Vector2 work;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				root,
				touchpos,
				targetCamera, out work);
			particleSystem.transform.localPosition = work;
			//タイムスケールの影響を受けるのでシミュレートで実行、こちらは頭から再生するフラグがオン
			particleSystem.Simulate(Time.unscaledDeltaTime, true, true);
			displayFrame = DISPLAY_FRAME;
			return;
		}
		if (--displayFrame >= 0)
		{
			//タイムスケールの影響を受けるのでシミュレート毎フレーム回す
			particleSystem.Simulate(Time.unscaledDeltaTime, true, false);
		}
	}
}