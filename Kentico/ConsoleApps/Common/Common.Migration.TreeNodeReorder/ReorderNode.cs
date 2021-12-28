namespace Common.Migration.TreeNodeReorder
{
	public class ReorderNode
	{
		public int NodeId { get; set; }
		public int NodeOrder { get; set; } = 1;
		public int TargetNodeId { get; set; } = 0;	
		public ReorderType ReorderType { get; set; } = ReorderType.Exact;
	}
}
