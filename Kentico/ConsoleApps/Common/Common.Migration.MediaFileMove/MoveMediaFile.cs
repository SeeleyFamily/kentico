namespace Common.Migration.TreeNodeMove
{
	public class MoveMediaFile
	{
		public int FileId { get; set; }
		public string TargetFilePath { get; set; } = "";
		public int TargetLibraryId { get; set; } = 0;
	}
}
