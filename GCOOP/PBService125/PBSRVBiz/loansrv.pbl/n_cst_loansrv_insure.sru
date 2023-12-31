$PBExportHeader$n_cst_loansrv_insure.sru
forward
global type n_cst_loansrv_insure from NonVisualObject
end type
end forward

global type n_cst_loansrv_insure from NonVisualObject
end type
global n_cst_loansrv_insure n_cst_loansrv_insure

type variables
transaction		itr_sqlca
Exception		ithw_exception

string		is_coopcontrol, is_coopid

n_cst_dbconnectservice		inv_transection
n_cst_dwxmlieservice		inv_dwxmliesrv
n_cst_datawindowsservice	inv_dwsrv
n_cst_stringservice			inv_stringsrv
n_cst_datetimeservice		inv_datetime
n_cst_doccontrolservice		inv_docsrv
end variables

forward prototypes
private function integer of_setsrvdwxmlie (boolean ab_switch)
private function integer of_setsrvdw (boolean ab_switch)
private function integer of_setsrvstring (boolean ab_switch)
private function integer of_parsetoarray (string as_source, ref string as_contclr[])
private function integer of_setsrvdatetime (boolean ab_switch)
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans)
public function integer of_setsrvdoccontrol (boolean ab_switch)
public function integer of_calinsurelnreq (ref str_lninsure astr_lninsure) throws Exception
end prototypes

private function integer of_setsrvdwxmlie (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_dwxmliesrv ) or not isvalid( inv_dwxmliesrv ) then
		inv_dwxmliesrv	= create n_cst_dwxmlieservice
		return 1
	end if
else
	if isvalid( inv_dwxmliesrv ) then
		destroy inv_dwxmliesrv
		return 1
	end if
end if

return 0
end function

private function integer of_setsrvdw (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_dwsrv ) or not isvalid( inv_dwsrv ) then
		inv_dwsrv	= create n_cst_datawindowsservice
		inv_dwsrv.of_initservice( inv_transection )
		return 1
	end if
else
	if isvalid( inv_dwsrv ) then
		destroy inv_dwsrv
		return 1
	end if
end if

return 0
end function

private function integer of_setsrvstring (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_stringsrv ) or not isvalid( inv_stringsrv ) then
		inv_stringsrv	= create n_cst_stringservice
		return 1
	end if
else
	if isvalid( inv_stringsrv ) then
		destroy inv_stringsrv
		return 1
	end if
end if

return 0
end function

private function integer of_parsetoarray (string as_source, ref string as_contclr[]);this.of_setsrvstring( true )
inv_stringsrv.of_parsetoarray( as_source, ",", as_contclr )
this.of_setsrvstring( false )

return 1
end function

private function integer of_setsrvdatetime (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_datetime ) or not isvalid( inv_datetime ) then
		inv_datetime	= create n_cst_datetimeservice
		return 1
	end if
else
	if isvalid( inv_datetime ) then
		destroy inv_datetime
		return 1
	end if
end if

return 0
end function

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans);// Service Transection
if isnull( inv_transection ) or not isvalid( inv_transection ) then
	inv_transection	= create n_cst_dbconnectservice
end if

inv_transection	= anv_dbtrans
itr_sqlca 			= inv_transection.itr_dbconnection
is_coopcontrol	= inv_transection.is_coopcontrol
is_coopid			= inv_transection.is_coopid

this.of_setsrvdwxmlie( true )

return 1
end function

