$PBExportHeader$n_cst_loansrv_atm.sru
forward
global type n_cst_loansrv_atm from NonVisualObject
end type
end forward

global type n_cst_loansrv_atm from NonVisualObject
end type
global n_cst_loansrv_atm n_cst_loansrv_atm

type variables
transaction	itr_sqlca
Exception	iex_exception

string		is_coopcontrol, is_coopid

n_cst_dbconnectservice		inv_transection
n_cst_doccontrolservice		inv_docsrv
n_cst_loansrv_interest		inv_intsrv
n_cst_loansrv_lnoperate		inv_lnoperate

constant string	DWO_PAYOUTSLIP		= "d_loansrv_lnpayout_slip"
constant string	DWO_PAYINSLIP 		= "d_loansrv_lnpayin_slip"
constant string	DWO_PAYINSLIPDET	= "d_loansrv_lnpayin_slipdet"
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception
private function integer of_setsrvdoccontrol (boolean ab_switch)
private function integer of_setsrvinterest (boolean ab_switch)
private function integer of_operate (ref str_lnatm astr_lnatm)
private function integer of_operate_chkbal (ref str_lnatm astr_lnatm)
private function integer of_operate_lnrcv (ref str_lnatm astr_lnatm)
private function integer of_operate_payin (ref str_lnatm astr_lnatm)
public function integer of_setsrvlnoperate (boolean ab_switch) throws Exception
public function integer of_atmtrans (ref str_lnatm astr_lnatm)
private function integer of_cclslip (ref str_lnatm astr_lnatm)
private function integer of_cclslip_payin (ref str_lnatm astr_lnatm)
private function integer of_cclslip_lnrcv (ref str_lnatm astr_lnatm)
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception;// Service Transection
if isnull( inv_transection ) or not isvalid( inv_transection ) then
	inv_transection	= create n_cst_dbconnectservice
end if

inv_transection	= anv_dbtrans
itr_sqlca 		= inv_transection.itr_dbconnection
is_coopcontrol	= inv_transection.is_coopcontrol
is_coopid		= inv_transection.is_coopid

return 1
end function

private function integer of_setsrvdoccontrol (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_docsrv ) or not isvalid( inv_docsrv ) then
		inv_docsrv	= create n_cst_doccontrolservice
		inv_docsrv.of_settrans( inv_transection )
		return 1
	end if
else
	if isvalid( inv_docsrv ) then
		destroy inv_docsrv
		return 1
	end if
end if

return 0
end function

private function integer of_setsrvinterest (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_intsrv ) or not isvalid( inv_intsrv ) then
		inv_intsrv	= create n_cst_loansrv_interest
		inv_intsrv.of_initservice( inv_transection )
		return 1
	end if
else
	if isvalid( inv_intsrv ) then
		destroy inv_intsrv
		return 1
	end if
end if

return 0
end function

private function integer of_operate (ref str_lnatm astr_lnatm);string		ls_slipno

// ถ้าเป็นการสอบถามข้อมูล
if astr_lnatm.action_status = 0 then
	this.of_operate_chkbal( astr_lnatm )
	return 1
end if

// ตรวจสอบว่่าเป็นรายการอะไร
if astr_lnatm.operate_cd = "002" then
	// 002=กู้
	this.of_operate_lnrcv( astr_lnatm )
elseif astr_lnatm.operate_cd = "003" then
	// 003=ชำระ
	this.of_operate_payin( astr_lnatm )
else
	// ไม่รู้ว่าเป็นรายการอะไร
end if

return 1
end function

private function integer of_operate_chkbal (ref str_lnatm astr_lnatm);string	ls_sql, ls_contno
decimal ldc_apvamt, ldc_wtdamt, ldc_prnbal

ls_contno	= trim( astr_lnatm.loancontract_no )

ls_sql	= " select loanapprove_amt, withdrawable_amt, principal_balance "
ls_sql	+= " from lncontmaster "
ls_sql	+= " where coop_id = '"+is_coopcontrol+"'"
ls_sql	+= " and loancontract_no = '"+ls_contno+"'"

declare data_cur dynamic cursor for sqlsa;
prepare sqlsa from :ls_sql using itr_sqlca;

open dynamic data_cur ;

