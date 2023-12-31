$PBExportHeader$n_cst_lnproc_intchanged.sru
forward
global type n_cst_lnproc_intchanged from nonvisualobject
end type
end forward

global type n_cst_lnproc_intchanged from nonvisualobject
end type
global n_cst_lnproc_intchanged n_cst_lnproc_intchanged

type variables
string			is_arg[]
integer		ii_rangetype
datetime		idtm_balancedate
n_ds			ids_intchgattrib

transaction	itr_sqlca
Exception	ithw_exception

n_cst_dbconnectservice		inv_connection
n_cst_progresscontrol		inv_progress
n_cst_stringservice			inv_stringsrv
n_cst_loansrv_interest		inv_intsrv
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans)
public function integer of_procintchg (string as_xmlintsetcriteria, string as_entryid) throws Exception
private function integer of_setsrvlninterest (boolean ab_switch)
public function integer of_procintchg_rate () throws Exception
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans);// Service Transection
if isnull( inv_connection ) or not isvalid( inv_connection ) then
	inv_connection	= create n_cst_dbconnectservice
	inv_connection	= anv_dbtrans
end if

itr_sqlca 		= inv_connection.itr_dbconnection

ids_intchgattrib	= create n_ds
ids_intchgattrib.dataobject = "d_lnproc_intchg_criteria"

inv_stringsrv	= create n_cst_stringservice

inv_progress	= create n_cst_progresscontrol

return 1
end function

public function integer of_procintchg (string as_xmlintsetcriteria, string as_entryid) throws Exception;/***********************************************************
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

ids_intchgattrib.reset()
ids_intchgattrib.importstring( XML!, as_xmlintsetcriteria )

if ids_intchgattrib.rowcount() <= 0 then
	ithw_exception.text	="ไม่พบข้อมูลเงือนไขการประมวลผล"
	throw ithw_exception
end if

ii_rangetype		= ids_intchgattrib.getitemnumber( 1, "rangedata_type" )
ls_grptext		= ids_intchgattrib.getitemstring( 1, "rangedata_grp" )
ls_memtext		= ids_intchgattrib.getitemstring( 1, "rangedata_mem" )

try
	// ถ้าประมวลผลแบบย่อย
	choose case ii_rangetype
		case 2	// ตามกลุ่ม
			inv_stringsrv.of_analyzestring( ls_grptext, is_arg[] )
		case 3 // ตามทะเบียน
			inv_stringsrv.of_analyzestring( ls_memtext, is_arg[] )
	end choose
	
	this.of_procintchg_rate()

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

public function integer of_procintchg_rate () throws Exception;string		ls_memno, ls_contno, ls_loantype, ls_concode,ls_coopid
string		ls_lntypetext, ls_loanfilter[]
long		ll_index, ll_count
integer	li_accyear, li_accmonth
dec{2}	ldc_prnbal, ldc_bfintyeararr, ldc_bfintarr, ldc_intamt, ldc_intyeararrbal, ldc_intarrbal
dec{4}	ldc_newintrate, ldc_intrate
n_ds		lds_contdata
datetime	ldtm_lastcalint, ldtm_calintto, ldtm_procdate, ldtm_bflastcalint

ls_lntypetext		= ids_intchgattrib.getitemstring( 1, "loantype_text" )
ls_concode		= ids_intchgattrib.getitemstring( 1, "condition_code" )
ldc_newintrate	= ids_intchgattrib.getitemnumber( 1, "new_intrate" )
ldtm_procdate	= ids_intchgattrib.getitemdatetime( 1, "intproc_date" )

inv_progress.istr_progress.progress_text		= "ประมวลผลปรับอัตราดอกเบี้ย"
inv_progress.istr_progress.progress_max		= 1
inv_progress.istr_progress.subprogress_text	= "กำลังดึงข้อมูลสัญญาที่จะประมวลผลปรับอัตราดอกเบี้ย..."
inv_progress.istr_progress.status					= 8

//จะตั้งค้างเฉพาะสัญญาปกติเท่านั้น ส่วนสงสัยจะสูญ, ดำเนินคดี, พิพากษา ไม่ต้องตั้ง
lds_contdata	= create n_ds
lds_contdata.dataobject	= "d_lnproc_intchg_info_contractall"
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
	ls_coopid             = lds_contdata.getitemstring( ll_index, "coop_id" )
	ls_contno			= lds_contdata.getitemstring( ll_index, "loancontract_no" )
	ls_loantype			= lds_contdata.getitemstring( ll_index, "loantype_code" )
	ldc_prnbal			= lds_contdata.getitemdecimal( ll_index, "principal_balance" )
	ldc_bfintarr			= lds_contdata.getitemdecimal( ll_index, "interest_arrear" )
	ldc_intrate			= lds_contdata.getitemdecimal( ll_index, "int_contintrate" )
	ldtm_bflastcalint	= lds_contdata.getitemdatetime( ll_index, "lastcalint_date" )
	
	inv_progress.istr_progress.subprogress_index	= ll_index	
	inv_progress.istr_progress.subprogress_text	= "ปรับอัตราดอกเบี้ยของสัญญา "+ls_contno

	choose case ls_concode
		case "HIGHER"
			if ldc_intrate < ldc_newintrate then
				continue
			end if
		case "LOWER"
			if ldc_intrate > ldc_newintrate then
				continue
			end if
	end choose
	
	// ตรวจว่าถ้าวันที่คิด ด/บ ถึงน้อยกว่าวันที่เปลี่ยนต้องตั้ง ด/บ ค้างเอาไว้
	if ldtm_bflastcalint < ldtm_procdate then
		ldc_intamt		= inv_intsrv.of_computeinterest( ls_contno,ls_coopid, ldc_prnbal, ldtm_lastcalint, ldtm_procdate )
		ldtm_lastcalint	= ldtm_procdate
	else
		ldtm_lastcalint	= ldtm_bflastcalint
		ldc_intamt		= 0
	end if
	
	ldc_intarrbal			= ldc_bfintarr + ldc_intamt
	
	update	lncontmaster
	set			lastcalint_date	= :ldtm_lastcalint,
				interest_arrear	= :ldc_intarrbal,
				int_contintrate	= :ldc_newintrate
	where	( loancontract_no	= :ls_contno )
	using		itr_sqlca ;
	
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถเปลี่ยนแปลงอัตราดอกเบี้ยใหม่ของสัญญาได้ สัญญาเลขที่ "+ls_contno+" ~n"+itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
	
	insert into lncontchgintrate
				( loancontract_no, chgintrate_date, old_intrate, new_intrate, lastcalint_date, intarrear_amt )
	values	( :ls_contno, :ldtm_procdate, :ldc_intrate, :ldc_newintrate, :ldtm_bflastcalint, :ldc_intamt )
	using		itr_sqlca ;

	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถบันทึกรายการเปลี่ยนแปลงอัตราดอกเบี้ยได้ สัญญาเลขที่ "+ls_contno+" ~n"+itr_sqlca.sqlerrtext
		throw ithw_exception
	end if
next

inv_progress.istr_progress.status					= 1

destroy lds_contdata

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws Exception;anv_progress = inv_progress

return 1
end function

on n_cst_lnproc_intchanged.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_lnproc_intchanged.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

