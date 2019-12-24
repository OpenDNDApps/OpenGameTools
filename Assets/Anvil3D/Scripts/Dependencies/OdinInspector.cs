using System;

namespace Sirenix.OdinInspector
{
	internal class PropertySpaceAttribute : Attribute
	{
		public float SpaceBefore;

		public PropertySpaceAttribute() { }
		public PropertySpaceAttribute(object _) { }

		public PropertySpaceAttribute(float SpaceBefore)
		{
			this.SpaceBefore = SpaceBefore;
		}
	}

	internal class EnableIfAttribute : Attribute
	{
		public EnableIfAttribute() { }
		public EnableIfAttribute(object _) { }
	}

	internal class PropertyOrderAttribute : Attribute
	{
		public PropertyOrderAttribute(object _) { }
	}

	internal class ToggleGroupAttribute : Attribute
	{
		public object Order;

		public ToggleGroupAttribute(object _) { }

		public ToggleGroupAttribute(object _, object _1) { }

		public ToggleGroupAttribute(object _, object _1, object _2) { }
	}

	internal class GUIColorAttribute : Attribute
	{
		public GUIColorAttribute(object _, object _1, object _2, object _3) { }
	}

	internal class ButtonAttribute : Attribute
	{
		public ButtonAttribute() { }
		public ButtonAttribute(object _) { }
	}

	internal class TitleAttribute : Attribute
	{
		public TitleAttribute() { }
		public TitleAttribute(object _) { }
	}

	internal class BoxGroupAttribute : Attribute
	{
		public BoxGroupAttribute() { }
	}

	internal class PreviewFieldAttribute : Attribute
	{
		public PreviewFieldAttribute() { }
		public PreviewFieldAttribute(object _, object _1) { }
		public PreviewFieldAttribute(object _, object _1, object _2) { }
	}

	internal enum ObjectFieldAlignment { Center }

	internal class ShowInInspectorAttribute : Attribute { }
}
