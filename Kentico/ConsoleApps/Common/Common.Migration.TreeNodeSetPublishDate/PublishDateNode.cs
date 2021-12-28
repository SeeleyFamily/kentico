using System;

namespace Common.Migration.TreeNodeSetPublishDate
{
	public class PublishDateNode
	{
		public int NodeId { get; set; }
		public DateTime? PublishFrom { get; set; }
		public DateTime? PublishTo { get; set; }
	}
}
