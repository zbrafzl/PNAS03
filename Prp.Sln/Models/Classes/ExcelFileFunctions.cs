
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Prp.Sln
{
    public static class ExcelFileFunctions
    {
        public static Message FileDownload(this string serverFilePath)
        {
            Message m = new Message();
            string destinationPath = "";
            try
            {
                FileInfo file = new FileInfo(serverFilePath);
                if (file.Exists)
                {
                    byte[] fileContent = File.ReadAllBytes(serverFilePath);
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    HttpContext.Current.Response.AddHeader("Content-Length", fileContent.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.BinaryWrite(fileContent);
                    HttpContext.Current.Response.End();

                    m.msg = "File downloaded at :" + destinationPath;
                    m.status = true;
                }
                else
                {
                    m.msg = "This file does not exist" + "File downloaded at :" + destinationPath;
                    m.status = false;
                }
            }
            catch (Exception ex)
            {
                m.status = false;
                m.msg = "Download Path is : " + destinationPath + " <br/> Error : " + ex.Message;
            }
            return m;
        }

        public static Message ExcelFileWrite(this string filePath, System.Data.DataTable dt, string sheetName = "Sheet1", string Cell = "A1")
        {
            Message m = new Message();

            try
            {
                Message mfg = GenrateExcelFile(filePath, sheetName);
                if (mfg.status)
                {
                    FileInfo file = new FileInfo(filePath);
                    using (ExcelPackage pck = new ExcelPackage(file))
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets[sheetName];
                        ws.Cells[Cell].LoadFromDataTable(dt, true);
                        pck.Save();
                    }
                    m.status = true;
                    m.msg += mfg.msg;
                    m.msg += "Data exported into excel sucessfylly.";
                }
                else m.msg += mfg.msg;

            }
            catch (Exception ex)
            {
                m.msg = "Error in exported : " + ex.Message;
                m.status = false;
            }

            return m;
        }

        public static Message GenrateExcelFile(this string filePath, string SheetName = "Sheet1")
        {
            Message m = new Message();
            try
            {
                // delete old file
                //filePath.FileDelete();

                FileInfo file = new FileInfo(filePath);
                string folderPath = filePath.Replace(file.Name, "");

                // create folder if not exist
                folderPath.CreateDirectory();
                // delete  file in directory
                try
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                catch (Exception)
                {
                }


                using (var package = new ExcelPackage(file))
                {
                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(SheetName);
                    package.Save();
                }
                m.status = true;
                m.msg = "File genrated sucessfully.";
            }
            catch (Exception ex)
            {
                m.msg = "Error in file genration : " + ex.Message;
                m.status = false;
            }

            return m;
        }

        public static string GenerateFilePath(this string fileName, User loggedInUser)
        {

            string filePath = "";
            try
            {
                filePath = "/ExcelFiles/";
                filePath = filePath + fileName;


                filePath = Path.Combine(HttpContext.Current.Server.MapPath(filePath));

                FileInfo file = new FileInfo(filePath);
                string folderPath = filePath.Replace(file.Name, "");
            }
            catch (Exception)
            {
                filePath = "";
            }
            return filePath;
        }

        public static string GenerateFilePath(this string fileName, string folderName = "/ExcelFiles/Gazat/")
        {
            string filePath = "";
            try
            {
                filePath = folderName;
                filePath = filePath + fileName;

                filePath = Path.Combine(HttpContext.Current.Server.MapPath(filePath));

                FileInfo file = new FileInfo(filePath);
                string folderPath = filePath.Replace(file.Name, "");
            }
            catch (Exception)
            {
                filePath = "";
            }
            return filePath;
        }

        public static void CreateDirectory(this string folderpath)
        {
            //string path = HttpContext.Current.Request.ApplicationPath + folderpath;
            if (!Directory.Exists(folderpath))
                Directory.CreateDirectory(folderpath);
        }
    }
}