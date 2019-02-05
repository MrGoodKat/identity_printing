using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
namespace kimlik.Models
{
    public class Utils
    {
        public static string slugify(string title)
        {
            string result = "";

            result = title.Replace("Ç", "c");
            result = result.Replace("Ğ", "g");
            result = result.Replace("İ", "i");
            result = result.Replace("I", "i");
            result = result.Replace("Ö", "o");
            result = result.Replace("Ş", "s");
            result = result.Replace("Ü", "u");

            result = result.ToLower().Replace(" ", "-");
            result = result.Replace("ç", "c");
            result = result.Replace("ğ", "g");
            result = result.Replace("ı", "i");
            result = result.Replace("ö", "o");
            result = result.Replace("ş", "s");
            result = result.Replace("ü", "u");
            result = result.Replace("â", "a");
            result = result.Replace("î", "i");

            //return Regex.Replace(Temp, @"\W*", "-");
            result = Regex.Replace(result.ToLower(), "[^a-z0-9]", "-");
            result = clearLines(result);
            if (result.LastIndexOf("-") == (result.Length - 1))
            {
                result = result.Substring(0, result.Length - 1);
            }
            if (result.IndexOf("-") == 0)
            {
                result = result.Substring(1, result.Length - 1);
            }

            return result;

        }
        public static string clearLines(string txt)
        {
            txt = txt.Replace("--", "-");
            if (txt.IndexOf("--") >= 0)
            {
                txt = clearLines(txt);
            }
            return txt;
        }

        /// <summary>
        /// Image upload and resize
        /// </summary>
        /// <param name="file">HttpPostedFileBase file</param>
        /// <param name="MaxWidthSize">int MaximumWidth</param>
        /// <param name="MinWidthSize">int MinumumWidth</param>
        /// <returns>string Image Name</returns>
        public static string ImageUploadAndResize(HttpPostedFileBase file, int MaxWidthSize, int MinWidthSize)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string ImgName = ""; string extention = "";
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/Images/"), Path.GetFileName(file.FileName));

                    if (path != null)
                    {
                        extention = Path.GetExtension(path);
                        file.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
                        ImgName = Guid.NewGuid().ToString() + extention;

                        Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
                        using (Bitmap orjImg = bmp)
                        {
                            double imgHeight = bmp.Height;
                            double imgWidth = bmp.Width;
                            double oran = 0;

                            if (imgWidth > MaxWidthSize)
                            {
                                oran = imgWidth / imgHeight;
                                imgWidth = MaxWidthSize;
                                imgHeight = MaxWidthSize / oran;
                                Size newValues = new Size(Convert.ToInt32(imgWidth), Convert.ToInt32(imgHeight));
                                Bitmap newImg = new Bitmap(orjImg, newValues);
                                newImg.Save(HttpContext.Current.Server.MapPath("~/Uploads/Images/Large/" + ImgName));
                                newImg.Dispose();
                                bmp.Dispose();
                                orjImg.Dispose();
                            }
                            else
                            {
                                file.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Large/" + ImgName));
                            }


                        }

                        bmp = new Bitmap(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
                        using (Bitmap orjImg = bmp)
                        {
                            double imgHeight = bmp.Height;
                            double imgWidth = bmp.Width;
                            double oran = 0;

                            if (imgWidth >= MinWidthSize)
                            {
                                oran = imgWidth / imgHeight;
                                imgWidth = MinWidthSize;
                                imgHeight = MinWidthSize / oran;
                                Size newValues = new Size(Convert.ToInt32(imgWidth), Convert.ToInt32(imgHeight));
                                Bitmap newImg = new Bitmap(orjImg, newValues);
                                newImg.Save(HttpContext.Current.Server.MapPath("~/Uploads/Images/Small/" + ImgName));
                                newImg.Dispose();
                                orjImg.Dispose();
                                bmp.Dispose();
                            }
                            else
                            {
                                file.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Small/" + ImgName));
                            }
                        }

                    }

                    FileInfo gecici = new FileInfo(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
                    gecici.Delete();
                    return ImgName;
                }
                else {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }



            
        }

        //public static string ImageUploadAndResizeLeft(HttpPostedFileBase ImageLeft, int MaxWidthSize, int MinWidthSize)
        //{
        //    string ImgName = ""; string extention = "";
        //    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/Images/"), Path.GetFileName(ImageLeft.FileName));
        //    //try
        //    //{

        //    if (path != null)
        //    {
        //        extention = Path.GetExtension(path);
        //        ImageLeft.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //        ImgName = Guid.NewGuid().ToString() + extention;

