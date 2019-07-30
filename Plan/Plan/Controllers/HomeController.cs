using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Text.RegularExpressions;
using Plan.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using Plan.HasCredentialAttribute;

namespace Plan.Controllers
{
    public class HomeController : BaseController
    {
        PlanEntities db = new PlanEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportPicklist_VatTu()
        {
            return View();
        }

        public ActionResult Import_CodeList()
        {
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "IMPORT_PSI")]
        public ActionResult Import_PSI_WM_REF()
        {
            return View();
        }

        public ActionResult Import_PSI_VC()
        {
            return View();
        }

        public ActionResult Import_Assy_PSI_Shortage()
        {
            return View();
        }

        public ActionResult Import_Stock()
        {
            return View();
        }

        public ActionResult Import_Production_Result()
        {
            return View();
        }

        public ActionResult ViewStock()
        {
            ViewBag.ListStock = db.Stocks.ToList();
            return View();
        }

        public ActionResult ResinViewStock()
        {
            ViewBag.ListStock = db.ResinStocks.ToList();
            return View();
        }

        public ActionResult Import_Succeeded()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult View_VatTu()
        {
            ViewBag.VatTu = db.PicklistVatTus.ToList();
            return View();
        }

        public ActionResult View_Codelist()
        {
            ViewBag.Codelist = db.CodeLists.ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "NHAP_KQSX")]
        public ActionResult View_PlanWeek_ForProDuct()
        {
            ViewBag.PlanWeek = db.Plan_Week.OrderByDescending(x=>x.STT).ToList();
            return View();
        }

        public ActionResult View_PlanWeek_ForViewProduct()
        {
            ViewBag.PlanWeek = db.Plan_Week.OrderByDescending(x => x.STT).ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_PlanWeek_SummaryProduct()
        {
            ViewBag.PlanWeek = db.Plan_Week.OrderByDescending(x => x.STT).ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_PlanWeek_SummaryResin()
        {
            ViewBag.PlanWeek = db.Plan_Week.OrderByDescending(x => x.STT).ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_SummaryProduct(string week,DateTime dateStart,DateTime dateEnd)
        {
            ViewBag.Week = week;
            ViewBag.dateStart = dateStart;
            ViewBag.dateEnd = dateEnd;
            ViewBag.Plan = db.DA_Injection.Where(x => x.Week == week).ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_SummaryResin(string week, DateTime dateStart, DateTime dateEnd)
        {
            ViewBag.Week = week;
            ViewBag.dateStart = dateStart;
            ViewBag.dateEnd = dateEnd;

            ViewBag.Plan = db.DA_Injection.Where(x => x.Week == week).ToList();
            var check = db.SummaryByProducts.FirstOrDefault(x => x.Week == week);
            string codetemp = "";
            foreach (DA_Injection i in ViewBag.Plan)
            {
                if (i.Code != codetemp)
                {
                    SummaryByProduct sm = new SummaryByProduct();

                    codetemp = i.Code;
                    var kq = db.CodeLists.Where(x => x.Injection_Code == i.Code || x.Another_Code == i.Code).FirstOrDefault();
                    kq.Weight = kq.Weight.Replace(".", ",");
                    int index = kq.Resin_Code.IndexOf("\n");

                    if(index != -1)
                    {
                        string[] arrListStr = kq.Resin_Code.Split('\n');
                        sm.Code = i.Code;
                        sm.Mc = i.So_May;
                        sm.ResinCode = arrListStr[0];
                        sm.MixRate = 0.96;
                        if (kq != null)
                        {
                            sm.Resin = kq.Resin;
                            sm.NetWeight = Convert.ToDouble(kq.Weight);
                        }

                        foreach (DA_Injection k in ViewBag.Plan)
                        {
                            if (k.Code == i.Code && k.PSI == i.PSI && k.So_May == i.So_May)
                            {
                                double sum = Convert.ToDouble(k.Plan_D_ + k.Plan_N_);
                                sum = Math.Ceiling(sum * Convert.ToDouble(kq.Weight) * 0.96);

                                sm.Date = k.Date;
                                sm.SoLuong = (int)sum;
                                sm.Week = week;

                                if (check == null)
                                {
                                    db.SummaryByProducts.Add(sm);
                                    db.SaveChanges();
                                }
                            }
                        }

                        sm.Code = i.Code;
                        sm.Mc = i.So_May;
                        sm.ResinCode = arrListStr[1];
                        sm.MixRate = 0.04;
                        if (kq != null)
                        {
                            sm.NetWeight = Convert.ToDouble(kq.Weight);
                        }

                        foreach (DA_Injection k in ViewBag.Plan)
                        {
                            if (k.Code == i.Code && k.PSI == i.PSI && k.So_May == i.So_May)
                            {
                                double sum = Convert.ToDouble(k.Plan_D_ + k.Plan_N_);
                                sum = Math.Ceiling(sum * Convert.ToDouble(kq.Weight) * 0.04);

                                sm.Date = k.Date;
                                sm.SoLuong = (int)sum;
                                sm.Week = week;
                                if (check == null)
                                {
                                    db.SummaryByProducts.Add(sm);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    else
                    {
                        sm.Code = i.Code;
                        sm.Mc = i.So_May;
                        sm.MixRate = 1;
                        if (kq != null)
                        {
                            sm.Resin = kq.Resin;
                            sm.ResinCode = kq.Resin_Code;
                            sm.NetWeight = Convert.ToDouble(kq.Weight);
                        }

                        foreach (DA_Injection k in ViewBag.Plan)
                        {
                            if (k.Code == i.Code && k.PSI == i.PSI && k.So_May == i.So_May)
                            {
                                double sum = Convert.ToDouble(k.Plan_D_ + k.Plan_N_);
                                sum = Math.Ceiling(sum * Convert.ToDouble(kq.Weight));

                                sm.Date = k.Date;
                                sm.SoLuong = (int)sum;
                                sm.Week = week;
                                if (check == null)
                                {
                                    db.SummaryByProducts.Add(sm);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            List<SummaryByProduct> listtemp = new List<SummaryByProduct>();
            var tam = db.SummaryByProducts.Where(x => x.Week == week).ToList();
            foreach(var k in tam)
            {
                bool flag = false;
                foreach(var p in listtemp)
                {
                    if(p.ResinCode == k.ResinCode)
                    {
                        flag = true;
                    }
                }

                if(flag == false)
                {
                    listtemp.Add(k);
                }
            }
            ViewBag.CodeList = listtemp;
            List<string> listcodenull = new List<string>();
            foreach(var k in listtemp)
            {
                var stock = db.ResinStocks.FirstOrDefault(x=>x.Code == k.ResinCode);
                if(stock == null)
                {
                    listcodenull.Add(k.ResinCode);
                }
            }
            ViewBag.CodeNull = listcodenull;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportPicklist_VatTu(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets[13];
                    Excel.Range range = worksheet.UsedRange;
                    for (int row = 2; row <= 96; row++)
                    {
                        PicklistVatTu picklistVatTu = new PicklistVatTu();
                        picklistVatTu.Injection_Code = ((Excel.Range)range.Cells[row, 2]).Text;
                        picklistVatTu.Machine_No_ = ((Excel.Range)range.Cells[row, 3]).Text;
                        picklistVatTu.Machine = ((Excel.Range)range.Cells[row, 4]).Text;
                        picklistVatTu.Cycle_Time = ((Excel.Range)range.Cells[row, 5]).Text;
                        picklistVatTu.Cavity = ((Excel.Range)range.Cells[row, 6]).Text;
                        picklistVatTu.Manpower = ((Excel.Range)range.Cells[row, 7]).Text;
                        picklistVatTu.Resin = ((Excel.Range)range.Cells[row, 8]).Text;
                        picklistVatTu.Resin_Code = ((Excel.Range)range.Cells[row, 9]).Text;
                        //picklistVatTu.Weight = ((Excel.Range)range.Cells[row, 10]).Text;
                        picklistVatTu.Vat_Tu_Code = ((Excel.Range)range.Cells[row, 11]).Text;
                        picklistVatTu.Ten_Vat_Tu = ((Excel.Range)range.Cells[row, 12]).Text;
                        picklistVatTu.Ty_Le = ((Excel.Range)range.Cells[row, 13]).Text;
                        picklistVatTu.Another_Code = ((Excel.Range)range.Cells[row, 14]).Text;
                        db.PicklistVatTus.Add(picklistVatTu);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Import_Succeeded");
                }
                else
                {
                    ViewBag.Error = "File Excel không đúng";
                    return RedirectToAction("Import_PSI_WM_REF");
                }
            }
            else
            {
                ViewBag.Error = "Chưa Chọn File";
                return RedirectToAction("Import_PSI_WM_REF");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import_CodeList(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets["Code List"];
                    Excel.Range range = worksheet.UsedRange;
                    worksheet.Columns.AutoFit();
                    for (int row = 2; row <= range.Rows.Count; row++)
                    {
                        if(range.Cells[row, 2].Text == "")
                        {
                            continue;
                        }
                        CodeList codeList = new CodeList();
                        codeList.Injection_Code = (((Excel.Range)range.Cells[row, 2]).Text);
                        codeList.ItemName = (((Excel.Range)range.Cells[row, 3]).Text);
                        codeList.Machine_No_ = Convert.ToString(((Excel.Range)range.Cells[row, 4]).Text);
                        codeList.Machine = Convert.ToString(((Excel.Range)range.Cells[row, 5]).Text);
                        codeList.Cycle_Time = Convert.ToString(((Excel.Range)range.Cells[row, 6]).Text);
                        codeList.Cavity = Convert.ToString(((Excel.Range)range.Cells[row, 7]).Text);
                        codeList.Manpower = Convert.ToString(((Excel.Range)range.Cells[row, 8]).Text);
                        codeList.Resin = ((Excel.Range)range.Cells[row, 9]).Text;
                        codeList.Resin_Code = ((Excel.Range)range.Cells[row, 10]).Text;
                        codeList.Weight = Convert.ToString(((Excel.Range)range.Cells[row, 11]).Text);
                        codeList.Another_Code = ((Excel.Range)range.Cells[row, 12]).Text;
                        db.CodeLists.Add(codeList);
                        db.SaveChanges();
                    }
                    workbook.Close(0);
                    app.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                    System.IO.File.Delete(path);
                    return RedirectToAction("Import_Succeeded");
                }
                else
                {
                    ViewBag.Error = "File Excel không đúng";
                    return RedirectToAction("Import_PSI_WM_REF");
                }
            }
            else
            {
                ViewBag.Error = "Chưa Chọn File";
                return RedirectToAction("Import_PSI_WM_REF");
            }
        }

        public bool isNumber(string text)
        {
            bool flag = false;
            Regex reg = new Regex("^[()]$");
            if (reg.IsMatch(text))
            {
                flag = true;
            }
            return flag;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import_PSI_WM_REF(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    //app.Visible = true;
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    try
                    {
                        Excel.Worksheet worksheet = workbook.Worksheets["PSI WM REF"];
                        worksheet.Columns.AutoFit();
                        //worksheet.Rows.AutoFit();
                        Excel.Range range = worksheet.UsedRange;
                        try
                        {
                            int temp = 0;
                            for (int row = 7; row <= range.Rows.Count; row++)
                            {
                                if (((Excel.Range)range.Cells[row, 3]).Text == "")
                                {
                                    continue;
                                }
                                PSI_WM_REF psi_wm_ref = new PSI_WM_REF();
                                int row2 = 5;
                                for (int col = 23; col <= range.Columns.Count; col++)
                                {
                                    string replace_balance1;
                                    if (IsDate(((Excel.Range)range.Cells[row2, col]).Text) == false)
                                    {
                                        continue;
                                    }
                                    if (row == 7)
                                    {
                                        string code_check = ((Excel.Range)range.Cells[row, 3]).Text;
                                        string date_str = ((Excel.Range)range.Cells[row2, col]).Text;
                                        DateTime date_check = Convert.ToDateTime(date_str);
                                        var kq = db.PSI_WM_REF.Where(x => x.Date == date_check && x.Code == code_check).FirstOrDefault();
                                        if (kq != null)
                                        {
                                            kq.Code = (((Excel.Range)range.Cells[row, 3]).Text);
                                            kq.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            kq.Date = Convert.ToDateTime(date);
                                            int t1 = row2 + 2;
                                            string t3 = ((Excel.Range)range.Cells[t1, col]).Text;
                                            string replace_number_plus = t3.Replace(".", "");
                                            string replace_number = replace_number_plus.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                psi_wm_ref.Requirements = 0;
                                            }
                                            else
                                            {
                                                kq.Requirements = Convert.ToInt32(replace_number);
                                            }

                                            int t4 = row2 + 4;
                                            char[] charsToTrim = { '(', ')' };
                                            string balance_str = ((Excel.Range)range.Cells[t4, col]).Text;
                                            string replace_balance_plus = balance_str.Replace(".", "");
                                            string replace_balance = replace_balance_plus.Replace(",", "");
                                            if (isNumber(replace_balance))
                                            {
                                                replace_balance1 = replace_balance.Trim(charsToTrim);
                                                replace_balance1 = "-" + replace_balance1;
                                            }
                                            else
                                            {
                                                replace_balance1 = replace_balance;
                                            }

                                            if (replace_balance1 == "#NAME!" || replace_balance1 == "#N/A" || replace_balance1 == "#VALUE!" || replace_balance1 == "#REF!" || replace_balance1 == "#NUM!" || replace_balance1 == "#NULL" || replace_balance1 == "#DIV/0!" || replace_balance1 == "")
                                            {
                                                kq.Balace_Stock_ = 0;
                                            }
                                            else
                                            {
                                                kq.Balace_Stock_ = Convert.ToInt32(replace_balance1);
                                            }
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            psi_wm_ref.Code = (((Excel.Range)range.Cells[row, 3]).Text);
                                            psi_wm_ref.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            psi_wm_ref.Date = Convert.ToDateTime(date);
                                            int t1 = row2 + 2;
                                            string t3 = ((Excel.Range)range.Cells[t1, col]).Text;
                                            string replace_number_plus = t3.Replace(".", "");
                                            string replace_number = replace_number_plus.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                psi_wm_ref.Requirements = 0;
                                            }
                                            else
                                            {
                                                psi_wm_ref.Requirements = Convert.ToInt32(replace_number);
                                            }

                                            int t4 = row2 + 4;
                                            char[] charsToTrim = { '(', ')' };
                                            string balance_str = ((Excel.Range)range.Cells[t4, col]).Text;
                                            string replace_balance_plus = balance_str.Replace(".", "");
                                            string replace_balance = replace_balance_plus.Replace(",", "");
                                            if (isNumber(replace_balance))
                                            {
                                                replace_balance1 = replace_balance.Trim(charsToTrim);
                                                replace_balance1 = "-" + replace_balance1;
                                            }
                                            else
                                            {
                                                replace_balance1 = replace_balance;
                                            }

                                            if (replace_balance1 == "#NAME!" || replace_balance1 == "#N/A" || replace_balance1 == "#VALUE!" || replace_balance1 == "#REF!" || replace_balance1 == "#NUM!" || replace_balance1 == "#NULL" || replace_balance1 == "#DIV/0!" || replace_balance1 == "")
                                            {
                                                psi_wm_ref.Balace_Stock_ = 0;
                                            }
                                            else
                                            {
                                                psi_wm_ref.Balace_Stock_ = Convert.ToInt32(replace_balance1);
                                            }
                                            db.PSI_WM_REF.Add(psi_wm_ref);
                                            db.SaveChanges();
                                        }

                                    }
                                    if (row > 7)
                                    {
                                        string code_check = ((Excel.Range)range.Cells[row, 3]).Text;
                                        string date_str = ((Excel.Range)range.Cells[row2, col]).Text;
                                        DateTime date_check = Convert.ToDateTime(date_str);
                                        var kq = db.PSI_WM_REF.Where(x => x.Date == date_check && x.Code == code_check).FirstOrDefault();
                                        if (kq != null)
                                        {
                                            kq.Code = (((Excel.Range)range.Cells[row, 3]).Text);
                                            kq.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            kq.Date = Convert.ToDateTime(date);
                                            int t2 = temp + 7;
                                            string t3 = ((Excel.Range)range.Cells[t2, col]).Text;
                                            string replace_number_plus = t3.Replace(".", "");
                                            string replace_number = replace_number_plus.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                kq.Requirements = 0;
                                            }
                                            else
                                            {
                                                kq.Requirements = Convert.ToInt32(replace_number);
                                            }

                                            int t4 = temp + 9;
                                            char[] charsToTrim = { '(', ')' };
                                            string balance_str = ((Excel.Range)range.Cells[t4, col]).Text;
                                            string replace_balance_plus = balance_str.Replace(".", "");
                                            string replace_balance = replace_balance_plus.Replace(",", "");

                                            if (isNumber(replace_balance))
                                            {
                                                replace_balance1 = replace_balance.Trim(charsToTrim);
                                                replace_balance1 = "-" + replace_balance1;
                                            }
                                            else
                                            {
                                                replace_balance1 = replace_balance;
                                            }

                                            if (replace_balance1 == "#NAME!" || replace_balance1 == "#N/A" || replace_balance1 == "#VALUE!" || replace_balance1 == "#REF!" || replace_balance1 == "#NUM!" || replace_balance1 == "#NULL" || replace_balance1 == "#DIV/0!" || replace_balance1 == "")
                                            {
                                                kq.Balace_Stock_ = 0;
                                            }
                                            else
                                            {
                                                kq.Balace_Stock_ = Convert.ToInt32(replace_balance1);
                                            }
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            psi_wm_ref.Code = (((Excel.Range)range.Cells[row, 3]).Text);
                                            psi_wm_ref.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            psi_wm_ref.Date = Convert.ToDateTime(date);
                                            int t2 = temp + 7;
                                            string t3 = ((Excel.Range)range.Cells[t2, col]).Text;
                                            string replace_number_plus = t3.Replace(".", "");
                                            string replace_number = replace_number_plus.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                psi_wm_ref.Requirements = 0;
                                            }
                                            else
                                            {
                                                psi_wm_ref.Requirements = Convert.ToInt32(replace_number);
                                            }

                                            int t4 = temp + 9;
                                            char[] charsToTrim = { '(', ')' };
                                            string balance_str = ((Excel.Range)range.Cells[t4, col]).Text;
                                            string replace_balance_plus = balance_str.Replace(".", "");
                                            string replace_balance = replace_balance_plus.Replace(",", "");

                                            if (isNumber(replace_balance))
                                            {
                                                replace_balance1 = replace_balance.Trim(charsToTrim);
                                                replace_balance1 = "-" + replace_balance1;
                                            }
                                            else
                                            {
                                                replace_balance1 = replace_balance;
                                            }

                                            if (replace_balance1 == "#NAME!" || replace_balance1 == "#N/A" || replace_balance1 == "#VALUE!" || replace_balance1 == "#REF!" || replace_balance1 == "#NUM!" || replace_balance1 == "#NULL" || replace_balance1 == "#DIV/0!" || replace_balance1 == "")
                                            {
                                                psi_wm_ref.Balace_Stock_ = 0;
                                            }
                                            else
                                            {
                                                psi_wm_ref.Balace_Stock_ = Convert.ToInt32(replace_balance1);
                                            }
                                            db.PSI_WM_REF.Add(psi_wm_ref);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                temp += 5;
                                row += 4;
                            }
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return RedirectToAction("Import_Succeeded");
                        }
                        catch
                        {
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return RedirectToAction("NotFound");
                        }
                    }
                    catch
                    {
                        workbook.Close(0);
                        app.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                        System.IO.File.Delete(path);
                        ViewBag.ErrorWorksheet1 = "Không tìm thấy sheet PSI WM REF!";
                        return View("Import_PSI_WM_REF");
                    }
                }
                else
                {

                    ViewBag.Error1 = "File Excel không đúng";
                    return View("Import_PSI_WM_REF");
                }
            }
            else
            {
                ViewBag.Error1 = "Chưa Chọn File";
                return View("Import_PSI_WM_REF");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "IMPORT_PSI")]
        public ActionResult Import_PSI_VC(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    //app.Visible = true;
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    try
                    {
                        Excel.Worksheet worksheet = workbook.Worksheets["PSI VC"];
                        //Excel.Worksheet worksheet = workbook.Worksheets["Daily FC"];
                        worksheet.Columns.AutoFit();
                        //worksheet.Rows.AutoFit();
                        Excel.Range range = worksheet.UsedRange;
                        try
                        {
                            int temp = 0;
                            for (int row = 4; row <= range.Rows.Count; row++)
                            {
                                if (((Excel.Range)range.Cells[row, 3]).Text == "")
                                {
                                    continue;
                                }
                                PSI_V_C psi_vc = new PSI_V_C();
                                int row2 = 3;
                                for (int col = 15; col <= range.Columns.Count; col++)
                                {
                                    if (IsDate(((Excel.Range)range.Cells[row2, col]).Text) == false)
                                    {
                                        continue;
                                    }
                                    if (row == 4)
                                    {
                                        string code_check = ((Excel.Range)range.Cells[row, 3]).Text;
                                        string demand = ((Excel.Range)range.Cells[1, col]).Text;
                                        if (demand == "Balance")
                                        {
                                            break;
                                        }
                                        string date_str = ((Excel.Range)range.Cells[row2, col]).Text;
                                        DateTime date_check = Convert.ToDateTime(date_str);
                                        var kq = db.PSI_V_C.Where(x => x.Date == date_check && x.Code == code_check).FirstOrDefault();
                                        if (kq != null)
                                        {
                                            kq.Code = (((Excel.Range)range.Cells[row, 3]).Text);

                                            kq.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            kq.Date = Convert.ToDateTime(date);
                                            int t1 = row2 + 1;
                                            string t3 = ((Excel.Range)range.Cells[t1, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            replace_number = replace_number.Replace(".", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                kq.Requirement = 0;
                                            }
                                            else
                                            {
                                                kq.Requirement = Convert.ToInt32(replace_number);
                                            }
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            psi_vc.Code = (((Excel.Range)range.Cells[row, 3]).Text);

                                            psi_vc.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            psi_vc.Date = Convert.ToDateTime(date);
                                            int t1 = row2 + 1;
                                            string t3 = ((Excel.Range)range.Cells[t1, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            replace_number = replace_number.Replace(".", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                psi_vc.Requirement = 0;
                                            }
                                            else
                                            {
                                                psi_vc.Requirement = Convert.ToInt32(replace_number);
                                            }
                                            db.PSI_V_C.Add(psi_vc);
                                            db.SaveChanges();
                                        }
                                    }
                                    if (row > 4)
                                    {
                                        string code_check = ((Excel.Range)range.Cells[row, 3]).Text;
                                        string demand = ((Excel.Range)range.Cells[1, col]).Text;
                                        if (demand == "Balance")
                                        {
                                            break;
                                        }
                                        string date_str = ((Excel.Range)range.Cells[row2, col]).Text;
                                        DateTime date_check = Convert.ToDateTime(date_str);
                                        var kq = db.PSI_V_C.Where(x => x.Date == date_check && x.Code == code_check).FirstOrDefault();
                                        if (kq != null)
                                        {
                                            kq.Code = (((Excel.Range)range.Cells[row, 3]).Text);

                                            kq.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            kq.Date = Convert.ToDateTime(date);
                                            int t2 = temp + 4;
                                            string t3 = ((Excel.Range)range.Cells[t2, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            replace_number = replace_number.Replace(".", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                kq.Requirement = 0;
                                            }
                                            else
                                            {
                                                kq.Requirement = Convert.ToInt32(replace_number);
                                            }
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            psi_vc.Code = (((Excel.Range)range.Cells[row, 3]).Text);

                                            psi_vc.Description = (((Excel.Range)range.Cells[row, 4]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            psi_vc.Date = Convert.ToDateTime(date);
                                            int t2 = temp + 4;
                                            string t3 = ((Excel.Range)range.Cells[t2, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            replace_number = replace_number.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                psi_vc.Requirement = 0;
                                            }
                                            else
                                            {
                                                psi_vc.Requirement = Convert.ToInt32(replace_number);
                                            }
                                            db.PSI_V_C.Add(psi_vc);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                temp += 1;
                            }
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return RedirectToAction("Import_Succeeded");
                        }
                        catch
                        {
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return RedirectToAction("NotFound");
                        }
                    }
                    catch
                    {
                        workbook.Close(0);
                        app.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                        System.IO.File.Delete(path);
                        ViewBag.ErrorWorksheet2 = "Không tìm thấy sheet PSI VC!";
                        return View("Import_PSI_WM_REF");
                    }
                }
                else
                {
                    ViewBag.Error2 = "File Excel không đúng";
                    return View("Import_PSI_WM_REF");
                }
            }
            else
            {
                ViewBag.Error2 = "Chưa Chọn File";
                return View("Import_PSI_WM_REF");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "IMPORT_PSI")]
        public ActionResult Import_Assy_PSI_Shortage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    //app.Visible = true;
                    try
                    {
                        Excel.Worksheet worksheet = workbook.Worksheets["Assy PSI Shortage"];
                        worksheet.Columns.AutoFit();
                        //worksheet.Rows.AutoFit();
                        Excel.Range range = worksheet.UsedRange;
                        try
                        {
                            int temp = 0;
                            int coltemp = 57;
                            for (int row = 3; row <= range.Rows.Count; row++)
                            {
                                if (((Excel.Range)range.Cells[row, coltemp]).Text == "")
                                {
                                    continue;
                                }
                                Assy_PSI_Shortage assy_psi_shortage = new Assy_PSI_Shortage();
                                int row2 = 2;
                                for (int col = 52; col <= range.Columns.Count; col++)
                                {
                                    if (IsDate(((Excel.Range)range.Cells[row2, col]).Text) == false)
                                    {
                                        continue;
                                    }
                                    if (row == 3)
                                    {
                                        string code_check = ((Excel.Range)range.Cells[row, coltemp]).Text;
                                        string date_str = ((Excel.Range)range.Cells[row2, col]).Text;
                                        DateTime date_check = Convert.ToDateTime(date_str);
                                        var kq = db.Assy_PSI_Shortage.Where(x => x.Date == date_check && x.Code == code_check).FirstOrDefault();
                                        if (kq != null)
                                        {
                                            kq.Code = (((Excel.Range)range.Cells[row, coltemp]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            kq.Date = Convert.ToDateTime(date);
                                            int t1 = row2 + 1;
                                            string t3 = ((Excel.Range)range.Cells[t1, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                kq.Requirement = 0;
                                            }
                                            else
                                            {
                                                kq.Requirement = Convert.ToInt32(t3);
                                            }
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            assy_psi_shortage.Code = (((Excel.Range)range.Cells[row, coltemp]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            assy_psi_shortage.Date = Convert.ToDateTime(date);
                                            int t1 = row2 + 1;
                                            string t3 = ((Excel.Range)range.Cells[t1, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                assy_psi_shortage.Requirement = 0;
                                            }
                                            else
                                            {
                                                assy_psi_shortage.Requirement = Convert.ToInt32(t3);
                                            }
                                            db.Assy_PSI_Shortage.Add(assy_psi_shortage);
                                            db.SaveChanges();
                                        }
                                    }
                                    if (row > 3)
                                    {
                                        string code_check = ((Excel.Range)range.Cells[row, coltemp]).Text;
                                        string date_str = ((Excel.Range)range.Cells[row2, col]).Text;
                                        DateTime date_check = Convert.ToDateTime(date_str);
                                        var kq = db.Assy_PSI_Shortage.Where(x => x.Date == date_check && x.Code == code_check).FirstOrDefault();
                                        if (kq != null)
                                        {
                                            kq.Code = (((Excel.Range)range.Cells[row, coltemp]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            kq.Date = Convert.ToDateTime(date);
                                            int t2 = temp + 3;
                                            string t3 = ((Excel.Range)range.Cells[t2, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                kq.Requirement = 0;
                                            }
                                            else
                                            {
                                                kq.Requirement = Convert.ToInt32(t3);
                                            }
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            assy_psi_shortage.Code = (((Excel.Range)range.Cells[row, coltemp]).Text);
                                            string date = ((Excel.Range)range.Cells[row2, col]).Text;
                                            assy_psi_shortage.Date = Convert.ToDateTime(date);
                                            int t2 = temp + 3;
                                            string t3 = ((Excel.Range)range.Cells[t2, col]).Text;
                                            string replace_number = t3.Replace(",", "");
                                            if (replace_number == "#NAME!" || replace_number == "#N/A" || replace_number == "#VALUE!" || replace_number == "#REF!" || replace_number == "#NUM!" || replace_number == "#NULL" || replace_number == "#DIV/0!" || replace_number == "")
                                            {
                                                assy_psi_shortage.Requirement = 0;
                                            }
                                            else
                                            {
                                                assy_psi_shortage.Requirement = Convert.ToInt32(t3);
                                            }

                                            db.Assy_PSI_Shortage.Add(assy_psi_shortage);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                temp += 1;
                            }
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return RedirectToAction("Import_Succeeded");
                        }
                        catch
                        {
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return RedirectToAction("NotFound");
                        }
                    }
                    catch
                    {
                        workbook.Close(0);
                        app.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                        System.IO.File.Delete(path);
                        ViewBag.ErrorWorksheet3 = "Không tìm thấy sheet Assy PSI Shortage!";
                        return View("Import_PSI_WM_REF");
                    }
                }
                else
                {
                    ViewBag.Error3 = "File Excel không đúng";
                    return View("Import_PSI_WM_REF");
                }
            }
            else
            {
                ViewBag.Error3 = "Chưa Chọn File";
                return View("Import_PSI_WM_REF");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "IMPORT_STOCK")]
        public ActionResult Import_Stock(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    //app.Visible = true;
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    try
                    {
                        Excel.Worksheet worksheet = workbook.Worksheets["Stock"];
                        worksheet.Columns.AutoFit();
                        //worksheet.Rows.AutoFit();
                        Excel.Range range = worksheet.UsedRange;
                        try
                        {
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                if (((Excel.Range)range.Cells[row, 1]).Text == "")
                                {
                                    continue;
                                }
                                Stock stock = new Stock();
                                stock.Code = (((Excel.Range)range.Cells[row, 1]).Text);
                                string str_stock = ((Excel.Range)range.Cells[row, 2]).Text;
                                string replace_stock = str_stock.Replace(".", "");
                                string replace_stock1 = replace_stock.Replace(",", "");
                                stock.Stock1 = Convert.ToInt32(replace_stock1);
                                db.Stocks.Add(stock);
                                db.SaveChanges();
                            }
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return View("Import_Succeeded");
                        }
                        catch
                        {
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return View("NotFound");
                        }
                    }
                    catch
                    {
                        workbook.Close(0);
                        app.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                        System.IO.File.Delete(path);
                        ViewBag.ErrorWorksheet4 = "Không tìm thấy sheet Stock!";
                        return View("Import_PSI_WM_REF");
                    }
                }
                else
                {
                    ViewBag.Error4 = "File Excel không đúng";
                    return View("Import_PSI_WM_REF");
                }
            }
            else
            {
                ViewBag.Error4 = "Chưa Chọn File";
                return View("Import_PSI_WM_REF");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "IMPORT_RESINSTOCK")]
        public ActionResult Import_ResinStock(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    //app.Visible = true;
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    try
                    {
                        Excel.Worksheet worksheet = workbook.Worksheets["Resin Stock"];
                        worksheet.Columns.AutoFit();
                        //worksheet.Rows.AutoFit();
                        Excel.Range range = worksheet.UsedRange;
                        try
                        {
                            for (int row = 2; row <= range.Rows.Count; row++)
                            {
                                if (((Excel.Range)range.Cells[row, 1]).Text == "")
                                {
                                    continue;
                                }
                                ResinStock stock = new ResinStock();
                                stock.Code = (((Excel.Range)range.Cells[row, 1]).Text);
                                string str_stock = ((Excel.Range)range.Cells[row, 2]).Text;
                                string replace_stock = str_stock.Replace(".", "");
                                string replace_stock1 = replace_stock.Replace(",", "");
                                stock.ResinStock1 = Convert.ToInt32(replace_stock1);
                                db.ResinStocks.Add(stock);
                                db.SaveChanges();
                            }
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return View("Import_Succeeded");
                        }
                        catch
                        {
                            workbook.Close(0);
                            app.Quit();
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                            System.IO.File.Delete(path);
                            return View("NotFound");
                        }
                    }
                    catch
                    {
                        workbook.Close(0);
                        app.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                        System.IO.File.Delete(path);
                        ViewBag.ErrorWorksheet5 = "Không tìm thấy sheet Resin Stock!";
                        return View("Import_PSI_WM_REF");
                    }
                }
                else
                {
                    ViewBag.Error5 = "File Excel không đúng";
                    return View("Import_PSI_WM_REF");
                }
            }
            else
            {
                ViewBag.Error5 = "Chưa Chọn File";
                return View("Import_PSI_WM_REF");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import_Production_Result(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(file.FileName);
                    file.SaveAs(path);
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets[5];
                    Excel.Range range = worksheet.UsedRange;
                    for (int row = 4; row <= range.Rows.Count; row++)
                    {
                        Production_Result production_result = new Production_Result();
                        production_result.Date = (((Excel.Range)range.Cells[row, 2]).Text);
                        production_result.Code = (((Excel.Range)range.Cells[row, 3]).Text);
                        production_result.Shift1_G_ = ((Excel.Range)range.Cells[row, 4]).Text;
                        production_result.Shift2_G_ = ((Excel.Range)range.Cells[row, 5]).Text;
                        production_result.Shift1_NG_ = ((Excel.Range)range.Cells[row, 6]).Text;
                        production_result.Shift1_NG_ = ((Excel.Range)range.Cells[row, 7]).Text;
                        db.Production_Result.Add(production_result);
                        db.SaveChanges();
                    }
                    return View();
                }
                else
                {
                    ViewBag.Error = "File Excel không đúng";
                    return RedirectToAction("Import_Production_Result");
                }
            }
            else
            {
                ViewBag.Error = "Chưa Chọn File";
                return RedirectToAction("Import_Production_Result");
            }
        }

        public bool IsDate(string pText)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(pText);
                if (/*(dt.Month != System.DateTime.Now.Month) ||*/ dt.Day < 1 && dt.Day > 31 /*|| dt.Year != System.DateTime.Now.Year*/)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_STOCK")]
        public ActionResult Edit_Stock(string Code)
        {
            var stock = db.Stocks.Where(x => x.Code == Code).FirstOrDefault();
            ViewBag.Stock = stock;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_STOCK")]
        public ActionResult Edit_Stock(Stock md_stock)
        {
                if (!ModelState.IsValid)
                {
                    return View(md_stock);
                }      
                var stock = db.Stocks.Where(x => x.Code == md_stock.Code).FirstOrDefault();
                ViewBag.Stock = stock;
                stock.Stock1 = md_stock.Stock1;
                db.SaveChanges();
                return RedirectToAction("ViewStock");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_RESINSTOCK")]
        public ActionResult Edit_ResinStock(string Code)
        {
            var stock = db.ResinStocks.Where(x => x.Code == Code).FirstOrDefault();
            ViewBag.Stock = stock;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_RESINSTOCK")]
        public ActionResult Edit_ResinStock(ResinStock md_stock)
        {
            if (!ModelState.IsValid)
            {
                return View(md_stock);
            }
            var stock = db.ResinStocks.Where(x => x.Code == md_stock.Code).FirstOrDefault();
            ViewBag.Stock = stock;
            stock.ResinStock1 = md_stock.ResinStock1;
            db.SaveChanges();
            return RedirectToAction("ResinViewStock");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "ADD_STOCK")]
        public ActionResult AddStock()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "ADD_STOCK")]
        public ActionResult AddStock(Stock stock)
        {
            if(!ModelState.IsValid)
            {
                return View(stock);
            }
            Stock db_stock = new Stock();
            db_stock.Code = stock.Code;
            db_stock.Stock1 = stock.Stock1;
            db.Stocks.Add(db_stock);
            db.SaveChanges();
            return RedirectToAction("ViewStock");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "ADD_RESINSTOCK")]
        public ActionResult AddResinStock()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "ADD_RESINSTOCK")]
        public ActionResult AddResinStock(ResinStock stock)
        {
            if (!ModelState.IsValid)
            {
                return View(stock);
            }
            ResinStock db_stock = new ResinStock();
            db_stock.Code = stock.Code;
            db_stock.ResinStock1 = stock.ResinStock1;
            db.ResinStocks.Add(db_stock);
            db.SaveChanges();
            return RedirectToAction("ResinViewStock");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_PICKLISTVATTU")]
        public ActionResult Edit_VatTu(string Code)
        {
            var vattu = db.PicklistVatTus.Where(x => x.Injection_Code == Code).FirstOrDefault();
            ViewBag.VatTu = vattu;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_PICKLISTVATTU")]
        public ActionResult Edit_VatTu(PicklistVatTu md_vattu)
        {
            var vattu = db.PicklistVatTus.Where(x => x.Injection_Code == md_vattu.Injection_Code).FirstOrDefault();
            vattu.Machine_No_ = md_vattu.Machine_No_;
            vattu.Machine = md_vattu.Machine;
            vattu.Cycle_Time = md_vattu.Cycle_Time;
            vattu.Cavity = md_vattu.Cavity;
            vattu.Manpower = md_vattu.Manpower;
            vattu.Resin = md_vattu.Resin;
            vattu.Resin_Code = md_vattu.Resin_Code;
            vattu.Weight = md_vattu.Weight;
            vattu.Vat_Tu_Code = md_vattu.Vat_Tu_Code;
            vattu.Ten_Vat_Tu = md_vattu.Ten_Vat_Tu;
            vattu.Ty_Le = md_vattu.Ty_Le;
            vattu.Another_Code = md_vattu.Another_Code;
            db.SaveChanges();
            return RedirectToAction("View_VatTu");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_CODELIST")]
        public ActionResult Edit_Codelist(string Code)
        {
            var codelist = db.CodeLists.Where(x => x.Injection_Code == Code).FirstOrDefault();
            ViewBag.Codelist = codelist;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_CODELIST")]
        public ActionResult Edit_Codelist(CodeList md_codelist)
        {
            var codelist = db.CodeLists.Where(x => x.Injection_Code == md_codelist.Injection_Code).FirstOrDefault();
            codelist.Machine_No_ = md_codelist.Machine_No_;
            codelist.Machine = md_codelist.Machine;
            codelist.Cycle_Time = md_codelist.Cycle_Time;
            codelist.Cavity = md_codelist.Cavity;
            codelist.Manpower = md_codelist.Manpower;
            codelist.Resin = md_codelist.Resin;
            codelist.Resin_Code = md_codelist.Resin_Code;
            codelist.Weight = md_codelist.Weight;
            codelist.Another_Code = md_codelist.Another_Code;
            db.SaveChanges();
            return RedirectToAction("View_Codelist");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "ADD_CODELIST")]
        public ActionResult Add_CodeList()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "ADD_CODELIST")]
        public ActionResult Add_CodeList(CodeList md_codelist)
        {
            if(!ModelState.IsValid)
            {
                return View(md_codelist);
            }
            CodeList db_codelist = new CodeList();
            db_codelist.Injection_Code = md_codelist.Injection_Code;
            db_codelist.Machine_No_ = md_codelist.Machine_No_;
            db_codelist.Machine = md_codelist.Machine;
            db_codelist.Cycle_Time = md_codelist.Cycle_Time;
            db_codelist.Cavity = md_codelist.Cavity;
            db_codelist.Manpower = md_codelist.Manpower;
            db_codelist.Resin = md_codelist.Resin;
            db_codelist.Resin_Code = md_codelist.Resin_Code;
            db_codelist.Weight = md_codelist.Weight;
            db_codelist.Another_Code = md_codelist.Another_Code;
            db.CodeLists.Add(db_codelist);
            db.SaveChanges();
            return RedirectToAction("View_CodeList");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "ADD_PICKLISTVATTU")]
        public ActionResult Add_VatTu()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "ADD_PICKLISTVATTU")]
        public ActionResult Add_VatTu(PicklistVatTu md_codelist)
        {
            if(!ModelState.IsValid)
            {
                return View(md_codelist);
            }
            PicklistVatTu db_codelist = new PicklistVatTu();
            db_codelist.Injection_Code = md_codelist.Injection_Code;
            db_codelist.Machine_No_ = md_codelist.Machine_No_;
            db_codelist.Machine = md_codelist.Machine;
            db_codelist.Cycle_Time = md_codelist.Cycle_Time;
            db_codelist.Cavity = md_codelist.Cavity;
            db_codelist.Manpower = md_codelist.Manpower;
            db_codelist.Resin = md_codelist.Resin;
            db_codelist.Resin_Code = md_codelist.Resin_Code;
            db_codelist.Weight = md_codelist.Weight;
            db_codelist.Vat_Tu_Code = md_codelist.Vat_Tu_Code;
            db_codelist.Ten_Vat_Tu = md_codelist.Ten_Vat_Tu;
            db_codelist.Ty_Le = md_codelist.Ty_Le;
            db_codelist.Another_Code = md_codelist.Another_Code;
            db.PicklistVatTus.Add(db_codelist);
            db.SaveChanges();
            return RedirectToAction("View_VatTu");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "NHAP_KQSX")]
        public ActionResult Chose_Date_Product_Result(DateTime dateStart,DateTime dateEnd,string week)
        {
            ViewBag.Week = week;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "NHAP_KQSX")]
        public ActionResult Add_Production(DA_Injection da)
        {
            if(da != null)
            {
                var kq = db.DA_Injection.Where(x => x.Week == da.Week && x.Date == da.Date).ToList();
                ViewBag.Date = da.Date;
                ViewBag.Plan = kq;
            }
            return View();
        }

        public ActionResult View_Product(string week)
        {
            ViewBag.Week = week;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult View_Product(DA_Injection da)
        {
            if (da != null)
            {
                var date_format = String.Format("{0:yyyy-MM-dd}", da.Date);
                ViewBag.Date = date_format;
                ViewBag.Week = da.Week;
                var kq = db.DA_Injection.Where(x => x.Date == da.Date && x.Week == da.Week && (x.Good_D_ != null || x.Good_N_ != null)).ToList();
                ViewBag.Product = kq;
                return View();
            }
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "DELETE_STOCK")]
        public ActionResult Delete_Stock()
        {
            db.Delete_Stock();
            return View("ViewStock");
        }

        [HasCredentialAttribute.HasCredential(RoleID = "DELETE_RESINSTOCK")]
        public ActionResult Delete_ResinStock()
        {
            db.Delete_ResinStock();
            return View("ResinViewStock");
        }

        public ActionResult Edit_Product(string Code,DateTime Date)
        {
            var date_format = String.Format("{0:yyyy-MM-dd}", Date);
            ViewBag.Date = date_format;
            ViewBag.Code = Code;
            var kq = db.Production_Result.Where(x => x.Date == Date && x.Code == Code).FirstOrDefault();
            ViewBag.Good_S1 = kq.Shift1_G_;
            ViewBag.Good_S3 = kq.Shift2_G_;
            ViewBag.NGood_S1 = kq.Shift1_NG_;
            ViewBag.NGood_S3 = kq.Shift2_NG_;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Product(Production_Result pro)
        {
            string date_format;
            Production_Result kq = new Production_Result();
            if (!ModelState.IsValid)
            {
                date_format = String.Format("{0:yyyy-MM-dd}", pro.Date);
                ViewBag.Date = date_format;
                kq = db.Production_Result.Where(x => x.Date == pro.Date && x.Code == pro.Code).FirstOrDefault();
                kq.Date = pro.Date;
                kq.Code = pro.Code;
                kq.Shift1_G_ = pro.Shift1_G_;
                kq.Shift2_G_ = pro.Shift2_G_;
                kq.Shift1_NG_ = pro.Shift1_NG_;
                kq.Shift2_NG_ = pro.Shift2_NG_;
                ViewBag.Code = kq.Code;
                ViewBag.Good_S1 = kq.Shift1_G_;
                ViewBag.Good_S3 = kq.Shift2_G_;
                ViewBag.NGood_S1 = kq.Shift1_NG_;
                ViewBag.NGood_S3 = kq.Shift2_NG_;
                return View(pro);
            }
            date_format = String.Format("{0:yyyy-MM-dd}", pro.Date);
            ViewBag.Date = date_format;
            kq = db.Production_Result.Where(x => x.Date == pro.Date && x.Code == pro.Code).FirstOrDefault();
            kq.Date = pro.Date;
            kq.Code = pro.Code;
            kq.Shift1_G_ = pro.Shift1_G_;
            kq.Shift2_G_ = pro.Shift2_G_;
            kq.Shift1_NG_ = pro.Shift1_NG_;
            kq.Shift2_NG_ = pro.Shift2_NG_;
            ViewBag.Code = kq.Code;
            ViewBag.Good_S1 = kq.Shift1_G_;
            ViewBag.Good_S3 = kq.Shift2_G_;
            ViewBag.NGood_S1 = kq.Shift1_NG_;
            ViewBag.NGood_S3 = kq.Shift2_NG_;
            db.SaveChanges();
            ViewBag.Successed = "Đã Lưu Chỉnh Sửa";
            return View();
        }

        public List<Assy_PSI_Shortage> getAssy()
        {
            return db.Assy_PSI_Shortage.ToList();
        }

        public List<PSI_V_C> getVC()
        {
            return db.PSI_V_C.ToList();
        }

        public List<PSI_WM_REF> getRef()
        {
            return db.PSI_WM_REF.ToList();
        }

        public ActionResult View_Requirement()
        {
            ViewBag.Assy = getAssy();
            ViewBag.VC = getVC();
            ViewBag.Ref = getRef();
            return View();
        }

        [HttpPost]
        public ActionResult View_Requirement(PSI_WM_REF psi_ref)
        {
            ViewBag.AssyOfDate = db.Assy_PSI_Shortage.Where(x => x.Date == psi_ref.Date).ToList();
            ViewBag.VC_OfDate = db.PSI_V_C.Where(x => x.Date == psi_ref.Date).ToList();
            ViewBag.RefOfDate = db.PSI_WM_REF.Where(x => x.Date == psi_ref.Date).ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "LAP_KEHOACH")]
        public ActionResult View_CreateDatePlan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "LAP_KEHOACH")]
        public ActionResult View_CreateDatePlan(DA_Injection DA)
        {
            string datestart_format = String.Format("{0:yyyy-MM-dd}", DA.Date);
            ViewBag.DateStart = datestart_format;

            string dateend_format = String.Format("{0:yyyy-MM-dd}", DA.Date_End);
            ViewBag.DateEnd = dateend_format;

            ViewBag.Week = DA.Week;

            var assy = db.Assy_PSI_Shortage.Where(x => x.Date >= DA.Date && x.Date <= DA.Date_End).OrderBy(x=>x.Code).ThenBy(x => x.Date).ToList();
         
            var psi_ref = db.PSI_WM_REF.Where(x => x.Date >= DA.Date && x.Date <= DA.Date_End).OrderBy(x => x.Code).ThenBy(x => x.Date).ToList();
          
            var VC = db.PSI_V_C.Where(x => x.Date >= DA.Date && x.Date <= DA.Date_End).OrderBy(x => x.Code).ThenBy(x => x.Date).ToList();

            List<DA_Injection> list = new List<DA_Injection>();
            int j = 0;
            bool flag = false;
            foreach(var i in assy)
            {
                DA_Injection psi = new DA_Injection();
                psi.No_ = j;
                psi.Code = i.Code;
                psi.Date = i.Date;
                psi.Requirements = i.Requirement;
                psi.PSI = "assy";
                list.Add(psi);
                j++;
            }

            foreach (var i in VC)
            {
                flag = false;

                foreach(DA_Injection k in list)
                {
                    if(k.Code == i.Code && k.Date == i.Date)
                    {
                        k.Requirements += i.Requirement;
                        flag = true;
                    }
                }

                if(flag == false)
                {
                    DA_Injection psi = new DA_Injection();
                    psi.No_ = j;
                    psi.Code = i.Code;
                    psi.Date = i.Date;
                    psi.Requirements = i.Requirement;
                    psi.PSI = "vc";
                    list.Add(psi);
                    j++;
                }
                
            }



            foreach (var i in psi_ref)
            {
                flag = false;

                var require = i.Requirements;
                var balace_stock = i.Balace_Stock_;
                if (balace_stock >= 0)
                {
                    require = 0;
                }

                if (balace_stock < 0)
                {
                    balace_stock = balace_stock * -1;
                    if (require > balace_stock)
                    {
                        require = balace_stock;
                    }
                }

                foreach (DA_Injection k in list)
                {
                    if(k.Code == i.Code && k.Date == i.Date)
                    {
                        k.Requirements += require;
                        flag = true;
                    }
                }

                if(flag == false)
                {
                    DA_Injection psi = new DA_Injection();
                    psi.No_ = j;
                    psi.Code = i.Code;
                    psi.Date = i.Date;

                    psi.Requirements = require;
                    psi.PSI = "ref";
                    list.Add(psi);
                    j++;
                }
            }

            ViewBag.List = list;

            var check = db.Plan_Week.Where(x => x.DateStart == DA.Date && x.DateEnd == DA.Date_End && x.Week == DA.Week).FirstOrDefault();
            var check2 = db.Plan_Week.Where(x =>x.Week == DA.Week).FirstOrDefault();
            if (check != null)
            {
                ViewBag.Error = "Bảng kế hoạch này đã được lập!";
            }
            else if(check2 != null)
            {
                ViewBag.ErrorWeek = DA.Week;
            }
            else
            {
                Plan_Week pw = new Plan_Week();
                pw.DateStart = DA.Date;
                pw.DateEnd = DA.Date_End;
                pw.Week = DA.Week;
                db.Plan_Week.Add(pw);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        [HasCredentialAttribute.HasCredential(RoleID = "NHAP_KQSX")]
        public JsonResult Add_Product(string strdate,string code,string strgoods1, string strgoods2, string strngoods1, string strngoods2,string psi,string week,int somay)
        {
            DateTime date = Convert.ToDateTime(strdate);
            bool flag = false;
            var kq = db.DA_Injection.Where(x => x.Date == date && x.Code == code && x.PSI == psi && x.Week == week && x.So_May == somay).FirstOrDefault();
            if(kq != null)
            {
                if(strgoods1 != "")
                {
                    int goods1 = Convert.ToInt32(strgoods1);
                    if (kq.Plan_D_ > goods1 && goods1 >= 0)
                    {
                        //Them vao bang KetQuaSanXuatThieu
                        var rel = db.KetQuaSanXuatThieux.FirstOrDefault(x => x.Code == code && x.Week == week && x.Date == date && x.SoMay == somay && x.PSI == psi);
                        if (rel != null)
                        {
                            if (rel.FlagDem == false)
                            {
                                rel.SoThieuCaDem = kq.Plan_D_ - goods1;
                                kq.Good_D_ = goods1;
                                flag = true;
                            }
                        }
                        else
                        {
                            kq.Good_D_ = goods1;
                            KetQuaSanXuatThieu k = new KetQuaSanXuatThieu();
                            k.Code = code;
                            k.Date = date;
                            k.SoThieuCaDem = kq.Plan_D_ - goods1;
                            k.SoMay = kq.So_May;
                            k.PSI = psi;
                            k.Week = week;
                            k.FlagDem = false;
                            k.FlagNgay = false;
                            db.KetQuaSanXuatThieux.Add(k);
                            db.SaveChanges();
                            flag = true;
                        }
                    }

                    if(goods1 == kq.Plan_D_)
                    {
                        var rel = db.KetQuaSanXuatThieux.FirstOrDefault(x => x.Code == code && x.Week == week && x.Date == date && x.SoMay == somay && x.PSI == psi);
                        if(rel != null)
                        {
                            if (rel.FlagDem == false)
                            {
                                rel.SoThieuCaDem = 0;
                                kq.Good_D_ = goods1;
                                flag = true;
                            }
                        }
                        else
                        {
                            kq.Good_D_ = goods1;
                            KetQuaSanXuatThieu k = new KetQuaSanXuatThieu();
                            k.Code = code;
                            k.Date = date;
                            k.SoThieuCaDem = 0;
                            k.SoMay = kq.So_May;
                            k.PSI = psi;
                            k.Week = week;
                            k.FlagDem = false;
                            k.FlagNgay = false;
                            db.KetQuaSanXuatThieux.Add(k);
                            db.SaveChanges();
                            flag = true;
                        }
                    }
                }
                
                if(strgoods2 != "")
                {
                    int goods2 = Convert.ToInt32(strgoods2);
                    if (kq.Plan_N_ > goods2 && goods2 >= 0)
                    {
                        //Them vao bang KetQuaSanXuatThieu
                        var rel = db.KetQuaSanXuatThieux.FirstOrDefault(x => x.Code == code && x.Week == week && x.Date == date && x.SoMay == somay && x.PSI == psi);
                        if (rel != null)
                        {
                            if (rel.FlagNgay == false)
                            {
                                rel.SoThieuCaNgay = kq.Plan_N_ - goods2;
                                kq.Good_N_ = goods2;
                                flag = true;
                            }
                        }
                        else
                        {
                            kq.Good_N_ = goods2;
                            KetQuaSanXuatThieu k = new KetQuaSanXuatThieu();
                            k.Code = code;
                            k.Date = date;
                            k.SoThieuCaNgay = kq.Plan_N_ - goods2;
                            k.SoMay = kq.So_May;
                            k.Week = week;
                            k.PSI = psi;
                            k.FlagDem = false;
                            k.FlagNgay = false;
                            db.KetQuaSanXuatThieux.Add(k);
                            db.SaveChanges();
                            flag = true;
                        }
                    }

                    if (goods2 == kq.Plan_N_)
                    {
                        var rel = db.KetQuaSanXuatThieux.FirstOrDefault(x => x.Code == code && x.Week == week && x.Date == date && x.SoMay == somay && x.PSI == psi);
                        if (rel != null)
                        {
                            if (rel.FlagNgay == false)
                            {
                                rel.SoThieuCaNgay = 0;
                                kq.Good_N_ = goods2;
                                flag = true;
                            }
                        }
                        else
                        {
                            kq.Good_N_ = goods2;
                            KetQuaSanXuatThieu k = new KetQuaSanXuatThieu();
                            k.Code = code;
                            k.Date = date;
                            k.SoThieuCaNgay = 0;
                            k.SoMay = kq.So_May;
                            k.PSI = psi;
                            k.Week = week;
                            k.FlagDem = false;
                            k.FlagNgay = false;
                            db.KetQuaSanXuatThieux.Add(k);
                            db.SaveChanges();
                            flag = true;
                        }
                    }
                }
               
                if(strngoods1 != "")
                {
                    int ngoods1 = Convert.ToInt32(strngoods1);
                    kq.NG_D_ = ngoods1;
                    flag = true;
                }
                
                if(strngoods2 != "")
                {
                    int ngoods2 = Convert.ToInt32(strngoods2);
                    kq.NG_N_ = ngoods2;
                    flag = true;
                }
               
                db.SaveChanges();
            }
            return Json(flag,JsonRequestBehavior.AllowGet);
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_PlanWeek()
        {
            ViewBag.PlanWeek = db.Plan_Week.OrderByDescending(x => x.STT).ToList();
            return View();
        }

        public List<GetViewPlan_Result> GetViewPlan(string week)
        {
            return db.GetViewPlan(week, "false").ToList();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_Plan(DateTime dateStart,DateTime dateEnd,string week)
        {
            ViewBag.DateStart = dateStart;
            ViewBag.DateEnd = dateEnd;
            ViewBag.Week = week;
            ViewBag.Plan = db.DA_Injection.Where(x => x.Week == week && (x.Plan_Time_D_ > 0 || x.C_Plan_Time_N_ > 0)).OrderBy(x => x.So_May).ThenBy(x => x.STT).ToList();
            var kq = db.KetQuaSanXuatThieux.Where(x => x.Week == week && (x.SoThieuCaDem > 0 && x.FlagDem == false || x.SoThieuCaNgay > 0 && x.FlagNgay == false)).ToList();
            ViewBag.KQSXThieu = kq;
            return View();
        }

        [HttpGet]
        public JsonResult UpdateDA(string code, string datemoi, string id_plantime, string week,int plan_time,int plan,int khbs)
        {
            DateTime ngay = Convert.ToDateTime(datemoi);
            var flag = 0;
            var kq = db.DA_Injection.Where(x => x.Code == code && x.Date == ngay && x.Week == week && x.ID_PlanTimeDem == id_plantime).FirstOrDefault();
            if(kq != null)
            {
                kq.Plan_Time_D_ = plan_time;
                kq.Plan_D_ = plan;
                kq.Ke_Hoach_Bo_Sung = khbs;
                db.SaveChanges();
                flag = 1;
            }

            var result = db.DA_Injection.Where(x => x.Code == code && x.Date == ngay && x.Week == week && x.ID_PlanTimeNgay == id_plantime).FirstOrDefault();
            if(result != null)
            {
                result.C_Plan_Time_N_ = plan_time;
                result.Plan_N_ = plan;
                result.Ke_Hoach_Bo_Sung = khbs;
                db.SaveChanges();
                flag = 1;
            }
            return Json(flag, JsonRequestBehavior.AllowGet);
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_PlanWeek_Picklist()
        {
            ViewBag.PlanWeek = db.Plan_Week.OrderByDescending(x => x.STT).ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_PickList(string week,string dateStart,string dateEnd)
        {
            DateTime datestart = Convert.ToDateTime(dateStart);
            DateTime dateend = Convert.ToDateTime(dateEnd);
            ViewBag.DateStart = datestart;
            ViewBag.DateEnd = dateend;
            ViewBag.Week = week;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "VIEW_USER")]
        public ActionResult View_PickList(DA_Injection da)
        {
            ViewBag.Week = da.Week;
            var date_format = String.Format("{0:yyyy-MM-dd}", da.Date);
            ViewBag.Date = date_format;
            ViewBag.DA = db.DA_Injection.Where(x => x.Week == da.Week && x.Date == da.Date).ToList();
            return View();
        }

        [HasCredentialAttribute.HasCredential(RoleID = "XUAT_EXCEL")]
        public void ExportPlanDaily(DA_Injection da)
        {
            if(da.Date != null && da.Week != null)
            {
                string strdate = Convert.ToDateTime(da.Date).ToShortDateString();
                strdate = strdate.Replace("/", "-");
                string week = da.Week;
                var list = db.DA_Injection.Where(x => x.Week == week && (x.Plan_Time_D_ > 0 || x.C_Plan_Time_N_ > 0) && x.Date == da.Date).OrderBy(x => x.So_May).ThenBy(x => x.STT).ToList();
                if(list.Count > 0)
                {
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    worksheet.Cells[1, 1] = "Kế hoạch sản xuất ngày " + strdate;
                    worksheet.Cells[2, 1] = "Ngày";
                    worksheet.Cells[2, 2] = "Code";
                    worksheet.Cells[2, 3] = "Số Máy";
                    worksheet.Cells[2, 4] = "Máy";
                    worksheet.Cells[2, 5] = "Kế Hoạch/Plan";
                    worksheet.Cells[2, 6] = "Kế Hoạch Bổ Sung";
                    worksheet.Cells[2, 7] = "Manpower";
                    worksheet.Cells[2, 8] = "Stock";
                    worksheet.Cells[2, 9] = "Cycle Time";
                    worksheet.Cells[2, 10] = "Cavity";
                    worksheet.Cells[2, 11] = "Capa/Sheep";
                    worksheet.Cells[2, 12] = "Plan Time(Day)";
                    worksheet.Cells[2, 13] = "Plan(Day)";
                    worksheet.Cells[2, 14] = "Plan Time(Night)";
                    worksheet.Cells[2, 15] = "Plan(Night)";

                    int row = 3;
                    foreach (var k in list)
                    {
                        Range formatRange;

                        worksheet.Cells[row, 1] = strdate;
                        worksheet.Cells[row, 2] = k.Code;

                        worksheet.Cells[row, 3] = k.So_May;
                        formatRange = worksheet.Cells[row, 3];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);

                        worksheet.Cells[row, 4] = k.Trong_Luong_May;

                        worksheet.Cells[row, 5] = k.Ke_Hoach;
                        formatRange = worksheet.Cells[row, 5];
                        formatRange.NumberFormat = "#,##0";

                        worksheet.Cells[row, 6] = k.Ke_Hoach_Bo_Sung;
                        formatRange = worksheet.Cells[row, 6];
                        formatRange.NumberFormat = "#,##0";

                        worksheet.Cells[row, 7] = k.Manpower;

                        worksheet.Cells[row, 8] = k.Stock;
                        formatRange = worksheet.Cells[row, 8];
                        formatRange.NumberFormat = "#,##0";

                        worksheet.Cells[row, 9] = k.Cycle_time;
                        worksheet.Cells[row, 10] = k.Cavity;
                        worksheet.Cells[row, 11] = k.Capa;

                        double minute = (double)k.Plan_Time_D_ - Math.Floor(Convert.ToDouble(k.Plan_Time_D_));
                        minute = Math.Floor(minute * 60);
                        string time = Math.Floor(Convert.ToDouble(k.Plan_Time_D_)) + "h " + minute + "m"; 

                        worksheet.Cells[row, 12] = time;
                        formatRange = worksheet.Cells[row, 12];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                        worksheet.Cells[row, 13] = k.Plan_D_;
                        formatRange = worksheet.Cells[row, 13];
                        formatRange.NumberFormat = "#,##0";
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                        minute = (double)k.C_Plan_Time_N_ - Math.Floor(Convert.ToDouble(k.C_Plan_Time_N_));
                        minute = Math.Floor(minute * 60);
                        time = Math.Floor(Convert.ToDouble(k.C_Plan_Time_N_)) + "h " + minute + "m";

                        worksheet.Cells[row, 14] = time;
                        formatRange = worksheet.Cells[row, 14];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                        worksheet.Cells[row, 15] = k.Plan_N_;
                        formatRange = worksheet.Cells[row, 15];
                        formatRange.NumberFormat = "#,##0";
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                        row++;
                    }

                    Range usedRange = worksheet.Range["A1", "O1"];
                    usedRange.Merge(false);
                    usedRange.Font.Bold = true;
                    usedRange.Font.Size = 16;

                    usedRange = worksheet.Range["A2", "O2"];
                    usedRange.Font.Bold = true;
                    usedRange.Font.Size = 13;

                    usedRange = worksheet.UsedRange;
                    usedRange.VerticalAlignment = 3;
                    usedRange.HorizontalAlignment = 3;

                    Excel.Range cell = worksheet.UsedRange;
                    Excel.Borders border = cell.Borders;
                    border.LineStyle = Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;

                    worksheet.Columns.AutoFit();
                    worksheet.Rows.AutoFit();

                    string FileName = "Kế hoạch sản xuất ngày " + strdate + ".xlsx";
                    string FolderPath = HttpContext.Server.MapPath("/Content/Files");

                    string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(FileName);
                    workbook.SaveAs(path);

                    workbook.Close(0);
                    application.Quit();
                    Marshal.FinalReleaseComObject(worksheet);
                    Marshal.FinalReleaseComObject(workbook);
                    Marshal.FinalReleaseComObject(application);

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FileName));
                    Response.WriteFile(path);
                    Response.End();
                    Response.Flush();
                    System.IO.File.Delete(path);
                }
            }
        }

        [HasCredentialAttribute.HasCredential(RoleID = "XUAT_EXCEL")]
        public void ExportResinCodeNull(string week)
        {
            List<SummaryByProduct> listtemp = new List<SummaryByProduct>();
            var tam = db.SummaryByProducts.Where(x => x.Week == week).ToList();
            foreach (var k in tam)
            {
                bool flag = false;
                foreach (var p in listtemp)
                {
                    if (p.ResinCode == k.ResinCode)
                    {
                        flag = true;
                    }
                }

                if (flag == false)
                {
                    listtemp.Add(k);
                }
            }
            ViewBag.CodeList = listtemp;
            List<string> listcodenull = new List<string>();
            foreach (var k in listtemp)
            {
                var stock = db.ResinStocks.FirstOrDefault(x => x.Code == k.ResinCode);
                if (stock == null)
                {
                    listcodenull.Add(k.ResinCode);
                }
            }
            ViewBag.CodeNull = listcodenull;

            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Cells[1, 1] = "Danh sách resin code sai";
            worksheet.Cells[2, 1] = "Code";
            worksheet.Cells[2, 2] = "Resin Code";

            int row = 3;
            foreach(var k in listcodenull)
            {
                string resincode = k;
                var kq = db.CodeLists.Where(x => x.Resin_Code == resincode).ToList();
                if(kq.Count > 0)
                {
                    foreach(var p in kq)
                    {
                        worksheet.Cells[row, 1] = p.Injection_Code;
                        worksheet.Cells[row, 2] = p.Resin_Code;
                    }
                }
                row++;
            }
            
            Range usedRange = worksheet.Range["A1", "C1"];
            usedRange.Merge(false);
            usedRange.Font.Bold = true;
            usedRange.Font.Size = 16;

            usedRange = worksheet.Range["A2", "B2"];
            usedRange.Font.Bold = true;
            usedRange.Font.Size = 13;

            usedRange = worksheet.UsedRange;
            usedRange.VerticalAlignment = 3;
            usedRange.HorizontalAlignment = 3;

            usedRange = worksheet.Range["A1", "B1"];
            usedRange.HorizontalAlignment = HorizontalAlign.Left;

            Excel.Range cell = worksheet.UsedRange;
            Excel.Borders border = cell.Borders;
            border.LineStyle = Excel.XlLineStyle.xlContinuous;
            border.Weight = 2d;

            worksheet.Columns.AutoFit();
            worksheet.Rows.AutoFit();

            string FileName = "Danh sách resin code sai.xlsx";
            string FolderPath = HttpContext.Server.MapPath("/Content/Files");

            string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(FileName);
            workbook.SaveAs(path);

            workbook.Close(0);
            application.Quit();
            Marshal.FinalReleaseComObject(worksheet);
            Marshal.FinalReleaseComObject(workbook);
            Marshal.FinalReleaseComObject(application);

            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FileName));
            Response.WriteFile(path);
            Response.End();
            Response.Flush();
            System.IO.File.Delete(path);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "XUAT_EXCEL")]
        public void ExportExcel(DA_Injection da)
        {
            if(da.Date != null)
            {
                DateTime date = Convert.ToDateTime(da.Date);
                var list = db.DA_Injection.Where(x => x.Week == da.Week && x.Date == date).ToList();
                DateTime datetemp = date;
                string shortdate = datetemp.ToShortDateString();

                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Cells[1, 1] = "Picklist " + shortdate;
                worksheet.Cells[2, 1] = "Date";
                worksheet.Cells[2, 2] = "Code";
                worksheet.Cells[2, 3] = "Machine";
                worksheet.Cells[2, 4] = "Material Code";
                worksheet.Cells[2, 5] = "Type";
                worksheet.Cells[2, 6] = "Description";
                worksheet.Cells[2, 7] = "Rate";
                worksheet.Cells[2, 8] = "Net Weight (g)";
                worksheet.Cells[2, 9] = "Plan Quantity (pcs)";
                worksheet.Cells[2, 10] = "Plan Resin (g)";

                int row = 3;
                foreach (DA_Injection i in list)
                {
                    var code = i.Code;
                    DateTime datetam = Convert.ToDateTime(i.Date);
                    int sum = 0;
                    int index = 0;
                    var kq = db.CodeLists.Where(x => x.Injection_Code == code || x.Another_Code == code).FirstOrDefault();
                    if (kq != null)
                    {
                        index = kq.Resin_Code.IndexOf("\n");

                        worksheet.Cells[row, 1] = da.Date;
                        Range formatRange = worksheet.Cells[row, 1];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                        worksheet.Cells[row, 2] = i.Code;
                        formatRange = worksheet.Cells[row, 2];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                       
                        worksheet.Cells[row, 3] = kq.Machine_No_;
                        formatRange = worksheet.Cells[row, 3];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                        worksheet.Cells[row, 4] = kq.Resin_Code;
                        formatRange = worksheet.Cells[row, 4];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                        worksheet.Cells[row, 5] = "Resin";
                        formatRange = worksheet.Cells[row, 5];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                        worksheet.Cells[row, 6] = kq.Resin;
                        formatRange = worksheet.Cells[row, 6];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

                        string strtemp = "";
                        if (index != -1)
                        {
                            strtemp = "0,96";
                        }
                        else
                        {
                            strtemp = "1";
                        }
                        worksheet.Cells[row, 7] = strtemp;
                        formatRange = worksheet.Cells[row, 7];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        

                        worksheet.Cells[row, 8] = kq.Weight;
                        formatRange = worksheet.Cells[row, 8];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                       

                        if (i.Plan_D_ != null)
                        {
                            sum += (int)i.Plan_D_;
                        }
                        if (i.Plan_N_ != null)
                        {
                            sum += (int)i.Plan_N_;
                        }
                        worksheet.Cells[row, 9] = sum;
                        formatRange = worksheet.Cells[row, 9];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        formatRange = worksheet.Cells[row, 9];
                        formatRange.NumberFormat = "#,##0";

                        index = kq.Resin_Code.IndexOf("\n");
                        double planresin = 0;
                        if (index != -1)
                        {
                            planresin = sum * Convert.ToDouble(kq.Weight) * 0.96;
                        }
                        else
                        {
                            planresin = sum * Convert.ToDouble(kq.Weight) * 1;
                        }
                        planresin = Math.Ceiling(planresin);
                        worksheet.Cells[row, 10] = planresin;
                        formatRange = worksheet.Cells[row, 10];
                        formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        formatRange = worksheet.Cells[row, 10];
                        formatRange.NumberFormat = "#,##0";

                        row++;

                        //Tách code trộn
                        if (index != -1)
                        {
                            string[] arrListStr = kq.Resin_Code.Split('\n');
                            worksheet.Cells[row, 1] = da.Date;
                            formatRange = worksheet.Cells[row, 1];
                            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                            worksheet.Cells[row, 2] = i.Code;
                            formatRange = worksheet.Cells[row, 2];
                            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                            worksheet.Cells[row, 3] = kq.Machine_No_;
                            formatRange = worksheet.Cells[row, 3];
                            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                            worksheet.Cells[row, 4] = arrListStr[1];
                            worksheet.Cells[row, 5] = "Mix Resin";
                            worksheet.Cells[row, 6] = "";
                            worksheet.Cells[row, 7] = "0,04";
                            worksheet.Cells[row, 8] = kq.Weight;
                            worksheet.Cells[row, 9] = sum;
                            formatRange = worksheet.Cells[row, 9];
                            formatRange.NumberFormat = "#,##0";

                            double temp = 0.04;
                            planresin = sum * Convert.ToDouble(kq.Weight) * temp;
                            planresin = Math.Ceiling(planresin);
                            worksheet.Cells[row, 10] = planresin;
                            formatRange = worksheet.Cells[row, 10];
                            formatRange.NumberFormat = "#,##0";
                            row++;
                        }

                        //lay ben picklist
                        var rel = db.PicklistVatTus.Where(x => x.Injection_Code == code || x.Another_Code == code).ToList();
                        if (rel.Count > 0)
                        {
                            foreach (var k in rel)
                            {
                                k.Ty_Le = k.Ty_Le.Replace(".", ",");
                                worksheet.Cells[row, 1] = da.Date;
                                formatRange = worksheet.Cells[row, 1];
                                formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                                worksheet.Cells[row, 2] = i.Code;
                                formatRange = worksheet.Cells[row, 2];
                                formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                                worksheet.Cells[row, 3] = kq.Machine_No_;
                                formatRange = worksheet.Cells[row, 3];
                                formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                                worksheet.Cells[row, 4] = k.Vat_Tu_Code;
                                worksheet.Cells[row, 5] = "Material";
                                worksheet.Cells[row, 6] = k.Ten_Vat_Tu;
                                worksheet.Cells[row, 7] = k.Ty_Le;
                                worksheet.Cells[row, 8] = "";
                                worksheet.Cells[row, 9] = sum;
                                formatRange = worksheet.Cells[row, 9];
                                formatRange.NumberFormat = "#,##0";

                                if (k.Ty_Le != "")
                                {
                                    index = k.Ty_Le.IndexOf(" ");
                                    if (index != -1)
                                    {
                                        string[] arrListStr = k.Ty_Le.Split(' ');
                                        double tyle = Convert.ToDouble(arrListStr[0]);
                                        planresin = sum * tyle;
                                        planresin = Math.Ceiling(planresin);
                                        worksheet.Cells[row, 10] = planresin;
                                    }
                                    else
                                    {
                                        double tyle = Convert.ToDouble(k.Ty_Le);
                                        planresin = sum * tyle;
                                        planresin = Math.Ceiling(planresin);
                                        worksheet.Cells[row, 10] = planresin;
                                        formatRange = worksheet.Cells[row, 10];
                                        formatRange.NumberFormat = "#,##0";
                                    }
                                }
                                else
                                {
                                    worksheet.Cells[row, 10] = "";
                                }
                                row++;
                            }
                        }
                    }
                }

                Range usedRange = worksheet.Range["A2", "J2"];
                usedRange.Font.Bold = true;
                usedRange.Font.Size = 13;
                usedRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                usedRange = worksheet.Range["A1", "J1"];
                usedRange.Merge(false);

                usedRange = worksheet.Range["A1", "B1"];
                usedRange.Font.Bold = true;
                usedRange.Font.Size = 16;

                usedRange = worksheet.UsedRange;
                usedRange.VerticalAlignment = VerticalAlign.Middle;
                usedRange.HorizontalAlignment = HorizontalAlign.Center;

                usedRange = worksheet.Range["A2", "J2"];
                usedRange.VerticalAlignment = 3;
                usedRange.HorizontalAlignment = 3;

                Excel.Range cell = worksheet.UsedRange;
                Excel.Borders border = cell.Borders;
                border.LineStyle = Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;

                worksheet.Columns.AutoFit();
                worksheet.Rows.AutoFit();

                shortdate = shortdate.Replace("/", "-");
                string FileName = "Picklist_(" + shortdate + ").xlsx";
                //string FileName = "picklist.xlsx";
                string FolderPath = HttpContext.Server.MapPath("/Content/Files");

                string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(FileName);
                workbook.SaveAs(path);

                workbook.Close(0);
                application.Quit();
                Marshal.FinalReleaseComObject(worksheet);
                Marshal.FinalReleaseComObject(workbook);
                Marshal.FinalReleaseComObject(application);

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FileName));
                Response.WriteFile(path);
                Response.End();
                Response.Flush();
                System.IO.File.Delete(path);
            }
        }

        [HasCredentialAttribute.HasCredential(RoleID = "XUAT_EXCEL")]
        public void ExportSummary(string week, string strdateStart, string strdateEnd)
        {
            if(week != "")
            {
                DateTime dateStart = Convert.ToDateTime(strdateStart);
                DateTime dateEnd = Convert.ToDateTime(strdateEnd);
                ViewBag.Week = week;
                ViewBag.dateStart = dateStart;
                ViewBag.dateEnd = dateEnd;

                List<SummaryByProduct> listtemp = new List<SummaryByProduct>();
                var tam = db.SummaryByProducts.Where(x => x.Week == week).ToList();
                foreach (var k in tam)
                {
                    bool flag = false;
                    foreach (var p in listtemp)
                    {
                        if (p.ResinCode == k.ResinCode)
                        {
                            flag = true;
                        }
                    }

                    if (flag == false)
                    {
                        listtemp.Add(k);
                    }
                }
                ViewBag.CodeList = listtemp;


                Excel.Application application = new Excel.Application();
                application.DisplayAlerts = false;
                Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                worksheet.Cells[1, 1] = "Plan Resin Summary -- "+ week +" (by resin code) ";
                worksheet.Cells[2, 1] = "Resin Code";
                worksheet.Cells[2, 2] = "Resin";
                worksheet.Cells[2, 3] = "Resin Stock (g)";
                worksheet.Cells[2, 4] = "Total Plan Resin (g)";

                Range formatRange;

                Range usedRange = worksheet.Range["A1", "D1"];
                usedRange.Merge(false);
                usedRange = worksheet.Range["A2", "A3"];
                usedRange.Merge(false);
                usedRange = worksheet.Range["B2", "B3"];
                usedRange.Merge(false);
                usedRange = worksheet.Range["C2", "C3"];
                usedRange.Merge(false);
                usedRange = worksheet.Range["D2", "D3"];
                usedRange.Merge(false);

                int colum = 0;
                DateTime n = ViewBag.dateEnd;
                for (DateTime i = ViewBag.dateStart; i <= n; i = i.AddDays(1))
                {
                    colum++;
                }

                int tem1 = colum;
                colum += 4;
                
                usedRange = worksheet.Range["E2", worksheet.Cells[2,colum]];
                worksheet.Range["E2", worksheet.Cells[2, colum]] = "Plan";
                usedRange.Merge(false);
                formatRange = usedRange;
                formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);

                int tem = colum + 1;
                colum += tem1;
                usedRange = worksheet.Range[worksheet.Cells[2, tem], worksheet.Cells[2, colum]];
                worksheet.Range[worksheet.Cells[2, tem], worksheet.Cells[2, colum]] = "Balance";
                usedRange.Merge(false);
                formatRange = usedRange;
                formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleGreen);

                colum = 5;
                for (DateTime i = ViewBag.dateStart; i <= n; i = i.AddDays(1))
                {
                    string fm = String.Format("{0:dd}", i);
                    string fm2 = String.Format("{0:MMMM}", i);
                    string strdate = fm + " - " + fm2;
                    worksheet.Cells[3, colum] = strdate;
                    formatRange = worksheet.Cells[3, colum];
                    formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightBlue);
                    colum++;
                }
                for (DateTime i = ViewBag.dateStart; i <= n; i = i.AddDays(1))
                {
                    string fm = String.Format("{0:dd}", i);
                    string fm2 = String.Format("{0:MMMM}", i);
                    string strdate = fm + " - " + fm2;
                    worksheet.Cells[3, colum] = strdate;
                    formatRange = worksheet.Cells[3, colum];
                    formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleGreen);
                    colum++;
                }

                int row = 4;
                if(ViewBag.CodeList != null)
                {
                    string codetemp = "";
                    foreach(SummaryByProduct i in ViewBag.CodeList)
                    {
                        if(i.ResinCode != codetemp)
                        {
                            codetemp = i.ResinCode;
                            string resincode = i.ResinCode;
                            var resin_stock = db.ResinStocks.FirstOrDefault(x => x.Code == resincode);
                            if (resin_stock != null)
                            {
                                resin_stock.ResinStock1 = resin_stock.ResinStock1 * 1000;
                            }

                            worksheet.Cells[row, 1] = i.ResinCode;
                            formatRange = worksheet.Cells[row, 1];
                            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                            worksheet.Cells[row, 2] = i.Resin;
                            if (resin_stock != null)
                            {
                                worksheet.Cells[row, 3] = resin_stock.ResinStock1;
                                formatRange = worksheet.Cells[row, 3];
                                formatRange.NumberFormat = "#,##0";
                            }

                            int tong = 0;
                            int sum = 0;
                            for (DateTime j = ViewBag.dateStart; j <= n; j = j.AddDays(1))
                            {
                                sum = 0;
                                sum = db.SummaryByProducts.Where(x => x.ResinCode == resincode && x.Week == week && x.Date == j).Sum(x => x.SoLuong).Value;
                                tong += sum;
                            }
                            worksheet.Cells[row, 4] = tong;
                            formatRange = worksheet.Cells[row, 4];
                            formatRange.NumberFormat = "#,##0";
                            formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DeepSkyBlue);

                            sum = 0;
                            colum = 5;
                            for (DateTime j = ViewBag.dateStart; j <= n; j = j.AddDays(1))
                            {
                                sum = db.SummaryByProducts.Where(x => x.ResinCode == resincode && x.Week == week && x.Date == j).Sum(x => x.SoLuong).Value;
                                worksheet.Cells[row, colum] = sum;
                                formatRange = worksheet.Cells[row, colum];
                                formatRange.NumberFormat = "#,##0";
                                formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
                                colum++;
                            }

                            sum = 0;
                            for (DateTime j = ViewBag.dateStart; j <= n; j = j.AddDays(1))
                            {
                                if (j == ViewBag.dateStart)
                                {
                                    sum = db.SummaryByProducts.Where(x => x.ResinCode == resincode && x.Week == week && x.Date == j).Sum(x => x.SoLuong).Value;
                                    if(resin_stock != null)
                                    {
                                        sum = (int)resin_stock.ResinStock1 - sum;
                                        worksheet.Cells[row, colum] = sum;
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, colum] = "#N/A";
                                    }
                                    formatRange = worksheet.Cells[row, colum];
                                    formatRange.NumberFormat = "#,##0";
                                    formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                                    if (sum < 0)
                                    {
                                        formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    colum++;
                                }
                                else
                                {
                                    int temp = db.SummaryByProducts.Where(x => x.ResinCode == resincode && x.Week == week && x.Date == j).Sum(x => x.SoLuong).Value;
                                    sum = sum - temp;
                                    worksheet.Cells[row, colum] = sum;
                                    formatRange = worksheet.Cells[row, colum];
                                    formatRange.NumberFormat = "#,##0";
                                    formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGreen);
                                    if (sum < 0)
                                    {
                                        formatRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    colum++;
                                }
                            }
                            if(resin_stock != null)
                            {
                                resin_stock.ResinStock1 = 0;
                            }
                            row++;
                        }
                    }
                }

                usedRange = worksheet.UsedRange;
                usedRange.VerticalAlignment = VerticalAlign.Middle;
                usedRange.HorizontalAlignment = HorizontalAlign.Center;

                usedRange = worksheet.Range["E2", worksheet.Cells[2, colum]];
                usedRange.VerticalAlignment = 3;
                usedRange.HorizontalAlignment = 3;

                usedRange = worksheet.Range[worksheet.Cells[2, tem], worksheet.Cells[2, colum]];
                usedRange.VerticalAlignment = 3;
                usedRange.HorizontalAlignment = 3;

                usedRange = worksheet.Range["A1", "D1"];
                usedRange.Font.Bold = true;
                usedRange.Font.Size = 16;

                usedRange = worksheet.Range["A2", "D3"];
                usedRange.Font.Bold = true;
                usedRange.Font.Size = 11;
                formatRange = usedRange;
                formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                Excel.Range cell = worksheet.UsedRange;
                Excel.Borders border = cell.Borders;
                border.LineStyle = Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;

                worksheet.Columns.AutoFit();
                worksheet.Rows.AutoFit();

                string FileName = "Plan Resin Summary -- " + week + " (By Resin Code).xlsx";
                //string FileName = "picklist.xlsx";
                string FolderPath = HttpContext.Server.MapPath("/Content/Files");

                string path = Server.MapPath("~/Content/Files/") + Path.GetFileName(FileName);
                workbook.SaveAs(path);

                workbook.Close(0);
                application.Quit();
                Marshal.FinalReleaseComObject(worksheet);
                Marshal.FinalReleaseComObject(workbook);
                Marshal.FinalReleaseComObject(application);

                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(FileName));
                Response.WriteFile(path);
                Response.End();
                Response.Flush();
                System.IO.File.Delete(path);
            }
        }

        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_KQSX")]
        public ActionResult ViewEditKQSX(string strdate,string code,int somay,string week)
        {
            DateTime date = Convert.ToDateTime(strdate);
            var result = db.DA_Injection.FirstOrDefault(x => x.Code == code && x.Date == date && x.So_May == somay && x.Week == week);
            ViewBag.KQ = result;
            ViewBag.Week = week;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_KQSX")]
        public ActionResult ViewEditKQSX(DA_Injection da)
        {
            string code = da.Code;
            DateTime date = Convert.ToDateTime(da.Date);
            string week = da.Week;
            int somay = (int)da.So_May;
            var result = db.DA_Injection.FirstOrDefault(x => x.Code == code && x.Date == date && x.So_May == somay && x.Week == week);
            if(result != null)
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Dữ liệu không hợp lệ!");
                }
                else
                {
                    result.Good_D_ = da.Good_D_;
                    result.Good_N_ = da.Good_N_;
                    db.SaveChanges();
                    ModelState.AddModelError("", "Cập nhật thành công!");
                }
                ViewBag.KQ = result;
            }
            return View();
        }
    }
}
