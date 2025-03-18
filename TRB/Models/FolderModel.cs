using System.IO;

namespace TRB.Models
{
	public class FolderModel
	{
		private string _fullPath;

		public string Name { get; set; }

		public string FullPath
		{
			get { return _fullPath; }
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					_fullPath = value;
					Name = Path.GetDirectoryName(value);
				}
			}
		}

		public FolderModel(string path)
		{
			FullPath = path;
		}
	}

}
