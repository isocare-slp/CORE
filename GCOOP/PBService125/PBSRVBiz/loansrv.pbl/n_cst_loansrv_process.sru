$PBExportHeader$n_cst_loansrv_process.sru
forward
global type n_cst_loansrv_process from NonVisualObject
end type
end forward

global type n_cst_loansrv_process from NonVisualObject
end type
global n_cst_loansrv_process n_cst_loansrv_process

type variables
transaction		itr_sqlca
Exception		ithw_exception

n_ds		ids_infomemdet, ids_infosliptype

n_cst_dbconnectservice		inv_transection
n_cst_procservice				inv_procsrv
n_cst_progresscontrol		inv_progress
n_cst_doccontrolservice		inv_docsrv
n_cst_dwxmlieservice			inv_dwxmliesrv
n_cst_loansrv_lnoperate		inv_lnoperatesrv
n_cst_loansrv_interest		inv_intsrv
n_cst_loansrv_calperiod		inv_calperiodsrv

string is_coopid , is_coopcontrol

constant string	DWO_PAYOUTSLIP	= "d_loansrv_lnpayout_slip"
constant string	DWO_PAYINSLIP 		= "d_loansrv_lnpayin_slip"
constant string	DWO_PAYINSLIPDET	= "d_loansrv_lnpayin_slipdet"
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception
private function integer of_setsrvdoccontrol (boolean ab_switch)
private function integer of_setsrvdwxmlie (boolean ab_switch)
private function integer of_setsrvlnoperate (boolean ab_switch) throws Exception
private function integer of_setsrvlninterest (boolean ab_switch)
private function any of_getattribsliptype (string as_sliptype, string as_attrib)
public function integer of_proc_lnprepare_opt (ref str_proclnprepare astr_proclnprepare) throws Exception
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception
private function integer of_setsrvproc (boolean ab_switch)
private function integer of_proc_lnprepare_clr (n_ds ads_lnprepare, n_ds ads_lnprepare_lntyp) throws Exception
private function integer of_gen_lntype (n_ds ads_lnprepare_lntyp, ref string as_lntyp)
private function integer of_proc_lnprepare_cal (n_ds ads_lnprepare, n_ds ads_lnprepare_lntyp) throws Exception
public function integer of_savenewlntype (string as_xmloption) throws Exception
private function string of_savenewlntype_ins (string as_table, string as_keynew, string as_keyold)
private function string of_savenewlntype_upd (n_ds ads_option)
private function integer of_setsrvcalperiod (boolean ab_switch)
public function integer of_post_chgfixperiodpay () throws Exception
public function integer of_proc_chgfixperiodpay (datetime adtm_adjdate, string as_entryid) throws Exception
public function integer of_proc_trnpayin_back (str_proctrnpayin astr_proctrnpayin) throws Exception
public function integer of_proc_trnpayin_buildslip (n_ds ads_trnpayin, ref n_ds ads_payinslip, ref n_ds ads_payinslipdet) throws Exception
public function integer of_proc_trnpayin_genslipno (ref n_ds ads_trnpayin) throws Exception
public function integer of_proc_trnpayin (ref str_proctrnpayin astr_proctrnpayin) throws Exception
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception;// Service Transection
if isnull( inv_transection ) or not isvalid( inv_transection ) then
	inv_transection	= create n_cst_dbconnectservice
	inv_transection	= anv_dbtrans
end if

inv_progress = create n_cst_progresscontrol

// ส่วนบริการการนำเข้าข้อมูล
this.of_setsrvdwxmlie( true )

itr_sqlca 		= inv_transection.itr_dbconnection

is_coopcontrol	= inv_transection.is_coopcontrol
is_coopid			= inv_transection.is_coopid

// initial info memdetail
ids_infomemdet = create n_ds
ids_infomemdet.dataobject = "d_loansrv_info_memdetail"
ids_infomemdet.settransobject( itr_sqlca )

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

private function integer of_setsrvlnoperate (boolean ab_switch) throws Exception;// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_lnoperatesrv ) or not isvalid( inv_lnoperatesrv ) then
		inv_lnoperatesrv	= create n_cst_loansrv_lnoperate
		inv_lnoperatesrv.of_initservice( inv_transection )
		return 1
	end if
else
	if isvalid( inv_lnoperatesrv ) then
		destroy inv_lnoperatesrv
		return 1
	end if
end if

return 0
end function

private function integer of_setsrvlninterest (boolean ab_switch);// Check argument
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

private function any of_getattribsliptype (string as_sliptype, string as_attrib);integer	li_row
any		la_attrib

if not isvalid( ids_infosliptype ) or isnull( ids_infosliptype ) then
	return ""
end if

// validate parameter
li_row	= ids_infosliptype.find( "sliptype_code = '" + as_sliptype+"'", 0, ids_infosliptype.rowcount())

if li_row > 0 then
	choose case lower ( Left ( ids_infosliptype.describe ( as_attrib + ".ColType" ) , 5 ) )
		case "char(", "char"
			la_attrib	= ids_infosliptype.getitemstring( li_row, as_attrib ) 
			
		case "date"
			la_attrib	= ids_infosliptype.getitemdate( li_row, as_attrib ) 
			
		case "datet"
			la_attrib	= ids_infosliptype.getitemdatetime( li_row, as_attrib ) 
			
		case "decim"
			la_attrib	= ids_infosliptype.getitemdecimal( li_row, as_attrib ) 
			
		case "numbe", "long", "ulong", "real", "int"
			la_attrib	= ids_infosliptype.getitemnumber( li_row, as_attrib ) 
			
		case "time", "times"
			la_attrib	= ids_infosliptype.getitemtime( li_row, as_attrib ) 
			
		case else
			setnull( la_attrib )
	end choose
	
end if

return la_attrib
end function

public function integer of_proc_lnprepare_opt (ref str_proclnprepare astr_proclnprepare) throws Exception;string ls_xmloption , ls_xmllntyp
string ls_coopid
string ls_proctext
integer li_proctype
integer li_prgmax , li_prelonfg , li_preclrfg
boolean lb_err
n_ds lds_lnprepare , lds_lnprepare_lntyp

this.of_setsrvdwxmlie( true )
this.of_setsrvproc( true )

inv_progress.istr_progress.progress_text = "ประมวลเตรียมข้อมูลชำระหนี้จากระบบต่างๆ"
inv_progress.istr_progress.progress_index = 0
inv_progress.istr_progress.progress_max = 1
inv_progress.istr_progress.subprogress_text = "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1
inv_progress.istr_progress.status = 8

lds_lnprepare		= create n_ds
lds_lnprepare.dataobject	= "d_loansrv_proc_lnprepare"
lds_lnprepare.settransobject( itr_sqlca )

lds_lnprepare_lntyp		= create n_ds
lds_lnprepare_lntyp.dataobject	= "d_loansrv_proc_lnprepare_lntyp"
lds_lnprepare_lntyp.settransobject( itr_sqlca )

