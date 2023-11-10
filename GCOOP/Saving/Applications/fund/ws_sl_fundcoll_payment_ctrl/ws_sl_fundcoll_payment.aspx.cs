using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using CoreSavingLibrary.WcfNShrlon;
using CoreSavingLibrary.WcfNDeposit;
using DataLibrary;
using System.Data;

namespace Saving.Applications.fund.ws_sl_fundcoll_payment_ctrl
{
    public partial class ws_sl_fundcoll_payment : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string JsPostMember { get; set; }
        [JsPostBack]
        public string JsPostCalint { get; set; }
        [JsPostBack]
        public string JsPostTofromaccid { get; set; }
        [JsPostBack]
        public string PostOperateFlag { get; set; }

        Sdt ta = new Sdt();
        Sdt ta2 = new Sdt();

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                SetDefault();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == JsPostMember)
            {
                PostMember();
            }
            else if (eventArg == JsPostCalint)
            {

            }
            else if (eventArg == JsPostTofromaccid)
            {
                SetDefaultTofromaccid();
            }
            else if (eventArg == PostOperateFlag)
            {
                int row = dsList.GetRowFocus();
                decimal operate_flag = dsList.DATA[row].OPERATE_FLAG;
                decimal bf_fundbalance = dsList.DATA[row].BALANCE;

                if (operate_flag == 1)
                {
                    dsList.DATA[row].ITEMPAY_AMT = bf_fundbalance;
                    dsMain.DATA[0].PAYOUTNET_AMT = bf_fundbalance;
                }
                else if (operate_flag == 0)
                {
                    dsList.DATA[row].ITEMPAY_AMT = 0;
                    dsMain.DATA[0].PAYOUTNET_AMT = 0;
                }
            }
            
        }

        public void SaveWebSheet()
        {
            String payoutslip_no = "", fundmember_no = "", member_no = "", membgroup_code = "", sliptype_code = "", rslip = "", loantype_code = "", loancontract_no = "";
            String moneytype_code = "", expense_bank = "", expense_branch = "", expense_accid = "", tofrom_accid = "", table_fund = "", table_fundstm = "", ls_slipno = "";
            DateTime slip_date, operate_date;
            decimal payout_amtnet = 0, fundbalance = 0;
            decimal seq_no = 0;
            try
            {
                int row_list = dsList.RowCount;

                for (int i = 0; i < row_list; i++)
                {
                    if (dsList.DATA[i].OPERATE_FLAG == 1)
                    {
                        sliptype_code = dsMain.DATA[0].SLIPTYPE_CODE;
                        member_no = dsMain.DATA[0].MEMBER_NO;
                        membgroup_code = dsMain.DATA[0].MEMBGROUP_CODE.Trim();
                        slip_date = dsMain.DATA[0].SLIP_DATE;
                        operate_date = dsMain.DATA[0].OPERATE_DATE;
                        //payout_amtnet = dsMain.DATA[0].PAYOUTNET_AMT;
                        moneytype_code = dsMain.DATA[0].MONEYTYPE_CODE;
                        expense_bank = dsMain.DATA[0].EXPENSE_BANK;
                        expense_branch = dsMain.DATA[0].EXPENSE_BRANCH;
                        expense_accid = dsMain.DATA[0].EXPENSE_ACCID;
                        tofrom_accid = dsMain.DATA[0].TOFROM_ACCID;

                        fundbalance = dsList.DATA[i].BALANCE;
                        fundmember_no = dsList.DATA[i].LOANCONTRACT_NO.Trim();

                        //dsMain.DATA[0].FUNDMEMBER_NO;
                        String sqlxseq = @"select loancontract_no , loantype_code from lncontmaster where coop_id = {0} and rtrim(ltrim(loancontract_no)) = {1}";
                        sqlxseq = WebUtil.SQLFormat(sqlxseq, state.SsCoopControl, fundmember_no);
                        ta = WebUtil.QuerySdt(sqlxseq);
                        if (ta.Next())
                        {
                            loantype_code = ta.GetString("loantype_code").Trim();
                            loancontract_no = ta.GetString("loancontract_no").Trim();
                        }

                        //payoutslip_no = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopControl, "SLSLIPPAYOUT");

                        ////insert slslippayout
//                        String sqlInsertpay = @"INSERT INTO SLSLIPPAYOUT 
//                        (COOP_ID, PAYOUTSLIP_NO, MEMCOOP_ID, MEMBER_NO, DOCUMENT_NO, 
//                        SLIPTYPE_CODE, SLIP_DATE, OPERATE_DATE, RCVFROMREQCONT_CODE, 
//                        PAYOUT_AMT, PAYOUTNET_AMT, MONEYTYPE_CODE, EXPENSE_BANK, 
//                        EXPENSE_BRANCH, EXPENSE_ACCID, TOFROM_ACCID, SLIP_STATUS, 
//                        MEMBGROUP_CODE, ENTRY_ID, ENTRY_DATE, ENTRY_BYCOOPID, 
//                        POSTTOVC_FLAG, POST_TOFIN , REF_DOCNO, SHRLONTYPE_CODE, LOANCONTRACT_NO) VALUES 
//                        ({0},{1},{2},{3},{4},
//                        {5},{6},{7},{8},
//                        {9},{10},{11},{12},
//                        {13},{14},{15},{16},
//                        {17},{18},{19},{20},
//                        {21},{22},{23},{24},{25})";
//                        sqlInsertpay = WebUtil.SQLFormat(sqlInsertpay, state.SsCoopControl, payoutslip_no, state.SsCoopControl, member_no, fundmember_no,
//                                sliptype_code, slip_date, operate_date, "FUD",
//                                fundbalance, fundbalance, moneytype_code, expense_bank,
//                                expense_branch, expense_accid, tofrom_accid, 1,
//                                membgroup_code, state.SsUsername, DateTime.Now, state.SsCoopId,
//                                0, 1, fundmember_no, loantype_code, loancontract_no);
//                        Sdt dt = WebUtil.QuerySdt(sqlInsertpay);

//                        String sqlInsertpaydet = "";
//                        //insert slslippayoutdet ถอนต้น
//                        sqlInsertpaydet = @"INSERT INTO SLSLIPPAYOUTDET 
//                        (COOP_ID, PAYOUTSLIP_NO, SLIPITEMTYPE_CODE, SEQ_NO, CONCOOP_ID, 
//                        SLIPITEM_DESC, ITEM_PAYAMT, BFSHRCONT_BALAMT, BFCONTLAW_STATUS ) VALUES 
//                        ({0},{1},{2},{3},{4},
//                        {5},{6},{7},{8})";
//                        sqlInsertpaydet = WebUtil.SQLFormat(sqlInsertpaydet, state.SsCoopControl, payoutslip_no, sliptype_code, 1, state.SsCoopControl,
//                                "ถอนกองทุน", fundbalance, 0, 0);
//                        Sdt dt5 = WebUtil.QuerySdt(sqlInsertpaydet);

                        payout_amtnet = fundbalance - fundbalance;
                        //tomy ยังไม่ใช้ที่นี้ไม่มีดอก
                        //รอบสองยิงรายการ fundcollstatement ยอดรายการจ่าย
                        seq_no = GetSeqFundstate(state.SsCoopControl, member_no);
                        table_fund = GetSeqFundTable(state.SsCoopControl, member_no);

                        if (table_fund == "fundcollmaster") { table_fundstm = "fundcollstatement"; }
                        else { table_fundstm = "fundcollstatement"; }

                        String sqlInsertfundstate = @"INSERT INTO " + table_fundstm + @" (MEMBER_NO, COOP_ID, SEQ_NO, ITEMTYPE_CODE, OPERATE_DATE, 
                        REFSLIP_NO, ITEMPAY_AMT, BALANCE, ENTRY_ID, ENTRY_DATE, loancontract_no, balance_forward, fund_status) VALUES 
                        ({0},{1},{2},{3},{4},
                        {5},{6},{7},{8},{9},{10},{11},{12})";
                        sqlInsertfundstate = WebUtil.SQLFormat(sqlInsertfundstate, member_no, state.SsCoopControl, seq_no, sliptype_code, operate_date,
                                payoutslip_no, fundbalance, payout_amtnet, state.SsUsername, DateTime.Now, loancontract_no, fundbalance, 1);
                        Sdt dt3 = WebUtil.QuerySdt(sqlInsertfundstate);

                        //update fundcollmaster
                        String sqlUpfundmast = @"update " + table_fund + @" set fundbalance = {2}, laststm_no = {3}, fund_status = {4}, lastaccess_date = {5}, RESIGN_DATE = {6}
                        where coop_id = {0} and rtrim(ltrim(member_no)) = {1}";
                        sqlUpfundmast = WebUtil.SQLFormat(sqlUpfundmast, state.SsCoopControl, member_no, payout_amtnet, seq_no, -1, operate_date, operate_date);
                        Sdt dt4 = WebUtil.QuerySdt(sqlUpfundmast);

                        #region FIN

                        ls_slipno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "FNRECEIVENO");

                        String sqlStr = @"    INSERT INTO FINSLIP  
                    (		SLIP_NO,			ENTRY_ID,				ENTRY_DATE,				OPERATE_DATE,   
                            FROM_SYSTEM,		PAYMENT_STATUS,		    CASH_TYPE,				PAYMENT_DESC,   
                            MEMBER_NO,			PAY_RECV_STATUS,		ITEMPAY_AMT,			PAY_TOWHOM,   
                            MEMBER_FLAG,		NONMEMBER_DETAIL,	    coop_id,				MACHINE_ID,   
                            TOFROM_ACCID,		ACCOUNT_NO,				ITEMPAYTYPE_CODE,       REF_SYSTEM ,
                            ITEM_AMTNET  
                    )  
                    VALUES
                    ( 		{0},   			    {1},					{2},				    {2},
                            'FIN',		        8,			            'CSH',					'จ่ายคืนกองทุน',
                            {3},		        0,		                {4},					{5},
                            1,   			    {5},			        {6},					{7},
                            {8},			    {8},				    'FUN',				    'LON',
                            {4}
                    ) ";
                        sqlStr = WebUtil.SQLFormat(sqlStr, ls_slipno, state.SsUsername, operate_date
                            , member_no, fundbalance, dsMain.DATA[0].fullname, state.SsCoopId, state.SsClientIp
                            , tofrom_accid);
                        Sdt dtfin = WebUtil.QuerySdt(sqlStr);

                        String sqlStr2 = @"    INSERT INTO FINSLIPDET  
                    (		COOP_ID,			SLIP_NO,				SEQ_NO,				SLIPITEMTYPE_CODE,   
                            SLIPITEM_DESC,		SLIPITEM_STATUS,		ITEMPAY_AMT,		ACCOUNT_ID,   
                            ITEMPAYAMT_NET,		OPERATE_FLAG 
                    )  
                    VALUES
                    ( 		{0},   			    {1},					1,				    {2},
                            {3},	            1,			            {4},				{2},
                            {4},		        1
                    ) ";
                        sqlStr2 = WebUtil.SQLFormat(sqlStr2, state.SsCoopId, ls_slipno, tofrom_accid
                            , dsList.DATA[i].SLIPITEM_DESC, fundbalance);
                        Sdt dtfindet = WebUtil.QuerySdt(sqlStr2);

                        #endregion
                    }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกการจ่ายเงินกองทุนเลขที่ " + fundmember_no + " สำเร็จ");
                SetDefault();
                dsList.ResetRow();
                dsMain.ResetRow();
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }


        }

        public void WebSheetLoadEnd()
        {

        }

        #region AllFunction
        private void PostMember()
        {
            string memberNo = "";
            int check_flag = 0;
            decimal itempay_amt = 0;
            DateTime calint_date = new DateTime();
            memberNo = dsMain.DATA[0].MEMBER_NO;
            memberNo = WebUtil.MemberNoFormat(memberNo);
            dsMain.DATA[0].MEMBER_NO = memberNo.Trim();
            calint_date = dsMain.DATA[0].SLIP_DATE;
            try
            {
                dsMain.Retrieve(memberNo);
                int row2 = dsMain.RowCount;
                if (row2 <= 0)
                {
                    LtServerMessage.Text = WebUtil.WarningMessage2("ไม่มีข้อมูลกองทุน กรุณาตรวจสอบ สมาชิกเลขที่ " + memberNo);
                    dsList.ResetRow();
                }
                SetDefault();
                check_flag = 1; //ไม่เช็คเงื่อนไขใดๆๆ
                if (check_flag == 1)
                {
                    dsList.Retrieve(memberNo);
                    int row3 = dsList.RowCount;
                    if (row3 <= 0)
                    {
                        LtServerMessage.Text = WebUtil.WarningMessage2("ไม่มีข้อมูลกองทุน กรุณาตรวจสอบ สมาชิกเลขที่ " + memberNo);
                        //dsList.ResetRow();
                    }
                    else
                    {
                        dsList.DATA[0].ITEMPAY_AMT = itempay_amt;
                        dsMain.DATA[0].PAYOUTNET_AMT = itempay_amt;
                    }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.WarningMessage2("ยังไม่สามารถจ่ายกองทุนได้ กรุณาตรวจสอบเงื่อนไขสมาชิกเลขที่ " + memberNo);
                }
            }
            catch (Exception ex)
            { LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message); }
        }

        public void SetDefault()
        {
            dsMain.DATA[0].SLIP_DATE = state.SsWorkDate;
            dsMain.DATA[0].OPERATE_DATE = state.SsWorkDate;
            dsMain.DATA[0].DOCUMENT_NO = "AUTO";
            dsMain.DATA[0].SLIPTYPE_CODE = "FRT";
            dsMain.DATA[0].MONEYTYPE_CODE = "CSH";
            dsMain.DdMoneytype();
            dsMain.DdTofromaccid(dsMain.DATA[0].MONEYTYPE_CODE);
        }

        private decimal GetSeqFundstate(string coop_id, string fundmembno)
        {
            decimal maxseq_no = 0;
            String sqlgetmaxseq = @"select laststm_no from fundcollmaster where coop_id = {0} and rtrim(ltrim(member_no)) = {1}";
            sqlgetmaxseq = WebUtil.SQLFormat(sqlgetmaxseq, coop_id, fundmembno);
            ta = WebUtil.QuerySdt(sqlgetmaxseq);
            if (ta.Next())
            {
                maxseq_no = ta.GetDecimal("laststm_no");
            }
            maxseq_no++;
            return maxseq_no;
        }

        private string GetSeqFundTable(string coop_id, string fundmembno)
        {
            string table_no = "";
            String sqlgetmaxseq = @"select laststm_no from fundcollmaster where coop_id = {0} and rtrim(ltrim(member_no)) = {1}";
            sqlgetmaxseq = WebUtil.SQLFormat(sqlgetmaxseq, coop_id, fundmembno);
            ta = WebUtil.QuerySdt(sqlgetmaxseq);
            if (ta.Next())
            {
                table_no = "fundcollmaster";
            }

            return table_no;
        }


        private void SetDefaultTofromaccid()
        {
            string moneytypecode = dsMain.DATA[0].MONEYTYPE_CODE;
            dsMain.DdTofromaccid(moneytypecode);
        }
      
        #endregion
    }
}