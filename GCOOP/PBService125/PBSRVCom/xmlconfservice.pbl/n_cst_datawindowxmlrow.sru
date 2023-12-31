$PBExportHeader$n_cst_datawindowxmlrow.sru
$PBExportComments$DatawindowXMLRow Parser v.1.0.1.
forward
global type n_cst_datawindowxmlrow from n_cst_base
end type
end forward

global type n_cst_datawindowxmlrow from n_cst_base descriptor "PB_ObjectCodeAssistants" = "{1E00F051-675A-11D2-BCA5-000086095DDA}" 
end type
global n_cst_datawindowxmlrow n_cst_datawindowxmlrow

type variables

Protected:
n_cst_xml inv_xml		//เมื่อเรียก setxml แล้วเท่านั้น.
long il_row				//row ID.
end variables

forward prototypes
public function string of_getitem (integer ai_item)
public function long of_colcount ()
public function integer of_setxml (n_cst_xml anv_xmlrowelement, long al_rownumber)
public function string of_getitemname (integer ai_item)
end prototypes

public function string of_getitem (integer ai_item);//ขอข้อมูลในคอลัมน์ในลำดับตาม ai_item.
string ls_value
n_cst_xml lnv_column
lnv_column = inv_xml.getelement( ai_item )
if( not isvalid( lnv_column ) )then
	setnull( ls_value )
	return ls_value
end if
ls_value = lnv_column.text
return ls_value

end function

public function long of_colcount ();if( isvalid( inv_xml )) then
	return inv_xml.gettotalelements( )
end if
return 0
end function

public function integer of_setxml (n_cst_xml anv_xmlrowelement, long al_rownumber);//กำหนด XML สำหรับ 1 row
inv_xml = anv_xmlrowelement
if( isvalid( inv_xml ) )then
	return -1
end if
il_row = al_rownumber
return 1

end function

public function string of_getitemname (integer ai_item);//ชื่อข้อมูลในคอลัมน์ในลำดับตาม ai_item.
string ls_value
n_cst_xml lnv_column
lnv_column = inv_xml.getelement( ai_item )
if( not isvalid( lnv_column ) )then
	setnull( ls_value )
	return ls_value
end if
ls_value = lnv_column.tag
return ls_value

end function

on n_cst_datawindowxmlrow.create
call super::create
end on

on n_cst_datawindowxmlrow.destroy
call super::destroy
end on

