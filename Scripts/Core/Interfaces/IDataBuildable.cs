using UnityEngine;

namespace VGDevs
{
	public interface IDataBuildable<TData>
	{
		void Build(TData data);
	}
}