fetch data_cur into :ldc_apvamt, :ldc_wtdamt, :ldc_prnbal ;

if itr_sqlca.SQLCode <> 0 then
	astr_lnatm.msg_status = "0012"
	return 0
end if

astr_lnatm.msg_status		= "0000"
astr_lnatm.withdrawable_amt	= Round( ldc_wtdamt, 2 )
astr_lnatm.principal_amt	= Round( ldc_prnbal, 2 )
astr_lnatm.approve_amt		= Round( ldc_apvamt, 2 )

close data_cur;

return 1
end function

private function integer of_operate_lnrcv (ref str_lnatm astr_lnatm);string	ls_sql, ls_slipno, ls_contno, ls_memno, ls_lntype, ls_refatmno, ls_mbgrp, ls_tofromaccid
integer	li_lastrcv, li_inttype, li_paysts, li_contlaw, li_rcvperiod
long	ll_row
decimal	ldc_apvamt, ldc_bfprnbal, ldc_bfwtdamt, ldc_bfintarr, ldc_rcvamt, ldc_prnbal, ldc_wtdamt, ldc_intarr
decimal	ldc_prncalint, ldc_intperiod
datetime	ldtm_lastcal, ldtm_lastrcv, ldtm_lastproc, ldtm_opedate
datetime	ldtm_calintfrom, ldtm_calintto
boolean	lb_error = false
n_ds	lds_slipout
str_poststmloan	lstr_lnstm

ls_contno		= Trim( astr_lnatm.loancontract_no )
ls_refatmno		= Trim( astr_lnatm.atm_no ) + Trim( astr_lnatm.atm_seqno )
ldtm_opedate	= datetime( date( astr_lnatm.operate_date ) )
ldc_rcvamt		= astr_lnatm.item_amt

ls_sql	= " select	ln.member_no, ln.loantype_code, ln.last_periodrcv, "
ls_sql	+= "		ln.int_continttype, ln.loanapprove_amt, ln.principal_balance, ln.withdrawable_amt, ln.payment_status, "
ls_sql	+= "		ln.lastcalint_date, ln.lastreceive_date, ln.lastprocess_date, ln.interest_arrear, ln.contlaw_status, "
ls_sql	+= "		mb.membgroup_code "
ls_sql	+= " from	lncontmaster ln join mbmembmaster mb on ln.member_no = mb.member_no"
ls_sql	+= " where	ln.coop_id = '"+is_coopcontrol+"' "
ls_sql	+= " and	ln.loancontract_no = '"+ls_contno+"'"

declare data_cur dynamic cursor for sqlsa;
prepare sqlsa from :ls_sql using itr_sqlca;

open dynamic data_cur ;

fetch data_cur into :ls_memno, :ls_lntype, :li_lastrcv, :li_inttype, :ldc_apvamt, :ldc_bfprnbal, :ldc_bfwtdamt, :li_paysts,
					:ldtm_lastcal, :ldtm_lastrcv, :ldtm_lastproc, :ldc_bfintarr, :li_contlaw, :ls_mbgrp ;

if itr_sqlca.SQLCode <> 0 then
	astr_lnatm.msg_status = "0011"
	return 0
end if

close data_cur ;

ldc_wtdamt	= ldc_apvamt - ldc_bfprnbal

// ยอดไม่พอ
if ldc_wtdamt < ldc_rcvamt then
	astr_lnatm.msg_status		= "0012"
	astr_lnatm.withdrawable_amt	= ldc_wtdamt
	astr_lnatm.principal_amt	= ldc_bfprnbal
	astr_lnatm.approve_amt		= ldc_apvamt
	
	return 0
end if

// สร้าง ds ไว้สำหรับบันทึกและผ่านรายการ
lds_slipout	= create n_ds
lds_slipout.DataObject	= DWO_PAYOUTSLIP
lds_slipout.SetTransObject( itr_sqlca )

try
	// ดึงเลข Slip
	this.of_setsrvdoccontrol(true)
	ls_slipno	= inv_docsrv.of_getnewdocno( is_coopcontrol, "SLSLIPPAYOUTATM")
	this.of_setsrvdoccontrol(false)
catch( Exception lex_docerr )
	lb_error			= true
	iex_exception.Text	= lex_docerr.Text
end try

