﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_loancont_payment
{
    public partial class u_cri_coopid_loancont_payment : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;
        [JsPostBack]
        public string PostMemberNo { get; set; }
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            //--- Page Arguments
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
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            if (!IsPostBack)
            {
                dsMain.DdCoopId();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string member_no = WebUtil.MemberNoFormat(dsMain.DATA[0].member_no);
                dsMain.DdLoancont(member_no);
                dsMain.DATA[0].member_no = member_no;
            }
        }

        public void RunReport()
        {
            string as_coopid = dsMain.DATA[0].coop_id;
            string as_loancont = dsMain.DATA[0].loancontract_no;
            string as_payment = dsMain.DATA[0].payment;
            string as_tpayment = dsMain.DATA[0].tpayment;

            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_loancont", iReportArgumentType.String, as_loancont);
                arg.Add("as_payment", iReportArgumentType.String, as_payment);
                arg.Add("as_tpayment", iReportArgumentType.String, as_tpayment);

                iReportBuider report = new iReportBuider(this, arg);

                report.Retrieve();
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {

        }
    }
}