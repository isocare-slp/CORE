using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_sl_fundcoll_payment_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.FUNDCOLLSTATEMENTDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.FUNDCOLLSTATEMENT;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();
        }

        public void Retrieve(string memberno)
        {
            string sql = @"select 0 as operate_flag, 'จ่ายคืนทะเบียน ' + fund.member_no as slipitem_desc, 
                  fund.fundbalance,
				  fund.operate_date,
				  fund.loancontract_no,
                  fund.member_no,
				  fund.balance
                    from  ( select fundcollmaster.member_no,fundcollmaster.fundbalance,fundcollstatement.balance,fundcollstatement.operate_date,fundcollstatement.loancontract_no  
                    from  fundcollmaster , fundcollstatement where ltrim(rtrim(fundcollmaster.member_no)) = ltrim(rtrim(fundcollstatement.member_no)) 
                    and fundcollmaster.fund_status = 1 and fundcollstatement.seq_no =(  select max(c.seq_no) from fundcollstatement c where fundcollstatement.member_no = c.member_no ) ) fund 
				where member_no = {0}";
            sql = WebUtil.SQLFormat(sql, memberno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}