if lb_error then
	goto objdestroy
end if

li_rcvperiod	= li_lastrcv + 1

if is_coopcontrol = "040001" then
	ls_tofromaccid = "10010200"
end if

// เพิ่มแถวใน ds payout
ll_row		= lds_slipout.insertrow( 0 )

lds_slipout.setitem( ll_row, "coop_id", is_coopcontrol )
lds_slipout.setitem( ll_row, "payoutslip_no", ls_slipno )
lds_slipout.setitem( ll_row, "member_no", ls_memno )
lds_slipout.setitem( ll_row, "memcoop_id", is_coopcontrol )
lds_slipout.setitem( ll_row, "membgroup_code", ls_mbgrp )
lds_slipout.setitem( ll_row, "sliptype_code", "LWD" )
lds_slipout.setitem( ll_row, "concoop_id", is_coopcontrol )
lds_slipout.setitem( ll_row, "loancontract_no", ls_contno )
lds_slipout.setitem( ll_row, "rcv_period", li_rcvperiod )
lds_slipout.setitem( ll_row, "rcvperiod_flag", 0 )
lds_slipout.setitem( ll_row, "rcvfromreqcont_code", "CON" )
lds_slipout.setitem( ll_row, "slip_date", ldtm_opedate )
lds_slipout.setitem( ll_row, "operate_date", ldtm_opedate )
lds_slipout.setitem( ll_row, "lnpayment_status", 1 )
lds_slipout.setitem( ll_row, "entry_id", astr_lnatm.entry_id )
lds_slipout.setitem( ll_row, "entry_date", astr_lnatm.operate_date )
lds_slipout.setitem( ll_row, "entry_bycoopid", is_coopid )
lds_slipout.setitem( ll_row, "slip_status", 1 )
lds_slipout.setitem( ll_row, "check_code", ls_refatmno )

lds_slipout.setitem( ll_row, "shrlontype_code", ls_lntype )
lds_slipout.setitem( ll_row, "bfcontint_type", li_inttype )
lds_slipout.setitem( ll_row, "bfperiod", li_lastrcv )
lds_slipout.setitem( ll_row, "bfloanapprove_amt", ldc_apvamt )
lds_slipout.setitem( ll_row, "bfshrcont_balamt", ldc_bfprnbal )
lds_slipout.setitem( ll_row, "bfwithdraw_amt", ldc_bfwtdamt )
lds_slipout.setitem( ll_row, "bfpayment_status", li_paysts )
lds_slipout.setitem( ll_row, "bflastcalint_date", ldtm_lastcal )
lds_slipout.setitem( ll_row, "bflastreceive_date", ldtm_lastrcv )
lds_slipout.setitem( ll_row, "bflastproc_date", ldtm_lastproc )
lds_slipout.setitem( ll_row, "bfinterest_arrear", ldc_bfintarr )
lds_slipout.setitem( ll_row, "bfcontlaw_status", li_contlaw )
lds_slipout.setitem( ll_row, "payout_amt", ldc_rcvamt )
lds_slipout.setitem( ll_row, "payoutclr_amt", 0 )
lds_slipout.setitem( ll_row, "payoutnet_amt", ldc_rcvamt )
lds_slipout.setitem( ll_row, "moneytype_code", astr_lnatm.moneytype_code )
lds_slipout.setitem( ll_row, "expense_bank", astr_lnatm.bank_cd )
lds_slipout.setitem( ll_row, "expense_branch", astr_lnatm.branch_cd )
lds_slipout.setitem( ll_row, "expense_accid", astr_lnatm.bank_accid )

lds_slipout.setitem( ll_row, "interest_period", 0 )
lds_slipout.setitem( ll_row, "prncalint_amt", 0 )
lds_slipout.setitem( ll_row, "calint_from", ldtm_opedate )
lds_slipout.setitem( ll_row, "calint_to", ldtm_opedate )
lds_slipout.setitem( ll_row, "tofrom_accid", ls_tofromaccid )

