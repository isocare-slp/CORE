﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Saving.WcfCommon;
using Saving.WcfShrlon;
using Saving.WcfBusscom;
using Sybase.DataWindow;
using System.Web.Services.Protocols;
using DataLibrary;

namespace Saving.Applications.shrlon
{
    public partial class w_sheet_sl_loan_requestment_emermhd : PageWebSheet, WebSheet
    {
        public String lc_loangroup = "01";
        public String lc_loantype = null;
        String loangroup_code = "";
        private DwThDate tDwMain;
        //*******ประกาศตัวเกี่ยวกับ  Javascript********//
        protected String jsExpenseBank;
        protected String jsExpenseCode;
        protected String jsGetMemberInfo;
        protected String jsGetMemberCollno;
        protected String jsReNewPage;
        protected String jsOpenOldDocNo;
        protected String jsPostSetZero;
        protected String jsSetpriod;
        protected String jsCancelRequest;
        protected String jsRefresh;
        protected String jsCollInitP;
        protected String jsCollCondition;
        protected String jsSetDataList;
        protected String jsPostColl;
        protected String jsSumOthClr;
        protected String jsContPeriod;
        protected String jsChangeStartkeep;
        protected String jsResumLoanClear;
        protected String jsPermissSalary;
        protected String jsRevert;
        protected String jsPaycoopid;
        protected String jsObjective;
        protected String jsSetloantype;
        protected String jsPostreturn;

        //*******ประกาศตัวเกี่ยวกับ  Reprot********//
        string reqdoc_no = "";
        String member_no = "";
        string fromset = "";
        int x = 1;
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String jspopupReport;
        protected String jsrunProcess;
        protected String jspopupReportInvoice;
        protected String jsRunProcessInvoice;
        protected String jsmaxcreditperiod;
        //*******end Reprot********//

        private ShrlonClient shrlonService;
        private BusscomClient BusscomService;
        private CommonClient commonService;
        String ls_membtype = "";
        String loantype = "";
        String membtype_code = "";
        String pbl = "sl_loan_requestment_cen.pbl";
        int li_cramationstatus = 0;//สถานะฌาปนกิจ
        /// <summary>
        /// Check Init Javascript
        /// </summary>
        public void InitJsPostBack()
        {
            jsExpenseCode = WebUtil.JsPostBack(this, "jsExpenseCode");
            jsExpenseBank = WebUtil.JsPostBack(this, "jsExpenseBank");
            jsGetMemberInfo = WebUtil.JsPostBack(this, "jsGetMemberInfo");
            jsGetMemberCollno = WebUtil.JsPostBack(this, "jsGetMemberCollno");
            jsReNewPage = WebUtil.JsPostBack(this, "jsReNewPage");
            jsOpenOldDocNo = WebUtil.JsPostBack(this, "jsOpenOldDocNo");
            jsPostSetZero = WebUtil.JsPostBack(this, "jsPostSetZero");
            jsSetpriod = WebUtil.JsPostBack(this, "jsSetpriod");
            jsCancelRequest = WebUtil.JsPostBack(this, "jsCancelRequest");
            jsRefresh = WebUtil.JsPostBack(this, "jsRefresh");
            jsCollInitP = WebUtil.JsPostBack(this, "jsCollInitP");
            jsCollCondition = WebUtil.JsPostBack(this, "jsCollCondition");
            jsSetDataList = WebUtil.JsPostBack(this, "jsSetDataList");
            jsPostColl = WebUtil.JsPostBack(this, "jsPostColl");
            jsSumOthClr = WebUtil.JsPostBack(this, "jsSumOthClr");
            jsChangeStartkeep = WebUtil.JsPostBack(this, "jsChangeStartkeep");
            jsContPeriod = WebUtil.JsPostBack(this, "jsContPeriod");
            jsResumLoanClear = WebUtil.JsPostBack(this, "jsResumLoanClear");
            jsPermissSalary = WebUtil.JsPostBack(this, "jsPermissSalary");
            jsRevert = WebUtil.JsPostBack(this, "jsRevert");
            jsPaycoopid = WebUtil.JsPostBack(this, "jsPaycoopid");
            jsObjective = WebUtil.JsPostBack(this, "jsObjective");
            jsSetloantype = WebUtil.JsPostBack(this, "jsSetloantype");
            jsPostreturn = WebUtil.JsPostBack(this, "jsPostreturn");
            jsmaxcreditperiod = WebUtil.JsPostBack(this, "jsmaxcreditperiod");

            jspopupReport = WebUtil.JsPostBack(this, "jspopupReport");
            jsrunProcess = WebUtil.JsPostBack(this, "jsrunProcess");
            jspopupReportInvoice = WebUtil.JsPostBack(this, "jspopupReportInvoice");
            jsRunProcessInvoice = WebUtil.JsPostBack(this, "jsRunProcessInvoice");

            tDwMain = new DwThDate(dw_main, this);
            tDwMain.Add("loanrequest_date", "loanrequest_tdate");
            tDwMain.Add("loanrcvfix_date", "loanrcvfix_tdate");
            tDwMain.Add("startkeep_date", "startkeep_tdate");

        }

        public void WebSheetLoadBegin()
        {
            this.ConnectSQLCA();
            try
            {
                shrlonService = wcf.Shrlon;
                commonService = wcf.Common;
                BusscomService = wcf.Busscom;

            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ติดต่อ Web Service ไม่ได้");
                return;
            }

            if (IsPostBack)
            {
                this.RestoreContextDw(dw_main);
                this.RestoreContextDw(dw_coll);
                this.RestoreContextDw(dw_clear);
                this.RestoreContextDw(dw_otherclr);
                String setting = dw_coll.Describe("coop_id.Protect");
                bool isChecked = CbCheckcoop.Checked;
                if (isChecked)
                {
                    dw_coll.Modify("coop_id.Protect=0");
                    RetreiveDDDW();
                    JsExpenseCode();
                }
                else
                {

                    dw_coll.SetItemString(1, "coop_id", state.SsCoopControl);
                    dw_coll.Modify("coop_id.Protect=1");
                }
            }
            if (dw_main.RowCount < 1)
            {
                dw_main.DisplayOnly = false;
                dw_clear.DisplayOnly = false;
                dw_coll.DisplayOnly = false;
                dw_otherclr.DisplayOnly = false;
                Session["strItemchange"] = "";
                dw_main.InsertRow(0);
                dw_main.SetItemString(1, "memcoop_id", state.SsCoopControl);
                RetreiveDDDW();
                JsChangeStartkeep();
                tDwMain.Eng2ThaiAllRow();

                CbCheckcoop.Checked = false;
                HdIsPostBack.Value = "false";
            }
            if (dw_main.RowCount > 1)
            {
                dw_main.DeleteRow(dw_main.RowCount);
            }
            dw_message.Reset();
            dw_message.InsertRow(0);
            dw_message.DisplayOnly = false;
            dw_message.Visible = false;
        }

        /// <summary>
        /// Check  PostBack Javascript
        /// </summary>
        /// <param name="eventArg"></param>
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "jsGetMemberInfo")
            {
                JsGetMemberInfo();
            }
            else if (eventArg == "jsExpenseBank")
            {
                JsExpenseBank();
            }
            else if (eventArg == "jsExpenseCode")
            {
                JsExpenseCode();
            }
            else if (eventArg == "jsGetMemberCollno")
            {
                JsGetMemberCollno();
            }
            else if (eventArg == "jsReNewPage")
            {
                JsReNewPage();
            }
            else if (eventArg == "jsOpenOldDocNo")
            {
                JsOpenOldDocNo();
            }
            else if (eventArg == "jsPostSetZero")
            {
                //LtServerMessage.Text = "ยอดเงินขอกู้มากกว่ายอดเงินสิทธิ์ให้กู้ ";
                //hardcode 
                //dw_main.SetItemDecimal(1, "loanrequest_amt", 0);
            }
            else if (eventArg == "jsSetpriod")
            {
                JsSetpriod();

            }
            else if (eventArg == "jsCancelRequest")
            {

                JsCancelRequest();
            }
            else if (eventArg == "jsRefresh")
            {
            }
            else if (eventArg == "jsCollInitP")
            {
                JsCollInitP();
            }
            else if (eventArg == "jsCollCondition")
            {
                JsCollCondition();
            }
            else if (eventArg == "jsSetDataList")
            {
                JsSetDataList();
            }
            else if (eventArg == "jsPostColl")
            {
                JsPostColl();
            }
            else if (eventArg == "jsChangeStartkeep")
            {
                JsChangeStartkeep();
            }
            else if (eventArg == "jsSumOthClr")
            {
                JsSumOthClr();
            }
            else if (eventArg == "jsContPeriod")
            {
                JsContPeriod();

            }
            else if (eventArg == "jsResumLoanClear")
            {

                JsResumLoanClear();

            }
            else if (eventArg == "jsPermissSalary")
            {
                JsPermissSalary();
            }
            else if (eventArg == "jsRevert")
            {
                JsRevert();
            }
            else if (eventArg == "jsObjective")
            {
                JsObjective();
            }
            else if (eventArg == "jsPaycoopid")
            {
                JsPaycoopid();
            }

