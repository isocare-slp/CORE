﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using System.Data;

namespace Saving.Applications.hr.ws_hr_cancel_leave_ctrl
{
    public partial class DsSearch : DataSourceFormView
    {
        public DataSet1.HRLOGLEAVEDataTable DATA { get; set; }

        public void InitMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.HRLOGLEAVE;
            this.EventItemChanged = "OnDsSearchItemChanged";
            this.EventClicked = "OnDsSearchClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsSearch");
            this.Button.Add("b_search");
            this.Register();
        }
    }
}