if ldc_bfprnbal > 0 then
	// เบิกเงินกู้ก่อนประมวลผล
	if ldtm_opedate > ldtm_lastproc then
		ldc_prncalint		= ldc_bfprnbal
		ldtm_calintfrom		= ldtm_lastcal
		ldtm_calintto		= ldtm_opedate
	else
		ldc_prncalint		= ldc_rcvamt
		ldtm_calintfrom		= ldtm_opedate
		ldtm_calintto		= ldtm_lastproc
	end if
	
	try
		this.of_setsrvinterest(true)
		ldc_intperiod	= inv_intsrv.of_computeinterest( is_coopcontrol, ls_contno, ldc_prncalint, ldtm_calintfrom, ldtm_calintto )	
		this.of_setsrvinterest(false)
	catch( exception lex_err )
		lb_error	= true
		iex_exception.Text	= lex_err.Text
	end try

	if lb_error then
		goto objdestroy
	end if
	
	if ldc_intperiod <= 0 then
		ldc_intperiod = 0
	end if
	
	lds_slipout.setitem( ll_row, "interest_period", ldc_intperiod )
	lds_slipout.setitem( ll_row, "prncalint_amt", ldc_prncalint )
	lds_slipout.setitem( ll_row, "calint_from", ldtm_calintfrom )
	lds_slipout.setitem( ll_row, "calint_to", ldtm_calintto )
	
end if

// บันทึก slip
if lds_slipout.Update() <> 1 then
	iex_exception.text = "บันทึกข้อมูล Slip Pay Out ไม่ได้"
	iex_exception.text += lds_slipout.of_geterrormessage()
	lb_error = true
	goto objdestroy
end if

// post slip
try
	this.of_setsrvlnoperate( true )
	inv_lnoperate.of_postslip( "LWD", lds_slipout, lds_slipout, is_coopcontrol )
	this.of_setsrvlnoperate( false )
catch( exception lex_errpost )
	lb_error			= true
	iex_exception.Text	= lex_errpost.Text
end try

if lb_error then
	goto objdestroy
end if

// เพิ่มลดยอดสำหรับส่งกลับ ATM
ldc_prnbal	= ldc_bfprnbal + ldc_rcvamt
ldc_wtdamt	= ldc_apvamt - ldc_prnbal
ldc_intarr	= ldc_bfintarr + ldc_intperiod

if ldc_wtdamt < 0 then
	ldc_wtdamt	= 0
end if

objdestroy:
if isvalid( lds_slipout ) then destroy lds_slipout

if lb_error then
	rollback using itr_sqlca ;
	astr_lnatm.msg_status	= "0099"
	astr_lnatm.msg_output	= iex_exception.Text
	
	return 0
end if

commit using sqlca ;

astr_lnatm.msg_status		= "0000"
astr_lnatm.ref_slipno		= ls_slipno
astr_lnatm.withdrawable_amt	= ldc_wtdamt
astr_lnatm.principal_amt	= ldc_prnbal
astr_lnatm.approve_amt		= ldc_apvamt

return 1
end function

private function integer of_operate_payin (ref str_lnatm astr_lnatm);string	ls_slipno, ls_receiptno, ls_contno, ls_memno, ls_mbgrp, ls_lntype, ls_sql
string	ls_refatmno
long	ll_row, ll_rwdet
integer	li_lastpay, li_contsts, li_contlaw, li_paymeth, li_intretsts, li_pxmthkeep
decimal	ldc_apvamt, ldc_bfwtdamt, ldc_bfprnbal, ldc_bfintarr, ldc_bfintyear, ldc_periodpay, ldc_intperiod
decimal	ldc_rkeepprn, ldc_rkeepint, ldc_nkeepint, ldc_payamt, ldc_intaccum, ldc_shrstkval, ldc_shrstkbf
decimal	ldc_intall, ldc_intpay, ldc_prnpay, ldc_prncalint, ldc_intreturn, ldc_prnbal, ldc_wtdamt
decimal	ldc_intarrbal, ldc_bfintret, ldc_intretbal
boolean	lb_error = false
n_ds	lds_payin, lds_payindet
datetime	ldtm_lastcalint, ldtm_lastproc, ldtm_opedate, ldtm_calfrom, ldtm_calto

ls_contno		= Trim( astr_lnatm.loancontract_no )
ls_refatmno		= Trim( astr_lnatm.atm_no ) + Trim( astr_lnatm.atm_seqno )
ldtm_opedate	= datetime( date( astr_lnatm.operate_date ) )

