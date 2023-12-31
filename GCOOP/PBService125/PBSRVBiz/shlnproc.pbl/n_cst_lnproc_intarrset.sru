$PBExportHeader$n_cst_lnproc_intarrset.sru
forward
global type n_cst_lnproc_intarrset from nonvisualobject
end type
end forward

global type n_cst_lnproc_intarrset from nonvisualobject
end type
global n_cst_lnproc_intarrset n_cst_lnproc_intarrset

type variables
string			is_arg[]
integer		ii_rangetype
datetime		idtm_balancedate
n_ds			ids_intsetattrib

transaction	itr_sqlca
Exception	ithw_exception

n_cst_dbconnectservice		inv_connection
n_cst_progresscontrol		inv_progress
n_cst_stringservice			inv_stringsrv
n_cst_loansrv_interest		inv_intsrv
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans)
public function integer of_procintset (string as_xmlintsetcriteria, string as_entryid) throws Exception
private function integer of_setsrvlninterest (boolean ab_switch)
public function integer of_procintset_year () throws Exception
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans);// Service Transection
if isnull( inv_connection ) or not isvalid( inv_connection ) then
	inv_connection	= create n_cst_dbconnectservice
	inv_connection	= anv_dbtrans
end if

itr_sqlca 		= inv_connection.itr_dbconnection

ids_intsetattrib	= create n_ds
ids_intsetattrib.dataobject = "d_lnproc_intset_criteria"

inv_stringsrv	= create n_cst_stringservice

inv_progress	= create n_cst_progresscontrol

return 1
end function

public function integer of_procintset (string as_xmlintsetcriteria, string as_entryid) throws Exception;/***********************************************************
<description>
	สำหรับประมวลผลหนังสือยืนยันยอดหุ้น,หนี้,เงินฝาก,การค้ำประกัน
</description>

<arguments>  
	as_xmlintsetcriteria	String		เงื่อนไขการประมวลผลหนังสือยืนยันยอด
	as_entryid				String		ผู้ที่ทำการประมวลผล
</arguments> 

<return> 
	1						Integer	ถ้าไม่มีข้อผิดพลาด
</return> 

<usage> 
	ส่งเงื่อนไขที่อยู่ในรูปแบบ XML พร้อม ผู้ที่ทำการประมวลผลเข้ามา
	ระบบจะทำการ Process ข้อมูลแล้วบันทึกลงฐานข้อมูลเอาไว้
	
	string		ls_xmlbalcriteria, ls_entryid
	
	ls_xmlbalcriteria	= dw_criteria.describe( "Datawindow.Data.XML" )
	ls_entryid			= entry_id
	
	lnv_lnoperate.of_procconfirmbalance( ls_xmlbalcriteria, ls_entryid )
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 20/9/2010 by OhhO

</usage> 
***********************************************************/
string		ls_grptext, ls_memtext

ids_intsetattrib.reset()
ids_intsetattrib.importstring( XML!, as_xmlintsetcriteria )

if ids_intsetattrib.rowcount() <= 0 then
	ithw_exception.text	="ไม่พบข้อมูลเงือนไขการประมวลผล"
	throw ithw_exception
end if

ii_rangetype		= ids_intsetattrib.getitemnumber( 1, "rangedata_type" )
ls_grptext		= ids_intsetattrib.getitemstring( 1, "rangedata_grp" )
ls_memtext		= ids_intsetattrib.getitemstring( 1, "rangedata_mem" )

try
	// ถ้าประมวลผลแบบย่อย
	choose case ii_rangetype
		case 2	// ตามกลุ่ม
			inv_stringsrv.of_analyzestring( ls_grptext, is_arg[] )
		case 3 // ตามทะเบียน
			inv_stringsrv.of_analyzestring( ls_memtext, is_arg[] )
	end choose
	
	this.of_procintset_year()

catch( Exception lthw_error )
	rollback using itr_sqlca ;
	throw lthw_error
end try

// ถ้าไม่มี Error เลย
commit using itr_sqlca ;

return 1
end function

private function integer of_setsrvlninterest (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_intsrv ) or not isvalid( inv_intsrv ) then
		inv_intsrv	= create n_cst_loansrv_interest
		inv_intsrv.of_initservice( inv_connection )
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

public function integer of_procintset_year () throws Exception;string		ls_memno, ls_contno, ls_loantype,ls_coopid
string		ls_lntypetext, ls_loanfilter[]
long		ll_index, ll_count
integer	li_accyear, li_accmonth
dec{2}	ldc_prnbal, ldc_bfintyeararr, ldc_bfintarr, ldc_intamt, ldc_intyeararrbal, ldc_intarrbal
n_ds		lds_contdata
datetime	ldtm_lastcalint, ldtm_calintto, ldtm_procdate

