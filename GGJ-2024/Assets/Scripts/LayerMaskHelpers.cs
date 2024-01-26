using UnityEngine;

namespace WinterwireGames.PhysicsHelpers
{
	public static class LayerMaskHelpers
	{
		public static bool HasLayer(this LayerMask mask, int layer)
		{
			return (1 << layer & mask) > 0;
		}
	}
}
