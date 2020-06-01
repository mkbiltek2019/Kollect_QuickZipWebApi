using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using DownloadmandateApi.Models;
using Ionic.Zip;

namespace DownloadmandateApi.Controllers
{
    public class DownloadMandateController : ApiController
    {
        DownloadMandate objdmandate = new DownloadMandate();

        [HttpGet]
        [Route("api/DownloadMandate/getdropdownbank/{userId}")]
        public IEnumerable<DownloadMandateDetails> getdropdownbank(string userId)
        {
            return objdmandate.Binddropdownbank(userId);
        }

        [HttpPost]
        [Route("api/DownloadMandate/getBindGrid")]
        public IEnumerable<DownloadMandateGridDetails> getBindGrid([FromBody] Bindgrid griddata)
        {
            return objdmandate.BindGrid(griddata);
        }



        [HttpGet]
        [Route("api/DownloadMandate/getBindGridRef/{userID}/{refNo}")]
        public IEnumerable<DownloadMandateGridDetails> getBindGridRef(string userID, string refNo)
        {
            return objdmandate.BindGridRef(userID, refNo);
        }

        [HttpPost]
        [Route("api/DownloadMandate/getRejectMandate")]
        public Dictionary<string, object> getRejectMandate([FromBody] RejectMandateModel data)
        {
            return objdmandate.RejectMandate(data);
        }

        [HttpGet]
        [Route("api/DownloadMandate/getsubmitted/{userID}/{selectMandateId}")]
        public Dictionary<string, object> getsubmitted(string userID, string selectMandateId)
        {
            return objdmandate.submitted(userID, selectMandateId);
        }
        //Exceldata

        [HttpPost]
        [Route("api/DownloadMandate/getexceldownload")]
        public Dictionary<string, object> getexceldownload([FromBody] Exceldata data)
        {
            
            return objdmandate.ExcelDownload(data);
        }

        //[HttpPost]
        //[Route("api/DownloadMandate/getzip/")]
        //public IEnumerable<DownloadMandateDetails> getzip([FromBody] ZipDownload data)
        //{
        //    return objdmandate.getzip(data);
        //}



        [HttpPost]
        [Route("api/DownloadMandate/getzip/")]
        public IEnumerable<DownloadMandateDetails> getzip([FromBody] ZipDownload data)
        {
            //using (ZipFile zip = new ZipFile())  [FromBody] ZipDownload data
            //{
            //    zip.AddDirectory("C:/Users/Rishabh/Pictures/Saved Pictures/");

            //    MemoryStream output = new MemoryStream();
            //    zip.Save(output);
            //   // return File(output, "application/zip", "sample.zip");
            //}
            return objdmandate.getzip(data);

            //Create HTTP Response.
            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            //    string[] url = { "E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/CHKrDLa.jpg", "E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/LW8EKdK.jpg" };
            //    //Create the Zip File.
            //    using (ZipFile zip = new ZipFile())
            //    {
            //        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            //        zip.AddDirectoryByName("Files");
            //        //foreach (FileModel file in files)
            //        //{
            //        //    if (file.IsSelected)
            //        //    {
            //        for (int i = 0; i < url.Length; i++)
            //        {
            //            zip.AddFile(url[i], "Files");
            //        }
            //        //    }
            //        //}

            //        //Set the Name of Zip File.
            //        string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            //        using (MemoryStream memoryStream = new MemoryStream())
            //        {
            //            //Save the Zip File to MemoryStream.
            //            zip.Save(memoryStream);

            //            //Set the Response Content.
            //            System.Web.HttpContext.Current.Response.conte = new ByteArrayContent(memoryStream.ToArray());

            //            //Set the Response Content Length.
            //            response.Content.Headers.ContentLength = memoryStream.ToArray().LongLength;

            //            //Set the Content Disposition Header Value and FileName.
            //            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //            response.Content.Headers.ContentDisposition.FileName = zipName;

            //            //Set the File Content Type.
            //            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
            //            return response;
            //        }
            //    }
        }


        //    //return objdmandate.getzip();
        //}




