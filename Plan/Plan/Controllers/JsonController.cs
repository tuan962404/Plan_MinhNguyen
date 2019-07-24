using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Plan.Controllers
{
    public class JsonController : BaseController
    {
        // GET: Json
        PlanEntities db = new PlanEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Add_Plan(string code, string psi,int somay,string trongluongmay,int kehoach,int kehoachbosung,
                                   float manpower,int stock,float cycle,int cavity, float capa,string week,
                                   DateTime dateStart,DateTime dateEnd, string id_plantimedem, string id_plantimengay,
                                   string id_plandem, string id_planngay, string html_plantimedem, string html_plantimengay)
        {
            if(psi == "assy")
            {
                var kq = db.Assy_PSI_Shortage.Where(x=>x.Code == code && x.Date >= dateStart && x.Date <= dateEnd).ToList();
                foreach (var i in kq)
                {
                    DA_Injection da = new DA_Injection();
                    da.Code = i.Code;
                    da.Date = i.Date;
                    da.Requirements = i.Requirement;
                    da.So_May = somay;
                    da.Trong_Luong_May = trongluongmay;
                    da.Ke_Hoach = kehoach;
                    da.Ke_Hoach_Bo_Sung = kehoachbosung;
                    da.Manpower = manpower;
                    da.Stock = stock;
                    da.Cycle_time = cycle;
                    da.Cavity = cavity;
                    da.Capa = capa;
                    da.Plan_D_ = 0;
                    da.Plan_N_ = 0;
                    da.C_Plan_Time_N_ = 0;
                    da.Plan_Time_D_ = 0;
                    da.Week = week;
                    da.ID_PlanTimeDem = id_plantimedem;
                    da.ID_PlanTimeNgay = id_plantimengay;
                    da.ID_PlanDem = id_plandem;
                    da.ID_PlanNgay = id_planngay;
                    da.Statuss = "false";
                    da.PSI = "assy";
                    da.Html_PlanTimeDem = html_plantimedem;
                    da.Html_PlanTimeNgay = html_plantimengay;
                    db.DA_Injection.Add(da);
                    db.SaveChanges();
                }
                return Json(kq, JsonRequestBehavior.AllowGet);
            }

            if(psi == "vc")
            {
                var kq = db.PSI_V_C.Where(x => x.Code == code && x.Date >= dateStart && x.Date <= dateEnd).ToList();
                foreach (var i in kq)
                {
                    DA_Injection da = new DA_Injection();
                    da.Code = i.Code;
                    da.Date = i.Date;
                    da.Requirements = i.Requirement;
                    da.So_May = somay;
                    da.Trong_Luong_May = trongluongmay;
                    da.Ke_Hoach = kehoach;
                    da.Ke_Hoach_Bo_Sung = kehoachbosung;
                    da.Manpower = manpower;
                    da.Stock = stock;
                    da.Cycle_time = cycle;
                    da.Cavity = cavity;
                    da.Capa = capa;
                    da.Plan_D_ = 0;
                    da.Plan_N_ = 0;
                    da.C_Plan_Time_N_ = 0;
                    da.Plan_Time_D_ = 0;
                    da.Week = week;
                    da.ID_PlanTimeDem = id_plantimedem;
                    da.ID_PlanTimeNgay = id_plantimengay;
                    da.ID_PlanDem = id_plandem;
                    da.ID_PlanNgay = id_planngay;
                    da.Statuss = "false";
                    da.PSI = "vc";
                    da.Html_PlanTimeDem = html_plantimedem;
                    da.Html_PlanTimeNgay = html_plantimengay;
                    db.DA_Injection.Add(da);
                    db.SaveChanges();
                }
                return Json(kq, JsonRequestBehavior.AllowGet);
            }

            if(psi == "ref")
            {
                var kq = db.PSI_WM_REF.Where(x => x.Code == code && x.Date >= dateStart && x.Date <= dateEnd).ToList();
                foreach (var i in kq)
                {
                    DA_Injection da = new DA_Injection();
                    da.Code = i.Code;
                    da.Date = i.Date;
                    da.Requirements = i.Requirements;
                    da.So_May = somay;
                    da.Trong_Luong_May = trongluongmay;
                    da.Ke_Hoach = kehoach;
                    da.Ke_Hoach_Bo_Sung = kehoachbosung;
                    da.Manpower = manpower;
                    da.Stock = stock;
                    da.Cycle_time = cycle;
                    da.Cavity = cavity;
                    da.Capa = Convert.ToDouble(Math.Round(capa, 1));
                    da.Plan_D_ = 0;
                    da.Plan_N_ = 0;
                    da.C_Plan_Time_N_ = 0;
                    da.Plan_Time_D_ = 0;
                    da.Week = week;
                    da.ID_PlanTimeDem = id_plantimedem;
                    da.ID_PlanTimeNgay = id_plantimengay;
                    da.ID_PlanDem = id_plandem;
                    da.ID_PlanNgay = id_planngay;
                    da.Statuss = "false";
                    da.PSI = "ref";
                    da.Html_PlanTimeDem = html_plantimedem;
                    da.Html_PlanTimeNgay = html_plantimengay;
                    db.DA_Injection.Add(da);
                    db.SaveChanges();
                }
                return Json(kq, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }

        [HttpGet]
        public JsonResult KiemTra(string code, string psi,string week)
        {
            var flag = 0;
            var data = db.DA_Injection.FirstOrDefault(x=>x.Code == code && x.PSI == psi && x.Week == week);
            if(data == null)
            {
                flag = 0;
            }
            else
            {
                flag = 1;
            }
            return Json(flag,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDA(string code, string week,string psi)
        {
            var data = db.DA_Injection.Where(x => x.Code == code && x.Week == week && x.PSI == psi).ToList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult StartPlan(string code,string strdate ,string week,string id_plantime,string psi,int plan,float plantime)
        {
            var kehoach = 0;
            double public_time = 0;
            var status = 0;
            int stt = 0;
            DateTime date = Convert.ToDateTime(strdate);
            /*
            var data = db.DA_Injection.Where(x => x.Week == week).ToList();
            foreach (var d in data)
            {
                d.Plan_Time_D_ = 0;
                d.C_Plan_Time_N_ = 0;
                d.Plan_D_ = 0;
                d.Plan_N_ = 0;
                d.FlagDem = false;
                d.FlagNgay = false;
                db.SaveChanges();
            }*/
            var kq = db.DA_Injection.Where(x => x.Code == code && x.Week == week && x.PSI == psi).ToList();
            var result = db.DA_Injection.Where(x => x.Code == code && x.Date == date && x.Week == week && x.PSI == psi).FirstOrDefault();
            if (result != null)
            {
                //Tính tổng thời gian của 1 máy
                double sumtime_1may = SumTime_1May(Convert.ToInt32(result.So_May), result.Week);
                var time_kehoach = Convert.ToDouble(result.Ke_Hoach) / result.Capa;
                
                if (id_plantime == result.ID_PlanTimeDem)
                {
                    if (time_kehoach <= sumtime_1may)
                    {
                        var sumkhbs = db.DA_Injection.Where(x => x.Code == result.Code && x.PSI == result.PSI && x.Week == result.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                        kehoach = (int)(result.Ke_Hoach + sumkhbs);
                        if (plan > kehoach)
                        {
                            plan = kehoach;
                            plantime = (float)(kehoach / result.Capa);
                            result.Plan_D_ = plan;
                            result.Plan_Time_D_ = plantime;
                            result.FlagDem = true;
                            result.Statuss = "true";
                            result.STT = stt;
                            stt++;
                            db.SaveChanges();
                            
                            kehoach = 0;
                            public_time = plantime + 2;
                        }
                        else
                        {
                            result.Plan_D_ = plan;
                            result.Plan_Time_D_ = plantime;
                            result.FlagDem = true;
                            result.Statuss = "true";
                            result.STT = stt;
                            stt++;
                            db.SaveChanges();
                            var list = db.DA_Injection.Where(x => x.So_May == result.So_May && x.Date == result.Date && x.Week == result.Week).ToList();
                            foreach (var j in list)
                            {
                                j.FlagDem = true;
                                j.STT = stt;
                                stt++;
                                db.SaveChanges();
                            }

                            kehoach = (int)((result.Ke_Hoach + sumkhbs) - plan);
                            status = 1;
                        }
                    }

                    if (kq.Count > 0)
                    {
                        if(time_kehoach <= sumtime_1may)
                        {
                            foreach (var i in kq)
                            {
                                double temp = 12 * Convert.ToDouble(i.Capa);
                                int max = Convert.ToInt32(Math.Ceiling(temp));
                                if (i.Date >= date)
                                {
                                    if (kehoach > 0)
                                    {
                                        if (status == 1)
                                        {
                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                i.C_Plan_Time_N_ = hours;
                                                i.Plan_N_ = kehoach;
                                                i.STT = stt;
                                                stt++;
                                                if(kehoach == max)
                                                {
                                                    i.FlagNgay = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagNgay = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = hours + 2;
                                                    if(public_time >= 12)
                                                    {
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagNgay = true;
                                                            j.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                
                                                db.SaveChanges();
                                                kehoach = 0;
                                                break;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                i.C_Plan_Time_N_ = hours;
                                                i.Plan_N_ = max;
                                                i.FlagNgay = true;
                                                i.STT = stt;
                                                stt++;
                                                var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagNgay = true;
                                                    j.STT = stt;
                                                    stt++;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }
                                            status = 0;
                                        }
                                        else
                                        {
                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                
                                                i.Plan_Time_D_ = hours;
                                                i.Plan_D_ = kehoach;
                                                i.STT = stt;
                                                stt++;
                                                if(kehoach == max)
                                                {
                                                    i.FlagDem = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagDem = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = hours + 2;
                                                    if(public_time >= 12)
                                                    {
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagDem = true;
                                                            j.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                                kehoach = 0;
                                                break;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                
                                                i.Plan_Time_D_ = hours;
                                                i.Plan_D_ = max;
                                                i.FlagDem = true;
                                                i.STT = stt;
                                                stt++;
                                                var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagDem = true;
                                                    j.STT = stt;
                                                    stt++;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }

                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                
                                                i.C_Plan_Time_N_ = hours;
                                                i.Plan_N_ = kehoach;
                                                i.STT = stt;
                                                stt++;
                                                if (kehoach == max)
                                                {
                                                    i.FlagNgay = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagNgay = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = hours + 2;
                                                    if(public_time >= 12)
                                                    {
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagNgay = true;
                                                            j.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                                kehoach = 0;
                                                break;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                
                                                i.C_Plan_Time_N_ = hours;
                                                i.Plan_N_ = max;
                                                i.FlagNgay = true;
                                                i.STT = stt;
                                                stt++;
                                                var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagNgay = true;
                                                    j.STT = stt;
                                                    stt++;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (id_plantime == result.ID_PlanTimeNgay)
                {
                    if(time_kehoach <= sumtime_1may)
                    {
                        var sumkhbs = db.DA_Injection.Where(x => x.Code == result.Code && x.PSI == result.PSI && x.Week == result.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                        kehoach = (int)(result.Ke_Hoach + sumkhbs);
                        if (plan > kehoach)
                        {
                            plan = kehoach;
                            plantime = (float)(kehoach / result.Capa);
                            result.Plan_D_ = plan;
                            result.Plan_Time_D_ = plantime;
                            result.FlagDem = true;
                            result.Statuss = "true";
                            db.SaveChanges();

                            var list = db.DA_Injection.Where(x => x.So_May == result.So_May && x.Date == result.Date && x.Week == result.Week).ToList();
                            foreach (var j in list)
                            {
                                j.FlagDem = true;
                                db.SaveChanges();
                            }
                            kehoach = 0;
                            public_time = plantime + 2;
                        }
                        else
                        {
                            result.Plan_N_ = plan;
                            result.C_Plan_Time_N_ = plantime;
                            result.FlagNgay = true;
                            result.Statuss = "true";
                            db.SaveChanges();

                            var list = db.DA_Injection.Where(x => x.So_May == result.So_May && x.Date == result.Date && x.Week == result.Week).ToList();
                            foreach (var j in list)
                            {
                                j.FlagNgay = true;
                                db.SaveChanges();
                            }

                            kehoach = (int)((result.Ke_Hoach + sumkhbs) - plan);
                            status = 1;
                        }
                    }

                    if(kq != null)
                    {
                        if (time_kehoach <= sumtime_1may)
                        {
                            foreach (var i in kq)
                            {
                                double temp = 12 * Convert.ToDouble(i.Capa);
                                int max = Convert.ToInt32(Math.Ceiling(temp));
                                if (i.Date >= date)
                                {
                                    if (kehoach > 0)
                                    {
                                        if (status == 1)
                                        {
                                            status = 0;
                                            continue;
                                        }
                                        else
                                        {
                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                
                                                i.Plan_Time_D_ = hours;
                                                i.Plan_D_ = kehoach;
                                                if (kehoach == max)
                                                {
                                                    i.FlagDem = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagDem = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = hours + 2;
                                                    if(public_time >= 12)
                                                    {
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                                kehoach = 0;
                                                break;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                
                                                i.Plan_Time_D_ = hours;
                                                i.Plan_D_ = max;
                                                i.FlagDem = true;
                                                var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagDem = true;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }

                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                
                                                i.C_Plan_Time_N_ = hours;
                                                i.Plan_N_ = kehoach;
                                                if (kehoach == max)
                                                {
                                                    i.FlagNgay = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = hours + 2;
                                                    if(public_time >= 12)
                                                    {
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                               
                                                kehoach = 0;
                                                break;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                
                                                i.C_Plan_Time_N_ = hours;
                                                i.Plan_N_ = max;
                                                i.FlagNgay = true;
                                                var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //List cùng số máy
                var listsomay = db.DA_Injection.Where(x =>x.So_May == result.So_May && x.Week == result.Week).ToList();
                var code_temp = "";

                if (listsomay != null)
                {
                    time_kehoach = 0;
                    sumtime_1may = -1;
                    foreach (var k in listsomay)
                    {
                        var flag = 0;
                        if (k.FlagDem == false)
                        {
                            if (k.Code != result.Code)
                            {
                                if (k.Code != code_temp)
                                {
                                    flag = 1;
                                    time_kehoach = (Convert.ToDouble(k.Ke_Hoach) + Convert.ToDouble(k.Ke_Hoach_Bo_Sung)) / Convert.ToDouble(k.Capa);
                                    //Tính tổng thời gian của 1 máy
                                    var temp = 12 - plantime;
                                    sumtime_1may = SumTime_1May(Convert.ToInt32(k.So_May),k.Week);
                                    sumtime_1may -= temp;
                                }

                                if(time_kehoach <= sumtime_1may)
                                {
                                    if (flag == 1)
                                    {
                                        var time_private = 12 - public_time;
                                        double plan_private = time_private * Convert.ToDouble(k.Capa);
                                        plan_private = (int)Math.Ceiling(plan_private);
                                        var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                        kehoach = (int)k.Ke_Hoach + sumkhbs;
                                        if (kehoach > 0)
                                        {
                                            if (plan_private <= kehoach)
                                            {
                                                kehoach = kehoach - (int)plan_private;
                                                k.Plan_D_ = (int)plan_private;
                                                k.Plan_Time_D_ = time_private;
                                                k.FlagDem = true;
                                                k.STT = stt;
                                                stt++;
                                                public_time = 0;
                                                db.SaveChanges();
                                                var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagDem = true;
                                                    j.STT = stt;
                                                    stt++;
                                                    db.SaveChanges();
                                                }

                                                double temp = 12 * Convert.ToDouble(k.Capa);
                                                int max = Convert.ToInt32(Math.Ceiling(temp));
                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                    
                                                    k.C_Plan_Time_N_ = hours;
                                                    k.Plan_N_ = kehoach;
                                                    k.STT = stt;
                                                    stt++;
                                                    if (kehoach == max)
                                                    {
                                                        k.FlagNgay = true;
                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list_)
                                                        {
                                                            j.FlagNgay = true;
                                                            j.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = public_time + hours + 2;
                                                        if(public_time >= 12)
                                                        {
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                j.STT = stt;
                                                                stt++;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                    db.SaveChanges();
                                                   
                                                    kehoach = 0;
                                                    code_temp = k.Code;
                                                    continue;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                    
                                                    k.C_Plan_Time_N_ = hours;
                                                    k.Plan_N_ = max;
                                                    k.FlagNgay = true;
                                                    k.STT = stt;
                                                    stt++;
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                    code_temp = k.Code;
                                                    continue;
                                                }
                                            }

                                            if (plan_private > kehoach)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                
                                                public_time = time_private + 2;
                                                if(public_time >= 12)
                                                {
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagDem = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                                plan_private = kehoach;
                                                k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                                k.Plan_Time_D_ = hours;
                                                k.STT = stt;
                                                stt++;
                                                db.SaveChanges();
                                                kehoach = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (kehoach > 0)
                                        {
                                            double temp = 12 * Convert.ToDouble(k.Capa);
                                            int max = Convert.ToInt32(Math.Ceiling(temp));
                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                
                                                k.Plan_Time_D_ = hours;
                                                k.Plan_D_ = kehoach;
                                                k.STT = stt;
                                                stt++;
                                                if (kehoach == max)
                                                {
                                                    k.FlagDem = true;
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagDem = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = public_time + hours + 2;
                                                    if(public_time >= 12)
                                                    {
                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list_)
                                                        {
                                                            j.FlagDem = true;
                                                            j.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                                kehoach = 0;
                                                code_temp = k.Code;
                                                continue;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                
                                                k.Plan_Time_D_ = hours;
                                                k.Plan_D_ = max;
                                                k.FlagDem = true;
                                                k.STT = stt;
                                                stt++;
                                                var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagDem = true;
                                                    j.STT = stt;
                                                    stt++;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }

                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                
                                                k.C_Plan_Time_N_ = hours;
                                                k.Plan_N_ = kehoach;
                                                k.STT = stt;
                                                stt++;
                                                if (kehoach == max)
                                                {
                                                    k.FlagNgay = true;
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = public_time + hours + 2;
                                                    if(public_time >= 12)
                                                    {
                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list_)
                                                        {
                                                            j.FlagNgay = true;
                                                            j.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                                kehoach = 0;
                                                code_temp = k.Code;
                                                continue;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                
                                                k.C_Plan_Time_N_ = hours;
                                                k.Plan_N_ = max;
                                                k.FlagNgay = true;
                                                k.STT = stt;
                                                stt++;
                                                var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagNgay = true;
                                                    j.STT = stt;
                                                    stt++;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                }
                                code_temp = k.Code;
                                continue;
                            }
                        }

                        /********************/

                        if (k.FlagNgay == false)
                        {
                            if (k.Code != result.Code)
                            {
                                time_kehoach = 0;
                                sumtime_1may = -1;
                                if (k.Code != code_temp)
                                {
                                    flag = 1;
                                    time_kehoach = (Convert.ToDouble(k.Ke_Hoach) + Convert.ToDouble(k.Ke_Hoach_Bo_Sung)) / Convert.ToDouble(k.Capa);
                                    //Tính tổng thời gian của 1 máy
                                    var temp = 12 - plantime;
                                    sumtime_1may = SumTime_1May(Convert.ToInt32(k.So_May), k.Week);
                                    sumtime_1may -= temp;
                                }

                                if(time_kehoach <= sumtime_1may)
                                {
                                    if (flag == 1)
                                    {
                                        var time_private = 12 - public_time;
                                        double plan_private = time_private * Convert.ToDouble(k.Capa);
                                        plan_private = (int)Math.Ceiling(plan_private);
                                        var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                        kehoach = (int)k.Ke_Hoach + sumkhbs;
                                        if (kehoach > 0)
                                        {
                                            if (plan_private <= kehoach)
                                            {
                                                kehoach = kehoach - (int)plan_private;
                                                k.Plan_N_ = (int)plan_private;
                                                k.C_Plan_Time_N_ = time_private;
                                                k.FlagNgay = true;
                                                k.STT = stt;
                                                stt++;
                                                public_time = 0;
                                                db.SaveChanges();
                                                var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list)
                                                {
                                                    j.FlagNgay = true;
                                                    j.STT = stt;
                                                    stt++;
                                                    db.SaveChanges();
                                                }
                                                code_temp = k.Code;
                                                continue;
                                            }

                                            if (plan_private > kehoach)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                
                                                plan_private = kehoach;
                                                k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                                k.C_Plan_Time_N_ = hours;
                                                k.STT = stt;
                                                stt++;
                                                db.SaveChanges();
                                                kehoach = 0;
                                                public_time = public_time + hours + 2;
                                                if(public_time >= 12)
                                                {
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        j.STT = stt;
                                                        stt++;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }
                                        }
                                    }
                                }
                                code_temp = k.Code;
                            }
                        }
                    }
                }

                //List những máy khác

                var list_mayconlai = db.DA_Injection.AsNoTracking().Where(x => x.So_May != result.So_May && x.Week == week).ToList();
                var code_temp1 = "";
                int somay = 0;
                if(list_mayconlai != null)
                {
                    for(int i = 0;i < list_mayconlai.Count; i=0)
                    {
                        if (list_mayconlai[i].So_May != somay)
                        {
                            somay = Convert.ToInt32(list_mayconlai[i].So_May);
                            string strweek = list_mayconlai[i].Week;
                            var list_cungmotmay = db.DA_Injection.Where(x => x.So_May == somay && x.Week == strweek).ToList();
                            if (list_cungmotmay != null)
                            {
                                time_kehoach = 0;
                                sumtime_1may = -1;
                                public_time = 0;
                                foreach (var k in list_cungmotmay)
                                {
                                    var flag = 0;
                                    if (k.FlagDem == false)
                                    {
                                        if (k.Code != result.Code)
                                        {
                                            if (k.Code != code_temp1)
                                            {
                                                flag = 1;
                                                time_kehoach = (Convert.ToDouble(k.Ke_Hoach) + Convert.ToDouble(k.Ke_Hoach_Bo_Sung)) / Convert.ToDouble(k.Capa);
                                                //Tính tổng thời gian của 1 máy
                                                sumtime_1may = SumTime_1May(Convert.ToInt32(k.So_May), k.Week);
                                            }

                                            if (time_kehoach <= sumtime_1may)
                                            {
                                                if (flag == 1)
                                                {
                                                    var time_private = 12 - public_time;
                                                    
                                                    double plan_private = time_private * Convert.ToDouble(k.Capa);
                                                    plan_private = (int)Math.Ceiling(plan_private);
                                                    var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                                    kehoach = (int)k.Ke_Hoach + sumkhbs;
                                                    if (kehoach > 0)
                                                    {
                                                        if (plan_private <= kehoach)
                                                        {
                                                            kehoach = kehoach - (int)plan_private;
                                                            k.Plan_D_ = (int)plan_private;
                                                            k.Plan_Time_D_ = time_private;
                                                            k.FlagDem = true;
                                                            k.STT = stt;
                                                            stt++;
                                                            public_time = 0;
                                                            status = 1;
                                                            db.SaveChanges();
                                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list)
                                                            {
                                                                j.FlagDem = true;
                                                                j.STT = stt;
                                                                stt++;
                                                                db.SaveChanges();
                                                            }

                                                            double temp = 12 * Convert.ToDouble(k.Capa);
                                                            int max = Convert.ToInt32(Math.Ceiling(temp));
                                                            if (kehoach <= max)
                                                            {
                                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                                
                                                                k.C_Plan_Time_N_ = hours;
                                                                k.Plan_N_ = kehoach;
                                                                k.STT = stt;
                                                                stt++;
                                                                if (kehoach == max)
                                                                {
                                                                    k.FlagNgay = true;
                                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                    foreach (var j in list_)
                                                                    {
                                                                        j.FlagNgay = true;
                                                                        j.STT = stt;
                                                                        stt++;
                                                                        db.SaveChanges();
                                                                    }
                                                                    public_time = 0;
                                                                }
                                                                else
                                                                {
                                                                    public_time = public_time + hours + 2;
                                                                    if(public_time >= 12)
                                                                    {
                                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                        foreach (var j in list_)
                                                                        {
                                                                            j.FlagNgay = true;
                                                                            j.STT = stt;
                                                                            stt++;
                                                                            db.SaveChanges();
                                                                        }
                                                                        public_time = public_time - 12;
                                                                    }
                                                                }
                                                                db.SaveChanges();
                                                                kehoach = 0;
                                                                code_temp1 = k.Code;
                                                                continue;
                                                            }

                                                            if (kehoach > max)
                                                            {
                                                                kehoach = kehoach - max;
                                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                                
                                                                k.C_Plan_Time_N_ = hours;
                                                                k.Plan_N_ = max;
                                                                k.FlagNgay = true;
                                                                k.STT = stt;
                                                                stt++;
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagNgay = true;
                                                                    j.STT = stt;
                                                                    stt++;
                                                                    db.SaveChanges();
                                                                }
                                                                db.SaveChanges();
                                                                code_temp1 = k.Code;
                                                                continue;
                                                            }
                                                        }

                                                        if (plan_private > kehoach)
                                                        {
                                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                            
                                                            public_time = public_time + hours + 2;
                                                            if(public_time >= 12)
                                                            {
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagDem = true;
                                                                    j.STT = stt;
                                                                    stt++;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = public_time - 12;
                                                            }
                                                            plan_private = kehoach;
                                                            k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                                            k.Plan_Time_D_ = hours;
                                                            k.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                            kehoach = 0;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if(kehoach > 0)
                                                    {
                                                        double temp = 12 * Convert.ToDouble(k.Capa);
                                                        int max = Convert.ToInt32(Math.Ceiling(temp));
                                                        if (kehoach <= max)
                                                        {
                                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.Plan_Time_D_ = hours;
                                                            k.Plan_D_ = kehoach;
                                                            k.STT = stt;
                                                            stt++;
                                                            if (kehoach == max)
                                                            {
                                                                k.FlagDem = true;
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagDem = true;
                                                                    j.STT = stt;
                                                                    stt++;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = 0;
                                                            }
                                                            else
                                                            {
                                                                public_time = public_time + hours + 2;
                                                                if (public_time >= 12)
                                                                {
                                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                    foreach (var j in list_)
                                                                    {
                                                                        j.FlagDem = true;
                                                                        j.STT = stt;
                                                                        stt++;
                                                                        db.SaveChanges();
                                                                    }
                                                                    public_time = public_time - 12;
                                                                }
                                                            }
                                                            db.SaveChanges();
                                                            kehoach = 0;
                                                            code_temp1 = k.Code;
                                                            continue;
                                                        }

                                                        if (kehoach > max)
                                                        {
                                                            kehoach = kehoach - max;
                                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.Plan_Time_D_ = hours;
                                                            k.Plan_D_ = max;
                                                            k.FlagDem = true;
                                                            k.STT = stt;
                                                            stt++;
                                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list)
                                                            {
                                                                j.FlagDem = true;
                                                                j.STT = stt;
                                                                stt++;
                                                                db.SaveChanges();
                                                            }
                                                            db.SaveChanges();
                                                        }

                                                        if (kehoach <= max)
                                                        {
                                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.C_Plan_Time_N_ = hours;
                                                            k.Plan_N_ = kehoach;
                                                            k.STT = stt;
                                                            stt++;
                                                            if (kehoach == max)
                                                            {
                                                                k.FlagNgay = true;
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagNgay = true;
                                                                    j.STT = stt;
                                                                    stt++;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = 0;
                                                            }
                                                            else
                                                            {
                                                                public_time = public_time + hours + 2;
                                                                if (public_time >= 12)
                                                                {
                                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                    foreach (var j in list_)
                                                                    {
                                                                        j.FlagNgay = true;
                                                                        j.STT = stt;
                                                                        stt++;
                                                                        db.SaveChanges();
                                                                    }
                                                                    public_time = public_time - 12;
                                                                }
                                                            }
                                                            db.SaveChanges();
                                                            kehoach = 0;
                                                            code_temp1 = k.Code;
                                                            continue;
                                                        }

                                                        if (kehoach > max)
                                                        {
                                                            kehoach = kehoach - max;
                                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.C_Plan_Time_N_ = hours;
                                                            k.Plan_N_ = max;
                                                            k.FlagNgay = true;
                                                            k.STT = stt;
                                                            stt++;
                                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list)
                                                            {
                                                                j.FlagNgay = true;
                                                                j.STT = stt;
                                                                stt++;
                                                                db.SaveChanges();
                                                            }
                                                            db.SaveChanges();
                                                        }
                                                    }
                                                }
                                            }
                                            code_temp1 = k.Code;
                                            continue;
                                        }
                                    }

                                    /********************/

                                    if (k.FlagNgay == false)
                                    {
                                        if (k.Code != result.Code)
                                        {
                                            time_kehoach = 0;
                                            sumtime_1may = -1;
                                            if (k.Code != code_temp1)
                                            {
                                                flag = 1;
                                                time_kehoach = (Convert.ToDouble(k.Ke_Hoach) + Convert.ToDouble(k.Ke_Hoach_Bo_Sung)) / Convert.ToDouble(k.Capa);
                                                //Tính tổng thời gian của 1 máy
                                                sumtime_1may = SumTime_1May(Convert.ToInt32(k.So_May), k.Week);
                                            }

                                            if (time_kehoach <= sumtime_1may)
                                            {
                                                if (flag == 1)
                                                {
                                                    var time_private = 12 - public_time;
                                                    double plan_private = time_private * Convert.ToDouble(k.Capa);
                                                    plan_private = (int)Math.Ceiling(plan_private);
                                                    var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                                    kehoach = (int)k.Ke_Hoach + sumkhbs;
                                                    if (kehoach > 0)
                                                    {
                                                        if (plan_private <= kehoach)
                                                        {
                                                            kehoach = kehoach - (int)plan_private;
                                                            k.Plan_N_ = (int)plan_private;
                                                            k.C_Plan_Time_N_ = time_private;
                                                            k.FlagNgay = true;
                                                            k.STT = stt;
                                                            stt++;
                                                            public_time = 0;
                                                            db.SaveChanges();
                                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list)
                                                            {
                                                                j.FlagNgay = true;
                                                                j.STT = stt;
                                                                stt++;
                                                                db.SaveChanges();
                                                            }
                                                            code_temp1 = k.Code;
                                                            continue;
                                                        }

                                                        if (plan_private > kehoach)
                                                        {
                                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                            
                                                            plan_private = kehoach;
                                                            k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                                            k.C_Plan_Time_N_ = hours;
                                                            k.STT = stt;
                                                            stt++;
                                                            db.SaveChanges();
                                                            kehoach = 0;
                                                            public_time = public_time + hours + 2;
                                                            if(public_time >= 12)
                                                            {
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagNgay = true;
                                                                    j.STT = stt;
                                                                    stt++;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = public_time - 12;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            code_temp1 = k.Code;
                                        }
                                    }
                                }

                                foreach(var j in list_mayconlai.ToList())
                                {
                                    if (j.So_May.Equals(somay))
                                    {
                                        list_mayconlai.Remove(j);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var data = db.DA_Injection.AsNoTracking().Where(x=>x.Week == week).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public Double SumTime_1May(int somay,string week)
        {
            double tong = 0;
            int n = 0;
            var listtemp = db.DA_Injection.AsNoTracking().Where(x => x.So_May == somay && x.Week == week).ToList();
            foreach(var i in listtemp)
            {
                n = db.DA_Injection.AsNoTracking().Where(x => x.So_May == somay && x.Week == week && x.Code == i.Code && x.PSI == i.PSI).Count();
                break;
            }

            for(int i = 0; i < n; i++)
            {
                tong += 24;
            }

            double sumtime_1code = 0;
            string codetemp = "";
            var listsomay = db.DA_Injection.AsNoTracking().Where(x => x.So_May == somay && x.Week == week).ToList();
            foreach (var u in listsomay)
            {
                if(codetemp != u.Code)
                {
                    sumtime_1code += db.DA_Injection.AsNoTracking().Where(x => x.So_May == u.So_May && x.Week == u.Week && x.Code == u.Code && x.PSI == u.PSI).Sum(x => x.Plan_Time_D_).Value;
                    sumtime_1code += db.DA_Injection.AsNoTracking().Where(x => x.So_May == u.So_May && x.Week == u.Week && x.Code == u.Code && x.PSI == u.PSI).Sum(x => x.C_Plan_Time_N_).Value;
                    if(sumtime_1code > 0)
                    {
                        sumtime_1code += 2;
                    }
                    tong = tong - sumtime_1code;
                    sumtime_1code = 0;
                }
                codetemp = u.Code;
            }
            return tong;
        }

        [HttpGet]
        public JsonResult UpdateKHBS(string code, string date, string week, int khbs,string psi)
        {
            DateTime ngay = Convert.ToDateTime(date);
            var KHBS = db.DA_Injection.Where(x => x.Code == code && x.Date == ngay && x.Week == week && x.PSI == psi).FirstOrDefault();
            if (KHBS != null)
            {
                KHBS.Ke_Hoach_Bo_Sung = khbs;
                db.SaveChanges();
            }

            var data = db.DA_Injection.Where(x => x.Code == code && x.Date > ngay && x.Week == week && x.PSI == psi).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCodeThieu(string week)
        {
            List<DA_Injection> ListCodeThieu = new List<DA_Injection>();
            var list = db.DA_Injection.Where(x=>x.Week == week).ToList();
            var code = "";
            foreach(var i in list)
            {
                if(i.Code != code)
                {
                    var sum = 0;
                    sum += db.DA_Injection.Where(x=>x.Code == i.Code && x.Week == i.Week && x.PSI == i.PSI).Sum(x => x.Plan_D_).Value;
                    sum += db.DA_Injection.Where(x => x.Code == i.Code && x.Week == i.Week && x.PSI == i.PSI).Sum(x => x.Plan_N_).Value;
                    if(sum == 0)
                    {
                        ListCodeThieu.Add(i);
                    }
                }
                code = i.Code;
            }
            var data = ListCodeThieu;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOption(int somay,float time,string week,int somay_start,double plantime)
        {
            var list = db.DA_Injection.AsNoTracking().Where(x => x.So_May != somay && x.Week == week).ToList();
            int somay_ = 0;
            List<string> list_option = new List<string>();
            for (int i = 0; i < list.Count; i = 0)
            {
                if(list[i].So_May != somay_)
                {
                    somay_ = (int)list[i].So_May;
                    var code = list[i].Code;
                    double time_1may = 0;
                    if(somay_ != somay_start)
                    {
                        time_1may = SumTime_1May(Convert.ToInt32(somay_), week);
                    }
                    else
                    {
                        var temp = 12 - plantime;
                        time_1may = SumTime_1May(Convert.ToInt32(somay_), week); ;
                        time_1may -= temp;
                    }

                    if (time <= time_1may)
                    {
                        string strsomay = somay_.ToString();
                        list_option.Add(strsomay);
                    }
                    
                    foreach (var j in list.ToList())
                    {
                        if (j.So_May.Equals(somay_))
                        {
                            list.Remove(j);
                        }
                    }
                }
            }
            return Json(list_option,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTimeOption(string week,int somay,int somay_start,double plantime)
        {
            double time = 0;
            if (somay != somay_start)
            {
                time = SumTime_1May(Convert.ToInt32(somay), week);
            }
            else
            {
                var temp = 12 - plantime;
                time = SumTime_1May(Convert.ToInt32(somay), week); ;
                time -= temp;
            }
            return Json(time,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChangeMachine(string code,string week,int kehoach_edit,int somay,string psi,int somay_start,double plantime)
        {
            bool co = false;
            double public_time = 0;
            var code_temp1 = "";
            var kehoach = 0;
            var stt = 0;
            var tam = db.DA_Injection.OrderByDescending(x=>x.STT).FirstOrDefault(x=>x.Week == week);
            stt = (int)tam.STT + 1;

            var list = db.DA_Injection.AsNoTracking().Where(x => x.So_May == somay && x.Week == week).ToList();
            var result = db.DA_Injection.Where(x => x.Week == week && x.Statuss == "true").FirstOrDefault();
            //reset flag để tính lại public_time
            foreach(var i in list)
            {
                i.FlagDem = false;
                i.FlagNgay = false;
            }

            if(somay == somay_start)
            {
                //Lấy public_time theo số máy lúc start
                foreach (var p in list)
                {
                    if (p.Code == result.Code && p.Statuss == "true")
                    {
                        p.Plan_D_ = result.Plan_D_;
                        p.Plan_Time_D_ = result.Plan_Time_D_;
                        p.FlagDem = true;
                        db.SaveChanges();
                        foreach (var j in list)
                        {
                            if (j.Date == result.Date)
                            {
                                j.FlagDem = true;
                                db.SaveChanges();
                            }
                        }
                        var sumkhbs = db.DA_Injection.Where(x => x.Code == result.Code && x.PSI == result.PSI && x.Week == result.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                        kehoach = (int)((result.Ke_Hoach + sumkhbs) - result.Plan_D_);

                        var status = 1;

                        foreach (var i in list)
                        {
                            double temp = 12 * Convert.ToDouble(i.Capa);
                            int max = Convert.ToInt32(Math.Ceiling(temp));
                            if (i.Date >= result.Date)
                            {
                                if (kehoach > 0)
                                {
                                    if (status == 1)
                                    {
                                        if (kehoach <= max)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);

                                            i.C_Plan_Time_N_ = hours;
                                            i.Plan_N_ = kehoach;
                                            if (kehoach == max)
                                            {
                                                i.FlagNgay = true;
                                                var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                foreach (var j in list2)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = 0;
                                            }
                                            else
                                            {
                                                public_time = hours + 2;
                                                if (public_time >= 12)
                                                {
                                                    var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list2)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }

                                            db.SaveChanges();
                                            kehoach = 0;
                                            break;
                                        }

                                        if (kehoach > max)
                                        {
                                            kehoach = kehoach - max;
                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);

                                            i.C_Plan_Time_N_ = hours;
                                            i.Plan_N_ = max;
                                            i.FlagNgay = true;
                                            var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                            foreach (var j in list2)
                                            {
                                                j.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            db.SaveChanges();
                                        }
                                        status = 0;
                                    }
                                    else
                                    {
                                        if (kehoach <= max)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);

                                            i.Plan_Time_D_ = hours;
                                            i.Plan_D_ = kehoach;
                                            if (kehoach == max)
                                            {
                                                i.FlagDem = true;
                                                var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                foreach (var j in list2)
                                                {
                                                    j.FlagDem = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = 0;
                                            }
                                            else
                                            {
                                                public_time = hours + 2;
                                                if (public_time >= 12)
                                                {
                                                    var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list2)
                                                    {
                                                        j.FlagDem = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }
                                            db.SaveChanges();
                                            kehoach = 0;
                                            break;
                                        }

                                        if (kehoach > max)
                                        {
                                            kehoach = kehoach - max;
                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);

                                            i.Plan_Time_D_ = hours;
                                            i.Plan_D_ = max;
                                            i.FlagDem = true;
                                            var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                            foreach (var j in list2)
                                            {
                                                j.FlagDem = true;
                                                db.SaveChanges();
                                            }
                                            db.SaveChanges();
                                        }

                                        if (kehoach <= max)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);

                                            i.C_Plan_Time_N_ = hours;
                                            i.Plan_N_ = kehoach;
                                            if (kehoach == max)
                                            {
                                                i.FlagNgay = true;
                                                var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                foreach (var j in list2)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = 0;
                                            }
                                            else
                                            {
                                                public_time = hours + 2;
                                                if (public_time >= 12)
                                                {
                                                    var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list2)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }
                                            db.SaveChanges();
                                            kehoach = 0;
                                            break;
                                        }

                                        if (kehoach > max)
                                        {
                                            kehoach = kehoach - max;
                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);

                                            i.C_Plan_Time_N_ = hours;
                                            i.Plan_N_ = max;
                                            i.FlagNgay = true;
                                            var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                            foreach (var j in list2)
                                            {
                                                j.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }
                        }

                        //update cung may
                        var code_temp = "";
                        foreach (var k in list)
                        {
                            var flag = 0;
                            if (k.FlagDem == false)
                            {
                                if (k.Code != result.Code)
                                {
                                    if (k.Code != code_temp)
                                    {
                                        flag = 1;
                                    }

                                    if (flag == 1)
                                    {
                                        var time_private = 12 - public_time;
                                        double plan_private = time_private * Convert.ToDouble(k.Capa);
                                        plan_private = (int)Math.Ceiling(plan_private);
                                        sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                        kehoach = (int)k.Ke_Hoach + sumkhbs;

                                        if (kehoach > 0)
                                        {
                                            if (plan_private <= kehoach)
                                            {
                                                kehoach = kehoach - (int)plan_private;
                                                k.Plan_D_ = (int)plan_private;
                                                k.Plan_Time_D_ = time_private;
                                                k.FlagDem = true;
                                                public_time = 0;
                                                db.SaveChanges();
                                                var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list2)
                                                {
                                                    j.FlagDem = true;
                                                    db.SaveChanges();
                                                }

                                                double temp = 12 * Convert.ToDouble(k.Capa);
                                                int max = Convert.ToInt32(Math.Ceiling(temp));
                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                                    k.C_Plan_Time_N_ = hours;
                                                    k.Plan_N_ = kehoach;
                                                    if (kehoach == max)
                                                    {
                                                        k.FlagNgay = true;
                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list_)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = public_time + hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                    db.SaveChanges();

                                                    kehoach = 0;
                                                    code_temp = k.Code;
                                                    continue;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);

                                                    k.C_Plan_Time_N_ = hours;
                                                    k.Plan_N_ = max;
                                                    k.FlagNgay = true;
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                    code_temp = k.Code;
                                                    continue;
                                                }
                                            }

                                            if (plan_private > kehoach)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                                public_time = time_private + 2;
                                                if (public_time >= 12)
                                                {
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagDem = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                                plan_private = kehoach;
                                                k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                                k.Plan_Time_D_ = hours;
                                                db.SaveChanges();
                                                kehoach = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (kehoach > 0)
                                        {
                                            double temp = 12 * Convert.ToDouble(k.Capa);
                                            int max = Convert.ToInt32(Math.Ceiling(temp));
                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                                k.Plan_Time_D_ = hours;
                                                k.Plan_D_ = kehoach;
                                                if (kehoach == max)
                                                {
                                                    k.FlagDem = true;
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagDem = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = public_time + hours + 2;
                                                    if (public_time >= 12)
                                                    {
                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list_)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                                kehoach = 0;
                                                code_temp = k.Code;
                                                continue;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);

                                                k.Plan_Time_D_ = hours;
                                                k.Plan_D_ = max;
                                                k.FlagDem = true;
                                                var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list2)
                                                {
                                                    j.FlagDem = true;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }

                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                                k.C_Plan_Time_N_ = hours;
                                                k.Plan_N_ = kehoach;
                                                if (kehoach == max)
                                                {
                                                    k.FlagNgay = true;
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = public_time + hours + 2;
                                                    if (public_time >= 12)
                                                    {
                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list_)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();
                                                kehoach = 0;
                                                code_temp = k.Code;
                                                continue;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);

                                                k.C_Plan_Time_N_ = hours;
                                                k.Plan_N_ = max;
                                                k.FlagNgay = true;
                                                var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list2)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                    code_temp = k.Code;
                                    continue;
                                }
                            }

                            /********************/

                            if (k.FlagNgay == false)
                            {
                                if (k.Code != result.Code)
                                {
                                    if (k.Code != code_temp)
                                    {
                                        flag = 1;
                                    }

                                    if (flag == 1)
                                    {
                                        var time_private = 12 - public_time;
                                        double plan_private = time_private * Convert.ToDouble(k.Capa);
                                        plan_private = (int)Math.Ceiling(plan_private);
                                        sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                        kehoach = (int)k.Ke_Hoach + sumkhbs;

                                        if (kehoach > 0)
                                        {
                                            if (plan_private <= kehoach)
                                            {
                                                kehoach = kehoach - (int)plan_private;
                                                k.Plan_N_ = (int)plan_private;
                                                k.C_Plan_Time_N_ = time_private;
                                                k.FlagNgay = true;
                                                public_time = 0;
                                                db.SaveChanges();
                                                var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list2)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                code_temp = k.Code;
                                                continue;
                                            }

                                            if (plan_private > kehoach)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                                plan_private = kehoach;
                                                k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                                k.C_Plan_Time_N_ = hours;
                                                db.SaveChanges();
                                                kehoach = 0;
                                                public_time = public_time + hours + 2;
                                                if (public_time >= 12)
                                                {
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }
                                        }
                                    }
                                    code_temp = k.Code;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                //Lấy public_time theo may khac may start
                foreach (var k in list)
                {
                    var flag = 0;
                    if (k.FlagDem == false)
                    {
                        if (k.Code != code_temp1)
                        {
                            flag = 1;
                        }

                        if (flag == 1)
                        {
                            var time_private = 12 - public_time;

                            double plan_private = time_private * Convert.ToDouble(k.Capa);
                            plan_private = (int)Math.Ceiling(plan_private);
                            var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                            kehoach = (int)k.Ke_Hoach + sumkhbs;
                            if (kehoach > 0)
                            {
                                if (plan_private <= kehoach)
                                {
                                    kehoach = kehoach - (int)plan_private;
                                    k.Plan_D_ = (int)plan_private;
                                    k.Plan_Time_D_ = time_private;
                                    k.FlagDem = true;
                                    public_time = 0;
                                    foreach (var j in list)
                                    {
                                        if (k.Date == j.Date)
                                        {
                                            j.FlagDem = true;
                                        }
                                    }

                                    double temp = 12 * Convert.ToDouble(k.Capa);
                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                    if (kehoach <= max)
                                    {
                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = kehoach;
                                        if (kehoach == max)
                                        {
                                            k.FlagNgay = true;
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagNgay = true;
                                                }
                                            }
                                            public_time = 0;
                                        }
                                        else
                                        {
                                            public_time = public_time + hours + 2;
                                            if (public_time >= 12)
                                            {
                                                foreach (var j in list)
                                                {
                                                    if (k.Date == j.Date)
                                                    {
                                                        j.FlagNgay = true;
                                                    }
                                                }
                                                public_time = public_time - 12;
                                            }
                                        }

                                        kehoach = 0;
                                        code_temp1 = k.Code;
                                        continue;
                                    }

                                    if (kehoach > max)
                                    {
                                        kehoach = kehoach - max;
                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);

                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = max;
                                        k.FlagNgay = true;
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagNgay = true;
                                            }
                                        }

                                        code_temp1 = k.Code;
                                        continue;
                                    }
                                }

                                if (plan_private > kehoach)
                                {
                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                    public_time = public_time + hours + 2;
                                    if (public_time >= 12)
                                    {
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagDem = true;
                                            }
                                        }
                                        public_time = public_time - 12;
                                    }
                                    plan_private = kehoach;
                                    k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                    k.Plan_Time_D_ = hours;
                                    kehoach = 0;
                                }
                            }
                        }
                        else
                        {
                            if (kehoach > 0)
                            {
                                double temp = 12 * Convert.ToDouble(k.Capa);
                                int max = Convert.ToInt32(Math.Ceiling(temp));
                                if (kehoach <= max)
                                {
                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                    k.Plan_Time_D_ = hours;
                                    k.Plan_D_ = kehoach;
                                    if (kehoach == max)
                                    {
                                        k.FlagDem = true;
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagDem = true;
                                            }
                                        }
                                        public_time = 0;
                                    }
                                    else
                                    {
                                        public_time = public_time + hours + 2;
                                        if (public_time >= 12)
                                        {
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagDem = true;
                                                }
                                            }
                                            public_time = public_time - 12;
                                        }
                                    }

                                    kehoach = 0;
                                    code_temp1 = k.Code;
                                    continue;
                                }

                                if (kehoach > max)
                                {
                                    kehoach = kehoach - max;
                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);

                                    k.Plan_Time_D_ = hours;
                                    k.Plan_D_ = max;
                                    k.FlagDem = true;
                                    foreach (var j in list)
                                    {
                                        if (k.Date == j.Date)
                                        {
                                            j.FlagDem = true;
                                        }
                                    }

                                }

                                if (kehoach <= max)
                                {
                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                    k.C_Plan_Time_N_ = hours;
                                    k.Plan_N_ = kehoach;
                                    if (kehoach == max)
                                    {
                                        k.FlagNgay = true;
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagNgay = true;
                                            }
                                        }
                                        public_time = 0;
                                    }
                                    else
                                    {
                                        public_time = public_time + hours + 2;
                                        if (public_time >= 12)
                                        {
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagNgay = true;
                                                }
                                            }
                                            public_time = public_time - 12;
                                        }
                                    }

                                    kehoach = 0;
                                    code_temp1 = k.Code;
                                    continue;
                                }

                                if (kehoach > max)
                                {
                                    kehoach = kehoach - max;
                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);

                                    k.C_Plan_Time_N_ = hours;
                                    k.Plan_N_ = max;
                                    k.FlagNgay = true;
                                    foreach (var j in list)
                                    {
                                        if (k.Date == j.Date)
                                        {
                                            j.FlagNgay = true;
                                        }
                                    }

                                }
                            }
                        }
                        code_temp1 = k.Code;
                        continue;
                    }

                    /********************/

                    if (k.FlagNgay == false)
                    {
                        if (k.Code != code_temp1)
                        {
                            flag = 1;
                        }

                        if (flag == 1)
                        {
                            var time_private = 12 - public_time;
                            double plan_private = time_private * Convert.ToDouble(k.Capa);
                            plan_private = (int)Math.Ceiling(plan_private);
                            var sumkhbs = db.DA_Injection.AsNoTracking().Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                            kehoach = (int)k.Ke_Hoach + sumkhbs;
                            if (kehoach > 0)
                            {
                                if (plan_private <= kehoach)
                                {
                                    kehoach = kehoach - (int)plan_private;
                                    k.Plan_N_ = (int)plan_private;
                                    k.C_Plan_Time_N_ = time_private;
                                    k.FlagNgay = true;
                                    public_time = 0;
                                    foreach (var j in list)
                                    {
                                        j.FlagNgay = true;
                                    }
                                    code_temp1 = k.Code;
                                    continue;
                                }

                                if (plan_private > kehoach)
                                {
                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);

                                    plan_private = kehoach;
                                    k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                    k.C_Plan_Time_N_ = hours;

                                    kehoach = 0;
                                    public_time = public_time + hours + 2;
                                    if (public_time >= 12)
                                    {
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagNgay = true;
                                            }
                                        }
                                        public_time = public_time - 12;
                                    }
                                }
                            }
                        }
                        code_temp1 = k.Code;
                    }
                }
            }
            
            
            //Đã có public_time

            var kq = db.DA_Injection.Where(x => x.So_May == somay && x.Week == week).ToList();
            double time_kehoach = 0;
            double sumtime_1may = 0;
            foreach (var j in kq)
            {
                if (j.FlagDem == true)
                {
                    var i = db.DA_Injection.Where(x => x.Code == code && x.PSI == psi && x.Week == week && x.Date == j.Date).FirstOrDefault();
                    time_kehoach = Convert.ToDouble(i.Ke_Hoach) / Convert.ToDouble(i.Capa);
                    //Tính tổng thời gian của 1 máy
                    sumtime_1may = 0;
                    if (somay != somay_start)
                    {
                        sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                    }
                    else
                    {
                        var temp = 12 - plantime;
                        sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                        sumtime_1may -= temp;
                    }
                    if (time_kehoach <= sumtime_1may)
                    {
                        i.FlagDem = true;
                        db.SaveChanges();
                        co = false;
                    }
                    else
                    {
                        co = true;
                    }
                }
                if (j.FlagNgay == true)
                {
                    var i = db.DA_Injection.Where(x => x.Code == code && x.PSI == psi && x.Week == week && x.Date == j.Date).FirstOrDefault();
                    time_kehoach = Convert.ToDouble(i.Ke_Hoach) / Convert.ToDouble(i.Capa);
                    //Tính tổng thời gian của 1 máy
                    sumtime_1may = 0;
                    if (somay != somay_start)
                    {
                        sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                    }
                    else
                    {
                        var temp = 12 - plantime;
                        sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                        sumtime_1may -= temp;
                    }
                    if (time_kehoach <= sumtime_1may)
                    {
                        i.FlagNgay = true;
                        db.SaveChanges();
                        co = false;
                    }
                    else
                    {
                        co = true;
                    }
                }


                if (j.FlagDem == false)
                {
                    var k = db.DA_Injection.Where(x => x.Code == code && x.PSI == psi && x.Week == week && x.Date == j.Date).FirstOrDefault();
                    time_kehoach = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                    //Tính tổng thời gian của 1 máy
                    sumtime_1may = 0;
                    if (somay != somay_start)
                    {
                        sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                    }
                    else
                    {
                        var temp = 12 - plantime;
                        sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                        sumtime_1may -= temp;
                    }
                    if (time_kehoach <= sumtime_1may)
                    {
                        co = false;
                        var time_private = 12 - public_time;
                        double plan_private = time_private * Convert.ToDouble(k.Capa);
                        plan_private = (int)Math.Ceiling(plan_private);
                        if (kehoach_edit > 0)
                        {
                            if (plan_private <= kehoach_edit)
                            {
                                kehoach_edit = kehoach_edit - (int)plan_private;
                                k.Plan_D_ = (int)plan_private;
                                k.Plan_Time_D_ = time_private;
                                k.FlagDem = true;
                                k.STT = stt;
                                stt++;
                                public_time = 0;
                                db.SaveChanges();
                                var list_ = db.DA_Injection.Where(x => x.So_May == somay && x.Date == k.Date && x.Week == k.Week).ToList();
                                foreach (var o in list_)
                                {
                                    o.FlagDem = true;
                                    db.SaveChanges();
                                }

                                double temp = 12 * Convert.ToDouble(k.Capa);
                                int max = Convert.ToInt32(Math.Ceiling(temp));
                                if (kehoach_edit <= max)
                                {
                                    double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                    
                                    k.C_Plan_Time_N_ = hours;
                                    k.Plan_N_ = kehoach_edit;
                                    k.STT = stt;
                                    stt++;
                                    if (kehoach_edit == max)
                                    {
                                        k.FlagNgay = true;
                                        var _list_ = db.DA_Injection.Where(x => x.So_May == somay && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in _list_)
                                        {
                                            o.FlagNgay = true;
                                            db.SaveChanges();
                                        }
                                        public_time = 0;
                                    }
                                    else
                                    {
                                        public_time = public_time + hours + 2;
                                        if (public_time >= 12)
                                        {
                                            var _list_ = db.DA_Injection.Where(x => x.So_May == somay && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var o in _list_)
                                            {
                                                o.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            public_time = public_time - 12;
                                        }
                                    }
                                    db.SaveChanges();
                                    kehoach_edit = 0;
                                    continue;
                                }

                                if (kehoach_edit > max)
                                {
                                    kehoach_edit = kehoach_edit - max;
                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                    
                                    k.C_Plan_Time_N_ = hours;
                                    k.Plan_N_ = max;
                                    k.FlagNgay = true;
                                    k.STT = stt;
                                    stt++;
                                    var _list_ = db.DA_Injection.Where(x => x.So_May == somay && x.Date == k.Date && x.Week == k.Week).ToList();
                                    foreach (var o in _list_)
                                    {
                                        o.FlagNgay = true;
                                        db.SaveChanges();
                                    }
                                    db.SaveChanges();
                                    continue;
                                }
                            }

                            if (plan_private > kehoach_edit)
                            {
                                double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                
                                public_time = public_time + time_private + 2;
                                if (public_time >= 12)
                                {
                                    var list_ = db.DA_Injection.Where(x => x.So_May == somay && x.Date == k.Date && x.Week == k.Week).ToList();
                                    foreach (var o in list_)
                                    {
                                        o.FlagDem = true;
                                        db.SaveChanges();
                                    }
                                    public_time = public_time - 12;
                                }
                                plan_private = kehoach_edit;
                                k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                k.Plan_Time_D_ = hours;
                                k.STT = stt;
                                stt++;
                                db.SaveChanges();
                                kehoach_edit = 0;
                            }
                        }
                    }
                    else
                    {
                        co = true;
                    }
                }
                else
                {
                    if (j.FlagNgay == false)
                    {
                        var k = db.DA_Injection.Where(x => x.Code == code && x.PSI == psi && x.Week == week && x.Date == j.Date).FirstOrDefault();
                        time_kehoach = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                        //Tính tổng thời gian của 1 máy
                        sumtime_1may = 0;
                        if (somay != somay_start)
                        {
                            sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                        }
                        else
                        {
                            var temp = 12 - plantime;
                            sumtime_1may = SumTime_1May(Convert.ToInt32(j.So_May), j.Week);
                            sumtime_1may -= temp;
                        }
                        if (time_kehoach <= sumtime_1may)
                        {
                            co = false;
                            var time_private = 12 - public_time;
                            double plan_private = time_private * Convert.ToDouble(k.Capa);
                            plan_private = (int)Math.Ceiling(plan_private);
                            if (kehoach_edit > 0)
                            {
                                if (plan_private <= kehoach_edit)
                                {
                                    kehoach_edit = kehoach_edit - (int)plan_private;
                                    k.Plan_N_ = (int)plan_private;
                                    k.C_Plan_Time_N_ = time_private;
                                    k.FlagNgay = true;
                                    k.STT = stt;
                                    stt++;
                                    public_time = 0;
                                    db.SaveChanges();
                                    var list_ = db.DA_Injection.Where(x => x.So_May == somay && x.Date == k.Date && x.Week == k.Week).ToList();
                                    foreach (var o in list_)
                                    {
                                        o.FlagNgay = true;
                                        db.SaveChanges();
                                    }
                                    code_temp1 = k.Code;
                                    continue;
                                }

                                if (plan_private > kehoach_edit)
                                {
                                    double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                    
                                    plan_private = kehoach_edit;
                                    k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                    k.C_Plan_Time_N_ = hours;
                                    k.STT = stt;
                                    stt++;
                                    db.SaveChanges();
                                    kehoach_edit = 0;
                                    public_time = public_time + hours + 2;
                                    if (public_time >= 12)
                                    {
                                        var _list_ = db.DA_Injection.Where(x => x.So_May == somay && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in _list_)
                                        {
                                            o.FlagNgay = true;
                                            db.SaveChanges();
                                        }
                                        public_time = public_time - 12;
                                    }
                                }
                            }
                        }
                        else
                        {
                            co = true;
                        }
                    }
                }
            }

            if (co == false)
            {
                var rel = db.DA_Injection.Where(x => x.Code == code && x.PSI == psi && x.Week == week).ToList();
                foreach(var p in rel)
                {
                    p.So_May = somay;
                    db.SaveChanges();
                }
            }

            return Json(co,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCodeChanged(string code,string week,string psi,int somay)
        {
            var data = db.DA_Injection.Where(x => x.Code == code && x.Week == week && x.PSI == psi && x.So_May == somay).ToList();
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTimeOption2(int somay, string week,int somay_start,double plantime)
        {
            double time = 0;
            if (somay != somay_start)
            {
                time = SumTime_1May(Convert.ToInt32(somay), week);
            }
            else
            {
                var temp = 12 - plantime;
                time = SumTime_1May(Convert.ToInt32(somay), week); ;
                time -= temp;
            }
            return Json(time, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [HasCredentialAttribute.HasCredential(RoleID = "EDIT_KEHOACH")]
        public JsonResult EditPlan(string code, string week, int somay,string strdate,int kehoach_edit,string psi,int maychaybosung)
        {
            bool co = false;
            var stt = 0;
            var tam = db.DA_Injection.OrderByDescending(x=>x.STT).FirstOrDefault(x => x.Week == week);
            stt = (int)tam.STT + 1;
            var result = db.DA_Injection.Where(x => x.Week == week && x.Statuss == "true").FirstOrDefault();
            if (somay == maychaybosung)
            {
                if(maychaybosung != result.So_May)
                {
                    var listsomay = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Week == week).ToList();
                    if (listsomay != null)
                    {
                        foreach (var i in listsomay)
                        {
                            i.FlagDem = false;
                            i.FlagNgay = false;
                            db.SaveChanges();
                        }

                        var code_temp = "";
                        int kehoach = 0;
                        double public_time = 0;
                        foreach (var k in listsomay)
                        {
                            var flag = 0;
                            if (k.FlagDem == false)
                            {
                                if (k.Code != code_temp)
                                {
                                    flag = 1;
                                }

                                if (flag == 1)
                                {
                                    double time_private = 12 - public_time;
                                    double plan_private = time_private * Convert.ToDouble(k.Capa);
                                    plan_private = (int)Math.Ceiling(plan_private);
                                    var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                    if (k.Code == code)
                                    {
                                        var sumtemp = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Plan_D_ + x.Plan_N_).Value;
                                        kehoach = sumtemp + kehoach_edit;
                                    }
                                    else
                                    {
                                        kehoach = (int)k.Ke_Hoach + sumkhbs;
                                    }

                                    if (kehoach > 0)
                                    {
                                        if (plan_private <= kehoach)
                                        {
                                            kehoach = kehoach - (int)plan_private;
                                            k.Plan_D_ = (int)plan_private;
                                            k.Plan_Time_D_ = time_private;
                                            k.FlagDem = true;
                                            public_time = 0;
                                            db.SaveChanges();
                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var j in list)
                                            {
                                                j.FlagDem = true;
                                                
                                                db.SaveChanges();
                                            }

                                            double temp = 12 * Convert.ToDouble(k.Capa);
                                            int max = Convert.ToInt32(Math.Ceiling(temp));
                                            if (kehoach <= max)
                                            {
                                                double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                
                                                k.C_Plan_Time_N_ = hours;
                                                k.Plan_N_ = kehoach;
                                                if (kehoach == max)
                                                {
                                                    k.FlagNgay = true;
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = 0;
                                                }
                                                else
                                                {
                                                    public_time = public_time + hours + 2;
                                                    if (public_time >= 12)
                                                    {
                                                        var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list_)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = public_time - 12;
                                                    }
                                                }
                                                db.SaveChanges();

                                                kehoach = 0;
                                                code_temp = k.Code;
                                                continue;
                                            }

                                            if (kehoach > max)
                                            {
                                                kehoach = kehoach - max;
                                                double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                
                                                k.C_Plan_Time_N_ = hours;
                                                k.Plan_N_ = max;
                                                k.FlagNgay = true;
                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list_)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                db.SaveChanges();
                                                code_temp = k.Code;
                                                continue;
                                            }
                                        }

                                        if (plan_private > kehoach)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                            
                                            public_time = time_private + 2;
                                            if (public_time >= 12)
                                            {
                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list_)
                                                {
                                                    j.FlagDem = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = public_time - 12;
                                            }
                                            plan_private = kehoach;
                                            k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                            k.Plan_Time_D_ = hours;
                                            db.SaveChanges();
                                            kehoach = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    if (kehoach > 0)
                                    {
                                        double temp = 12 * Convert.ToDouble(k.Capa);
                                        int max = Convert.ToInt32(Math.Ceiling(temp));
                                        if (kehoach <= max)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                            
                                            k.Plan_Time_D_ = hours;
                                            k.Plan_D_ = kehoach;
                                            if (kehoach == max)
                                            {
                                                k.FlagDem = true;
                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list_)
                                                {
                                                    j.FlagDem = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = 0;
                                            }
                                            else
                                            {
                                                public_time = public_time + hours + 2;
                                                if (public_time >= 12)
                                                {
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagDem = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }
                                            db.SaveChanges();
                                            kehoach = 0;
                                            code_temp = k.Code;
                                            continue;
                                        }

                                        if (kehoach > max)
                                        {
                                            kehoach = kehoach - max;
                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                            
                                            k.Plan_Time_D_ = hours;
                                            k.Plan_D_ = max;
                                            k.FlagDem = true;
                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var j in list)
                                            {
                                                j.FlagDem = true;
                                                db.SaveChanges();
                                            }
                                            db.SaveChanges();
                                        }

                                        if (kehoach <= max)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                            
                                            k.C_Plan_Time_N_ = hours;
                                            k.Plan_N_ = kehoach;
                                            if (kehoach == max)
                                            {
                                                k.FlagNgay = true;
                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list_)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = 0;
                                            }
                                            else
                                            {
                                                public_time = public_time + hours + 2;
                                                if (public_time >= 12)
                                                {
                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                    foreach (var j in list_)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }
                                            db.SaveChanges();
                                            kehoach = 0;
                                            code_temp = k.Code;
                                            continue;
                                        }

                                        if (kehoach > max)
                                        {
                                            kehoach = kehoach - max;
                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                            
                                            k.C_Plan_Time_N_ = hours;
                                            k.Plan_N_ = max;
                                            k.FlagNgay = true;
                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var j in list)
                                            {
                                                j.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            db.SaveChanges();
                                        }
                                    }
                                }
                                code_temp = k.Code;
                                continue;
                            }

                            /********************/

                            if (k.FlagNgay == false)
                            {
                                if (k.Code != code_temp)
                                {
                                    flag = 1;
                                }

                                if (flag == 1)
                                {
                                    double time_private = 12 - public_time;
                                    double plan_private = time_private * Convert.ToDouble(k.Capa);
                                    plan_private = (int)Math.Ceiling(plan_private);
                                    var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                    if (k.Code == code)
                                    {
                                        var sumtemp = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Plan_D_ + x.Plan_N_).Value;
                                        kehoach = sumtemp + kehoach_edit;
                                    }
                                    else
                                    {
                                        kehoach = (int)k.Ke_Hoach + sumkhbs;
                                    }

                                    if (kehoach > 0)
                                    {
                                        if (plan_private <= kehoach)
                                        {
                                            kehoach = kehoach - (int)plan_private;
                                            k.Plan_N_ = (int)plan_private;
                                            k.C_Plan_Time_N_ = time_private;
                                            k.FlagNgay = true;
                                            public_time = 0;
                                            db.SaveChanges();
                                            var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var j in list)
                                            {
                                                j.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            code_temp = k.Code;
                                            continue;
                                        }

                                        if (plan_private > kehoach)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                            
                                            plan_private = kehoach;
                                            k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                            k.C_Plan_Time_N_ = hours;
                                            db.SaveChanges();
                                            kehoach = 0;
                                            public_time = public_time + hours + 2;
                                            if (public_time >= 12)
                                            {
                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var j in list_)
                                                {
                                                    j.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = public_time - 12;
                                            }
                                        }
                                    }
                                }
                                code_temp = k.Code;
                            }
                        }
                    }
                }
                else
                {
                    var listsomay = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Week == week && x.PSI == psi).ToList();
                    double public_time = 0;
                    if(listsomay != null)
                    {
                        int kehoach = 0;
                        foreach (var i in listsomay)
                        {
                            i.FlagDem = false;
                            i.FlagNgay = false;
                            db.SaveChanges();
                        }

                        foreach (var p in listsomay)
                        {
                            if (p.Code == result.Code && p.Statuss == "true")
                            {
                                p.Plan_D_ = result.Plan_D_;
                                p.Plan_Time_D_ = result.Plan_Time_D_;
                                p.FlagDem = true;
                                db.SaveChanges();
                                foreach (var j in listsomay)
                                {
                                    if (j.Date == result.Date)
                                    {
                                        j.FlagDem = true;
                                        db.SaveChanges();
                                    }
                                }
                                var sumkhbs = db.DA_Injection.Where(x => x.Code == result.Code && x.PSI == result.PSI && x.Week == result.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                if(p.Code == code)
                                {
                                    kehoach = (int)((result.Ke_Hoach + sumkhbs + kehoach_edit) - result.Plan_D_);
                                }
                                else
                                {
                                    kehoach = (int)((result.Ke_Hoach + sumkhbs) - result.Plan_D_);
                                }
                                
                                var status = 1;

                                foreach (var i in listsomay)
                                {
                                    double temp = 12 * Convert.ToDouble(i.Capa);
                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                    if (i.Date >= result.Date)
                                    {
                                        if (kehoach > 0)
                                        {
                                            if (status == 1)
                                            {
                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = kehoach;
                                                    if (kehoach == max)
                                                    {
                                                        i.FlagNgay = true;
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                            foreach (var j in list)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }

                                                    db.SaveChanges();
                                                    kehoach = 0;
                                                    break;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = max;
                                                    i.FlagNgay = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                }
                                                status = 0;
                                            }
                                            else
                                            {
                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.Plan_Time_D_ = hours;
                                                    i.Plan_D_ = kehoach;
                                                    if (kehoach == max)
                                                    {
                                                        i.FlagDem = true;
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                            foreach (var j in list)
                                                            {
                                                                j.FlagDem = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                    db.SaveChanges();
                                                    kehoach = 0;
                                                    break;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.Plan_Time_D_ = hours;
                                                    i.Plan_D_ = max;
                                                    i.FlagDem = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagDem = true;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                }

                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = kehoach;
                                                    if (kehoach == max)
                                                    {
                                                        i.FlagNgay = true;
                                                        var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                            foreach (var j in list)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                    db.SaveChanges();
                                                    kehoach = 0;
                                                    break;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = max;
                                                    i.FlagNgay = true;
                                                    var list = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                                    foreach (var j in list)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }

                                //update cung may
                                var code_temp = "";
                                foreach (var k in listsomay)
                                {
                                    var flag = 0;
                                    if (k.FlagDem == false)
                                    {
                                        if (k.Code != result.Code)
                                        {
                                            if (k.Code != code_temp)
                                            {
                                                flag = 1;
                                            }

                                            if (flag == 1)
                                            {
                                                var time_private = 12 - public_time;
                                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                                plan_private = (int)Math.Ceiling(plan_private);
                                                sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                                if(k.Code == code)
                                                {
                                                    var sumtemp = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Plan_D_ + x.Plan_N_).Value;
                                                    kehoach = sumtemp + kehoach_edit;
                                                }
                                                else
                                                {
                                                    kehoach = (int)k.Ke_Hoach + sumkhbs;
                                                }
                                                
                                                if (kehoach > 0)
                                                {
                                                    if (plan_private <= kehoach)
                                                    {
                                                        kehoach = kehoach - (int)plan_private;
                                                        k.Plan_D_ = (int)plan_private;
                                                        k.Plan_Time_D_ = time_private;
                                                        k.FlagDem = true;
                                                        public_time = 0;
                                                        db.SaveChanges();
                                                        var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }

                                                        double temp = 12 * Convert.ToDouble(k.Capa);
                                                        int max = Convert.ToInt32(Math.Ceiling(temp));
                                                        if (kehoach <= max)
                                                        {
                                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.C_Plan_Time_N_ = hours;
                                                            k.Plan_N_ = kehoach;
                                                            if (kehoach == max)
                                                            {
                                                                k.FlagNgay = true;
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagNgay = true;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = 0;
                                                            }
                                                            else
                                                            {
                                                                public_time = public_time + hours + 2;
                                                                if (public_time >= 12)
                                                                {
                                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                    foreach (var j in list_)
                                                                    {
                                                                        j.FlagNgay = true;
                                                                        db.SaveChanges();
                                                                    }
                                                                    public_time = public_time - 12;
                                                                }
                                                            }
                                                            db.SaveChanges();

                                                            kehoach = 0;
                                                            code_temp = k.Code;
                                                            continue;
                                                        }

                                                        if (kehoach > max)
                                                        {
                                                            kehoach = kehoach - max;
                                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.C_Plan_Time_N_ = hours;
                                                            k.Plan_N_ = max;
                                                            k.FlagNgay = true;
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            db.SaveChanges();
                                                            code_temp = k.Code;
                                                            continue;
                                                        }
                                                    }

                                                    if (plan_private > kehoach)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        public_time = time_private + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagDem = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                        plan_private = kehoach;
                                                        k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                                        k.Plan_Time_D_ = hours;
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (kehoach > 0)
                                                {
                                                    double temp = 12 * Convert.ToDouble(k.Capa);
                                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                                    if (kehoach <= max)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.Plan_Time_D_ = hours;
                                                        k.Plan_D_ = kehoach;
                                                        if (kehoach == max)
                                                        {
                                                            k.FlagDem = true;
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagDem = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = 0;
                                                        }
                                                        else
                                                        {
                                                            public_time = public_time + hours + 2;
                                                            if (public_time >= 12)
                                                            {
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagDem = true;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = public_time - 12;
                                                            }
                                                        }
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                        code_temp = k.Code;
                                                        continue;
                                                    }

                                                    if (kehoach > max)
                                                    {
                                                        kehoach = kehoach - max;
                                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.Plan_Time_D_ = hours;
                                                        k.Plan_D_ = max;
                                                        k.FlagDem = true;
                                                        var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }
                                                        db.SaveChanges();
                                                    }

                                                    if (kehoach <= max)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.C_Plan_Time_N_ = hours;
                                                        k.Plan_N_ = kehoach;
                                                        if (kehoach == max)
                                                        {
                                                            k.FlagNgay = true;
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = 0;
                                                        }
                                                        else
                                                        {
                                                            public_time = public_time + hours + 2;
                                                            if (public_time >= 12)
                                                            {
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagNgay = true;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = public_time - 12;
                                                            }
                                                        }
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                        code_temp = k.Code;
                                                        continue;
                                                    }

                                                    if (kehoach > max)
                                                    {
                                                        kehoach = kehoach - max;
                                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.C_Plan_Time_N_ = hours;
                                                        k.Plan_N_ = max;
                                                        k.FlagNgay = true;
                                                        var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        db.SaveChanges();
                                                    }
                                                }
                                            }
                                            code_temp = k.Code;
                                            continue;
                                        }
                                    }

                                    /********************/

                                    if (k.FlagNgay == false)
                                    {
                                        if (k.Code != result.Code)
                                        {
                                            if (k.Code != code_temp)
                                            {
                                                flag = 1;
                                            }

                                            if (flag == 1)
                                            {
                                                var time_private = 12 - public_time;
                                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                                plan_private = (int)Math.Ceiling(plan_private);
                                                sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                                if(k.Code == code)
                                                {
                                                    var sumtemp = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Plan_D_ + x.Plan_N_).Value;
                                                    kehoach = sumtemp + kehoach_edit;
                                                }
                                                else
                                                {
                                                    kehoach = (int)k.Ke_Hoach + sumkhbs;
                                                }
                                                
                                                if (kehoach > 0)
                                                {
                                                    if (plan_private <= kehoach)
                                                    {
                                                        kehoach = kehoach - (int)plan_private;
                                                        k.Plan_N_ = (int)plan_private;
                                                        k.C_Plan_Time_N_ = time_private;
                                                        k.FlagNgay = true;
                                                        public_time = 0;
                                                        db.SaveChanges();
                                                        var list = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        code_temp = k.Code;
                                                        continue;
                                                    }

                                                    if (plan_private > kehoach)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        plan_private = kehoach;
                                                        k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                                        k.C_Plan_Time_N_ = hours;
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                        public_time = public_time + hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                }
                                            }
                                            code_temp = k.Code;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                double public_time = 0;
                var code_temp1 = "";
                var kehoach = 0;
                var list = db.DA_Injection.AsNoTracking().Where(x => x.So_May == maychaybosung && x.Week == week).ToList();

                //reset flag để tính lại public_time
                foreach (var i in list)
                {
                    i.FlagDem = false;
                    i.FlagNgay = false;
                }

                if(maychaybosung != result.So_May)
                {
                    //Lấy public_time
                    foreach (var k in list)
                    {
                        var flag = 0;
                        if (k.FlagDem == false)
                        {
                            if (k.Code != code_temp1)
                            {
                                flag = 1;
                            }

                            if (flag == 1)
                            {
                                var time_private = 12 - public_time;

                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                plan_private = (int)Math.Ceiling(plan_private);
                                var sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                kehoach = (int)k.Ke_Hoach + sumkhbs;
                                if (kehoach > 0)
                                {
                                    if (plan_private <= kehoach)
                                    {
                                        kehoach = kehoach - (int)plan_private;
                                        k.Plan_D_ = (int)plan_private;
                                        k.Plan_Time_D_ = time_private;
                                        k.FlagDem = true;
                                        public_time = 0;
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagDem = true;
                                            }
                                        }

                                        double temp = 12 * Convert.ToDouble(k.Capa);
                                        int max = Convert.ToInt32(Math.Ceiling(temp));
                                        if (kehoach <= max)
                                        {
                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                            
                                            k.C_Plan_Time_N_ = hours;
                                            k.Plan_N_ = kehoach;
                                            if (kehoach == max)
                                            {
                                                k.FlagNgay = true;
                                                foreach (var j in list)
                                                {
                                                    if (k.Date == j.Date)
                                                    {
                                                        j.FlagNgay = true;
                                                    }
                                                }
                                                public_time = 0;
                                            }
                                            else
                                            {
                                                public_time = public_time + hours + 2;
                                                if (public_time >= 12)
                                                {
                                                    foreach (var j in list)
                                                    {
                                                        if (k.Date == j.Date)
                                                        {
                                                            j.FlagNgay = true;
                                                        }
                                                    }
                                                    public_time = public_time - 12;
                                                }
                                            }

                                            kehoach = 0;
                                            code_temp1 = k.Code;
                                            continue;
                                        }

                                        if (kehoach > max)
                                        {
                                            kehoach = kehoach - max;
                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                            
                                            k.C_Plan_Time_N_ = hours;
                                            k.Plan_N_ = max;
                                            k.FlagNgay = true;
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagNgay = true;
                                                }
                                            }

                                            code_temp1 = k.Code;
                                            continue;
                                        }
                                    }

                                    if (plan_private > kehoach)
                                    {
                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                        
                                        public_time = public_time + hours + 2;
                                        if (public_time >= 12)
                                        {
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagDem = true;
                                                }
                                            }
                                            public_time = public_time - 12;
                                        }
                                        plan_private = kehoach;
                                        k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                        k.Plan_Time_D_ = hours;
                                        kehoach = 0;
                                    }
                                }
                            }
                            else
                            {
                                if (kehoach > 0)
                                {
                                    double temp = 12 * Convert.ToDouble(k.Capa);
                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                    if (kehoach <= max)
                                    {
                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                        
                                        k.Plan_Time_D_ = hours;
                                        k.Plan_D_ = kehoach;
                                        if (kehoach == max)
                                        {
                                            k.FlagDem = true;
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagDem = true;
                                                }
                                            }
                                            public_time = 0;
                                        }
                                        else
                                        {
                                            public_time = public_time + hours + 2;
                                            if (public_time >= 12)
                                            {
                                                foreach (var j in list)
                                                {
                                                    if (k.Date == j.Date)
                                                    {
                                                        j.FlagDem = true;
                                                    }
                                                }
                                                public_time = public_time - 12;
                                            }
                                        }

                                        kehoach = 0;
                                        code_temp1 = k.Code;
                                        continue;
                                    }

                                    if (kehoach > max)
                                    {
                                        kehoach = kehoach - max;
                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                        
                                        k.Plan_Time_D_ = hours;
                                        k.Plan_D_ = max;
                                        k.FlagDem = true;
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagDem = true;
                                            }
                                        }

                                    }

                                    if (kehoach <= max)
                                    {
                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                        
                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = kehoach;
                                        if (kehoach == max)
                                        {
                                            k.FlagNgay = true;
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagNgay = true;
                                                }
                                            }
                                            public_time = 0;
                                        }
                                        else
                                        {
                                            public_time = public_time + hours + 2;
                                            if (public_time >= 12)
                                            {
                                                foreach (var j in list)
                                                {
                                                    if (k.Date == j.Date)
                                                    {
                                                        j.FlagNgay = true;
                                                    }
                                                }
                                                public_time = public_time - 12;
                                            }
                                        }

                                        kehoach = 0;
                                        code_temp1 = k.Code;
                                        continue;
                                    }

                                    if (kehoach > max)
                                    {
                                        kehoach = kehoach - max;
                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                        
                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = max;
                                        k.FlagNgay = true;
                                        foreach (var j in list)
                                        {
                                            if (k.Date == j.Date)
                                            {
                                                j.FlagNgay = true;
                                            }
                                        }

                                    }
                                }
                            }
                            code_temp1 = k.Code;
                            continue;
                        }

                        /********************/

                        if (k.FlagNgay == false)
                        {
                            if (k.Code != code_temp1)
                            {
                                flag = 1;
                            }

                            if (flag == 1)
                            {
                                var time_private = 12 - public_time;
                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                plan_private = (int)Math.Ceiling(plan_private);
                                var sumkhbs = db.DA_Injection.AsNoTracking().Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                kehoach = (int)k.Ke_Hoach + sumkhbs;
                                if (kehoach > 0)
                                {
                                    if (plan_private <= kehoach)
                                    {
                                        kehoach = kehoach - (int)plan_private;
                                        k.Plan_N_ = (int)plan_private;
                                        k.C_Plan_Time_N_ = time_private;
                                        k.FlagNgay = true;
                                        public_time = 0;
                                        foreach (var j in list)
                                        {
                                            j.FlagNgay = true;
                                        }
                                        code_temp1 = k.Code;
                                        continue;
                                    }

                                    if (plan_private > kehoach)
                                    {
                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                        
                                        plan_private = kehoach;
                                        k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                        k.C_Plan_Time_N_ = hours;

                                        kehoach = 0;
                                        public_time = public_time + hours + 2;
                                        if (public_time >= 12)
                                        {
                                            foreach (var j in list)
                                            {
                                                if (k.Date == j.Date)
                                                {
                                                    j.FlagNgay = true;
                                                }
                                            }
                                            public_time = public_time - 12;
                                        }
                                    }
                                }
                            }
                            code_temp1 = k.Code;
                        }
                    }


                    //Đã có public_time

                    var kq = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Week == week).ToList();
                    var kh = kehoach_edit;
                    foreach (var j in kq)
                    {
                        if (j.FlagDem == false)
                        {
                            var k = db.DA_Injection.AsNoTracking().Where(x => x.Code == code && x.Week == week && x.Date == j.Date).FirstOrDefault();
                            k.Plan_D_ = 0;
                            k.Plan_Time_D_ = 0;
                            k.Plan_N_ = 0;
                            k.C_Plan_Time_N_ = 0;
                            k.Ke_Hoach = kh;
                            k.Ke_Hoach_Bo_Sung = 0;
                            k.Good_D_ = null;
                            k.Good_N_ = null;
                            k.FlagDem = false;
                            k.FlagNgay = false;
                            k.So_May = maychaybosung;

                            var time_private = 12 - public_time;
                            double plan_private = time_private * Convert.ToDouble(k.Capa);
                            plan_private = (int)Math.Ceiling(plan_private);
                            if (kehoach_edit > 0)
                            {
                                if (plan_private <= kehoach_edit)
                                {
                                    kehoach_edit = kehoach_edit - (int)plan_private;
                                    k.Plan_D_ = (int)plan_private;
                                    k.Plan_Time_D_ = time_private;
                                    k.FlagDem = true;
                                    k.STT = stt;
                                    stt++;
                                    public_time = 0;
                                    var list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                    foreach (var o in list_)
                                    {
                                        o.FlagDem = true;
                                        db.SaveChanges();
                                    }

                                    double temp = 12 * Convert.ToDouble(k.Capa);
                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                    if (kehoach_edit <= max)
                                    {
                                        double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                        
                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = kehoach_edit;
                                        k.STT = stt;
                                        stt++;
                                        if (kehoach_edit == max)
                                        {
                                            k.FlagNgay = true;
                                            var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var o in _list_)
                                            {
                                                o.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            public_time = 0;
                                        }
                                        else
                                        {
                                            public_time = public_time + hours + 2;
                                            if (public_time >= 12)
                                            {
                                                var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var o in _list_)
                                                {
                                                    o.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = public_time - 12;
                                            }
                                        }
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        kehoach_edit = 0;
                                        continue;
                                    }

                                    if (kehoach_edit > max)
                                    {
                                        kehoach_edit = kehoach_edit - max;
                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                        
                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = max;
                                        k.FlagNgay = true;
                                        k.STT = stt;
                                        stt++;
                                        var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in _list_)
                                        {
                                            o.FlagNgay = true;
                                            db.SaveChanges();
                                        }
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        continue;
                                    }
                                }

                                if (plan_private > kehoach_edit)
                                {
                                    double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                    
                                    public_time = public_time + time_private + 2;
                                    if (public_time >= 12)
                                    {
                                        var list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in list_)
                                        {
                                            o.FlagDem = true;
                                            db.SaveChanges();
                                        }
                                        public_time = public_time - 12;
                                    }
                                    plan_private = kehoach_edit;
                                    k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                    k.Plan_Time_D_ = hours;
                                    k.STT = stt;
                                    stt++;
                                    db.DA_Injection.Add(k);
                                    db.SaveChanges();
                                    kehoach_edit = 0;
                                }
                            }
                        }
                        else
                        {
                            if (j.FlagNgay == false)
                            {
                                var k = db.DA_Injection.AsNoTracking().Where(x => x.Code == code && x.Week == week && x.Date == j.Date).FirstOrDefault();
                                k.Plan_D_ = 0;
                                k.Plan_Time_D_ = 0;
                                k.Plan_N_ = 0;
                                k.C_Plan_Time_N_ = 0;
                                k.Ke_Hoach = kh;
                                k.Ke_Hoach_Bo_Sung = 0;
                                k.Good_D_ = null;
                                k.Good_N_ = null;
                                k.FlagDem = false;
                                k.FlagNgay = false;
                                k.So_May = maychaybosung;

                                var time_private = 12 - public_time;
                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                plan_private = (int)Math.Ceiling(plan_private);
                                if (kehoach_edit > 0)
                                {
                                    if (plan_private <= kehoach_edit)
                                    {
                                        kehoach_edit = kehoach_edit - (int)plan_private;
                                        k.Plan_N_ = (int)plan_private;
                                        k.C_Plan_Time_N_ = time_private;
                                        k.FlagNgay = true;
                                        k.STT = stt;
                                        stt++;
                                        public_time = 0;
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        var list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in list_)
                                        {
                                            o.FlagNgay = true;
                                            db.SaveChanges();
                                        }
                                        code_temp1 = k.Code;
                                        continue;
                                    }

                                    if (plan_private > kehoach_edit)
                                    {
                                        double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                        
                                        plan_private = kehoach_edit;
                                        k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                        k.C_Plan_Time_N_ = hours;
                                        k.STT = stt;
                                        stt++;
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        kehoach_edit = 0;
                                        public_time = public_time + hours + 2;
                                        if (public_time >= 12)
                                        {
                                            var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var o in _list_)
                                            {
                                                o.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            public_time = public_time - 12;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else //edit nếu số máy là số máy lúc start
                {
                    //Lay public_time theo luc start
                    public_time = 0;
                    if (list != null)
                    {
                        kehoach = 0;
                        foreach (var i in list)
                        {
                            i.FlagDem = false;
                            i.FlagNgay = false;
                            db.SaveChanges();
                        }

                        foreach (var p in list)
                        {
                            if (p.Code == result.Code && p.Statuss == "true")
                            {
                                p.Plan_D_ = result.Plan_D_;
                                p.Plan_Time_D_ = result.Plan_Time_D_;
                                p.FlagDem = true;
                                db.SaveChanges();
                                foreach (var j in list)
                                {
                                    if (j.Date == result.Date)
                                    {
                                        j.FlagDem = true;
                                        db.SaveChanges();
                                    }
                                }
                                var sumkhbs = db.DA_Injection.Where(x => x.Code == result.Code && x.PSI == result.PSI && x.Week == result.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                kehoach = (int)((result.Ke_Hoach + sumkhbs) - result.Plan_D_);

                                var status = 1;

                                foreach (var i in list)
                                {
                                    double temp = 12 * Convert.ToDouble(i.Capa);
                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                    if (i.Date >= result.Date)
                                    {
                                        if (kehoach > 0)
                                        {
                                            if (status == 1)
                                            {
                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = kehoach;
                                                    if (kehoach == max)
                                                    {
                                                        i.FlagNgay = true;
                                                        var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list2)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                            foreach (var j in list2)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }

                                                    db.SaveChanges();
                                                    kehoach = 0;
                                                    break;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = max;
                                                    i.FlagNgay = true;
                                                    var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                    foreach (var j in list2)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                }
                                                status = 0;
                                            }
                                            else
                                            {
                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.Plan_Time_D_ = hours;
                                                    i.Plan_D_ = kehoach;
                                                    if (kehoach == max)
                                                    {
                                                        i.FlagDem = true;
                                                        var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list2)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                            foreach (var j in list2)
                                                            {
                                                                j.FlagDem = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                    db.SaveChanges();
                                                    kehoach = 0;
                                                    break;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.Plan_Time_D_ = hours;
                                                    i.Plan_D_ = max;
                                                    i.FlagDem = true;
                                                    var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                                    foreach (var j in list2)
                                                    {
                                                        j.FlagDem = true;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                }

                                                if (kehoach <= max)
                                                {
                                                    double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = kehoach;
                                                    if (kehoach == max)
                                                    {
                                                        i.FlagNgay = true;
                                                        var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                        foreach (var j in list2)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        public_time = 0;
                                                    }
                                                    else
                                                    {
                                                        public_time = hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.Week == i.Week).ToList();
                                                            foreach (var j in list2)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                    db.SaveChanges();
                                                    kehoach = 0;
                                                    break;
                                                }

                                                if (kehoach > max)
                                                {
                                                    kehoach = kehoach - max;
                                                    double hours = Convert.ToDouble(max) / Convert.ToDouble(i.Capa);
                                                    
                                                    i.C_Plan_Time_N_ = hours;
                                                    i.Plan_N_ = max;
                                                    i.FlagNgay = true;
                                                    var list2 = db.DA_Injection.Where(x => x.So_May == i.So_May && x.Date == i.Date && x.PSI == i.PSI && x.Week == i.Week).ToList();
                                                    foreach (var j in list2)
                                                    {
                                                        j.FlagNgay = true;
                                                        db.SaveChanges();
                                                    }
                                                    db.SaveChanges();
                                                }
                                            }
                                        }
                                    }
                                }

                                //update cung may
                                var code_temp = "";
                                foreach (var k in list)
                                {
                                    var flag = 0;
                                    if (k.FlagDem == false)
                                    {
                                        if (k.Code != result.Code)
                                        {
                                            if (k.Code != code_temp)
                                            {
                                                flag = 1;
                                            }

                                            if (flag == 1)
                                            {
                                                var time_private = 12 - public_time;
                                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                                plan_private = (int)Math.Ceiling(plan_private);
                                                sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                                kehoach = (int)k.Ke_Hoach + sumkhbs;

                                                if (kehoach > 0)
                                                {
                                                    if (plan_private <= kehoach)
                                                    {
                                                        kehoach = kehoach - (int)plan_private;
                                                        k.Plan_D_ = (int)plan_private;
                                                        k.Plan_Time_D_ = time_private;
                                                        k.FlagDem = true;
                                                        public_time = 0;
                                                        db.SaveChanges();
                                                        var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list2)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }

                                                        double temp = 12 * Convert.ToDouble(k.Capa);
                                                        int max = Convert.ToInt32(Math.Ceiling(temp));
                                                        if (kehoach <= max)
                                                        {
                                                            double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.C_Plan_Time_N_ = hours;
                                                            k.Plan_N_ = kehoach;
                                                            if (kehoach == max)
                                                            {
                                                                k.FlagNgay = true;
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagNgay = true;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = 0;
                                                            }
                                                            else
                                                            {
                                                                public_time = public_time + hours + 2;
                                                                if (public_time >= 12)
                                                                {
                                                                    var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                    foreach (var j in list_)
                                                                    {
                                                                        j.FlagNgay = true;
                                                                        db.SaveChanges();
                                                                    }
                                                                    public_time = public_time - 12;
                                                                }
                                                            }
                                                            db.SaveChanges();

                                                            kehoach = 0;
                                                            code_temp = k.Code;
                                                            continue;
                                                        }

                                                        if (kehoach > max)
                                                        {
                                                            kehoach = kehoach - max;
                                                            double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                            
                                                            k.C_Plan_Time_N_ = hours;
                                                            k.Plan_N_ = max;
                                                            k.FlagNgay = true;
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            db.SaveChanges();
                                                            code_temp = k.Code;
                                                            continue;
                                                        }
                                                    }

                                                    if (plan_private > kehoach)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        public_time = time_private + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagDem = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                        plan_private = kehoach;
                                                        k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                                        k.Plan_Time_D_ = hours;
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (kehoach > 0)
                                                {
                                                    double temp = 12 * Convert.ToDouble(k.Capa);
                                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                                    if (kehoach <= max)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.Plan_Time_D_ = hours;
                                                        k.Plan_D_ = kehoach;
                                                        if (kehoach == max)
                                                        {
                                                            k.FlagDem = true;
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagDem = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = 0;
                                                        }
                                                        else
                                                        {
                                                            public_time = public_time + hours + 2;
                                                            if (public_time >= 12)
                                                            {
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagDem = true;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = public_time - 12;
                                                            }
                                                        }
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                        code_temp = k.Code;
                                                        continue;
                                                    }

                                                    if (kehoach > max)
                                                    {
                                                        kehoach = kehoach - max;
                                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.Plan_Time_D_ = hours;
                                                        k.Plan_D_ = max;
                                                        k.FlagDem = true;
                                                        var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list2)
                                                        {
                                                            j.FlagDem = true;
                                                            db.SaveChanges();
                                                        }
                                                        db.SaveChanges();
                                                    }

                                                    if (kehoach <= max)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.C_Plan_Time_N_ = hours;
                                                        k.Plan_N_ = kehoach;
                                                        if (kehoach == max)
                                                        {
                                                            k.FlagNgay = true;
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = 0;
                                                        }
                                                        else
                                                        {
                                                            public_time = public_time + hours + 2;
                                                            if (public_time >= 12)
                                                            {
                                                                var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                                foreach (var j in list_)
                                                                {
                                                                    j.FlagNgay = true;
                                                                    db.SaveChanges();
                                                                }
                                                                public_time = public_time - 12;
                                                            }
                                                        }
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                        code_temp = k.Code;
                                                        continue;
                                                    }

                                                    if (kehoach > max)
                                                    {
                                                        kehoach = kehoach - max;
                                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                                        
                                                        k.C_Plan_Time_N_ = hours;
                                                        k.Plan_N_ = max;
                                                        k.FlagNgay = true;
                                                        var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list2)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        db.SaveChanges();
                                                    }
                                                }
                                            }
                                            code_temp = k.Code;
                                            continue;
                                        }
                                    }

                                    /********************/

                                    if (k.FlagNgay == false)
                                    {
                                        if (k.Code != result.Code)
                                        {
                                            if (k.Code != code_temp)
                                            {
                                                flag = 1;
                                            }

                                            if (flag == 1)
                                            {
                                                var time_private = 12 - public_time;
                                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                                plan_private = (int)Math.Ceiling(plan_private);
                                                sumkhbs = db.DA_Injection.Where(x => x.Code == k.Code && x.PSI == k.PSI && x.Week == k.Week).Sum(x => x.Ke_Hoach_Bo_Sung).Value;
                                                kehoach = (int)k.Ke_Hoach + sumkhbs;

                                                if (kehoach > 0)
                                                {
                                                    if (plan_private <= kehoach)
                                                    {
                                                        kehoach = kehoach - (int)plan_private;
                                                        k.Plan_N_ = (int)plan_private;
                                                        k.C_Plan_Time_N_ = time_private;
                                                        k.FlagNgay = true;
                                                        public_time = 0;
                                                        db.SaveChanges();
                                                        var list2 = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                        foreach (var j in list2)
                                                        {
                                                            j.FlagNgay = true;
                                                            db.SaveChanges();
                                                        }
                                                        code_temp = k.Code;
                                                        continue;
                                                    }

                                                    if (plan_private > kehoach)
                                                    {
                                                        double hours = Convert.ToDouble(kehoach) / Convert.ToDouble(k.Capa);
                                                        
                                                        plan_private = kehoach;
                                                        k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                                        k.C_Plan_Time_N_ = hours;
                                                        db.SaveChanges();
                                                        kehoach = 0;
                                                        public_time = public_time + hours + 2;
                                                        if (public_time >= 12)
                                                        {
                                                            var list_ = db.DA_Injection.Where(x => x.So_May == k.So_May && x.Date == k.Date && x.Week == k.Week).ToList();
                                                            foreach (var j in list_)
                                                            {
                                                                j.FlagNgay = true;
                                                                db.SaveChanges();
                                                            }
                                                            public_time = public_time - 12;
                                                        }
                                                    }
                                                }
                                            }
                                            code_temp = k.Code;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }

                    //da co public_time
                    var kq = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Week == week).ToList();
                    int kh = kehoach_edit;
                    foreach (var j in kq)
                    {
                        if (j.FlagDem == false)
                        {
                            var k = db.DA_Injection.AsNoTracking().Where(x => x.Code == code && x.Week == week && x.Date == j.Date && x.So_May == somay && x.PSI == psi).FirstOrDefault();
                            k.Plan_D_ = 0;
                            k.Plan_Time_D_ = 0;
                            k.Plan_N_ = 0;
                            k.C_Plan_Time_N_ = 0;
                            k.Ke_Hoach = kh;
                            k.Ke_Hoach_Bo_Sung = 0;
                            k.Good_D_ = null;
                            k.Good_N_ = null;
                            k.FlagDem = false;
                            k.FlagNgay = false;
                            k.So_May = maychaybosung;
                            k.STT = stt;
                            stt++;

                            var time_private = 12 - public_time;
                            double plan_private = time_private * Convert.ToDouble(k.Capa);
                            plan_private = (int)Math.Ceiling(plan_private);
                            if (kehoach_edit > 0)
                            {
                                if (plan_private <= kehoach_edit)
                                {
                                    kehoach_edit = kehoach_edit - (int)plan_private;
                                    k.Plan_D_ = (int)plan_private;
                                    k.Plan_Time_D_ = time_private;
                                    k.FlagDem = true;
                                    public_time = 0;
                                    var list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                    foreach (var o in list_)
                                    {
                                        o.FlagDem = true;
                                        db.SaveChanges();
                                    }

                                    double temp = 12 * Convert.ToDouble(k.Capa);
                                    int max = Convert.ToInt32(Math.Ceiling(temp));
                                    if (kehoach_edit <= max)
                                    {
                                        double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                        
                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = kehoach_edit;
                                        if (kehoach_edit == max)
                                        {
                                            k.FlagNgay = true;
                                            var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var o in _list_)
                                            {
                                                o.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            public_time = 0;
                                        }
                                        else
                                        {
                                            public_time = public_time + hours + 2;
                                            if (public_time >= 12)
                                            {
                                                var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                                foreach (var o in _list_)
                                                {
                                                    o.FlagNgay = true;
                                                    db.SaveChanges();
                                                }
                                                public_time = public_time - 12;
                                            }
                                        }
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        kehoach_edit = 0;
                                        continue;
                                    }

                                    if (kehoach_edit > max)
                                    {
                                        kehoach_edit = kehoach_edit - max;
                                        double hours = Convert.ToDouble(max) / Convert.ToDouble(k.Capa);
                                        
                                        k.C_Plan_Time_N_ = hours;
                                        k.Plan_N_ = max;
                                        k.FlagNgay = true;
                                        var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in _list_)
                                        {
                                            o.FlagNgay = true;
                                            db.SaveChanges();
                                        }
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        continue;
                                    }
                                }

                                if (plan_private > kehoach_edit)
                                {
                                    double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                    
                                    public_time = public_time + time_private + 2;
                                    if (public_time >= 12)
                                    {
                                        var list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in list_)
                                        {
                                            o.FlagDem = true;
                                            db.SaveChanges();
                                        }
                                        public_time = public_time - 12;
                                    }
                                    plan_private = kehoach_edit;
                                    k.Plan_D_ = (int)Math.Ceiling(plan_private);
                                    k.Plan_Time_D_ = hours;
                                    db.DA_Injection.Add(k);
                                    db.SaveChanges();
                                    kehoach_edit = 0;
                                }
                            }
                        }
                        else
                        {
                            if (j.FlagNgay == false)
                            {
                                var k = db.DA_Injection.Where(x => x.Code == code && x.Week == week && x.Date == j.Date).FirstOrDefault();
                                k.Plan_D_ = 0;
                                k.Plan_Time_D_ = 0;
                                k.Plan_N_ = 0;
                                k.C_Plan_Time_N_ = 0;
                                k.Ke_Hoach = kh;
                                k.Ke_Hoach_Bo_Sung = 0;
                                k.Good_D_ = null;
                                k.Good_N_ = null;
                                k.FlagDem = false;
                                k.FlagNgay = false;
                                k.So_May = maychaybosung;
                                k.STT = stt;
                                stt++;

                                var time_private = 12 - public_time;
                                double plan_private = time_private * Convert.ToDouble(k.Capa);
                                plan_private = (int)Math.Ceiling(plan_private);
                                if (kehoach_edit > 0)
                                {
                                    if (plan_private <= kehoach_edit)
                                    {
                                        kehoach_edit = kehoach_edit - (int)plan_private;
                                        k.Plan_N_ = (int)plan_private;
                                        k.C_Plan_Time_N_ = time_private;
                                        k.FlagNgay = true;
                                        public_time = 0;
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        var list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                        foreach (var o in list_)
                                        {
                                            o.FlagNgay = true;
                                            db.SaveChanges();
                                        }
                                        code_temp1 = k.Code;
                                        continue;
                                    }

                                    if (plan_private > kehoach_edit)
                                    {
                                        double hours = Convert.ToDouble(kehoach_edit) / Convert.ToDouble(k.Capa);
                                        
                                        plan_private = kehoach_edit;
                                        k.Plan_N_ = (int)Math.Ceiling(plan_private);
                                        k.C_Plan_Time_N_ = hours;
                                        db.DA_Injection.Add(k);
                                        db.SaveChanges();
                                        kehoach_edit = 0;
                                        public_time = public_time + hours + 2;
                                        if (public_time >= 12)
                                        {
                                            var _list_ = db.DA_Injection.Where(x => x.So_May == maychaybosung && x.Date == k.Date && x.Week == k.Week).ToList();
                                            foreach (var o in _list_)
                                            {
                                                o.FlagNgay = true;
                                                db.SaveChanges();
                                            }
                                            public_time = public_time - 12;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (co == false)
            {
                DateTime date = Convert.ToDateTime(strdate);
                var rel = db.KetQuaSanXuatThieux.FirstOrDefault(x=>x.Code == code && x.Week == week && x.Date == date && x.PSI == psi);
                if(rel.SoThieuCaDem > 0)
                {
                    rel.FlagDem = true;
                }

                if(rel.SoThieuCaNgay > 0)
                {
                    rel.FlagNgay = true;
                }
                db.SaveChanges();
            }
            return Json(co,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCodeEdit(string week, int somay)
        {
            var data = db.DA_Injection.Where(x =>x.Week == week && x.So_May == somay).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}