        //        Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //        using (Bitmap orjImg = bmp)
        //        {
        //            double imgHeight = bmp.Height;
        //            double imgWidth = bmp.Width;
        //            double oran = 0;

        //            if (imgWidth > MaxWidthSize)
        //            {
        //                oran = imgWidth / imgHeight;
        //                imgWidth = MaxWidthSize;
        //                imgHeight = MaxWidthSize / oran;
        //                Size newValues = new Size(Convert.ToInt32(imgWidth), Convert.ToInt32(imgHeight));
        //                Bitmap newImg = new Bitmap(orjImg, newValues);
        //                newImg.Save(HttpContext.Current.Server.MapPath("~/Uploads/Images/Large/" + ImgName));
        //                newImg.Dispose();
        //                bmp.Dispose();
        //                orjImg.Dispose();
        //            }
        //            else
        //            {
        //                ImageLeft.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Large/" + ImgName));
        //            }


        //        }

        //        bmp = new Bitmap(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //        using (Bitmap orjImg = bmp)
        //        {
        //            double imgHeight = bmp.Height;
        //            double imgWidth = bmp.Width;
        //            double oran = 0;

        //            if (imgWidth >= MinWidthSize)
        //            {
        //                oran = imgWidth / imgHeight;
        //                imgWidth = MinWidthSize;
        //                imgHeight = MinWidthSize / oran;
        //                Size newValues = new Size(Convert.ToInt32(imgWidth), Convert.ToInt32(imgHeight));
        //                Bitmap newImg = new Bitmap(orjImg, newValues);
        //                newImg.Save(HttpContext.Current.Server.MapPath("~/Uploads/Images/Small/" + ImgName));
        //                newImg.Dispose();
        //                orjImg.Dispose();
        //                bmp.Dispose();
        //            }
        //            else
        //            {
        //                ImageLeft.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Small/" + ImgName));
        //            }
        //        }

        //    }
        //    //}
        //    //catch (Exception)
        //    //{

        //    //}
        //    FileInfo gecici = new FileInfo(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //    gecici.Delete();
        //    return ImgName;
        //}


        //public static string ImageUploadAndResizeRight(HttpPostedFileBase ImageRight, int MaxWidthSize, int MinWidthSize)
        //{
        //    string ImgName = ""; string extention = "";
        //    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Uploads/Images/"), Path.GetFileName(ImageRight.FileName));
        //    //try
        //    //{

        //    if (path != null)
        //    {
        //        extention = Path.GetExtension(path);
        //        ImageRight.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //        ImgName = Guid.NewGuid().ToString() + extention;

        //        Bitmap bmp = new Bitmap(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //        using (Bitmap orjImg = bmp)
        //        {
        //            double imgHeight = bmp.Height;
        //            double imgWidth = bmp.Width;
        //            double oran = 0;

        //            if (imgWidth > MaxWidthSize)
        //            {
        //                oran = imgWidth / imgHeight;
        //                imgWidth = MaxWidthSize;
        //                imgHeight = MaxWidthSize / oran;
        //                Size newValues = new Size(Convert.ToInt32(imgWidth), Convert.ToInt32(imgHeight));
        //                Bitmap newImg = new Bitmap(orjImg, newValues);
        //                newImg.Save(HttpContext.Current.Server.MapPath("~/Uploads/Images/Large/" + ImgName));
        //                newImg.Dispose();
        //                bmp.Dispose();
        //                orjImg.Dispose();
        //            }
        //            else
        //            {
        //                ImageRight.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Large/" + ImgName));
        //            }


        //        }

        //        bmp = new Bitmap(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //        using (Bitmap orjImg = bmp)
        //        {
        //            double imgHeight = bmp.Height;
        //            double imgWidth = bmp.Width;
        //            double oran = 0;

        //            if (imgWidth >= MinWidthSize)
        //            {
        //                oran = imgWidth / imgHeight;
        //                imgWidth = MinWidthSize;
        //                imgHeight = MinWidthSize / oran;
        //                Size newValues = new Size(Convert.ToInt32(imgWidth), Convert.ToInt32(imgHeight));
        //                Bitmap newImg = new Bitmap(orjImg, newValues);
        //                newImg.Save(HttpContext.Current.Server.MapPath("~/Uploads/Images/Small/" + ImgName));
        //                newImg.Dispose();
        //                orjImg.Dispose();
        //                bmp.Dispose();
        //            }
        //            else
        //            {
        //                ImageRight.SaveAs(HttpContext.Current.Server.MapPath("~/Uploads/Images/Small/" + ImgName));
        //            }
        //        }

