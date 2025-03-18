using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TRB.Enums;
using TRB.Models;
using TRB.Resources;

namespace TRB.Utils.File
{
	public static class File
	{
		private static readonly Dictionary<DocumentExtensions, string> ExtensionMappings = new Dictionary<DocumentExtensions, string>()
		{
			{ DocumentExtensions.Pdf, ".pdf" },
			{ DocumentExtensions.Doc, ".doc" },
			{ DocumentExtensions.Docx, ".docx" },
			{ DocumentExtensions.Xls, ".xls" },
			{ DocumentExtensions.Xlsx, ".xlsx" },
			{ DocumentExtensions.Ppt, ".ppt" },
			{ DocumentExtensions.Pptx, ".pptx" },
			{ DocumentExtensions.Txt, ".txt" },
			{ DocumentExtensions.Csv, ".csv" },
			{ DocumentExtensions.Xml, ".xml" }
		};

		public static async Task<List<T>> RetrieveFilesFromPathAsync<T>(
			string path,
			IProgress<string> status = null,
			IProgress<int> progress = null,
			params DocumentExtensions[] values) where T : FileModel, new()
		{
			List<T> retrievedFiles = new List<T>();

			try
			{
				if (values?.Length < 1)
				{
					throw new ArgumentException(TRBLocalization.Get(MessageKey.NoValidExtensions), nameof(values));
				}

				ConcurrentBag<T> concurrentFiles = new ConcurrentBag<T>();
				status?.Report("Retrieving files...");
				progress?.Report(0);

				List<string> fileExtensions = values.Where(v => ExtensionMappings.ContainsKey(v)).Select(v => ExtensionMappings[v]).ToList();
				if (fileExtensions?.Any() == false)
				{
					throw new ArgumentException(TRBLocalization.Get(MessageKey.NoValidExtensions), nameof(fileExtensions));
				}

				List<string> files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).ToList();

				int filesCount = files.Count;
				int counter = 0;

				await Task.Run(() =>
				{
					Parallel.ForEach(files, file =>
					{
						int localCounter = Interlocked.Increment(ref counter);
						int currentProgress = (int)((double)localCounter / filesCount * 100);
						status?.Report($"Retrieving {localCounter} of {filesCount} files.");
						progress?.Report(currentProgress);

						T item = new T { FullPath = file };

						if (!fileExtensions.Any(ext => string.Equals(ext, item.Extension, StringComparison.OrdinalIgnoreCase)))
						{
							return;
						}

						concurrentFiles.Add(item);

					});
				});

				retrievedFiles.AddRange(concurrentFiles.OrderBy(file => file.Name));
				status?.Report("Files Loaded");
				await Task.Delay(100);
			}
			catch (Exception ex)
			{
				Dialog.ErrorMessage(ex);
			}

			return retrievedFiles;
		}

	}
}
