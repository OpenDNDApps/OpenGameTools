using UnityEngine;

namespace Anvil3D
{
	public interface IDataBuildable<TData>
	{
		void Build(TData data);
	}
}