ldc_payamt		= astr_lnatm.item_amt

ls_sql	= " select	ln.member_no, ln.loantype_code, ln.loanapprove_amt, ln.withdrawable_amt, ln.principal_balance, "
ls_sql	+= "		ln.interest_arrear, ln.intyear_arrear, ln.interest_return, "
ls_sql	+= "		ln.last_periodpay, ln.period_payment, ln.lastcalint_date, ln.lastprocess_date, ln.contract_status, ln.contlaw_status, "
ls_sql	+= "		ln.rkeep_principal, ln.rkeep_interest, ln.nkeep_interest, "
ls_sql	+= "		lt.payspec_method, lt.intreturn_flag,  lt.pxaftermthkeep_type, mb.membgroup_code, mb.accum_interest, "
ls_sql	+= "		sh.sharestk_amt * st.unitshare_value as sharestk_value,  sh.sharebegin_amt * st.unitshare_value as sharebf_value "
ls_sql	+= " from	lncontmaster ln "
ls_sql	+= " 		join lnloantype lt on ln.loantype_code = lt.loantype_code "
ls_sql	+= " 		join mbmembmaster mb on ln.member_no = mb.member_no "
ls_sql	+= " 		join shsharemaster sh on ln.member_no = sh.member_no "
ls_sql	+= " 		join shsharetype st on sh.sharetype_code = st.sharetype_code "
ls_sql	+= " where	( ln.coop_id = '"+is_coopcontrol+"' ) "
ls_sql	+= " and	( ln.loancontract_no = '"+ls_contno+"' ) "

declare data_cur dynamic cursor for sqlsa;
prepare sqlsa from :ls_sql using itr_sqlca;

open dynamic data_cur ;
fetch data_cur 
into	:ls_memno, :ls_lntype, :ldc_apvamt, :ldc_bfwtdamt, :ldc_bfprnbal, :ldc_bfintarr, :ldc_bfintyear, :ldc_bfintret,
		:li_lastpay, :ldc_periodpay, :ldtm_lastcalint, :ldtm_lastproc, :li_contsts, :li_contlaw,
		:ldc_rkeepprn, :ldc_rkeepint, :ldc_nkeepint,
		:li_paymeth, :li_intretsts, :li_pxmthkeep, :ls_mbgrp, :ldc_intaccum, :ldc_shrstkval, :ldc_shrstkbf ;

if itr_sqlca.SQLCode <> 0 then
	astr_lnatm.msg_status = "0011"
	return 0
end if
close data_cur ;

// Slip สำหรับบันทึก
lds_payin	= create n_ds
lds_payin.dataobject	= DWO_PAYINSLIP
lds_payin.settransobject( itr_sqlca )

lds_payindet	= create n_ds
lds_payindet.dataobject	= DWO_PAYINSLIPDET
lds_payindet.settransobject( itr_sqlca )

this.of_setsrvinterest( true )

try
	// ดึงเลข Slip
	this.of_setsrvdoccontrol(true)
	ls_slipno		= inv_docsrv.of_getnewdocno( is_coopcontrol, "SLSLIPPAYINATM")
	ls_receiptno	= inv_docsrv.of_getnewdocno( is_coopcontrol, "SLRECEIPTNOATM")
	this.of_setsrvdoccontrol(false)
catch( Exception lex_docerr )
	lb_error			= true
	iex_exception.Text	= lex_docerr.Text
end try

if lb_error then
	goto objdestroy
end if

ll_row	= lds_payin.InsertRow( 0 )

lds_payin.setitem( ll_row, "coop_id", is_coopcontrol )
lds_payin.setitem( ll_row, "payinslip_no", ls_slipno )
lds_payin.setitem( ll_row, "memcoop_id", is_coopcontrol )
lds_payin.setitem( ll_row, "member_no", ls_memno )
lds_payin.setitem( ll_row, "document_no", ls_receiptno )
lds_payin.setitem( ll_row, "sliptype_code", "PX" )
lds_payin.setitem( ll_row, "slip_date", ldtm_opedate )
lds_payin.setitem( ll_row, "slip_amt", ldc_payamt )
lds_payin.setitem( ll_row, "operate_date", ldtm_opedate )
lds_payin.setitem( ll_row, "moneytype_code", astr_lnatm.moneytype_code )
lds_payin.setitem( ll_row, "sharestkbf_value", ldc_shrstkbf )
lds_payin.setitem( ll_row, "sharestk_value", ldc_shrstkval )
lds_payin.setitem( ll_row, "intaccum_amt", ldc_intaccum )

