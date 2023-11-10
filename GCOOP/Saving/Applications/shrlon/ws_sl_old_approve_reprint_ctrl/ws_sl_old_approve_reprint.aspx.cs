using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLibrary;
using System.Drawing;
using CoreSavingLibrary.WcfNShrlon;
using System.Data;
using System.IO;

namespace Saving.Applications.shrlon.ws_sl_old_approve_reprint_ctrl
{
    public partial class ws_sl_old_approve_reprint_ctrl : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PrintCont { get; set; }
        [JsPostBack]
        public string PrintColl { get; set; }
        [JsPostBack]
        public string PrintIns { get; set; }
        [JsPostBack]
        public string PrintContSpc { get; set; }
        [JsPostBack]
        public string PostPrintFlag { get; set; }
        static string apvlist = "";
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {

        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string memb_no = WebUtil.MemberNoFormat(dsMain.DATA[0].member_no);
                dsList.Retrieve(memb_no);
                dsMain.DATA[0].member_no = memb_no;
                dsMain.DATA[0].membname = dsList.DATA[0].fullname;
                dsMain.DATA[0].entry_id = state.SsUsername;
            }
            else if (eventArg == PrintCont)
            {
                apvlist = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal flag = dsList.DATA[i].print_flag;
                    if (flag == 1)
                    {
                        if (apvlist == "")
                        {
                            apvlist = "'" + dsList.DATA[i].loancontract_no.Trim() + "'";
                        }
                        else
                        {
                            apvlist = apvlist + ",'" + dsList.DATA[i].loancontract_no.Trim() + "'";
                        }
                    }
                }
                PrintCont_Click(apvlist);

            }
            else if (eventArg == PrintColl)
            {
                apvlist = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal flag = dsList.DATA[i].print_flag;
                    if (flag == 1)
                    {
                        if (apvlist == "")
                        {
                            apvlist = "'" + dsList.DATA[i].loancontract_no.Trim() + "'";
                        }
                        else
                        {
                            apvlist = apvlist + ",'" + dsList.DATA[i].loancontract_no.Trim() + "'";
                        }
                    }
                }
                PrintColl_Click(apvlist);
            }
            else if (eventArg == PrintIns)
            {
                apvlist = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal flag = dsList.DATA[i].print_flag;
                    if (flag == 1)
                    {
                        if (apvlist == "")
                        {
                            apvlist = "'" + dsList.DATA[i].loancontract_no.Trim() + "'";
                        }
                        else
                        {
                            apvlist = apvlist + ",'" + dsList.DATA[i].loancontract_no.Trim() + "'";
                        }
                    }
                }
                PrintIns_Click(apvlist);
            }
            else if (eventArg == PrintContSpc)
            {
                apvlist = "";
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    decimal flag = dsList.DATA[i].print_flag;
                    if (flag == 1)
                    {
                        if (apvlist == "")
                        {
                            apvlist = dsList.DATA[i].loancontract_no.Trim();
                        }
                        else
                        {
                            apvlist = apvlist + "," + dsList.DATA[i].loancontract_no.Trim();
                        }
                    }
                }
                PrintContSpc_Click(apvlist);
            }
            else if (eventArg == PostPrintFlag)
            {
                Int32 row = Convert.ToInt32(hdrow.Value.ToString().Trim());
                decimal flag = dsList.DATA[row].print_flag;
                if (flag == 1)
                {
                    dsList.DATA[row].print_flag = 1;
                }
                else if (flag == 0)
                {
                    dsList.DATA[row].print_flag = 0;
                }

            }


        }

        public void SaveWebSheet()
        {

        }

        public void WebSheetLoadEnd()
        {

        }
        public void PrintCont_Click(string loanrequest_docno)
        {

            if (loanrequest_docno != "")
            {
                try
                {
                    string sql = @"SELECT distinct mn.PRENAME_DESC+' '+mb.MEMB_NAME+' '+mb.MEMB_SURNAME as fullname,
DATEDIFF(year,mb.BIRTH_DATE,getdate())/12 as age,
         lr.MEMBER_NO,
         lr.LOANTYPE_CODE,
         lr.LOANCREDIT_AMT,
         lr.loanapprove_amt as LOANPERMISS_AMT,
         lr.LOANREQUEST_AMT,
'ftreadtbaht(lr.LOANREQUEST_AMT)' as thbathloan,
'ftreadtbaht(lr.PERIOD_PAYMENT)' as thbathpayment,
         lr.PERIOD_PAYMENT,
         lr.LOANPAYMENT_TYPE,
         mb.MEMB_NAME,
         mb.MEMB_SURNAME,
         mb.MEMBGROUP_CODE,
         mg.MEMBGROUP_DESC,
         cc.COOP_NAME,
         cc.MANAGER,
convert(varchar(2),month(lr.loanapprove_date))+'/'+convert(varchar(2),day(lr.loanapprove_date)) +'/'+ convert(varchar(4),convert(float,convert(datetime,YEAR(lr.loanapprove_date))+543)),
         lr.loanapprove_date as loanrequest_date,
         lr.LOANCONTRACT_NO,
         lr.PERIOD_LASTPAYMENT,
         mb.SALARY_AMOUNT as salary_amt,
         0 as SHARE_LASTPERIOD,
         0 as SHARE_PERIODVALUE,
         mb.ADDR_NO,
         mb.ADDR_MOO,
         mb.ADDR_SOI,
         mb.ADDR_VILLAGE,
         mb.ADDR_ROAD,
         md.DISTRICT_DESC,
         mp.PROVINCE_DESC,
         mt.TAMBOL_DESC,
         mb.TAMBOL_CODE,
         mb.AMPHUR_CODE,
         mb.PROVINCE_CODE,
         mb.ADDR_POSTCODE,
		pt.POSITION_DESC,
isnull(mb.POSITION_DESC,'   ') as POSDESC ,
convert(varchar(2),month(lr.loanapprove_date)),
         lr.PERIOD_PAYAMT,
		mb.ADDR_PHONE,
		lr.PERIOD_LASTPAYMENT,
		li.INTEREST_RATE,
		lo.LOANOBJECTIVE_DESC,
		dbo.FT_CALAGEMTH(mb.BIRTH_DATE,getdate()) AS BIRTH_DATE
    FROM  MBMEMBMASTER mb LEFT JOIN  lncontmaster lr ON   lr.COOP_ID = mb.COOP_ID and lr.MEMBER_NO = mb.MEMBER_NO
LEFT JOIN LNLOANTYPE lt  ON  lr.LOANTYPE_CODE = lt.LOANTYPE_CODE 
LEFT JOIN LNCFLOANINTRATEDET li ON  li.LOANINTRATE_CODE = lt.INTTABRATE_CODE
LEFT JOIN LNUCFLOANOBJECTIVE lo ON  lr.LOANOBJECTIVE_CODE = lo.LOANOBJECTIVE_CODE
LEFT JOIN MBUCFDISTRICT md  ON mb.amphur_code = md.district_code AND rtrim(ltrim(mb.province_code)) = md.province_code
LEFT JOIN MBUCFPROVINCE mp  ON   mp.province_code = md.province_code
LEFT JOIN MBUCFTAMBOL mt ON  mb.tambol_code = mt.tambol_code  and mb.tambol_code = mt.tambol_code
LEFT JOIN   MBUCFMEMBGROUP mg ON  mg.COOP_ID = mb.COOP_ID and  rtrim( ltrim(mg.MEMBGROUP_CODE)) =  rtrim( ltrim(mb.MEMBGROUP_CODE) )
LEFT JOIN MBUCFPOSITION pt ON  pt.POSITION_CODE = mb.POSITION_CODE 
LEFT JOIN   MBUCFPRENAME mn ON mn.PRENAME_CODE = mb.PRENAME_CODE ,        CMCOOPCONSTANT cc
where (lr.LOANAPPROVE_DATE between li.EFFECTIVE_DATE and li.EXPIRE_DATE ) and
	( lr.loancontract_no in (" + apvlist + @")  )
    ORDER By lr.loancontract_no";

                    //LtServerMessage.Text = apvlist;

                    sql = WebUtil.SQLFormat(sql, state.SsCoopId);

                    iReportArgument args = new iReportArgument(sql);
                    //args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopId);
                    //args.Add("as_loanreqdocno", iReportArgumentType.String, apvlist);
                    iReportBuider report = new iReportBuider(this, "กำลังสร้างใบปะหน้าพิจารณาการขอกู้");
                    report.AddCriteria("r_ln_print_loan_req_doc_gsb", "ใบปะหน้าพิจารณาการขอกู้", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        public void PrintColl_Click(string loanrequest_docno)
        { 
            //apvlist = "'Q580000207','Q580000208'";
            if (loanrequest_docno != "")
            {
                try
                {
                    string sql = @"select DISTINCT ma.PRENAME_DESC+' '+mb.MEMB_NAME+' '+mb.MEMB_SURNAME as fullname,
mb.member_no,
lm.loanapprove_date as loanrequest_date,
SUBSTRING (CONVERT(VARCHAR(8), lm.loanapprove_date, 112), 5, 2) as day,
SUBSTRING (CONVERT(VARCHAR(8), lm.loanapprove_date, 112), 7, 2) as month,
SUBSTRING (CONVERT(VARCHAR(8), lm.loanapprove_date, 112 ), 5, 2)  as  year,
mb.card_person,
mb.Addr_No,
mb.Addr_Moo,
mb.Addr_Soi,
mb.Addr_Village,
mb.Addr_Road,
mb.Tambol_Code,
tb.tambol_desc,
mb.Amphur_Code,
dt.district_desc,
mb.Province_Code,
mb.Addr_Postcode,
mb.Addr_Mobilephone,
lm.loanapprove_amt,
mg.membgroup_desc,
mp.position_desc,
'ftreadtbaht(lm.loanapprove_amt)'as loanapprove_tbaht,
'ft_getmbname(lc.coop_id,trim(lc.ref_collno))' as coll_name ,
lc.ref_collno,
'ft_memgrp(lc.coop_id,mb.membgroup_code)' as membgroup_desc,
lc.collactive_amt,
'ftreadtbaht(lc.collactive_amt)' as collactive_tbaht,
'trunc(Ft_Calage( birth_date , sysdate , 4 )) 'as birth,
pr.province_desc,
ln.LOANOBJECTIVE_DESC
from mbmembmaster mb LEFT JOIN  lncontmaster lm on mb.member_no =  lm.member_no and  mb.coop_id = lm.coop_id
LEFT JOIN lncontcoll  lc ON lm.loancontract_no = lc.loancontract_no and lm.coop_id = lc.coop_id  
LEFT JOIN mbucfprename ma on mb.prename_code = ma.prename_code
LEFT JOIN LNUCFLOANOBJECTIVE ln ON  lm.LOANOBJECTIVE_CODE = ln.LOANOBJECTIVE_CODE
LEFT JOIN  mbucfmembgroup mg on  mb.membgroup_code = mg.membgroup_code
LEFT JOIN  mbucfprovince pr on mb.province_code = pr.province_code
LEFT JOIN  mbucfdistrict dt on mb.amphur_code = dt.district_code
LEFT JOIN  mbucftambol tb on mb.tambol_code = tb.tambol_code
LEFT JOIN mbucfposition mp on  mb.position_code = mp.position_code
where lm.loancontract_no in (" + apvlist + @")
and lm.coop_id = {0}
and lc.loancolltype_code in ('01','55','99')";
                    sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                    iReportArgument args = new iReportArgument(sql);
                    //args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopId);
                    //args.Add("as_loanrequest_docno", iReportArgumentType.String, apvlist);

                    iReportBuider report = new iReportBuider(this, "กำลังสร้างใบปะหน้าพิจารณาการขอกู้");
                    report.AddCriteria("r_ln_print_loan_coll_doc_gsb", "ใบปะหน้าพิจารณาการขอกู้", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        public void PrintIns_Click(string loanrequest_docno)
        {
            if (loanrequest_docno != "")
            {
                try
                {
                    string sql = @"SELECT distinct mp.PRENAME_DESC+''+mb.MEMB_NAME+' '+ mb.MEMB_SURNAME as fullname,
         lr.MEMBER_NO,
         lr.LOANTYPE_CODE,
         lr.LOANREQUEST_DOCNO,
         lr.loanapprove_amt as LOANPERMISS_AMT,
         lr.LOANREQUEST_AMT,
         lr.LOANPAYMENT_TYPE,
         lr.PERIOD_PAYMENT,
         mb.MEMB_NAME,
         mb.MEMB_SURNAME,
         mb.MEMBGROUP_CODE,
         mg.MEMBGROUP_DESC,
         cc.COOP_NAME,
         cc.MANAGER,
         lr.LOANCONTRACT_NO,
         mb.MEMBER_NO,
         mb.SALARY_ID,
         pt.POSITION_DESC,
convert(DATE, GETDATE(), 103) as dateAPP,
         lr.LOANAPPROVE_AMT,
'ftreadtbaht( lr.LOANAPPROVE_AMT)' as LOANAPPROVE_TBAHT
    FROM lncontmaster lr LEFT JOIN MBMEMBMASTER mb on lr.COOP_ID = mb.COOP_ID and  lr.MEMBER_NO = mb.MEMBER_NO 
LEFT JOIN  LNUCFLOANOBJECTIVE lo on  lr.LOANOBJECTIVE_CODE = lo.LOANOBJECTIVE_CODE
LEFT JOIN MBUCFPOSITION pt on pt.POSITION_CODE = mb.POSITION_CODE 
LEFT JOIN MBUCFPRENAME mp ON mp.PRENAME_CODE = mb.PRENAME_CODE
LEFT JOIN    MBUCFMEMBGROUP mg on  mg.COOP_ID = mb.COOP_ID  and mg.MEMBGROUP_CODE = mb.MEMBGROUP_CODE 
LEFT JOIN  LNLOANTYPE lt on lr.LOANTYPE_CODE = lt.LOANTYPE_CODE 
LEFT JOIN   LNCFLOANINTRATEDET li on li.LOANINTRATE_CODE = lt.INTTABRATE_CODE ,   CMCOOPCONSTANT cc
WHERE (lr.LOANAPPROVE_DATE between li.EFFECTIVE_DATE and li.EXPIRE_DATE) and
         ( lr.COOP_ID = {0}) AND
         ( lr.loancontract_no in (" + apvlist + @"))
    ORDER By lr.loancontract_no";

                    sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                    iReportArgument args = new iReportArgument(sql);
                    //args.Add("as_coop_id", iReportArgumentType.String, state.SsCoopId);
                    //args.Add("as_loanrequest_docno", iReportArgumentType.String, apvlist);

                    iReportBuider report = new iReportBuider(this, "");
                    report.AddCriteria("r_ln_print_loan_ins_doc_gsb", "ใบปะหน้าพิจารณาการขอกู้", ReportType.pdf, args);
                    report.AutoOpenPDF = true;
                    report.Retrieve();

                }
                catch (Exception ex)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
                }
            }
        }

        public void PrintContSpc_Click(string loanrequest_docno)
        {
            try
            {
                iReportArgument args = new iReportArgument();
                args.Add("as_coopid", iReportArgumentType.String, state.SsCoopId);
                args.Add("as_loanreq", iReportArgumentType.String, apvlist);
                iReportBuider report = new iReportBuider(this, "กำลังสร้างใบปะหน้าพิจารณาการขอกู้เงินกู้พิเศษ");
                report.AddCriteria("r_ln_print_loan_req_doc_spc_gsb_reprint", "ใบปะหน้าพิจารณาการขอกู้เงินกู้พิเศษ", ReportType.pdf, args);
                report.AutoOpenPDF = true;
                report.Retrieve();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }

        }

    }
}