public function integer of_setsrvdoccontrol (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull(inv_docsrv ) or not isvalid( inv_docsrv ) then
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

public function integer of_calinsurelnreq (ref str_lninsure astr_lninsure) throws Exception;string ls_memno, ls_instype, ls_contclr
string ls_sqlbal, ls_sqlins, ldc_sqlmain
integer li_inscaltype, li_dayinsmain, li_dayprot, li_insrecal
decimal ldc_stepprot, ldc_steprate, ldc_ratemin, ldc_ratemax, ldc_stepall
decimal ldc_sumprn, ldc_protcur, ldc_protall, ldc_protadd, ldc_lnexcept
decimal ldc_insamt, ldc_mod, ldc_lnreq
datetime ldtm_protstart, ldtm_protend, ldtm_paydate

ls_memno = astr_lninsure.member_no
ls_instype = astr_lninsure.instype_code
ls_contclr = astr_lninsure.lncont_clr
ldc_lnreq = astr_lninsure.loanrequest_amt
ldtm_paydate = astr_lninsure.loanrcv_date

if ls_contclr = "" or IsNull(ls_contclr) then
	ls_contclr = "''"
end if

// หายอดเงินกู้ทั้งหมดที่ต้องทำประกันประเภทนี้
ls_sqlbal = ""
ls_sqlbal += " select sum( ln.withdrawable_amt + ln.principal_balance ) "
ls_sqlbal += " from	lncontmaster ln "
ls_sqlbal += " 		join lnloantype lt on ln.coop_id = lt.coop_id and ln.loantype_code = lt.loantype_code "
ls_sqlbal += " where ln.contract_status > 0 "
ls_sqlbal += " and lt.insman_status = 1 "
ls_sqlbal += " and lt.insmantype_code = '" + ls_instype + "' "
ls_sqlbal += " and ln.member_no = '" + ls_memno + "' "
ls_sqlbal += " and ln.loancontract_no not in (" + ls_contclr + ") "

declare lncursor dynamic cursor for sqlsa;
prepare sqlsa from :ls_sqlbal using itr_sqlca;

open dynamic lncursor;
fetch lncursor into :ldc_sumprn;
if itr_sqlca.SQLCode <> 0 or isnull(ldc_sumprn) then
	ldc_sumprn = 0
end if
close lncursor;

// หายอดทุนประกันที่เคยทำไว้ (ไม่ใช้การระบุจากสถานะตรงๆ แต่เป็นการกวาดเอามาแล้วเลือกอันล่าสุดโดยดูจากวันที่คุ้มครอง)
ls_sqlins = ""
ls_sqlins += " select protect_amt"
ls_sqlins += " from	isinsuremaster "
ls_sqlins += " where insure_status = 1 "
ls_sqlins += " and instype_code = '" + ls_instype + "' "
ls_sqlins += " and member_no = '" + ls_memno + "' "
ls_sqlins += " order by protectstart_date desc "

declare inscursor dynamic cursor for sqlsa;
prepare sqlsa from :ls_sqlins using itr_sqlca;

open dynamic inscursor;
fetch inscursor into :ldc_protcur;
if itr_sqlca.SQLCode <> 0 then
	ldc_protcur = 0
end if
close inscursor;

// ข้อกำหนดระบบประกัน
select inscal_type, lnexcept_amt, reins_type
into :li_inscaltype, :ldc_lnexcept, :li_insrecal
from iscfinstype
where coop_id = :is_coopcontrol
and instype_code = :ls_instype
using itr_sqlca;
if itr_sqlca.SQLCode <> 0 then
	li_inscaltype = 1
	ldc_lnexcept = 0
end if

// ยอดที่ต้องทำประกัน
ldc_protall = (ldc_sumprn + ldc_lnreq)-ldc_lnexcept

if ldc_protall < 0 then
	ldc_protall = 0
end if

// ตรวจสอบว่ายอดเกินที่คุ้มครองเดิมมั้ยถ้าไม่เกินก็ไม่ต้องทำ
if li_insrecal = 1 and ldc_protall <= ldc_protcur then
	return 0
end if

// หายอดทุนประกันที่ต้องทำเพิ่ม
if li_insrecal = 1 then
	ldc_protadd = ldc_protall - ldc_protcur
else
	ldc_protadd = ldc_protall
end if
	
// วิ่งไปหาประกันหลักที่อยู่ในช่วงคุ้มครอง
ldc_sqlmain = ""
ldc_sqlmain += " select	ins.protectstart_date, ins.protectend_date, insr.step_protect, insr.step_rate, insr.insure_minamt, insr.insure_maxamt "
ldc_sqlmain += " from	isinsuremain ins "
ldc_sqlmain += " 		join isinsuremainmap insm on ins.coop_id = insm.coop_id and ins.insmain_no = insm.insmain_no "
ldc_sqlmain += " 		join isinsuremainrate insr on ins.coop_id = insr.coop_id and ins.insmain_no = insr.insmain_no "
ldc_sqlmain += " where insm.instype_code = '" + ls_instype + "' "
ldc_sqlmain += " and to_date('" + string(ldtm_paydate, "yyyymmdd")+"','yyyymmdd') between ins.protectstart_date and ins.protectend_date "
ldc_sqlmain += " and " + string(ldc_protall)+" between insr.protect_from and insr.protect_to "

declare maincursor dynamic cursor for sqlsa;
prepare sqlsa from :ldc_sqlmain using itr_sqlca;

open dynamic maincursor;
fetch maincursor into :ldtm_protstart, :ldtm_protend, :ldc_stepprot, :ldc_steprate, :ldc_ratemin, :ldc_ratemax;
if itr_sqlca.SQLCode <> 0 then
	ithw_exception.Text = "ไม่พบข้อมูลอัตราเบี้ยประกันประเภท " + ls_instype + " ที่อยู่ในช่วงวันที่ " + string(ldtm_paydate, "dd/mm/yyyy")+" ช่วงทุนประกัน " + string(ldc_protall)
	throw ithw_exception
end if
close maincursor;

// ปรับทุนที่ต้องทำเพิ่มให้เต็มขั้น
ldc_mod = Mod(ldc_protadd, ldc_stepprot)

if ldc_mod > 0 then
	ldc_protadd = (ldc_protadd - ldc_mod)+ldc_stepprot
end if
	
// ดูว่ามีกี่ขั้น
ldc_stepall = Truncate(ldc_protadd / ldc_stepprot, 0)
	
// เอาจำนวนขั้นคูณจำนวน rate
ldc_insamt = ldc_stepall * ldc_steprate
	
// ตรวจสอบว่าส่งคำนวณเบี้ยเป็นวันหรือเปล่า
if li_inscaltype = 2 then
	li_dayinsmain = DaysAfter(date(ldtm_protstart), date(ldtm_protend))+1
	li_dayprot = DaysAfter(date(ldtm_paydate), date(ldtm_protend))+1
		
	ldc_insamt = ldc_insamt * (dec(li_dayprot) / dec(li_dayinsmain))
end if
	
astr_lninsure.insprotect_amt = ldc_protadd
astr_lninsure.ins_amt = ldc_insamt

return 1
end function

on n_cst_loansrv_insure.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_loansrv_insure.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;
// ประกาศ Throw สำหรับ Err
ithw_exception	= create Exception
end event