lds_payin.setitem( ll_row, "ref_system", "ATM" )
lds_payin.setitem( ll_row, "ref_slipno", ls_refatmno )
lds_payin.setitem( ll_row, "ref_slipamt", ldc_payamt )

//lds_payin.setitem( ll_row, "tofrom_accid", ls_tofromaccid )

lds_payin.setitem( ll_row, "membgroup_code", ls_mbgrp )
lds_payin.setitem( ll_row, "entry_id", astr_lnatm.entry_id )
lds_payin.setitem( ll_row, "entry_date", astr_lnatm.operate_date )

// ส่วนรายการ
ll_rwdet	= lds_payindet.insertrow( 0 )

lds_payindet.setitem( ll_rwdet, "coop_id", is_coopcontrol )
lds_payindet.setitem( ll_rwdet, "payinslip_no", ls_slipno )
lds_payindet.setitem( ll_rwdet, "seq_no", 1 )
lds_payindet.SetItem( ll_rwdet, "shrlontype_code", ls_lntype )
lds_payindet.SetItem( ll_rwdet, "rkeep_principal", ldc_rkeepprn )
lds_payindet.SetItem( ll_rwdet, "rkeep_interest", ldc_rkeepint )
lds_payindet.SetItem( ll_rwdet, "nkeep_interest", ldc_nkeepint )
lds_payindet.SetItem( ll_rwdet, "period", li_lastpay )
lds_payindet.SetItem( ll_rwdet, "bfperiod", li_lastpay )
lds_payindet.SetItem( ll_rwdet, "bfintarr_amt", ldc_bfintarr )
lds_payindet.SetItem( ll_rwdet, "bfintarrset_amt", ldc_bfintyear )
lds_payindet.setitem( ll_rwdet, "bfshrcont_balamt", ldc_bfprnbal )
lds_payindet.SetItem( ll_rwdet, "bfwithdraw_amt", ldc_bfwtdamt )
lds_payindet.SetItem( ll_rwdet, "bfperiod_payment", ldc_periodpay )
lds_payindet.SetItem( ll_rwdet, "bfshrcont_status", li_contsts )
lds_payindet.SetItem( ll_rwdet, "bfcontlaw_status", li_contlaw )
lds_payindet.SetItem( ll_rwdet, "bflastcalint_date", ldtm_lastcalint )
lds_payindet.SetItem( ll_rwdet, "bflastproc_date", ldtm_lastproc )
lds_payindet.SetItem( ll_rwdet, "bfcountpay_flag", 1 )
lds_payindet.SetItem( ll_rwdet, "bfpayspec_method", li_paymeth )
lds_payindet.SetItem( ll_rwdet, "bfintreturn_flag", li_intretsts )
lds_payindet.SetItem( ll_rwdet, "bfpxaftermthkeep_type", li_pxmthkeep )

lds_payindet.setitem( ll_rwdet, "stm_itemtype", "LPX" )

lds_payindet.setitem( ll_rwdet, "operate_flag", 1 )
lds_payindet.setitem( ll_rwdet, "slipitemtype_code", "LON" )
lds_payindet.setitem( ll_rwdet, "slipitem_desc", "ชำระพิเศษ" )
lds_payindet.setitem( ll_rwdet, "concoop_id", is_coopcontrol )
lds_payindet.setitem( ll_rwdet, "loancontract_no", ls_contno )

