﻿using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Sybase.DataWindow;
using DataLibrary;
using System.Web.Services.Protocols;

namespace Saving.Applications.app_assist
{
    public partial class w_sheet_as_scholarship_mu : PageWebSheet, WebSheet
    {
        private String pbl = "as_public_funds.pbl";
        private Decimal level_School,
                        seq_no_same,
                        level_School_same;
        private DataStore Ds;
        private Decimal capital_year,
                        now_year,
                        salary_amt,
                        assist_amt;
        protected DwThDate tDwMain;
        //protected DwThDate tDwDetail;
        private Sta ta;
        private Sdt dt;
        protected DwThDate tDwDetail;
        protected String assist_docno,
                         sqlStr,
                         membgroup_code,
                         entry_date,
                         member_no,
                         expense_accid,
                         moneytype_code,
                         deptaccount_no,
                         docNo
                         ;
        protected int Chk;
        //private String ss_dropdown_scholarship_branch;
        protected String postToFromAccid;
        protected String postDepptacount;
        protected String postRefresh;
        protected String postChangeHeight;
        protected String postRetreiveDwMem;
        protected String postRetrieveDwMain;
        protected String postGetMemberDetail;
        protected String postChangeHost;
        protected String postCheckDate;
        protected String postRetrieveBankBranch;
        protected String postCopyAddress;
        protected String postMemProvince;
        protected String postMemDistrict;
        protected String postMemTambol;
        protected String postMainProvince;
        protected String postMainDistrict;
        protected String postMainTambol;
        protected String saveBranchId;
        protected String jsformatDeptaccid;
        protected String jsformatIDCard;
        //------------------------ ทุนศึกษา -------------------------------------------
        protected String postNewClear;
        protected String postGetMoney;
        protected String postFilterScholarship;
        protected String postCheckReQuest;
        protected String postGetLevelSchool;
        protected string postSetBranch;
        protected String postChildAge;
        protected String postMateName;
        private DateTime pay_date, cancel_date = DateTime.Now, req_date;
        private Nullable<Decimal> cancel_id;
        private String surname_child;

        //------------------------- common-------------------------------------------
        //private String sqlStr, envvalue;
        //private String member_no, assist_docno, membgroup_code;
        //private String married_member, to_system;
        //private String expense_bank, expense_branch, expense_accid, paytype_code, remark_cancel;
        //private String memb_addr, addr_group, soi, mooban, road, tambol, district_code, province_code, postcode, deptaccount_no, province_desc;
        //private Decimal capital_year, salary_amt, assist_amt, req_status, seq_pay, pay_status, rowcount, rowcount2, perpay;
        //private DateTime pay_date, cancel_date = DateTime.Now, center_date;
        //private Nullable<Decimal> cancel_id;

        //private String assisttype_code, cancal_id, expense_type, voucher_no, sumpay, moneytype_code;
        //private Decimal approve_amt, approve_date, paytype_date, posttovc_flag, assistdept_amt;
        //private DateTime req_date;

        //TimeSpan tp;

        #region WebSheets Member
        public void InitJsPostBack()
        {
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postChangeHost = WebUtil.JsPostBack(this, "postChangeHost");
            postChangeHeight = WebUtil.JsPostBack(this, "postChangeHeight");
            postRetreiveDwMem = WebUtil.JsPostBack(this, "postRetreiveDwMem");
            postRetrieveDwMain = WebUtil.JsPostBack(this, "postRetrieveDwMain");
            postCheckDate = WebUtil.JsPostBack(this, "postCheckDate");
            postGetMemberDetail = WebUtil.JsPostBack(this, "postGetMemberDetail");
            postRetrieveBankBranch = WebUtil.JsPostBack(this, "postRetrieveBankBranch");
            postCopyAddress = WebUtil.JsPostBack(this, "postCopyAddress");
            postMemProvince = WebUtil.JsPostBack(this, "postMemProvince");
            postMemDistrict = WebUtil.JsPostBack(this, "postMemDistrict");
            postMemTambol = WebUtil.JsPostBack(this, "postMemTambol");
            postMainProvince = WebUtil.JsPostBack(this, "postMainProvince");
            postMainDistrict = WebUtil.JsPostBack(this, "postMainDistrict");
            postDepptacount = WebUtil.JsPostBack(this, "postDepptacount");
            postToFromAccid = WebUtil.JsPostBack(this, "postToFromAccid");
            jsformatDeptaccid = WebUtil.JsPostBack(this, "jsformatDeptaccid");
            jsformatIDCard = WebUtil.JsPostBack(this, "jsformatIDCard");

            postMainTambol = WebUtil.JsPostBack(this, "postMainTambol");
            saveBranchId = WebUtil.JsPostBack(this, "saveBranchId");
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postGetMoney = WebUtil.JsPostBack(this, "postGetMoney");
            postFilterScholarship = WebUtil.JsPostBack(this, "postFilterScholarship");
            postCheckReQuest = WebUtil.JsPostBack(this, "postCheckReQuest");
            postCheckReQuest = WebUtil.JsPostBack(this, "postGetLevelSchool");
            postSetBranch = WebUtil.JsPostBack(this, "postSetBranch");
            postChildAge = WebUtil.JsPostBack(this, "postChildAge");
            postMateName = WebUtil.JsPostBack(this, "postMateName");
            tDwMain = new DwThDate(DwMain, this);
            tDwMain.Add("req_date", "req_tdate");
            tDwMain.Add("approve_date", "approve_tdate");

            tDwDetail = new DwThDate(DwDetail, this);
            tDwDetail.Add("childbirth_date", "childbirth_tdate");
        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            ta = new Sta(sqlca.ConnectionString);
            //tDwMain.Eng2ThaiAllRow();
            //tDwDetail.Eng2ThaiAllRow();
            //this.ConnectSQLCA();
            //ta = new Sta(sqlca.ConnectionString);
            //dt = new Sdt();

            HdDuplicate.Value = "";
            HdNameDuplicate.Value = "";
            LtAlert.Text = "";
            //try {
            //    ss_dropdown_scholarship_branch = Session["ss_dropdown_scholarship_branch"].ToString();
            //    if (ss_dropdown_scholarship_branch == "") {
            //        ss_dropdown_scholarship_branch = state.SsCoopId;
            //   //   ss_dropdown_scholarship_branch = state.ssCoopid; ** ถ้ารวมโปรแกรมที่มหิดลเปลี่ยนเป็น coopid
            //    }
            //}
            //catch {
            //    ss_dropdown_scholarship_branch = "001001";//state.SsCoopId;
            //}
            if (!IsPostBack)
            {
                //NewClear();
                HdActionStatus.Value = "Insert";
                DwMem.InsertRow(0);
                DwMain.InsertRow(0);
                DwDetail.InsertRow(0);
                DwMain.SetItemDateTime(1, "req_date", state.SsWorkDate);
                DwMain.SetItemDateTime(1, "approve_date", state.SsWorkDate);
                DwDetail.SetItemDateTime(1, "childbirth_date", state.SsWorkDate);
                SetDefaultAccid();
            }
            else
            {
                this.RestoreContextDw(DwMem);
                this.RestoreContextDw(DwMain, tDwMain);
                this.RestoreContextDw(DwDetail, tDwDetail);
            }
            //DwMain.SetItemString(1, "assisttype_code", "10");
            //DwUtil.RetrieveDDDW(DwMain, "moneytype_code", "as_public_funds.pbl", null);
            DwUtil.RetrieveDDDW(DwDetail, "level_school", pbl, null);//LEK เพิ่ม Retrieve DropDownDataWindow ระดับชั้น
            DwUtil.RetrieveDDDW(DwDetail, "moneytype_code", pbl, null);
            DwUtil.RetrieveDDDW(DwMain, "assisttype_code", pbl, state.SsCoopControl);
            DwUtil.RetrieveDDDW(DwMain, "capital_year", pbl, null);
            try { capital_year = DwMain.GetItemDecimal(1, "capital_year"); }
            catch { capital_year = state.SsWorkDate.Year + 543; }
            DwMain.SetItemDecimal(1, "capital_year", capital_year);
            //GetLastDocNo(capital_year);
            //DwMain.SetItemString(1, "assist_docno", assist_docno);
            //aek
            //DwUtil.RetrieveDDDW(DwMain, "expense_bank", pbl, null);
            // DwUtil.RetrieveDDDW(DwMain, "cancel_id", pbl, null);
            DwUtil.RetrieveDDDW(DwMain, "req_status", pbl, null);
            //DwUtil.RetrieveDDDW(DwMain, "pay_status", pbl, null);
            // DwUtil.RetrieveDDDW(DwMain, "paytype_code", "as_capital.pbl", null);

            DwUtil.RetrieveDDDW(DwDetail, "scholarship_type", pbl, state.SsCoopControl);
            //DwDetail.SetItemDecimal(1, "scholarship_type", 3);
            DwUtil.RetrieveDDDW(DwDetail, "scholarship_level", pbl, null);
            //DwUtil.RetrieveDDDW(DwDetail, "level_school", "as_capital.pbl", null);
            DwUtil.RetrieveDDDW(DwDetail, "childprename_code", pbl, null);
            DwUtil.RetrieveDDDW(DwDetail, "expense_branch", pbl, null);
            tDwMain.Eng2ThaiAllRow();
            tDwDetail.Eng2ThaiAllRow();
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "postRetreiveDwMem")
            {
                RetreiveDwMem();
            }
            else if (eventArg == "postRefresh")
            {
                //Refresh();
            }
            else if (eventArg == "postCheckDate")
            {
                //CheckDate();
            }
            else if (eventArg == "postRetrieveDwMain")
            {
                RetrieveDwMain();
            }
            else if (eventArg == "postChangeHost")
            {
                //ChangeHost();
            }
            else if (eventArg == "postRetrieveBankBranch")
            {
                RetrieveBankBranch();
            }
            else if (eventArg == "postChangeHeight")
            {
                //ChangeHeight();
            }
            else if (eventArg == "postGetMemberDetail")
            {
                GetMemberDetail();
            }
            else if (eventArg == "postCopyAddress")
            {
                JsPostCopyAddress();
            }
            else if (eventArg == "postMemProvince")
            {
                DwMem.SetItemNull(1, "postcode");
                DwMem.SetItemNull(1, "district_code");
                DwMem.SetItemNull(1, "tambol_code");
            }
            else if (eventArg == "postMemDistrict")
            {
                DwMem.SetItemNull(1, "tambol_code");
                try
                {
                    String sqlPostCode = "select * from mbucfdistrict where district_code = '" + DwMem.GetItemString(1, "district_code") + "'";
                    Sdt dtPostCode = WebUtil.QuerySdt(sqlPostCode);
                    if (dtPostCode.Next())
                    {
                        DwMem.SetItemString(1, "postcode", dtPostCode.GetString("postcode"));
                    }
                }
                catch { }
            }
            else if (eventArg == "postMemTambol")
            {
                //xxxxx
            }
            else if (eventArg == "postMainProvince")
            {
                DwMain.SetItemNull(1, "postcode");
                DwMain.SetItemNull(1, "district_code");
                DwMain.SetItemNull(1, "tambol_code");
            }
            else if (eventArg == "postMainDistrict")
            {
                DwMain.SetItemNull(1, "tambol_code");
                try
                {
                    String sqlPostCode = "select * from mbucfdistrict where district_code = '" + DwMain.GetItemString(1, "district_code") + "'";
                    Sdt dtPostCode = WebUtil.QuerySdt(sqlPostCode);
                    if (dtPostCode.Next())
                    {
                        DwMain.SetItemString(1, "postcode", dtPostCode.GetString("postcode"));
                    }
                }
                catch { }
            }
            else if (eventArg == "postMainTambol")
            {
                //xxxxx
            }
            else if (eventArg == "postGetMoney")
            {
                GetMoney();
            }
            else if (eventArg == "postFilterScholarship")
            {
                FilterScholarship();
            }
            else if (eventArg == "postCheckReQuest")
            {
                CheckReQuest();
            }
            else if (eventArg == "postGetLevelSchool")
            {
                CheckLevelSchool();
            }

