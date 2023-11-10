﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Saving.Applications.trading.dlg
{
    public partial class w_dlg_td_search_slip_store : PageWebDialog, WebDialog
    {
        private DwThDate tDwCri;
        private DwThDate tDwMain;
        protected String jsSearchData;
        public void InitJsPostBack()
        {
            tDwCri = new DwThDate(DwCri);
            tDwCri.Add("s_slip_date", "s_slip_tdate");
            tDwCri.Add("e_slip_date", "e_slip_tdate");

            tDwMain = new DwThDate(DwMain);
            tDwMain.Add("slip_date", "slip_tdate");

            jsSearchData = WebUtil.JsPostBack(this, "jsSearchData");
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                DwCri.InsertRow(0);
                try
                {
                    HdSlipType.Value = Request["sliptype_code"].ToString().Trim();
                    DwCri.SetItemString(1, "sliptype_code", HdSlipType.Value);
                }
                catch
                {
                    HdSlipType.Value = "";
                    //LtServerMessage.Text = WebUtil.ErrorMessage("เกิดข้อผิดพลาดทางเทคนิค กรุณากดใหม่");
                    //return;
                }                
            }
            else
            {
                this.RestoreContextDw(DwCri, tDwCri);
                this.RestoreContextDw(DwMain, tDwMain);
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            switch (eventArg)
            {
                case "jsSearchData":
                    InitData();
                    break;
            }
        }

        public void WebDialogLoadEnd()
        {
            tDwCri.Eng2ThaiAllRow();
            tDwMain.Eng2ThaiAllRow();

            DwCri.SaveDataCache();
            DwMain.SaveDataCache();
        }

        private void InitData()
        {
            try
            {
                DwUtil.RetrieveDataWindow(DwMain, "dlg_td_search_slip_store.pbl", null, null);

                DwTrans SQLCA = new DwTrans();
                SQLCA.Connect();
                DwMain.SetTransaction(SQLCA);
                String SQLBegin = DwMain.GetSqlSelect();
                string slip_no, s_slip_tdate, e_slip_tdate;
                decimal slip_status;
                try
                {
                    slip_no = DwCri.GetItemString(1, "slip_no");
                }
                catch { slip_no = ""; }
                //try
                //{
                //    debt_no = DwCri.GetItemString(1, "debt_no");
                //}
                //catch { debt_no = ""; }
                //try
                //{
                //    debt_name = DwCri.GetItemString(1, "debt_name");
                //}
                //catch { debt_name = ""; }
                try
                {
                    slip_status = DwCri.GetItemDecimal(1, "slip_status");
                }
                catch { slip_status = 8; }
                try
                {
                    s_slip_tdate = (DwCri.GetItemDateTime(1, "s_slip_date")).ToString("dd/MM/yyyy");
                }
                catch { s_slip_tdate = ""; }
                try
                {
                    e_slip_tdate = (DwCri.GetItemDateTime(1, "e_slip_date")).ToString("dd/MM/yyyy");
                }
                catch { e_slip_tdate = ""; }

                if (slip_no != "")
                {
                    SQLBegin = SQLBegin + " and tdstockslip.slip_no like '" + slip_no + "%'";
                }
                //if (debt_no != "")
                //{
                //    SQLBegin = SQLBegin + " and tdstockslip.debt_no like '" + debt_no + "%'";
                //}
                //if (debt_name != "")
                //{
                //    SQLBegin = SQLBegin + " and tddebtmaster.debt_name like '" + debt_name + "%'";
                //}
                if (s_slip_tdate != "" )
                {
                    SQLBegin = SQLBegin + " and trunc(tdstockslip.slip_date) >= trunc(to_date('" + s_slip_tdate + "', 'dd/mm/yyyy'))";
                }

                if (e_slip_tdate != "")
                {
                    SQLBegin = SQLBegin + " and trunc(tdstockslip.slip_date) <= trunc(to_date('" + e_slip_tdate + "', 'dd/mm/yyyy'))";
                }
                SQLBegin = SQLBegin + " and tdstockslip.slip_status = " + slip_status + "";
                try
                {
                    String SQL = SQLBegin + " and tdstockslip.coop_id = '" + state.SsCoopId + "'";

                    if ((HdSlipType.Value).Trim() != "")
                    {
                        SQL = SQLBegin + " and tdstockslip.sliptype_code = '" + HdSlipType.Value + "'";
                    }

                    DwMain.SetSqlSelect(SQL);
                    DwMain.Retrieve();
                    SQLCA.Disconnect();
                }
                catch
                {
                    SQLCA.Disconnect();
                    LtServerMessage.Text = WebUtil.ErrorMessage("แก้ข้อผิดพลาด ไม่สามารถค้นหาข้อมูลได้");
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}