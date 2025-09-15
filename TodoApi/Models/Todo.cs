namespace TodoApi.Models  // プロジェクト名.Models と揃える
{
	public class Todo
	{
		public int Id { get; set; }        // 主キー
		public string? Title { get; set; } // タイトル
		public bool IsDone { get; set; }   // 完了フラグ
	}
}