// ตรวจว่าเป็นพวกที่มีการเรียกเก็บไปแล้วและเงื่อนไขการชำระหลังเรียกเก็บเป็นไม่คิดดอกเบี้ย
if ( ldc_rkeepprn > 0 or ldc_rkeepint > 0 ) and li_pxmthkeep = 1 then
	ldc_intperiod	= 0
	
	// ให้คำนวณดอกเบี้ยคืน
	try
		ldc_intreturn	= inv_intsrv.of_computeinterest( is_coopcontrol, ls_contno, ldc_payamt, ldtm_opedate, ldtm_lastproc )
	catch( exception lex_errcalret )
		iex_exception.text	= lex_errcal.text
		lb_error = true
	end try
	if lb_error then goto objdestroy
	
	lds_payindet.setitem( ll_rwdet, "prncalint_amt", ldc_payamt )
	lds_payindet.setitem( ll_rwdet, "calint_from", ldtm_opedate )
	lds_payindet.setitem( ll_rwdet, "calint_to", ldtm_lastproc )
	lds_payindet.setitem( ll_rwdet, "interest_period", ldc_intperiod )
	lds_payindet.setitem( ll_rwdet, "interest_return", ldc_intreturn )
else
	ldc_intreturn	= 0
	// คิด ด/บ
	try
		ldc_intperiod	= inv_intsrv.of_computeinterest( is_coopcontrol, ls_contno, ldtm_opedate )
	catch( exception lex_errcal )
		iex_exception.text	= lex_errcal.text
		lb_error = true
	end try
	if lb_error then goto objdestroy
	
	lds_payindet.setitem( ll_rwdet, "prncalint_amt", ldc_bfprnbal )
	lds_payindet.setitem( ll_rwdet, "calint_from", ldtm_calfrom )
	lds_payindet.setitem( ll_rwdet, "calint_to", ldtm_opedate )
	lds_payindet.setitem( ll_rwdet, "interest_period", ldc_intperiod )
	lds_payindet.setitem( ll_rwdet, "interest_return", ldc_intreturn )
end if

// ชำระดูจากยอดรวมหรือเปล่า
ldc_intall	= ( ldc_intperiod + ldc_bfintarr )

if ldc_intall >= ldc_payamt then
	ldc_intpay	= ldc_payamt
else
	ldc_intpay	= ldc_intall
end if

ldc_prnpay	= ldc_payamt - ldc_intpay
			
lds_payindet.setitem( ll_rwdet, "principal_payamt", ldc_prnpay )
lds_payindet.setitem( ll_rwdet, "interest_payamt", ldc_intpay )
lds_payindet.setitem( ll_rwdet, "item_payamt", ldc_payamt )
lds_payindet.setitem( ll_rwdet, "item_balance", ldc_bfprnbal - ldc_prnpay )

// บันทึก Slip
if lds_payin.update () <> 1 then
	iex_exception.text = "บันทึกข้อมูล Slip ไม่ได้"
	iex_exception.text += lds_payin.of_geterrormessage()
	lb_error	= true
	goto objdestroy
end if

// บันทึก Slip Detail
if lds_payindet. update () <> 1 then
	iex_exception.text = "บันทึกข้อมูล SlipDet ไม่ได้"
	iex_exception.text += lds_payindet.of_geterrormessage()
	lb_error	= true
	goto objdestroy
end if

// post slip
try
	this.of_setsrvlnoperate( true )
	inv_lnoperate.of_postslip( "PX", lds_payin, lds_payindet, is_coopcontrol )
	this.of_setsrvlnoperate( false )
catch( exception lex_errpost )
	lb_error			= true
	iex_exception.Text	= lex_errpost.Text
end try

if lb_error then
	goto objdestroy
end if

// ปรับค่าคงเหลือสำหรับส่งกลับ ATM
ldc_prnbal	= ldc_bfprnbal - ldc_prnpay
ldc_wtdamt	= ldc_apvamt - ldc_prnbal

if ldc_wtdamt < 0 then ldc_wtdamt = 0

objdestroy:
if isvalid( lds_payin ) then destroy lds_payin
if isvalid( lds_payindet ) then destroy lds_payindet
this.of_setsrvinterest( false )

if lb_error then
	rollback using itr_sqlca ;
	astr_lnatm.msg_status	= "0099"
	astr_lnatm.msg_output	= iex_exception.Text
	
	return 0
end if

commit using sqlca ;

astr_lnatm.msg_status		= "0000"
astr_lnatm.ref_slipno		= ls_slipno
astr_lnatm.withdrawable_amt	= ldc_wtdamt
astr_lnatm.principal_amt	= ldc_prnbal
astr_lnatm.approve_amt		= ldc_apvamt

return 1
end function