// นำเข้าข้อมูล
ls_xmloption		= astr_proclnprepare.xml_option
if inv_dwxmliesrv.of_xmlimport( lds_lnprepare , ls_xmloption ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเงื่อนไขการประมวลเตรียมข้อมูลชำระหนี้จากระบบต่างๆ(0.1)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

ls_xmllntyp		= astr_proclnprepare.xml_lntype
if inv_dwxmliesrv.of_xmlimport( lds_lnprepare_lntyp , ls_xmllntyp ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูลเงื่อนไขการประมวลเตรียมข้อมูลชำระหนี้จากระบบต่างๆ(0.2)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

// กรองให้เหลือแต่พวกที่ทำรายการเท่านั้น
lds_lnprepare_lntyp.setfilter( "operate_flag > 0" )
lds_lnprepare_lntyp.filter()
// ลบพวกไม่ทำรายการทิ้งไป
lds_lnprepare_lntyp.rowsdiscard( 1, lds_lnprepare_lntyp.filteredcount(), filter! )

if lds_lnprepare_lntyp.rowcount() < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาด(0.3)"
	ithw_exception.text += "~r~nไม่พบข้อมูลการเลือกประเภทหนี้เพื่อทำรายการ"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

ls_coopid		= lds_lnprepare.object.coop_id[1]
ls_proctext	= lds_lnprepare.object.proc_text[1]
li_proctype	= lds_lnprepare.object.proc_type[1]
li_preclrfg	= lds_lnprepare.object.prepareclr_flag[1]
li_prelonfg	= lds_lnprepare.object.preparelon_flag[1]

li_prgmax	= li_preclrfg + li_prelonfg
inv_progress.istr_progress.progress_max = li_prgmax

// กำหนดค่าที่เตรียมประมวล
try
	inv_procsrv.of_set_proctype( li_proctype ) // กำหนดวิธีดึงข้อมูล 60 ดึงข้อมูลตามทะเบียนสมาชิก
	inv_procsrv.of_set_txtproc( ls_proctext ) // ใส่ค่าที่ดึงข้อมูล
	inv_procsrv.of_set_analyze() // gen ข้อมูลในการดึง
	inv_procsrv.of_set_sqlselect( "mbmembmaster") // set ค่าที่ gen ลงในตารางที่เลือก
catch( Exception lthw_setproc )
	ithw_exception.text	= "~r~nพบขอผิดพลาด (0.3)"
	ithw_exception.text	+= lthw_setproc.text
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	lb_err = true
end try
if lb_err then Goto objdestroy

// ตรวจสอบการประมวลผล
try
	if li_preclrfg = 1 then
		inv_progress.istr_progress.progress_index	= li_preclrfg
		this.of_proc_lnprepare_clr( lds_lnprepare , lds_lnprepare_lntyp )
	end if
	if li_prelonfg = 1 then
		inv_progress.istr_progress.progress_index	= li_preclrfg + li_prelonfg
		this.of_proc_lnprepare_cal( lds_lnprepare , lds_lnprepare_lntyp )
	end if
catch( Exception lthw_setdwsql )
	ithw_exception.text	= "~r~nพบขอผิดพลาด (40.01)" + lthw_setdwsql.text
	lb_err = true
end try

objdestroy:
if isvalid(lds_lnprepare) then destroy lds_lnprepare
if isvalid(lds_lnprepare_lntyp) then destroy lds_lnprepare_lntyp

this.of_setsrvdwxmlie( false )
this.of_setsrvproc( false )

if lb_err then
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_loansrv_process.of_proc_lnprepare_opt()" + ithw_exception.text
	throw ithw_exception
else
	commit using itr_sqlca;
end if

inv_progress.istr_progress.status = 1

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception;/***********************************************************
<description>
	กำหนด progress bar ที่ใช้แสดงผล
</description>

<arguments>

</arguments> 

<return> 
	Integer	1 = success
</return> 

<usage> 
	
	Revision History:
	Version 1.0 (Initial version) Modified Date 28/9/2010 by MiT
</usage> 
***********************************************************/
anv_progress = inv_progress

return 1
end function

private function integer of_setsrvproc (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_procsrv ) or not isvalid( inv_procsrv ) then
		inv_procsrv	= create n_cst_procservice
		inv_procsrv.of_initservice()
		return 1
	end if
else
	if isvalid( inv_procsrv ) then
		destroy inv_procsrv
		return 1
	end if
end if

return 0
end function

private function integer of_proc_lnprepare_clr (n_ds ads_lnprepare, n_ds ads_lnprepare_lntyp) throws Exception;string ls_sql , ls_sqlselect , ls_sqlselect_lntyp
string ls_coopid , ls_preparetype , ls_lntyp
string ls_preparedate
datetime ldtm_prepare

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลชำระหนี้จากระบบต่างๆ รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 1

ls_coopid			= ads_lnprepare.object.coop_id[1]
ls_preparetype	= ads_lnprepare.object.preparetype_code[1]
ldtm_prepare	= ads_lnprepare.object.prepare_date[1]

ls_preparedate	= string( ldtm_prepare , 'dd/mm/yyyy' )

yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "~r~nยกเลิกการประมวลโดยผู้ทำรายการ (PRELNCLR 50.1)"
	throw ithw_exception
end if

ls_sql	= " delete from lnpreparepay " 
ls_sql += " where coop_id = '" + is_coopcontrol + "' "
ls_sql += " and preparetype_code = '" + ls_preparetype + "' "
ls_sql += " and prepare_date = to_date( '" + ls_preparedate + "' , 'dd/mm/yyyy' ) "
ls_sql += " and exists ( select 1 from mbmembmaster , lncontmaster where mbmembmaster.coop_id = '" + is_coopcontrol + "' "
ls_sql += " and mbmembmaster.coop_id = lncontmaster.memcoop_id and mbmembmaster.member_no = lncontmaster.member_no "
ls_sql += " and lncontmaster.coop_id = lnpreparepay.coop_id and lncontmaster.loancontract_no = lnpreparepay.loancontract_no ) "

execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "~r~nพบข้อผิดพลาด of_proc_lnprepare_clr (70.01) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลชำระหนี้จากระบบต่างๆ"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	throw ithw_exception
end if

yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "~r~nยกเลิกการประมวลโดยผู้ทำรายการ (PRELNCLR 50.2)"
	throw ithw_exception
end if
inv_progress.istr_progress.subprogress_text = "ลบข้อมูลชำระหนี้จากระบบต่างๆ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1

return 1
end function

private function integer of_gen_lntype (n_ds ads_lnprepare_lntyp, ref string as_lntyp);string ls_lntyp , ls_lntype
integer li_index , li_count

li_count	= ads_lnprepare_lntyp.rowcount()

for li_index = 1 to li_count
	ls_lntype	= ads_lnprepare_lntyp.object.loantype_code[li_index]
	ls_lntyp	= ls_lntyp + " '" + ls_lntype + "' , "
next 

ls_lntyp	= left( ls_lntyp , len( ls_lntyp ) - 2 )
as_lntyp	= ls_lntyp

return 1
end function

private function integer of_proc_lnprepare_cal (n_ds ads_lnprepare, n_ds ads_lnprepare_lntyp) throws Exception;string ls_sql , ls_sqlselect , ls_sqlselect_lntyp
string ls_coopid , ls_contcoop , ls_lntyp , ls_bizzperiod
string ls_preparetype , ls_caltype
string ls_loantype , ls_contno , ls_continttabcode
string ls_entry
string ls_preparedate , ls_calinttodate
integer li_seqno , li_intsteptype , li_calintmethod , li_continttype
long ll_index , ll_count
dec{2} ldc_prinpay , ldc_intpay , ldc_itempay
dec{2} ldc_apvamt , ldc_balance , ldc_contintfixrate , ldc_contintincrease
datetime ldtm_prepare , ldtm_calintto , ldtm_calintfrom
boolean lb_err
str_calinterest lstr_calintcri
n_ds lds_info_prepare

this.of_setsrvlninterest( true )

lds_info_prepare		= create n_ds
lds_info_prepare.dataobject	= "d_loansrv_info_preparepay"
lds_info_prepare.settransobject( itr_sqlca )

inv_progress.istr_progress.subprogress_text = "ลบข้อมูลชำระหนี้จากระบบต่างๆ รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = 2

ls_coopid				= ads_lnprepare.object.coop_id[1]
ls_bizzperiod		= ads_lnprepare.object.bizz_period[1]
ls_preparetype		= ads_lnprepare.object.preparetype_code[1]
ls_caltype			= ads_lnprepare.object.caltype_code[1]
ls_entry				= ads_lnprepare.object.entry_id[1]
ldtm_calintto		= ads_lnprepare.object.calintto_date[1]
ldtm_prepare		= ads_lnprepare.object.prepare_date[1]

ls_calinttodate		= string( ldtm_calintto , 'dd/mm/yyyy' )
ls_preparedate		= string( ldtm_prepare , 'dd/mm/yyyy' )

if isnull( ls_bizzperiod ) then ls_bizzperiod = ""

// ดึงข้อมูลเงินกู้ที่ต้องการคำนวณ
this.of_gen_lntype( ads_lnprepare_lntyp , ls_lntyp )
if len( ls_lntyp ) > 0 then ls_sqlselect_lntyp = " and lncontmaster.loantype_code in ( " + ls_lntyp + " ) "

try
	inv_procsrv.of_set_sqlselect( "mbmembmaster")
	inv_procsrv.of_get_sqlselect( ls_sqlselect )
	inv_procsrv.of_set_sqldw_column( lds_info_prepare , " and preparetype_code = '" + ls_preparetype + "' " )
	inv_procsrv.of_set_sqldw_column( lds_info_prepare , " and prepare_date = to_date( '" + ls_preparedate + "' , 'dd/mm/yyyy' ) " )
	inv_procsrv.of_set_sqldw_column( lds_info_prepare , " and exists ( select 1 from mbmembmaster , lncontmaster where mbmembmaster.coop_id = '" + ls_coopid + "' and mbmembmaster.coop_id = lncontmaster.memcoop_id and mbmembmaster.member_no = lncontmaster.member_no and lncontmaster.coop_id = lnpreparepay.coop_id and lncontmaster.loancontract_no = lnpreparepay.loancontract_no " + ls_sqlselect_lntyp + ls_sqlselect + " ) " )
catch( Exception lthw_sqlselect )
	ithw_exception.text	+= lthw_sqlselect.text
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	lb_err = true 
end try
if lb_err then Goto objdestroy

/*
yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "~r~nยกเลิกการประมวลโดยผู้ทำรายการ (PRELNCAL 50.1)"
	lb_err = true ; Goto objdestroy
end if

ls_sql	= " delete from lnpreparepay " 
ls_sql += " where preparetype_code = '" + ls_preparetype + "' "
ls_sql += " and prepare_date = to_date( '" + ls_preparedate + "' , 'dd/mm/yyyy' ) "
ls_sql += " and exists ( select 1 from mbmembmaster , lncontmaster where mbmembmaster.coop_id = '" + ls_coopid + "' "
ls_sql += " and mbmembmaster.coop_id = lncontmaster.memcoop_id and mbmembmaster.member_no = lncontmaster.member_no "
ls_sql += " and lncontmaster.coop_id = lnpreparepay.coop_id and lncontmaster.loancontract_no = lnpreparepay.loancontract_no "
ls_sql += ls_sqlselect_lntyp
ls_sql += ls_sqlselect + " ) "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "~r~nพบข้อผิดพลาด of_proc_lnprepare_cal (70.01) "
	ithw_exception.text 	+= "~r~nไม่สามารถลบข้อมูลชำระหนี้จากระบบต่างๆ"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if
*/

yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "~r~nยกเลิกการประมวลโดยผู้ทำรายการ (PRELNCAL 50.2)"
	lb_err = true ; Goto objdestroy
end if
inv_progress.istr_progress.subprogress_text = "ลบข้อมูลชำระหนี้จากระบบต่างๆ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 1
inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลชำระหนี้จากระบบต่างๆ รอสักครู่..."

//insert into lnpreparepay
//( coop_id , loancontract_no , seq_no , preparetype_code , prepare_date , prin_payment , caltype_code , calintfrom_date , calintto_date , prepare_status , entry_id , entry_date )
//select coop_id , loancontract_no , nvl( ( select max( lp.seq_no ) from lnpreparepay lp where lp.coop_id = lm.coop_id and lp.loancontract_no = lm.loancontract_no ) , 0 ) + 1 as seq_no ,
//'DIV' as preparetype_code , to_date( '01/01/1900' , 'dd/mm/yyyy' ) as prepare_date , principal_balance as prin_payment , 'ALL'  as caltype_code , 
//to_date( '01/01/1900' , 'dd/mm/yyyy' ) as calintfrom_date  , lastcalint_date as calintto_date , 8 as prepare_status , 'AdminG' as entry_id , sysdate as entry_date
//from lncontmaster lm
//where exists ( select 1 from mbmembmaster , lncontmaster where mbmembmaster.coop_id = '001001'
//and mbmembmaster.coop_id = lncontmaster.memcoop_id and mbmembmaster.member_no = lncontmaster.member_no
//and lncontmaster.coop_id = lm.coop_id and lncontmaster.loancontract_no = lm.loancontract_no 
//and lncontmaster.contract_status > 0 
//and lncontmaster.loantype_code = '18' );

ls_sql	= " insert into lnpreparepay "
ls_sql	+= " ( coop_id , loancontract_no , seq_no , loantype_code , memcoop_id , member_no , preparetype_code , prepare_date , prncalint_amt , caltype_code , calintfrom_date , calintto_date , prepare_status , entry_id , entry_date , bizz_period ) "
ls_sql	+= " select coop_id , loancontract_no , nvl( ( select max( lp.seq_no ) from lnpreparepay lp where lp.coop_id = lm.coop_id and lp.loancontract_no = lm.loancontract_no and lp.prepare_date = to_date( '" + ls_preparedate + "' , 'dd/mm/yyyy' ) ) , 0 ) + 1 as seq_no , "
ls_sql += " loantype_code , memcoop_id , member_no , "
ls_sql	+= " '" + ls_preparetype + "' as preparetype_code , to_date( '" + ls_preparedate + "' , 'dd/mm/yyyy' ) as prepare_date , principal_balance , '" + ls_caltype + "'  as caltype_code , "
ls_sql	+= " lastcalint_date as calintfrom_date , to_date( '" +ls_calinttodate+ "' , 'dd/mm/yyyy' ) as calintto_date  , 8 as prepare_status , 'AdminG' as entry_id , sysdate as entry_date , '" + ls_bizzperiod + "' "
ls_sql	+= " from lncontmaster lm "
ls_sql	+= " where exists ( select 1 from mbmembmaster , lncontmaster where mbmembmaster.coop_id = '" + is_coopcontrol + "' "
ls_sql	+= " and mbmembmaster.coop_id = lncontmaster.memcoop_id and mbmembmaster.member_no = lncontmaster.member_no "
ls_sql	+= " and lncontmaster.coop_id = lm.coop_id and lncontmaster.loancontract_no = lm.loancontract_no "
ls_sql += " and lncontmaster.contract_status > 0 "
ls_sql += ls_sqlselect_lntyp
ls_sql += ls_sqlselect + " ) "
execute immediate :ls_sql using itr_sqlca;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text 	= "~r~nพบข้อผิดพลาด of_proc_lnprepare_cal (70.02) "
	ithw_exception.text 	+= "~r~nไม่สามารถสร้างข้อมูลชำระหนี้จากระบบต่างๆ"
	ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
	ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

yield()
if inv_progress.of_is_stop() then
	ithw_exception.text = "~r~nยกเลิกการประมวลโดยผู้ทำรายการ (PRELNCAL 50.3)"
	lb_err = true ; Goto objdestroy
end if
inv_progress.istr_progress.subprogress_text = "สร้างข้อมูลชำระหนี้จากระบบต่างๆ เสร็จเรียบร้อย"
inv_progress.istr_progress.subprogress_index = 2

ll_count	= lds_info_prepare.retrieve()
inv_progress.istr_progress.subprogress_text = "คำนวณข้อมูลชำระหนี้จากระบบต่างๆ รอสักครู่..."
inv_progress.istr_progress.subprogress_index = 0
inv_progress.istr_progress.subprogress_max = ll_count

for ll_index = 1 to ll_count
	
	inv_progress.istr_progress.subprogress_index = ll_index
	
	yield()
	if inv_progress.of_is_stop() then
		ithw_exception.text = "~r~nยกเลิกการประมวลโดยผู้ทำรายการ (PRELNCAL 50.4)"
		lb_err = true ; Goto objdestroy
	end if
	
	ls_contcoop				= lds_info_prepare.object.coop_id[ll_index]
	ls_loantype				= lds_info_prepare.object.loantype_code[ll_index]
	ls_contno				= lds_info_prepare.object.loancontract_no[ll_index]
	ls_continttabcode		= lds_info_prepare.object.int_continttabcode[ll_index]
	li_seqno					= lds_info_prepare.object.seq_no [ll_index]
	li_intsteptype			= lds_info_prepare.object.int_intsteptype[ll_index]
	li_calintmethod			= lds_info_prepare.object.interest_method[ll_index]
	li_continttype			= lds_info_prepare.object.int_continttype[ll_index]
	ldc_apvamt				= lds_info_prepare.object.loanapprove_amt[ll_index]
	ldc_balance				= lds_info_prepare.object.prncalint_amt[ll_index]
	ldc_contintfixrate		= lds_info_prepare.object.int_contintrate[ll_index]
	ldc_contintincrease	= lds_info_prepare.object.int_contintincrease[ll_index]
	ldtm_calintfrom			= lds_info_prepare.object.calintfrom_date[ll_index]

	lstr_calintcri.loancontract_no		= ls_contno
	lstr_calintcri.contcoop_id				= ls_contcoop
	lstr_calintcri.loantype_code			= ls_loantype
	lstr_calintcri.int_continttabcode		= ls_continttabcode
	lstr_calintcri.int_intsteptype			= li_intsteptype
	lstr_calintcri.interest_method		= li_calintmethod
	lstr_calintcri.int_continttype			= li_continttype
	lstr_calintcri.principal_balance		= ldc_balance
	lstr_calintcri.loanapprove_amt		= ldc_apvamt
	lstr_calintcri.int_contintrate			= ldc_contintfixrate
	lstr_calintcri.int_contintincrease	= ldc_contintincrease

	choose case ls_caltype
		case "PRN"
			ldc_prinpay		= ldc_balance
			ldc_intpay		= 0
		case "INT"
			ldc_prinpay		= 0
		case "ALL"
			ldc_prinpay		= ldc_balance
	end choose
	
	if ls_caltype = "INT" or ls_caltype = "ALL" then
		if ldtm_calintfrom = ldtm_calintto then 
			ldc_intpay		= 0
		else
			inv_intsrv.of_computeinterest( lstr_calintcri , ldtm_calintfrom , ldtm_calintto , ldc_intpay )
		end if
	end if
	
	ldc_itempay		= ldc_prinpay + ldc_intpay
	
	update lnpreparepay
	set prin_payment		= :ldc_prinpay ,
	int_payment				= :ldc_intpay ,
	item_payment			= :ldc_itempay
	where coop_id			= :ls_contcoop
	and loancontract_no	= :ls_contno
	and prepare_date		= :ldtm_prepare
	and seq_no				= :li_seqno
	using itr_sqlca;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text 	= "~r~nพบข้อผิดพลาด of_proc_lnprepare_cal (70.03) "
		ithw_exception.text 	+= "~r~nไม่สามารถอัพเดทดอกเบี้ยได้"
		ithw_exception.text 	+= "~r~nเลขที่สัญญา : " + ls_contno
		ithw_exception.text 	+= "~r~n"+ string( itr_sqlca.sqlcode ) + "  " + itr_sqlca.sqlerrtext
		ithw_exception.text 	+= "~r~nกรุณาตรวจสอบ"
		lb_err = true ; Goto objdestroy
	end if
	
	inv_progress.istr_progress.subprogress_text = string( ll_index , "#,##0" ) + "/" + string( ll_count , "#,##0" ) + " ทำรายการสัญญา : " + string( ls_contno ) + " ชำระหนี้คงเหลือ : " + string( ldc_prinpay , "#,##0.00" ) + " ชำระดอกเบี้ย : " + string( ldc_intpay , "#,##0.00" ) + " รวมชำระ : " + string( ldc_itempay , "#,##0.00" )
	
next

objdestroy:
this.of_setsrvlninterest( false )
if isvalid(lds_info_prepare) then destroy lds_info_prepare

if lb_err then
	throw ithw_exception
end if

return 1
end function

public function integer of_savenewlntype (string as_xmloption) throws Exception;string		ls_pattlncode, ls_tabname, ls_pkname, ls_subtab, ls_sqlsyntax
string		ls_lntypenew, ls_lntypedesc, ls_lntypeprefix, ls_lntypegrp
integer	li_pattflag
boolean	lb_error = false
n_ds		lds_option

lds_option	= create n_ds
lds_option.dataobject		= "d_loansrv_opt_cflntype_newtype"

try
	this.of_setsrvdwxmlie( true )
	inv_dwxmliesrv.of_xmlimport( lds_option, as_xmloption )
	this.of_setsrvdwxmlie( false )
catch( Exception lthw_error )
	ithw_exception.text	= lthw_error.text
	lb_error		= true
end try

if lb_error then goto objdestroy

// ไม่พบข้อมูลเงินกู้ที่จะทำการเพิ่่ม
if lds_option.rowcount() <= 0 then
	ithw_exception.text	= "ไม่มีการส่งข้อมูลเงินกู้ประเภทใหม่ที่ต้องการจะเพิ่มเข้ามาทำการบันทึก"
	lb_error					= true
	goto objdestroy
end if

ls_lntypenew	= lds_option.getitemstring( 1, "loantype_code" )
ls_lntypedesc	= lds_option.getitemstring( 1, "loantype_desc" )
ls_lntypeprefix	= lds_option.getitemstring( 1, "prefix" )
ls_lntypegrp		= lds_option.getitemstring( 1, "loangroup_code" )

// บันทึกข้อมูลรหัสและชื่อก่อน
insert into lnloantype
(		coop_id, loantype_code, loantype_desc, loangroup_code, prefix	)
values
(		:is_coopcontrol, :ls_lntypenew, :ls_lntypedesc, :ls_lntypegrp, :ls_lntypeprefix		)
using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "บันทึกเงินกู้ประเภทใหม่ไม่ได้ (Insert Lntype) "+itr_sqlca.sqlerrtext
	lb_error					= true
	goto objdestroy
end if

li_pattflag		= lds_option.getitemnumber( 1, "usepattern_flag" )
ls_pattlncode	= lds_option.getitemstring( 1, "usepattern_lncode" )

// ถ้าเป็นการเพิ่มเฉยๆ ไม่มีการ copy pattern ก็ออกเลย
if li_pattflag <> 1 then
	goto objdestroy
end if

// copy ค่าจากประเภทเงินกู้ต้นแบบ
ls_sqlsyntax		= this.of_savenewlntype_upd( lds_option )

execute immediate :ls_sqlsyntax using itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "ไม่สามารถคัดลอกเงินกู้สัญญาต้นแบบได้ (Update Lntype) "+itr_sqlca.sqlerrtext
	lb_error					= true
	goto objdestroy
end if

ls_tabname		= "LNLOANTYPE"

// --- เริ่ม init sub table ---
// หาชื่อ PK ก่อน
select		constraint_name
into		:ls_pkname
from		user_constraints
where	( table_name = :ls_tabname )
and		( constraint_type = 'P' )
using itr_sqlca ;

if isnull( ls_pkname ) then
	return 1
end if

// เอาชื่อ PK ไปหา sub table โดยดูจาก FK เอา
declare subtab_cur cursor for
select		table_name
from		user_constraints
where	( r_constraint_name = :ls_pkname )
and		( constraint_type = 'R' )
using itr_sqlca ;

open subtab_cur ;
	fetch subtab_cur into :ls_subtab ;
	do while itr_sqlca.sqlcode = 0
		ls_sqlsyntax		= this.of_savenewlntype_ins( ls_subtab, ls_lntypenew, ls_pattlncode )
		
		execute immediate :ls_sqlsyntax using itr_sqlca ;
		
		if itr_sqlca.sqlcode <> 0 then
			ithw_exception.text	= "ไม่สามารถคัดลอกเงินกู้สัญญาต้นแบบได้ (Insert "+ls_subtab+") "+itr_sqlca.sqlerrtext
			lb_error					= true
			close subtab_cur ;
			goto objdestroy
		end if
		
		fetch subtab_cur into :ls_subtab ;
	loop
close subtab_cur ;

objdestroy:
destroy lds_option

if lb_error then
	rollback using itr_sqlca ;
	throw ithw_exception
end if

commit using itr_sqlca ;

return 1
end function

private function string of_savenewlntype_ins (string as_table, string as_keynew, string as_keyold);string		ls_keycol1, ls_keycol2
string		ls_cname, ls_syntax, ls_column

ls_keycol1		= "COOP_ID"
ls_keycol2		= "LOANTYPE_CODE"
ls_column		= ""
ls_syntax			= ""

declare colname_cur cursor for
select		column_name
from		user_tab_cols
where	( table_name = :as_table )
and ( user_generated = 'YES' )
order by	column_id asc
using itr_sqlca ;

open colname_cur ;
	fetch colname_cur into :ls_cname ;
	do while itr_sqlca.sqlcode = 0
		
		if not ( ls_cname = ls_keycol1 or ls_cname = ls_keycol2 ) then
			ls_column	= ls_column + ","+ls_cname
		end if
		
		fetch colname_cur into :ls_cname ;
	loop
close colname_cur ;

ls_syntax		= "INSERT INTO "+as_table+" ( COOP_ID, LOANTYPE_CODE"+ls_column+") SELECT '"+is_coopcontrol+"', '"+as_keynew+"'"+ls_column+" FROM "+as_table+" WHERE COOP_ID = '"+is_coopcontrol+"' AND LOANTYPE_CODE = '"+as_keyold+"'"

return ls_syntax
end function

private function string of_savenewlntype_upd (n_ds ads_option);string		ls_table, ls_keycol, ls_keynew, ls_keyold
string		ls_cname, ls_syntax, ls_colupdate, ls_colname
integer	li_colcount, li_colindex, li_pos, li_namelen

ls_table			= "LNLOANTYPE"
ls_keycol			= "LOANTYPE_CODE"
ls_keynew		= ads_option.getitemstring( 1, "loantype_code" )
ls_keyold			= ads_option.getitemstring( 1, "usepattern_lncode" )

ls_colupdate		= ""
ls_syntax			= ""

declare colname_cur cursor for
select		column_name
from		user_tab_cols
where	( table_name = :ls_table )
and ( user_generated = 'YES' )
order by	column_id asc
using itr_sqlca ;

open colname_cur ;
	fetch colname_cur into :ls_cname ;
	do while itr_sqlca.sqlcode = 0
		ls_colupdate		= ls_colupdate + ","+ls_cname
		
		fetch colname_cur into :ls_cname ;
	loop
close colname_cur ;

// ตัดพวก column ที่ใส่ค่าตั้งต้นเองออก
li_colcount		= integer( ads_option.Describe("DataWindow.Column.Count") )
for li_colindex = 1 to li_colcount
	ls_colname	= upper( ads_option.Describe("#"+string( li_colindex )+".Name") )
	
	li_namelen	= len( ","+ls_colname )
	li_pos			= pos( ls_colupdate, ","+ls_colname, 1 )
	
	if li_pos > 0 then
		ls_colupdate		= mid( ls_colupdate, 1, li_pos - 1 )+mid( ls_colupdate, li_pos + li_namelen ) 
	end if
next

if trim( ls_colupdate ) <> "" then
	ls_colupdate		= mid( ls_colupdate, 2 )
end if

ls_syntax		= "UPDATE "+ls_table+" set ("+ls_colupdate+") = (SELECT "+ls_colupdate+" FROM "+ls_table+" WHERE COOP_ID = '"+is_coopcontrol+"' and "+ls_keycol+" = '"+ls_keyold+"') WHERE COOP_ID = '"+is_coopcontrol+"' AND "+ls_keycol+" = '"+ls_keynew+"'"

return ls_syntax
end function

private function integer of_setsrvcalperiod (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_calperiodsrv ) or not isvalid( inv_calperiodsrv ) then
		inv_calperiodsrv	= create n_cst_loansrv_calperiod
		inv_calperiodsrv.of_initservice( inv_transection )
		return 1
	end if
else
	if isvalid( inv_calperiodsrv ) then
		destroy inv_calperiodsrv
		return 1
	end if
end if

return 0
end function

public function integer of_post_chgfixperiodpay () throws Exception;string		ls_ccoopid, ls_contno
long		ll_count, ll_index
dec		ldc_periodpay

// เรียก Service การปรับงวด
this.of_setsrvproc( true )

inv_progress.istr_progress.progress_text	= "ประมวลผลปรับงวดชำระสัญญาคงยอด"
inv_progress.istr_progress.progress_index	= 1
inv_progress.istr_progress.progress_max	= 1
inv_progress.istr_progress.status				= 8

select		count( lam.loancontract_no )
into		:ll_count
from		lnreqcontadjust lam, lnreqcontadjustdet lamd
where	( lam.coop_id				=  lamd.coop_id )
and		( lam.contadjust_docno	= lamd.contadjust_docno )
and		( lamd.contadjust_code	= 'PFX' )
and		( lam.contadjust_status	= 8 )
and		( lam.coop_id				= :is_coopcontrol )
using itr_sqlca ;

ll_index		= 0
inv_progress.istr_progress.subprogress_text	= "กำลังเตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index	= ll_index
inv_progress.istr_progress.subprogress_max	= ll_count

declare data_cur cursor for
select		lam.concoop_id, lam.loancontract_no, lamd.period_payment
from		lnreqcontadjust lam, lnreqcontadjustdet lamd
where	( lam.coop_id				=  lamd.coop_id )
and		( lam.contadjust_docno	= lamd.contadjust_docno )
and		( lamd.contadjust_code	= 'PFX' )
and		( lam.contadjust_status	= 8 )
and		( lam.coop_id				= :is_coopcontrol )
using itr_sqlca ;

open data_cur ;
fetch data_cur into :ls_ccoopid, :ls_contno, :ldc_periodpay ;
do while itr_sqlca.sqlcode = 0
	ll_index			= ll_index + 1
	
	inv_progress.istr_progress.subprogress_text	= "ปรับยอดสัญญา "+ls_contno+" งวดชำระใหม่ "+string( ldc_periodpay , "#,##0.00" )
	inv_progress.istr_progress.subprogress_index	= ll_index
	
	update	lncontmaster
	set			period_payment	= :ldc_periodpay
	where	( coop_id				= :ls_ccoopid )
	and		( loancontract_no	= :ls_contno )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถปรับปรุงงวดชำระได้ (Update Contract) เลขสัญญา "+ls_contno+" กรุณาตรวจสอบ "+itr_sqlca.sqlerrtext
		close data_cur ;
		rollback using itr_sqlca ;
		throw ithw_exception
	end if

	fetch data_cur into :ls_ccoopid, :ls_contno, :ldc_periodpay ;
loop

close data_cur ;

inv_progress.istr_progress.status				= 1

commit using itr_sqlca ;

return 1
end function

public function integer of_proc_chgfixperiodpay (datetime adtm_adjdate, string as_entryid) throws Exception;string		ls_ccoopid, ls_mcoopid, ls_lntype, ls_contno, ls_memno
string		ls_adjdocno
integer	li_installment, li_lastperiod, li_installbal, li_roundtype
long		ll_roundfactor, ll_lastdocno, ll_count, ll_index
dec		ldc_prnbal, ldc_intrate, ldc_periodpay, ldc_paymentnew
boolean	lb_error = false
datetime	ldtm_entrydate
str_lncalperiod	lstr_lncalperiod

ll_index			= 0
ldtm_entrydate	= datetime( today(), now() )

// เรียก Service การปรับงวด
this.of_setsrvcalperiod( true )
this.of_setsrvproc( true )

inv_progress.istr_progress.progress_text	= "ประมวลผลปรับงวดชำระสัญญาคงยอด"
inv_progress.istr_progress.progress_index	= 1
inv_progress.istr_progress.progress_max	= 2
inv_progress.istr_progress.status				= 8

inv_progress.istr_progress.subprogress_text	= "กำลังลบข้อมูลเก่า"
inv_progress.istr_progress.subprogress_index	= 1
inv_progress.istr_progress.subprogress_max	= 1

select		count( loancontract_no )
into		:ll_count
from		lncontmaster
where	( contract_status		= 1 )
and		( loanpayment_type	= 2 )
using		itr_sqlca ;

// ลบพวกของเดิมไปก่อน
delete from lnreqcontadjust
where	( coop_id		= :is_coopcontrol )
and		( contadjust_status = 8 )
using		itr_sqlca ;

// ดึงเลขเอกสารจากตัวคุม
select		last_documentno
into		:ll_lastdocno
from		cmdocumentcontrol
where	( coop_id				= :is_coopcontrol )
and		( document_code	= 'CONTADJDOCNO' )
using		itr_sqlca ;

inv_progress.istr_progress.progress_index		= 2
inv_progress.istr_progress.subprogress_text	= "กำลังเตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index	= 0
inv_progress.istr_progress.subprogress_max	= ll_count

declare data_cur cursor for
select		lm.coop_id, lm.loancontract_no, lm.memcoop_id, lm.member_no,
			lm.loantype_code, lm.period_payamt, lm.period_payment,
			lm.principal_balance, lm.last_periodpay,
			ft_getlncontintrate( lm.coop_id, lm.loancontract_no, :adtm_adjdate ) as int_contintrate,
			lt.payround_type, lt.payround_factor
from		lncontmaster lm, lnloantype lt
where	( lm.coop_id					= lt.coop_id )
and		( lm.loantype_code		= lt.loantype_code )
and		( lm.contract_status		= 1 )
and		( lm.loanpayment_type	= 2 )
order by lm.loantype_code asc, lm.loancontract_no asc
using		itr_sqlca ;

open data_cur ;
fetch data_cur into :ls_ccoopid, :ls_contno, :ls_mcoopid, :ls_memno, :ls_lntype, :li_installment, :ldc_periodpay, :ldc_prnbal, :li_lastperiod, :ldc_intrate, :li_roundtype, :ll_roundfactor ;
do while itr_sqlca.sqlcode = 0
	ll_index			= ll_index + 1
	
	inv_progress.istr_progress.subprogress_text	= "ประมวลผลคำนวณงวดชำระใหม่สัญญา "+ls_contno+" งวดชำระใหม่ "+string( ldc_periodpay , "#,##0.00" )
	inv_progress.istr_progress.subprogress_index	= ll_index
	
	li_installbal		= li_installment - li_lastperiod
	
	if li_installbal < 1 then
		goto nextfetch
	end if

	lstr_lncalperiod.loanpayment_type				= 2
	lstr_lncalperiod.calperiod_prnamt				= ldc_prnbal
	lstr_lncalperiod.calperiod_intrate				= ldc_intrate
	lstr_lncalperiod.period_installment				= li_installbal

	try
		inv_calperiodsrv.of_setpayroundattrib( li_roundtype, ll_roundfactor )
		inv_calperiodsrv.of_calperiodpay( lstr_lncalperiod )
		
		ldc_paymentnew	= lstr_lncalperiod.period_payment
		
	catch( Exception lthw_error )
	
	end try
	
	if ldc_paymentnew >= ldc_periodpay then
		goto nextfetch
	end if
	
	ll_lastdocno		= ll_lastdocno + 1
	ls_adjdocno		= "CP"+right( "00000000"+string( ll_lastdocno ), 8 )
	
	insert into lnreqcontadjust
	( 		coop_id, contadjust_docno, memcoop_id, member_no, concoop_id, loancontract_no, contadjust_date, contadjust_status, entry_id, entry_date, entry_bycoopid, bfprnbal_amt, bfperiod, int_contintrate	)
	values
	(		:is_coopcontrol, :ls_adjdocno, :ls_mcoopid, :ls_memno, :ls_ccoopid, :ls_contno, :adtm_adjdate, 8, :as_entryid, :ldtm_entrydate, :is_coopid, :ldc_prnbal, :li_lastperiod, :ldc_intrate	)
	using	itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถบันทึกรายการเปลี่ยนแปลงงวดชำระได้ (Insert ContadjMain) เลขสัญญา "+ls_contno+" กรุณาตรวจสอบ "+itr_sqlca.sqlerrtext
		lb_error					= true
		goto objdestroy
	end if
	
	insert into lnreqcontadjustdet
	( 		coop_id, contadjust_docno, contadjust_code, loanpayment_type, period_payamt, period_payment, period_payment_max, payment_status,
			oldpayment_type, oldperiod_payamt, oldperiod_payment, oldperiod_paymax, oldpayment_status	)
	values
	(		:is_coopcontrol, :ls_adjdocno, 'PFX', 2, :li_installment, :ldc_paymentnew, 0, 1,
			2, :li_installment, :ldc_periodpay, 0, 1	)
	using	itr_sqlca ;
			
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถบันทึกรายการเปลี่ยนแปลงงวดชำระได้ (Insert ContadjDetail) เลขสัญญา "+ls_contno+" กรุณาตรวจสอบ "+itr_sqlca.sqlerrtext
		lb_error					= true
		goto objdestroy
	end if

	nextfetch:
	fetch data_cur into :ls_ccoopid, :ls_contno, :ls_mcoopid, :ls_memno, :ls_lntype, :li_installment, :ldc_periodpay, :ldc_prnbal, :li_lastperiod, :ldc_intrate, :li_roundtype, :ll_roundfactor ;
loop

inv_progress.istr_progress.status				= 1

objdestroy:
close data_cur ;
this.of_setsrvcalperiod( false )

if lb_error then
	rollback using itr_sqlca ;
	throw ithw_exception
end if

update	cmdocumentcontrol
set			last_documentno	= :ll_lastdocno
where	( coop_id				= :is_coopcontrol )
and		( document_code	= 'CONTADJDOCNO' )
using		itr_sqlca ;

if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "ไม่สามารถบันทึกเลขเอกสารทำรายการล่าสุดได้  "+itr_sqlca.sqlerrtext
	rollback using itr_sqlca ;
	throw ithw_exception
end if

commit using itr_sqlca ;

return 1
end function

public function integer of_proc_trnpayin_back (str_proctrnpayin astr_proctrnpayin) throws Exception;string		ls_coopid, ls_source, ls_refslipno, ls_slipno, ls_receiptno
string		ls_memcoopid, ls_memno, ls_sliptype, ls_shrtypecode, ls_contcoopid, ls_contno
string		ls_memgroup, ls_subgroup, ls_trnitemcode, ls_itemcode, ls_desc
string		ls_moneytype, ls_tofromaccid, ls_entryid
integer	li_trnpaytype, li_lastperiod, li_shrstatus, li_seqno
long		ll_count, ll_index, ll_row
dec{2}	ldc_prnpay, ldc_intpay, ldc_itempay, ldc_shrstkamt, ldc_unitshare, ldc_shrstkvalue
dec{2}	ldc_prnbal, ldc_intperiod, ldc_intarrear, ldc_intall, ldc_shrstkbegin, ldc_intaccum
datetime	ldtm_trndate, ldtm_opedate, ldtm_entrydate, ldtm_lastcalint
n_ds		lds_trnpayin, lds_payinslip, lds_payinslipdet, lds_infocont

ls_coopid			= astr_proctrnpayin.coop_id
ls_source		= astr_proctrnpayin.source_code
ldtm_trndate	= astr_proctrnpayin.trans_date
ls_contcoopid    = astr_proctrnpayin.contcoop_id
ls_entryid		= astr_proctrnpayin.entry_id
ldtm_entrydate	= datetime( today(), now() )

lds_trnpayin		= create n_ds
lds_trnpayin.dataobject	= "d_loansrv_proc_trnpayin"
lds_trnpayin.settransobject( itr_sqlca )

lds_trnpayin.retrieve( ls_coopid, ls_source, ldtm_trndate )

ll_count	= lds_trnpayin.rowcount()

if ll_count <= 0 then
	destroy lds_trnpayin
	return 1
end if

// สำหรับดึงข้อมูลสัญญาเงินกู้
lds_infocont = create n_ds
lds_infocont.dataobject = "d_loansrv_info_contract"
lds_infocont.settransobject( itr_sqlca )

// รายละเอียดรายการ Slip
ids_infosliptype	= create n_ds
ids_infosliptype.dataobject	= "d_loansrv_attrib_ucfsliptype"
ids_infosliptype.settransobject( itr_sqlca )
ids_infosliptype.retrieve()

// Slip สำหรับบันทึก
lds_payinslip	= create n_ds
lds_payinslip.dataobject	= "d_loansrv_lnpayin_slip"
lds_payinslip.settransobject( itr_sqlca )

lds_payinslipdet	= create n_ds
lds_payinslipdet.dataobject	= "d_loansrv_lnpayin_slipdet"
lds_payinslipdet.settransobject( itr_sqlca )

this.of_setsrvdoccontrol( true )
this.of_setsrvlninterest( true )
this.of_setsrvlnoperate( true )

for ll_index = 1 to ll_count
	
	ls_memcoopid		= lds_trnpayin.getitemstring( ll_index, "memcoop_id" )
	ls_memno			= lds_trnpayin.getitemstring( ll_index, "member_no" )
	li_seqno				= lds_trnpayin.getitemnumber( ll_index, "seq_no" )
	ls_sliptype			= lds_trnpayin.getitemstring( ll_index, "sliptype_code" )
	ls_trnitemcode		= lds_trnpayin.getitemstring( ll_index, "transitem_code" )
	ldtm_opedate		= lds_trnpayin.getitemdatetime( ll_index, "realpay_date" )
	ls_shrtypecode		= lds_trnpayin.getitemstring( ll_index, "shrtype_code" )
	ls_contcoopid		= lds_trnpayin.getitemstring( ll_index, "concoop_id" )
	ls_contno			= lds_trnpayin.getitemstring( ll_index, "loancontract_no" )

	ls_moneytype		= lds_trnpayin.getitemstring( ll_index, "moneytype_code" )
	ls_tofromaccid		= lds_trnpayin.getitemstring( ll_index, "tofrom_accid" )
	ls_refslipno			= lds_trnpayin.getitemstring( ll_index, "trnsource_refslipno" )
	
	ls_memgroup		= lds_trnpayin.getitemstring( ll_index, "membgroup_code" )
	
	ldc_shrstkamt		= lds_trnpayin.getitemdecimal( ll_index, "sharestk_amt" )
	ldc_shrstkbegin		= lds_trnpayin.getitemdecimal( ll_index, "sharebegin_amt" )
	ldc_intaccum		= lds_trnpayin.getitemdecimal( ll_index, "accum_interest" )
	
	li_trnpaytype		= lds_trnpayin.getitemnumber( ll_index, "transpay_type" )
	ldc_prnpay			= lds_trnpayin.getitemdecimal( ll_index, "principal_trnamt" )
	ldc_intpay			= lds_trnpayin.getitemdecimal( ll_index, "interest_trnamt" )
	ldc_itempay			= lds_trnpayin.getitemdecimal( ll_index, "trans_amt" )
	
	lds_payinslip.reset()
	lds_payinslipdet.reset()
	
	ll_row		= lds_payinslip.insertrow( 0 )
	
	// ขอเลขที่ Slip และ Receipt No
	ls_slipno			= inv_docsrv.of_getnewdocno( ls_coopid, "SLSLIPPAYIN" )
	ls_receiptno		= inv_docsrv.of_getnewdocno( ls_coopid, "SLRECEIPTNO" )
	
	lds_payinslip.setitem( ll_row, "coop_id", ls_coopid )
	lds_payinslip.setitem( ll_row, "payinslip_no", ls_slipno )
	lds_payinslip.setitem( ll_row, "memcoop_id", ls_memcoopid )
	lds_payinslip.setitem( ll_row, "member_no", ls_memno )
	lds_payinslip.setitem( ll_row, "document_no", ls_receiptno )
	lds_payinslip.setitem( ll_row, "sliptype_code", ls_sliptype )
	lds_payinslip.setitem( ll_row, "slip_date", ldtm_trndate )
	lds_payinslip.setitem( ll_row, "slip_amt", ldc_itempay )
	lds_payinslip.setitem( ll_row, "operate_date", ldtm_opedate )
	lds_payinslip.setitem( ll_row, "moneytype_code", ls_moneytype )
	lds_payinslip.setitem( ll_row, "sharestkbf_value", ldc_shrstkbegin * 10 )
	lds_payinslip.setitem( ll_row, "sharestk_value", ldc_shrstkamt * 10 )
	lds_payinslip.setitem( ll_row, "intaccum_amt", ldc_intaccum )
	
	if ls_moneytype = "TRN" then
		lds_payinslip.setitem( ll_row, "ref_system", ls_source )
		lds_payinslip.setitem( ll_row, "ref_slipno", ls_refslipno )
		lds_payinslip.setitem( ll_row, "ref_slipamt", ldc_itempay )
	else
		lds_payinslip.setitem( ll_row, "tofrom_accid", ls_tofromaccid )
	end if
	
	lds_payinslip.setitem( ll_row, "membgroup_code", ls_memgroup )
	lds_payinslip.setitem( ll_row, "entry_id", ls_entryid )
	lds_payinslip.setitem( ll_row, "entry_date", ldtm_entrydate )
	
	ll_row	= lds_payinslipdet.insertrow( 0 )

	lds_payinslipdet.setitem( ll_row, "coop_id", ls_coopid )
	lds_payinslipdet.setitem( ll_row, "payinslip_no", ls_slipno )
	lds_payinslipdet.setitem( ll_row, "seq_no", 1 )

	// ดึงข้อมูลตามประเภทการชำระ
	choose case ls_trnitemcode
		case "SHR"
			// กำหนดค่าต่าง ๆ
			ls_itemcode	= string( this.of_getattribsliptype( ls_sliptype, "shstm_itemtype" ) )
			ls_desc		= string( this.of_getattribsliptype( ls_sliptype, "shpay_desc" ) )
			
			select		shsharemaster.sharestk_amt, shsharemaster.last_period,
						shsharemaster.sharemaster_status, shsharetype.unitshare_value
			into		:ldc_shrstkamt, :li_lastperiod, :li_shrstatus, :ldc_unitshare
			from		shsharemaster, shsharetype
			where	( shsharemaster.coop_id				= shsharetype.coop_id ) and
						( shsharemaster.sharetype_code	= shsharetype.sharetype_code ) and
						( shsharemaster.coop_id				= :ls_memcoopid ) and
						( shsharemaster.member_no		= :ls_memno )
			using		itr_sqlca ;
			
			ldc_shrstkvalue		= ldc_shrstkamt * ldc_unitshare
			
			lds_payinslipdet.object.shrlontype_code[ ll_row ]	= ls_shrtypecode
			lds_payinslipdet.object.period[ ll_row ]				= li_lastperiod
			lds_payinslipdet.object.bfperiod[ ll_row ]				= li_lastperiod
			lds_payinslipdet.object.bfshrcont_status[ ll_row ]	= li_shrstatus
			
			lds_payinslipdet.setitem( ll_row, "operate_flag", 1 )
			lds_payinslipdet.setitem( ll_row, "slipitemtype_code", ls_trnitemcode )
			lds_payinslipdet.setitem( ll_row, "slipitem_desc", ls_desc )
			lds_payinslipdet.setitem( ll_row, "principal_payamt", 0 )
			lds_payinslipdet.setitem( ll_row, "interest_payamt", 0 )
			lds_payinslipdet.setitem( ll_row, "item_payamt", ldc_itempay )
			lds_payinslipdet.setitem( ll_row, "item_balance", ldc_shrstkvalue + ldc_itempay )
			lds_payinslipdet.setitem( ll_row, "bfshrcont_balamt", ldc_shrstkvalue )
			lds_payinslipdet.setitem( ll_row, "stm_itemtype", ls_itemcode )
			
		case "LON"
			// กำหนดค่าต่าง ๆ
			ls_itemcode	= string( this.of_getattribsliptype( ls_sliptype, "lnstm_itemtype" ) )
			ls_desc		= string( this.of_getattribsliptype( ls_sliptype, "lnpay_desc" ) )
			
			lds_infocont.retrieve( ls_contcoopid, ls_contno )
			
			ldc_prnbal		= lds_infocont.getitemdecimal( 1, "principal_balance" )
			ldc_intarrear	= lds_infocont.getitemdecimal( 1, "interest_arrear" )
			ldtm_lastcalint	= lds_infocont.getitemdatetime( 1, "lastcalint_date" )
			
			lds_payinslipdet.object.shrlontype_code[ ll_row ]			= lds_infocont.object.loantype_code[ 1 ]
		
			lds_payinslipdet.object.rkeep_principal[ ll_row ]			= lds_infocont.object.rkeep_principal[ 1 ]
			lds_payinslipdet.object.rkeep_interest[ ll_row ]				= lds_infocont.object.rkeep_interest[ 1 ]
			lds_payinslipdet.object.nkeep_interest[ ll_row ]			= lds_infocont.object.nkeep_interest[ 1 ]
			lds_payinslipdet.object.period[ ll_row ]						= lds_infocont.object.last_periodpay[ 1 ]+1
			lds_payinslipdet.object.calint_from[ ll_row ]					= lds_infocont.object.lastcalint_date[ 1 ]
			
			lds_payinslipdet.object.bfperiod[ ll_row ]						= lds_infocont.object.last_periodpay[ 1 ]
			lds_payinslipdet.object.bfintarr_amt[ ll_row ]				= lds_infocont.object.interest_arrear[ 1 ]
			lds_payinslipdet.object.bfintarrset_amt[ ll_row ]			= lds_infocont.object.intyear_arrear[ 1 ]
			lds_payinslipdet.object.bfwithdraw_amt[ ll_row ]			= lds_infocont.object.withdrawable_amt[ 1 ]
			lds_payinslipdet.object.bfperiod_payment[ ll_row ]		= lds_infocont.object.period_payment[ 1 ]
			lds_payinslipdet.object.bfshrcont_status[ ll_row ]			= lds_infocont.object.contract_status[ 1 ]
			lds_payinslipdet.object.bfcontlaw_status[ ll_row ]			= lds_infocont.object.contlaw_status[ 1 ]
			lds_payinslipdet.object.bflastcalint_date[ ll_row ]			= lds_infocont.object.lastcalint_date[ 1 ]
			lds_payinslipdet.object.bflastproc_date[ ll_row ]			= lds_infocont.object.lastprocess_date[ 1 ]
			lds_payinslipdet.object.bfcountpay_flag[ ll_row ]			= lds_infocont.object.countpay_flag[ 1 ]
			lds_payinslipdet.object.bfcontstatus_desc[ ll_row ]		= lds_infocont.object.status_desc[ 1 ]
			
			lds_payinslipdet.object.bfpayspec_method[ ll_row ]		= lds_infocont.object.payspec_method[ 1 ]
			lds_payinslipdet.object.bfintreturn_flag[ ll_row ]			= lds_infocont.object.intreturn_flag[ 1 ]
			lds_payinslipdet.object.bfpxaftermthkeep_type[ ll_row ]	= lds_infocont.object.pxaftermthkeep_type[ 1 ]
			
			lds_payinslipdet.setitem( ll_row, "operate_flag", 1 )
			lds_payinslipdet.setitem( ll_row, "seq_no", ll_row )
			lds_payinslipdet.setitem( ll_row, "slipitemtype_code", "LON" )
			lds_payinslipdet.setitem( ll_row, "slipitem_desc", ls_desc )
			lds_payinslipdet.setitem( ll_row, "loancontract_no", ls_contno )
			lds_payinslipdet.setitem( ll_row, "concoop_id", ls_contcoopid )
			
			// คิด ด/บ ไว้ก่อน
			ldc_intperiod		= inv_intsrv.of_computeinterest( ls_contcoopid, ls_contno, ldtm_trndate )
			
			lds_payinslipdet.setitem( ll_row, "prncalint_amt", ldc_prnbal )
			lds_payinslipdet.setitem( ll_row, "calint_from", ldtm_lastcalint )
			lds_payinslipdet.setitem( ll_row, "calint_to", ldtm_opedate )
			lds_payinslipdet.setitem( ll_row, "interest_period", ldc_intperiod )
			
			// ดูว่าชำระดูจากยอดรวมหรือเปล่า
			if li_trnpaytype = 1 then
				ldc_intall			= ( ldc_intperiod + ldc_intarrear )
				
				if ldc_intall > ldc_itempay then
					ldc_intpay	= ldc_itempay
				else
					ldc_intpay	= ldc_intall
				end if
				
				ldc_prnpay		= ldc_itempay - ( ldc_intperiod + ldc_intarrear )
			end if
			
//			ldc_inttemp	= ( adc_principal * adc_intrate * li_dayinterest ) / ( li_daysinyear + ( adc_intrate * li_dayinterest ) )
//			ดอกเบี้ย = ( ยอดชำระ * อัตราดอกเบี้ย * จำนวนวันที่คิดดอกเบี้ย ) / ( จำนวนวันทั้งปี + ( อัตราดอกเบี้ย * จำนวนวันที่คิดดอกเบี้ย ))
			
			lds_payinslipdet.setitem( ll_row, "principal_payamt", ldc_prnpay )
			lds_payinslipdet.setitem( ll_row, "interest_payamt", ldc_intpay )
			lds_payinslipdet.setitem( ll_row, "item_payamt", ldc_itempay )
			lds_payinslipdet.setitem( ll_row, "item_balance", ldc_prnbal - ldc_prnpay )
			
			lds_payinslipdet.setitem( ll_row, "bfshrcont_balamt", ldc_prnbal )
			lds_payinslipdet.setitem( ll_row, "stm_itemtype", ls_itemcode )
		
		case else
	end choose
	
	// บันทึก Slip
	if lds_payinslip.update() <> 1 then
		ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้"
		ithw_exception.text	+= lds_payinslip.of_geterrormessage()
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
	
	// บันทึก Slip Detail
	if lds_payinslipdet.update() <> 1 then
		ithw_exception.text	= "บันทึกข้อมูล SlipDet ไม่ได้"
		ithw_exception.text	+= lds_payinslipdet.of_geterrormessage()
		rollback using itr_sqlca ;
		throw ithw_exception
	end if
	
	try
		inv_lnoperatesrv.of_postslip( trim( ls_sliptype ), lds_payinslip, lds_payinslipdet ,ls_contcoopid)//by a
	catch( Exception lthw_error )
		rollback using itr_sqlca ;
		throw lthw_error
	end try
	
	update	sltranspayin
	set			post_status		= 1,
				post_date		= :ldtm_entrydate,
				payinslip_no		= :ls_slipno
	where	( coop_id			= :ls_coopid ) and
				( memcoop_id	= :ls_memcoopid ) and
				( member_no	= :ls_memno ) and
				( trans_date		= :ldtm_trndate ) and
				( seq_no			= :li_seqno )
	using		itr_sqlca ;
	
	
next

commit using itr_sqlca ;

destroy lds_payinslip
destroy lds_payinslipdet

this.of_setsrvlnoperate( false )
this.of_setsrvdoccontrol( false )
this.of_setsrvlninterest( false )

return 1
end function

public function integer of_proc_trnpayin_buildslip (n_ds ads_trnpayin, ref n_ds ads_payinslip, ref n_ds ads_payinslipdet) throws Exception;string		ls_mcoopid, ls_memno, ls_memgroup, ls_sliptype, ls_trnitemcode, ls_itemcode, ls_desc, ls_shrlontype
string		ls_ccoopid, ls_contno, ls_moneytype, ls_tofromaccid, ls_refslipno
long		ll_index, ll_count, ll_row
integer	li_lastperiod, li_shrstatus, li_contlaw, li_contstatus, li_countpay
integer	li_cfpaymthd, li_cfintret, li_cfpxaftkeep, li_seqno, li_trnpaytype
dec		ldc_shrstkbegin, ldc_shrstkamt, ldc_shrstkvalue, ldc_unitshare, ldc_itempay
dec		ldc_periodpay, ldc_wtdamt, ldc_prnbal, ldc_intarrear, ldc_intsetarr, ldc_intret, ldc_intaccum, ldc_prnpay, ldc_intpay, ldc_intarrpay
dec		ldc_rkeepprn, ldc_rkeepint, ldc_nkeepint, ldc_intperiod, ldc_intall
datetime	ldtm_lastcalint, ldtm_lastproc, ldtm_opedate, ldtm_lastpay

ads_payinslip.reset()
ads_payinslipdet.reset()

ll_count		= ads_trnpayin.rowcount()

ls_mcoopid			= ads_trnpayin.getitemstring( 1, "memcoop_id" )
ls_memno			= ads_trnpayin.getitemstring( 1, "member_no" )
ls_memgroup		= ads_trnpayin.getitemstring( 1, "membgroup_code" )
ls_sliptype			= ads_trnpayin.getitemstring( 1, "sliptype_code" )

for ll_index = 1 to ll_count
	li_seqno				= ads_trnpayin.getitemnumber( ll_index, "seq_no" )
	ls_trnitemcode		= ads_trnpayin.getitemstring( ll_index, "transitem_code" )
	ldtm_opedate		= ads_trnpayin.getitemdatetime( ll_index, "realpay_date" )
	ls_ccoopid			= ads_trnpayin.getitemstring( ll_index, "concoop_id" )
	ls_contno			= ads_trnpayin.getitemstring( ll_index, "loancontract_no" )

	ls_moneytype		= ads_trnpayin.getitemstring( ll_index, "moneytype_code" )
	ls_tofromaccid		= ads_trnpayin.getitemstring( ll_index, "tofrom_accid" )
	ls_refslipno			= ads_trnpayin.getitemstring( ll_index, "trnsource_refslipno" )
	
	ldc_shrstkamt		= ads_trnpayin.getitemdecimal( ll_index, "sharestk_amt" )
	ldc_shrstkbegin		= ads_trnpayin.getitemdecimal( ll_index, "sharebegin_amt" )
	ldc_intaccum		= ads_trnpayin.getitemdecimal( ll_index, "accum_interest" )
	
	li_trnpaytype		= ads_trnpayin.getitemnumber( ll_index, "transpay_type" )
	ldc_prnpay			= ads_trnpayin.getitemdecimal( ll_index, "principal_trnamt" )
	ldc_intpay			= ads_trnpayin.getitemdecimal( ll_index, "interest_trnamt" )
	ldc_itempay			= ads_trnpayin.getitemdecimal( ll_index, "trans_amt" )
	
	ll_row		= ads_payinslipdet.insertrow( 0 )
	
	// ดึงข้อมูลตามประเภทการชำระ
	choose case ls_trnitemcode
		case "SHR"
			// กำหนดค่าต่าง ๆ
			ls_itemcode	= "SPX"
			ls_desc		= "ซื้อหุ้น"
			
			select		sm.sharetype_code, sm.sharebegin_amt, sm.sharestk_amt, sm.last_period,
						sm.sharemaster_status, st.unitshare_value
			into		:ls_shrlontype, :ldc_shrstkbegin, :ldc_shrstkamt, :li_lastperiod, :li_shrstatus, :ldc_unitshare
			from		shsharemaster sm, shsharetype st
			where	( sm.coop_id			= st.coop_id ) and
						( sm.sharetype_code	= st.sharetype_code ) and
						( sm.coop_id			= :ls_mcoopid ) and
						( sm.member_no		= :ls_memno )
			using		itr_sqlca ;
			
			ldc_shrstkvalue		= ldc_shrstkamt * ldc_unitshare
			
			ads_payinslipdet.setitem( ll_row, "shrlontype_code", ls_shrlontype )
			ads_payinslipdet.setitem( ll_row, "period", li_lastperiod )
			ads_payinslipdet.setitem( ll_row, "bfperiod", li_lastperiod )
			ads_payinslipdet.setitem( ll_row, "bfshrcont_status", li_shrstatus )
			
			ads_payinslipdet.setitem( ll_row, "operate_flag", 1 )
			ads_payinslipdet.setitem( ll_row, "slipitemtype_code", ls_trnitemcode )
			ads_payinslipdet.setitem( ll_row, "slipitem_desc", ls_desc )
			ads_payinslipdet.setitem( ll_row, "principal_payamt", 0 )
			ads_payinslipdet.setitem( ll_row, "interest_payamt", 0 )
			ads_payinslipdet.setitem( ll_row, "item_payamt", ldc_itempay )
			ads_payinslipdet.setitem( ll_row, "item_balance", ldc_shrstkvalue + ldc_itempay )
			ads_payinslipdet.setitem( ll_row, "bfshrcont_balamt", ldc_shrstkvalue )
			ads_payinslipdet.setitem( ll_row, "stm_itemtype", ls_itemcode )
			
		case "LON"
			// กำหนดค่าต่าง ๆ
			ls_itemcode	= "LPX"
			ls_desc		= "ชำระหนี้"
			
			select		lm.loantype_code, lm.period_payment, lm.last_periodpay, lm.lastcalint_date, lm.withdrawable_amt, lm.principal_balance, 
						lm.interest_arrear, lm.intset_arrear, lm.interest_return, lm.lastpayment_date, 
						lm.lastprocess_date, lm.rkeep_principal, lm.rkeep_interest, lm.nkeep_interest,
						lm.contlaw_status, lm.contract_status, lm.countpay_flag, lt.payspec_method, lt.intreturn_flag, lt.pxaftermthkeep_type
			into		:ls_shrlontype, :ldc_periodpay, :li_lastperiod, :ldtm_lastcalint, :ldc_wtdamt, :ldc_prnbal,
						:ldc_intarrear, :ldc_intsetarr, :ldc_intret, :ldtm_lastpay,
						:ldtm_lastproc, :ldc_rkeepprn, :ldc_rkeepint, :ldc_nkeepint,
						:li_contlaw, :li_contstatus, :li_countpay, :li_cfpaymthd, :li_cfintret, :li_cfpxaftkeep
			from		lncontmaster lm, lnloantype lt
			where	( lm.coop_id				= lt.coop_id )
			and		( lm.loantype_code	= lt.loantype_code )
			and		( lm.coop_id				= :ls_ccoopid )
			and		( lm.loancontract_no	= :ls_contno )
			using		itr_sqlca ;
			
			ads_payinslipdet.setitem( ll_row, "slipitemtype_code", "LON" )
			ads_payinslipdet.setitem( ll_row, "seq_no", li_seqno )
			ads_payinslipdet.setitem( ll_row, "operate_flag", 1 )
			ads_payinslipdet.setitem( ll_row, "shrlontype_code", ls_shrlontype )
			ads_payinslipdet.setitem( ll_row, "concoop_id", ls_ccoopid )
			ads_payinslipdet.setitem( ll_row, "loancontract_no", ls_contno )
			ads_payinslipdet.setitem( ll_row, "slipitem_desc", ls_desc )
			ads_payinslipdet.setitem( ll_row, "interest_return", ldc_intret )
			ads_payinslipdet.setitem( ll_row, "stm_itemtype", ls_itemcode )
			ads_payinslipdet.setitem( ll_row, "bfperiod", li_lastperiod )
			ads_payinslipdet.setitem( ll_row, "bfintarr_amt", ldc_intarrear )
			ads_payinslipdet.setitem( ll_row, "bfintarrset_amt", ldc_intsetarr )
			ads_payinslipdet.setitem( ll_row, "bflastcalint_date", ldtm_lastcalint )
			ads_payinslipdet.setitem( ll_row, "bflastproc_date", ldtm_lastproc )
			ads_payinslipdet.setitem( ll_row, "bflastpay_date", ldtm_lastpay )
			ads_payinslipdet.setitem( ll_row, "bfwithdraw_amt", ldc_wtdamt )
			ads_payinslipdet.setitem( ll_row, "bfperiod_payment", ldc_periodpay )
			ads_payinslipdet.setitem( ll_row, "bfshrcont_balamt", ldc_prnbal )
			ads_payinslipdet.setitem( ll_row, "bfshrcont_status", li_contstatus )
			ads_payinslipdet.setitem( ll_row, "bfcontlaw_status", li_contlaw )
			ads_payinslipdet.setitem( ll_row, "bfcontpay_flag", li_countpay )
			ads_payinslipdet.setitem( ll_row, "bfpayspec_method", li_cfpaymthd )
			ads_payinslipdet.setitem( ll_row, "rkeep_principal", ldc_rkeepprn )
			ads_payinslipdet.setitem( ll_row, "rkeep_interest", ldc_rkeepint )
			ads_payinslipdet.setitem( ll_row, "nkeep_interest", ldc_nkeepint )
			ads_payinslipdet.setitem( ll_row, "bfintreturn_flag", li_cfintret )
			ads_payinslipdet.setitem( ll_row, "bfintreturn_amt", ldc_intret )
			ads_payinslipdet.setitem( ll_row, "bfpxaftermthkeep_type", li_cfpxaftkeep )
						
			// คิด ด/บ ไว้ก่อน
			ldc_intperiod		= inv_intsrv.of_computeinterest( ls_ccoopid, ls_contno, ldtm_opedate )
		
			ads_payinslipdet.setitem( ll_row, "prncalint_amt", ldc_prnbal )
			ads_payinslipdet.setitem( ll_row, "calint_from", ldtm_lastcalint )
			ads_payinslipdet.setitem( ll_row, "calint_to", ldtm_opedate )
			ads_payinslipdet.setitem( ll_row, "interest_period", ldc_intperiod )
			
			// ดูว่าชำระดูจากยอดรวมหรือเปล่า
			if li_trnpaytype = 1 then
				ldc_intall			= ( ldc_intperiod + ldc_intarrear )
				
				if ldc_intall > ldc_itempay then
					ldc_intpay	= ldc_itempay
				else
					ldc_intpay	= ldc_intall
				end if
				
				ldc_prnpay		= ldc_itempay - ( ldc_intperiod + ldc_intarrear )
			end if
			
			ads_payinslipdet.setitem( ll_row, "principal_payamt", ldc_prnpay )
			ads_payinslipdet.setitem( ll_row, "interest_payamt", ldc_intpay )
			ads_payinslipdet.setitem( ll_row, "intarrear_payamt", ldc_intarrpay )
			ads_payinslipdet.setitem( ll_row, "item_payamt", ldc_itempay )
			ads_payinslipdet.setitem( ll_row, "item_balance", ldc_prnbal - ldc_prnpay )
	end choose
next

// Create Slip Master
ll_row		= ads_payinslip.insertrow( 0 )

//ads_payinslip.setitem( ll_row, "coop_id", ls_coopid )
//ads_payinslip.setitem( ll_row, "payinslip_no", ls_slipno )
//ads_payinslip.setitem( ll_row, "memcoop_id", ls_memcoopid )
//ads_payinslip.setitem( ll_row, "member_no", ls_memno )
//ads_payinslip.setitem( ll_row, "document_no", ls_receiptno )
//ads_payinslip.setitem( ll_row, "sliptype_code", ls_sliptype )
//ads_payinslip.setitem( ll_row, "slip_date", ldtm_trndate )
//ads_payinslip.setitem( ll_row, "slip_amt", ldc_itempay )
//ads_payinslip.setitem( ll_row, "operate_date", ldtm_opedate )
//ads_payinslip.setitem( ll_row, "moneytype_code", ls_moneytype )
//ads_payinslip.setitem( ll_row, "sharestkbf_value", ldc_shrstkbegin * 10 )
//ads_payinslip.setitem( ll_row, "sharestk_value", ldc_shrstkamt * 10 )
//ads_payinslip.setitem( ll_row, "intaccum_amt", ldc_intaccum )
//
//if ls_moneytype = "TRN" then
//	ads_payinslip.setitem( ll_row, "ref_system", ls_source )
//	ads_payinslip.setitem( ll_row, "ref_slipno", ls_refslipno )
//	ads_payinslip.setitem( ll_row, "ref_slipamt", ldc_itempay )
//else
//	ads_payinslip.setitem( ll_row, "tofrom_accid", ls_tofromaccid )
//end if
//
//ads_payinslip.setitem( ll_row, "membgroup_code", ls_memgroup )
//ads_payinslip.setitem( ll_row, "entry_id", ls_entryid )
//ads_payinslip.setitem( ll_row, "entry_date", ldtm_entrydate )


return 1
end function

public function integer of_proc_trnpayin_genslipno (ref n_ds ads_trnpayin) throws Exception;string		ls_mcoopid, ls_memno, ls_sliptype, ls_slipno, ls_shortyear
string		ls_memprior, ls_slipprior
long		ll_index, ll_count
datetime	ldtm_trndate

ads_trnpayin.setsort( "memcoop_id asc, member_no asc, sliptype_code, seq_no asc" )
ads_trnpayin.sort()

ll_count		= ads_trnpayin.rowcount()

if ll_count <= 0 then
	return 0
end if

ldtm_trndate	= ads_trnpayin.getitemdatetime( ll_index, "trans_date" )

ls_shortyear		= string( ( year( date( ldtm_trndate ) ) + 543 ) - 2500 )

for ll_index = 1 to ll_count
	ls_memno		= ads_trnpayin.getitemstring( ll_index, "member_no" )
	ls_sliptype		= ads_trnpayin.getitemstring( ll_index, "sliptype_code" )
	
	
	if ls_memno <> ls_memprior or ls_sliptype <> ls_slipprior then
		ls_slipno			= "TP"+ls_shortyear+right( "000000"+string( ll_index ), 7 )

		ls_memprior	= ls_memno
		ls_slipprior		= ls_sliptype
	end if
	
	ads_trnpayin.setitem( ll_index, "payinslip_no", ls_slipno )
next

return 1
end function

public function integer of_proc_trnpayin (ref str_proctrnpayin astr_proctrnpayin) throws Exception;string		ls_source, ls_refslipno, ls_slipno, ls_receiptno
string		ls_memcoopid, ls_memno, ls_sliptype, ls_shrtypecode, ls_contcoopid, ls_contno, ls_memprior
string		ls_memgroup, ls_subgroup, ls_trnitemcode, ls_itemcode, ls_desc
string		ls_moneytype, ls_tofromaccid, ls_entryid, ls_sliplist[]
integer	li_trnpaytype, li_lastperiod, li_shrstatus, li_seqno, li_seqnomst
long		ll_count, ll_index, ll_row, ll_rowdet, ll_slipcnt
dec{2}	ldc_prnpay, ldc_intpay, ldc_itempay, ldc_shrstkamt, ldc_unitshare, ldc_shrstkvalue
dec{2}	ldc_prnbal, ldc_intperiod, ldc_intarrear, ldc_intall, ldc_shrstkbegin, ldc_intaccum
datetime	ldtm_trndate, ldtm_opedate, ldtm_entrydate, ldtm_lastcalint
n_ds		lds_trnpayin, lds_payinslip, lds_payinslipdet, lds_infocont

ls_source		= astr_proctrnpayin.source_code
ldtm_trndate	= astr_proctrnpayin.trans_date
ls_entryid		= astr_proctrnpayin.entry_id

ldtm_entrydate	= datetime( today(), now() )

lds_trnpayin		= create n_ds
lds_trnpayin.dataobject	= "d_loansrv_proc_trnpayin"
lds_trnpayin.settransobject( itr_sqlca )

lds_trnpayin.retrieve( is_coopcontrol, ls_source, ldtm_trndate )

ll_count	= lds_trnpayin.rowcount()

if ll_count <= 0 then
	destroy lds_trnpayin
	ithw_exception.text	= "ไม่พบการโอนข้อมูลสำหรับประมวลผลรับชำระประจำวันที่ "+string( ldtm_trndate, "dd/mm/" )+string( year( date( ldtm_trndate ) )+543 )+" ระบบต้นทาง("+ls_source+") "
	throw ithw_exception
end if

// สำหรับดึงข้อมูลสัญญาเงินกู้
lds_infocont = create n_ds
lds_infocont.dataobject = "d_loansrv_info_contract"
lds_infocont.settransobject( itr_sqlca )

// รายละเอียดรายการ Slip
ids_infosliptype	= create n_ds
ids_infosliptype.dataobject	= "d_loansrv_attrib_ucfsliptype"
ids_infosliptype.settransobject( itr_sqlca )
ids_infosliptype.retrieve()

// Slip สำหรับบันทึก
lds_payinslip	= create n_ds
lds_payinslip.dataobject	= "d_loansrv_lnpayin_slip"
lds_payinslip.settransobject( itr_sqlca )

lds_payinslipdet	= create n_ds
lds_payinslipdet.dataobject	= "d_loansrv_lnpayin_slipdet"
lds_payinslipdet.settransobject( itr_sqlca )

this.of_setsrvdoccontrol( true )
this.of_setsrvlninterest( true )
this.of_setsrvlnoperate( true )

ll_slipcnt			= 0

lds_payinslip.reset()
lds_payinslipdet.reset()

for ll_index = 1 to ll_count
	
	ls_memcoopid		= lds_trnpayin.getitemstring( ll_index, "memcoop_id" )
	ls_memno			= lds_trnpayin.getitemstring( ll_index, "member_no" )
	li_seqnomst			= lds_trnpayin.getitemnumber( ll_index, "seq_no" )
	ls_sliptype			= lds_trnpayin.getitemstring( ll_index, "sliptype_code" )
	ls_trnitemcode		= lds_trnpayin.getitemstring( ll_index, "transitem_code" )
	ldtm_opedate		= lds_trnpayin.getitemdatetime( ll_index, "realpay_date" )
	ls_shrtypecode		= lds_trnpayin.getitemstring( ll_index, "shrtype_code" )
	ls_contcoopid		= lds_trnpayin.getitemstring( ll_index, "concoop_id" )
	ls_contno			= lds_trnpayin.getitemstring( ll_index, "loancontract_no" )

	ls_moneytype		= lds_trnpayin.getitemstring( ll_index, "moneytype_code" )
	ls_tofromaccid		= lds_trnpayin.getitemstring( ll_index, "tofrom_accid" )
	ls_refslipno			= lds_trnpayin.getitemstring( ll_index, "trnsource_refslipno" )
	
	ls_memgroup		= lds_trnpayin.getitemstring( ll_index, "membgroup_code" )
	
	ldc_shrstkamt		= lds_trnpayin.getitemdecimal( ll_index, "sharestk_amt" )
	ldc_shrstkbegin		= lds_trnpayin.getitemdecimal( ll_index, "sharebegin_amt" )
	ldc_intaccum		= lds_trnpayin.getitemdecimal( ll_index, "accum_interest" )
	
	li_trnpaytype		= lds_trnpayin.getitemnumber( ll_index, "transpay_type" )
	ldc_prnpay			= lds_trnpayin.getitemdecimal( ll_index, "principal_trnamt" )
	ldc_intpay			= lds_trnpayin.getitemdecimal( ll_index, "interest_trnamt" )
	ldc_itempay			= lds_trnpayin.getitemdecimal( ll_index, "trans_amt" )
	
	if ls_memno <> ls_memprior then
		ll_slipcnt			= ll_slipcnt + 1
		ll_row				= lds_payinslip.insertrow( 0 )
		li_seqno			= 0
		ls_memprior	= ls_memno

		// ขอเลขที่ Slip และ Receipt No
		ls_slipno			= inv_docsrv.of_getnewdocno( is_coopcontrol, "SLSLIPPAYIN" )
		ls_receiptno		= inv_docsrv.of_getnewdocno( is_coopcontrol, "SLRECEIPTNO" )
		
		lds_payinslip.setitem( ll_row, "coop_id", is_coopcontrol )
		lds_payinslip.setitem( ll_row, "payinslip_no", ls_slipno )
		lds_payinslip.setitem( ll_row, "memcoop_id", ls_memcoopid )
		lds_payinslip.setitem( ll_row, "member_no", ls_memno )
		lds_payinslip.setitem( ll_row, "document_no", ls_receiptno )
		lds_payinslip.setitem( ll_row, "sliptype_code", ls_sliptype )
		lds_payinslip.setitem( ll_row, "slip_date", ldtm_trndate )
		lds_payinslip.setitem( ll_row, "slip_amt", ldc_itempay )
		lds_payinslip.setitem( ll_row, "operate_date", ldtm_opedate )
		lds_payinslip.setitem( ll_row, "moneytype_code", ls_moneytype )
		lds_payinslip.setitem( ll_row, "sharestkbf_value", ldc_shrstkbegin * 10 )
		lds_payinslip.setitem( ll_row, "sharestk_value", ldc_shrstkamt * 10 )
		lds_payinslip.setitem( ll_row, "intaccum_amt", ldc_intaccum )
		
		if ls_moneytype = "TRN" then
			lds_payinslip.setitem( ll_row, "ref_system", ls_source )
			lds_payinslip.setitem( ll_row, "ref_slipno", ls_refslipno )
			lds_payinslip.setitem( ll_row, "ref_slipamt", ldc_itempay )
		else
			lds_payinslip.setitem( ll_row, "tofrom_accid", ls_tofromaccid )
		end if
		
		lds_payinslip.setitem( ll_row, "membgroup_code", ls_memgroup )
		lds_payinslip.setitem( ll_row, "entry_id", ls_entryid )
		lds_payinslip.setitem( ll_row, "entry_date", ldtm_entrydate )
		
		ls_sliplist[ ll_slipcnt ]		= ls_slipno
	end if
	
	li_seqno			= li_seqno + 1
	
	ll_rowdet	= lds_payinslipdet.insertrow( 0 )

	lds_payinslipdet.setitem( ll_rowdet, "coop_id", is_coopcontrol )
	lds_payinslipdet.setitem( ll_rowdet, "payinslip_no", ls_slipno )
	lds_payinslipdet.setitem( ll_rowdet, "seq_no", li_seqno )

	// ดึงข้อมูลตามประเภทการชำระ
	choose case ls_trnitemcode
		case "SHR"
			// กำหนดค่าต่าง ๆ
			ls_itemcode	= string( this.of_getattribsliptype( ls_sliptype, "shstm_itemtype" ) )
			ls_desc		= string( this.of_getattribsliptype( ls_sliptype, "shpay_desc" ) )
			
			select		shsharemaster.sharestk_amt, shsharemaster.last_period,
						shsharemaster.sharemaster_status, shsharetype.unitshare_value
			into		:ldc_shrstkamt, :li_lastperiod, :li_shrstatus, :ldc_unitshare
			from		shsharemaster, shsharetype
			where	( shsharemaster.coop_id				= shsharetype.coop_id ) and
						( shsharemaster.sharetype_code	= shsharetype.sharetype_code ) and
						( shsharemaster.coop_id				= :ls_memcoopid ) and
						( shsharemaster.member_no		= :ls_memno )
			using		itr_sqlca ;
			
			ldc_shrstkvalue		= ldc_shrstkamt * ldc_unitshare
			
			lds_payinslipdet.object.shrlontype_code[ ll_rowdet ]	= ls_shrtypecode
			lds_payinslipdet.object.period[ ll_rowdet ]				= li_lastperiod
			lds_payinslipdet.object.bfperiod[ ll_rowdet ]				= li_lastperiod
			lds_payinslipdet.object.bfshrcont_status[ ll_rowdet ]	= li_shrstatus
			
			lds_payinslipdet.setitem( ll_rowdet, "operate_flag", 1 )
			lds_payinslipdet.setitem( ll_rowdet, "slipitemtype_code", ls_trnitemcode )
			lds_payinslipdet.setitem( ll_rowdet, "slipitem_desc", ls_desc )
			lds_payinslipdet.setitem( ll_rowdet, "principal_payamt", 0 )
			lds_payinslipdet.setitem( ll_rowdet, "interest_payamt", 0 )
			lds_payinslipdet.setitem( ll_rowdet, "item_payamt", ldc_itempay )
			lds_payinslipdet.setitem( ll_rowdet, "item_balance", ldc_shrstkvalue + ldc_itempay )
			lds_payinslipdet.setitem( ll_rowdet, "bfshrcont_balamt", ldc_shrstkvalue )
			lds_payinslipdet.setitem( ll_rowdet, "stm_itemtype", ls_itemcode )
			
		case "LON"
			// กำหนดค่าต่าง ๆ
			ls_itemcode	= string( this.of_getattribsliptype( ls_sliptype, "lnstm_itemtype" ) )
			ls_desc		= string( this.of_getattribsliptype( ls_sliptype, "lnpay_desc" ) )
			
			lds_infocont.retrieve( ls_contcoopid, ls_contno )
			
			ldc_prnbal		= lds_infocont.getitemdecimal( 1, "principal_balance" )
			ldc_intarrear	= lds_infocont.getitemdecimal( 1, "interest_arrear" )
			ldtm_lastcalint	= lds_infocont.getitemdatetime( 1, "lastcalint_date" )
			
			lds_payinslipdet.object.shrlontype_code[ ll_rowdet ]			= lds_infocont.object.loantype_code[ 1 ]
		
			lds_payinslipdet.object.rkeep_principal[ ll_rowdet ]			= lds_infocont.object.rkeep_principal[ 1 ]
			lds_payinslipdet.object.rkeep_interest[ ll_rowdet ]				= lds_infocont.object.rkeep_interest[ 1 ]
			lds_payinslipdet.object.nkeep_interest[ ll_rowdet ]			= lds_infocont.object.nkeep_interest[ 1 ]
			lds_payinslipdet.object.period[ ll_rowdet ]						= lds_infocont.object.last_periodpay[ 1 ]+1
			lds_payinslipdet.object.calint_from[ ll_rowdet ]					= lds_infocont.object.lastcalint_date[ 1 ]
			
			lds_payinslipdet.object.bfperiod[ ll_rowdet ]						= lds_infocont.object.last_periodpay[ 1 ]
			lds_payinslipdet.object.bfintarr_amt[ ll_rowdet ]				= lds_infocont.object.interest_arrear[ 1 ]
			lds_payinslipdet.object.bfintarrset_amt[ ll_rowdet ]			= lds_infocont.object.intyear_arrear[ 1 ]
			lds_payinslipdet.object.bfwithdraw_amt[ ll_rowdet ]			= lds_infocont.object.withdrawable_amt[ 1 ]
			lds_payinslipdet.object.bfperiod_payment[ ll_rowdet ]		= lds_infocont.object.period_payment[ 1 ]
			lds_payinslipdet.object.bfshrcont_status[ ll_rowdet ]			= lds_infocont.object.contract_status[ 1 ]
			lds_payinslipdet.object.bfcontlaw_status[ ll_rowdet ]			= lds_infocont.object.contlaw_status[ 1 ]
			lds_payinslipdet.object.bflastcalint_date[ ll_rowdet ]			= lds_infocont.object.lastcalint_date[ 1 ]
			lds_payinslipdet.object.bflastproc_date[ ll_rowdet ]			= lds_infocont.object.lastprocess_date[ 1 ]
			lds_payinslipdet.object.bfcountpay_flag[ ll_rowdet ]			= lds_infocont.object.countpay_flag[ 1 ]
			
			lds_payinslipdet.object.bfpayspec_method[ ll_rowdet ]		= lds_infocont.object.payspec_method[ 1 ]
			lds_payinslipdet.object.bfintreturn_flag[ ll_rowdet ]			= lds_infocont.object.intreturn_flag[ 1 ]
			lds_payinslipdet.object.bfpxaftermthkeep_type[ ll_rowdet ]	= lds_infocont.object.pxaftermthkeep_type[ 1 ]
			
			lds_payinslipdet.setitem( ll_rowdet, "operate_flag", 1 )
			lds_payinslipdet.setitem( ll_rowdet, "slipitemtype_code", "LON" )
			lds_payinslipdet.setitem( ll_rowdet, "slipitem_desc", ls_desc )
			lds_payinslipdet.setitem( ll_rowdet, "loancontract_no", ls_contno )
			lds_payinslipdet.setitem( ll_rowdet, "concoop_id", ls_contcoopid )
			
			// คิด ด/บ ไว้ก่อน
			ldc_intperiod		= inv_intsrv.of_computeinterest( ls_contcoopid, ls_contno, ldtm_trndate )
			
			lds_payinslipdet.setitem( ll_rowdet, "prncalint_amt", ldc_prnbal )
			lds_payinslipdet.setitem( ll_rowdet, "calint_from", ldtm_lastcalint )
			lds_payinslipdet.setitem( ll_rowdet, "calint_to", ldtm_opedate )
			lds_payinslipdet.setitem( ll_rowdet, "interest_period", ldc_intperiod )
			
			// ดูว่าชำระดูจากยอดรวมหรือเปล่า
			if li_trnpaytype = 1 then
				ldc_intall			= ( ldc_intperiod + ldc_intarrear )
				
				if ldc_intall > ldc_itempay then
					ldc_intpay	= ldc_itempay
				else
					ldc_intpay	= ldc_intall
				end if
				
				ldc_prnpay		= ldc_itempay - ( ldc_intperiod + ldc_intarrear )
			end if
			
//			ldc_inttemp	= ( adc_principal * adc_intrate * li_dayinterest ) / ( li_daysinyear + ( adc_intrate * li_dayinterest ) )
//			ดอกเบี้ย = ( ยอดชำระ * อัตราดอกเบี้ย * จำนวนวันที่คิดดอกเบี้ย ) / ( จำนวนวันทั้งปี + ( อัตราดอกเบี้ย * จำนวนวันที่คิดดอกเบี้ย ))
			
			lds_payinslipdet.setitem( ll_rowdet, "principal_payamt", ldc_prnpay )
			lds_payinslipdet.setitem( ll_rowdet, "interest_payamt", ldc_intpay )
			lds_payinslipdet.setitem( ll_rowdet, "item_payamt", ldc_itempay )
			lds_payinslipdet.setitem( ll_rowdet, "item_balance", ldc_prnbal - ldc_prnpay )
			
			lds_payinslipdet.setitem( ll_rowdet, "bfshrcont_balamt", ldc_prnbal )
			lds_payinslipdet.setitem( ll_rowdet, "stm_itemtype", ls_itemcode )
		
		case else
	end choose
	
	update	sltranspayin
	set			post_status		= 1,
				post_date		= :ldtm_entrydate,
				payinslip_no		= :ls_slipno
	where	( coop_id			= :is_coopcontrol ) and
				( memcoop_id	= :ls_memcoopid ) and
				( member_no	= :ls_memno ) and
				( trans_date		= :ldtm_trndate ) and
				( seq_no			= :li_seqnomst )
	using		itr_sqlca ;
	
next

// บันทึก Slip
if lds_payinslip.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้"
	ithw_exception.text	+= lds_payinslip.of_geterrormessage()
	rollback using itr_sqlca ;
	throw ithw_exception
end if

// บันทึก Slip Detail
if lds_payinslipdet.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDet ไม่ได้"
	ithw_exception.text	+= lds_payinslipdet.of_geterrormessage()
	rollback using itr_sqlca ;
	throw ithw_exception
end if

ll_count		= upperbound( ls_sliplist )

for ll_index = 1 to ll_count
	ls_slipno		= ls_sliplist[ ll_index ]
	
	lds_payinslip.setfilter( "payinslip_no = '"+ls_slipno+"'" )
	lds_payinslip.filter()

	lds_payinslipdet.setfilter( "payinslip_no = '"+ls_slipno+"'" )
	lds_payinslipdet.filter()
	
	try
		inv_lnoperatesrv.of_postslip( trim( ls_sliptype ), lds_payinslip, lds_payinslipdet ,ls_contcoopid)//by a
	catch( Exception lthw_error )
		rollback using itr_sqlca ;
		throw lthw_error
	end try
	
next

if ll_count > 0 then
	astr_proctrnpayin.payinslip_startno	=  ls_sliplist[ 1 ]
	astr_proctrnpayin.payinslip_endno		=  ls_sliplist[ ll_count ]
end if

commit using itr_sqlca ;

destroy lds_payinslip
destroy lds_payinslipdet

this.of_setsrvlnoperate( false )
this.of_setsrvdoccontrol( false )
this.of_setsrvlninterest( false )

return 1
end function

on n_cst_loansrv_process.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_loansrv_process.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event destructor;// ส่วนบริการการนำเข้าข้อมูล
this.of_setsrvdwxmlie( false )
if isvalid( inv_progress ) then destroy inv_progress
end event

event constructor;/***************************************************************
<object>
เป็น Object ที่รวบรวมฟังก์ชั่นสำหรับการทำงานที่เกี่ยวข้องกับการทำรายการเงินกู้ต่างๆ
เช่นการจ่ายเงินกู้ การชำระ การโอนหนี้ การยกเลิก การปรับปรุง ฯลฯ
เมื่อมีการประกาศ Object นี้เสร็จแล้วจำเป็นที่จะต้องเรียกใช้ฟังก์ชั่น
of_initservice( transection ) เพื่อกำหนดตัวแปรหรือค่าคงที่ต่างๆที่จำเป็นสำหรับ
การทำงานของ Object 
</object>
	  
<author>
	Initial Version by Oh ho
</author>
 
<usage>  
  	ตัวอย่างการเรียกใช้งาน
	n_cst_loansrv_lnoperate		lnv_lnoperate 
	lnv_lnoperate	= create n_cst_loansrv_lnoperate
	lnv_lnoperate.initservice( transection ) 
</usage> 
****************************************************************/ 

// ประกาศ Throw สำหรับ Err
ithw_exception	= create Exception
end event
