using UnityEngine;
/// <summary>
/// タップエフェクト
/// </summary>
public class TapEffectController : MonoBehaviour
{
	[Header("パーティクルエフェクト")]
	private new ParticleSystem particleSystem;

	public int displayFrame = 60;
	private void Start()
	{
		particleSystem = this.GetComponent<ParticleSystem>();
	}
	void Update()
	{
		if (--displayFrame >= 0)
		{
			//タイムスケールの影響を受けるのでシミュレート毎フレーム回す
			particleSystem.Simulate(Time.unscaledDeltaTime, true, false);
        }
        else
        {
			Destroy(this.gameObject);
        }
	}
}