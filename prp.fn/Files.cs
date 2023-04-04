using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public static class Paths
{
    public static string GetPathApplication()
    {
        string content = "";

        try
        {

        }
        catch (Exception)
        {

            content = "";
        }

        return content;
    }
    public static string GetServerPathFolder(this string folderPath)
    {
        string fullPaht = "";

        try
        {
            fullPaht = HttpContext.Current.Server.MapPath(folderPath);

        }
        catch (Exception)
        {

            fullPaht = "";
        }

        return fullPaht;
    }

}

public static class Files
{

    public static string ReadFile(this string Path)
    {
        string Result = "";
        try
        {

            System.IO.StreamReader MyFile = new System.IO.StreamReader(Path);
            Result = MyFile.ReadToEnd();
            MyFile.Close();
            MyFile.Dispose();
        }
        catch (Exception)
        {
            Result = "";
        }
        return Result;
    }
    public static void WriteFile(this string Path, string Content)
    {

        try
        {
            StreamWriter str = new StreamWriter(Path);
            str.BaseStream.Seek(0, SeekOrigin.End);
            //string Html = ("&nbsp;", "");
            str.Write(Content);
            str.Flush();
            str.Close();
        }
        catch (Exception)
        {
            throw;
        }
    }


    public static string SaveMultipleFiles(IEnumerable<HttpPostedFileBase> SaveFiles, string Path)
    {
        string Images = string.Empty;
        int imageNo = 1;
        foreach (var File in SaveFiles)
        {
            string ImageName = SaveFile(File, Path, imageNo);
            Images += ImageName + ",";
            imageNo = imageNo + 1;
        }
        return Images;
    }
    public static string SaveFile(HttpPostedFileBase ImageFile, string Path, int imageNo = 0, string FileName = "")
    {
        FileName = FileName.Replace("|", "");
        FileName = string.IsNullOrEmpty(FileName) ? Convert.ToString(DateTime.Now.Ticks) + "_" + imageNo.TooString() + ImageFile.FileName.Substring(ImageFile.FileName.IndexOf(".")) : FileName;

        string ImageServerPath = HttpContext.Current.Server.MapPath(Path + "/" + FileName);
        ImageFile.SaveAs(ImageServerPath);
        return FileName;
    }

}