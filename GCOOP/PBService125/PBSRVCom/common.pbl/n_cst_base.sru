$PBExportHeader$n_cst_base.sru
$PBExportComments$เป็นคลาสพื้นฐานสำหรับสืบทอดคุณสมบัติและความสามารถไปใช้ในส่วนบริการที่ใช้มาตรฐานเดียวกันทั้งหมด เช่น PrintService, LoanService ฯ
forward
global type n_cst_base from nonvisualobject
end type
end forward

global type n_cst_base from nonvisualobject
end type
global n_cst_base n_cst_base

type variables
Protected:
n_cst_debuglog inv_debuglog
n_cst_error inv_lasterror
transaction	itr_transaction

end variables

forward prototypes
public subroutine of_setdebuglog (ref n_cst_debuglog anv_debuglog)
protected subroutine of_seterror (string as_message)
public function string of_getdebuglogxml ()
public function str_error of_getlasterror ()
protected subroutine of_debuglog (string as_logtext)
public function integer of_settransobject (ref transaction atr_transaction)
end prototypes

public subroutine of_setdebuglog (ref n_cst_debuglog anv_debuglog);/***********************************************
<description>
กำหนด DebugLog สำหรับใช้ debug การทำงานภายใน object นี้
</description>

<arguments>	
anv_debuglog   instance ของ n_cst_debuglog
</arguments>

<return>
</return>

<usage>
โดยปกติจำเป็นต้องส่ง instance ที่สมบูรณ์มาเท่านั้น แต่ถ้าหากส่ง instance ที่ไม่สมบูรณ์มาจะมีผลทำให้ debuglog ใน object นี้ถูกปิดการใช้งานโดยอัตโนมัติ.

lnv_object.of_setdebuglog( inv_debuglog )
lnv_object.of_log( "debug information" )
</usage>
************************************************/
if( isvalid( anv_debuglog ) )then
	inv_debuglog = anv_debuglog
else
	setnull( inv_debuglog )
end if

end subroutine

protected subroutine of_seterror (string as_message);//เมื่อเกิด errror ในคลาสนี้ก็ยังข้อความ error เก็บไว้ที่นี่.
if( not isvalid( inv_lasterror ) )then
	inv_lasterror = create n_cst_error
end if
inv_lasterror.of_seterror( this, as_message )

end subroutine

public function string of_getdebuglogxml ();/***********************************************
<description>
ขอดู DebugLog ที่กำลังใช้ใน Object นี้
</description>

<arguments>	
</arguments>

<return>
คืน DatawindowXML ของ DebugLog ที่ใช้งานอยู่, หากยังไม่มีใช้งานคืนค่า null
</return>

<usage>
</usage>
************************************************/
string ls_xml
if( isvalid( inv_debuglog ) and inv_debuglog.getxml(ls_xml) = 1 )then
	return ls_xml
else
	setnull( ls_xml )
	return ls_xml
end if

end function

public function str_error of_getlasterror ();return inv_lasterror.of_geterror()

end function

protected subroutine of_debuglog (string as_logtext);/***********************************************
<description>
เก็บข้อความ log เพิ่มเข้าใน DebugLog ของ object นี้
</description>

<arguments>
as_logtext		ข้อความ Log ที่ให้เพิ่มเข้าใน DebugLog
</arguments>

<return>
</return>

<usage>
</usage>
************************************************/
if( isvalid( inv_debuglog ) )then
	inv_debuglog.log( as_logtext )
end if

end subroutine

public function integer of_settransobject (ref transaction atr_transaction);if( isvalid( atr_transaction ) )then
	itr_transaction = atr_transaction
	return 1
end if
return -1

end function

on n_cst_base.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_base.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

