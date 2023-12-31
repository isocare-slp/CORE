﻿using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Sybase.DataWindow;
using DataLibrary;
//using CoreSavingLibrary.WcfNKeeping;
using System.Web.Services.Protocols;
using System.Text;
using System.Collections.Generic;

namespace Saving.Applications.keeping
{
    public partial class w_sheet_kp_slcls_year_shrlon : PageWebSheet,WebSheet
    {

        //ประกาศตัวแปร
        #region Variable
        //private n_keepingClient KeepingService;
        protected String postNewClear;
        protected String postRefresh;
        protected String postProcClsYear;
        public string outputProcess;
        string pbl = "kp_slcls_year_shrlon.pbl";
        #endregion
        //============================================

        #region Websheet Members
        public void InitJsPostBack()
        {
            //=========================================
            postNewClear = WebUtil.JsPostBack(this, "postNewClear");
            postRefresh = WebUtil.JsPostBack(this, "postRefresh");
            postProcClsYear = WebUtil.JsPostBack(this, "postProcClsYear");
        }

        public void WebSheetLoadBegin()
        {
            Hd_process.Value = "false";
            if (!IsPostBack)
            {
                JspostNewClear();
            }
            else
            {
                this.RestoreContextDw(Dw_option);
                this.RestoreContextDw(Dw_list);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            // Event ที่เกิดจาก JavaScript
            switch (eventArg)
            {
                case "postNewClear":
                    JspostNewClear();
                    break;
                case "postRefresh":
                    //Refresh();
                    break;
                case "postProcClsYear":
                    JspostProcClsYear();
                    break;
            }
        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {
            Dw_option.SaveDataCache();
            Dw_list.SaveDataCache();
        }
        #endregion
        //=============================================

        //function การหาปีปันผล จากตารางปีบัญชี
        private void JspostSetYear()
        {
            try
            {
                int li_year = 0;
                DateTime ldtm_workdate = state.SsWorkDate;
                String sql = @"select min(account_year) from accaccountyear where close_account_stat = 0";
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    
                    //li_year = int.Parse(dt.GetString("min(account_year)")) + 543;

                    Dw_option.SetItemDecimal(1, "clsyr_year", Convert.ToDecimal(ldtm_workdate.Year) + 543);
                    
                }
                else
                {
                    sqlca.Rollback();
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
        }
        // function เคลียร์หน้าจอ
        private void JspostNewClear()
        {
            Dw_option.Reset();
            Dw_option.InsertRow(0);
            Dw_option.SetItemString(1, "application", state.SsApplication);
            Dw_option.SetItemDateTime(1, "operate_date", state.SsWorkDate);
            Dw_option.SetItemDateTime(1, "entry_date", state.SsWorkDate);
            Dw_option.SetItemString(1, "coop_id", state.SsCoopId);
            Dw_option.SetItemString(1, "entryby_coopid", state.SsCoopId);
            Dw_option.SetItemString(1, "entry_id", state.SsUsername);
            JspostSetYear();
            JspostInitLoan();
        }

        // function ประมาณผลปันผลเฉลี่ยคืน
        private void JspostProcClsYear()
        {
            String xml_option = "", xml_clrdoc = "";
            DateTime? start_date = null;
            decimal clsyr_year = Dw_option.GetItemDecimal(1, "clsyr_year");
            String sql = @"select accstart_date from cmaccountyear where account_year= {0}";
            sql = WebUtil.SQLFormat(sql, clsyr_year);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                start_date = dt.GetDate("accstart_date");
            }
            sql = @"update lncontmaster set interest_accum =
                (
                 select
                 sum( (case lnucfloanitemtype.sign_flag when -1 then 1 when 1 then -1 else 0 end ) * case when ls.interest_payment = null then 0 else ls.interest_payment end   ) as interest_payment
                 from   lncontmaster ln , lncontstatement ls,lnucfloanitemtype
                 where  ln.loancontract_no = ls.loancontract_no
                 and  ls.loanitemtype_code = lnucfloanitemtype.loanitemtype_code 
                 and ls.slip_date >= {0}
                 and ln.member_no in(select member_no from mbmembmaster where membgroup_code <> '999' and resign_status=0)
                 and lncontmaster.member_no = ln.member_no
                 and lncontmaster.loancontract_no = ln.loancontract_no
                )";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update shsharestatement set share_date = operate_date where shritemtype_code not in ('SWD','STL', 'RWD') and share_date is null";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update shsharestatement set share_date = slip_date where shritemtype_code in ('SWD','STL', 'RWD', 'RPX')  and share_date is null";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update lncontstatement set intaccum_date = slip_date  where ( loanitemtype_code like 'R%' or loanitemtype_code like 'T%' ) and intaccum_date is null ";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update lncontstatement set intaccum_date = operate_date where ( loanitemtype_code like 'L%' or loanitemtype_code like 'A%' ) and intaccum_date is null ";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update lncontstatement set calavg_status = 0  where intaccum_date >= {0}";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update lncontstatement set calavg_status = 1  where intaccum_date < {0}";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update shsharestatement set caldiv_status = 0  where share_date >= {0}";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            sql = @"update shsharestatement set caldiv_status = 1  where share_date < {0}";
            sql = WebUtil.SQLFormat(sql, start_date);
            WebUtil.ExeSQL(sql);
            try
            {
                // JspostInitLoan();
                //KeepingService = wcf.NKeeping;
                //str_slcls_proc astr_slcls_proc = new str_slcls_proc();
                //astr_slcls_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //astr_slcls_proc.xml_clrdoc = Dw_list.Describe("DataWindow.Data.XML");
                //KeepingService.RunKpSlClsYearShrlonProcess(state.SsWsPass, astr_slcls_proc, state.SsApplication, state.CurrentPage);
                //Hd_process.Value = "true";
                xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //List<string> docnoClr = ListDocClr();
                //String TempFile = WebUtil.createTempFile(docnoClr, "LNCLOSEYEAR");
                //xml_clrdoc = ListDocClr(); //Dw_list.Describe("DataWindow.Data.XML");
                //xml_clrdoc = TempFile; //Dw_list.Describe("DataWindow.Data.XML");
                //LNCLOSEDAYYEAR ต้องแก้ไขให้อ่านข้อมูล string จาก File path ที่ส่งไปแทน xml_clrdoc
                outputProcess = WebUtil.runProcessing(state, "LNCLOSEYEAR", xml_option, "", "");
            }
            catch (SoapException ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(WebUtil.SoapMessage(ex));
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.ToString());
            }
        }

        private void JspostInitLoan()
        {
            try
            {
                //KeepingService = wcf.NKeeping;
                //str_slcls_proc astr_slcls_proc = new str_slcls_proc();
                //astr_slcls_proc.xml_option = Dw_option.Describe("DataWindow.Data.XML");
                //int result = KeepingService.of_init_slcls_year_lon_opt(state.SsWsPass, ref astr_slcls_proc);
                //if (result == 1)
                //{
                //    DwUtil.ImportData(astr_slcls_proc.xml_clrdoc, Dw_list, null, FileSaveAsType.Xml);
                //}
                DwUtil.RetrieveDataWindow(Dw_list, pbl, null, null);
            }
            catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }

        }

        private List<string> ListDocClr()
        {
            List<string> docnoClr =new List<string>(); String coopId = "", docNo = "";            
            Dw_list.SetFilter("clear_type = 1");
            Dw_list.Filter();
            int c = Dw_list.RowCount;
            for (int i = 1; i <= c; i++)
            {
                coopId = Dw_list.GetItemString(i, "coop_id") + ",";
                docNo = Dw_list.GetItemString(i, "document_code").Trim() + ";";
                docnoClr.Add( coopId + docNo );
            }
            return docnoClr;
        }
    }
}