$PBExportHeader$n_cst_tradesrv_search.sru
$PBExportComments$ค้นหาข้อมูล
forward
global type n_cst_tradesrv_search from nonvisualobject
end type
end forward

global type n_cst_tradesrv_search from nonvisualobject
end type
global n_cst_tradesrv_search n_cst_tradesrv_search

type variables
Public:

transaction						itr_sqlca
throwable						ithw_exception

n_cst_dbconnectservice		inv_transection
n_cst_dwxmlieservice			inv_dwxmliesrv
n_cst_doccontrolservice		inv_docsrv
end variables

forward prototypes
private function integer of_setsrvdwxmlie (boolean ab_switch)
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws throwable
private function integer of_setsrvdoccontrol (boolean ab_switch)
public function integer of_search_stockslip_so (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_stockslip_po (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_stockslip_rc (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_stockslip_qt (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_stockslip_iv (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_stockslip_sr (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_stockslip_ap (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_stockslip_am (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_info_mem (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_info_debt (str_tradesrv_search astr_tradesrv_search) throws throwable
public function integer of_search_info_cred (str_tradesrv_search astr_tradesrv_search) throws throwable
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

public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws throwable;/***********************************************************
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

public function integer of_search_stockslip_so (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_stockslip_po (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_stockslip_rc (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_stockslip_qt (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_stockslip_iv (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_stockslip_sr (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_stockslip_ap (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_stockslip_am (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_info_mem (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_info_debt (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

public function integer of_search_info_cred (str_tradesrv_search astr_tradesrv_search) throws throwable;return 1
end function

on n_cst_tradesrv_search.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_tradesrv_search.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create throwable
end event

