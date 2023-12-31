$PBExportHeader$n_cst_keeping_memdet.sru
$PBExportComments$รายละเอียดสมาชิก
forward
global type n_cst_keeping_memdet from nonvisualobject
end type
end forward

global type n_cst_keeping_memdet from nonvisualobject
end type
global n_cst_keeping_memdet n_cst_keeping_memdet

type variables
Public:

transaction						itr_sqlca
Exception						ithw_exception

n_cst_dbconnectservice		inv_transection
n_cst_dwxmlieservice			inv_dwxmliesrv
n_cst_procservice				inv_procsrv
n_cst_doccontrolservice		inv_docsrv
end variables

forward prototypes
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception
public function integer of_init_kpmast_detail (ref str_keep_detail astr_keep_detail) throws Exception
protected function integer of_setdsmodify (ref n_ds ads_requester, boolean ab_keymodify)
protected function integer of_setsrvdwxmlie (boolean ab_switch)
protected function integer of_setsrvproc (boolean ab_switch)
end prototypes

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws Exception;/***********************************************************
<description>
	ไว้สำหรับเริ่มข้อมูลของ service ทำรายการเกี่ยวกับปันผล
</description>

<arguments>  
	atr_dbtrans					n_cst_dbconnectservice		user object สำหรับต่อฐานข้อมูล
</arguments> 

<return> 
	1								Integer		ถ้าไม่มีข้อมูลผิดพลาด
</return> 

<usage>
	สำหรับเริ่มข้อมูลของ service ทำรายการเกี่ยวกับปันผล
	ตัวอย่าง
	
	n_cst_dbconnectservice inv_db
	lnv_service = create n_cst_divavgoperate_service
	lnv_service.of_initservice( inv_db )
	
	----------------------------------------------------------------------
	Revision History:
	Version 1.0 (Initial version) Modified Date 16/11/2010 by Godji

</usage>

***********************************************************/
itr_sqlca = anv_dbtrans.itr_dbconnection

if isnull( inv_transection ) or not isvalid( inv_transection ) then
	inv_transection = create n_cst_dbconnectservice
	inv_transection = anv_dbtrans
end if

return 1
end function

public function integer of_init_kpmast_detail (ref str_keep_detail astr_keep_detail) throws Exception;string ls_xmlmain
string ls_coopid , ls_divyear , ls_memno
boolean lb_err = false

n_ds lds_det_main , lds_det_detail

this.of_setsrvproc( true )
this.of_setsrvdwxmlie( true )

lds_det_main = create n_ds
lds_det_main.dataobject = "d_kp_det_main"
lds_det_main.settransobject( itr_sqlca )

lds_det_detail = create n_ds
lds_det_detail.dataobject = "d_kp_det_detail"
lds_det_detail.settransobject( itr_sqlca )

ls_xmlmain		= astr_keep_detail.xml_main

if inv_dwxmliesrv.of_xmlimport( lds_det_main , ls_xmlmain ) < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการนำเข้าข้อมูล(0.1)"
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

ls_coopid			= lds_det_main.object.coop_id[1]
ls_memno		= lds_det_main.object.member_no[1]

// set ค่าในการดึงข้อมูลสมาชิก
try
	inv_procsrv.of_set_sqldw_column( lds_det_main, " and mbmembmaster.coop_id = '" + ls_coopid + "' " ) // ใส่เงื่อนไขอื่นๆ
	inv_procsrv.of_set_sqldw_column( lds_det_main, " and mbmembmaster.member_no = '" + ls_memno + "' " ) // ใส่เงื่อนไขอื่นๆ
	inv_procsrv.of_set_sqldw_column( lds_det_detail, " and kpmastreceive.memcoop_id = '" + ls_coopid + "' " ) // ใส่เงื่อนไขอื่นๆ
	inv_procsrv.of_set_sqldw_column( lds_det_detail, " and kpmastreceive.member_no = '" + ls_memno + "' " ) // ใส่เงื่อนไขอื่นๆ
catch( Exception lthw_setdwsql )
	ithw_exception.text	+= "~r~nพบขอผิดพลาด (0.2)"
	ithw_exception.text	+= lthw_setdwsql.text
	lb_err = true
end try
if lb_err then Goto objdestroy

if lds_det_main.retrieve() < 1 then
	ithw_exception.text = "~r~nพบขอผิดพลาดในการดึงข้อมูลรายละเอียดสมาชิก(รายละเอียดสมาชิก 10.1)"
	ithw_exception.text += "~r~nเลขสมาชิก : " + ls_memno
	ithw_exception.text += "~r~n" + lds_det_main.of_geterrormessage( )
	ithw_exception.text += "~r~nกรุณาตรวจสอบ"
	lb_err = true ; Goto objdestroy
end if

//lds_det_detail.retrieve()

astr_keep_detail.xml_main			= inv_dwxmliesrv.of_xmlexport(lds_det_main)
astr_keep_detail.xml_detail			= inv_dwxmliesrv.of_xmlexport(lds_det_detail)
astr_keep_detail.sql_select_detail	= lds_det_detail.getsqlselect()
objdestroy:
if isvalid(lds_det_main) then destroy lds_det_main
if isvalid(lds_det_detail) then destroy lds_det_detail

this.of_setsrvproc( false )
this.of_setsrvdwxmlie( false )

if lb_err then
	astr_keep_detail.xml_main			= ""
	astr_keep_detail.xml_detail			= ""
	astr_keep_detail.sql_select_detail	= ""
	rollback using itr_sqlca ;
	ithw_exception.text = "n_cst_keeping_memdet.of_init_kpmast_detail()" + ithw_exception.text
	throw ithw_exception
end if

return 1
end function

protected function integer of_setdsmodify (ref n_ds ads_requester, boolean ab_keymodify);string		ls_iskey
long		ll_index, ll_count , ll_row , ll_rwcount

ll_rwcount	= ads_requester.rowcount()
for ll_row = 1 to ll_rwcount
	ads_requester.setitemstatus( ll_row, 0, primary!, datamodified! )
	
	// ปรับ Attrib ทุก Column ให้เป็น Update ซะ
	ll_count	= long( ads_requester.describe( "DataWindow.Column.Count" ) )
	for ll_index = 1 to ll_count
		
		ls_iskey	= ads_requester.describe("#"+string( ll_index )+".Key")
		
		// ถ้าเป็น PK และเงื่อนไขคือไม่ปรับ Key ไม่ต้องปรับสถานะ
		if upper( ls_iskey ) = "YES" and not( ab_keymodify ) then
			continue
		end if
		
		ads_requester.setitemstatus( 1, ll_index, primary!, datamodified! )
	next
next

return 1
end function

protected function integer of_setsrvdwxmlie (boolean ab_switch);// Check argument
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

protected function integer of_setsrvproc (boolean ab_switch);// Check argument
if isnull( ab_switch ) then
	return -1
end if

if ab_switch then
	if isnull( inv_procsrv ) or not isvalid( inv_procsrv ) then
		inv_procsrv	= create n_cst_procservice
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

on n_cst_keeping_memdet.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_keeping_memdet.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create Exception
end event