        [HttpGet]
        [Route("api/DownloadMandate/getzipIDFC")]
        public HttpResponseMessage getzipIDFC()
        {
            //using (ZipFile zip = new ZipFile())  [FromBody] ZipDownload data
            //{
            //    zip.AddDirectory("C:/Users/Rishabh/Pictures/Saved Pictures/");

            //    MemoryStream output = new MemoryStream();
            //    zip.Save(output);
            //   // return File(output, "application/zip", "sample.zip");
            //}

            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string[] url = { "E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/CHKrDLa.jpg", "E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/LW8EKdK.jpg" };
            //Create the Zip File.
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("Files");
                //foreach (FileModel file in files)
                //{
                //    if (file.IsSelected)
                //    {
                for (int i = 0; i < url.Length; i++)
                {
                    zip.AddFile(url[i], "Files");
                }
                //    }
                //}

                //Set the Name of Zip File.
                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    //Save the Zip File to MemoryStream.
                    zip.Save(memoryStream);

                    //Set the Response Content.
                    response.Content = new ByteArrayContent(memoryStream.ToArray());

                    //Set the Response Content Length.
                    response.Content.Headers.ContentLength = memoryStream.ToArray().LongLength;

                    //Set the Content Disposition Header Value and FileName.
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = zipName;

                    //Set the File Content Type.
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                    return response;
                }
            }
        }



        //[HttpPost]
        //[Route("api/DownloadMandate/getzip/")]
        //public HttpResponseMessage getzip([FromBody] ZipDownload data)
        //{

        //  //  string[] docs= { "E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/CHKrDLa.jpg" };
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        //this code takes an array of documents' paths and Zip them
        //        zip.AddFiles(data.docs, false, "");
        //        return ZipContentResult(zip);
        //    }
        //}

        //protected HttpResponseMessage ZipContentResult(ZipFile zipFile)
        //{
        //    var pushStreamContent = new PushStreamContent((stream, content, context) =>
        //    {
        //        zipFile.Save(stream);
        //        stream.Close();
        //    }, "application/zip");

        //    return new HttpResponseMessage(HttpStatusCode.OK) { Content = pushStreamContent };
        //}


        //public IActionResult GetZip([FromBody] List<DocumentAndSourceDto> documents)
        //{
        //    List<Document> listOfDocuments = new List<Document>();

        //    foreach (DocumentAndSourceDto doc in documents)
        //        listOfDocuments.Add(_documentService.GetDocumentWithServerPath(doc.Id));

        //    using (var ms = new MemoryStream())
        //    {
        //        using (var zipArchive = new ZipArchive(ms, ZipArchiveMode.Create, true))
        //        {
        //            foreach (var attachment in listOfDocuments)
        //            {
        //                var entry = zipArchive.CreateEntry(attachment.FileName);

        //                using (var fileStream = new FileStream(attachment.FilePath, FileMode.Open))
        //                using (var entryStream = entry.Open())
        //                {
        //                    fileStream.CopyTo(entryStream);
        //                }
        //            }

        //        }
        //        ms.Position = 0;
        //        return File(ms.ToArray(), "application/zip");
        //    }

        //    throw new ErrorException("Can't zip files");
        //}





        //    [HttpGet]
        //    [Route("api/DownloadMandate/getzip/")]
        //    public HttpResponseMessage getzip()
        //    {
        //        //using (ZipFile zip = new ZipFile())
        //        //{
        //        //    zip.AddDirectory("C:/Users/Rishabh/Pictures/Saved Pictures/");

        //        //    MemoryStream output = new MemoryStream();
        //        //    zip.Save(output);
        //        //   // return File(output, "application/zip", "sample.zip");
        //        //}

        //        //Create HTTP Response.
        //        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

        //        //Create the Zip File.
        //        using (ZipFile zip = new ZipFile())
        //        {
        //            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
        //            zip.AddDirectoryByName("Files");
        //            //foreach (FileModel file in files)
        //            //{
        //            //    if (file.IsSelected)
        //            //    {
        //            zip.AddFile("E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/CHKrDLa.jpg", "Files");
        //            //    }
        //            //}

        //            //Set the Name of Zip File.
        //            string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                //Save the Zip File to MemoryStream.
        //                zip.Save(memoryStream);

        //                //Set the Response Content.
        //                response.Content = new ByteArrayContent(memoryStream.ToArray());

        //                //Set the Response Content Length.
        //                response.Content.Headers.ContentLength = memoryStream.ToArray().LongLength;

        //                //Set the Content Disposition Header Value and FileName.
        //                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //                response.Content.Headers.ContentDisposition.FileName = zipName;

        //                //Set the File Content Type.
        //                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
        //                return response;
        //            }
        //        }
        //    }


        //    //return objdmandate.getzip();
        //}
    }
}

