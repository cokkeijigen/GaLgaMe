using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking.Samples
{
	[RequireComponent(typeof(RectTransform))]
	public class PaintedMask : UIBehaviour
	{
		public Camera renderCamera;

		public SoftMask targetMask;

		private RenderTexture _renderTexture;

		private Vector2 maskSize => ((RectTransform)targetMask.transform).rect.size;

		protected override void Start()
		{
			base.Start();
			_renderTexture = new RenderTexture((int)maskSize.x, (int)maskSize.y, 0, RenderTextureFormat.ARGB32);
			_renderTexture.Create();
			renderCamera.targetTexture = _renderTexture;
			targetMask.renderTexture = _renderTexture;
		}
	}
}