            //else if (eventArg == "saveBranchId")
            //{
            //    Session["ss_dropdown_scholarship_branch"] = DwMain.GetItemString(1, "branch_id");
            //    ss_dropdown_scholarship_branch = Session["ss_dropdown_scholarship_branch"].ToString();
            //}
            else if (eventArg == "postDepptacount")
            {
                PostDepptacount();
            }
            else if (eventArg == "postToFromAccid")
            {
                PostToFromAccid();
            }
            else if (eventArg == "jsformatDeptaccid")
            {
                string deptaccid = "";
                //deptaccid = DwMain.GetItemString(1, "deptaccount_no");
                deptaccid = DwDetail.GetItemString(1, "deptaccount_no");
                deptaccid = formatDeptaccid(deptaccid);
                //DwMain.SetItemString(1, "deptaccount_no", deptaccid);
                DwDetail.SetItemString(1, "deptaccount_no", deptaccid);
            }
            else if (eventArg == "jsformatIDCard")
            {
                string card_person;
                card_person = DwDetail.GetItemString(1, "child_card_person");
                card_person = formatIDCard(card_person);
                DwDetail.SetItemString(1, "child_card_person", card_person);
            }
            else if (eventArg == "postSetBranch")
            {
                SetBankbranch();
            }
            else if (eventArg == "postChildAge") { SetChildAge(); }
            else if (eventArg == "postMateName") { SetMateName(); }
        }

        protected void SetMateName()
        {
            //คู่สมรส : mate_name
            String mate_name = "", mate_cardperson = "", mate_salaryid = "";
            try { mate_name = DwMem.GetItemString(1, "mate_name"); }
            catch { }
            //เลขบัตรฯ คู่สมรส : mate_cardperson
            try { mate_cardperson = DwMem.GetItemString(1, "mate_cardperson").Trim(); }
            catch { }
            //เลขทะเบียนคู่สมรส: mate_salaryid
            try { mate_salaryid = DwMem.GetItemString(1, "mate_salaryid").Trim(); }
            catch { }
            DwMem.SetItemString(1, "mate_name", mate_name);
            DwMem.SetItemString(1, "mate_cardperson", mate_cardperson);
            DwMem.SetItemString(1, "mate_salaryid", mate_salaryid);
        }

        protected void SetChildAge()
        {
            //3. เช็คว่า อายุบุตรถึง 3 ปี หรือไม่ :ถ้าไม่ ให้ฟ้องเตือนว่า อายุบุตรไม่ถึง 3 ปี
            Decimal child_age = 0;
            try { child_age = DwDetail.GetItemDecimal(1, "child_age"); }
            catch { child_age = 0; }
            if (child_age < 3) { LtServerMessage.Text = WebUtil.WarningMessage("อายุบุตรไม่ถึง 3 ปี"); return; }
        }

        protected void SetBankbranch()
        {
            string expense_branch = "";
            try { expense_branch = DwDetail.GetItemString(1, "expense_branch"); }
            catch { }
            DwMain.SetItemString(1, "expense_bank", "034");
            DwMain.SetItemString(1, "expense_branch", expense_branch);
            DwDetail.SetItemString(1, "expense_bank", "034");
            DwDetail.SetItemString(1, "expense_branch", expense_branch);
        }