            else if (eventArg == "jspopupReport")
            {
                JspPopupReport();
            }
            else if (eventArg == "jsrunProcess")
            {
                JsRunProcess();
            }
            else if (eventArg == "jspopupReportInvoice")
            {
                JspPopupReportInvoice();
            }
            else if (eventArg == "jsRunProcessInvoice")
            {
                JsRunProcessInvoice();
            }
            else if (eventArg == "jsSetloantype")
            {
                JsSetloantype();
            }
            else if (eventArg == "jsPostreturn")
            {
                JsPostreturn();
            }
            else if (eventArg == "jsmaxcreditperiod")
            {
                Jsmaxcreditperiod();
            }


        }

        private void JsPostreturn()
        {
            String ls_loantype = dw_main.GetItemString(1, "loantype_code");

            Decimal useamt = Convert.ToDecimal(HUseamt.Value);
            Decimal loancredit_amt = dw_main.GetItemDecimal(1, "loancredit_amt");
            Decimal Maxloancredit_amt = 0;
            Decimal ldc_maxcredit = loancredit_amt + useamt;
            String sqlStrperiod = @"  SELECT COOP_ID,   
                                        LOANTYPE_CODE,   
                                        SEQ_NO,   
                                        MONEY_FROM,
                                        MONEY_TO,
                                        MAX_PERIOD
                                    FROM LNLOANTYPEPERIOD
                                    WHERE  COOP_ID ='" + state.SsCoopControl + @"'  
                                    and LOANTYPE_CODE='" + ls_loantype + @"'  ";

            Sdt dtperiod = WebUtil.QuerySdt(sqlStrperiod);
            Decimal ldc_maxperiod = 0;
            Decimal ldc_max = 0;
            if (dtperiod.Next())
            {

                ldc_maxperiod = dtperiod.GetDecimal("MAX_PERIOD");
                ldc_max = dtperiod.GetDecimal("MONEY_TO");
            }
            if (ldc_maxcredit > ldc_max)
            {
                Maxloancredit_amt = ldc_max;

                dw_main.SetItemDecimal(1, "loancredit_amt", Maxloancredit_amt);
            }
            else
            {
                dw_main.SetItemDecimal(1, "loancredit_amt", ldc_maxcredit);
            }
            dw_main.SetItemDecimal(1, "maxsend_payamt", ldc_maxperiod);
            dw_main.SetItemDecimal(1, "period_payamt", ldc_maxperiod);

        }

        private void JsSetloantype()
        {
            string membtypecode = Hdmembtype_code.Value;
            dw_main.SetItemString(1, "loantype_code", "");
            DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
            dw_main.SetItemString(1, "membtype_code", membtypecode);
            if (membtypecode == "05")
            {
                DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                dwloantype_code.SetFilter("MEMBTYPE_CODE ='" + membtypecode + "'");
                dwloantype_code.Filter();
                dw_main.SetItemString(1, "loantype_code", "11");
            }
            else if (membtypecode == "12")
            {
                DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                dwloantype_code.SetFilter("MEMBTYPE_CODE ='" + membtypecode + "'");
                dwloantype_code.Filter();
                dw_main.SetItemString(1, "loantype_code", "12");
            }
            else if (membtypecode == "13")
            {
                DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                dwloantype_code.SetFilter("MEMBTYPE_CODE ='" + membtypecode + "'");
                dwloantype_code.Filter();
                dw_main.SetItemString(1, "loantype_code", "21");
            }
            else
            {
                //mai แก้ไขเพิ่่ม filter loangroup
                DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                dwloantype_code.SetFilter("LNLOANTYPE.LOANGROUP_CODE   ='" + lc_loangroup + "'");
                dwloantype_code.Filter();

                if (dwloantype_code.RowCount > 0)
                {
                    loantype = dwloantype_code.GetItemString(1, "loantype_code");
                    dw_main.SetItemString(1, "loantype_code", loantype);
                }

            }


        }


        /// <summary>
        ///บันทึกใบคำขอ
        /// </summary>
        public void SaveWebSheet()
        {

            try
            {
                Sta ta = new Sta(sqlca.ConnectionString);
                int li_status = 1;
                Decimal ldc_apvamt = dw_main.GetItemDecimal(1, "loanrequest_amt");
                DateTime loanrcvfix_date = dw_main.GetItemDateTime(1, "loanrcvfix_date");
                String loantype_code = dw_main.GetItemString(1, "loantype_code");
                String loan_no = shrlonService.GenReqDocNo(state.SsWsPass, state.SsCoopId, loantype_code);
                dw_main.SetItemString(1, "loancontract_no", loan_no);



                dw_main.SetItemString(1, "coop_id", state.SsCoopId);
                String memcoop_id = dw_main.GetItemString(1, "memcoop_id");
                member_no = dw_main.GetItemString(1, "member_no");
                String dwMain_XML = dw_main.Describe("DataWindow.Data.XML");
                String dwColl_XML = "";
                String dwClear_XML = "";
                String dwOtherClr_XML = "";

                try
                {
                    for (int i = 1; i <= dw_coll.RowCount; i++)
                    {

                        dw_coll.SetItemString(i, "coop_id", state.SsCoopId);
                        dw_coll.SetItemString(i, "refcoop_id", state.SsCoopControl);
                    }


                    //mai กรณี ไม่มีการค้ำประกันให้สามารถบันทึกได้ 
                    if (dw_coll.RowCount > 0)
                    {
                        dwColl_XML = dw_coll.Describe("DataWindow.Data.XML");
                    }

                }
                catch { dwColl_XML = ""; }
                try
                {
                    for (int i = 1; i <= dw_clear.RowCount; i++)
                    {
                        dw_clear.SetItemString(i, "coop_id", state.SsCoopId);
                        dw_clear.SetItemString(i, "concoop_id", state.SsCoopControl);
                    }

                    //mai กรณี ไม่มีการค้ำประกันให้สามารถบันทึกได้ 
                    if (dw_clear.RowCount > 0)
                    {
                        dwClear_XML = dw_clear.Describe("DataWindow.Data.XML");
                    }

                }
                catch { dwClear_XML = ""; }


                try
                {
                    for (int i = 1; i <= dw_otherclr.RowCount; i++)
                    {
                        dw_otherclr.SetItemString(i, "coop_id", state.SsCoopId);

                    }
                    if (dw_otherclr.RowCount > 0)
                    {
                        dwOtherClr_XML = dw_otherclr.Describe("DataWindow.Data.XML");
                    }

                }
                catch { dwOtherClr_XML = ""; }
                str_itemchange strList = new str_itemchange();
                str_savereqloan strSave = new str_savereqloan();
                //strList = Session["strItemchange"] as str_itemchange;
                strList = WebUtil.str_itemchange_session(this);



                strSave.xml_main = dwMain_XML;
                strSave.xml_clear = dwClear_XML;
                strSave.xml_guarantee = dwColl_XML;
                strSave.xml_otherclr = dwOtherClr_XML;
                strSave.contcoopid = state.SsCoopControl;
                strSave.format_type = "CAT";
                strSave.entry_id = state.SsUsername;
                strSave.coop_id = state.SsCoopId;

                String period_payamt = dw_main.GetItemDecimal(1, "period_payamt").ToString("0.00");
                bool is_point1 = period_payamt.IndexOf(".00") < 0;

                String period_payment = dw_main.GetItemDecimal(1, "period_payment").ToString("0.00");
                bool is_point2 = period_payment.IndexOf(".00") < 0;
                string expense_accid = "";
                try
                {
                    expense_accid = dw_main.GetItemString(1, "expense_accid");
                }
                catch { expense_accid = ""; }

                if (is_point1 == true || is_point2 == true || expense_accid == "" || expense_accid == null)
                {

                    if (expense_accid == "" || expense_accid == null)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกบัญชีโอนเงินเข้าฝาก");
                    }
                    else if (is_point1 == true) { LtServerMessage.Text = WebUtil.ErrorMessage("จำนวนงวดเป็นทศนิยม =" + period_payamt); }
                    else if (is_point2 == true) { LtServerMessage.Text = WebUtil.ErrorMessage("ต้นชำระเป็นทศนิยม =" + period_payment); }
                    else { LtServerMessage.Text = WebUtil.ErrorMessage("ยอดค้ำน้อยกว่ายอดขอกู้ กรุณาตรวจสอบ หรือกด คำนวณ % ใหม่อีกครั้ง "); }
                }
                else
                {

                    String runningNo = shrlonService.LoanRightSaveReqloan(state.SsWsPass, ref strSave);
                    reqdoc_no = strSave.request_no;
                    txt_reqNo.Text = reqdoc_no;
                    txt_member_no.Text = member_no;
                    x = 2;
                    JsRunProcessInvoice();
                    JsRunProcess();


                    string sqlStr = @"update	lnreqloan
	                set			loanrequest_status	= " + li_status + @",
				                loanapprove_amt		= " + ldc_apvamt + @",
				                loancontract_no		= '" + loan_no + @"',
				                approve_date        =to_date('" + loanrcvfix_date.ToString("ddMMyyyy") + @"','ddmmyyyy')
	                where	( loanrequest_docno	= '" + reqdoc_no + @"') and
				                ( coop_id					= '" + state.SsCoopId + @"' )                ";
                    ta.Exe(sqlStr);


                    int ret = shrlonService.of_buildcontno_reqloan(state.SsWsPass, ref loan_no, reqdoc_no);
                    if (ret == 1)
                    {
                        LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อยแล้ว");

                        String ls_loantype = dw_main.GetItemString(1, "loantype_code");
                        Session["loantypeEmer"] = "10";

                        LinkButton2.Visible = true;
                        HdIsPostBack.Value = "true";
                        // JsReNewPage();
                    }

                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }


        public void WebSheetLoadEnd()
        {

            try
            {
                DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                //mai แก้เพิ่ม filter membtype_code

                dwloantype_code.SetFilter("LNLOANTYPE.LOANGROUP_CODE   ='" + lc_loangroup + "'");
                dwloantype_code.Filter();

                str_itemchange strList = new str_itemchange();
                strList.xml_main = dw_main.Describe("DataWindow.Data.XML");
                strList.xml_clear = dw_clear.Describe("DataWindow.Data.XML");
                Session["strItemchange"] = strList;
                //หาจำนวนงวดที่ชำระ
                string member_no = dw_main.GetItemString(1, "member_no");
                if (member_no != null || member_no != "")
                {
                    DateTime startkeep_date = dw_main.GetItemDate(1, "startkeep_date");
                    DateTime retry_date = dw_main.GetItemDateTime(1, "retry_date");
                    Int32 month_ = 12 - (startkeep_date.Month - 1);
                    Int32 retryage = (retry_date.Year - startkeep_date.Year - 1) * 12 + 9 + month_;
                    dw_main.SetItemDecimal(1, "retry_age", retryage);
                }
            }
            catch { }

            DwUtil.RetrieveDDDW(dw_coll, "loancolltype_code", pbl, null);
            dw_main.SaveDataCache();//main
            dw_coll.SaveDataCache();//หลักประกัน
            dw_clear.SaveDataCache();//หักกลบ     
            dw_otherclr.SaveDataCache();//หักอื่น
        }

        /// <summary>
        /// fillter ประเภทการจ่ายเงิน
        /// </summary>
        private void JsExpenseCode()
        {
            str_itemchange strList = new str_itemchange();
            strList = WebUtil.str_itemchange_session(this);
            string expendCode = "";
            try
            {
                expendCode = dw_main.GetItemString(1, "expense_code");
            }
            catch { }
            if (expendCode == null || expendCode == "") return;

            if ((expendCode == "CHQ") || (expendCode == "TRN") || (expendCode == "CBT") || (expendCode == "DRF") || (expendCode == "TBK"))
            {
                //dw_main.Modify("t_32.visible =0");
                //dw_main.Modify("postagefee_amt.visible =0");
                //dw_main.Modify("t_50.visible =0");
                //dw_main.Modify("tax_amt.visible =0");

                //dw_main.Modify("t_31.visible =0");
                //dw_main.Modify("emsfee_amt.visible =0");
                //dw_main.Modify("t_52.visible =0");
                //dw_main.Modify("onlinefee_amt.visible =0");

                //dw_main.Modify("expense_accid_t.visible =0");
                //dw_main.Modify("compute_12.visible =0");

                //dw_main.Modify("t_44.visible =0");
                //dw_main.Modify("compute_11.visible =0");
                //dw_main.Modify("t_47.visible =0");
                //dw_main.Modify("postagesrv_amt.visible =0");

                //dw_main.Modify("t_53.visible =0");
                //dw_main.Modify("paytoorder_desc.visible =0");
                //dw_main.Modify("paytoorder_desc_1.visible =0");

                //ฝั่งธนาคาร
                dw_main.Modify("t_20.visible =1");
                dw_main.Modify("expense_bank.visible =1");
                dw_main.Modify("t_30.visible =1");
                dw_main.Modify("expense_bank_1.visible =1");

                dw_main.Modify("t_39.visible =1");
                dw_main.Modify("expense_branch.visible =1");
                dw_main.Modify("t_27.visible =1");
                dw_main.Modify("expense_branch_1.visible =1");

                //dw_main.Modify("t_38.visible =1");
                //dw_main.Modify("expense_accid.visible =1");

                //dw_main.Modify("t_40.visible =1");
                //dw_main.Modify("bankfee_amt.visible =1");
                //dw_main.Modify("t_45.visible =1");
                //dw_main.Modify("banksrv_amt.visible =1");

                //dw_main.Modify("t_41.visible =1");
                //dw_main.Modify("compute_10.visible =1");
                try
                {

                    DwUtil.RetrieveDDDW(dw_main, "expense_bank_1", "sl_loan_requestment.pbl", null);

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                }
            }
            else if ((expendCode == "CSH") || (expendCode == "BEX") || (expendCode == "MON") || (expendCode == "MOS") || (expendCode == "MOO"))
            {
                //dw_main.Modify("t_32.visible =1");
                //dw_main.Modify("postagefee_amt.visible =1");
                //dw_main.Modify("t_50.visible =1");
                //dw_main.Modify("tax_amt.visible =1");

                //dw_main.Modify("t_31.visible =1");
                //dw_main.Modify("emsfee_amt.visible =1");
                //dw_main.Modify("t_52.visible =1");
                //dw_main.Modify("onlinefee_amt.visible =1");

                //dw_main.Modify("expense_accid_t.visible =1");
                //dw_main.Modify("compute_12.visible =1");

                //dw_main.Modify("t_44.visible =1");
                //dw_main.Modify("compute_11.visible =1");
                //dw_main.Modify("t_47.visible =1");
                //dw_main.Modify("postagesrv_amt.visible =1");

                //if (expendCode == "CSH")
                //{
                //    dw_main.Modify("t_53.visible =0");
                //    dw_main.Modify("paytoorder_desc.visible =0");
                //    dw_main.Modify("paytoorder_desc_1.visible =0");
                //}
                //else
                //{
                //    dw_main.Modify("t_53.visible =1");
                //    dw_main.Modify("paytoorder_desc.visible =1");
                //    dw_main.Modify("paytoorder_desc_1.visible =1");
                //}
                //ฝั่งธนาคาร
                dw_main.Modify("t_20.visible =0");
                dw_main.Modify("expense_bank.visible =0");
                dw_main.Modify("t_30.visible =0");
                dw_main.Modify("expense_bank_1.visible =0");

                dw_main.Modify("t_39.visible =0");
                dw_main.Modify("expense_branch.visible =0");
                dw_main.Modify("t_27.visible =0");
                dw_main.Modify("expense_branch_1.visible =0");

                //dw_main.Modify("t_38.visible =0");
                //dw_main.Modify("expense_accid.visible =0");

                //dw_main.Modify("t_40.visible =0");
                //dw_main.Modify("bankfee_amt.visible =0");
                //dw_main.Modify("t_45.visible =0");
                //dw_main.Modify("banksrv_amt.visible =0");

                //dw_main.Modify("t_41.visible =0");
                //dw_main.Modify("compute_10.visible =0");

            }
            if ((expendCode == "MON") || (expendCode == "MOS") || (expendCode == "MOO"))
            {
                if ((strList.xml_main == null) || (strList.xml_main == ""))
                {
                    strList.xml_main = dw_main.Describe("DataWindow.Data.XML");
                    strList.xml_main = shrlonService.ReCalFee(state.SsWsPass, strList.xml_main);
                    //นำเข้าข้อมูลหลัก
                    dw_main.Reset();
                    dw_main.ImportString(strList.xml_main, FileSaveAsType.Xml);
                    //DwUtil.ImportData(strList.xml_main, dw_main, tDwMain, FileSaveAsType.Xml);
                    if (dw_main.RowCount > 1) dw_main.DeleteRow(dw_main.RowCount);
                }
            }
        }


        /// <summary>
        /// fillter สาขาธนาคาร
        /// </summary>
        private void JsExpenseBank()
        {
            try
            {

                DwUtil.RetrieveDDDW(dw_main, "expense_branch_1", "sl_loan_requestment.pbl", null);
                String bankCode;
                try { bankCode = dw_main.GetItemString(1, "expense_bank").Trim(); }
                catch { bankCode = ""; }
                DataWindowChild dwExpenseBranch = dw_main.GetChild("expense_branch_1");

                dwExpenseBranch.SetFilter("CMUCFBANKBRANCH.BANK_CODE='" + bankCode + "'");
                dwExpenseBranch.Filter();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        /// <summary>
        ///  reset หน้าใหม่
        /// </summary>
        private void JsReNewPage()
        {
            dw_main.Reset();
            dw_main.InsertRow(0);
            dw_coll.Reset();
            dw_clear.Reset();
            dw_otherclr.Reset();
            dw_main.SetItemString(1, "memcoop_id", state.SsCoopControl);
            RetreiveDDDW();
            JsChangeStartkeep();
            tDwMain.Eng2ThaiAllRow();
            HdIsPostBack.Value = "false";
            DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
            dwloantype_code.SetFilter("LNLOANTYPE.LOANGROUP_CODE   ='" + lc_loangroup + "'");
            dwloantype_code.Filter();
        }

        /// <summary>
        ///  retreive datawindows dropdown
        /// </summary>
        public void RetreiveDDDW()
        {


            try
            {
                loantype = Session["loantypeEmer"].ToString();

                Session.Remove("loantype");

            }
            catch
            {
                String sql = "select min(loantype_code) from lnloantype where loangroup_code='" + lc_loangroup + "' and coop_id = '" + state.SsCoopControl + "'";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Rows.Count > 0)
                {
                    lc_loantype = dt.Rows[0][0].ToString().Trim();
                    loantype = lc_loantype;
                    Session.Remove("loantype");
                }
                else
                {
                    throw new Exception("ไม่พบประเภทเงินกู้");
                }

            }
            try
            {
                DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "expense_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "loanobjective_code_1", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "membtype_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "coop_id_1", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "memcoop_id", pbl, null);
                DwUtil.RetrieveDDDW(dw_main, "paytoorder_desc_1", pbl, null);
                DataWindowChild paytoorder = dw_main.GetChild("paytoorder_desc_1");


                if (loantype == "")
                {
                    ///<กำหนดค่าเริ่มต้น เป็นสามัญ>
                    dw_main.SetItemString(1, "loantype_code", lc_loantype);

                }
                else
                {

                    DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                    dwloantype_code.SetFilter("LNLOANTYPE.LOANGROUP_CODE   ='" + lc_loangroup + "'");
                    dwloantype_code.Filter();
                    dw_main.SetItemString(1, "loantype_code", loantype);

                }

                dw_main.SetItemString(1, "coop_id", state.SsCoopId);

                string paytoorder_desc = "";

                if (state.SsUsername == "Loan01")
                {

                    dw_main.SetItemString(1, "paytoorder_desc", "001001");
                }
                else if (state.SsUsername == "Loan02")
                {

                    dw_main.SetItemString(1, "paytoorder_desc", "001002");
                }
                else if (state.SsUsername == "Loan03")
                {

                    dw_main.SetItemString(1, "paytoorder_desc", "001003");
                }
                else if (state.SsUsername == "Loan04")
                {
                    dw_main.SetItemString(1, "paytoorder_desc", "001004");
                }

                else { dw_main.SetItemString(1, "paytoorder_desc", state.SsCoopId); }

                DwUtil.RetrieveDDDW(dw_coll, "loancolltype_code", pbl, null);
                DwUtil.RetrieveDDDW(dw_coll, "coop_id", pbl, null);
                DwUtil.RetrieveDDDW(dw_otherclr, "clrothertype_code", pbl, null);


            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        ///  init ข้อมูลสมาชิก
        /// </summary>
        private void JsGetMemberInfo()
        {
            try
            {


                JsReNewPage(); ;
                LinkButton2.Visible = false;
                // JsReNewPage();
                CbCheckcoop.Checked = false;
                Decimal ldc_sharestk, ldc_periodshramt, ldc_periodshrvalue, ldc_shrvalue;
                Decimal ldc_salary = 0, ldc_incomemth = 0, ldc_paymonth, ldc_loanrequeststatus = 0;
                Decimal ldc_shrstkvalue = 0;
                int li_shrpaystatus, li_lastperiod = 0, li_membertype;
                Decimal lndroploanall_flag = 0;
                DateTime ldtm_birth = new DateTime(), ldtm_member = new DateTime(), ldtm_work = new DateTime(), ldtm_retry = new DateTime();
                string ls_position, ls_remark, ls_membname, ls_membgroup, ls_groupname;
                string ls_membtypedesc = "", ls_controlname = "", ls_membcontrol = "", ls_appltype, ls_memno, ls_CoopControl;

                String member_no = WebUtil.MemberNoFormat(HdMemberNo.Value);
                dw_main.SetItemString(1, "member_no", member_no);

                ls_CoopControl = state.SsCoopControl; //สาขาที่ทำรายการ

                string as_xmlmessage = "";
                String bfls_loantype = "";
                String ls_loantype = "";
                String loangroup_code = "";
                String membtype_desc = "";
                String loanoftype_code = "";
                try
                {
                    bfls_loantype = dw_main.GetItemString(1, "loantype_code");
                    string sqltype = @"SELECT   DISTINCT   LNLOANMBTYPE.TYPEOFLOAN_CODE    
                                        FROM LNLOANMBTYPE  WHERE LNLOANMBTYPE.LOANTYPE_CODE ='" + bfls_loantype + @"' ";
                    Sdt dttype = WebUtil.QuerySdt(sqltype);
                    if (dttype.Next())
                    {

                        loanoftype_code = dttype.GetString("TYPEOFLOAN_CODE");
                    }

                    string sqlloan = @"SELECT LOANMBGROUP_CODE ,MEMBTYPE_DESC ,MEMBTYPE_CODE FROM MBUCFMEMBTYPE  
							   WHERE MEMBTYPE_CODE =( SELECT MEMBTYPE_CODE  FROM MBMEMBMASTER WHERE  MEMBER_NO= '" + member_no + @"')";
                    Sdt dtloan = WebUtil.QuerySdt(sqlloan);
                    if (dtloan.Next())
                    {
                        loangroup_code = dtloan.GetString("LOANMBGROUP_CODE");
                        membtype_desc = dtloan.GetString("MEMBTYPE_DESC");
                        ls_membtype = dtloan.GetString("MEMBTYPE_CODE");
                        dw_main.SetItemString(1, "membtype_code", ls_membtype);
                        dw_main.SetItemString(1, "membtype_desc", membtype_desc);
                    }
                    if (loangroup_code == lc_loangroup)
                    {

                        bfls_loantype = loanoftype_code;
                    }

                    string loan_group = @"  SELECT
                                             LNLOANMBTYPE.LOANTYPE_CODE    
                                             FROM LNLOANMBTYPE  
                                            WHERE 
                                             LNLOANMBTYPE.LOANTYPE_CODE in (SELECT LNLOANTYPE.LOANTYPE_CODE
                                            FROM   LNLOANTYPE 
                                            WHERE LNLOANTYPE.LOANGROUP_CODE =(
                                            SELECT DISTINCT LNLOANTYPE.LOANGROUP_CODE  
                                                                                    FROM LNLOANMBTYPE,   
                                                                                         LNLOANTYPE  
                                                                                   WHERE ( LNLOANMBTYPE.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                                                                                         ( LNLOANMBTYPE.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                                                                                         ( ( LNLOANMBTYPE.LOANTYPE_CODE ='" + bfls_loantype + @"' ))) )
                                             and LNLOANMBTYPE.MEMBTYPE_CODE='" + loangroup_code + @"'  and  LNLOANMBTYPE.TYPEOFLOAN_CODE='" + loanoftype_code + @"'
                                                                                        ";
                    Sdt dtloan_group = WebUtil.QuerySdt(loan_group);
                    if (dtloan_group.Next())
                    {

                        ls_loantype = dtloan_group.GetString("LOANTYPE_CODE");
                        dw_main.SetItemString(1, "loantype_code", ls_loantype);
                        dw_main.SetItemString(1, "loantype_code_1", ls_loantype);

                        string lnrequest_date = dw_main.GetItemString(1, "loanrequest_tdate");

                        String entry_year = WebUtil.Right(lnrequest_date, 4);
                        int yyyy = Convert.ToInt32(entry_year) - 543;
                        String entry_day = WebUtil.Left(lnrequest_date, 4);

                        String dd = WebUtil.Left(entry_day, 2);
                        String mm = WebUtil.Right(entry_day, 2);
                        String entry_tt = dd + "/" + mm + "/" + yyyy.ToString();


                        // เช็คว่ามีใบคำขอกู้รออนุมัติอยู่หรือไม่
                        string Message = wcf.InterPreter.Getloanrequest(state.SsConnectionIndex, ls_CoopControl, member_no, entry_tt, ls_loantype);

                        if (Message != "")
                        {
                            if (Message == "1")
                            {
                                LtServerMessage.Text = WebUtil.WarningMessage("มีใบคำขอกู้ของ ท." + member_no + " ได้ผ่านการอนุมัติแล้ว");
                            }
                            else
                            {
                                str_itemchange strList = new str_itemchange();
                                str_requestopen strRequestOpen = new str_requestopen();
                                strList = WebUtil.str_itemchange_session(this);

                                strRequestOpen.memcoop_id = state.SsCoopControl;
                                strRequestOpen.request_no = Message;
                                strRequestOpen.coop_id = state.SsCoopId;
                                strRequestOpen.format_type = "CAT";
                                strRequestOpen.xml_main = dw_main.Describe("DataWindow.Data.XML");
                                strRequestOpen.xml_clear = "";
                                strRequestOpen.xml_guarantee = "";
                                strRequestOpen.xml_insurance = "";
                                strRequestOpen.xml_intspc = "";
                                strRequestOpen.xml_otherclr = "";

                                try
                                {
                                    strRequestOpen = shrlonService.of_loanrequestopen(state.SsWsPass, strRequestOpen);

                                    //นำข้อมูลเก็บไว้ใน DataWindow
                                    dw_main.Reset();
                                    dw_main.ImportString(strRequestOpen.xml_main, FileSaveAsType.Xml);

                                    if (dw_main.RowCount > 1) dw_main.DeleteRow(dw_main.RowCount);

                                    string expendBank = "";
                                    try
                                    { expendBank = dw_main.GetItemString(1, "expense_bank"); }
                                    catch { expendBank = ""; }
                                    //ตรวจสอบ ธนาคารว่ามีหรือไม่ 
                                    if (expendBank != "")
                                    {
                                        // เรียก Method  ShowBranch() สำหรับแสดงสาขาธนาคาร
                                        JsExpenseBank();
                                    }
                                    tDwMain.Eng2ThaiAllRow();


                                    try
                                    {
                                        dw_coll.Reset();
                                        dw_coll.ImportString(strRequestOpen.xml_guarantee, FileSaveAsType.Xml);
                                    }
                                    catch
                                    {
                                        dw_coll.Reset();
                                        DwUtil.ImportData(strRequestOpen.xml_guarantee, dw_coll, null, FileSaveAsType.Xml);

                                    }
                                    try
                                    {
                                        dw_clear.Reset();
                                        dw_clear.ImportString(strRequestOpen.xml_clear, FileSaveAsType.Xml);
                                    }
                                    catch
                                    {
                                        dw_clear.Reset();
                                    }
                                    try
                                    {
                                        dw_otherclr.Reset();
                                        dw_otherclr.ImportString(LtXmlOtherlr.Text, FileSaveAsType.Xml);
                                    }
                                    catch
                                    {
                                        dw_otherclr.Reset();
                                    }

                                    strList.xml_main = strRequestOpen.xml_main;
                                    strList.xml_guarantee = strRequestOpen.xml_guarantee;
                                    strList.xml_clear = strRequestOpen.xml_clear;
                                    strList.xml_otherclr = strRequestOpen.xml_otherclr;

                                    Session["strItemchange"] = strList;
                                    LtXmlOtherlr.Text = strRequestOpen.xml_otherclr;

                                    Decimal loanrequestStatus = dw_main.GetItemDecimal(1, "loanrequest_status");

                                }
                                catch
                                {

                                }

                                LtServerMessage.Text = WebUtil.WarningMessage("มีใบคำขอกู้สำหรับวันที่" + entry_tt + "แล้ว ระบบจะดึงข้อมูลใบคำขอให้อัตโนมัติ");
                            }


                        }
                        else
                        {


                            //เช็คสิทธิ์ ก่อนขอกู้ว่ามีสัญญาเก่าค้างหรือไม่
                            int checkoldloanpayment = wcf.Shrlon.of_checkoldloanpayment(state.SsWsPass, state.SsCoopControl, member_no, ls_loantype, ref as_xmlmessage);

                            if (checkoldloanpayment != 1)   //มีสัญญาเก่าค้าง
                            {
                                if ((as_xmlmessage != "") && (as_xmlmessage != null))
                                {
                                    LtServerMessage.Text = WebUtil.WarningMessage(as_xmlmessage);
                                }
                                //hardcode ไม่เช็คสัญญาเก่า
                            }
                            else    //ไม่มีสัญญาเก่าค้าง
                            {
                                String ls_memcoopid;
                                if (HdMemcoopId.Value == "")
                                {
                                    ls_memcoopid = state.SsCoopControl;
                                }
                                else
                                {
                                    ls_memcoopid = HdMemcoopId.Value;
                                }

                                String sqlstr = @"   SELECT a.membgroup_control,
                                                b.membgroup_desc as control_desc,
                                                a.membgroup_code,
                                                a.membgroup_desc , 
                                             MBMEMBMASTER.BIRTH_DATE,   
                                             MBMEMBMASTER.MEMBER_DATE,   
                                             MBMEMBMASTER.WORK_DATE,   
                                             MBMEMBMASTER.RETRY_DATE,   
                                             MBMEMBMASTER.SALARY_AMOUNT,   
                                             MBMEMBMASTER.MEMBTYPE_CODE,   
                                             MBUCFMEMBTYPE.MEMBTYPE_DESC,   
                                             SHSHAREMASTER.LAST_PERIOD,   
                                             SHSHAREMASTER.PERIODSHARE_AMT,   
                                             mbucfprename.prename_desc||mbmembmaster.memb_name||'   '||mbmembmaster.memb_surname as member_name,   
                                             SHSHAREMASTER.PAYMENT_STATUS,   
                                             MBMEMBMASTER.POSITION_DESC,   
                                             MBMEMBMASTER.POSITION_CODE,   
                                             MBMEMBMASTER.REMARK,   
                                             MBMEMBMASTER.MEMBER_STATUS,   
                                             MBMEMBMASTER.RESIGN_STATUS,   
                                             SHSHAREMASTER.SHARESTK_AMT,   
                                             SHSHARETYPE.UNITSHARE_VALUE,   
                                             MBMEMBMASTER.MEMBER_TYPE,   
                                             MBMEMBMASTER.APPLTYPE_CODE,   MBMEMBMASTER.LNDROPGRANTEE_FLAG,
                                             MBMEMBMASTER.RETRY_STATUS,      MBMEMBMASTER.LNDROPLOANALL_FLAG, 
                                             MBMEMBMASTER.MEMBER_NO,   
                                             MBMEMBMASTER.PAUSEKEEP_FLAG,   
                                             MBMEMBMASTER.PAUSEKEEP_DATE,       
                                             MBMEMBMASTER.COOP_ID  ,MBMEMBMASTER.CREMATION_STATUS
                               FROM MBMEMBMASTER,   
                                     MBUCFMEMBGROUP a ,   MBUCFMEMBGROUP b,
                                     MBUCFPRENAME,
                                     MBUCFMEMBTYPE,
                                     SHSHAREMASTER,   
                                     SHSHARETYPE  
                               WHERE ( a.MEMBGROUP_CODE = MBMEMBMASTER.MEMBGROUP_CODE ) and  a.membgroup_control = b.membgroup_code and
                                        a.coop_id = b.coop_id and 
                                    ( SHSHAREMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and    
                                     ( MBMEMBMASTER.PRENAME_CODE = MBUCFPRENAME.PRENAME_CODE ) and
                                     ( MBMEMBMASTER.MEMBTYPE_CODE = MBUCFMEMBTYPE.MEMBTYPE_CODE ) and  
                                     ( SHSHAREMASTER.SHARETYPE_CODE = SHSHARETYPE.SHARETYPE_CODE ) and  
                                     ( MBMEMBMASTER.COOP_ID = a.COOP_ID ) and     ( MBMEMBMASTER.MEMBER_STATUS=1) AND
                                     ( MBMEMBMASTER.COOP_ID = SHSHAREMASTER.COOP_ID ) and  
                                     ( SHSHAREMASTER.COOP_ID = SHSHARETYPE.COOP_ID ) and  
                                     ( ( mbmembmaster.member_no = '" + member_no + @"' ) AND  
                                     ( shsharemaster.sharetype_code = '01' ) )    and  
                                     MBMEMBMASTER.COOP_ID   ='" + ls_memcoopid + @"' ";
                                Sdt dt = WebUtil.QuerySdt(sqlstr);//เป็น service

                                while (dt.Next())
                                {
                                    try
                                    {
                                        li_cramationstatus = dt.GetInt32("cremation_status");
                                    }
                                    catch { li_cramationstatus = 0; }

                                    try
                                    {
                                        lndroploanall_flag = dt.GetDecimal("LNDROPLOANALL_FLAG");
                                    }
                                    catch { lndroploanall_flag = 0; }
                                    ls_membname = dt.GetString("member_name");
                                    ls_membcontrol = dt.GetString("membgroup_control");
                                    ls_controlname = dt.GetString("control_desc");
                                    ls_membgroup = dt.GetString("membgroup_code");
                                    ls_groupname = dt.GetString("membgroup_desc");
                                    ldc_salary = dt.GetDecimal("salary_amount");
                                    li_lastperiod = dt.GetInt32("last_period");



                                    try
                                    {
                                        ldtm_birth = dt.GetDate("birth_date");
                                    }
                                    catch { }
                                    try
                                    {
                                        ///<หาวันที่เกษียณ>
                                        ldtm_retry = wcf.InterPreter.CalReTryDate(state.SsConnectionIndex, state.SsCoopControl, ldtm_birth);
                                    }
                                    catch { }
                                    try
                                    {
                                        ldtm_member = dt.GetDate("member_date");
                                    }
                                    catch { }
                                    try
                                    {
                                        ldtm_work = dt.GetDate("work_date");
                                    }
                                    catch { }

                                    ldc_incomemth = 0;
                                    ldc_paymonth = 0;
                                    ls_position = dt.GetString("position_desc");
                                    ls_remark = dt.GetString("remark");
                                    ldc_shrvalue = dt.GetDecimal("unitshare_value");
                                    ldc_sharestk = dt.GetDecimal("sharestk_amt");
                                    ldc_periodshramt = dt.GetDecimal("periodshare_amt");
                                    li_shrpaystatus = dt.GetInt32("payment_status");
                                    li_membertype = dt.GetInt32("member_type");
                                    ls_appltype = dt.GetString("appltype_code");
                                    ls_memno = dt.GetString("member_no");
                                    ls_memcoopid = dt.GetString("coop_id");

                                    // ls_membtype = dt.GetString("membtype_code");
                                    ls_membtypedesc = dt.GetString("membtype_desc");
                                    ldc_shrstkvalue = Convert.ToDecimal((Convert.ToInt32(ldc_shrvalue) * Convert.ToInt32(ldc_sharestk)));
                                    ldc_periodshrvalue = Convert.ToDecimal((Convert.ToInt32(ldc_periodshramt) * Convert.ToInt32(ldc_shrvalue)));



                                    dw_main.SetItemString(1, "memcoop_id", ls_memcoopid);
                                    dw_main.SetItemString(1, "coop_id", state.SsCoopId);
                                    dw_main.SetItemString(1, "member_name", ls_membname);
                                    dw_main.SetItemString(1, "mbucfmembgroup_membgroup_code", ls_membgroup);
                                    dw_main.SetItemString(1, "mbucfmembgroup_membgroup_desc", ls_groupname);
                                    dw_main.SetItemString(1, "membgroup_desc", ls_controlname);
                                    dw_main.SetItemDecimal(1, "salary_amt", ldc_salary);
                                    dw_main.SetItemDecimal(1, "share_lastperiod", li_lastperiod);
                                    dw_main.SetItemDateTime(1, "birth_date", ldtm_birth);
                                    dw_main.SetItemDateTime(1, "member_date", ldtm_member);

                                    try
                                    {
                                        dw_main.SetItemDateTime(1, "retry_date", ldtm_retry);
                                    }
                                    catch { dw_main.SetItemDateTime(1, "retry_date", DateTime.Now); }
                                    dw_main.SetItemDecimal(1, "incomemonth_other", ldc_incomemth);
                                    dw_main.SetItemDecimal(1, "paymonth_other", ldc_paymonth);
                                    dw_main.SetItemString(1, "position_desc", ls_position);
                                    dw_main.SetItemString(1, "remark", "");
                                    dw_main.SetItemString(1, "member_remark", ls_remark);

                                    dw_main.SetItemDecimal(1, "sharestk_value", ldc_shrstkvalue);
                                    dw_main.SetItemDecimal(1, "periodshare_value", ldc_periodshrvalue);
                                    dw_main.SetItemDecimal(1, "sharepay_status", li_shrpaystatus);
                                    dw_main.SetItemDecimal(1, "intestimate_amt", 0);
                                    dw_main.SetItemDecimal(1, "member_type", li_membertype);
                                    dw_main.SetItemString(1, "appltype_code", ls_appltype);

                                    try
                                    {
                                        ///<หาอายุสมาชิก>
                                        Decimal birth_age = BusscomService.of_cal_yearmonth(state.SsWsPass, ldtm_birth, DateTime.Now);
                                        dw_main.SetItemDecimal(1, "birth_age", birth_age);
                                    }
                                    catch
                                    {

                                        Decimal birth_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                                        dw_main.SetItemDecimal(1, "birth_age", birth_age);
                                    }
                                    try
                                    {
                                        ///<หาเกษียณอายุ>
                                        Decimal retry_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, ldtm_retry);
                                        String retry_agel = WebUtil.Left(retry_age.ToString("000.00"), 3);
                                        String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                                        int retryagel = Convert.ToInt32(retry_agel) * 12;
                                        Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                                        dw_main.SetItemDecimal(1, "retry_age", retryagel + retryager);
                                    }
                                    catch
                                    {
                                        Decimal retry_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                                        String retry_agel = WebUtil.Left(retry_age.ToString("000.00"), 3);
                                        String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                                        int retryagel = Convert.ToInt32(retry_agel) * 12;
                                        Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                                        dw_main.SetItemDecimal(1, "retry_age", retryagel + retryager);
                                    }
                                    try
                                    {
                                        ///<หาอายุการเป็นสมาชิก>
                                        Decimal member_age = BusscomService.of_cal_yearmonth(state.SsWsPass, ldtm_member, DateTime.Now);
                                        dw_main.SetItemDecimal(1, "member_age", member_age);
                                    }
                                    catch
                                    {
                                        Decimal member_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                                        dw_main.SetItemDecimal(1, "member_age", member_age);
                                    }
                                }

                                //DateTime adtm_reqdate = dw_main.GetItemDateTime(1, "loanrequest_date");
                                //object[] Setloantypeinfo = shrlonService.Setloantypeinfo(state.SsWsPass, ls_memcoopid, ls_loantype, adtm_reqdate);
                                dw_main.SetItemDecimal(1, "loanpayment_type", 1);
                                //dw_main.SetItemDecimal(1, "minsalary_perc", Convert.ToDecimal(Setloantypeinfo[1]));
                                //dw_main.SetItemDecimal(1, "minsalary_amt", Convert.ToDecimal(Setloantypeinfo[2]));
                                //dw_main.SetItemDecimal(1, "minsalary_inc", Convert.ToDecimal(Setloantypeinfo[3]));
                                dw_main.SetItemDecimal(1, "int_intsteptype", 1);
                                //dw_main.SetItemDecimal(1, "contract_time", Convert.ToDecimal(Setloantypeinfo[5]));
                                //dw_main.SetItemDecimal(1, "od_flag", Convert.ToDecimal(Setloantypeinfo[6]));
                                //dw_main.SetItemString(1, "loanobjective_code", "007");//Setloantypeinfo[7].ToString()
                                //dw_main.SetItemDecimal(1, "loanrcvfix_flag", Convert.ToDecimal(Setloantypeinfo[8]));
                                ////dw_main.SetItemDate(1, "loanrcvfix_date", Convert.ToDateTime(Setloantypeinfo[9]));
                                ////dw_main.SetItemDate(1, "startkeep_date", Convert.ToDateTime(Setloantypeinfo[10]));
                                //dw_main.SetItemDecimal(1, "loanrcvperiod_month", Convert.ToDecimal(Setloantypeinfo[11]));
                                //dw_main.SetItemDecimal(1, "loanrcvperiod_year", Convert.ToDecimal(Setloantypeinfo[12]));
                                //dw_main.SetItemDecimal(1, "apvimmediate_flag", Convert.ToDecimal(Setloantypeinfo[13]));
                                //dw_main.SetItemDecimal(1, "loanrequest_status", Convert.ToDecimal(Setloantypeinfo[14]));
                                //dw_main.SetItemDecimal(1, "clearinsure_flag", Convert.ToDecimal(Setloantypeinfo[15]));
                                dw_main.SetItemDecimal(1, "int_continttype", 2);
                                dw_main.SetItemDecimal(1, "int_contintrate", new Decimal(0.055));
                                dw_main.SetItemString(1, "int_continttabcode", "INT01");
                                dw_main.SetItemDecimal(1, "int_contintincrease", 0);
                                SetAcci_dept();//เซ็ค เลขที่บัญชี

                                if (lndroploanall_flag != 0)
                                {

                                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกท่านนี้ งดกู้");
                                }
                                else
                                {
                                    ///<สิทธิ์กู้สูดสุง>
                                    ///
                                    string re_lastperiod = wcf.InterPreter.GetlastperiodShare(li_lastperiod);
                                    if (re_lastperiod == "1")
                                    {
                                        Jsmaxcreditperiod();
                                    }
                                    else { LtServerMessage.Text = WebUtil.ErrorMessage("งวดการส่ค่าหุ้นของสมาชิกท่านนี้ยังไม่ถึง 4 งวด"); }
                                    ///<ตรวจหาสัญญาที่จะหักกลบ ถ้ามีก็เอามาแสดง ใน dw_clear>
                                    Genbaseloanclear();
                                }




                            } //end เช็คสิทธิ์ ก่อนขอกู้ว่ามีสัญญาเก่าค้างหรือไม่

                            DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
                            DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                            dwloantype_code.SetFilter("MEMBTYPE_CODE ='" + loangroup_code + "' and LNLOANTYPE.LOANGROUP_CODE   ='01'");
                            dwloantype_code.Filter();

                            dw_main.SetItemString(1, "loantype_code", ls_loantype);
                            dw_main.SetItemString(1, "loantype_code_1", ls_loantype);
                        }
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage("ไม่มีสิทธิ์กู้เงินประเภทนี้ เนื่องจากเป็นสมาชิกประเภท " + membtype_desc);

                        DwUtil.RetrieveDDDW(dw_main, "loantype_code_1", pbl, null);
                        DataWindowChild dwloantype_code = dw_main.GetChild("loantype_code_1");
                        dwloantype_code.SetFilter("MEMBTYPE_CODE ='01' and LNLOANTYPE.LOANGROUP_CODE   ='" + lc_loangroup + "'");
                        dwloantype_code.Filter();

                        dw_main.SetItemString(1, "loantype_code", bfls_loantype);
                        dw_main.SetItemString(1, "loantype_code_1", bfls_loantype);

                    }


                }

                catch { LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาเลือกประเภทเงินกู้ที่จะทำรายการ"); }




            }
            catch (Exception ex)
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage("JsGetMemberInfo===>" + ex);

            }
            HdIsPostBack.Value = "false";
        }

        private void Jsmaxcreditperiod()
        {
            try
            {
                String ls_loantype = dw_main.GetItemString(1, "loantype_code");
                DateTime ldtm_member;
                try
                {
                    ldtm_member = dw_main.GetItemDateTime(1, "member_date");
                }
                catch { ldtm_member = state.SsWorkDate; }

                Decimal ldc_shrstkvalue = dw_main.GetItemDecimal(1, "sharestk_value");
                Decimal ldc_salary = dw_main.GetItemDecimal(1, "salary_amt");
                String ls_memcoopid = dw_main.GetItemString(1, "memcoop_id");
                String ls_membtypedesc = dw_main.GetItemString(1, "membtype_desc");
                String member_no = dw_main.GetItemString(1, "member_no");

                ///<หาอายุงานของาสมาชิก>
                int memtime = 0;
                Decimal member_age = dw_main.GetItemDecimal(1, "member_age");
                if (member_age > 1)
                {
                    memtime = Convert.ToInt32(BusscomService.of_cal_yearmonth(state.SsWsPass, ldtm_member, state.SsWorkDate) * 12);

                }
                else
                {

                    memtime = Convert.ToInt32(member_age * 100);
                }
                ///<กำหนดวันที่จ่ายและเรียกเก็บแต่ปละเภทสัญญาวันจ่ายไม่เหมือนกัน>
                JsChangeStartkeep();

                Decimal[] max_creditperiod = new Decimal[4];

                Decimal loancredit_amt = 0;
                ls_loantype = dw_main.GetItemString(1, "loantype_code");
                ///< หาสิทธิ์กู้สูงสุด,งวดสูงสุด>
                max_creditperiod = shrlonService.CalloanpermissMHD(state.SsWsPass, ls_memcoopid, ls_loantype, ldc_salary, ldc_shrstkvalue, memtime);
                loancredit_amt = max_creditperiod[0];
                Decimal ldc_percenshare = max_creditperiod[2];
                Decimal ldc_percensaraly = max_creditperiod[3];
                if (loancredit_amt > ldc_shrstkvalue) { loancredit_amt = ldc_shrstkvalue; }


                ///<12 เกษียณรับบำนาญ,รับบำเหน็จ กู้ ฉฉ.ได้ 1 เท่าของเงินเดือน>                 
                if (ls_membtype == "12" && ls_loantype == "12") { loancredit_amt = ldc_salary * 2; }

                ///<05 ลูกจ้างชั่วคราว กู้ ฉฉ.ได้ 90% ของค่าหุ้น>
                if (ls_membtype == "05" && ls_loantype == "11")
                {
                    decimal loancredit_shr = ldc_shrstkvalue * ldc_percenshare;
                    decimal loancredit_sal = ldc_salary * ldc_percensaraly;
                    if (loancredit_shr > loancredit_sal) { loancredit_amt = loancredit_sal; } else { loancredit_amt = loancredit_shr; }
                }



                dw_main.SetItemDecimal(1, "loancredit_amt", loancredit_amt);
                dw_main.SetItemDecimal(1, "maxsend_payamt", max_creditperiod[1]);
                dw_main.SetItemDecimal(1, "period_payamt", max_creditperiod[1]);


                //defalut ค่าหุ้นเป็นหลักประกัน

                dw_coll.Reset();
                dw_coll.InsertRow(1);
                dw_coll.SetItemString(1, "loancolltype_code", "02");
                dw_coll.SetItemString(1, "ref_collno", dw_main.GetItemString(1, "member_no"));
                dw_coll.SetItemString(1, "description", dw_main.GetItemString(1, "member_name"));
                dw_coll.SetItemDecimal(1, "coll_balance", dw_main.GetItemDecimal(1, "sharestk_value"));
                dw_coll.SetItemDecimal(1, "use_amt", dw_main.GetItemDecimal(1, "sharestk_value"));
                dw_coll.SetItemString(1, "coop_id", state.SsCoopControl);




            }
            catch (Exception ex)
            {
                // LtServerMessage.Text = WebUtil.ErrorMessage("Jsmaxcreditperiod===>" + ex);
            }

        }


        /// <summary>
        /// init ข้อมูลคนค้ำ
        /// </summary>
        private void JsGetMemberCollno()
        {
            try
            {
                int row = dw_coll.RowCount;
                String ls_memcoopid;
                String ref_collno = HdRefcoll.Value;
                string loancolltype_code = dw_coll.GetItemString(row, "loancolltype_code");

                if (loancolltype_code == "01")
                {
                    //bool isChecked = CbCheckcoop.Checked;
                    //String setting = dw_coll.Describe("coop_id.Protect");
                    //if (isChecked)
                    //{
                    //    dw_coll.Modify("coop_id.Protect=0");
                    //    RetreiveDDDW();
                    //}
                    //else
                    //{

                    //    dw_coll.SetItemString(row, "coop_id", dw_main.GetItemString(1, "memcoop_id"));
                    //    dw_coll.Modify("coop_id.Protect=1");
                    //}
                    dw_coll.SetItemString(row, "ref_collno", ref_collno);

                    if (HdMemcoopId.Value == "")
                    {
                        ls_memcoopid = dw_main.GetItemString(1, "memcoop_id");
                    }
                    else
                    {
                        ls_memcoopid = HdMemcoopId.Value;
                    }
                    dw_coll.SetItemString(row, "coop_id", ls_memcoopid);

                    String[] mem_coll = shrlonService.GetMembercoll(state.SsWsPass, ls_memcoopid, ref_collno, state.SsWorkDate);

                    if (mem_coll[0] != "")
                    {
                        dw_coll.SetItemString(row, "ref_collno", mem_coll[0]);
                        dw_coll.SetItemString(row, "description", mem_coll[1]);
                        dw_coll.SetItemDecimal(row, "coll_balance", Convert.ToDecimal(mem_coll[2]));
                        HUseamt.Value = mem_coll[2];
                        JsPostreturn();
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage(mem_coll[3]);
                        dw_coll.SetItemString(row, "ref_collno", "");
                    }
                }
                else if (loancolltype_code == "02")
                {

                    dw_coll.SetItemString(row, "ref_collno", dw_main.GetItemString(1, "member_no"));
                    dw_coll.SetItemString(row, "description", dw_main.GetItemString(1, "member_name"));
                    dw_coll.SetItemDecimal(row, "coll_balance", dw_main.GetItemDecimal(1, "sharestk_value"));
                    dw_coll.SetItemDecimal(row, "use_amt", dw_main.GetItemDecimal(1, "sharestk_value"));
                    HUseamt.Value = dw_main.GetItemDecimal(1, "sharestk_value").ToString();
                    JsPostreturn();
                }
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
        }

        /// <summary>
        ///  เปิดใบคำขอเก่าขึ้นมาแก้ไข
        /// </summary>
        private void JsOpenOldDocNo()
        {

            str_itemchange strList = new str_itemchange();
            str_requestopen strRequestOpen = new str_requestopen();
            strList = WebUtil.str_itemchange_session(this);
            string docno = dw_main.GetItemString(1, "loanrequest_docno");
            string coop_id = dw_main.GetItemString(1, "coop_id");

            strRequestOpen.memcoop_id = state.SsCoopControl;
            strRequestOpen.request_no = docno;
            strRequestOpen.coop_id = coop_id;
            strRequestOpen.format_type = "CAT";
            strRequestOpen.xml_main = dw_main.Describe("DataWindow.Data.XML");
            strRequestOpen.xml_clear = "";
            strRequestOpen.xml_guarantee = "";
            strRequestOpen.xml_insurance = "";
            strRequestOpen.xml_intspc = "";
            strRequestOpen.xml_otherclr = "";

            try
            {
                strRequestOpen = shrlonService.of_loanrequestopen(state.SsWsPass, strRequestOpen);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            DateTime ldtm_birth = new DateTime();
            DateTime ldtm_retry = new DateTime();


            //นำข้อมูลเก็บไว้ใน DataWindow
            dw_main.Reset();
            dw_main.ImportString(strRequestOpen.xml_main, FileSaveAsType.Xml);

            if (dw_main.RowCount > 1) dw_main.DeleteRow(dw_main.RowCount);
            try
            {
                ldtm_birth = dw_main.GetItemDate(1, "birth_date");
            }
            catch { }
            try
            {
                ///<หาวันที่เกษียณ>

                ldtm_retry = wcf.InterPreter.CalReTryDate(state.SsConnectionIndex, state.SsCoopControl, ldtm_birth);

            }
            catch { }
            try
            {
                dw_main.SetItemDateTime(1, "retry_date", ldtm_retry);
            }
            catch { dw_main.SetItemDateTime(1, "retry_date", DateTime.Now); }

            try
            {
                ///<หาเกษียณอายุ>
                Decimal retry_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, ldtm_retry);
                String retry_agel = WebUtil.Left(retry_age.ToString("00.00"), 2);
                String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                int retryagel = Convert.ToInt32(retry_agel) * 12;
                Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                dw_main.SetItemDecimal(1, "retry_age", retryagel + retryager);
            }
            catch
            {
                Decimal retry_age = BusscomService.of_cal_yearmonth(state.SsWsPass, DateTime.Now, DateTime.Now);
                String retry_agel = WebUtil.Left(retry_age.ToString("00.00"), 2);
                String retry_ager = WebUtil.Right(retry_age.ToString(""), 2);
                int retryagel = Convert.ToInt32(retry_agel) * 12;
                Decimal retryager = (Math.Round(Convert.ToDecimal(retry_ager)) / 10) * 10;
                dw_main.SetItemDecimal(1, "retry_age", retryagel + retryager);
            }
            string expendBank = "";
            try
            { expendBank = dw_main.GetItemString(1, "expense_bank"); }
            catch { expendBank = ""; }
            //ตรวจสอบ ธนาคารว่ามีหรือไม่ 
            if (expendBank != "")
            {
                // เรียก Method  ShowBranch() สำหรับแสดงสาขาธนาคาร
                JsExpenseBank();
            }
            tDwMain.Eng2ThaiAllRow();


            try
            {
                dw_coll.Reset();
                dw_coll.ImportString(strRequestOpen.xml_guarantee, FileSaveAsType.Xml);
            }
            catch
            {
                dw_coll.Reset();
                DwUtil.ImportData(strRequestOpen.xml_guarantee, dw_coll, null, FileSaveAsType.Xml);

            }
            try
            {
                dw_clear.Reset();
                dw_clear.ImportString(strRequestOpen.xml_clear, FileSaveAsType.Xml);
            }
            catch
            {
                dw_clear.Reset();
            }
            try
            {
                dw_otherclr.Reset();
                dw_otherclr.ImportString(LtXmlOtherlr.Text, FileSaveAsType.Xml);
            }
            catch
            {
                dw_otherclr.Reset();
            }

            strList.xml_main = strRequestOpen.xml_main;
            strList.xml_guarantee = strRequestOpen.xml_guarantee;
            strList.xml_clear = strRequestOpen.xml_clear;
            strList.xml_otherclr = strRequestOpen.xml_otherclr;

            Session["strItemchange"] = strList;
            LtXmlOtherlr.Text = strRequestOpen.xml_otherclr;

            Decimal loanrequestStatus = dw_main.GetItemDecimal(1, "loanrequest_status");

        }

        /// <summary>
        /// set วันที่เริ่มเรียกเก็บ
        /// </summary>
        private void JsChangeStartkeep()
        {
            try
            {

                DateTime postingdate = new DateTime();
                DateTime processdate = new DateTime();
                DateTime ldtm_loanreceive = new DateTime();
                dw_main.SetItemDateTime(1, "loanrequest_date", state.SsWorkDate);
                DateTime ldtm_loanrequest = dw_main.GetItemDateTime(1, "loanrequest_date");
                String loantype = dw_main.GetItemString(1, "loantype_code");
                short ai_increase = 0;

                //ประเภคบุคคลค้ำ จ่ายถัดวันขอกู้อีก 3 วันทำการ
                //a hardcode
                if (loantype == "23" || loantype == "26")
                {
                    ai_increase = 3;
                    ldtm_loanreceive = wcf.Busscom.of_relativeworkdate(state.SsWsPass, ldtm_loanrequest, ai_increase);
                }
                else
                {
                    ldtm_loanreceive = ldtm_loanrequest;
                }
                int year = Convert.ToInt16(ldtm_loanreceive.Year + 543);
                short month = Convert.ToInt16(ldtm_loanreceive.Month);
                //a hardcode
                String sqlpro = " SELECT MAX(LASTPROCESS_DATE)as LASTPROCESS_DATE FROM  LNCONTMASTER ";
                Sdt dtpro = WebUtil.QuerySdt(sqlpro);
                if (dtpro.Next())
                {

                    //processdate = dtpro.GetDate("LASTPROCESS_DATE");

                    //กรณี วันที่จ่ายเงิน=วันที่ขอกู้
                    processdate = wcf.Busscom.of_getpostingdate(state.SsWsPass, ldtm_loanreceive);
                }

                //จ่ายเงินกู้หลังเรียกเก็บหรือไม่
                if (ldtm_loanreceive < processdate)
                {
                    month = Convert.ToInt16(ldtm_loanreceive.Month + 1);
                    if (month > 12)
                    {
                        month = 1;
                        year = year + 1;

                    }
                    short month_old = Convert.ToInt16(ldtm_loanreceive.Month);
                    DateTime postingdate_old = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(year), month_old);

                    int day = ldtm_loanreceive.DayOfYear - postingdate_old.DayOfYear;
                    if (day == 1)
                    {
                        month = Convert.ToInt16(ldtm_loanreceive.Month + 1);
                        postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(year), month);
                    }
                    else
                    {
                        month = Convert.ToInt16(processdate.Month + 1);
                        postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(year), month);
                    }



                }
                else if (ldtm_loanreceive > processdate)
                {
                    //ai_increase = 1;

                    ldtm_loanreceive = wcf.Busscom.of_relativeworkdate(state.SsWsPass, ldtm_loanreceive, ai_increase);
                    month = Convert.ToInt16(ldtm_loanreceive.Month + 2);
                    postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(year), month);

                }
                else
                {
                    month = Convert.ToInt16(ldtm_loanreceive.Month + 1);
                    postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(year), month);
                }

                postingdate = wcf.InterPreter.Getstartemer_shr(state.SsConnectionIndex, state.SsCoopControl, ldtm_loanreceive);
                dw_main.SetItemDate(1, "startkeep_date", postingdate);

                dw_main.SetItemDate(1, "loanrcvfix_date", ldtm_loanreceive);
                tDwMain.Eng2ThaiAllRow();

            }
            catch (Exception ex)
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage("JsChangeStartkeep===>" + ex);
            }
        }

        /// <summary>
        /// ดึงข้อมูลสัญญาหักกลบ
        /// </summary>
        private void Genbaseloanclear()
        {
            Decimal loancredit = 0;
            String member_no = dw_main.GetItemString(1, "member_no");
            DateTime loanrcvfix_date = dw_main.GetItemDate(1, "loanrcvfix_date");
            String ls_memcoopid;
            try { ls_memcoopid = dw_main.GetItemString(1, "memcoop_id"); }
            catch { ls_memcoopid = state.SsCoopId; }
            Decimal period_payment = 0, period_payamt = 0;
            String ls_contno, ls_conttype, ls_prefix, ls_permgrp, ls_intcontintcode, ls_coopid = "";
            Decimal li_minperiod = 0, li_period, li_continttype, li_transfersts = 0;
            Decimal li_paytype, li_status = 0, li_contstatus, li_intcontinttype, li_intsteptype;
            Decimal li_checkcontclr = 0, li_periodamt, li_contlaw, li_paystatus, li_clearinsure, li_countpayflag = 0;
            Decimal ldc_appvamt, ldc_balance = 0, ldc_withdrawable, ldc_rkeepprin, ldc_rkeepint, ldc_transbal;
            Decimal ldc_intarrear, ldc_payment, ldc_intestim;
            Decimal ldc_minpay = 0, ldc_intrate, ldc_intcontintrate, ldc_intincrease;
            DateTime ldtm_lastcalint, ldtm_lastproc, ldtm_approve, ldtm_startcont;

            String sqlStr1 = @"SELECT LNLOANTYPECLR.LOANTYPE_CODE,   
                                 LNLOANTYPECLR.LOANTYPE_CLEAR,   
                                 LNLOANTYPECLR.MINPERIOD_PAY,   
                                 LNLOANTYPECLR.MINPERCENT_PAY,   
                                 LNLOANTYPECLR.CHKCONTCREDIT_FLAG,   
                                 LNLOANTYPE.LOANRIGHT_TYPE,   
                                 LNLOANTYPECLR.CONTRACT_STATUS  
                            FROM LNLOANTYPECLR,   
                                 LNLOANTYPE  
                           WHERE ( LNLOANTYPECLR.LOANTYPE_CLEAR = LNLOANTYPE.LOANTYPE_CODE ) and  
                                 ( LNLOANTYPECLR.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                                 ( ( LNLOANTYPECLR.COOP_ID = '" + ls_memcoopid + "' ) )    ";

            Sdt dt1 = WebUtil.QuerySdt(sqlStr1);
            if (dt1.Next())
            {
                for (int i = 0; i < dt1.GetRowCount(); i++)
                {
                    li_minperiod = Convert.ToDecimal(dt1.Rows[i]["minperiod_pay"]);
                    ldc_minpay = Convert.ToDecimal(dt1.Rows[i]["minpercent_pay"]);
                    li_checkcontclr = Convert.ToDecimal(dt1.Rows[i]["contract_status"]);
                }
            }

            String sqlStr = @"   SELECT LNCONTMASTER.LOANCONTRACT_NO,   
                                             LNCONTMASTER.MEMBER_NO,   
                                             LNCONTMASTER.LOANTYPE_CODE,   
                                             LNCONTMASTER.LOANAPPROVE_AMT,   
                                             LNCONTMASTER.WITHDRAWABLE_AMT,   
                                             LNCONTMASTER.PRINCIPAL_BALANCE,   
                                             LNCONTMASTER.LAST_PERIODPAY,   
                                             LNCONTMASTER.LASTCALINT_DATE,   
                                             LNCONTMASTER.LASTPROCESS_DATE,   
                                             LNCONTMASTER.INTEREST_ARREAR,   
                                             LNCONTMASTER.RKEEP_PRINCIPAL,   
                                             LNCONTMASTER.RKEEP_INTEREST,   
                                             LNLOANTYPE.PREFIX,   
                                             LNCONTMASTER.LOANPAYMENT_TYPE,   
                                             LNCONTMASTER.PERIOD_PAYMENT,   
                                             LNLOANTYPE.LOANPERMGRP_CODE,   
                                             LNCONTMASTER.CONTRACT_STATUS,   
                                             LNCONTMASTER.CONTRACTINT_TYPE,   
                                             LNCONTMASTER.CONTRACT_INTEREST,   
                                             LNCONTMASTER.LOANAPPROVE_DATE,   
                                             LNCONTMASTER.STARTCONT_DATE,   
                                             LNCONTMASTER.INT_CONTINTTYPE,   
                                             LNCONTMASTER.INT_CONTINTRATE,   
                                             LNCONTMASTER.INT_CONTINTTABCODE,   
                                             LNCONTMASTER.INT_CONTINTINCREASE,   
                                             LNCONTMASTER.INT_INTSTEPTYPE,   
                                             LNCONTMASTER.PERIOD_PAYAMT,   
                                             LNCONTMASTER.CONTLAW_STATUS,   
                                             LNCONTMASTER.PRINCIPAL_TRANSBAL,   
                                             LNCONTMASTER.PAYMENT_STATUS,   
                                             LNLOANTYPE.CLEARINSURE_FLAG,   
                                             LNCONTMASTER.INSURECOLL_FLAG  
                                        FROM LNCONTMASTER,   
                                             LNLOANTYPE  
                                       WHERE ( LNCONTMASTER.LOANTYPE_CODE = LNLOANTYPE.LOANTYPE_CODE ) and  
                                             ( LNCONTMASTER.COOP_ID = LNLOANTYPE.COOP_ID ) and  
                                             ( ( lncontmaster.member_no = '" + member_no + @"' ) AND  
                                             ( lncontmaster.principal_balance + lncontmaster.withdrawable_amt > 0 ) AND  
                                             ( lncontmaster.contract_status > 0 ) AND  
                                             ( LNCONTMASTER.MEMCOOP_ID = '" + ls_memcoopid + @"' ) )   
                                    ORDER BY LNCONTMASTER.LOANCONTRACT_NO ASC  ";
            Sdt dt = WebUtil.QuerySdt(sqlStr);
            if (dt.Next())
            {
                dw_clear.Reset();

                int rowCount = dt.GetRowCount();
                for (int i = 0; i < rowCount; i++)
                {
                    try { ls_contno = dt.Rows[i]["loancontract_no"].ToString(); }
                    catch { ls_contno = ""; }
                    try
                    {
                        ls_conttype = dt.Rows[i]["loantype_code"].ToString();
                    }
                    catch { ls_conttype = ""; }
                    try
                    {
                        ls_prefix = dt.Rows[i]["prefix"].ToString();
                    }
                    catch { ls_prefix = ""; }
                    try
                    {// คำนำหน้าประเภทสัญญา
                        ls_permgrp = dt.Rows[i]["loanpermgrp_code"].ToString();
                    }
                    catch { ls_permgrp = ""; }
                    try
                    {// กลุ่มวงเงินกู้
                        li_paytype = Convert.ToDecimal(dt.Rows[i]["loanpayment_type"]);
                    }
                    catch { li_paytype = 0; }
                    try
                    {
                        li_period = Convert.ToDecimal(dt.Rows[i]["last_periodpay"]);
                    }
                    catch { li_period = 0; }
                    try
                    {
                        li_contstatus = Convert.ToDecimal(dt.Rows[i]["contract_status"]);
                    }
                    catch { li_contstatus = 0; }
                    try
                    {
                        li_continttype = Convert.ToDecimal(dt.Rows[i]["contractint_type"]);

                    }
                    catch { li_continttype = 0; }
                    try
                    {
                        ldc_intrate = Convert.ToDecimal(dt.Rows[i]["contract_interest"]);
                    }
                    catch { ldc_intrate = 0; }
                    try
                    {
                        ldc_payment = Convert.ToDecimal(dt.Rows[i]["period_payment"]);
                    }
                    catch { ldc_payment = 0; }
                    try
                    {
                        ldc_appvamt = Convert.ToDecimal(dt.Rows[i]["loanapprove_amt"]);
                    }
                    catch { ldc_appvamt = 0; }
                    try
                    {
                        ldc_withdrawable = Convert.ToDecimal(dt.Rows[i]["withdrawable_amt"]);
                    }
                    catch { ldc_withdrawable = 0; }
                    try
                    {
                        ldc_balance = Convert.ToDecimal(dt.Rows[i]["principal_balance"]);
                    }
                    catch { ldc_balance = 0; }
                    try
                    {
                        ldc_intarrear = Convert.ToDecimal(dt.Rows[i]["interest_arrear"]);
                    }
                    catch { ldc_intarrear = 0; }
                    try
                    {
                        ldc_rkeepprin = Convert.ToDecimal(dt.Rows[i]["rkeep_principal"]);
                    }
                    catch { ldc_rkeepprin = 0; }
                    try
                    {
                        ldc_rkeepint = Convert.ToDecimal(dt.Rows[i]["rkeep_interest"]);
                    }
                    catch { ldc_rkeepint = 0; }
                    try
                    {
                        ldtm_lastcalint = Convert.ToDateTime(dt.Rows[i]["lastcalint_date"]);
                    }
                    catch { ldtm_lastcalint = DateTime.Now; }
                    try
                    {
                        ldtm_lastproc = Convert.ToDateTime(dt.Rows[i]["lastprocess_date"]);
                    }
                    catch { ldtm_lastproc = DateTime.Now; }
                    try
                    {
                        ldtm_approve = Convert.ToDateTime(dt.Rows[i]["loanapprove_date"]);
                    }
                    catch { ldtm_approve = DateTime.Now; }
                    try
                    {
                        ldtm_startcont = Convert.ToDateTime(dt.Rows[i]["startcont_date"]);
                    }
                    catch { ldtm_startcont = DateTime.Now; }
                    try
                    {
                        li_intcontinttype = Convert.ToDecimal(dt.Rows[i]["int_continttype"]);
                    }
                    catch { li_intcontinttype = 0; }
                    try
                    {
                        ldc_intcontintrate = Convert.ToDecimal(dt.Rows[i]["int_contintrate"]);
                    }
                    catch { ldc_intcontintrate = 0; }
                    try
                    {
                        ls_intcontintcode = dt.Rows[i]["int_continttabcode"].ToString();
                    }
                    catch { ls_intcontintcode = ""; }
                    try
                    {
                        ldc_intincrease = Convert.ToDecimal(dt.Rows[i]["int_contintincrease"]);
                    }
                    catch { ldc_intincrease = 0; }
                    try
                    {
                        li_intsteptype = Convert.ToDecimal(dt.Rows[i]["int_intsteptype"]);
                    }
                    catch { li_intsteptype = 0; }
                    try
                    {
                        li_periodamt = Convert.ToDecimal(dt.Rows[i]["period_payamt"]);
                    }
                    catch { li_periodamt = 0; }
                    try
                    {
                        li_transfersts = Convert.ToDecimal(dt.Rows[i]["transfer_status"]);
                    }
                    catch { li_transfersts = 0; }
                    try
                    {
                        ls_coopid = dt.Rows[i]["coop_id"].ToString();
                    }
                    catch { ls_coopid = ls_memcoopid; }
                    try
                    {

                        li_contlaw = Convert.ToDecimal(dt.Rows[i]["contlaw_status"]);
                    }
                    catch { li_contlaw = 0; }
                    try
                    {
                        ldc_transbal = Convert.ToDecimal(dt.Rows[i]["principal_transbal"]);
                    }
                    catch { ldc_transbal = 0; }
                    try
                    {
                        li_paystatus = Convert.ToDecimal(dt.Rows[i]["payment_status"]);
                    }
                    catch { li_paystatus = 0; }
                    try
                    {
                        li_clearinsure = Convert.ToDecimal(dt.Rows[i]["insurecoll_flag"]);
                    }
                    catch { li_clearinsure = 0; }
                    try
                    {
                        ldc_intcontintrate = dw_main.GetItemDecimal(1, "int_contintrate");
                        String ls_loantype = dw_main.GetItemString(1, "loantype_code");
                        DateTime processdate = new DateTime(); int day_amount = 0;
                        //a hardcode
                        String sqlpro = " SELECT MAX(LASTPROCESS_DATE)as LASTPROCESS_DATE FROM  LNCONTMASTER ";
                        Sdt dtpro = WebUtil.QuerySdt(sqlpro);
                        if (dtpro.Next()) { processdate = dtpro.GetDate("LASTPROCESS_DATE"); }
                        short year = Convert.ToInt16(loanrcvfix_date.Year + 543);
                        short month_bf = Convert.ToInt16(loanrcvfix_date.Month - 1);
                        short month_af = Convert.ToInt16(loanrcvfix_date.Month);
                        DateTime postingdate_bf = wcf.Busscom.of_getpostingdate2(state.SsWsPass, year, month_bf);
                        DateTime postingdate_af = wcf.Busscom.of_getpostingdate2(state.SsWsPass, year, month_af);
                        if (postingdate_bf < postingdate_af)
                        {

                            day_amount = postingdate_af.DayOfYear - postingdate_bf.DayOfYear;

                        }
                        if (ls_conttype != ls_loantype)
                        {
                            ldc_intestim = wcf.Shrlon.of_roundmoney(state.SsWsPass, ldc_balance * (ldc_intcontintrate) * day_amount / 365);

                        }
                        else
                        {

                            ldc_intestim = wcf.Shrlon.of_roundmoney(state.SsWsPass, (wcf.Shrlon.of_computeinterest(state.SsWsPass, ls_memcoopid, ls_contno, loanrcvfix_date)));
                        }

                    }
                    catch { ldc_intestim = 0; }


                    if (ls_conttype == dw_main.GetItemString(1, "loantype_code") || ls_conttype == "10" || ls_conttype == "11" || ls_conttype == "12")
                    {
                        li_status = 1;//default การชำระหนี้เก่า

                    }
                    else { li_status = 0; }
                    try
                    {
                        dw_clear.InsertRow(i + 1);
                        dw_clear.SetItemString(i + 1, "loancontract_no", ls_contno);
                        dw_clear.SetItemString(i + 1, "coop_id", state.SsCoopId);
                        dw_clear.SetItemString(i + 1, "concoop_id", state.SsCoopControl);
                        dw_clear.SetItemString(i + 1, "loantype_code", ls_conttype);
                        dw_clear.SetItemString(i + 1, "prefix", ls_prefix);
                        dw_clear.SetItemDecimal(i + 1, "loanpayment_type", li_paytype);
                        dw_clear.SetItemDecimal(i + 1, "period_payment", ldc_payment);
                        dw_clear.SetItemDecimal(i + 1, "loanapprove_amt", ldc_appvamt);
                        dw_clear.SetItemDecimal(i + 1, "withdrawable_amt", ldc_withdrawable);
                        dw_clear.SetItemDecimal(i + 1, "principal_balance", ldc_balance);
                        dw_clear.SetItemDecimal(i + 1, "last_periodpay", li_period);
                        dw_clear.SetItemDecimal(i + 1, "minperiod_pay", li_minperiod);
                        dw_clear.SetItemDecimal(i + 1, "minpercent_pay", ldc_minpay);
                        dw_clear.SetItemDateTime(i + 1, "lastcalint_date", ldtm_lastcalint);
                        dw_clear.SetItemDecimal(i + 1, "contract_status", li_contstatus);
                        dw_clear.SetItemString(i + 1, "permissgroup_code", ls_permgrp);
                        dw_clear.SetItemDecimal(i + 1, "clear_status", li_status);
                        dw_clear.SetItemDateTime(i + 1, "lastprocess_date", ldtm_lastproc);
                        dw_clear.SetItemDecimal(i + 1, "contractint_type", li_continttype);
                        dw_clear.SetItemDecimal(i + 1, "contract_interest", ldc_intrate);
                        dw_clear.SetItemDecimal(i + 1, "rkeep_principal", ldc_rkeepprin);
                        dw_clear.SetItemDecimal(i + 1, "rkeep_interest", ldc_rkeepint);
                        dw_clear.SetItemDecimal(i + 1, "interest_arrear", ldc_intarrear);
                        if (li_paytype == 1)
                        {
                            dw_clear.SetItemDecimal(i + 1, "intestimate_amt", Convert.ToDecimal(ldc_intestim));
                        }
                        else
                        {
                            dw_clear.SetItemDecimal(i + 1, "intestimate_amt", 0);
                        }
                        dw_clear.SetItemDateTime(i + 1, "loanapprove_date", ldtm_approve);
                        dw_clear.SetItemDateTime(i + 1, "startcont_date", ldtm_startcont);
                        dw_clear.SetItemDecimal(i + 1, "int_continttype", li_intcontinttype);
                        dw_clear.SetItemDecimal(i + 1, "int_contintrate", ldc_intcontintrate);
                        dw_clear.SetItemString(i + 1, "int_continttabcode", ls_intcontintcode);
                        dw_clear.SetItemDecimal(i + 1, "int_contintincrease", ldc_intincrease);
                        dw_clear.SetItemDecimal(i + 1, "int_intsteptype", li_intsteptype);
                        dw_clear.SetItemDecimal(i + 1, "period_payamt", li_periodamt);
                        dw_clear.SetItemDecimal(i + 1, "contlaw_status", li_contlaw);
                        dw_clear.SetItemDecimal(i + 1, "payment_status", li_paystatus);
                        dw_clear.SetItemDecimal(i + 1, "principal_transbal", ldc_transbal);
                        dw_clear.SetItemDecimal(i + 1, "insurecoll_flag", li_clearinsure);
                        dw_clear.SetItemDecimal(i + 1, "countpay_flag", li_countpayflag);
                        //  dw_clear.SetItemDecimal(i+1, "transfer_status", li_transfersts);
                        //  dw_clear.SetItemDecimal(i+1, "prnclear_amt", ldc_balance);
                        if (ls_conttype == dw_main.GetItemString(1, "loantype_code") || ls_conttype == "10" || ls_conttype == "11" || ls_conttype == "12")
                        {
                            JsSumOthClr();
                        }
                        //เช็คสิทธิ์การกู้จากสามัญว่าส่งมาแล้วอย่างน้อย 6 งวด แล้วแต่จำนวนไหนจะน้อยกว่ากัน
                        Decimal loancredit_amt = 0;
                        Decimal ldc_shrstkvalue = dw_main.GetItemDecimal(1, "sharestk_value");
                        Decimal ldc_salary = dw_main.GetItemDecimal(1, "salary_amt");

                        if (ls_conttype == "20" || ls_conttype == "21" || ls_conttype == "22" || ls_conttype == "23" || ls_conttype == "24" || ls_conttype == "26")
                        {

                            period_payamt = dw_clear.GetItemDecimal(i + 1, "last_periodpay");
                            //ถ้าเป็นกลุ่ม เกษียณบำนาจ
                            if (loangroup_code == "03")
                            {
                                if (period_payamt < 6)
                                {
                                    period_payment = dw_clear.GetItemDecimal(i + 1, "period_payment");
                                    ldc_appvamt = dw_clear.GetItemDecimal(i + 1, "loanapprove_amt");
                                    ldc_balance = dw_clear.GetItemDecimal(i + 1, "principal_balance");
                                }
                                else
                                {
                                    ldc_appvamt = dw_clear.GetItemDecimal(i + 1, "loanapprove_amt");
                                    ldc_balance = dw_clear.GetItemDecimal(i + 1, "principal_balance");
                                    loancredit = ldc_appvamt - ldc_balance;
                                }

                                loancredit_amt = dw_main.GetItemDecimal(1, "loancredit_amt");
                                //   if (loancredit <= 0) { dw_main.SetItemDecimal(1, "loancredit_amt", loancredit_amt); }
                                if (loancredit_amt > loancredit && loancredit >= 0)
                                {
                                    dw_main.SetItemDecimal(1, "loancredit_amt", loancredit);
                                }
                                else
                                {
                                    period_payment = dw_clear.GetItemDecimal(i + 1, "period_payment");
                                    loancredit = period_payment * period_payamt;
                                    if (loancredit_amt < loancredit) { loancredit = loancredit_amt; }
                                    else
                                    {
                                        dw_main.SetItemDecimal(1, "loancredit_amt", loancredit);
                                    }
                                }
                            }
                            else
                            {
                                if (period_payamt < 3)
                                {
                                    period_payment = dw_clear.GetItemDecimal(i + 1, "period_payment");
                                    ldc_appvamt = dw_clear.GetItemDecimal(i + 1, "loanapprove_amt");
                                    ldc_balance = dw_clear.GetItemDecimal(i + 1, "principal_balance");
                                }
                                else
                                {
                                    ldc_appvamt = dw_clear.GetItemDecimal(i + 1, "loanapprove_amt");
                                    ldc_balance = dw_clear.GetItemDecimal(i + 1, "principal_balance");
                                    loancredit = ldc_appvamt - ldc_balance;
                                }

                                loancredit_amt = dw_main.GetItemDecimal(1, "loancredit_amt");
                                //   if (loancredit <= 0) { dw_main.SetItemDecimal(1, "loancredit_amt", loancredit_amt); }
                                if (loancredit_amt > loancredit && loancredit >= 0)
                                {
                                    dw_main.SetItemDecimal(1, "loancredit_amt", loancredit);
                                }
                                else
                                {
                                    period_payment = dw_clear.GetItemDecimal(i + 1, "period_payment");
                                    loancredit = period_payment * period_payamt;
                                    if (loancredit_amt < loancredit) { loancredit = loancredit_amt; }
                                    else
                                    {
                                        dw_main.SetItemDecimal(1, "loancredit_amt", loancredit);
                                    }
                                }

                            }
                        }
                        ////เช็คสิทธิ์การกู้จากพิเศษว่าส่งมาแล้วอย่างน้อย 6 งวด แล้วแต่จำนวนไหนจะน้อยกว่ากัน
                        else if (ls_conttype == "30" || ls_conttype == "31" || ls_conttype == "32" || ls_conttype == "33")
                        {
                            period_payamt = dw_clear.GetItemDecimal(i + 1, "last_periodpay");
                            //ถ้าเป็นกลุ่ม เกษียณบำนาจ
                            //if (loangroup_code == "03")
                            //{

                            if (period_payamt < 6)
                            {
                                period_payment = dw_clear.GetItemDecimal(i + 1, "period_payment");
                                ldc_appvamt = dw_clear.GetItemDecimal(i + 1, "loanapprove_amt");
                                ldc_balance = dw_clear.GetItemDecimal(i + 1, "principal_balance");
                            }
                            else
                            {
                                ldc_appvamt = dw_clear.GetItemDecimal(i + 1, "loanapprove_amt");
                                ldc_balance = dw_clear.GetItemDecimal(i + 1, "principal_balance");
                                loancredit = ldc_appvamt - ldc_balance;
                            }

                            loancredit_amt = dw_main.GetItemDecimal(1, "loancredit_amt");
                            // if (loancredit <= 0) { dw_main.SetItemDecimal(1, "loancredit_amt", loancredit_amt); }
                            if (loancredit_amt > loancredit && loancredit >= 0)
                            {
                                dw_main.SetItemDecimal(1, "loancredit_amt", loancredit);
                            }
                            else
                            {
                                period_payment = dw_clear.GetItemDecimal(i + 1, "period_payment");
                                loancredit = period_payment * period_payamt;
                                if (loancredit_amt < loancredit) { loancredit = loancredit_amt; }
                                else
                                {
                                    dw_main.SetItemDecimal(1, "loancredit_amt", loancredit);
                                }
                            }

                            //}
                            //    else
                            //    {
                            //        if (period_payamt < 0)
                            //        {
                            //            period_payment = dw_clear.GetItemDecimal(i + 1, "period_payment");
                            //            ldc_appvamt = dw_clear.GetItemDecimal(i + 1, "loanapprove_amt");
                            //            ldc_balance = dw_clear.GetItemDecimal(i + 1, "principal_balance");
                            //        }
                            //        else
                            //        {
                            //            loancredit = ldc_appvamt - ldc_balance;
                            //        }

                            //        loancredit_amt = dw_main.GetItemDecimal(1, "loancredit_amt");
                            //        // if (loancredit <= 0) { dw_main.SetItemDecimal(1, "loancredit_amt", loancredit_amt); }
                            //        if (loancredit_amt > loancredit && loancredit >= 0)
                            //        {
                            //            dw_main.SetItemDecimal(1, "loancredit_amt", loancredit);
                            //        }
                            //    }
                        }
                        else { li_status = 0; }
                    }
                    catch { }
                }
            }

        }

        /// <summary>
        ///ยอดขอกู้==> หายอดชำระ
        /// </summary>
        private void JsSetpriod()
        {
            string ls_loantype = dw_main.GetItemString(1, "loantype_code");
            Decimal loanrequest_amt = dw_main.GetItemDecimal(1, "loanrequest_amt");
            Decimal period_payamt = dw_main.GetItemDecimal(1, "period_payamt");


            Decimal period_payment = 0;

            try
            {
                // ปัดยอดขอกู้
                String ll_roundloan = wcf.Busscom.of_getattribloantype(state.SsWsPass, ls_loantype, "reqround_factor");
                int roundloan = Convert.ToInt16(ll_roundloan);
                if (roundloan > 0)
                {
                    if (loanrequest_amt > 0)
                    {
                        loanrequest_amt = Math.Round((loanrequest_amt) / roundloan) * roundloan;
                    }
                    else
                    {
                        loanrequest_amt = 0;

                    }
                }
                dw_main.SetItemDecimal(1, "loanrequest_amt", loanrequest_amt);

                // ปัดยอดชำระ
                String ll_roundpay = wcf.Busscom.of_getattribloantype(state.SsWsPass, ls_loantype, "payround_factor");
                int roundpay = Convert.ToInt16(ll_roundpay);
                if (roundpay > 0)
                {
                    if (period_payamt > 0)
                    {
                        period_payment = loanrequest_amt / period_payamt;

                    }
                    else
                    {
                        period_payment = 0;
                    }
                }
                dw_main.SetItemDecimal(1, "period_payment", period_payment);

                Decimal intestimate_amt = of_calintestimatemain();
                dw_main.SetItemDecimal(1, "intestimate_amt", intestimate_amt);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
            HdIsPostBack.Value = "false";
        }

        /// <summary>
        /// ยอดชำระ==> หาจน.งวด 
        /// </summary>
        private void JsRevert()
        {
            string ls_loantype = dw_main.GetItemString(1, "loantype_code");
            Decimal period_payamt = dw_main.GetItemDecimal(1, "period_payamt");
            Decimal loanrequest_amt = dw_main.GetItemDecimal(1, "loanrequest_amt");
            Decimal period_payment = dw_main.GetItemDecimal(1, "period_payment");
            Decimal maxsend_payamt = dw_main.GetItemDecimal(1, "maxsend_payamt");
            // ปัดยอดชำระ
            //String ll_roundpay = wcf.Busscom.of_getattribloantype(state.SsWsPass, ls_loantype, "payround_factor");
            //int roundpay = Convert.ToInt16(ll_roundpay);
            //if (roundpay > 0)
            //{
            //    if (period_payment > 0)
            //    {
            //        period_payment = Math.Round(period_payment / roundpay) * roundpay;

            //    }
            //    else
            //    {
            //        period_payment = 0;

            //    }
            //}

            if (loanrequest_amt == 0 && period_payamt > 0)
            {
                loanrequest_amt = period_payamt * period_payment;
                // ปัดยอดขอกู้
                String ll_roundloan = wcf.Busscom.of_getattribloantype(state.SsWsPass, ls_loantype, "reqround_factor");
                int roundloan = Convert.ToInt16(ll_roundloan);
                if (roundloan > 0)
                {
                    if (loanrequest_amt > 0)
                    {
                        loanrequest_amt = Math.Round((loanrequest_amt) / roundloan) * roundloan;
                    }
                    else
                    {
                        loanrequest_amt = 0;

                    }
                }
                dw_main.SetItemDecimal(1, "loanrequest_amt", loanrequest_amt);

            }
            else
            {
                period_payamt = loanrequest_amt / period_payment;
                if (period_payamt > maxsend_payamt)
                {
                    period_payamt = maxsend_payamt;
                    period_payment = loanrequest_amt / period_payamt;
                    //  period_payment = Math.Round(period_payment / roundpay) * roundpay;
                }
                dw_main.SetItemDecimal(1, "period_payment", period_payment);

                dw_main.SetItemDouble(1, "period_payamt", Convert.ToDouble(period_payamt));
            }
            HdIsPostBack.Value = "false";
        }

        /// <summary>
        /// จน.งวด==> หายอดชำระ
        ///</summary>         
        private void JsContPeriod()
        {
            string ls_loantype = dw_main.GetItemString(1, "loantype_code");
            Decimal period_payamt = dw_main.GetItemDecimal(1, "period_payamt");
            Decimal loanrequest_amt = dw_main.GetItemDecimal(1, "loanrequest_amt");
            Decimal period_payment = 0;

            // ปัดยอดชำระ
            String ll_roundpay = wcf.Busscom.of_getattribloantype(state.SsWsPass, ls_loantype, "payround_factor");
            int roundpay = Convert.ToInt16(ll_roundpay);
            if (roundpay > 0)
            {
                if (period_payamt > 0)
                {
                    period_payment = loanrequest_amt / period_payamt;
                    //period_payment = Math.Round((loanrequest_amt / period_payamt) / roundpay) * roundpay;
                    //if ((loanrequest_amt / period_payamt) > period_payment) { period_payment = period_payment + roundpay; }
                }
                else
                {
                    period_payment = 0;
                }

            }
            dw_main.SetItemDecimal(1, "period_payment", period_payment);
            HdIsPostBack.Value = "false";
        }

        /// <summary>
        /// ยกเลิก ใบคำขอกู้
        /// </summary>    
        private void JsCancelRequest()
        {
            String dwXmlMain = dw_main.Describe("DataWindow.Data.XML");
            String dwXmlMessage = "";
            String cancelID = state.SsUsername;
            String coop_id = state.SsCoopId;
            try
            {
                int result = shrlonService.CancelRequest(state.SsWsPass, ref dwXmlMain, ref dwXmlMessage, cancelID, coop_id);
                if (result == 1)
                {
                    //นำเข้าข้อมูลหลัก
                    dw_main.Reset();
                    dw_main.ImportString(dwXmlMain, FileSaveAsType.Xml);
                    if (dw_main.RowCount > 1) dw_main.DeleteRow(dw_main.RowCount);

                    SaveWebSheet();
                    LtServerMessage.Text = WebUtil.WarningMessage("ได้ทำการยกเลิกใบคำขอกู้เงินเรียบร้อยแล้ว");
                }
                else
                {
                    if ((dwXmlMessage != "") && (dwXmlMessage != null))
                    {
                        dw_message.Reset();
                        dw_message.ImportString(dwXmlMessage, FileSaveAsType.Xml);
                        string msgtext = dw_message.GetItemString(1, "msgtext");

                        LtServerMessage.Text = WebUtil.WarningMessage(msgtext);
                    }
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }

        }

        /// <summary>
        /// ค้ำประกัน
        /// </summary>
        private void JsCollCondition()
        {
            String dwXmlMain = dw_main.Describe("DataWindow.Data.XML");
            String dwXmlColl = dw_coll.Describe("DataWindow.Data.XML");
            String dwXmlMessage = "";

            dwXmlColl = shrlonService.CollPercCondition(state.SsWsPass, dwXmlMain, dwXmlColl, ref dwXmlMessage);

            try
            {
                if ((dwXmlColl != "") && (dwXmlColl != null))
                {
                    dw_coll.Reset();
                    dw_coll.ImportString(dwXmlColl, FileSaveAsType.Xml);
                }

            }
            catch
            {
                dw_coll.Reset();
                //dw_coll.InsertRow(0); 
            }


            if ((dwXmlMessage != "") && (dwXmlMessage != null))
            {
                dw_message.Reset();
                dw_message.ImportString(dwXmlMessage, FileSaveAsType.Xml);
                string msgtext = dw_message.GetItemString(1, "msgtext");
                LtServerMessage.Text = WebUtil.WarningMessage(msgtext);
            }

        }

        private void JsCollInitP()
        {
            String dwXmlMain = dw_main.Describe("DataWindow.Data.XML");
            String dwXmlColl = dw_coll.Describe("DataWindow.Data.XML");

            dwXmlColl = shrlonService.CollInitPrecent(state.SsWsPass, dwXmlMain, dwXmlColl);

            try
            {
                if ((dwXmlColl != "") && (dwXmlColl != null))
                {
                    dw_coll.Reset();
                    dw_coll.ImportString(dwXmlColl, FileSaveAsType.Xml);
                }

            }
            catch
            {
                dw_coll.Reset();
                //dw_coll.InsertRow(0); 
            }
        }

        private void JsSetDataList()
        {
            str_itemchange strList = new str_itemchange();
            strList = WebUtil.str_itemchange_session(this);
            if (dw_clear.RowCount == 0)
            { strList.xml_clear = null; }
            else { strList.xml_clear = dw_clear.Describe("DataWindow.Data.XML"); }
            if (dw_coll.RowCount == 0) { strList.xml_guarantee = null; }
            else { strList.xml_guarantee = dw_coll.Describe("DataWindow.Data.XML"); }

            Session["strItemchange"] = strList;
        }

        private void JsPostColl()
        {
            //try
            //{

            //    String columnName = "ref_collno";
            //    if (HdColumnName.Value == "" || HdColumnName.Value == "setcolldetail") { columnName = "setcolldetail"; }
            //    String dwMainXML = dw_main.Describe("DataWindow.Data.XML");
            //    String dwCollXML = dw_coll.Describe("DataWindow.Data.XML");
            //    String dwClearXML = dw_clear.Describe("DataWindow.Data.XML");
            //    String t = dw_main.GetItemString(1, "loantype_code");
            //    str_itemchange strList = new str_itemchange();

            //    // ตรวจสอบว่า HdRowNumber มีค่าหรือไม่ ถ้าไม่มี ไม่ต้องทำอะไร
            //    if (HdRowNumber.Value.ToString() != "")
            //    {
            //        int rowNumber = Convert.ToInt32(HdRowNumber.Value.ToString());

            //        HdColumnName.Value = "";
            //        if ((columnName == "checkmancollperiod") || (columnName == "setcolldetail"))
            //        {
            //            dw_coll.SetItemString(rowNumber, "ref_collno", HdRefcollNO.Value);
            //            dwCollXML = dw_coll.Describe("DataWindow.Data.XML");
            //        }
            //        String refCollNo = "";
            //        try
            //        {
            //            refCollNo = dw_coll.GetItemString(rowNumber, "ref_collno");
            //            HdRefcollNO.Value = refCollNo;
            //        }
            //        catch
            //        {
            //            refCollNo = "";
            //        }

            //        strList.column_name = columnName;
            //        strList.xml_main = dwMainXML;
            //        strList.xml_guarantee = dwCollXML;
            //        strList.xml_clear = dwClearXML;
            //        strList.import_flag = true;
            //        strList.format_type = "CAT";

            //        strList.column_data = dw_coll.GetItemString(rowNumber, "loancolltype_code") + refCollNo;
            //        // เรียก service สำหรับการเปลี่ยนแปลงค่า ของ DW_coll
            //        int result = shrlonService.LoanRightItemChangeColl(state.SsWsPass, ref strList);
            //        Session["strItemchange"] = strList;
            //        //if ((strList.xml_message != null) && (strList.xml_message != ""))
            //        //{
            //        //    //dw_message.Reset();MO
            //        //    //dw_message.ImportString(strList.xml_message, FileSaveAsType.Xml);
            //        //    DwUtil.ImportData(strList.xml_message, dw_message, null, FileSaveAsType.Xml);
            //        //    HdMsg.Value = dw_message.GetItemString(1, "msgtext");
            //        //    HdMsgWarning.Value = dw_message.GetItemString(1, "msgicon");
            //        //}
            //        if (result == 8)
            //        {
            //            HdReturn.Value = result.ToString();
            //            HdColumnName.Value = strList.column_name;
            //        }
            //        //try
            //        //{
            //        //    dw_otherclr.Reset();
            //        //    dw_otherclr.ImportString(LtXmlOtherlr.Text, FileSaveAsType.Xml);
            //        //}
            //        //catch { dw_otherclr.Reset(); }
            //        try
            //        {
            //            dw_coll.Reset();
            //            dw_coll.ImportString(strList.xml_guarantee, FileSaveAsType.Xml);
            //        }
            //        catch { dw_coll.Reset(); }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            //}

        }

        /// <summary>
        /// sum ชำระหนี้อืนทั้งหมด
        /// </summary>       
        private void JsSumOthClr()
        {


            DateTime loanrcvfix_date = dw_main.GetItemDate(1, "loanrcvfix_date");
            String ls_memcoopid;
            Decimal ldc_contintrate = dw_main.GetItemDecimal(1, "int_contintrate");

            short year = Convert.ToInt16(loanrcvfix_date.Year + 543);
            short month = Convert.ToInt16(loanrcvfix_date.Month);
            int day_amount = 31;

            //  DateTime postingdate_bf = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(loanrcvfix_date.Year + 543), Convert.ToInt16(loanrcvfix_date.Month));
            // DateTime postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(postingdate_bf.Year + 543), Convert.ToInt16(postingdate_bf.Month + 1));
            //DateTime postingdate_af = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(postingdate.Year + 543), Convert.ToInt16(postingdate.Month + 1));
            DateTime postingdate_bf = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(loanrcvfix_date.Year + 543), Convert.ToInt16(loanrcvfix_date.Month - 1));
            DateTime postingdate = wcf.Busscom.of_getpostingdate(state.SsWsPass, loanrcvfix_date);
            DateTime postingdate_af = wcf.Busscom.of_getpostingdate(state.SsWsPass, postingdate.AddMonths(1));

            //จ่ายเงินกู้หลังเรียกเก็บหรือไม่
            if (loanrcvfix_date > postingdate && loanrcvfix_date < postingdate_bf)
            {

                day_amount = loanrcvfix_date.DayOfYear - postingdate_bf.DayOfYear;

            }
            else if (loanrcvfix_date > postingdate_bf && loanrcvfix_date < postingdate)
            {

                //    postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(postingdate_af.Year + 543), Convert.ToInt16(postingdate_af.Month));

                day_amount = loanrcvfix_date.DayOfYear - postingdate_bf.DayOfYear;

            }
            else
            {
                postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(loanrcvfix_date.Year + 543), Convert.ToInt16(loanrcvfix_date.Month - 1));

                day_amount = loanrcvfix_date.DayOfYear - postingdate.DayOfYear;
            }

            try { ls_memcoopid = dw_main.GetItemString(1, "memcoop_id"); }
            catch { ls_memcoopid = state.SsCoopControl; }
            Decimal clrother_amt = 0;
            Decimal principal_balance = 0, intestimate_amt = 0;
            Decimal sum_clear1 = 0;
            Decimal otherclr_amt = 0;

            Decimal clear_status;
            int i, j;
            int row_clr = dw_otherclr.RowCount;
            int row_clear = dw_clear.RowCount;
            if (row_clr > 0)
            {
                for (i = 0; i < row_clr; i++)
                {
                    try
                    {
                        clrother_amt = dw_otherclr.GetItemDecimal(i + 1, "clrother_amt");
                    }
                    catch { clrother_amt = 0; }
                    otherclr_amt = otherclr_amt + clrother_amt;
                    if (otherclr_amt > 0)
                    {
                        dw_main.SetItemDecimal(1, "otherclr_flag", 1);
                        dw_main.SetItemDecimal(1, "otherclr_amt", otherclr_amt);
                    }
                    else if (clrother_amt == 0)
                    {
                        dw_main.SetItemDecimal(1, "otherclr_flag", 0);
                        dw_main.SetItemDecimal(1, "otherclr_amt", 0);
                    }
                }
            }
            if (row_clear > 0)
            {
                for (j = 0; j < row_clear; j++)
                {
                    Decimal loanpayment_type = dw_clear.GetItemDecimal(j + 1, "loanpayment_type");
                    string ls_contno = dw_clear.GetItemString(j + 1, "loancontract_no");
                    try
                    {
                        clear_status = dw_clear.GetItemDecimal(j + 1, "clear_status");
                    }
                    catch { clear_status = 0; }
                    if (clear_status == 1)
                    {

                        if (Hdprincipal.Value == null || Hdprincipal.Value == "")
                        {

                            try
                            {
                                principal_balance = dw_clear.GetItemDecimal(j + 1, "principal_balance");
                            }
                            catch { principal_balance = 0; }
                            try
                            {

                                if (loanpayment_type == 1)
                                {
                                    intestimate_amt = dw_clear.GetItemDecimal(j + 1, "intestimate_amt");
                                }
                                else { intestimate_amt = 0; }
                            }
                            catch { intestimate_amt = 0; }
                        }
                        else
                        {

                            try
                            {
                                principal_balance = dw_clear.GetItemDecimal(j + 1, "principal_balance");
                            }
                            catch { principal_balance = 0; }
                            try
                            {
                                if (loanpayment_type == 1)
                                {
                                    intestimate_amt = wcf.Shrlon.of_roundmoney(state.SsWsPass, (wcf.Shrlon.of_computeinterest(state.SsWsPass, ls_memcoopid, ls_contno, loanrcvfix_date)));
                                }
                                else { intestimate_amt = 0; }
                                dw_clear.SetItemDecimal(j + 1, "intestimate_amt", intestimate_amt);
                            }
                            catch { intestimate_amt = 0; }
                        }


                        sum_clear1 = sum_clear1 + principal_balance + intestimate_amt;

                    }


                    else if (clear_status == 0)
                    {
                        if (loanpayment_type == 1)
                        {
                            day_amount = postingdate.DayOfYear - postingdate_bf.DayOfYear;
                            principal_balance = dw_clear.GetItemDecimal(j + 1, "principal_balance");
                            intestimate_amt = principal_balance * (ldc_contintrate) * day_amount / 365;
                            intestimate_amt = wcf.Shrlon.of_roundmoney(state.SsWsPass, intestimate_amt);
                            //  intestimate_amt = wcf.Shrlon.of_roundmoney(state.SsWsPass, (wcf.Shrlon.of_computeinterest(state.SsWsPass, ls_memcoopid, ls_contno, loanrcvfix_date)));
                        }
                        else { intestimate_amt = 0; }
                        dw_clear.SetItemDecimal(j + 1, "intestimate_amt", intestimate_amt);

                    }
                }

            }
            Decimal total = sum_clear1;
            dw_main.SetItemDecimal(1, "sum_clear", total);
            if (total > 0)
            {
                dw_main.SetItemDecimal(1, "clearloan_flag", 1);
            }
            else { dw_main.SetItemDecimal(1, "clearloan_flag", 0); }
        }
        /// <summary>
        /// sum ชำระหนี้เก่าทั้งหมด
        /// </summary>  
        private void JsResumLoanClear()
        {
            //Session["xmlloandetail"];
            String xmlMain = "";
            String xmlColl = "";
            String xmlClear = "";
            String xmlLoanDetail = "";
            String xmlMessage = "";

            xmlMain = dw_main.Describe("DataWindow.Data.XML");

            xmlLoanDetail = Session["xmlloandetail"].ToString();
            if (dw_coll.RowCount == 0)
            { xmlColl = null; }
            else { xmlColl = dw_coll.Describe("DataWindow.Data.XML"); }

            if (dw_clear.RowCount == 0)
            { xmlClear = null; }
            else { xmlClear = dw_clear.Describe("DataWindow.Data.XML"); }

            int result = shrlonService.ResumLoanClear(state.SsWsPass, ref xmlMain, ref xmlClear, ref xmlColl, xmlLoanDetail, ref xmlMessage);

            if (result == 1)
            {
                try
                {
                    //นำเข้าข้อมูลหลัก
                    dw_main.Reset();
                    dw_main.ImportString(xmlMain, FileSaveAsType.Xml);
                    //DwUtil.ImportData(xmlMain, dw_main, tDwMain, FileSaveAsType.Xml);
                    if (dw_main.RowCount > 1) dw_main.DeleteRow(dw_main.RowCount);
                }
                catch { dw_main.Reset(); dw_main.InsertRow(0); }
                try
                {
                    dw_clear.Reset();
                    dw_clear.ImportString(xmlClear, FileSaveAsType.Xml);
                }
                catch { dw_clear.Reset(); }
                try
                {
                    dw_coll.Reset();
                    dw_coll.ImportString(xmlColl, FileSaveAsType.Xml);
                }
                catch { dw_coll.Reset(); }
                LtXmlLoanDetail.Text = xmlLoanDetail;
            }
            if ((xmlMessage != null) && (xmlMessage != ""))
            {
                dw_message.Reset();
                dw_message.ImportString(xmlMessage, FileSaveAsType.Xml);
                LtServerMessage.Text = WebUtil.ErrorMessage(dw_message.GetItemString(1, "msgtext"));
            }


        }



        /// <summary>
        /// ตรวจสอบสิทธิ์ตามเงินเดือน
        /// </summary>
        protected void JsPermissSalary()
        {

            string ls_memno, ls_loantype, ls_contno, ls_desc, ls_refreqloop;
            int li_index, li_count, li_exist, li_month;
            int li_clrstatus, li_paytype, li_shrpaystatus, li_signflag;
            int ll_row;
            Decimal ldc_shrperiod, ldc_payment, ldc_intestm, ldc_sumloan, ldc_reqpayment;
            Decimal ldc_sumpay, ldc_minsalaryamt, ldc_salary, ldc_reqloopclr, ldc_suminc;
            Decimal ldc_incomemthfix, ldc_intcomeoth, ldc_paymonthoth;
            Decimal ldc_minsalaryperc;



            ls_memno = dw_main.GetItemString(1, "member_no");
            ldc_salary = dw_main.GetItemDecimal(1, "salary_amt");

            try { ldc_minsalaryperc = dw_main.GetItemDecimal(1, "minsalary_perc"); }
            catch { ldc_minsalaryperc = 0; }
            try { ldc_minsalaryamt = dw_main.GetItemDecimal(1, "minsalary_amt"); }
            catch { ldc_minsalaryamt = 0; }
            try { ls_refreqloop = dw_main.GetItemString(1, "refreqloop_docno"); }
            catch { ls_refreqloop = ""; }
            try { ldc_incomemthfix = dw_main.GetItemDecimal(1, "incomemonth_fixed"); }
            catch { ldc_incomemthfix = 0; }
            try { ldc_intcomeoth = dw_main.GetItemDecimal(1, "incomemonth_other"); }
            catch { ldc_intcomeoth = 0; }
            try { ldc_paymonthoth = dw_main.GetItemDecimal(1, "paymonth_other"); }
            catch { ldc_paymonthoth = 0; }

            ldc_salary = ldc_salary + ldc_incomemthfix + ldc_intcomeoth;
            //75%
            Decimal percen_salary = ldc_salary - Convert.ToDecimal((Convert.ToDouble(ldc_salary) * 0.25));



            ldc_shrperiod = 0;
            ldc_sumloan = 0;
            ldc_reqpayment = 0;
            ldc_reqloopclr = 0;

            // ดึงรายการหุ้น
            ldc_shrperiod = dw_main.GetItemDecimal(1, "periodshare_value");
            li_shrpaystatus = Convert.ToInt32(dw_main.GetItemDecimal(1, "sharepay_status"));

            // ถ้างดเก็บค่าหุ้นให้หุ้นต่อเดือนเป็นศูนย์
            if (li_shrpaystatus == -1) { ldc_shrperiod = 0; }


            // ดึงยอดเงินชำระต่อเดือนใบขอกู้
            ls_loantype = dw_main.GetItemString(1, "loantype_code");
            li_paytype = Convert.ToInt32(dw_main.GetItemDecimal(1, "loanpayment_type"));
            ldc_intestm = dw_main.GetItemDecimal(1, "intestimate_amt");
            ldc_payment = dw_main.GetItemDecimal(1, "period_payment");

            Decimal ldc_payamt = dw_main.GetItemDecimal(1, "period_payamt");

            ldc_sumpay = ldc_payment;

            if (li_paytype == 1)
            {
                ldc_sumpay += ldc_intestm;
            }

            // ดึงรายการหนี้
            li_count = dw_clear.RowCount;
            for (li_index = 1; li_index <= li_count; li_index++)
            {

                li_clrstatus = Convert.ToInt32(dw_clear.GetItemDecimal(li_index, "clear_status"));
                li_paytype = Convert.ToInt32(dw_clear.GetItemDecimal(li_index, "loanpayment_type"));
                ldc_payment = dw_clear.GetItemDecimal(li_index, "period_payment");
                ldc_intestm = dw_clear.GetItemDecimal(li_index, "intestimate_amt");
                ldc_sumpay += ldc_payment + ldc_intestm;

            }

            ldc_sumpay = ldc_sumpay + ldc_shrperiod;
            dw_main.SetItemDecimal(1, "paymonth_coop", ldc_sumpay);
            if (ldc_sumpay > percen_salary) { LtServerMessage.Text = WebUtil.WarningMessage("สิทธิ์ตามเงินเดือนคงเหลือไม่ผ่าน"); }

        }

        private void JsPaycoopid()
        {

            if (Hdcoopid.Value.Length > 1)
            {
                dw_main.SetItemString(1, "paytoorder_desc", Hdcoopid.Value);
                dw_main.SetItemString(1, "paytoorder_desc_1", Hdcoopid.Value);
            }
            else
            {
                int paycoop_id = Convert.ToInt16(Hdcoopid.Value);
                dw_main.SetItemString(1, "paytoorder_desc", "001" + paycoop_id.ToString("000"));
                dw_main.SetItemString(1, "paytoorder_desc_1", "001" + paycoop_id.ToString("000"));
            }
        }

        private void JsObjective()
        {
            int objective_code = Convert.ToInt16(Hdobjective.Value);
            dw_main.SetItemString(1, "loanobjective_code", objective_code.ToString("000"));
            dw_main.SetItemString(1, "loanobjective_code_1", objective_code.ToString("000"));
        }



        private void JsRunProcess()
        {

            // --- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                //gid = Request["gid"].ToString();
                gid = "LNNORM_DAILY";
            }
            catch { }
            try
            {
                //rid = Request["rid"].ToString();
                rid = "LNNORM_DAILY15";
            }
            catch { }

            String doc_no = dw_main.GetItemString(1, "loanrequest_docno");

            if (x == 2)
            {
                doc_no = reqdoc_no;
            }
            if (doc_no == null || doc_no == "")
            {
                return;
            }

            //String ls_xmlmain = dw_main.Describe("DataWindow.Data.XML");
            //String ls_format = "CAT";
            //short li_membtime = 0; Decimal ldc_right25 = 0; Decimal ldc_right33 = 0; Decimal ldc_right35 = 0; Decimal ldc_right26 = 0; Decimal ldc_right40 = 0;
            //int re = shrlonService.of_print_callpermiss(state.SsWsPass, ls_xmlmain, ls_format, ref li_membtime, ref ldc_right25, ref ldc_right33, ref ldc_right35, ref ldc_right26, ref ldc_right40);
            //Decimal li_membtime_ = li_membtime;
            //Decimal ldc_right25_ = ldc_right25;
            //Decimal ldc_right33_ = ldc_right33;
            //Decimal ldc_right35_ = ldc_right35;
            //Decimal ldc_right26_ = ldc_right26;
            //Decimal ldc_right40_ = ldc_right40;
            //string loan26 = dw_main.GetItemString(1, "loantype_code");
            //if (loan26 == "26")
            //{
            //    decimal right26 = dw_main.GetItemDecimal(1, "loancredit_amt");
            //    ldc_right26_ = right26;
            //    ldc_right33_ = 0;
            //    ldc_right35_ = 0;
            //    ldc_right25_ = 0;
            //}


            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(doc_no, ArgumentType.String);
            //lnv_helper.AddArgument(li_membtime_.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(ldc_right25_.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(ldc_right33_.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(ldc_right35_.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(ldc_right26_.ToString(), ArgumentType.Number);
            //lnv_helper.AddArgument(ldc_right40_.ToString(), ArgumentType.Number);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF

            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                Saving.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                if (li_return == "true")
                {
                    HdOpenIFrame.Value = "True";
                    HdcheckPdf.Value = "True";

                }
                else if (li_return != "true")
                {
                    HdcheckPdf.Value = "False";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdf"] = pdf;
            //PopupReport();


        }
        private void JsRunProcessInvoice()
        {

            // --- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                //gid = Request["gid"].ToString();
                gid = "LNNORM_DAILY";
            }
            catch { }
            try
            {
                //rid = Request["rid"].ToString();
                rid = "LNNORM_DAILY13";
            }
            catch { }

            String doc_no = dw_main.GetItemString(1, "loanrequest_docno");

            if (x == 2)
            {
                doc_no = reqdoc_no;
            }

            if (doc_no == null || doc_no == "")
            {
                return;
            }




            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(doc_no, ArgumentType.String);

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF

            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                Saving.WcfReport.ReportClient lws_report = wcf.Report;
                String criteriaXML = lnv_helper.PopArgumentsXML();
                this.pdf = lws_report.GetPDFURL(state.SsWsPass) + pdfFileName;
                String li_return = lws_report.Run(state.SsWsPass, app, gid, rid, criteriaXML, pdfFileName);
                if (li_return == "true")
                {
                    HdOpenIFrame.Value = "True";
                    HdcheckPdf.Value = "True";

                }
                else if (li_return != "true")
                {
                    HdcheckPdf.Value = "False";
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }
            Session["pdfinvoice"] = pdf;
            //PopupReport();


        }
        public void JspPopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            String pop = "Gcoop.OpenPopup('" + Session["pdf"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }

        public void JspPopupReportInvoice()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            //String pop = "Gcoop.OpenPopup('http://localhost/GCOOP/WebService/Report/PDF/20110223093839_shrlonchk_SHRLONCHK001.pdf')";

            String pop = "Gcoop.OpenPopup('" + Session["pdfinvoice"].ToString() + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);

        }


        //   สำหรับคำนวณดอกเบี้ยประมาณการสำหรับสัญญาใหม่
        public Decimal of_calintestimatemain()
        {
            string ls_continttabcode, ls_coopid;

            int li_continttype, li_intsteptype;
            Decimal ldc_apvamt, ldc_inttotal = 0, ldc_prncalint;
            Decimal ldc_contintrate, ldc_intincrease;
            DateTime ldtm_calintfrom, ldtm_calintto, ldtm_request, ldtm_estimate;
            try
            {
                // ข้อกำหนดเรื่องดอกเบี้ย
                li_continttype = Convert.ToInt32(dw_main.GetItemDecimal(1, "int_continttype"));
                ls_continttabcode = dw_main.GetItemString(1, "int_continttabcode");
                ldc_contintrate = dw_main.GetItemDecimal(1, "int_contintrate");
                ldc_intincrease = dw_main.GetItemDecimal(1, "int_contintincrease");
                li_intsteptype = Convert.ToInt32(dw_main.GetItemDecimal(1, "int_intsteptype"));
                ls_coopid = dw_main.GetItemString(1, "coop_id");
                ldc_apvamt = dw_main.GetItemDecimal(1, "loanrequest_amt");
                ldc_prncalint = dw_main.GetItemDecimal(1, "loanrequest_amt");
                ldtm_request = dw_main.GetItemDateTime(1, "loanrequest_date");


                ldtm_estimate = ldtm_request.AddDays(31);


                // ถ้าไม่มียอดต้นที่จะคิด ด/บ เป็น 0
                if (ldc_prncalint <= 0) { return 0; }

                // ถ้าสถานะนี้เป็นไม่คิด ด/บ
                if (li_continttype == 0) { return 0; }

                // ถ้าวันที่คิดดอกเบี้ยถึงน้อยกว่าวันที่คิดดอกเบี้ยล่าสุด ด/บ 0
                if (ldtm_estimate <= ldtm_request) { return 0; }

                // ถ้าไม่ได้ set รูปแบบขั้นดอกเบี้ยไว้ set ให้เป็นจากยอดอนุมัติ
                if (li_intsteptype == null) { li_intsteptype = 1; }

                ldtm_calintfrom = ldtm_request;
                ldtm_calintto = ldtm_estimate;

                switch (li_continttype.ToString())
                {
                    case "1":	// ตาม rate ที่ระบุ

                        //// อัตราด/บเพิ่มลดพิเศษ
                        //inv_intsrv.of_setintincrease( 0 );

                        //ldc_inttotal	= inv_intsrv.of_computeinterest( ldc_contintrate, ldtm_calintfrom, ldtm_calintto, ldc_prncalint );
                        break;
                    case "2":	// ตามตารางด/บที่ระบุ

                        // อัตราด/บเพิ่มลดพิเศษ
                        //inv_intsrv.of_setintincrease( ldc_intincrease );

                        // ตรวจว่าดูอัตราด/บจากยอดอนุมัติหรือคงเหลือ
                        //if (li_intsteptype == 1)
                        //{
                        //    ldc_inttotal = wcf.Shrlon.of_computeinterest2(state.SsWsPass, ls_continttabcode, ls_coopid, ldtm_calintfrom, ldtm_calintto, ldc_prncalint, ldc_apvamt);
                        //}
                        //else
                        //{
                        //    ldc_inttotal = wcf.Shrlon.of_computeinterest3(state.SsWsPass, ls_continttabcode, ls_coopid, ldtm_calintfrom, ldtm_calintto, ldc_prncalint);
                        //}
                        break;

                }

                try
                {
                    DateTime startkeep_date = dw_main.GetItemDateTime(1, "startkeep_date");
                    DateTime loanrcvfix_date = dw_main.GetItemDateTime(1, "loanrcvfix_date");
                    short year = Convert.ToInt16(loanrcvfix_date.Year + 543);
                    short month = Convert.ToInt16(loanrcvfix_date.Month);
                    int day_amount = 31;

                    DateTime postingdate_bf = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(loanrcvfix_date.Year + 543), Convert.ToInt16(loanrcvfix_date.Month));
                    DateTime postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(postingdate_bf.Year + 543), Convert.ToInt16(postingdate_bf.Month + 1));
                    short year_pro = Convert.ToInt16(postingdate.Year + 543);
                    short month_pro = Convert.ToInt16(postingdate.Month + 1);
                    if (month_pro > 12)
                    {
                        month_pro = 1;
                        year_pro = Convert.ToInt16(postingdate.Year + 544);
                    }
                    DateTime postingdate_af = wcf.Busscom.of_getpostingdate2(state.SsWsPass, year_pro, month_pro);
                    //  processdate = wcf.Busscom.of_getpostingdate(state.SsWsPass, ldtm_loanreceive_af);


                    ////จ่ายเงินกู้หลังเรียกเก็บหรือไม่
                    //if (loanrcvfix_date < postingdate && loanrcvfix_date < postingdate_bf)
                    //{

                    //    day_amount = postingdate.DayOfYear - loanrcvfix_date.DayOfYear;

                    //}
                    //else if (loanrcvfix_date > postingdate_bf)
                    //{

                    //    postingdate = wcf.Busscom.of_getpostingdate2(state.SsWsPass, Convert.ToInt16(postingdate_af.Year + 543), Convert.ToInt16(postingdate_af.Month));
                    //    day_amount = postingdate.DayOfYear - loanrcvfix_date.DayOfYear;

                    //}
                    //else
                    //{

                    //    day_amount = postingdate_af.DayOfYear - loanrcvfix_date.DayOfYear;
                    //}

                    day_amount = 366 + startkeep_date.DayOfYear - loanrcvfix_date.DayOfYear;

                    ldc_inttotal = ldc_apvamt * ldc_contintrate * day_amount / 365;
                    ldc_inttotal = wcf.Shrlon.of_roundmoney(state.SsWsPass, ldc_inttotal);
                }
                catch { }


            }
            catch (Exception ex)
            {
                //LtServerMessage.Text = WebUtil.ErrorMessage("of_calintestimatemain==+>" + ex);
            }

            return ldc_inttotal;

        }


        protected void CheckColl(object sender, EventArgs e)
        {
            string member_no = dw_main.GetItemString(1, "member_no");
            decimal member_age = dw_main.GetItemDecimal(1, "member_age");


        }

        protected void SetAcci_dept()
        {
            string dept_acc = "";
            try
            {
                string memberNo = dw_main.GetItemString(1, "member_no");
                String strSQL = "";
                strSQL = @"SELECT DPDEPTMASTER.DEPTACCOUNT_NO,   
         DPDEPTMASTER.MEMBER_NO,   
         DPDEPTMASTER.DEPTACCOUNT_NAME,   
         DPDEPTMASTER.DEPTTYPE_CODE,   
         MBMEMBMASTER.MEMB_NAME,   
         MBMEMBMASTER.MEMB_SURNAME,   
         MBMEMBMASTER.MEMBGROUP_CODE,   
         DPDEPTMASTER.DEPT_OBJECTIVE,   
         DPDEPTMASTER.DEPTCLOSE_STATUS,   
         DPDEPTTYPE.DEPTTYPE_GROUP,   
         DPDEPTMASTER.PRNCBAL  
    FROM DPDEPTMASTER,   
         MBMEMBMASTER,   
         DPDEPTTYPE  
   WHERE ( DPDEPTMASTER.MEMBER_NO = MBMEMBMASTER.MEMBER_NO ) and  
         ( DPDEPTMASTER.DEPTTYPE_CODE = DPDEPTTYPE.DEPTTYPE_CODE ) and  
         ( DPDEPTMASTER.MEMCOOP_ID = DPDEPTTYPE.COOP_ID )  and DPDEPTMASTER.DEPTCLOSE_STATUS=0 and DPDEPTMASTER.DEPTTYPE_CODE=10 and
         ( DPDEPTMASTER.MEMBER_NO = '" + memberNo + "')  ORDER BY DPDEPTMASTER.DEPTACCOUNT_NO ASC ";
                Sdt dtdept = WebUtil.QuerySdt(strSQL);

                if (dtdept.Next())
                {
                    int rowCount = dtdept.GetRowCount();
                    if (rowCount == 1)
                    {

                        for (int x = 0; x < rowCount; x++)
                        {

                            dept_acc = dtdept.Rows[0]["DEPTACCOUNT_NO"].ToString();
                            if (x == 0)
                            {
                                dw_main.SetItemString(1, "expense_accid", dept_acc);
                            }
                        }
                    }
                }

            }
            catch { dept_acc = ""; }

        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            XmlConfigService xml = new XmlConfigService();

            JspopupLoanemerReport(true, xml.ShrlonPrintMode);

        }

        private void JspopupLoanemerReport()
        {
            reqdoc_no = txt_reqNo.Text;
            member_no = txt_member_no.Text;
            string as_xml = "";
            try
            {
                try
                {
                    fromset = state.SsPrinterSet;

                }
                catch (Exception ex)
                {
                    fromset = "216";

                }
                string re = wcf.Shrlon.of_printloan(state.SsWsPass, reqdoc_no, fromset, state.SsCoopId, member_no, ref as_xml);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์" + ex); }
        }

        private void JspopupLoanemerReport(bool isJsPrint, int printMode)
        {
            reqdoc_no = txt_reqNo.Text;
            try
            {
                Printing.ShrlonPrintEmerReport(this, state.SsCoopId, reqdoc_no, printMode);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ตรวจสอบเครื่องพิมพ์: " + ex.Message);
            }
        }



    }



}


/// 
/// ประเภทเงินกู้  lnloantype
/// ประเภทสมาชิก lnloanmbtype
/// กู้ฉุกเฉิน           10   salary X 3   max_period = 12 ใช้หุ้นค้ำ
/// กู้สามัญหุ้น/เงินฝากค้ำ  20   กู้หุ้นได้ 100%  ,เงินฝาก 100%    max_period  = 96  <800,000 ||  max_period  = 120  >  800,000 <2,000,000 
/// กู้สามัญบุคคลค้ำ      21   ตามตาราง สิทธิ์กู้  lnloantypecustum  ตามตาราง สิทธิํค้ำ lngrpmanprtpermtdet
///                       max_period  = 96  <800,000 ||  max_period  = 120  >  800,000 <2,000,000 
/// 
/// 
/// 
/// 
///