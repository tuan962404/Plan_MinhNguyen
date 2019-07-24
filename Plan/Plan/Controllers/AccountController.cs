using Plan.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Plan.Controllers
{
    public class AccountController : Controller
    {
        PlanEntities db = new PlanEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public int CheckLogin(string user, string password)
        {
            var kq = db.Users.FirstOrDefault(x => x.User1 == user && x.Password == password);
            if (kq != null)
            {
                if(kq.Status == true)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 1;
            }
        }

        public User GetUser(string user)
        {
            return db.Users.SingleOrDefault(x => x.User1 == user);
        }

        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User mod)
        {
            var temp = "@*@!" + mod.Password + "#^%";
            var kq = CheckLogin(mod.User1, MD5Hash(temp));
            if (kq == 0)
            {
                User userSession = new User();
                var user = GetUser(mod.User1);
                userSession.ID = user.ID;
                userSession.User1 = user.User1;
                userSession.Name = user.Name;
                userSession.GroupID = user.GroupID;
                var listCredentials = GetListCredential(mod.User1);
                Session.Add("SESSION_CREDENTIALS", listCredentials);
                Session.Add("USER_SESSION", userSession);
                return RedirectToAction("Index", "Home");
            }
            else if(kq == -1)
            {
                ModelState.AddModelError("", "Tài khoản này đã bị khóa!");
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public ActionResult ViewInfo()
        {
            var user = (User)Session["USER_SESSION"];
            var result = db.Users.FirstOrDefault(x => x.ID == user.ID);
            if(result != null)
            {
                ViewBag.Info = result;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo(User user)
        {
            if (!ModelState.IsValid)
            {
                var User = (User)Session["USER_SESSION"];
                var result = db.Users.FirstOrDefault(x => x.ID == User.ID);
                if (result != null)
                {
                    ViewBag.Info = result;
                }
                return View("ViewInfo",user);
            }
            else
            {
                var result = db.Users.FirstOrDefault(x => x.ID == user.ID);
                string password = "@*@!" + user.Password + "#^%";
                string temp = MD5Hash(password);
                if (temp == result.Password)
                {
                    result.Name = user.Name;
                    result.FullName = user.FullName;
                    result.NumberPhone = user.NumberPhone;
                    result.Email = user.Email;
                    result.BoPhan = user.BoPhan;
                    db.SaveChanges();

                    var User = (User)Session["USER_SESSION"];
                    result = db.Users.FirstOrDefault(x => x.ID == User.ID);
                    if (result != null)
                    {
                        ViewBag.Info = result;
                    }
                    ViewBag.Success = "success";
                    ModelState.AddModelError("", "Cập nhật thành công");
                    return View("ViewInfo");
                }
                else
                {
                    var User = (User)Session["USER_SESSION"];
                    result = db.Users.FirstOrDefault(x => x.ID == User.ID);
                    if (result != null)
                    {
                        ViewBag.Info = result;
                    }
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                    return View("ViewInfo");
                }
            }
        }

        public ActionResult CfmPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CfmPassword(User user)
        {
            var result = db.Users.Find(user.ID);
            string password = "@*@!" + user.Password + "#^%";
            string temp = MD5Hash(password);
            if(temp == result.Password)
            {
                return RedirectToAction("ChangePassword");
            }
            else
            {
                ModelState.AddModelError("", "Mật khẩu không đúng");
                return View();
            }
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(MUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                if(user.Password != user.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Xác thực mật khẩu không đúng!");
                    return View();
                }
                else
                {
                    var result = db.Users.Find(user.ID);
                    string password = "@*@!" + user.Password + "#^%";
                    string temp = MD5Hash(password);
                    if (result != null)
                    {
                        result.Password = temp;
                        db.SaveChanges();
                        ViewBag.Messege = "success";
                        ModelState.AddModelError("", "Đổi mật khẩu thành công!");
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đổi mật khẩu thất bại!");
                        return View();
                    }
                }
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(MUser user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                if (user.Password != user.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Xác thực mật khẩu không đúng!");
                    return View();
                }
                else
                {
                    var result = db.Users.FirstOrDefault(x=>x.User1 == user.User1);
                    if(result != null)
                    {
                        ModelState.AddModelError("", "Tên tài khoản đã được sử dụng!");
                        return View();
                    }
                    else
                    {
                        User u = new User();
                        u.User1 = user.User1;
                        string password = "@*@!" + user.Password + "#^%";
                        string temp = MD5Hash(password);
                        u.Password = temp;
                        u.Name = user.Name;
                        u.FullName = user.FullName;
                        u.BoPhan = user.BoPhan;
                        u.NumberPhone = user.NumberPhone;
                        u.Email = user.Email;
                        u.Status = false;
                        u.VIEW_USER = false;
                        u.LAP_KEHOACH = false;
                        u.EDIT_KEHOACH = false;
                        u.XUAT_EXCEL = false;
                        u.ADD_STOCK = false;
                        u.DELETE_STOCK = false;
                        u.ADD_RESINSTOCK = false;
                        u.DELETE_RESINSTOCK = false;
                        u.EDIT_STOCK = false;
                        u.EDIT_RESINSTOCK = false;
                        u.EDIT_KQSX = false;
                        u.NHAP_KQSX = false;
                        u.ADD_CODELIST = false;
                        u.ADD_PICKLISTVATTU = false;
                        u.IMPORT_PSI = false;
                        u.IMPORT_STOCK = false;
                        u.IMPORT_RESINSTOCK = false;
                        u.EDIT_CODELIST = false;
                        u.EDIT_PICKLISTVATTU = false;
                        u.GroupID = "MEMBER";
                        db.Users.Add(u);
                        db.SaveChanges();
                        ViewBag.Success = "success";
                        ModelState.AddModelError("", "Tạo tài khoản thành công!");
                        return View();
                    }
                }
            }
        }

        public List<string> GetListCredential(string userName)
        {
            List<string> list = new List<string>();
            var user = db.Users.FirstOrDefault(x => x.User1 == userName);
            if(user.GroupID == "MANAGER")
            {
                list.Add("MANAGER");
            }

            if(user.GroupID != "ADMIN" && user.GroupID != "MANAGER")
            {
                if (user.ADD_CODELIST.Equals(true))
                {
                    list.Add("ADD_CODELIST");
                }
                if (user.ADD_PICKLISTVATTU.Equals(true))
                {
                    list.Add("ADD_PICKLISTVATTU");
                }
                if (user.ADD_RESINSTOCK.Equals(true))
                {
                    list.Add("ADD_RESINSTOCK");
                }
                if (user.ADD_STOCK.Equals(true))
                {
                    list.Add("ADD_STOCK");
                }
                if (user.DELETE_RESINSTOCK.Equals(true))
                {
                    list.Add("DELETE_RESINSTOCK");
                }
                if (user.DELETE_STOCK.Equals(true))
                {
                    list.Add("DELETE_STOCK");
                }
                if (user.EDIT_CODELIST.Equals(true))
                {
                    list.Add("EDIT_CODELIST");
                }
                if (user.EDIT_KEHOACH.Equals(true))
                {
                    list.Add("EDIT_KEHOACH");
                }
                if (user.EDIT_KQSX.Equals(true))
                {
                    list.Add("EDIT_KQSX");
                }
                if (user.EDIT_PICKLISTVATTU.Equals(true))
                {
                    list.Add("EDIT_PICKLISTVATTU");
                }
                if (user.EDIT_RESINSTOCK.Equals(true))
                {
                    list.Add("");
                }
                if (user.EDIT_STOCK.Equals(true))
                {
                    list.Add("EDIT_RESINSTOCK");
                }
                if (user.IMPORT_PSI.Equals(true))
                {
                    list.Add("IMPORT_PSI");
                }
                if (user.IMPORT_RESINSTOCK.Equals(true))
                {
                    list.Add("IMPORT_RESINSTOCK");
                }
                if (user.IMPORT_STOCK.Equals(true))
                {
                    list.Add("IMPORT_STOCK");
                }
                if (user.LAP_KEHOACH.Equals(true))
                {
                    list.Add("LAP_KEHOACH");
                }
                if (user.NHAP_KQSX.Equals(true))
                {
                    list.Add("NHAP_KQSX");
                }
                if (user.VIEW_USER.Equals(true))
                {
                    list.Add("VIEW_USER");
                }
                if (user.XUAT_EXCEL.Equals(true))
                {
                    list.Add("XUAT_EXCEL");
                }
            }
            return list;
        }

        public ActionResult PhanQuyen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhanQuyen(User user)
        {
            var result = db.Users.FirstOrDefault(x => x.User1 == user.User1);
            var User = (User)Session["USER_SESSION"];
            if (result != null)
            {
                var GroupID = db.Users.FirstOrDefault(x=>x.ID == User.ID).GroupID;
                if (GroupID == "MANAGER")
                {
                    if (user.Password == null)
                    {
                        ViewBag.Info = result;
                        return View();
                    }
                    else
                    {
                        if (!ModelState.IsValid)
                        {
                            ViewBag.Info = result;
                            ViewBag.Error = "Dữ liệu không hợp lệ";
                            return View();
                        }
                        else
                        {
                            var checkpassword = db.Users.FirstOrDefault(x => x.ID == User.ID);
                            string password = "@*@!" + user.Password + "#^%";
                            string temp = MD5Hash(password);
                            if (temp == checkpassword.Password)
                            {
                                result.Name = user.Name;
                                result.FullName = user.FullName;
                                result.NumberPhone = user.NumberPhone;
                                result.Email = user.Email;
                                db.SaveChanges();

                                ViewBag.Info = result;
                                ViewBag.Success = "Cập nhật thành công";
                                return View();
                            }
                            else
                            {
                                ViewBag.Info = result;
                                ViewBag.Error = "Mật khẩu không đúng";
                                return View();
                            }
                        }
                    }
                }
                else if(result.GroupID == "ADMIN")
                {
                    ModelState.AddModelError("", "TÀI KHOẢN NÀY LÀ ADMIN. KHÔNG THỂ CẤP QUYỀN CHO TÀI KHOẢN NÀY! VUI LÒNG LIÊN HỆ KỸ THUẬT VIÊN ĐỂ ĐƯỢC HỖ TRỢ");
                    return View();
                }
                else
                {
                    if (user.Password == null)
                    {
                        ViewBag.Info = result;
                        return View();
                    }
                    else
                    {
                        if (!ModelState.IsValid)
                        {
                            ViewBag.Info = result;
                            ViewBag.Error = "Dữ liệu không hợp lệ";
                            return View();
                        }
                        else
                        {
                            var checkpassword = db.Users.FirstOrDefault(x => x.ID == User.ID);
                            string password = "@*@!" + user.Password + "#^%";
                            string temp = MD5Hash(password);
                            if (temp == checkpassword.Password)
                            {
                                result.Name = user.Name;
                                result.FullName = user.FullName;
                                result.NumberPhone = user.NumberPhone;
                                result.Email = user.Email;
                                result.BoPhan = user.BoPhan;
                                db.SaveChanges();

                                ViewBag.Info = result;
                                ViewBag.Success = "Cập nhật thành công";
                                return View();
                            }
                            else
                            {
                                ViewBag.Info = result;
                                ViewBag.Error = "Mật khẩu không đúng";
                                return View();
                            }
                        }
                    }
                }
            }
            else
            {
                ViewBag.NotFound = "Không tìm thấy tài khoản này";
                ModelState.AddModelError("", "Không tìm thấy tài khoản này");
                return View();
            }
        }

        [HttpPost]
        [HasCredentialAttribute.HasCredential(RoleID = "ADMIN")]
        public JsonResult EditQuyen(string user,string password,string GroupID, string ItemList)
        {
            var session = (User)Session["USER_SESSION"];
            var User = db.Users.FirstOrDefault(x=>x.ID == session.ID);
            password = "@*@!" + password + "#^%";
            string temp = MD5Hash(password);
            int flag = 0;
            if(temp == User.Password)
            {
                var result = db.Users.FirstOrDefault(x => x.User1 == user);
                string[] arr = ItemList.Split(',');

                if (result != null)
                {
                    if(arr.Length > 0)
                    {
                        int index = -2;
                        index = Array.IndexOf(arr, "VIEW_USER");
                        if (index != -1)
                        {
                            result.VIEW_USER = true;
                        }
                        else
                        {
                            result.VIEW_USER = false;
                        }

                        index = Array.IndexOf(arr, "LAP_KEHOACH");
                        if (index != -1)
                        {
                            result.LAP_KEHOACH = true;
                        }
                        else
                        {
                            result.LAP_KEHOACH = false;
                        }

                        index = Array.IndexOf(arr, "EDIT_KEHOACH");
                        if (index != -1)
                        {
                            result.EDIT_KEHOACH = true;
                        }
                        else
                        {
                            result.EDIT_KEHOACH = false;
                        }

                        index = Array.IndexOf(arr, "XUAT_EXCEL");
                        if (index != -1)
                        {
                            result.XUAT_EXCEL = true;
                        }
                        else
                        {
                            result.XUAT_EXCEL = false;
                        }

                        index = Array.IndexOf(arr, "ADD_STOCK");
                        if (index != -1)
                        {
                            result.ADD_STOCK = true;
                        }
                        else
                        {
                            result.ADD_STOCK = false;
                        }

                        index = Array.IndexOf(arr, "DELETE_STOCK");
                        if (index != -1)
                        {
                            result.DELETE_STOCK = true;
                        }
                        else
                        {
                            result.DELETE_STOCK = false;
                        }

                        index = Array.IndexOf(arr, "ADD_RESINSTOCK");
                        if (index != -1)
                        {
                            result.ADD_RESINSTOCK = true;
                        }
                        else
                        {
                            result.ADD_RESINSTOCK = false;
                        }

                        index = Array.IndexOf(arr, "DELETE_RESINSTOCK");
                        if (index != -1)
                        {
                            result.DELETE_RESINSTOCK = true;
                        }
                        else
                        {
                            result.DELETE_RESINSTOCK = false;
                        }

                        index = Array.IndexOf(arr, "EDIT_STOCK");
                        if (index != -1)
                        {
                            result.EDIT_STOCK = true;
                        }
                        else
                        {
                            result.EDIT_STOCK = false;
                        }

                        index = Array.IndexOf(arr, "EDIT_RESINSTOCK");
                        if (index != -1)
                        {
                            result.EDIT_RESINSTOCK = true;
                        }
                        else
                        {
                            result.EDIT_RESINSTOCK = false;
                        }

                        index = Array.IndexOf(arr, "EDIT_KQSX");
                        if (index != -1)
                        {
                            result.EDIT_KQSX = true;
                        }
                        else
                        {
                            result.EDIT_KQSX = false;
                        }

                        index = Array.IndexOf(arr, "NHAP_KQSX");
                        if (index != -1)
                        {
                            result.NHAP_KQSX = true;
                        }
                        else
                        {
                            result.NHAP_KQSX = false;
                        }

                        index = Array.IndexOf(arr, "ADD_CODELIST");
                        if (index != -1)
                        {
                            result.ADD_CODELIST = true;
                        }
                        else
                        {
                            result.ADD_CODELIST = false;
                        }

                        index = Array.IndexOf(arr, "ADD_PICKLISTVATTU");
                        if (index != -1)
                        {
                            result.ADD_PICKLISTVATTU = true;
                        }
                        else
                        {
                            result.ADD_PICKLISTVATTU = false;
                        }

                        index = Array.IndexOf(arr, "IMPORT_PSI");
                        if (index != -1)
                        {
                            result.IMPORT_PSI = true;
                        }
                        else
                        {
                            result.IMPORT_PSI = false;
                        }

                        index = Array.IndexOf(arr, "IMPORT_STOCK");
                        if (index != -1)
                        {
                            result.IMPORT_STOCK = true;
                        }
                        else
                        {
                            result.IMPORT_STOCK = false;
                        }

                        index = Array.IndexOf(arr, "IMPORT_RESINSTOCK");
                        if (index != -1)
                        {
                            result.IMPORT_RESINSTOCK = true;
                        }
                        else
                        {
                            result.IMPORT_RESINSTOCK = false;
                        }

                        index = Array.IndexOf(arr, "EDIT_CODELIST");
                        if (index != -1)
                        {
                            result.EDIT_CODELIST = true;
                        }
                        else
                        {
                            result.EDIT_CODELIST = false;
                        }

                        index = Array.IndexOf(arr, "EDIT_PICKLISTVATTU");
                        if (index != -1)
                        {
                            result.EDIT_PICKLISTVATTU = true;
                        }
                        else
                        {
                            result.EDIT_PICKLISTVATTU = false;
                        }

                        db.SaveChanges();
                    }
                    
                    result.GroupID = GroupID;
                    db.SaveChanges();
                    return Json(flag, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    flag = 1;
                    ViewBag.NotFound = "";
                    return Json(flag, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                flag = 2;
                return Json(flag, JsonRequestBehavior.AllowGet);
            } 
        }

        [HttpPost]
        [HasCredentialAttribute.HasCredential(RoleID = "ADMIN")]
        public JsonResult ResetPassword(string user,string matkhaureset,string matkhauxacthuc)
        {
            int flag = 0;
            var session = (User)Session["USER_SESSION"];
            var User = db.Users.FirstOrDefault(x => x.ID == session.ID);
            string password = "@*@!" + matkhauxacthuc + "#^%";
            string temp = MD5Hash(password);
            if(temp == User.Password)
            {
                var result = db.Users.FirstOrDefault(x=>x.User1 == user);
                if(result != null)
                {
                    if(matkhaureset.Length < 6 || matkhaureset.Length > 32)
                    {
                        flag = 3;
                    }
                    else
                    {
                        string pastemp = "@*@!" + matkhaureset + "#^%";
                        result.Password = MD5Hash(pastemp);
                        db.SaveChanges();
                        flag = 1;
                    }
                }
                else
                {
                    flag = 2;
                }
            }
            else
            {
                flag = 0;
            }
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HasCredentialAttribute.HasCredential(RoleID = "ADMIN")]
        public JsonResult BlockAccount(string user,string matkhauxacthuc, string Itemlist)
        {
            int flag = 0;
            var session = (User)Session["USER_SESSION"];
            var User = db.Users.FirstOrDefault(x => x.ID == session.ID);
            string password = "@*@!" + matkhauxacthuc + "#^%";
            string temp = MD5Hash(password);
            if (temp == User.Password)
            {
                var result = db.Users.FirstOrDefault(x => x.User1 == user);
                if (result != null)
                {
                    if (Itemlist != "")
                    {
                        result.Status = false;
                        flag = -1;
                    }
                    else
                    {
                        result.Status = true;
                        flag = 1;
                    }
                    db.SaveChanges();
                    
                }
                else
                {
                    flag = 2;
                }
            }
            else
            {
                flag = 0;
            }
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        public bool SendMail(string toEmail, string subject, string content,string fromEmailDisplayName)
        {
            bool flag = false;
            try
            {
                var mailserver = db.MailServers.FirstOrDefault(x => x.ID == 1);
                var fromEmailAddress = mailserver.Email;
                var fromEmailPassword = mailserver.Password;
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

                bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

                string path = Server.MapPath("~/Content/Image/Mail_Img.png");

                string body = content;
                MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmail));
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                LinkedResource LinkedImage = new LinkedResource(path);
                LinkedImage.ContentId = "MyPic";
                LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body + "<img src=cid:MyPic>", null, "text/html");
                htmlView.LinkedResources.Add(LinkedImage);
                message.AlternateViews.Add(htmlView);

                var client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                client.Host = smtpHost;
                client.EnableSsl = enabledSsl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
                client.Send(message);
                return flag;
            }
            catch
            {
                return flag = true;
            }
        }

        public ActionResult ServerError()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(User user)
        {
            var result = db.Users.FirstOrDefault(x=>x.Email == user.Email && x.User1 == user.User1);
            if(result != null)
            {
                string code = RandomCode();

                var kq = db.MailVerifies.FirstOrDefault(x => x.UserName.Equals(result.User1) && x.Email.Equals(result.Email));
                if(kq != null)
                {
                    kq.Status = false;
                    kq.CodeVerify = code;
                    db.SaveChanges();
                }
                else
                {
                    MailVerify mv = new MailVerify();
                    mv.UserName = result.User1;
                    mv.Email = result.Email;
                    mv.CodeVerify = code;
                    mv.Status = false;
                    db.MailVerifies.Add(mv);
                    db.SaveChanges();
                }

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/TemplateEmail/TemplateEmail.html"));

                var mailserver = db.MailServers.FirstOrDefault(x=>x.ID == 1);

                content = content.Replace("{{Code}}", code);
                content = content.Replace("{{MailServer}}", mailserver.Email);
                var toEmail = user.Email;
                string fromEmailDisplayName = "Xác thực email";

                bool flag = SendMail(toEmail, "Xác thực email lấy lại mật khẩu tài khoản Minh Nguyên", content, fromEmailDisplayName);
                if (flag.Equals(false))
                {
                    ViewBag.User = result.User1;
                    ViewBag.Email = result.Email;
                    return View("VerifyCode");
                }
                else
                {
                    return RedirectToAction("ServerError");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên tài khoản hoặc email không đúng!");
                return View();
            }
        }

        public ActionResult VerifyCode()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyCode(MailVerify mv)
        {
            var result = db.MailVerifies.FirstOrDefault(x=>x.UserName == mv.UserName && x.Email == mv.Email);
            if(result != null)
            {
                if (result.Status.Equals(false))
                {
                    mv.CodeVerify = mv.CodeVerify.Replace(" ","");
                    if (result.CodeVerify.Equals(mv.CodeVerify))
                    {
                        ViewBag.User = mv.UserName;
                        ViewBag.Email = mv.Email;
                        result.Status = true;
                        db.SaveChanges();
                        return View("GetPassword");
                    }
                    else
                    {
                        ViewBag.User = mv.UserName;
                        ViewBag.Email = mv.Email;
                        ModelState.AddModelError("", "Mã xác thực không đúng!");
                        return View();
                    }
                }
                else
                {
                    ViewBag.User = mv.UserName;
                    ViewBag.Email = mv.Email;
                    ModelState.AddModelError("", "Mã xác thực không còn hiệu lực!");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy tài khoản cần xác thực!");
                return View();
            }
        }

        public ActionResult GetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetPassword(MUser user)
        {
            var result = db.Users.FirstOrDefault(x=>x.User1 == user.User1 && x.Email == user.Email);
            if(result != null)
            {
                if(user.Password.Length < 6 || user.Password.Length > 32)
                {
                    ViewBag.User = user.User1;
                    ViewBag.Email = user.Email;
                    ModelState.AddModelError("", "Mật khẩu phải ít nhất 6 ký tự và tối đa 32 ký tự!");
                    return View();
                }
                else
                {
                    if (user.Password == user.ConfirmPassword)
                    {
                        string temp = "@*@!" + user.Password + "#^%";
                        string password = MD5Hash(temp);
                        result.Password = password;
                        db.SaveChanges();
                        return View("SendMailVerify");
                    }
                    else
                    {
                        ViewBag.User = user.User1;
                        ViewBag.Email = user.Email;
                        ModelState.AddModelError("", "Mật khẩu xác thực không đúng!");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Không tìm thấy tài khoản cần đổi mật khẩu!");
                return View();
            }
        }

        public string RandomCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public ActionResult SendMailVerify()
        {
            return View();
        }

        [HasCredentialAttribute._2HasCredential(RoleID = "MANAGER")]
        public ActionResult UpdateMailServer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute._2HasCredential(RoleID = "MANAGER")]
        public ActionResult UpdateMailServer(MailServer ms)
        {
            var email = db.MailServers.FirstOrDefault(x => x.ID == 1);
            if(email != null)
            {
                if(ms.Email == email.Email)
                {
                    if (ms.Password == email.Password)
                    {
                        return RedirectToAction("UPDATE_MAILSERVER");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Mật khẩu không đúng!");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("","Email hiện tại không đúng!");
                    return View();
                }
            }
            else
            {
                return View("NotFound","Home");
            }
        }

        [HasCredentialAttribute._2HasCredential(RoleID = "MANAGER")]
        public ActionResult UPDATE_MAILSERVER()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute._2HasCredential(RoleID = "MANAGER")]
        public ActionResult UPDATE_MAILSERVER(MailSV ms)
        {
            if(ms.Password == ms.CfmPassword)
            {
                var email = db.MailServers.FirstOrDefault(x => x.ID == 1);
                email.Email = ms.Email;
                email.Password = ms.Password;
                db.SaveChanges();
                ViewBag.Success = "success";
                ModelState.AddModelError("", "Cập nhật thành công.");
                return View();
            }
            else
            {
                ModelState.AddModelError("","Mật khẩu xác thực không đúng!");
                return View();
            }   
        }
    }
}