        public void SaveWebSheet()
        {
            Sta ta = new Sta(sqlca.ConnectionString);
            DwMain.SetItemString(1, "coop_id", state.SsCoopId);
            setAccItemDwMain();
            unfomatAll();

            try
            {
                //String docNo = DwUtil.GetString(DwMain, 1, "assist_docno", "");
                try { docNo = DwMain.GetItemString(1, "assist_docno"); }
                catch { docNo = ""; }
                decimal reqStatus = DwMain.GetItemDecimal(1, "req_status");

                String childprename_code, child_name, child_surname, child_sex, childschool_name, childbirth_tdate, remark, system_code = "", tofrom_accid = "", approve_tdate;
                Decimal capital_year, scholarship_type, scholarship_level, child_gpa, scholarship1_amt, child_age;
                DateTime childbirth_date, approve_date;

                String member_no = DwMem.GetItemString(1, "member_no");
                member_no = WebUtil.MemberNoFormat(member_no);
                //คู่สมรส : mate_name
                String mate_name = "", mate_cardperson = "", mate_salaryid = "";
                try { mate_name = DwMem.GetItemString(1, "mate_name").Trim(); }
                catch { }
                //เลขบัตรฯ คู่สมรส : mate_cardperson
                try { mate_cardperson = DwMem.GetItemString(1, "mate_cardperson").Trim(); }
                catch { }
                //เลขทะเบียนคู่สมรส: mate_salaryid
                try { mate_salaryid = DwMem.GetItemString(1, "mate_salaryid").Trim(); }
                catch { }
                if (mate_name != null && mate_name != "" && mate_cardperson != null && mate_cardperson != "")
                {
                    try
                    {
                        sqlStr = @" UPDATE  MBMEMBMASTER
                                    SET     mate_name       = '" + mate_name + @"',
                                            mate_cardperson = '" + mate_cardperson.Replace("-", "") + @"',
                                            mate_salaryid   = '" + mate_salaryid + @"'
                                    WHERE   member_no       = '" + member_no + @"'";
                        ta.Exe(sqlStr);
                        //LtServerMessage.Text = WebUtil.CompleteMessage("คู่สมรส : " + mate_name + "</br>เลขบัตรฯ คู่สมรส : " + mate_cardperson + "</br>เลขทะเบียนคู่สมรส: "+mate_salaryid);
                        //return;
                    }
                    catch (Exception ex) { ex.ToString(); }
                }
                //Retrieve DwMem
                DwUtil.RetrieveDataWindow(DwMem, pbl, null, member_no);
                capital_year = DwMain.GetItemDecimal(1, "capital_year");
                try { membgroup_code = DwMem.GetItemString(1, "membgroup_code"); }
                catch { membgroup_code = ""; }
                try { childprename_code = DwDetail.GetItemString(1, "childprename_code"); }
                catch { childprename_code = "01"; }
                try { child_age = DwDetail.GetItemDecimal(1, "child_age"); }
                catch { child_age = 0; }
                try { scholarship_type = DwDetail.GetItemDecimal(1, "scholarship_type"); }
                catch { scholarship_type = 0; }
                try { scholarship_level = DwDetail.GetItemDecimal(1, "scholarship_type"); }
                catch { scholarship_level = 0; }
                try { childschool_name = DwDetail.GetItemString(1, "childschool_name"); }
                catch { childschool_name = ""; }
                try
                {
                    //childbirth_date = DwDetail.GetItemDateTime(1, "childbirth_date");
                    childbirth_tdate = DwDetail.GetItemString(1, "childbirth_tdate");
                    //childbirth_date = DateTime.ParseExact(hdate1.Value, "ddMMyyyy", WebUtil.TH);
                    childbirth_date = DateTime.ParseExact(childbirth_tdate, "ddMMyyyy", WebUtil.TH);
                    DwDetail.SetItemDateTime(1, "childbirth_date", childbirth_date);
                }
                catch { childbirth_date = DateTime.Now; }

                //เพิ่มเช็คการ save โดยไม่ต้องมีการตรวจสอบ
                //decimal chksave = DwMem.GetItemDecimal(1, "chksave");
                //if (chksave == 1)
                //{
                //childbirth_date

                #region ตรวจสอบว่า กรอก ว/ด/ป เกิดของบุตร?
                try
                {
                    DateTime dtm = DwDetail.GetItemDateTime(1, "childbirth_date");
                    if (dtm == null)
                    {
                        //throw new Exception("กรุณากรอกค่า \"วัน/เดือน/ปี เกิด\"");
                        infomatAll();
                        Response.Write("<script language='javascript'>alert('กรุณากรอกค่า วัน/เดือน/ปี เกิด'\n );</script>");
                        return;
                    }
                }
                catch
                {
                    //throw new Exception("กรุณากรอกค่า วัน/เดือน/ปี เกิด");
                    Response.Write("<script language='javascript'>alert('กรุณากรอกค่า วัน/เดือน/ปี เกิด'\n );</script>");
                    infomatAll();
                    return;
                }
                #endregion

                #region ตรวจสอบว่ากรอกค่าวันที่ประสบภัย?
                try
                {
                    DateTime dtm = DwMain.GetItemDateTime(1, "req_date");
                    if (dtm == null)
                    {
                        //throw new Exception("กรุณากรอกค่า \"วันทีประสบภัย\"");
                        infomatAll();
                        Response.Write("<script language='javascript'>alert('กรุณากรอกค่า วันทีประสบภัย'\n );</script>");
                        return;
                    }
                }
                catch
                {
                    //throw new Exception("กรุณากรอกค่า วันทีประสบภัย");
                    Response.Write("<script language='javascript'>alert('กรุณากรอกค่า วันทีประสบภัย'\n );</script>");
                    infomatAll();
                    return;
                }
                #endregion

                #region ตรวจสอบว่ากรอกเลขที่ บปช?
                string child_card_person = "";
                try { child_card_person = DwDetail.GetItemString(1, "child_card_person"); }
                catch { }
                if (child_card_person == "" || child_card_person == null) { }
                else if (child_card_person.Length != 13)
                {
                    //throw new Exception("กรุณากรอกค่า \"เลขที่บัตรประชาชนให้ครบ\"");
                    Response.Write("<script language='javascript'>alert('กรุณากรอกค่า เลขที่บัตรประชาชนให้ครบ'\n );</script>");
                    infomatAll();
                    return;
                }
                #endregion

                #region ตรวจสอบว่าคู่สมรสขอทุนไปแล้ว?
                Int16 same_married = CheckMarried(member_no);
                if (same_married == 0)
                {
                    Response.Write("<script language='javascript'>alert('สมาชิกรายนี้มีคู่สมรสขอทุนไปแล้ว'\n );</script>");
                    infomatAll();
                    return;
                }
                #endregion

                #region ตรวจสอบสมาชิกรายนี้เคยขอทุนให้กับบุตรรายนี้ไปแล้ว (ไม่ใช้)
                //เช็คชื่อลูกซ้ำ
                child_name = DwDetail.GetItemString(1, "child_name").Trim();
                child_surname = DwDetail.GetItemString(1, "child_surname").Trim();


                //Int16 same = 1; // CheckNameSon(member_no, capital_year, child_name, child_surname);
                //if (same == 0)
                //{
                //    String level_text = "";

                //    if (level_School_same == 1)
                //    {
                //        level_text = "ประถม";
                //    }
                //    else if (level_School_same == 2)
                //    {
                //        level_text = "มัธยมต้น";
                //    }
                //    else if (level_School_same == 3)
                //    {
                //        level_text = "มัธยมปลายและปวช.";
                //    }
                //    else if (level_School_same == 5)
                //    {
                //        level_text = "ปวส.";
                //    }
                //    else if (level_School_same == 6)
                //    {
                //        level_text = "ปริญญาตรี";
                //    }
                //    else if (level_School_same == 7)
                //    {
                //        level_text = "ปริญญาโท";
                //    }
                //    LtServerMessage.Text = WebUtil.WarningMessage("สมาชิกรายนี้เคยขอทุนให้กับบุตรรายนี้ไปแล้วในระดับชั้น" + level_text + "ลำดับที่" + seq_no_same);

                //    return;
                //}
                #endregion

                #region ตรวจสอบ ทุนเรียนดี ต้องคีย์ชื่อโรงเรียน
                //decimal dcheckscholarship_type = DwDetail.GetItemDecimal(1, "scholarship_type");
                //string scheckscholarship_type = "";
                //try
                //{
                //    scheckscholarship_type = DwDetail.GetItemString(1, "childschool_name");
                //}
                //catch { scheckscholarship_type = ""; }
                //if (dcheckscholarship_type == 2 && scheckscholarship_type == "")
                //{
                //    Response.Write("<script language='javascript'>alert('กรุณากรอกข้อมูลสถานศึกษา'\n );</script>");
                //    return;
                //}
                #endregion

                #region ตรวจสอบเกรดไม่ถึง 1.50
                try { child_gpa = DwDetail.GetItemDecimal(1, "child_gpa"); }
                catch { child_gpa = 0; }
                //if (level_School_same != 11 || level_School_same != 12 || level_School_same != 13)
                //{
                //    if (Convert.ToDouble(child_gpa) < 1.50)
                //    {
                //        //LtServerMessage.Text = WebUtil.WarningMessage("บุตรของสมาชิกมีเกรดเฉลี่ยไม่ถึง 1.50");
                //        Response.Write("<script language='javascript'>alert('บุตรของสมาชิกมีเกรดเฉลี่ยไม่ถึง 1.50'\n );</script>");
                //        return;
                //    }
                //}
                #endregion

                if (HdActionStatus.Value != "Update")
                {
                    #region ตรวจสอบการซ้ำเลขบัตรประชาชนลูก แต่ละปี

                    string year = DwMain.GetItemString(1, "capital_year");
                    //string child_card_person = "";
                    try { child_card_person = DwDetail.GetItemString(1, "child_card_person").Trim(); }
                    catch { }
                    if (child_card_person != null || child_card_person != "")
                    {
                        string sqlcheck1 = @"select asnreqschooldet.child_card_person ,asnreqmaster.member_no
                                        from asnreqschooldet, asnreqmaster
                                        where asnreqschooldet.assist_docno = asnreqmaster.assist_docno
                                        and asnreqschooldet.child_card_person ='" + child_card_person + @"' 
                                        and asnreqschooldet.capital_year = '" + year + @"'";
                        //Sdt dtcheck1 = WebUtil.QuerySdt(sqlcheck1);
                        Sdt dtcheck1 = ta.Query(sqlcheck1);
                        if (dtcheck1.Next())
                        {
                            string both_member_no = dtcheck1.GetString("member_no");
                            Response.Write("<script language='javascript'>alert('หมายเลขบัตรประชาชนบุตรของท่านซ้ำ กับ " + both_member_no + "'\n );</script>");
                            infomatAll();
                            return;
                        }
                    }
                    #endregion

                    #region ตรวจสอบให้ขอได้ 3 คน
                    //                    string cp_year = DwMain.GetItemString(1, "capital_year");
                    //                    string mem_no = DwMem.GetItemString(1, "member_no");
                    //                    string assisttype_code = DwMain.GetItemString(1, "assisttype_code");
                    //                    string sqlcheck2 = @"select count(assist_docno) as sumdocno 
                    //                                        from asnreqmaster 
                    //                                        where member_no = '" + mem_no + @"' 
                    //                                        and capital_year = '" + cp_year + @"'
                    //                                        and assisttype_code = '" + assisttype_code + @"'
                    //                                        ";
                    //                    Sdt dtcheck2 = WebUtil.QuerySdt(sqlcheck2);
                    //                    if (dtcheck2.Rows.Count > 0)
                    //                    {
                    //                        int n = int.Parse(dtcheck2.Rows[0]["sumdocno"].ToString());
                    //                        if (n >= 1)
                    //                        {
                    //                            Response.Write("<script language='javascript'>alert('หมายเลขสมาชิก " + mem_no + " ได้ทำการขอทุนการศึกษาประจำปี " + year + " ครบจำนวนแล้ว'\n );</script>");
                    //                            infomatAll();
                    //                            return;
                    //                        }
                    //                    }
                    #endregion

                }

                #region ตรวจสอบความสัมพันธ์ของ เช็คระดับ , ชั้น?
                //try
                //{
                //    string levelschool = DwDetail.GetItemDecimal(1, "level_school").ToString();
                //    string scholarshiplevel = DwDetail.GetItemDecimal(1, "scholarship_level").ToString().Substring(0, 1);
                //    if (levelschool == "5")
                //    {
                //        if (scholarshiplevel != "4" && scholarshiplevel != "5")
                //        {
                //            Response.Write("<script language='javascript'>alert('ระดับการศึกษา , ระดับชั้นไม่สัมพันธ์กัน'\n );</script>");
                //            infomatAll();
                //            return;
                //        }
                //    }
                //    else if (levelschool != scholarshiplevel && levelschool != "5")
                //    {
                //        Response.Write("<script language='javascript'>alert('ระดับการศึกษา , ระดับชั้นไม่สัมพันธ์กัน'\n );</script>");
                //        infomatAll();
                //        return;
                //    }
                //}
                //catch (Exception ex) { ex.ToString(); }
                #endregion

                #region ตรวจสอบอายุ ตรี โท
                TimeSpan tp;
                tp = DateTime.Now - childbirth_date;
                Double a = (tp.TotalDays / 365);
                try { level_School = DwDetail.GetItemDecimal(1, "level_school"); }
                catch { level_School = 0; }
                if (level_School == 6)
                {
                    if (a > 27)
                    {
                        //LtServerMessage.Text = WebUtil.WarningMessage("บุตรอยู่ในระดับปริญญาตรีมีอายุเกิน 27 ไม่สามารถขอทุนได้");
                        Response.Write("<script language='javascript'>alert('บุตรอยู่ในระดับปริญญาตรีมีอายุเกิน 27 ไม่สามารถขอทุนได้'\n );</script>");
                        infomatAll();
                        //return;
                    }
                }
                else if (level_School == 7)
                {
                    if (a > 30)
                    {
                        //LtServerMessage.Text = WebUtil.WarningMessage("บุตรอยู่ในระดับปริญญาโทมีอายุเกิน 30 ไม่สามารถขอทุนได้");
                        Response.Write("<script language='javascript'>alert('บุตรอยู่ในระดับปริญญาโทมีอายุเกิน 30 ไม่สามารถขอทุนได้'\n );</script>");
                        infomatAll();
                        //return;
                    }
                }
                #endregion

                //#region ตรวจสอบ ว่ามี assist_docno อยู่ใน asnreqmaster?
                //sqlStr = @"SELECT * FROM asnreqmaster WHERE assist_docno = '" + HfAssistDocNo.Value + "'";
                //Sdt dt = ta.Query(sqlStr);
                //if (dt.Next())
                //{
                //    decimal status_was = 0;// DwMain.GetItemDecimal(1, "");
                //    decimal status_mar = 0;

                //#region ตรวจสอบ ที่อยู่อาศัย
                //try
                //{
                //    status_mar = DwMain.GetItemDecimal(1, "mar_flag");
                //    DwMain.SetItemDecimal(1, "mar_flag", status_mar);

                //    status_was = DwMain.GetItemDecimal(1, "status_was");
                //    if (status_was == 1)
                //    {
                //        DwMain.SetItemDecimal(1, "ahome_flag", 1);
                //        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "chome_flag", 0);
                //        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                //    }
                //    else if (status_was == 2)
                //    {
                //        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                //        DwMain.SetItemDecimal(1, "bhome_flag", 1);
                //        DwMain.SetItemDecimal(1, "chome_flag", 0);
                //        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                //    }
                //    else if (status_was == 3)
                //    {
                //        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                //        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "chome_flag", 1);
                //        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                //    }
                //    else if (status_was == 4)
                //    {
                //        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                //        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "chome_flag", 0);
                //        DwMain.SetItemDecimal(1, "dhome_flag", 1);
                //        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                //    }
                //    else if (status_was == 5)
                //    {
                //        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                //        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "chome_flag", 0);
                //        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                //        DwMain.SetItemDecimal(1, "ehome_flag", 1);
                //    }
                //}
                //catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                //#endregion

                //------------------------ ทุนศึกษา --------------------------
                DwDetail.SetItemDecimal(1, "capital_year", capital_year);
                DwDetail.SetItemDateTime(1, "capital_date", state.SsWorkDate); //DwMain.GetItemDateTime(1, "req_date") 
                DwDetail.SetItemDateTime(1, "entry_date", state.SsWorkDate); // DwMain.GetItemDateTime(1, "req_date")
                DwDetail.SetItemString(1, "entry_id", state.SsUsername);


                //}
                //*******************************************************************

                //else
                //{
                //ถ่ายังไม่ได้ลงทะเบียน

                decimal status_was = 0;// DwMain.GetItemDecimal(1, "");
                decimal status_mar = 0;

                #region ตรวจสอบ ที่อยู่อาศัย
                try
                {
                    status_mar = DwMain.GetItemDecimal(1, "mar_flag");
                    DwMain.SetItemDecimal(1, "mar_flag", status_mar);

                    status_was = DwMain.GetItemDecimal(1, "status_was");
                    if (status_was == 1)
                    {
                        DwMain.SetItemDecimal(1, "ahome_flag", 1);
                        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                        DwMain.SetItemDecimal(1, "chome_flag", 0);
                        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                    }
                    else if (status_was == 2)
                    {
                        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                        DwMain.SetItemDecimal(1, "bhome_flag", 1);
                        DwMain.SetItemDecimal(1, "chome_flag", 0);
                        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                    }
                    else if (status_was == 3)
                    {
                        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                        DwMain.SetItemDecimal(1, "chome_flag", 1);
                        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                    }
                    else if (status_was == 4)
                    {
                        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                        DwMain.SetItemDecimal(1, "chome_flag", 0);
                        DwMain.SetItemDecimal(1, "dhome_flag", 1);
                        DwMain.SetItemDecimal(1, "ehome_flag", 0);
                    }
                    else if (status_was == 5)
                    {
                        DwMain.SetItemDecimal(1, "ahome_flag", 0);
                        DwMain.SetItemDecimal(1, "bhome_flag", 0);
                        DwMain.SetItemDecimal(1, "chome_flag", 0);
                        DwMain.SetItemDecimal(1, "dhome_flag", 0);
                        DwMain.SetItemDecimal(1, "ehome_flag", 1);
                    }
                }
                catch (Exception ex) { ex.ToString(); }
                #endregion

                #region รับเลขที่ใบคำขอล่าสุด
                //String lastDocNo = GetLastDocNo(DwMain.GetItemDecimal(1, "capital_year"));
                //DwMain.SetItemString(1, "assist_docno", lastDocNo);
                #endregion

                //}
                //#endregion

                DwMem.SetItemString(1, "coop_id", state.SsCoopId);
                DwDetail.SetItemDecimal(1, "capital_year", capital_year);
                //หลุดจาก if , else
                //---get item----
                try { entry_date = DwMain.GetItemDateTime(1, "req_date").ToString("dd/MM/yyyy", WebUtil.EN); }
                catch { entry_date = state.SsWorkDate.ToString("dd/MM/yyyy", WebUtil.EN); }
                //ดึงค่าเงินเดือน มาเซตให้กับตัวแปร salary_amt
                try { salary_amt = DwMain.GetItemDecimal(1, "salary_amt"); }
                catch { salary_amt = 0; }
                try { assist_amt = DwDetail.GetItemDecimal(1, "asnreqmaster_assist_amt"); }
                catch { assist_amt = 0; }
                deptaccount_no = DwMain.GetItemString(1, "deptaccount_no");
                //---------------
                string expense_bank = "", expense_branch = "";
                try { expense_bank = DwDetail.GetItemString(1, "expense_bank"); }
                catch { }
                try { expense_branch = DwDetail.GetItemString(1, "expense_branch"); }
                catch { }

                //switch (HdActionStatus.Value)
                //{
                if ((docNo == "" || docNo == null) && reqStatus == 8)
                {
                    #region เพิ่มข้อมูล (ตาราง MBMEMBMASTER, ASNREQMASTER, asnreqschooldet, asnmemsalary, asndoccontrol, asnslippayout)
                    //case "Insert":

                    string sqlMaxPosttovc_flag = "select max(posttovc_flag) as max_posttovc_flag from asnreqmaster where member_no = '" + member_no + "'";
                    Sdt dt = WebUtil.QuerySdt(sqlMaxPosttovc_flag);
                    string max_posttovc_flag = dt.Rows[0]["max_posttovc_flag"].ToString();
                    if (max_posttovc_flag == "" || max_posttovc_flag == null) { max_posttovc_flag = "0"; }
                    int MaxPosttovc_flag = (int.Parse(max_posttovc_flag) + 1);
                    DwMain.SetItemDecimal(1, "posttovc_flag", MaxPosttovc_flag);

                    assist_docno = GetLastDocNo(capital_year);
                    DwMain.SetItemString(1, "assist_docno", assist_docno);
                    DwDetail.SetItemString(1, "assist_docno", assist_docno);
                    try { moneytype_code = DwMain.GetItemString(1, "moneytype_code").Trim(); }
                    catch { moneytype_code = "CBT"; }
                    try { req_date = DwMain.GetItemDateTime(1, "req_date"); }
                    catch { req_date = state.SsWorkDate; }
                    approve_tdate = req_date.ToString("dd/MM/yyyy", WebUtil.EN);
                    Decimal ahome_flag = 0, bhome_flag = 0, chome_flag = 0, dhome_flag = 0, ehome_flag = 0;
                    try { ahome_flag = DwMain.GetItemDecimal(1, "ahome_flag"); }
                    catch { }
                    try { bhome_flag = DwMain.GetItemDecimal(1, "bhome_flag"); }
                    catch { }
                    try { chome_flag = DwMain.GetItemDecimal(1, "chome_flag"); }
                    catch { }
                    try { dhome_flag = DwMain.GetItemDecimal(1, "dhome_flag"); }
                    catch { }
                    try { ehome_flag = DwMain.GetItemDecimal(1, "ehome_flag"); }
                    catch { }
                    sqlStr = @" SELECT * 
                                FROM asnsenvironmentvar 
                                WHERE envgroup  = 'school' 
                                AND   envcode   = 'scholarships_12'";
                    dt = ta.Query(sqlStr);
                    if (dt.Next())
                    {
                        system_code = dt.GetString("system_code");
                        tofrom_accid = dt.GetString("tofrom_accid");
                    }

                    try { int iii = DwUtil.UpdateDataWindow(DwMem, pbl, "MBMEMBMASTER"); }
                    catch { HdActionStatus.Value = "Insert"; }
                    //LtServerMessage.Text = WebUtil.CompleteMessage("UPDATE: MBMEMBMASTER"); return;
                    //int ii = DwUtil.InsertDataWindow(DwMain, pbl, "ASNREQMASTER");
                    String sqlInsert = @"INSERT INTO ASNREQMASTER  
					                                (   ASSIST_DOCNO,   
					                                    CAPITAL_YEAR,   
					                                    MEMBER_NO,   
					                                    ASSISTTYPE_CODE,   
					                                    ASSIST_AMT,   
					                                    REQ_STATUS,   
					                                    REQ_DATE,   
					                                    APPROVE_DATE,   
					                                    EXPENSE_BANK,   
					                                    EXPENSE_BRANCH,   
					                                    POSTTOVC_FLAG,   
                                                        ASSISTDEPT_AMT,
					                                    DEPTACCOUNT_NO,   
					                                    MONEYTYPE_CODE,   
					                                    AHOME_FLAG,   
					                                    BHOME_FLAG,   
					                                    CHOME_FLAG,   
					                                    DHOME_FLAG,   
					                                    DEAD_STATUS,   
					                                    COOP_ID,   
					                                    EHOME_FLAG,   
					                                    MAR_FLAG,   
					                                    TOFROM_ACCID)  
			                                    VALUES ( '" + assist_docno + @"',   
					                                     " + capital_year + @",   
					                                     '" + member_no + @"',   
					                                     '10',   
					                                     " + assist_amt + @",   
					                                     8,   
					                                     to_date('" + entry_date + @"', 'dd/mm/yyyy'),   
					                                     to_date('" + entry_date + @"', 'dd/mm/yyyy'),   
					                                     '" + expense_bank + @"',   
					                                     '" + expense_branch + @"',   
					                                     " + MaxPosttovc_flag + @",
                                                         0,   
					                                     '" + deptaccount_no + @"',   
					                                     '" + moneytype_code + @"',   
					                                     " + ahome_flag + @",   
					                                     " + bhome_flag + @",   
					                                     " + chome_flag + @",   
					                                     " + dhome_flag + @",   
					                                     0,   
					                                     '" + state.SsCoopId + @"',   
					                                     " + ehome_flag + @",   
                                                         " + status_mar + @",   
					                                     '" + tofrom_accid + @"'   
                                                         ) ";
                    ta.Exe(sqlInsert);
                    //LtServerMessage.Text = WebUtil.CompleteMessage("Insert: ASNREQMASTER</br>" + sqlInsert); return;
                    try
                    { //DwUtil.InsertDataWindow(DwDetail, pbl, "ASNREQSCHOOLDET"); 
                        sqlInsert = @"INSERT INTO ASNREQSCHOOLDET  
                                                 ( ASSIST_DOCNO,   
                                                   CAPITAL_YEAR,   
                                                   CAPITAL_DATE,   
                                                   CHILDPRENAME_CODE,   
                                                   CHILD_NAME,   
                                                   CHILD_SURNAME,   
                                                   CHILDSCHOOL_NAME,   
                                                   CHILD_GPA,   
                                                   CHILDBIRTH_DATE,   
                                                   CHILD_AGE,   
                                                   ASSIST_AMT,   
                                                   SCHOLARSHIP_LEVEL,   
                                                   ENTRY_ID,   
                                                   SCHOLARSHIP_TYPE,   
                                                   ENTRY_DATE,   
                                                   LEVEL_SCHOOL,   
                                                   CHILD_CARD_PERSON )  
                                          VALUES ( '" + assist_docno + @"',   
					                                " + capital_year + @",    
                                                   to_date('" + DwDetail.GetItemDateTime(1, "capital_date").ToString("dd/MM/yyyy", WebUtil.EN) + @"', 'dd/mm/yyyy'),   
                                                   '" + childprename_code + @"',   
                                                   '" + child_name + @"',   
                                                   '" + child_surname + @"',   
                                                   '" + childschool_name + @"',   
                                                    " + child_gpa + @",   
                                                   to_date('" + DwDetail.GetItemDateTime(1, "childbirth_date").ToString("dd/MM/yyyy", WebUtil.EN) + @"', 'dd/mm/yyyy'),   
                                                    " + child_age + @",   
                                                    " + assist_amt + @", 
                                                    " + scholarship_level + @",   
                                                   '" + state.SsUsername + @"',   
                                                    " + scholarship_type + @",   
                                                    to_date('" + entry_date + @"', 'dd/mm/yyyy'),   
                                                    " + level_School + @",   
                                                   '" + child_card_person + @"'  
                                                    )  ";
                        ta.Exe(sqlInsert);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        HdActionStatus.Value = "Insert";
                    }
                    //LtServerMessage.Text = WebUtil.CompleteMessage("Insert: asnreqschooldet</br>"); return;
                    try
                    {
                        sqlStr = @"INSERT INTO asnmemsalary(capital_year  ,  member_no  ,  assist_docno  ,
                                                            salary_amt    ,  entry_date  ,  membgroup_code, 
                                                            entry_id)
                                                    VALUES('" + capital_year + "','" + member_no + "','" + assist_docno + @"',
                                                            '" + salary_amt + "',to_date('" + entry_date + "', 'dd/mm/yyyy'),'" + membgroup_code + @"',
                                                            '" + state.SsUsername + @"' )";
                        ta.Exe(sqlStr);
                    }
                    catch (Exception ex) { ex.ToString(); }
                    //update เลขที่เอกสาร = 10: ทุนการศึกษา
                    try
                    {
                        sqlStr = @" UPDATE  asndoccontrol
                                    SET     last_docno   = '" + WebUtil.Right(assist_docno, 6) + @"'
                                    WHERE   doc_prefix   = 'AS' 
                                    AND     doc_year     = '" + capital_year + @"'";
                        ta.Exe(sqlStr);
                    }
                    catch (Exception ex) { ex.ToString(); }

                    //Insert asnslippayout
                    try
                    {
                        sqlStr = @"INSERT INTO asnslippayout (
                                                payoutslip_no,                            member_no,                        slip_date,                   operate_date,
                                                   payout_amt,                          slip_status,                    depaccount_no,                assisttype_code,
                                               moneytype_code,                             entry_id,                       entry_date,                         seq_no,
                                                 capital_year,                        sliptype_code,                     tofrom_accid
                                                     ) 
                                  VALUES(
                                      '" + assist_docno + @"',                 '" + member_no + @"',to_date('" + approve_tdate + @"','dd/MM/yyyy'),to_date('" + approve_tdate + @"','dd/MM/yyyy'),
                                        '" + assist_amt + @"',                                  '8',           '" + deptaccount_no + @"',                        '10',
                                    '" + moneytype_code + @"',          '" + state.SsUsername + @"',to_date('" + approve_tdate + @"','dd/MM/yyyy'),        " + 1 + @",
                                      '" + capital_year + @"',               '" + system_code + @"',         '" + tofrom_accid + @"'
                                        )";
                        ta.Exe(sqlStr);
                    }
                    catch (Exception ex) { ex.ToString(); }

                    NewClear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ");
                    HdActionStatus.Value = "Insert";
                    //break;
                    #endregion
                }
                else
                {
                    #region แก้ไขข้อมูล (Update ตาราง MBMEMBMASTER, ASNREQMASTER, asnmemsalary, asnreqschooldet)
                    //case "Update":
                    //try { DwUtil.UpdateDataWindow(DwMem, pbl, "MBMEMBMASTER"); }
                    //catch { HdActionStatus.Value = "Insert"; }

                    DwUtil.UpdateDataWindow(DwMain, pbl, "ASNREQMASTER");

                    try
                    {
                        sqlStr = @" UPDATE  asnmemsalary
                                    SET     salary_amt    = '" + salary_amt + @"',
                                            entry_id      = '" + state.SsUsername + @"'
                                    WHERE   assist_docno  = '" + docNo + @"'
                                    AND     capital_year  = " + capital_year;
                        ta.Exe(sqlStr);
                    }
                    catch (Exception ex) { ex.ToString(); }

                    try
                    {
                        //DwUtil.UpdateDataWindow(DwDetail, pbl, "ASNREQSCHOOLDET");
                        sqlStr = @" UPDATE  ASNREQSCHOOLDET
                                    SET     CHILDPRENAME_CODE   = {2},
                                            CHILD_NAME          = {3},
                                            CHILD_SURNAME       = {4},
                                            CHILDBIRTH_DATE     = {5},
                                            CHILD_AGE           = {6},
                                            CHILD_CARD_PERSON   = {7},
                                            SCHOLARSHIP_TYPE    = {8},
                                            LEVEL_SCHOOL        = {9},
                                            SCHOLARSHIP_LEVEL   = {10},
                                            CHILDSCHOOL_NAME    = {11},
                                            CHILD_GPA           = {12}
                                    WHERE	ASSIST_DOCNO        = {0}   
                                    AND     CAPITAL_YEAR        = {1} ";
                        sqlStr = WebUtil.SQLFormat(sqlStr,
                                                    docNo,
                                                    capital_year,
                                                    childprename_code,
                                                    child_name,
                                                    child_surname,
                                                    childbirth_date,
                                                    child_age,
                                                    child_card_person,
                                                    scholarship_type,
                                                    level_School,
                                                    scholarship_level,
                                                    childschool_name,
                                                    child_gpa
                                                    );
                        ta.Exe(sqlStr);
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                        HdActionStatus.Value = "Insert";
                    }
                    try
                    {
                        string assistdocno = DwMain.GetItemString(1, "assist_docno");
                        string coop_id = state.SsCoopId;
                        string updatecoopid = @"    update  asnreqmaster 
                                                    set     coop_id      = '" + coop_id + @"' 
                                                    where   assist_docno = '" + docNo + @"'
                                                    AND     capital_year = " + capital_year;
                        ta.Exe(updatecoopid);
                    }
                    catch { }
                    NewClear();
                    LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการแก้ไขข้อมูลสำเร็จ");
                    HdActionStatus.Value = "Insert";
                    //break;
                    #endregion
                }
                ta.Close();  //ปิด Connection Data Base.
            }

            //}
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
        public void WebSheetLoadEnd()
        {
            //DwMain.SetItemDateTime(1, "pay_date", state.SsWorkDate);
            //DwMain.SetItemDateTime(1, "req_date", state.SsWorkDate);
            //DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            //DwMain.SetItemDateTime(1, "approve_date", state.SsWorkDate);

            //deptaccount_no
            // tDwDetail.Eng2ThaiAllRow();
            //try {
            //    //branch_id
            //    DwMain.SetItemString(1, "branch_id", ss_dropdown_scholarship_branch);
            //}
            //catch { }

            //try
            //{
            //    String membNo = DwUtil.GetString(DwMem, 1, "member_no", "");
            //    if (membNo != "")
            //    {
            //        DwUtil.RetrieveDDDW(DwMain, "deptaccount_no", pbl, membNo);
            //        DataWindowChild dc = DwMain.GetChild("deptaccount_no");
            //        String accNo = DwUtil.GetString(DwMain, 1, "deptaccount_no", "").Trim();
            //        if (dc.RowCount > 0 && accNo == "")
            //        {
            //            DwMain.SetItemString(1, "deptaccount_no", dc.GetItemString(1, "deptaccount_no"));
            //        }
            //    }
            //}
            //catch (Exception ex) { ex.ToString(); }

            // DDDW - Tambol
            try
            {
                String pvCode = DwUtil.GetString(DwMem, 1, "district_code", "");
                if (pvCode != "")
                {
                    DwUtil.RetrieveDDDW(DwMem, "tambol_code", pbl, pvCode.Trim());
                }
            }
            catch (Exception ex) { ex.ToString(); }
            // DDDW - District
            try
            {
                String pvCode = DwUtil.GetString(DwMem, 1, "province_code", "");
                if (pvCode != "")
                {
                    DwUtil.RetrieveDDDW(DwMem, "district_code", pbl, pvCode.Trim());
                }
            }
            catch (Exception ex) { ex.ToString(); }
            // DDDW - Province
            try { DwUtil.RetrieveDDDW(DwMem, "province_code", pbl, null); }
            catch (Exception ex) { ex.ToString(); }

            try { DwUtil.RetrieveDDDW(DwMain, "assisttype_code", pbl, state.SsCoopId); }
            catch (Exception ex) { ex.ToString(); }
            try { DwUtil.RetrieveDDDW(DwMain, "capital_year", pbl, null); }
            catch (Exception ex) { ex.ToString(); }
            try { DwUtil.RetrieveDDDW(DwMain, "req_status", pbl, null); }
            catch (Exception ex) { ex.ToString(); }
            try
            {
                String pvCode = DwUtil.GetString(DwMain, 1, "district_code", "");
                if (pvCode != "")
                {
                    DwUtil.RetrieveDDDW(DwMain, "tambol_code", pbl, pvCode.Trim());
                }
            }
            catch { }
            try
            {
                String pvCode = DwUtil.GetString(DwMain, 1, "province_code", "");
                if (pvCode != "")
                {
                    DwUtil.RetrieveDDDW(DwMain, "district_code", pbl, pvCode.Trim());
                }
            }
            catch (Exception ex) { ex.ToString(); }
            //try { DwUtil.RetrieveDDDW(DwMain, "province_code", pbl, null); }
            //catch (Exception ex) { ex.ToString(); }

            try {                /* DwUtil.RetrieveDDDW(DwDetail, "scholarship_type", pbl, state.SsCoopId);*/            }
            catch (Exception ex) { ex.ToString(); }
            try { DwUtil.RetrieveDDDW(DwDetail, "childprename_code", pbl, null); }
            catch { }

            try
            {
                try { Ds.Reset(); }
                catch (Exception ex) { ex.ToString(); }
                DwMem.SaveDataCache();
                DwMain.SaveDataCache();
                DwDetail.SaveDataCache();
                ta.Close();  //ปิด Connection Data Base.
            }
            catch (Exception ex) { ex.ToString(); }
        }

        //    private int CheckMemberDuration (string memberno){
        //        int status = 1;
        //        DateTime date1 = DateTime.Now.Date;
        //        DateTime date2 = DateTime.Now.Date;
        //        string member_no1 = DwMem.GetItemString(1, "member_no");
        //        string sqlmember_date = "select member_date from mbmembmaster where member_no = '" + memberno + "'";
        //        Sdt da = WebUtil.QuerySdt(sqlmember_date);
        //        if (da.Next())
        //        {
        //            date1 = Convert.ToDateTime(da.Rows[0]["member_date"].ToString());
        //        }
        //        TimeSpan diffTime = date2.Subtract(date1);
        //        int days = int.Parse(diffTime.Days.ToString());
        //        if (days < 365) {
        //            status = 0;

        //        }
        //        return status;
        //}

        #endregion

        #region Functions
        private void RetrieveBankBranch()
        {
            String bank;

            try
            {
                bank = DwMain.GetItemString(1, "expense_bank");
            }
            catch { bank = ""; }

            DataWindowChild DcBankBranch = DwMain.GetChild("expense_branch");
            DcBankBranch.SetTransaction(sqlca);
            DcBankBranch.Retrieve();
            DcBankBranch.SetFilter("bank_code = '" + bank + "'");
            DcBankBranch.Filter();
        }
        private void RetreiveDwMem()
        {
            String member_no;
            //String coop_id;

            member_no = HfMemberNo.Value;
            //coop_id = state.SsCoopId;
            //object[] args = new object[2];
            //args[0] = member_no;
            //args[1] = coop_id;

            //DwMem.Reset();
            //DwUtil.RetrieveDataWindow(DwMem, "as_capital.pbl", null, args);
            DwUtil.RetrieveDataWindow(DwMem, pbl, null, member_no);
            if (DwMem.RowCount < 1)
            {
                LtAlert.Text = "<script>Alert()</script>";
                return;
            }

            GetMemberDetail();
            //GetSCHDetail();
        }
        /*        private String GetLastDocNo(Decimal capital_year)
                {
                    //try { capital_year = DwMain.GetItemDecimal(1, "capital_year"); }
                    //catch { capital_year = state.SsWorkDate.Year; }
                    try
                    {
                        //sqlStr = @"SELECT last_docno 
                        //FROM   asndoccontrol
                        //WHERE  doc_prefix = 'IMP' and
                        //doc_year = '" + capital_year + "'";
                        String sqlStr = "select max(assist_docno) as last_no from asnreqmaster where capital_year='"
                                         + capital_year + "' and assist_docno like 'AS%'";
                        Sdt dt = WebUtil.QuerySdt(sqlStr);
                        if (dt.Next())
                        {
                            String lastDocNo = dt.GetString(0);
                            if (lastDocNo == "")
                            {
                                lastDocNo = "000000";
                            }
                            else
                            {
                                lastDocNo = lastDocNo.Replace("AS", "");
                            }
                            int iii = int.Parse(lastDocNo);
                            assist_docno = "AS" + (iii + 1).ToString("000000");
                            return assist_docno;
                        }
                        else
                        {
                            String assist_docno = "AS000000";
                            return assist_docno;
                        }
                    }
                    catch (Exception ex)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                    }

                    return "";
                }
         * */
        private String GetLastDocNo(Decimal capital_year)
        {
            String doc_year = "";
            try
            {
                doc_year = WebUtil.Right(capital_year.ToString(), 2);
                sqlStr = @" SELECT substr( doc_year, 3, 2 ) || last_docno as last_docno 
                            FROM   asndoccontrol
                            WHERE  doc_prefix   = 'AS' 
                            and    doc_year     = '" + capital_year + "'";
                dt = ta.Query(sqlStr);
                dt.Next();

                try
                {
                    assist_docno = dt.GetString("last_docno");
                    if (assist_docno == "")
                    {
                        assist_docno = "000001";
                    }
                    else if (assist_docno == "000000")
                    {
                        assist_docno = "000001";
                    }
                    else
                    {
                        assist_docno = "000000" + Convert.ToString(Convert.ToInt32(assist_docno) + 1);
                        assist_docno = WebUtil.Right(assist_docno, 8);
                    }
                    assist_docno = "AS" + assist_docno;
                    assist_docno = assist_docno.Trim();
                }
                catch
                {
                    assist_docno = "AS" + doc_year + "000001";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

            return assist_docno;
        }

        private void GetMemberDetail()
        {
            HdActionStatus.Value = "Insert";
            String member_no = DwMem.GetItemString(1, "member_no");
            string card_person;
            member_no = WebUtil.MemberNoFormat(member_no);
            try
            {
                //String SqlLastperiod = "select last_period from shsharemaster where member_no ='" + member_no + "'";
                //Sdt dtcheckperiod = WebUtil.QuerySdt(SqlLastperiod);
                //if (dtcheckperiod.Rows.Count > 0)
                //{
                //    int period = Convert.ToInt32(dtcheckperiod.Rows[0]["last_period"].ToString());
                //    if (period < 13)
                //    {
                //        LtServerMessage.Text = WebUtil.WarningMessage("ทะเบียน " + member_no + " ยังไม่สามารถทำการขอทุนได้");
                //        DwMem.Reset();
                //        DwMem.InsertRow(0);
                //        DwMain.Reset();
                //        DwMain.InsertRow(0);
                //        DwDetail.Reset();
                //        DwDetail.InsertRow(0);
                //        return;
                //    }
                //}
            }
            catch { }
            //Retrieve DwMem
            DwUtil.RetrieveDataWindow(DwMem, pbl, null, member_no);
            capital_year = DwMain.GetItemDecimal(1, "capital_year");
            try { surname_child = DwMem.GetItemString(1, "memb_surname"); }
            catch { surname_child = ""; }
            try
            {
                String reMember_no = DwMem.GetItemString(1, "member_no").Trim();
                if (reMember_no == "")
                {
                    DwMem.Reset();
                    DwDetail.Reset();
                }
            }
            catch { DwMem.Reset(); DwDetail.Reset(); }
            if (DwMem.RowCount > 0)//กรณีใบคำขอ เก่า/ มีใบคำขอ ของสมาชิก เลขที่ n ไปแล้ว
            {
                //int checkold = CheckMemberDuration(WebUtil.MemberNoFormat(DwMem.GetItemString(1, "member_no")));
                //if (checkold == 0) {
                //    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถสมัครสมาชิกได้เนื่องจากอายุสมาชิกไม่ครบ 1 ปี");
                //    return;
                //}
                //LEK เซตค่าให้กับตัวแปร member_age (นับอายุสมาชิกถึง วันที่ยื่นขอทุน)
                sqlStr = @" select  ft_calagemth(member_date , {1}) as member_age
                                from    mbmembmaster 
                                where   member_no = {0} ";
                DateTime req_date = DwMain.GetItemDateTime(1, "req_date");
                sqlStr = WebUtil.SQLFormat(sqlStr, member_no, req_date);
                dt = WebUtil.QuerySdt(sqlStr);
                if (dt.Next())
                {
                    String member_age = dt.GetString("member_age") + " ปี";
                    DwMain.SetItemString(1, "member_age", member_age);
                    //LtServerMessage.Text = WebUtil.CompleteMessage("1.อายุการเป็นสมาชิก : " + member_age);
                }

                //ดึงค่าเงินเดือนมาจาก Field salary_amount
                try { salary_amt = DwMem.GetItemDecimal(1, "salary_amount"); }
                catch { salary_amt = 0; }
                //เซตค่าเงินเดือนให้กับตัวแปร salary_amt
                DwMain.SetItemDecimal(1, "salary_amt", salary_amt);

                //1.เช็คว่า อายุการเป็นสมาชิกถึง 1 ปี หรือไม่ :ถ้าไม่ ให้ฟ้องเตือนว่า อายุการเป็นสมาชิกไม่ถึง 1 ปี
//                String sqlMemAge = @" select to_number(FT_CALAGEMTH(member_Date, sysdate)) as member_age
//                                      from   mbmembmaster
//                                      where  member_no = '" + member_no + @"'";
//                Sdt dtMemAge = WebUtil.QuerySdt(sqlMemAge);
//                if (dtMemAge.Next())
//                {
//                    if (dtMemAge.GetDecimal("member_age") < 1) { LtServerMessage.Text = WebUtil.WarningMessage("อายุการเป็นสมาชิกไม่ถึง 1 ปี"); return; }
//                }
                //LtServerMessage.Text = WebUtil.CompleteMessage("1.เช็คว่า อายุการเป็นสมาชิกถึง 1 ปี หรือไม่ :ถ้าไม่ ให้ฟ้องเตือนว่า อายุการเป็นสมาชิกไม่ถึง 1 ปี : " + dtMemAge.GetDecimal("member_age"));
                //2. เช็คว่า เงินเดือนเกิน 50,000 บาท หรือไม่ :ถ้าใช่ ให้ฟ้องเตือนว่า เงินเดือนเกิน 50,000 บาท
                //if (salary_amt > 50000) { LtServerMessage.Text = WebUtil.WarningMessage("เงินเดือนเกิน 50,000 บาท"); return; }
                //LtServerMessage.Text = WebUtil.CompleteMessage("2. เช็คว่า เงินเดือนเกิน 50,000 บาท หรือไม่ :ถ้าใช่ ให้ฟ้องเตือนว่า เงินเดือนเกิน 50,000 บาท : " + salary_amt);
                String sqlMain = "select * from ASNREQMASTER where member_no = '" + member_no + "' and assist_docno like 'AS%'";
                Sdt dtMain = WebUtil.QuerySdt(sqlMain);
                if (dtMain.Next())
                {

                    // comment ไปก่อน เพื่อให้ ป้อนบุตรคนที่ 2,3,... ได้ กรณีสมาชิกเลขที่เดียวกัน
                    ////Retrieve DwMain
                    //DwUtil.RetrieveDataWindow(DwMain, pbl, tDwMain, dtMain.GetString("assist_docno"), dtMain.GetInt32("capital_year"));
                    ////Retrieve DwDetail
                    //DwUtil.RetrieveDataWindow(DwDetail, pbl, tDwMain, dtMain.GetString("assist_docno"), dtMain.GetInt32("capital_year"), member_no);

                    ////status_was
                    //if (DwMain.GetItemDecimal(1, "ahome_flag") == 1)
                    //{
                    //    DwMain.SetItemDecimal(1, "status_was", 1);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "bhome_flag") == 1)
                    //{
                    //    DwMain.SetItemDecimal(1, "status_was", 2);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "chome_flag") == 1)
                    //{
                    //    DwMain.SetItemDecimal(1, "status_was", 3);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "dhome_flag") == 1)
                    //{
                    //    DwMain.SetItemDecimal(1, "status_was", 4);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "ehome_flag") == 1)
                    //{
                    //    DwMain.SetItemDecimal(1, "status_was", 5);
                    //}
                    ////mar_flag
                    //if (DwMain.GetItemDecimal(1, "mar_flag") == 1)
                    //{
                    //    DwMain.SetItemDecimal(1, "mar_flag", 1);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "mar_flag") == 2)
                    //{
                    //    DwMain.SetItemDecimal(1, "mar_flag", 2);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "mar_flag") == 3)
                    //{
                    //    DwMain.SetItemDecimal(1, "mar_flag", 3);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "mar_flag") == 4)
                    //{
                    //    DwMain.SetItemDecimal(1, "mar_flag", 4);
                    //}
                    //else if (DwMain.GetItemDecimal(1, "mar_flag") == 5)
                    //{
                    //    DwMain.SetItemDecimal(1, "mar_flag", 5);
                    //}

                    ////tDwMain.Eng2ThaiAllRow();
                    ////tDwDetail.Eng2ThaiAllRow();
                    //LtServerMessage.Text = WebUtil.WarningMessage("เลขทะเบียนสมาชิก " + member_no + " มีใบคำขออยู่ในระบบแล้ว");
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    DwDetail.Reset();
                    DwDetail.InsertRow(0);

                    DwMain.SetItemDateTime(1, "req_date", state.SsWorkDate);//req_tdate
                    DwMain.SetItemDateTime(1, "approve_date", state.SsWorkDate);
                    DwMain.SetItemString(1, "member_no", member_no);

                    //ข้อมูลชุด = ครั้งที่ขอ :
                    string sqlMaxPosttovc_flag = "select max(posttovc_flag) as max_posttovc_flag from asnreqmaster where member_no = '" + member_no + "' and assisttype_code = '10' and capital_year = " + (capital_year - 1) + " and pay_status = 1";
                    dt = WebUtil.QuerySdt(sqlMaxPosttovc_flag);
                    string max_posttovc_flag = dt.Rows[0]["max_posttovc_flag"].ToString();
                    if (max_posttovc_flag == "" || max_posttovc_flag == null) { max_posttovc_flag = "0"; }
                    int maxposttovc_flag = (int.Parse(max_posttovc_flag) + 1);
                    DwMain.SetItemDecimal(1, "posttovc_flag", maxposttovc_flag);

                    //4. เช็คว่า รับทุนติดต่อกันแล้ว 2 ปี หรือไม่ :ถ้าใช่ ให้ฟ้องเตือนว่า รับทุนติดต่อกันแล้ว 2 ปี (ปี 2555-2556)
                    String sqlCountMembNo = @"  select  count(member_no) as count_membno
                                                from    asnreqmaster 
                                                where   member_no = '" + member_no + @"' 
                                                and     assisttype_code = '10' 
                                                and     capital_year in( " + (capital_year - 1) + @", " + (capital_year - 2) + @" )
                                                and     pay_status = 1 ";
                    Sdt dtCountMembNo = WebUtil.QuerySdt(sqlCountMembNo);
                    if (dtCountMembNo.Next())
                    {
                        if (dtCountMembNo.GetDecimal("count_membno") == 2) { LtServerMessage.Text = WebUtil.WarningMessage("รับทุนติดต่อกันแล้ว 2 ปี (ปี " + (capital_year - 2) + "-" + (capital_year - 1) + ")"); return; }
                    }
                    //LtServerMessage.Text = WebUtil.CompleteMessage("4. เช็คว่า รับทุนติดต่อกันแล้ว 2 ปี หรือไม่ :ถ้าใช่ ให้ฟ้องเตือนว่า รับทุนติดต่อกันแล้ว 2 ปี (ปี 2555-2556) : " + dtCountMembNo.GetDecimal("count_membno"));
                    //DwMain.SetItemDateTime(1, "req_date", state.SsWorkDate);//req_tdate
                    //DwMain.SetItemDateTime(1, "approve_date", state.SsWorkDate);
                    SetDefaultAccid();
                    try { card_person = DwMem.GetItemString(1, "card_person"); }
                    catch { card_person = ""; }
                    card_person = formatIDCard(card_person);
                    DwMem.SetItemString(1, "card_person", card_person);

                    //school_home เจ้าบ้าน1=A,เช่า2=B,พ่อแม่3=C,พี่น้อง4=D,ผู้อื่น5=E
                    string sqlEnvvalue = "select envvalue from asnsenvironmentvar where envcode = 'school_home'";
                    dt = WebUtil.QuerySdt(sqlEnvvalue);
                    if (dt.Next())
                    {
                        DwMain.SetItemDecimal(1, "status_was", dt.GetDecimal("envvalue"));
                    }

                    tDwMain.Eng2ThaiAllRow();
                    tDwDetail.Eng2ThaiAllRow();
                }
                else  //กรณีใบคำขอ ใหม่/ ยังไม่มีใบคำขอ ของสมาชิก เลขที่ n และตรวจสอบเงื่อไขต่างๆ
                {
                    //int checknew = CheckMemberDuration(WebUtil.MemberNoFormat(DwMem.GetItemString(1, "member_no")));
                    //if (checknew == 0)
                    //{
                    //    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถสมัครสมาชิกได้เนื่องจากอายุสมาชิกไม่ครบ 1 ปี");
                    //    return;
                    //}
                    DwMain.Reset();
                    DwMain.InsertRow(0);
                    DwDetail.Reset();
                    DwDetail.InsertRow(0);
                    DwMain.SetItemDecimal(1, "posttovc_flag", 1);
                    DwMain.SetItemDateTime(1, "req_date", state.SsWorkDate);//req_tdate
                    DwMain.SetItemDateTime(1, "approve_date", state.SsWorkDate);
                    DwMain.SetItemString(1, "member_no", member_no);
                    SetDefaultAccid();
                    try { card_person = DwMem.GetItemString(1, "card_person"); }
                    catch { card_person = ""; }
                    card_person = formatIDCard(card_person);
                    DwMem.SetItemString(1, "card_person", card_person);

                    deptaccount_no = SetdefaultDeptacc(member_no, state.SsCoopId);
                    deptaccount_no = formatDeptaccid(deptaccount_no);
                    DwMain.SetItemString(1, "deptaccount_no", deptaccount_no);
                    String sql = "";
                    ////maxdamage
                    //sql = "select envvalue from asnsenvironmentvar where envcode = 'maxdamage'";
                    //Sdt dt = WebUtil.QuerySdt(sql);
                    //if (dt.Next())
                    //{
                    //    DwMain.SetItemDecimal(1, "assist_amt", dt.GetDecimal("envvalue"));
                    //}

                    //school_series
                    //sql = "select envvalue from asnsenvironmentvar where envcode = 'school_series'";
                    //Sdt dt = WebUtil.QuerySdt(sql);
                    //if (dt.Next())
                    //{
                    //    DwMain.SetItemDecimal(1, "posttovc_flag", dt.GetDecimal("envvalue"));
                    //}

                    //school_home เจ้าบ้าน1=A,เช่า2=B,พ่อแม่3=C,พี่น้อง4=D,ผู้อื่น5=E
                    sql = "select envvalue from asnsenvironmentvar where envcode = 'school_home'";
                    dt = WebUtil.QuerySdt(sql);
                    if (dt.Next())
                    {
                        DwMain.SetItemDecimal(1, "status_was", dt.GetDecimal("envvalue"));
                    }
                    //LEK เซตค่าให้กับตัวแปร member_age (นับอายุสมาชิกถึง วันที่ยื่นขอทุน)
                    sqlStr = @" select  ft_calagemth(member_date , {1}) as member_age
                                from    mbmembmaster 
                                where   member_no = {0} ";
                    req_date = DwMain.GetItemDateTime(1, "req_date");
                    sqlStr = WebUtil.SQLFormat(sqlStr, member_no, req_date);
                    dt = WebUtil.QuerySdt(sqlStr);
                    if (dt.Next())
                    {
                        String member_age = dt.GetString("member_age") + " ปี";
                        DwMain.SetItemString(1, "member_age", member_age);
                        //LtServerMessage.Text = WebUtil.CompleteMessage("2.อายุการเป็นสมาชิก : " + member_age);
                    }
                    tDwMain.Eng2ThaiAllRow();
                    tDwDetail.Eng2ThaiAllRow();
                }
                //เซตค่าเงินเดือนให้กับตัวแปร salary_amt
                DwMain.SetItemDecimal(1, "salary_amt", salary_amt);
                DwDetail.SetItemString(1, "child_surname", surname_child);

                //5. เช็คว่า ส่งคำขอซ้ำ (สามี-ภรรยา) หรือไม่ :ถ้าใช่ ให้ฟ้องเตือนว่า ส่งคำขอซ้ำ (สามี-ภรรยา)
                //คู่สมรส : mate_name
                String mate_name = "", mate_cardperson = "";
                try { mate_name = DwMem.GetItemString(1, "mate_name"); }
                catch { }
                //เลขบัตรฯ คู่สมรส : mate_cardperson
                try { mate_cardperson = DwMem.GetItemString(1, "mate_cardperson").Trim(); }
                catch { }
                if (mate_name != null && mate_name != "" && mate_cardperson != null && mate_cardperson != "")
                {
                    String sqlMbMate = @" select 1 from mbmembmaster mb, asnreqmaster am
                                          where mb.member_no        = am.member_no
                                          and	mb.card_person      = '" + mate_cardperson + @"'
                                          and 	am.assisttype_code  = '10' 
                                          and 	am.capital_year     = " + capital_year;
                    dt = WebUtil.QuerySdt(sqlMbMate);
                    if (dt.Next()) { LtServerMessage.Text = WebUtil.WarningMessage("ส่งคำขอซ้ำ (สามี-ภรรยา)"); return; }
                }
                //LtServerMessage.Text = WebUtil.CompleteMessage("5. เช็คว่า ส่งคำขอซ้ำ (สามี-ภรรยา) หรือไม่ :ถ้าใช่ ให้ฟ้องเตือนว่า ส่งคำขอซ้ำ (สามี-ภรรยา) : " + mate_cardperson);
            }
            else
            {
                //LtServerMessage.Text = WebUtil.WarningMessage("ไม่พบทะเบียนสมาชิกเลขที่ " + member_no);
                Response.Write("<script language='javascript'>alert('ไม่พบทะเบียนสมาชิกเลขที่ " + member_no + "'\n );</script>");
                DwMem.Reset();
                DwMem.InsertRow(0);
                DwMain.Reset();
                DwMain.InsertRow(0);
                DwDetail.Reset();
                DwDetail.InsertRow(0);
            }
            try { capital_year = DwMain.GetItemDecimal(1, "capital_year"); }
            catch { capital_year = state.SsWorkDate.Year + 543; }
            DwMain.SetItemDecimal(1, "capital_year", capital_year);
            //LtServerMessage.Text = WebUtil.CompleteMessage("ปีที่ขอทุน : " + capital_year);
            //GetLastDocNo(capital_year);
            //DwMain.SetItemString(1, "assist_docno", assist_docno);
            //DwDetail.SetItemString(1, "assist_docno", assist_docno);
            //LEK เซตค่าให้กับตัวแปร member_age (นับอายุสมาชิกถึง วันที่ยื่นขอทุน)
            sqlStr = @" select  ft_calagemth(member_date , {1}) as member_age, 
                                FT_CNVTDATE(member_date, 1) as member_tdate
                        from    mbmembmaster 
                        where   member_no = {0} ";
            DateTime req_date2 = DwMain.GetItemDateTime(1, "req_date");
            sqlStr = WebUtil.SQLFormat(sqlStr, member_no, req_date2);
            dt = WebUtil.QuerySdt(sqlStr);
            if (dt.Next())
            {
                String member_age = dt.GetString("member_age") + " ปี";
                String member_tdate = dt.GetString("member_tdate").Trim();
                DwMain.SetItemString(1, "member_age", member_age);      //เซตค่าให้กับตัวแปร อายุการเป็นสมาชิก :
                DwMain.SetItemString(1, "member_tdate", member_tdate);  //เซตค่าให้กับตัวแปร วันที่เป็นสมาชิก:
                //LtServerMessage.Text = WebUtil.CompleteMessage("3.อายุการเป็นสมาชิก : " + member_age + "</br>วันที่เป็นสมาชิก : " + member_tdate);
            }
            GetMoney();
            //LtServerMessage.Text = WebUtil.CompleteMessage("3.อายุการเป็นสมาชิก : " + DwMain.GetItemString(1, "member_age") +" ปี");
        }
        private void JsPostCopyAddress()
        {
            //memb_addr, //addr_group, //soi, //mooban, //road, //tambol, //district_code, //province_code, //postcode
            //1 memb_addr
            try
            {
                DwMain.SetItemString(1, "memb_addr", DwMem.GetItemString(1, "memb_addr").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "memb_addr"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //2 addr_group
            try
            {
                DwMain.SetItemString(1, "addr_group", DwMem.GetItemString(1, "addr_group").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "addr_group"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //3 soi
            try
            {
                DwMain.SetItemString(1, "soi", DwMem.GetItemString(1, "soi").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "soi"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //4 mooban
            try
            {
                DwMain.SetItemString(1, "mooban", DwMem.GetItemString(1, "mooban").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "mooban"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //5 road
            try
            {
                DwMain.SetItemString(1, "road", DwMem.GetItemString(1, "road").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "road"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //6 tambol
            try
            {
                DwMain.SetItemString(1, "tambol_code", DwMem.GetItemString(1, "tambol_code").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "tambol"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //7 district_code
            try
            {
                DwMain.SetItemString(1, "district_code", DwMem.GetItemString(1, "district_code").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "district_code"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //8 province_code
            try
            {
                DwMain.SetItemString(1, "province_code", DwMem.GetItemString(1, "province_code").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "province_code"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
            //9 postcode
            try
            {
                DwMain.SetItemString(1, "postcode", DwMem.GetItemString(1, "postcode").Trim());
            }
            catch
            {
                try { DwMain.SetItemNull(1, "postcode"); }
                catch { }
            }
            //-----------------------------------------------------------------------------------
        }

        //------------------------ ทุนศึกษา -------------------------------------------
        private void NewClear()
        {
            #region Old code ไม่ใช้
            //DwMem.Reset();
            //DwMain.Reset();
            //DwDetail.Reset();

            //DwMem.InsertRow(0);
            //DwMain.InsertRow(0);
            //DwDetail.InsertRow(0);

            //level_School = Convert.ToDecimal(Session["level_School"]);
            //if (level_School != 0)
            //{
            //    DwDetail.SetItemDecimal(1, "level_School", level_School);

            //    DwUtil.RetrieveDDDW(DwDetail, "scholarship_level", "as_capital.pbl", null);
            //    DataWindowChild Dc = DwDetail.GetChild("scholarship_level");
            //    Dc.SetFilter("school_group =" + level_School + "");
            //    Dc.Filter();

            //    Session["level_School"] = 0;
            //}

            //now_year = DateTime.Now.Year;
            //DwMain.SetItemDecimal(1, "capital_year", now_year + 543);

            //DwMain.SetItemDecimal(1, "req_status", 8);
            //DwMain.SetItemString(1, "assisttype_code", "10");
            //DwMain.SetItemDateTime(1, "pay_date", state.SsWorkDate);
            //DwMain.SetItemDateTime(1, "req_date", state.SsWorkDate);
            ////DwMain.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            //DwMain.SetItemDateTime(1, "approve_date", state.SsWorkDate);
            //SetDefaultAccid();
            #endregion
            DwMem.Reset();
            DwMain.Reset();
            DwDetail.Reset();
            DwMem.InsertRow(0);
            DwMain.InsertRow(0);
            DwDetail.InsertRow(0);
            DwMain.SetItemDateTime(1, "req_date", state.SsWorkDate);
            DwMain.SetItemDateTime(1, "approve_date", state.SsWorkDate);
            DwDetail.SetItemDateTime(1, "childbirth_date", state.SsWorkDate);
            SetDefaultAccid();

            DwUtil.RetrieveDDDW(DwMain, "moneytype_code", pbl, null);
            DwUtil.RetrieveDDDW(DwMain, "assisttype_code", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(DwMain, "capital_year", pbl, null);
            try { capital_year = DwMain.GetItemDecimal(1, "capital_year"); }
            catch { capital_year = state.SsWorkDate.Year + 543; }
            DwMain.SetItemDecimal(1, "capital_year", capital_year);
            //GetLastDocNo(capital_year);
            DwMain.SetItemString(1, "assist_docno", assist_docno);
            DwUtil.RetrieveDDDW(DwMain, "req_status", pbl, null);

            DwUtil.RetrieveDDDW(DwDetail, "scholarship_type", pbl, state.SsCoopId);
            DwUtil.RetrieveDDDW(DwDetail, "scholarship_level", pbl, null);
            DwUtil.RetrieveDDDW(DwDetail, "childprename_code", pbl, null);

            tDwMain.Eng2ThaiAllRow();
            tDwDetail.Eng2ThaiAllRow();
        }
        private void GetSCHDetail()
        {

        }
        private void GetMoney()
        {
            Sdt dt;
            String sqlStr = "";
            String Hdscholarshiptype = "", Hdlevelschool = "";
            String scholarship_type = "";
            Decimal scholarship1_amt = 0;

            //Hdscholarshiptype = Hdscholarship_type.Value.Trim();
            try { Hdscholarshiptype = DwDetail.GetItemString(1, "scholarship_type"); }
            catch { Hdscholarshiptype = Hdscholarship_type.Value.Trim(); }
            //LtServerMessage.Text = WebUtil.CompleteMessage("ประเภททุน : " + Hdscholarshiptype);
            try { Hdlevelschool = Hdlevel_school.Value.Trim(); }
            catch { }
            //LtServerMessage.Text = WebUtil.CompleteMessage("กำลังศึกษาระดับชั้น : " + Hdlevelschool);
            //HC scholarshiptype and levelschool
            switch (Hdscholarshiptype)
            {
                case "1"://ทุนส่งเสริม
                    scholarship_type = "scholarships_12";
                    break;
                case "2"://ทุนดีเด่น
                    //LEK ดึงค่ากำลังศึกษาระดับชั้น : มาเซตค่าให้กับตัวแปร Hdlevelschool
                    sqlStr = @" select  school_group
                                from    asnucfschoollevel
                                where   school_level = {0}
                                ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, Hdlevelschool);
                    Sdt dtSchoolGroup = WebUtil.QuerySdt(sqlStr);
                    if (dtSchoolGroup.Next()) { Hdlevelschool = Convert.ToString(dtSchoolGroup.GetDecimal("school_group")); }
                    if (Hdlevelschool == "1") //อนุบาล
                    {
                        scholarship_type = "outsatnding_people_lv1";
                    }
                    else if (Hdlevelschool == "2")//ระดับประถมศึกษาตอนต้น
                    {
                        scholarship_type = "outsatnding_people_lv2";
                    }
                    else if (Hdlevelschool == "3")//ระดับประถมศึกษาตอนปลาย
                    {
                        scholarship_type = "outsatnding_people_lv3";
                    }
                    else if (Hdlevelschool == "4")//ระดับมัธยมศึกษาตอนต้น
                    {
                        scholarship_type = "outsatnding_people_lv4";
                    }
                    else if (Hdlevelschool == "5")//ระดับมัธยมศึกษาตอนปลายหรือประกาศนียบัตรวิชาชีพ(ปวช.)
                    {
                        scholarship_type = "outsatnding_people_lv5";
                    }
                    else if (Hdlevelschool == "6")//ระดับอุดมศึกษาปี1
                    {
                        scholarship_type = "outsatnding_people_lv6";
                    }
                    break;
            }

            sqlStr = @"SELECT *
                       FROM asnsenvironmentvar
                       WHERE envcode = '" + scholarship_type + @"' ";

            dt = WebUtil.QuerySdt(sqlStr);
            dt.Next();
            try { scholarship1_amt = Convert.ToDecimal(dt.GetDouble("envvalue")); }
            catch { scholarship1_amt = 0; }

            DwMain.SetItemDecimal(1, "assist_amt", scholarship1_amt);
            DwDetail.SetItemDecimal(1, "asnreqmaster_assist_amt", scholarship1_amt);
            DwDetail.SetItemDecimal(1, "assist_amt", scholarship1_amt);
            //LtServerMessage.Text = WebUtil.CompleteMessage("รวมเงิน : " + scholarship1_amt);
            FilterScholarship();
        }
        private void FilterScholarship()
        {
            try { level_School = DwDetail.GetItemDecimal(1, "level_school"); }
            catch { level_School = 0; }
            Session["level_School"] = level_School;
            //LtServerMessage.Text = WebUtil.CompleteMessage("กำลังศึกษาระดับชั้น : " + level_School);
            DwUtil.RetrieveDDDW(DwDetail, "scholarship_level", pbl, null);
            DataWindowChild Dc = DwDetail.GetChild("scholarship_level");
            Dc.SetFilter("school_group =" + level_School + "");
            Dc.Filter();

            //GetMoney();
        }
        private void CheckReQuest()
        {
            String member_no;
            try
            {
                member_no = DwMem.GetItemString(1, "member_no");
            }
            catch { member_no = ""; }

            //last_year = CheckReq(member_no);
            //capital_year = DwMain.GetItemDecimal(1, "capital_year");
            //if (last_year == capital_year - 1)
            //{
            //    LtServerMessage.Text = WebUtil.WarningMessage("สมาชิกรายนี้ขอไปแล้ว");
            //    return;
            //}
        }
        private void RetrieveDwMain()
        {
            string card_person, deptaccount_no;
            try
            {
                DwUtil.RetrieveDataWindow(DwMem, pbl, null, HfMemNo.Value);
                try { card_person = DwMem.GetItemString(1, "card_person"); }
                catch { card_person = ""; }
                card_person = formatIDCard(card_person);
                DwMem.SetItemString(1, "card_person", card_person);

                DwUtil.RetrieveDataWindow(DwMain, pbl, tDwMain, HfAssistDocNo.Value.ToString(), Convert.ToDecimal(HfCapitalYear.Value));
                try { deptaccount_no = DwMain.GetItemString(1, "deptaccount_no"); }
                catch { deptaccount_no = ""; }
                deptaccount_no = formatDeptaccid(deptaccount_no);
                DwMain.SetItemString(1, "deptaccount_no", deptaccount_no);

                DwUtil.RetrieveDataWindow(DwDetail, pbl, tDwDetail, HfAssistDocNo.Value.ToString(), Convert.ToDecimal(HfCapitalYear.Value), HfMemNo.Value.ToString());
                try { card_person = DwDetail.GetItemString(1, "child_card_person"); }
                catch { card_person = ""; }
                card_person = formatIDCard(card_person);
                DwDetail.SetItemString(1, "child_card_person", card_person);
                setAccItemDwDetail();

                HdActionStatus.Value = "Update";

                string sql = "select * from asnreqmaster where assist_docno = '" + HfAssistDocNo.Value + "' and capital_year = " + Convert.ToDecimal(HfCapitalYear.Value);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Rows.Count > 0)
                {
                    //Session["ss_dropdown_scholarship_branch"] = dt.Rows[0]["branch_id"];
                    //ss_dropdown_scholarship_branch = Session["ss_dropdown_scholarship_branch"].ToString();
                    if (dt.Rows[0]["ahome_flag"].ToString() == "1")
                    {
                        DwMain.SetItemDecimal(1, "status_was", 1);
                    }
                    if (dt.Rows[0]["bhome_flag"].ToString() == "1")
                    {
                        DwMain.SetItemDecimal(1, "status_was", 2);
                    }
                    if (dt.Rows[0]["chome_flag"].ToString() == "1")
                    {
                        DwMain.SetItemDecimal(1, "status_was", 3);
                    }
                    if (dt.Rows[0]["dhome_flag"].ToString() == "1")
                    {
                        DwMain.SetItemDecimal(1, "status_was", 4);
                    }
                    if (dt.Rows[0]["ehome_flag"].ToString() == "1")
                    {
                        DwMain.SetItemDecimal(1, "status_was", 5);
                    }
                    //ดึงค่าเงินเดือนมาจาก Field salary_amount
                    try { salary_amt = DwMem.GetItemDecimal(1, "salary_amount"); }
                    catch { salary_amt = 0; }
                    //เซตค่าเงินเดือนให้กับตัวแปร salary_amt
                    DwMain.SetItemDecimal(1, "salary_amt", salary_amt);
                }
            }
            catch { }
        }
        private Int16 CheckMarried(String member_no)
        {
            Int32 row;
            Int16 same_married = 1;
            Sdt dt;
            String sqlStr;

            sqlStr = @"SELECT *
                       FROM   asnreqmaster
                       WHERE  married_member = '" + member_no + @"' and
                              assisttype_code = '10'";
            dt = WebUtil.QuerySdt(sqlStr);
            dt.Next();

            row = dt.GetRowCount();
            if (row > 0)
            {
                same_married = 0;
            }

            return same_married;
        }
        private Int16 CheckNameSon(String member_no, Decimal capital_year, String son_name, String son_surname)
        {
            String name, surname;
            Int16 same_flag = 1;


            Ds.LibraryList = "C:/GCOOP_ALL/CORE/GCOOP/Saving/DataWindow/app_assist/as_capital.pbl";
            Ds.DataWindowObject = "d_member_name_school";

            Ds.SetTransaction(sqlca);
            Ds.Retrieve(member_no, capital_year);

            for (int i = 1; i <= Ds.RowCount; i++)
            {
                name = Ds.GetItemString(i, "child_name").Trim();
                surname = Ds.GetItemString(i, "child_surname").Trim();

                if ((son_name == name) && (son_surname == surname))
                {
                    same_flag = 0;
                    level_School_same = Ds.GetItemDecimal(i, "level_School");
                    seq_no_same = Ds.GetItemDecimal(i, "seq_no");
                }
                else
                {
                    same_flag = 1;
                }
            }

            return same_flag;
        }
        private void CheckLevelSchool()
        {
            if (DwDetail.GetItemDecimal(1, "scholarship_type") == 3)
            {
                object[] args = new object[4];
                args[0] = 1;
                args[1] = 2;
                args[2] = 3;
                args[3] = 4;
                DwUtil.RetrieveDDDW(DwDetail, "level_school", pbl, args);
            }
            else if (DwDetail.GetItemDecimal(1, "scholarship_type") == 2)
            {
                object[] args = new object[4];
                args[0] = 6;
                args[1] = 7;
                args[2] = 8;
                args[3] = 9;
                DwUtil.RetrieveDDDW(DwDetail, "level_school", pbl, args);
            }
            else
            {
                DwUtil.RetrieveDDDW(DwDetail, "level_school", pbl, null);
            }
        }
        private void PostDepptacount()
        {
            deptaccount_no = Hfdeptaccount_no.Value;
            DwMain.SetItemString(1, "deptaccount_no", deptaccount_no);
            if (DwDetail.GetItemString(1, "tofrom_accid") == "51020100")
            {
                try { member_no = DwMem.GetItemString(1, "member_no"); }
                catch { member_no = ""; }
                sqlStr = @" SELECT expense_accid 
                                    FROM mbmembmaster
                                    WHERE member_no = '" + member_no + @"'";
                dt = ta.Query(sqlStr);

                if (dt.Next())
                {
                    expense_accid = dt.GetString("expense_accid");
                }
                DwDetail.SetItemString(1, "deptaccount_no", expense_accid);
            }
        }
        private void PostToFromAccid()
        {
            //try { moneytype_code = DwMain.GetItemString(1, "moneytype_code"); }
            //catch { moneytype_code = ""; }
            //DwUtil.RetrieveDDDW(DwMain, "tofrom_accid", pbl, moneytype_code);

            try { moneytype_code = DwDetail.GetItemString(1, "moneytype_code"); }
            catch { moneytype_code = ""; }
            DwUtil.RetrieveDDDW(DwDetail, "tofrom_accid", pbl, moneytype_code);
            if (moneytype_code == "CSH")
            {
                DwDetail.SetItemString(1, "tofrom_accid", "11010100");
                DwDetail.Describe("t_25.Visible");
                DwDetail.Modify("t_25.Visible=false");
                DwDetail.Describe("tofrom_accid.Visible");
                DwDetail.Modify("tofrom_accid.Visible=false");
                DwDetail.Describe("t_26.Visible");
                DwDetail.Modify("t_26.Visible=false");
                DwDetail.Describe("deptaccount_no.Visible");
                DwDetail.Modify("deptaccount_no.Visible=false");
            }
            //LEK ดึงค่าเริ่มต้นของคู่บัญชี กำหนดเงื่อนไขจากประเภทการจ่ายเงิน: moneytype_code  นำมาเซตค่าให้กับคู่บัญชี: tofrom_accid
            String tofrom_accid = "";
            sqlStr = @" SELECT 	CMUCFTOFROMACCID.ACCOUNT_ID
                        FROM 	CMUCFMONEYTYPE,   
			                    CMUCFTOFROMACCID,   
			                    ACCMASTER  
                        WHERE 	( CMUCFMONEYTYPE.MONEYTYPE_CODE = CMUCFTOFROMACCID.MONEYTYPE_CODE ) and  
			                    ( CMUCFTOFROMACCID.ACCOUNT_ID = ACCMASTER.ACCOUNT_ID ) and  
			                    ( CMUCFMONEYTYPE.MONEYTYPE_CODE = '" + moneytype_code + @"' ) AND
            		            ( CMUCFTOFROMACCID.SLIPTYPE_CODE = 'LWD' ) AND
            		            ( CMUCFTOFROMACCID.DEFAULTPAYOUT_FLAG = 1 ) ";
            dt = ta.Query(sqlStr);
            if (dt.Next())
            {
                tofrom_accid = dt.GetString("ACCOUNT_ID");
            }
            DwDetail.SetItemString(1, "tofrom_accid", tofrom_accid);
        }
        private void SetDefaultAccid()
        {
            //DwMain.SetItemString(1, "moneytype_code", "CBT");
            DwDetail.SetItemString(1, "moneytype_code", "CBT");
            PostToFromAccid();
            //DwMain.SetItemString(1, "tofrom_accid", "11035200");
            //DwDetail.SetItemString(1, "tofrom_accid", "11035200"); //LEK Comment เปลี่ยนไปเรียกใช้ฟังก์ชัน PostToFromAccid() เพื่อเซตค่าให้กับคู่บัญชี: tofrom_accid
            //LEK ดึงค่าเริ่มต้นของเลขที่บัญชีมาเซตค่าให้กับ Field = เลขที่บัญชี : deptaccount_no
            try { member_no = DwMem.GetItemString(1, "member_no"); }
            catch { member_no = ""; }
            sqlStr = @" SELECT  expense_accid 
                        FROM    mbmembmaster
                        WHERE   member_no = '" + member_no + @"'";
            dt = ta.Query(sqlStr);

            if (dt.Next())
            {
                expense_accid = dt.GetString("expense_accid");
            }
            DwDetail.SetItemString(1, "deptaccount_no", expense_accid);
        }

        private string formatIDCard(string idcard)
        {
            string[] id = new string[5];
            idcard = idcard.Replace("-", "");
            if (idcard.Length == 13)
            {
                id[0] = idcard.Substring(0, 1);
                id[1] = idcard.Substring(1, 4);
                id[2] = idcard.Substring(5, 5);
                id[3] = idcard.Substring(10, 2);
                id[4] = idcard.Substring(12, 1);

                idcard = id[0] + "-" + id[1] + "-" + id[2] + "-" + id[3] + "-" + id[4];
            }
            return idcard;
        }
        private string unfomatIDCard(string idcard)
        {
            idcard = idcard.Replace("-", "");
            return idcard;
        }

        private string formatDeptaccid(string deptaccid)
        {
            string[] deptid = new string[5];
            deptaccid = deptaccid.Replace("-", "");
            deptaccid = deptaccid.Trim();
            if (deptaccid.Length == 12)
            {
                deptid[0] = deptaccid.Substring(0, 2);
                deptid[1] = deptaccid.Substring(2, 3);
                deptid[2] = deptaccid.Substring(5, 1);
                deptid[3] = deptaccid.Substring(6, 5);
                deptid[4] = deptaccid.Substring(11, 1);

                deptaccid = deptid[0] + "-" + deptid[1] + "-" + deptid[2] + "-" + deptid[3] + "-" + deptid[4];
            }
            return deptaccid;
        }
        private string unformatDeptaccid(string deptaccid)
        {
            deptaccid = deptaccid.Replace("-", "");
            return deptaccid;
        }

        private string SetdefaultDeptacc(string member_no, string coop_id)
        {
            sqlStr = @"SELECT expense_accid FROM mbmembmaster WHERE coop_id = '" + coop_id + @"' AND member_no = '" + member_no + @"'";
            dt = ta.Query(sqlStr);
            if (dt.Next())
            {
                expense_accid = dt.GetString("expense_accid");
            }
            else
            {
                expense_accid = "";
            }

            return expense_accid;
        }

        private void unfomatAll()
        {
            string card_person;
            card_person = DwMem.GetItemString(1, "card_person");
            card_person = unfomatIDCard(card_person);
            DwMem.SetItemString(1, "card_person", card_person);

            deptaccount_no = DwMain.GetItemString(1, "deptaccount_no");
            deptaccount_no = unformatDeptaccid(deptaccount_no);
            DwMain.SetItemString(1, "deptaccount_no", deptaccount_no);
            String child_card_person = "";
            try { child_card_person = DwDetail.GetItemString(1, "child_card_person"); }
            catch { }
            if (child_card_person == "" || child_card_person == null) { return; }
            child_card_person = unformatDeptaccid(child_card_person);
            DwDetail.SetItemString(1, "child_card_person", child_card_person);
        }

        private void infomatAll()
        {
            string card_person;
            card_person = DwMem.GetItemString(1, "card_person");
            card_person = formatIDCard(card_person);
            DwMem.SetItemString(1, "card_person", card_person);

            deptaccount_no = DwMain.GetItemString(1, "deptaccount_no");
            deptaccount_no = formatDeptaccid(deptaccount_no);
            DwMain.SetItemString(1, "deptaccount_no", deptaccount_no);

            card_person = DwDetail.GetItemString(1, "child_card_person");
            card_person = formatIDCard(card_person);
            DwDetail.SetItemString(1, "child_card_person", card_person);
        }

        /// <summary>
        /// MiW::10/04/2014
        /// Set การจ่ายเงิน คู่บัชี เลขบัญชี ลงในฟิวด์ DwMain เพื่อรอ Save
        /// </summary>
        private void setAccItemDwMain()
        {
            string moneytype_code               //การจ่ายเงิน
                   , tofrom_accid               //เข้าบัญชี
                   , deptaccount_no             //เลขที่บัญชี
                   ;
            #region GetItem
            try { moneytype_code = DwDetail.GetItemString(1, "moneytype_code"); }
            catch { moneytype_code = ""; }
            try { tofrom_accid = DwDetail.GetItemString(1, "tofrom_accid"); }
            catch { tofrom_accid = ""; }
            try { deptaccount_no = DwDetail.GetItemString(1, "deptaccount_no"); }
            catch { deptaccount_no = ""; }
            #endregion

            deptaccount_no = unformatDeptaccid(deptaccount_no);

            DwMain.SetItemString(1, "moneytype_code", moneytype_code);
            DwMain.SetItemString(1, "tofrom_accid", tofrom_accid);
            DwMain.SetItemString(1, "deptaccount_no", deptaccount_no);
        }

        /// <summary>
        /// MiW::10/04/2014
        /// Set การจ่ายเงิน คู่บัชี เลขบัญชี ลงในฟิวด์ DwDetail กรณีเปิดใบคำขอเดิม
        /// </summary>
        private void setAccItemDwDetail()
        {
            string moneytype_code               //การจ่ายเงิน
                   , tofrom_accid               //เข้าบัญชี
                   , deptaccount_no             //เลขที่บัญชี
                   ;
            #region GetItem
            try { moneytype_code = DwMain.GetItemString(1, "moneytype_code"); }
            catch { moneytype_code = ""; }
            try { tofrom_accid = DwMain.GetItemString(1, "tofrom_accid"); }
            catch { tofrom_accid = ""; }
            try { deptaccount_no = DwMain.GetItemString(1, "deptaccount_no"); }
            catch { deptaccount_no = ""; }
            #endregion

            deptaccount_no = formatDeptaccid(deptaccount_no);

            DwDetail.SetItemString(1, "moneytype_code", moneytype_code);
            DwDetail.SetItemString(1, "tofrom_accid", tofrom_accid);
            DwDetail.SetItemString(1, "deptaccount_no", deptaccount_no);
        }

        //        private void GetMoneyFromCheck()
        //        {
        //            Sdt dt;
        //            String sqlStr;
        //            Decimal scholarship_level, scholarship_type, scholarship1_amt;

        //            scholarship_level = DwDetail.GetItemDecimal(1, "scholarship_level");
        //            scholarship_type = DwDetail.GetItemDecimal(1, "scholarship_type");

        //            sqlStr = @"SELECT scholarship1_amt
        //                               FROM asnscholarship
        //                               WHERE school_level     = '" + scholarship_level + @"' and
        //                                     scholarship_type = '" + scholarship_type + "'";
        //            dt = WebUtil.QuerySdt(sqlStr);
        //            dt.Next();
        //            try
        //            {
        //                scholarship1_amt = Convert.ToDecimal(dt.GetDouble("scholarship1_amt"));
        //            }
        //            catch { scholarship1_amt = 0; }
        //            DwMain.SetItemDecimal(1, "assist_amt", scholarship1_amt);
        //            DwDetail.SetItemDecimal(1, "asnreqmaster_assist_amt", scholarship1_amt);
        //            DwDetail.SetItemDecimal(1, "assist_amt", scholarship1_amt);
        //        }
        //---------------------------------------------------------------------------        

        protected void DisableFields()
        {

        }

        protected void EnableFields()
        {

        }
        #endregion
    }
}
