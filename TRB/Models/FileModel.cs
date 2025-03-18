using System.IO;

namespace TRB.Models
{
	public class FileModel
	{
		private string _fullPath;

		public string Extension { get; set; }

		public string Name { get; set; }

		public string FullPath
		{
			get => _fullPath;
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					_fullPath = value;
					Name = Path.GetFileNameWithoutExtension(value);
					Extension = Path.GetExtension(value);
				}
			}
		}

		public FileModel(string path)
		{
			FullPath = path;
		}
	}

}