        //    }
        //    //}
        //    //catch (Exception)
        //    //{

        //    //}
        //    FileInfo gecici = new FileInfo(HttpContext.Current.Server.MapPath("~/Uploads/Images/Gecici/gecici" + extention));
        //    gecici.Delete();
        //    return ImgName;
        //}





        /// <summary>
        /// Uploaded File Control 
        /// </summary>
        /// <param name="file">HttpPostedFile file</param>
        /// <returns>string</returns>
        //public static string FileTypeControl(HttpPostedFile file) {

        //    //var extentions=new [] {".doc",".docx",".txt",".pdf",".ppt",".xls"}


        //    //return "";
        //}







        /// <summary>
        /// Mingus Content Analyzer - MNGCA ile contentteki verileri keywords lere ayırır.
        /// </summary>
        /// <param name="selectedContent">string contenteleman</param>
        /// <returns>string ifade döndürür</returns>
        public static string OrionContentAnalyzer(string selectedContent)
        {
            //OrionDBDataContext db = new OrionDBDataContext();
            string _keyword = "";
            try
            {
                string[] words = HttpUtility.HtmlDecode(Utils.clearTags(selectedContent)).Split(new string[] { "", " , ", ";", ".", " ", "'" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> keywords = new List<string>();

                foreach (var item in words.ToList())
                {
                    keywords.Add(item.Trim().ToLower());
                }

                string[] specials = { "i", "am", "you", "we", "they", "them", "he", "she", "it", "him", "her", "their", "there", "are", "is", "daha", "ve" };

                var result = keywords.GroupBy(x => x).Where(x => x.Count() > 0).Select(x => x.Key);

                foreach (var item in result.ToList())
                {
                    if (!_keyword.Contains(item))
                    {
                        if (!item.Contains("nbsp") || specials.Count(x => x.Contains(item)) < 0)
                        {
                            _keyword += item.Trim() + ",";
                        }
                    }
                }
                _keyword = _keyword.Substring(0, _keyword.Length - 1);
                return _keyword;

            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string clearEmptyAreas(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                string HtmlTagPattern = "<[^>]+?&nbsp;[^<]+?>";
                return Regex.Replace(html, HtmlTagPattern, string.Empty);
            }
            return html;
        }

        /// <summary>
        /// Html Taglarını Temizlemek için kullanılır.
        /// </summary>
        /// <param name="html">string döndürülen değer</param>
        /// <returns></returns>
        public static string clearTags(string html)
        {
            if (!String.IsNullOrEmpty(html))
            {
                //string HTML_TAG_PATTERN = "<.*?>";
                string HTML_TAG_PATTERN = @"<(.|\n)*?>";
                return Regex.Replace(html, HTML_TAG_PATTERN, String.Empty);
            }
            return html;
        }
        /// <summary>
        /// Paragraf Taglarını Temizlemek İçin Ayrı Bir Fonksiyon (\r - \n vs.)
        /// </summary>
        /// <param name="html">string döndürülen değer</param>
        /// <returns>string</returns>
        public static string clearParagraphTags(string html)
        {
            if (!String.IsNullOrEmpty(html))
            {
                string HTML_TAG_PATTERN = @"(?:\r\n|\n|\r|\'|\&)";
                return Regex.Replace(html, HTML_TAG_PATTERN, String.Empty);
            }
            return html;
        }


        public static void SendMailTo(string Subject, string Body, string OwnerMailAddress, string PortNumber, string UserMmailAddress, string HostAddress, string OwnerPass)
        {
            MailMessage msgMail = new MailMessage();
            MailMessage myMessage = new MailMessage();
            myMessage.From = new MailAddress(OwnerMailAddress, "Sisteme Kayıt İstemi Var - TECHNO.TECH BİLGİSAYAR");
            myMessage.To.Add(UserMmailAddress);
            myMessage.Subject = Subject;
            myMessage.IsBodyHtml = true;

            myMessage.Body = Body;

            SmtpClient mySmtpClient = new SmtpClient();
            System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential(OwnerMailAddress, OwnerPass);
            mySmtpClient.Host = HostAddress;
            mySmtpClient.UseDefaultCredentials = false;
            mySmtpClient.Credentials = myCredential;
            mySmtpClient.ServicePoint.MaxIdleTime = 1;
            mySmtpClient.EnableSsl = false;
            mySmtpClient.Port = Convert.ToInt32(PortNumber);
            mySmtpClient.Send(myMessage);
            myMessage.Dispose();

        }



    }
}