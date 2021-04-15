using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Kodszerkeszto.Models
{
    public class FilePersistence
    {

		public FilePersistence()
		{
		}
		public async Task<String> LoadAsync(String url)
		{
			try
			{
				using (StreamReader reader = new StreamReader(url)) // fájl megnyitása
				{
					String content = "";
					String line;
					while((line = await reader.ReadLineAsync()) != null)
                    {
						content += line+"\n";
                    }


					return content;
				}
			}
			catch
			{
				throw new FileOperationException();
			}
		}
		public async Task SaveAsync(String content, String url = "base")
		{
			String path = url;
			Debug.WriteLine("írom már");
			try
			{
				using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
				{
					await writer.WriteAsync(content);
				}
			}
			catch
			{
				throw new FileOperationException();
			}
		}
	}
}