public function integer of_setsrvlnoperate (boolean ab_switch) throws Exception;// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_lnoperate ) or not isvalid( inv_lnoperate ) then
		inv_lnoperate	= create n_cst_loansrv_lnoperate
		inv_lnoperate.of_initservice( inv_transection )
		return 1
	end if
else
	if isvalid( inv_lnoperate ) then
		destroy inv_lnoperate
		return 1
	end if
end if

return 0
end function

public function integer of_atmtrans (ref str_lnatm astr_lnatm);string		ls_slipno
integer		li_opetype

// สถานะการทำรายการ 1=ทำรายการ -1=ยกเลิกรายการ
li_opetype	= astr_lnatm.post_status

if li_opetype = 1 then
	this.of_operate( astr_lnatm )
else
	this.of_cclslip( astr_lnatm )
end if

return 1
end function

private function integer of_cclslip (ref str_lnatm astr_lnatm);// ตรวจสอบว่่าเป็นรายการอะไร
if astr_lnatm.operate_cd = "002" then
	// 002=กู้
	this.of_cclslip_lnrcv( astr_lnatm )
elseif astr_lnatm.operate_cd = "003" then
	// 003=ชำระ
	this.of_cclslip_payin( astr_lnatm )
else
	// ไม่รู้ว่าเป็นรายการอะไร
end if

return 1
end function

private function integer of_cclslip_payin (ref str_lnatm astr_lnatm);string		ls_memno, ls_refatmno, ls_slipno
datetime	ldtm_opedate
str_slipcancel	lstr_slipccl

ls_memno		= Trim( astr_lnatm.member_no )
ls_refatmno		= Trim( astr_lnatm.atm_no ) + Trim( astr_lnatm.atm_seqno )
ldtm_opedate	= datetime( date( astr_lnatm.operate_date ) )

select	payinslip_no
into	:ls_slipno
from	slslippayin
where	member_no = :ls_memno
and		ref_slipno = :ls_refatmno
using	itr_sqlca ;

try
	lstr_slipccl.slip_no		= ls_slipno
	lstr_slipccl.cancel_id		= astr_lnatm.entry_id
	lstr_slipccl.cancel_date	= ldtm_opedate
	
	this.of_setsrvlnoperate( true )
	inv_lnoperate.of_saveccl_payin( lstr_slipccl )
	this.of_setsrvlnoperate( false )
catch( exception lex_postccl )
	astr_lnatm.msg_status	= "0099"
	astr_lnatm.msg_output	= iex_exception.Text
	return 1
end try

astr_lnatm.msg_status		= "0000"
astr_lnatm.ref_slipno		= ls_slipno

return 1
end function

private function integer of_cclslip_lnrcv (ref str_lnatm astr_lnatm);string		ls_memno, ls_refatmno, ls_coopid, ls_slipno
datetime	ldtm_opedate
str_slipcancel	lstr_slipccl

ls_memno		= Trim( astr_lnatm.member_no )
ls_refatmno		= Trim( astr_lnatm.atm_no ) + Trim( astr_lnatm.atm_seqno )
ldtm_opedate	= datetime( date( astr_lnatm.operate_date ) )

select	coop_id, payoutslip_no
into	:ls_coopid, :ls_slipno
from	slslippayout
where	member_no = :ls_memno
and		check_code = :ls_refatmno
using	itr_sqlca ;

try
	lstr_slipccl.slipcoop_id		= ls_coopid
	lstr_slipccl.slip_no			= ls_slipno
	lstr_slipccl.cancel_id			= astr_lnatm.entry_id
	lstr_slipccl.cancel_date		= ldtm_opedate
	lstr_slipccl.operateccl_date	= ldtm_opedate
	
	this.of_setsrvlnoperate( true )
	inv_lnoperate.of_saveccl_lnrcv( lstr_slipccl )
	this.of_setsrvlnoperate( false )
catch( exception lex_postccl )
	astr_lnatm.msg_status	= "0099"
	astr_lnatm.msg_output	= iex_exception.Text
	return 1
end try

astr_lnatm.msg_status		= "0000"
astr_lnatm.ref_slipno		= ls_slipno

return 1
end function

on n_cst_loansrv_atm.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_loansrv_atm.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;// ประกาศ Throw สำหรับ Err
iex_exception	= create Exception
end event