ls_lntypetext		= ids_intsetattrib.getitemstring( 1, "loantype_text" )
li_accyear		= ids_intsetattrib.getitemnumber( 1, "account_year" )
li_accmonth		= ids_intsetattrib.getitemnumber( 1, "account_month" )
ldtm_procdate	= ids_intsetattrib.getitemdatetime( 1, "intproc_date" )
ldtm_calintto	= ids_intsetattrib.getitemdatetime( 1, "calintto_date" )

inv_progress.istr_progress.progress_text		= "ประมวลผลตั้งดอกเบี้ยค้างรับ"
inv_progress.istr_progress.subprogress_text	= "กำลัง Clear ข้อมูลเก่า..."

delete from lnsetintarryear
where	( account_year	= :li_accyear ) and
			( account_month	= :li_accmonth )
using		itr_sqlca ;

inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลสัญญาที่จะประมวลผลตั้งดอกเบี้ยค้างรับ..."

//จะตั้งค้างเฉพาะสัญญาปกติเท่านั้น ส่วนสงสัยจะสูญ, ดำเนินคดี, พิพากษา ไม่ต้องตั้ง
lds_contdata	= create n_ds
lds_contdata.dataobject	= "d_lnproc_intset_info_contractall"
lds_contdata.settransobject( itr_sqlca )

// กรองให้เหลือแต่ประเภทเงินกู้ที่ต้องการ
inv_stringsrv.of_parsetoarray( ls_lntypetext, ",", ls_loanfilter )

ll_count = lds_contdata.retrieve( ls_loanfilter )

inv_progress.istr_progress.subprogress_max	= ll_count

this.of_setsrvlninterest( true )

for ll_index = 1 to ll_count
	yield()
	if inv_progress.of_is_stop() then
		destroy lds_contdata
		rollback using itr_sqlca ;
		return 0
	end if

	ls_memno			= lds_contdata.getitemstring( ll_index, "member_no" )
	ls_contno			= lds_contdata.getitemstring( ll_index, "loancontract_no" )
	ls_coopid= lds_contdata.getitemstring( ll_index, "coop_id" )
	ls_loantype			= lds_contdata.getitemstring( ll_index, "loantype_code" )
	ldc_prnbal			= lds_contdata.getitemdecimal( ll_index, "principal_balance" )
	ldc_bfintyeararr	= lds_contdata.getitemdecimal( ll_index, "intyear_arrear" )
	ldc_bfintarr			= lds_contdata.getitemdecimal( ll_index, "interest_arrear" )
	ldtm_lastcalint		= lds_contdata.getitemdatetime( ll_index, "lastcalint_date" )
	
	inv_progress.istr_progress.subprogress_index	= ll_index	
	inv_progress.istr_progress.subprogress_text	= "ตั้งยอดดอกเบี้ยค้างรับ สัญญา "+ls_contno

	ldc_intamt	= inv_intsrv.of_computeinterest( ls_contno, ls_coopid,ldc_prnbal, ldtm_lastcalint, ldtm_calintto )		
	
	if ldc_intamt <= 0 then
		continue
	end if
	
	insert into lnsetintarryear
				( account_year, account_month, setintyear_date, member_no, loancontract_no,
				princalint_amt, bfprinbal_amt, bfintarryear_amt, bfintarr_amt, calintfrom_date, calintto_date, intarryear_amt )
	values	( :li_accyear, :li_accmonth, :ldtm_procdate, :ls_memno, :ls_contno,
			  	:ldc_prnbal, :ldc_prnbal, :ldc_bfintyeararr, :ldc_bfintarr, :ldtm_lastcalint, :ldtm_calintto, :ldc_intamt )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถเพิ่มรายการดอกเบี้ยค้างรับได้ (Setintarryear)"+itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
	
	ldc_intarrbal			= ldc_bfintarr + ldc_intamt
	ldc_intyeararrbal	= ldc_bfintyeararr + ldc_intamt
	
	update	lncontmaster
	set			lastcalint_date	= :ldtm_calintto,
				intyear_arrear	= :ldc_intyeararrbal,
				interest_arrear	= :ldc_intarrbal
	where	( loancontract_no	= :ls_contno )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถปรับปรุงยอดดอกเบี้ยค้างรับที่ตัวสัญญาได้~n"+itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
	
next

destroy lds_contdata

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception;anv_progress = inv_progress

return 1
end function

on n_cst_lnproc_intarrset.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_lnproc_intarrset.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception
end event

