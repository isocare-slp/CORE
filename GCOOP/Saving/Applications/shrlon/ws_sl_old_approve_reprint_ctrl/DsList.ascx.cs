using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_old_approve_reprint_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_LISTDataTable DATA { get; set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            css2.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_LIST;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            //this.Button.Add("b_detail");
            //this.Button.Add("b_contsearch");
            this.Register();
        }
        public void Retrieve(string memno)
        {
            string sql = @"SELECT  
		                        lm.loantype_code,
		                        lm.member_no+' '+mn.PRENAME_DESC+' '+mb.MEMB_NAME+' '+mb.MEMB_SURNAME as fullname,
		                        lm.loanrequest_amt,
		                        lm.loancontract_no,
                                lm.approve_id
                           FROM lncontmaster lm ,  mbmembmaster mb ,mbucfprename mn
                           WHERE 
		                        lm.member_no = mb.member_no and
		                        mb.prename_code = mn.prename_code and
							lm.principal_balance > 0 and
		                        (lm.COOP_ID = {0}) and 
                                lm.member_no = {1}
                           ORDER By lm.loancontract_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, memno);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}