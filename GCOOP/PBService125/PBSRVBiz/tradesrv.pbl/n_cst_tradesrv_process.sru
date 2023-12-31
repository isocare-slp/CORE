$PBExportHeader$n_cst_tradesrv_process.sru
$PBExportComments$ประมวลรายการซื้อขาย
forward
global type n_cst_tradesrv_process from nonvisualobject
end type
end forward

global type n_cst_tradesrv_process from nonvisualobject
end type
global n_cst_tradesrv_process n_cst_tradesrv_process

type prototypes

end prototypes

type variables
Public:
// - Common return value constants:
constant integer 		SUCCESS = 1
constant integer 		FAILURE = -1
constant integer 		NO_ACTION = 0

transaction						itr_sqlca
throwable						ithw_exception

n_cst_dbconnectservice		inv_transection
n_cst_divsrv_proc_service	inv_procsrv
n_cst_dwxmlieservice			inv_dwxmliesrv
n_cst_progresscontrol		inv_progress
n_cst_doccontrolservice		inv_docsrv

boolean	ib_stop

end variables

forward prototypes
private function integer of_setsrvdwxmlie (boolean ab_switch)
public function integer of_initservice (n_cst_dbconnectservice anv_dbtrans) throws throwable
public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws throwable
private function integer of_setsrvproc (boolean ab_switch)
public function integer of_prc_cls_year_opt (str_tradesrv_proc astr_tradesrv_proc) throws throwable
public function integer of_prc_divavg_opt (str_tradesrv_proc astr_tradesrv_proc) throws throwable
public function integer of_setsrvdoccontrol (boolean ab_switch)
public function string of_getdoctype (string as_branchid, string as_doctype)
public function integer of_prc_cls_day_opt (string as_branchid, datetime adt_operdate) throws throwable
private function integer of_create_credbalance (string as_branchid, string as_recvperiod, datetime adt_operdate)
private function integer of_create_debtbalance (string as_branchid, string as_recvperiod, datetime adt_operdate)
private function integer of_create_stockbalance (string as_branchid, string as_recvperiod, datetime adt_operdate)
public function integer of_prc_cls_mth_opt (string as_branchid, string as_recvprd, datetime adt_operdate) throws throwable
public function integer of_create_cred_creditslip (string as_branchid, datetime adt_operdate)
public function integer of_create_debt_creditslip (string as_branchid, datetime adt_operdate)
private function integer of_getoprflag (string as_sliptype)
private function integer of_post2debt (string as_branchid, string as_debtno, string as_creditno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt, string as_entryid)
private function integer of_post2debtmaster (string as_branchid, string as_debtno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt, string as_entryid)
private function integer of_post2debtstmt (string as_branchid, string as_debtno, string as_creditno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt, string as_entryid)
public function integer of_post2debtdet (string as_branchid, string as_debtno, string as_creditno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt)
public function integer of_create_debt_creditslip_bak (string as_branchid, datetime adt_operdate)
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

inv_progress = create n_cst_progresscontrol

return 1
end function

public function integer of_setprogress (ref n_cst_progresscontrol anv_progress) throws throwable;/***********************************************************
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
		inv_procsrv	= create n_cst_divsrv_proc_service
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

public function integer of_prc_cls_year_opt (str_tradesrv_proc astr_tradesrv_proc) throws throwable;return 1
end function

public function integer of_prc_divavg_opt (str_tradesrv_proc astr_tradesrv_proc) throws throwable;return 1
end function

public function integer of_setsrvdoccontrol (boolean ab_switch);// Check argument
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

public function string of_getdoctype (string as_branchid, string as_doctype);string	ls_doccd

select document_code
into	:ls_doccd
from 	tducfsliptype
where	coop_id = :as_branchid and
			sliptype_code = :as_doctype
using itr_sqlca;

return ls_doccd
end function

public function integer of_prc_cls_day_opt (string as_branchid, datetime adt_operdate) throws throwable;string		ls_xmloption, ls_rangetext
integer	li_rangetype
boolean	lb_err
n_ds		lds_option, lds_procdata, lds_noticemaster
long		ll_row

inv_progress.istr_progress.progress_text		= "ประมวลผล ปิดสิ้นวัน"
inv_progress.istr_progress.progress_index		= 0
inv_progress.istr_progress.progress_max		= 2
inv_progress.istr_progress.subprogress_text	= "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index	= 0
inv_progress.istr_progress.subprogress_max	= 1
inv_progress.istr_progress.status					= 8

try
	inv_progress.istr_progress.progress_index		= 1
	this.of_create_cred_creditslip( as_branchid, adt_operdate )

	inv_progress.istr_progress.progress_index		= 2
	ll_row = this.of_create_debt_creditslip( as_branchid, adt_operdate )

catch( throwable lthw_errgen )
	ithw_exception.text		= lthw_errgen.text
	lb_err		= true
end try

//objdestroy:
//if isvalid(lds_option) then destroy lds_option
//if isvalid(lds_procdata) then destroy lds_procdata
//if isvalid(lds_noticemaster) then destroy lds_noticemaster
//
//this.of_setsrvdwxmlie( false )
//this.of_setsrvproc( false )

if lb_err then
//	astr_procloan.xml_option	= ""
//	astr_procloan.xml_report	= ""
	rollback using itr_sqlca ;
	
	ithw_exception.text = "of_prc_cls_day_opt() " + ithw_exception.text
	throw ithw_exception
end if

inv_progress.istr_progress.status = 1

commit using itr_sqlca ;

return ll_row
end function

private function integer of_create_credbalance (string as_branchid, string as_recvperiod, datetime adt_operdate);integer	li_ret = 1
n_ds		lds_debt
long		ll_count, i
string		ls_sql
boolean	lb_err = False
dec		ldec_debtamt
string		ls_branchid, ls_debtno
 

inv_progress.istr_progress.progress_text		= "ประมวลผล ยอดยกมาเจ้าหนี้"
inv_progress.istr_progress.progress_index		= 0

delete from tddebtmonthbalance where coop_id = :ls_branchid and debttype_code = 'AP' and recv_period = :as_recvperiod using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "ไม่สามารถ ทำการ Delete ข้อมูล tddebtmonthbalance ได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n "
	ithw_exception.text	+= itr_sqlca.SQLErrText	
	lb_err = true; Goto objdestroy
//		messagebox( "นำเข้าข้อมูล tdstockmonthbalance", "ไม่สามารถ ทำการ insert ข้อมูลได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n " + SQLCA.SQLERRTEXT + " ", stopsign! )
//		rollback using itr_sqlca;
	return failure
end if


//ls_sql = "  select 	branch_id, cred_no, cred_amt "
//ls_sql += "  from		tdcredmaster "
//ls_sql += "  where		branch_id =  '" + as_branchid + "'"
//ls_sql += "  order by 	branch_id, cred_no "

ls_sql = "    select coop_id, debt_no, debt_name, bfbalance_amt, debtamt, payamt, reducedebt, "
ls_sql += "   nvl(bfbalance_amt,0) + nvl(debtamt,0) - nvl(payamt,0) - nvl(reducedebt,0) as remainamt "
ls_sql += "   from ( "
ls_sql += "   	select  dmb.coop_id as coop_id, dmb.debt_no as debt_no, "
ls_sql += "   	p.prename_desc||dm.cred_name||' '||suffname_desc as debt_name, "
ls_sql += "   	nvl(dmb.bfdebt_amt,0) as bfbalance_amt, "
ls_sql += "   	(select nvl(sum(ss.slipnet_amt),0) "
ls_sql += "   	from tdstockslip ss "
ls_sql += "   	where  ss.debt_no = dmb.debt_no "
ls_sql += "   	and  sliptype_code = 'RC' "
ls_sql += "   	and  paymentby = 'LON' "
ls_sql += "   	and  to_char(ss.slip_date ,'yyyymm' ) ='201307' ) as debtamt, "
ls_sql += "      	(select nvl(sum(ddd.debt_amt), 0) "
ls_sql += "   	 from tddebtdec dd , tddebtdecdet  ddd "
ls_sql += "   	where  dd.debtdecdoc_no = ddd.debtdecdoc_no "
ls_sql += "   	and dd.debt_no = dmb.debt_no "
ls_sql += "   	and dd.debtdec_status = 0 "
ls_sql += "   	and to_char(dd.debtintdoc_date ,'yyyymm') ='201307') as payamt, "
ls_sql += "   	(select nvl(sum(di.debt_amt),0) "
ls_sql += "   	from tddebtinc di "
ls_sql += "   	where di.debt_no = dmb.debt_no "
ls_sql += "   	and to_char(di.debtintdoc_date ,'yyyymm') ='201307' ) as reducedebt "
ls_sql += "   	from tdcredmaster dm,tddebtmonthbalance dmb , mbucfprename p "
ls_sql += "   	where 	dm.cred_no = dmb.debt_no(+) and "
ls_sql += "           		dm.prename_code = p.prename_code(+)  and "
ls_sql += "    		recv_period ='201306' ) "
ls_sql += "   order by debt_no  "

lds_debt = f_newdatastoresql(itr_sqlca, ls_sql) 
lds_debt.SetTransObject(itr_sqlca)
ll_count = lds_debt.retrieve()

for i = 1 to ll_count
	yield()
	if ib_stop then
		ib_stop		= false
		destroy( lds_debt )
		return -1
	end if
	ls_branchid = lds_debt.GetItemString(i, 1) //lds_debt.GetItemString(i, 'branch_id')
	ls_debtno =  lds_debt.GetItemString(i, 2)  //lds_debt.GetItemString(i, 'cred_no')
	ldec_debtamt = lds_debt.GetItemDecimal(i, 8) //lds_debt.GetItemDecimal(i, 'cred_amt')

	insert into tddebtmonthbalance
	( 	coop_id, debt_no, debttype_code, recv_period, bfdebt_amt, debtbalance_amt, work_date)
	values
	(	:ls_branchid, :ls_debtno, 'AP', :as_recvperiod,	:ldec_debtamt,	:ldec_debtamt, :adt_operdate)
	using itr_sqlca;
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถ ทำการ insert ข้อมูล tddebtmonthbalance ได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n "
		ithw_exception.text	+= itr_sqlca.SQLErrText	
		lb_err = true; Goto objdestroy
//		messagebox( "นำเข้าข้อมูล tdstockmonthbalance", "ไม่สามารถ ทำการ insert ข้อมูลได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n " + SQLCA.SQLERRTEXT + " ", stopsign! )
//		rollback using itr_sqlca;
		return failure
	end if
next

objdestroy:
if isvalid(lds_debt) then destroy lds_debt

if lb_err then
	rollback using itr_sqlca;
	li_ret = -1
else
	commit using itr_sqlca;
	li_ret = 1
end if

return li_ret

end function

private function integer of_create_debtbalance (string as_branchid, string as_recvperiod, datetime adt_operdate);integer	li_ret = 1
n_ds		lds_debt
long		ll_count, i
string		ls_sql
boolean	lb_err = False
dec		ldec_debtamt
string		ls_branchid, ls_debtno

inv_progress.istr_progress.progress_text		= "ประมวลผล ยอดยกมาลูกหนี้"
inv_progress.istr_progress.progress_index		= 0

delete from tddebtmonthbalance where coop_id = :as_branchid and debttype_code = 'AR'  and recv_period = :as_recvperiod using itr_sqlca;
if itr_sqlca.sqlcode <> 0 then
	ithw_exception.text	= "ไม่สามารถ ทำการ Delete ข้อมูล tddebtmonthbalance ได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n "
	ithw_exception.text	+= itr_sqlca.SQLErrText	
	lb_err = true; Goto objdestroy
//		messagebox( "นำเข้าข้อมูล tdstockmonthbalance", "ไม่สามารถ ทำการ insert ข้อมูลได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n " + SQLCA.SQLERRTEXT + " ", stopsign! )
//		rollback using itr_sqlca;
	return failure
end if

//ls_sql = "  select 	branch_id, debt_no, debt_amt "
//ls_sql += "  from		tddebtmaster "
//ls_sql += "  where		branch_id =  '" + as_branchid + "'"
//ls_sql += "  order by 	branch_id, debt_no "

ls_sql = "  select coop_id, debt_no, debt_name,bfbalance_amt,debtamt,payamt,reducedebt, "
ls_sql += "  nvl(bfbalance_amt,0) + nvl(debtamt,0) - nvl(payamt,0) - nvl(reducedebt,0) as remainamt "
ls_sql += "  from ( "
ls_sql += "  	select  dmb.coop_id as coop_id, dmb.debt_no as debt_no, "
ls_sql += "  	p.prename_desc||dm.debt_name||' '||suffname_desc as debt_name, "
ls_sql += "  	nvl(dmb.bfdebt_amt,0) as bfbalance_amt, "
ls_sql += "  	(select nvl(sum(ss.slipnet_amt),0) "
ls_sql += "  	from tdstockslip ss "
ls_sql += "  	where  ss.debt_no = dmb.debt_no "
ls_sql += "  	and  sliptype_code = 'IV' "
ls_sql += "  	and  paymentby = 'LON' "
ls_sql += "  	and  to_char(ss.slip_date ,'yyyymm' ) ='201307' ) as debtamt, "
ls_sql += "     	(select nvl(sum(ddd.debt_amt), 0) "
ls_sql += "  	 from tddebtdec dd , tddebtdecdet  ddd "
ls_sql += "  	where  dd.debtdecdoc_no = ddd.debtdecdoc_no "
ls_sql += "  	and dd.debt_no = dmb.debt_no "
ls_sql += "  	and dd.debtdec_status = 0 "
ls_sql += "  	and to_char(dd.debtintdoc_date ,'yyyymm') ='201307') as payamt, "
ls_sql += "  	(select nvl(sum(di.debt_amt),0) "
ls_sql += "  	from tddebtinc di "
ls_sql += "  	where di.debt_no = dmb.debt_no "
ls_sql += "  	and to_char(di.debtintdoc_date ,'yyyymm') ='201307' ) as reducedebt "
ls_sql += "  	from tddebtmaster dm,tddebtmonthbalance dmb , mbucfprename p "
ls_sql += "  	where 	dm.debt_no = dmb.debt_no(+) and "
ls_sql += "          		dm.prename_code = p.prename_code(+)  and "
ls_sql += "   		recv_period ='201306' ) "
ls_sql += "  order by debt_no "


lds_debt = f_newdatastoresql(itr_sqlca, ls_sql) 
lds_debt.SetTransObject(itr_sqlca)
ll_count = lds_debt.retrieve()

for i = 1 to ll_count
	yield()
	if ib_stop then
		ib_stop		= false
		destroy( lds_debt )
		return -1
	end if
	ls_branchid = lds_debt.GetItemString(i, 1) //lds_debt.GetItemString(i, 'branch_id')
	ls_debtno = lds_debt.GetItemString(i, 2)  //lds_debt.GetItemString(i, 'debt_no')
	ldec_debtamt = lds_debt.GetItemDecimal(i, 8) //lds_debt.GetItemDecimal(i, 'remainamt')

	insert into tddebtmonthbalance
	( 	coop_id, debt_no, debttype_code, recv_period, bfdebt_amt, debtbalance_amt, work_date)
	values
	(	:ls_branchid, :ls_debtno, 'AR', :as_recvperiod,	:ldec_debtamt,	:ldec_debtamt, :adt_operdate)
	using itr_sqlca;
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถ ทำการ insert ข้อมูล tddebtmonthbalance ได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n ้ "
		ithw_exception.text	+= itr_sqlca.SQLErrText	
		lb_err = true; Goto objdestroy
//		messagebox( "นำเข้าข้อมูล tdstockmonthbalance", "ไม่สามารถ ทำการ insert ข้อมูลได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n " + SQLCA.SQLERRTEXT + " ", stopsign! )
//		rollback using itr_sqlca;
		return failure
	end if
next

objdestroy:
if isvalid(lds_debt) then destroy lds_debt

if lb_err then
	rollback using itr_sqlca;
	li_ret = -1
else
	commit using itr_sqlca;
	li_ret = 1
end if

return li_ret

end function

private function integer of_create_stockbalance (string as_branchid, string as_recvperiod, datetime adt_operdate);integer	li_ret = 1
n_ds		lds_stock
long		ll_count, i
string		ls_sql
boolean	lb_err = False
string		ls_productno, ls_storeid  //, ls_branchid
dec		ldec_balqty, ldec_costamt

inv_progress.istr_progress.progress_text		= "ประมวลผล ยอดยกมาสินค้า"
inv_progress.istr_progress.progress_index		= 0

delete from tdstockmonthbalance where coop_id = :as_branchid and recv_period = :as_recvperiod using itr_sqlca; 

ls_sql = "  select 	coop_id, store_id, product_no, balance_qty, cost_amt, costavg_amt "
ls_sql += "  from		tdstockmaster "
ls_sql += "  where		coop_id =  '" + as_branchid + "'"
ls_sql += "  order by 	coop_id, store_id, product_no "

lds_stock = f_newdatastoresql(itr_sqlca, ls_sql) 
lds_stock.SetTransObject(itr_sqlca)
ll_count = lds_stock.retrieve()

for i = 1 to ll_count
	yield()
	if ib_stop then
		ib_stop		= false
		destroy( lds_stock )
		return -1
	end if
//	ls_branchid = lds_stock.GetItemString(i, 'coop_id')
	ls_storeid = lds_stock.GetItemString(i, 'store_id')
	ls_productno = lds_stock.GetItemString(i, 'product_no')
	ldec_balqty = lds_stock.GetItemDecimal(i, 'balance_qty')
//	ldec_costamt = lds_stock.GetItemDecimal(i, 'cost_amt')
	
	insert into tdstockmonthbalance
	( 	coop_id, store_id, product_no , recv_period, bfbalance_qty, work_date, balance_qty )
	values
	(	:as_branchid, :ls_storeid, :ls_productno,	:as_recvperiod,	:ldec_balqty, :adt_operdate, :ldec_balqty)
	using itr_sqlca;
	if itr_sqlca.sqlcode <> 0 then
		ithw_exception.text	= "ไม่สามารถ ทำการ insert ข้อมูล tdstockmonthbalance ได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n ้ "
		ithw_exception.text	+= itr_sqlca.SQLErrText	
		lb_err = true; Goto objdestroy
//		messagebox( "นำเข้าข้อมูล tdstockmonthbalance", "ไม่สามารถ ทำการ insert ข้อมูลได้ ~r~n กรุณาเซฟภาพแล้วติดต่อโปรแกรมเมอร์ ~r~n " + SQLCA.SQLERRTEXT + " ", stopsign! )
//		rollback using itr_sqlca;
		return failure
	end if
next

objdestroy:
if isvalid(lds_stock) then destroy lds_stock

if lb_err then
	rollback using itr_sqlca;
	li_ret = -1
else
	commit using itr_sqlca;
	li_ret = 1
end if

return li_ret

end function

public function integer of_prc_cls_mth_opt (string as_branchid, string as_recvprd, datetime adt_operdate) throws throwable;string		ls_xmloption, ls_rangetext
integer	li_rangetype
boolean	lb_err
n_ds		lds_option, lds_procdata, lds_noticemaster

inv_progress.istr_progress.progress_text		= "ประมวลผล ปิดสิ้นเดือน"
inv_progress.istr_progress.progress_index		= 0
inv_progress.istr_progress.progress_max		= 3
inv_progress.istr_progress.subprogress_text	= "เตรียมข้อมูล"
inv_progress.istr_progress.subprogress_index	= 0
inv_progress.istr_progress.subprogress_max	= 1
inv_progress.istr_progress.status					= 8

if mid(as_recvprd, 5, 2) = '03' then
	update cmdocumentcontrol set last_documentno = 0, document_year = to_char(to_number(substr(:as_recvprd, 1, 4)) + 544)
	where document_code in ('TDSLIPIV', 'TDSLIPAD', 'TDSLIPAP', 'TDSLIPAPRC', 'TDSLIPAR', 'TDSLIPARRC', 'TDSLIPCN', 'TDSLIPDN', 'TDSLIPIS', 'TDSLIPPO', 'TDSLIPPR', 'TDSLIPQT', 'TDSLIPRC', 'TDSLIPSR')
	using itr_sqlca ;
end if

try
	inv_progress.istr_progress.progress_index		= 1
	this.of_create_stockbalance( as_branchid, as_RecvPrd, adt_operdate )

	inv_progress.istr_progress.progress_index		= 2
	this.of_create_debtbalance( as_branchid, as_RecvPrd, adt_operdate )

	inv_progress.istr_progress.progress_index		= 3
	this.of_create_credbalance( as_branchid, as_RecvPrd, adt_operdate )

catch( throwable lthw_errgen )
	ithw_exception.text		= lthw_errgen.text
	lb_err		= true
end try

//objdestroy:
//if isvalid(lds_option) then destroy lds_option
//if isvalid(lds_procdata) then destroy lds_procdata
//if isvalid(lds_noticemaster) then destroy lds_noticemaster
//
//this.of_setsrvdwxmlie( false )
//this.of_setsrvproc( false )

if lb_err then
//	astr_procloan.xml_option	= ""
//	astr_procloan.xml_report	= ""
	rollback using itr_sqlca ;
	
	ithw_exception.text = "of_prc_cls_mth_opt() " + ithw_exception.text
	throw ithw_exception
end if

inv_progress.istr_progress.status = 1

commit using itr_sqlca ;

return 1
end function

public function integer of_create_cred_creditslip (string as_branchid, datetime adt_operdate);integer	li_ret = 1
n_ds		lds_hd, lds_dt, lds_debt
string		ls_sql
long		ll_count, i, ll_row, j = 0
boolean	lb_err = False
string		ls_debtno = '', ls_olddebtno = '', ls_branchid, ls_doccd, ls_slipno, ls_sliprefno
dec		ldec_amt, ldec_netamt = 0
datetime	ldt_refdate, ldt_duedate

inv_progress.istr_progress.progress_text		= "ประมวลผล ตั้งหนี้เจ้าหนี้"
inv_progress.istr_progress.progress_index		= 0

lds_hd = create n_ds
lds_hd.Dataobject = 'd_tradesrv_fin_apcredit_header'
lds_hd.SetTransObject(itr_sqlca)

lds_dt = create n_ds
lds_dt.Dataobject = 'd_tradesrv_fin_apcredit_detail'
lds_dt.SetTransObject(itr_sqlca)

lds_debt = create n_ds
lds_debt.dataobject = 'd_closedate'

lds_debt.SetTransObject(itr_sqlca)
ll_count = lds_debt.retrieve('RC', adt_operdate)
if ll_count > 0 then
	for i = 1 to ll_count
		ls_debtno = lds_debt.GetItemString(i, 'cred_no')
		ls_branchid = lds_debt.GetItemString(i, 'coop_id')
		ls_sliprefno = lds_debt.GetItemString(i, 'slip_no')
		ldec_amt = lds_debt.GetItemDecimal(i, 'slipnet_amt')
		ldt_refdate = lds_debt.GetItemDateTime(i, 'slip_date')
		ldt_duedate = lds_debt.GetItemDateTime(i, 'due_date')
		lds_debt.SetItem(i, 'slip_status', 0)
		if 	ls_olddebtno <> ls_debtno	then 
			if i > 1 then
				if lds_hd.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
					ithw_exception.text	+= lds_hd.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
					
				// บันทึก Slip
				if lds_dt.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
					ithw_exception.text	+= lds_dt.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
				lds_hd.Reset()
				lds_dt.Reset()
				of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'APC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
			end if
			try
				this.of_setsrvdoccontrol( true )
				ls_doccd = of_getdoctype(ls_branchid, 'APC')
				ls_slipno	= inv_docsrv.of_getnewdocno( ls_branchid , ls_doccd)	// get เลขที่เอกสาร
			catch( throwable lthw_getreqdoc )
				ithw_exception.text	= "~r~nพบขอผิดพลาด (20.1)" + lthw_getreqdoc.text
				lb_err = true
			end try
			ll_row = lds_hd.insertrow(0)
			lds_hd.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_hd.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_hd.SetItem(ll_row, 'sliptype_code', 'APC')
			lds_hd.SetItem(ll_row, 'creditdoc_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'cred_no', ls_debtno)
			lds_hd.SetItem(ll_row, 'entry_id', 'System')
			lds_hd.SetItem(ll_row, 'entry_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'debt_status', 8)
			ldec_netamt = ldec_amt
			j = 1
			ll_row = lds_dt.insertrow(0)
			lds_dt.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_dt.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_dt.SetItem(ll_row, 'seq_no', j)
			lds_dt.SetItem(ll_row, 'credittype_code', 'APC')
			lds_dt.SetItem(ll_row, 'refdoc_no', ls_sliprefno)
			lds_dt.SetItem(ll_row, 'refdoc_date', ldt_duedate)
			lds_dt.SetItem(ll_row, 'due_date', ldt_refdate)
			lds_dt.SetItem(ll_row, 'debt_amt', ldec_amt)
			inv_progress.istr_progress.subprogress_text	= "รหัสเจ้าหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
			inv_progress.istr_progress.subprogress_index	= i
		else
			ldec_netamt += ldec_amt
			j++
			ll_row = lds_dt.insertrow(0)
			lds_dt.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_dt.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_dt.SetItem(ll_row, 'seq_no', j)
			lds_dt.SetItem(ll_row, 'credittype_code', 'APC')
			lds_dt.SetItem(ll_row, 'refdoc_no', ls_sliprefno)
			lds_dt.SetItem(ll_row, 'refdoc_date', ldt_duedate)
			lds_dt.SetItem(ll_row, 'due_date', ldt_refdate)
			lds_dt.SetItem(ll_row, 'debt_amt', ldec_amt)
		end if
		ls_olddebtno = ls_debtno
	next
end if
lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
if lds_hd.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
	ithw_exception.text	+= lds_hd.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if
	
// บันทึก Slip
if lds_dt.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
	ithw_exception.text	+= lds_dt.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if

// บันทึก Slip
if lds_debt.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
	ithw_exception.text	+= lds_dt.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if
lds_hd.Reset()
lds_dt.Reset()
of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'APC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')

objdestroy:
if isvalid(lds_hd) then destroy lds_hd
if isvalid(lds_dt) then destroy lds_dt
if isvalid(lds_debt) then destroy lds_debt

if lb_err then
//	rollback using itr_sqlca;
	li_ret = -1
else
//	commit using itr_sqlca;
	li_ret = 1
end if

return li_ret

//		lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
//		if lds_hd.update() <> 1 then
//			ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
//			ithw_exception.text	+= lds_hd.of_geterrormessage()
//			lb_err = true; Goto objdestroy
//		end if
//			
//		// บันทึก Slip
//		if lds_dt.update() <> 1 then
//			ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
//			ithw_exception.text	+= lds_dt.of_geterrormessage()
//			lb_err = true; Goto objdestroy
//		end if
//		lds_hd.Reset()
//		lds_dt.Reset()
//		//update
//		of_post2debt ( ls_branchid,  ls_debtno, ls_slipno,  adt_operdate,  'APC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
//
////		w_td_test.of_settext(  w_td_test.of_gettext() + 'Detail' + ls_slipno + ': ' + string(j) + '. ' + ls_debtno + ' ' + ls_sliprefno + ' = ' + string(ldec_amt) + ' Sum = ' + string(ldec_netamt) + '~r~n')
//		if i+1 <= ll_count then
//			ls_olddebtno = ls_debtno
//			ls_debtno = lds_debt.GetItemString(i + 1, 'cred_no')
//			if ls_debtno <> ls_olddebtno then
//				lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
////			Messagebox('<', ldec_netamt)
//				//update
//				if lds_hd.update() <> 1 then
//					ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
//					ithw_exception.text	+= lds_hd.of_geterrormessage()
//					lb_err = true; Goto objdestroy
//				end if
//					
//				// บันทึก Slip
//				if lds_dt.update() <> 1 then
//					ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
//					ithw_exception.text	+= lds_dt.of_geterrormessage()
//					lb_err = true; Goto objdestroy
//				end if
//				lds_hd.Reset()
//				lds_dt.Reset()
////				w_td_test.of_settext(  w_td_test.of_gettext() + 'Header' + ls_slipno + ': ' + string(j) + '. ' + ls_debtno + ' ' + ls_sliprefno + ' = ' + string(ldec_amt) + ' Sum = ' + string(ldec_netamt) + '~r~n')
//				of_post2debt ( ls_branchid,  ls_debtno, ls_slipno,  adt_operdate,  'APC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
//				inv_progress.istr_progress.subprogress_text	= "รหัสเจ้าหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
//				inv_progress.istr_progress.subprogress_index	= i
//			end if
//		else
////			Messagebox('>', ldec_netamt)
//			lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
//			if lds_hd.update() <> 1 then
//				ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
//				ithw_exception.text	+= lds_hd.of_geterrormessage()
//				lb_err = true; Goto objdestroy
//			end if
//				
//			// บันทึก Slip
//			if lds_dt.update() <> 1 then
//				ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
//				ithw_exception.text	+= lds_dt.of_geterrormessage()
//				lb_err = true; Goto objdestroy
//			end if
//			lds_hd.Reset()
//			lds_dt.Reset()
//			//update
//			of_post2debt ( ls_branchid,  ls_debtno, ls_slipno,  adt_operdate,  'APC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
//			inv_progress.istr_progress.subprogress_text	= "รหัสเจ้าหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
//			inv_progress.istr_progress.subprogress_index	= i
////			w_td_test.of_settext(  w_td_test.of_gettext()  + 'Header' +  ls_slipno + ': ' + string(j) + '. ' + ls_debtno + ' ' + ls_sliprefno + ' = ' + string(ldec_amt) + ' Sum = ' + string(ldec_netamt) + '~r~n')
//		end if
////		of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'APC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
////		inv_progress.istr_progress.subprogress_text	= "รหัสเจ้าหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
////		inv_progress.istr_progress.subprogress_index	= i


end function

public function integer of_create_debt_creditslip (string as_branchid, datetime adt_operdate);integer	li_ret = 1
n_ds		lds_hd, lds_dt, lds_debt, lds_debt2
string		ls_sql
long		ll_count, i, ll_row, j = 0
boolean	lb_err = False
string		ls_debtno = '', ls_olddebtno = '', ls_branchid, ls_doccd, ls_slipno, ls_sliprefno
dec		ldec_amt, ldec_netamt = 0
datetime	ldt_refdate, ldt_duedate

inv_progress.istr_progress.progress_text		= "ประมวลผล ตั้งหนี้ลูกหนี้"
inv_progress.istr_progress.progress_index		= 0

lds_hd = create n_ds
lds_hd.Dataobject = 'd_tradesrv_fin_arcredit_header'
lds_hd.SetTransObject(itr_sqlca)

lds_dt = create n_ds
lds_dt.Dataobject = 'd_tradesrv_fin_arcredit_detail'
lds_dt.SetTransObject(itr_sqlca)

lds_debt = create n_ds
lds_debt.dataobject = 'd_closedate'

lds_debt.SetTransObject(itr_sqlca)
ll_count = lds_debt.retrieve('IV', adt_operdate)
if ll_count > 0 then
	for i = 1 to ll_count
		ls_debtno = lds_debt.GetItemString(i, 'debt_no')
		ls_branchid = lds_debt.GetItemString(i, 'coop_id')
		ls_sliprefno = lds_debt.GetItemString(i, 'slip_no')
		ldec_amt = lds_debt.GetItemDecimal(i, 'slipnet_amt')
		ldt_refdate = lds_debt.GetItemDateTime(i, 'slip_date')
		ldt_duedate = lds_debt.GetItemDateTime(i, 'due_date')
		lds_debt.SetItem(i, 'slip_status', 0)
		if 	ls_olddebtno <> ls_debtno	then 
			if i > 1 then
				if lds_hd.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
					ithw_exception.text	+= lds_hd.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
					
				// บันทึก Slip
				if lds_dt.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
					ithw_exception.text	+= lds_dt.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
				lds_hd.Reset()
				lds_dt.Reset()
				of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'ARC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
			end if
			try
				this.of_setsrvdoccontrol( true )
				ls_doccd = of_getdoctype(ls_branchid, 'ARC')
				ls_slipno	= inv_docsrv.of_getnewdocno( ls_branchid , ls_doccd)	// get เลขที่เอกสาร
			catch( throwable lthw_getreqdoc )
				ithw_exception.text	= "~r~nพบขอผิดพลาด (20.1)" + lthw_getreqdoc.text
				lb_err = true
			end try
			ll_row = lds_hd.insertrow(0)
			lds_hd.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_hd.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_hd.SetItem(ll_row, 'sliptype_code', 'ARC')
			lds_hd.SetItem(ll_row, 'creditdoc_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'debt_no', ls_debtno)
			lds_hd.SetItem(ll_row, 'entry_id', 'System')
			lds_hd.SetItem(ll_row, 'entry_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'debt_status', 8)
			ldec_netamt = ldec_amt
			lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
			j = 1
			ll_row = lds_dt.insertrow(0)
			lds_dt.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_dt.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_dt.SetItem(ll_row, 'seq_no', j)
			lds_dt.SetItem(ll_row, 'credittype_code', 'ARC')
			lds_dt.SetItem(ll_row, 'refdoc_no', ls_sliprefno)
			lds_dt.SetItem(ll_row, 'refdoc_date', ldt_refdate)
			lds_dt.SetItem(ll_row, 'due_date', ldt_duedate)
			lds_dt.SetItem(ll_row, 'debt_amt', ldec_amt)
			inv_progress.istr_progress.subprogress_text	= "รหัสลูกหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
			inv_progress.istr_progress.subprogress_index	= i
		else
			ldec_netamt += ldec_amt
			j++
			ll_row = lds_dt.insertrow(0)
			lds_dt.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_dt.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_dt.SetItem(ll_row, 'seq_no', j)
			lds_dt.SetItem(ll_row, 'credittype_code', 'ARC')
			lds_dt.SetItem(ll_row, 'refdoc_no', ls_sliprefno)
			lds_dt.SetItem(ll_row, 'refdoc_date', ldt_refdate)
			lds_dt.SetItem(ll_row, 'due_date', ldt_duedate)
			lds_dt.SetItem(ll_row, 'debt_amt', ldec_amt)
		end if
		ls_olddebtno = ls_debtno
	next
end if
lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
if lds_hd.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
	ithw_exception.text	+= lds_hd.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if
	
// บันทึก Slip
if lds_dt.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
	ithw_exception.text	+= lds_dt.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if

// บันทึก Slip
if lds_debt.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
	ithw_exception.text	+= lds_dt.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if
lds_hd.Reset()
lds_dt.Reset()
of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'ARC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')

lds_debt2 = create n_ds
lds_debt2.dataobject = 'd_closedate2'
lds_debt2.SetTransObject(itr_sqlca)
ll_count = lds_debt2.retrieve('IV', adt_operdate)
ls_olddebtno = ''
if ll_count > 0 then
	for i = 1 to ll_count
		ls_debtno = lds_debt2.GetItemString(i, 'debt_no')
		ls_branchid = lds_debt2.GetItemString(i, 'coop_id')
		ls_sliprefno = lds_debt2.GetItemString(i, 'slip_no')
		ldec_amt = lds_debt2.GetItemDecimal(i, 'slipnet_amt')
		ldt_refdate = lds_debt2.GetItemDateTime(i, 'slip_date')
		ldt_duedate = lds_debt2.GetItemDateTime(i, 'due_date')
		lds_debt2.SetItem(i, 'slip_status', 0)
		if 	ls_olddebtno <> ls_debtno	then 
			if i > 1 then
				if lds_hd.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
					ithw_exception.text	+= lds_hd.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
					
				// บันทึก Slip
				if lds_dt.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
					ithw_exception.text	+= lds_dt.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
				lds_hd.Reset()
				lds_dt.Reset()
//				of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'ARC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
			end if
			try
				this.of_setsrvdoccontrol( true )
				ls_doccd = of_getdoctype(ls_branchid, 'ARC')
				ls_slipno	= inv_docsrv.of_getnewdocno( ls_branchid , ls_doccd)	// get เลขที่เอกสาร
			catch( throwable lthw_getreqdoc2 )
				ithw_exception.text	= "~r~nพบขอผิดพลาด (20.1)" + lthw_getreqdoc2.text
				lb_err = true
			end try
			ll_row = lds_hd.insertrow(0)
			lds_hd.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_hd.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_hd.SetItem(ll_row, 'sliptype_code', 'ARC')
			lds_hd.SetItem(ll_row, 'creditdoc_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'debt_no', ls_debtno)
			lds_hd.SetItem(ll_row, 'entry_id', 'System')
			lds_hd.SetItem(ll_row, 'entry_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'debt_status', 8)
			ldec_netamt = ldec_amt
			lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
			j = 1
			ll_row = lds_dt.insertrow(0)
			lds_dt.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_dt.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_dt.SetItem(ll_row, 'seq_no', j)
			lds_dt.SetItem(ll_row, 'credittype_code', 'ARC')
			lds_dt.SetItem(ll_row, 'refdoc_no', ls_sliprefno)
			lds_dt.SetItem(ll_row, 'refdoc_date', ldt_refdate)
			lds_dt.SetItem(ll_row, 'due_date', ldt_duedate)
			lds_dt.SetItem(ll_row, 'debt_amt', ldec_amt)
			inv_progress.istr_progress.subprogress_text	= "รหัสลูกหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
			inv_progress.istr_progress.subprogress_index	= i
		else
			ldec_netamt += ldec_amt
			j++
			ll_row = lds_dt.insertrow(0)
			lds_dt.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_dt.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_dt.SetItem(ll_row, 'seq_no', j)
			lds_dt.SetItem(ll_row, 'credittype_code', 'ARC')
			lds_dt.SetItem(ll_row, 'refdoc_no', ls_sliprefno)
			lds_dt.SetItem(ll_row, 'refdoc_date', ldt_refdate)
			lds_dt.SetItem(ll_row, 'due_date', ldt_duedate)
			lds_dt.SetItem(ll_row, 'debt_amt', ldec_amt)
		end if
		ls_olddebtno = ls_debtno
	next
end if
lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
if lds_hd.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
	ithw_exception.text	+= lds_hd.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if
	
// บันทึก Slip
if lds_dt.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
	ithw_exception.text	+= lds_dt.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if

// บันทึก Slip
if lds_debt2.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
	ithw_exception.text	+= lds_dt.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if
lds_hd.Reset()
lds_dt.Reset()
//of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'ARC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')


objdestroy:
if isvalid(lds_hd) then destroy lds_hd
if isvalid(lds_dt) then destroy lds_dt
if isvalid(lds_debt) then destroy lds_debt

if lb_err then
//	rollback using itr_sqlca;
	li_ret = -1
else
//	commit using itr_sqlca;
	li_ret = 1
end if

return li_ret



end function

private function integer of_getoprflag (string as_sliptype);long		li_flag

select 	accnature_flag
into		:li_flag
from		tducfsliptype
where 	sliptype_code = :as_sliptype
using		itr_sqlca;
return li_flag
end function

private function integer of_post2debt (string as_branchid, string as_debtno, string as_creditno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt, string as_entryid);string		ls_lotno
dec		ldec_balqty

adec_slipamt *= of_GetOprFlag(as_sliptype)

if adec_slipamt <> 0 then
	if of_post2debtmaster ( as_branchid,  as_debtno,  adt_slipdate,  as_sliptype,  as_slipref,  adec_slipamt,  adec_balamt, as_entryid) < 0 then return -1
	if of_post2debtdet (as_branchid, as_debtno, as_creditno, adt_slipdate, as_sliptype, as_slipref, adec_slipamt, adec_balamt) < 0 then return -1
	if of_post2debtstmt ( as_branchid,  as_debtno, as_creditno,  adt_slipdate,  as_sliptype,  as_slipref,  adec_slipamt,  adec_balamt, as_entryid) < 0 then return -1
end if

return 1
end function

private function integer of_post2debtmaster (string as_branchid, string as_debtno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt, string as_entryid);
if mid(as_sliptype, 1, 2) = 'AR' then
	update		tddebtmaster
	set				debt_amt	=  nvl(debt_amt, 0) + :adec_slipamt,
					creditbalance_amt = credit_amt - (nvl(debt_amt, 0) + :adec_slipamt),
					entry_id = :as_entryid, 
					entry_date = :adt_slipdate
	where		coop_id	= :as_branchid and
					debt_no	= :as_debtno
	using 			itr_sqlca ;
elseif mid(as_sliptype, 1, 2) = 'AP' then
	update		tdcredmaster
	set				cred_amt	=  nvl(cred_amt, 0) + :adec_slipamt,
					creditbalance_amt = credit_amt - (nvl(cred_amt, 0) + :adec_slipamt),
					entry_id = :as_entryid, 
					entry_date = :adt_slipdate
	where		coop_id	= :as_branchid and
					cred_no	= :as_debtno
	using 			itr_sqlca ;
end if
return itr_sqlca.sqlcode


//if itr_sqlca.SQLNRows <= 0 then
//	insert into tddebtmaster ( branch_id, store_id, product_no, product_status, minpoint_qty, balance_qty, bfbalance_qty , taxtype_code, tax_rate, cost_amt, costavg_amt)
//	values (:as_branchid, :as_storeid, :as_prodcd, 1, null, :adec_qty, 0, 'E', 7, :adec_unitprice, :adec_unitprice) using itr_sqlca;
//end if

//select 		balance_qty
//into			:adec_balqty
//from			tddebtmaster
//where		branch_id	= :as_branchid and
//				product_no	= :as_prodcd
//using 			itr_sqlca ;
//

end function

private function integer of_post2debtstmt (string as_branchid, string as_debtno, string as_creditno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt, string as_entryid);long			li_seqno
dec			ldec_costamt
string			ls_debttype
dec			ldec_balqty

select			max ( seq_no )
into			:li_seqno
from			tddebtdet
where		coop_id		= :as_branchId and
				debt_no		= :as_debtno 
using itr_sqlca;

if itr_sqlca.sqlcode <> 0 or isnull( li_seqno ) then
	li_seqno = 0
end if

li_seqno	++
//
// เอาเลขที่ใบตั้งหนี้มา ด้วย รับมาจาก argument
if as_sliptype = 'APC' or as_sliptype = 'APDN' or as_sliptype = 'APCN' or as_sliptype = 'APRC' then

	ls_debttype = 'AP'
	select 	nvl(cred_amt, 0)
	into			:ldec_balqty
	from		tdcredmaster
	where 	coop_id = :as_branchId and
				cred_no = :as_debtno
	using itr_sqlca;
				
elseif as_sliptype = 'ARC' or as_sliptype = 'ARDN' or as_sliptype = 'ARCN' or as_sliptype = 'ARRC' then
	ls_debttype = 'AR'
	select 	nvl(debt_amt, 0)
	into			:ldec_balqty
	from		tddebtmaster
	where 	coop_id = :as_branchId and
				debt_no = :as_debtno
	using itr_sqlca;
end if

  INSERT INTO "TDDEBTDET"  
         ( "COOP_ID",   
           "DEBTBRANCH_ID",   
           "DEBT_NO",   
           "SEQ_NO",   
           "CREDITDOC_NO",   
           "DEBTDET_DATE",   
           "DEBTDETTYPE_CODE",   
           "DEBTINCDECTYPE_CODE",   
           "REFDOC_NO",   
           "DEBTDET_AMT",   
           "DEBTBALANCE_AMT",   
           "DEBTDET_STATUS",   
           "REMARK",   
           "ENTRY_ID",   
           "ENTRY_DATE" )  
  VALUES ( :as_branchid,   
           :as_branchid,   
           :as_debtno,   
           :li_seqno,   
		  :as_creditno,
           :adt_slipdate,   
           :ls_debttype,   
           :as_sliptype,   
           :as_slipref,   
           abs(:adec_slipamt),   
           :ldec_balqty,   
           1,   
           null,
		  :as_entryid,
		  :adt_slipdate) 
  using itr_sqlca;
return 1
end function

public function integer of_post2debtdet (string as_branchid, string as_debtno, string as_creditno, datetime adt_slipdate, string as_sliptype, string as_slipref, decimal adec_slipamt, decimal adec_balamt);//
//// ใบตั้งหนี้ = lot
//// ต้องมีลำดับใบตั้งหนี้
//
////long		li_startno, li_endno
////dec		ldec_cost, ldec_balqty
////boolean	lb_err = false
////
if  as_sliptype = 'APC' or as_sliptype =  'ARC' then 
	//	Insert table ตั้งหนี้ 
	
elseif as_sliptype = 'APDN' or as_sliptype =  'ARDN' then 
	// update ยอดใบตั้งตามใบตั้งหนี้ที่รับมา
	update 	tddebtcredit 
	set 		debt_amt = debt_amt + :adec_slipamt
	where 	coop_id = :as_branchid and
				credittype_code = :as_sliptype and
				creditdoc_no = :as_creditno
	using		itr_sqlca;
elseif as_sliptype = 'APRC' or as_sliptype =  'ARRC' or as_sliptype = 'APCN' or as_sliptype =  'ARCN'  then 
	// update ยอดใบตั้งตามใบตั้งหนี้ที่รับมา
	update 	tddebtcredit 
	set 		debt_amt = debt_amt - :adec_slipamt
	where 	coop_id = :as_branchid and
				credittype_code = :as_sliptype and
				creditdoc_no = :as_creditno
	using		itr_sqlca;
end if

return itr_sqlca.sqlcode

end function

public function integer of_create_debt_creditslip_bak (string as_branchid, datetime adt_operdate);integer	li_ret = 1
n_ds		lds_hd, lds_dt, lds_debt
string		ls_sql
long		ll_count, i, ll_row, j = 0
boolean	lb_err = False
string		ls_debtno = '', ls_olddebtno = '', ls_branchid, ls_doccd, ls_slipno, ls_sliprefno
dec		ldec_amt, ldec_netamt = 0
datetime	ldt_refdate, ldt_duedate

inv_progress.istr_progress.progress_text		= "ประมวลผล ตั้งหนี้ลูกหนี้"
inv_progress.istr_progress.progress_index		= 0

lds_hd = create n_ds
lds_hd.Dataobject = 'd_tradesrv_fin_arcredit_header'
lds_hd.SetTransObject(itr_sqlca)

lds_dt = create n_ds
lds_dt.Dataobject = 'd_tradesrv_fin_arcredit_detail'
lds_dt.SetTransObject(itr_sqlca)

//ls_sql = "  SELECT TDSTOCKSLIP.BRANCH_ID, "
//ls_sql += "  TDSTOCKSLIP.DEBT_NO,   "
//ls_sql += "  TDSTOCKSLIP.SLIPTYPE_CODE,   "
//ls_sql += "  TDSTOCKSLIP.SLIP_NO,   "
//ls_sql += "  TDSTOCKSLIP.SLIP_DATE,   "
//ls_sql += "  TDSTOCKSLIP.DUE_DATE,  "
//ls_sql += "  TDSTOCKSLIP.SLIPNET_AMT,   "
//ls_sql += "  TDSTOCKSLIP.SLIP_STATUS   "
//ls_sql += "  FROM TDSTOCKSLIP   "
//ls_sql += "  WHERE ( TDSTOCKSLIP.PAYMENTBY = 'LON' ) and  "
//ls_sql += "  ( TDSTOCKSLIP.SLIP_STATUS = 8 ) AND "
//ls_sql += "  ( TDSTOCKSLIP.SLIPTYPE_CODE = 'IV' )  and " 
//ls_sql += "  ( TRUNC(TDSTOCKSLIP.SLIP_DATE) <= trunc(to_date('" + string(date(adt_operdate)) + "', 'DD/MM/YYYY')))   " 
//ls_sql += "  ORDER BY  TDSTOCKSLIP.DEBT_NO, TDSTOCKSLIP.SLIP_DATE "    
//
//lds_debt = f_newdatastoresql(itr_sqlca, ls_sql) 
//
//ls_sql = "DataWindow.Table.UpdateTable='TDSTOCKSLIP'~t"
//ls_sql += "BRANCH_ID.Key=Yes~t"
//ls_sql += "SLIPTYPE_CODE.Key=Yes~t"
//ls_sql += "SLIP_NO.Key=Yes~t"
//ls_sql += "SLIP_STATUS.Update=Yes~t"
//lds_debt.Modify(ls_sql)

lds_debt = create n_ds
lds_debt.dataobject = 'd_closedate'

lds_debt.SetTransObject(itr_sqlca)
ll_count = lds_debt.retrieve('IV', adt_operdate)
if ll_count > 0 then
	for i = 1 to ll_count
		ls_debtno = lds_debt.GetItemString(i, 'debt_no')
		ls_branchid = lds_debt.GetItemString(i, 'coop_id')
		ls_sliprefno = lds_debt.GetItemString(i, 'slip_no')
		ldec_amt = lds_debt.GetItemDecimal(i, 'slipnet_amt')
		ldt_refdate = lds_debt.GetItemDateTime(i, 'slip_date')
		ldt_duedate = lds_debt.GetItemDateTime(i, 'due_date')
		lds_debt.SetItem(i, 'slip_status', 0)
//		Messagebox('test', ls_sliprefno)
		if 	ls_olddebtno <> ls_debtno	then 
			try
				this.of_setsrvdoccontrol( true )
				ls_doccd = of_getdoctype(ls_branchid, 'ARC')
				ls_slipno	= inv_docsrv.of_getnewdocno( ls_branchid , ls_doccd)	// get เลขที่เอกสาร
			catch( throwable lthw_getreqdoc )
				ithw_exception.text	= "~r~nพบขอผิดพลาด (20.1)" + lthw_getreqdoc.text
				lb_err = true
			end try
			ll_row = lds_hd.insertrow(0)
			lds_hd.SetItem(ll_row, 'coop_id', ls_branchid)
			lds_hd.SetItem(ll_row, 'creditdoc_no', ls_slipno)
			lds_hd.SetItem(ll_row, 'sliptype_code', 'ARC')
			lds_hd.SetItem(ll_row, 'creditdoc_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'debt_no', ls_debtno)
			lds_hd.SetItem(ll_row, 'entry_id', 'System')
			lds_hd.SetItem(ll_row, 'entry_date', adt_operdate)
			lds_hd.SetItem(ll_row, 'debt_status', 8)
			ldec_netamt = ldec_amt
			j = 1
		else
			ldec_netamt += ldec_amt
			j++
		end if
		ll_row = lds_dt.insertrow(0)
		lds_dt.SetItem(ll_row, 'coop_id', ls_branchid)
		lds_dt.SetItem(ll_row, 'creditdoc_no', ls_slipno)
		lds_dt.SetItem(ll_row, 'seq_no', j)
		lds_dt.SetItem(ll_row, 'credittype_code', 'ARC')
		lds_dt.SetItem(ll_row, 'refdoc_no', ls_sliprefno)
		lds_dt.SetItem(ll_row, 'refdoc_date', ldt_duedate)
		lds_dt.SetItem(ll_row, 'due_date', ldt_refdate)
		lds_dt.SetItem(ll_row, 'debt_amt', ldec_amt)
//		w_td_test.of_settext(  w_td_test.of_gettext() + 'Detail' + ls_slipno + ': ' + string(j) + '. ' + ls_debtno + ' ' + ls_sliprefno + ' = ' + string(ldec_amt) + ' Sum = ' + string(ldec_netamt) + '~r~n')
		if i+1 <= ll_count then
			ls_olddebtno = ls_debtno
			ls_debtno = lds_debt.GetItemString(i + 1, 'debt_no')
			if ls_debtno <> ls_olddebtno then
				lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
				//update
				if lds_hd.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
					ithw_exception.text	+= lds_hd.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
					
				// บันทึก Slip
				if lds_dt.update() <> 1 then
					ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
					ithw_exception.text	+= lds_dt.of_geterrormessage()
					lb_err = true; Goto objdestroy
				end if
				lds_hd.Reset()
				lds_dt.Reset()
				of_post2debt ( ls_branchid,  ls_debtno, ls_slipno,  adt_operdate,  'ARC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
				inv_progress.istr_progress.subprogress_text	= "รหัสลูกหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
				inv_progress.istr_progress.subprogress_index	= i
//				w_td_test.of_settext(  w_td_test.of_gettext() + 'Header' + ls_slipno + ': ' + string(j) + '. ' + ls_debtno + ' ' + ls_sliprefno + ' = ' + string(ldec_amt) + ' Sum = ' + string(ldec_netamt) + '~r~n')
			end if
		else
//			Messagebox('>', ldec_netamt)
			lds_hd.SetItem(1, 'debt_amt', ldec_netamt)
			if lds_hd.update() <> 1 then
				ithw_exception.text	= "บันทึกข้อมูล Slip ไม่ได้ "
				ithw_exception.text	+= lds_hd.of_geterrormessage()
				lb_err = true; Goto objdestroy
			end if
				
			// บันทึก Slip
			if lds_dt.update() <> 1 then
				ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
				ithw_exception.text	+= lds_dt.of_geterrormessage()
				lb_err = true; Goto objdestroy
			end if
			lds_hd.Reset()
			lds_dt.Reset()
			of_post2debt ( ls_branchid,  ls_debtno, ls_slipno,  adt_operdate,  'ARC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
			inv_progress.istr_progress.subprogress_text	= "รหัสลูกหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
			inv_progress.istr_progress.subprogress_index	= i
			//update
//			w_td_test.of_settext(  w_td_test.of_gettext()  + 'Header' +  ls_slipno + ': ' + string(j) + '. ' + ls_debtno + ' ' + ls_sliprefno + ' = ' + string(ldec_amt) + ' Sum = ' + string(ldec_netamt) + '~r~n')
		end if
//		of_post2debt ( ls_branchid,  ls_olddebtno, ls_slipno,  adt_operdate,  'ARC',  ls_slipno,  ldec_netamt,  ldec_netamt,  'Auto')
//		inv_progress.istr_progress.subprogress_text	= "รหัสลูกหนี้ "+ ls_debtno +" ยอดหนี้ "+string( ldec_netamt )
//		inv_progress.istr_progress.subprogress_index	= i
	next
end if
// บันทึก Slip
if lds_debt.update() <> 1 then
	ithw_exception.text	= "บันทึกข้อมูล SlipDetail ไม่ได้ "
	ithw_exception.text	+= lds_dt.of_geterrormessage()
	lb_err = true; Goto objdestroy
end if

objdestroy:
if isvalid(lds_hd) then destroy lds_hd
if isvalid(lds_dt) then destroy lds_dt
if isvalid(lds_debt) then destroy lds_debt

if lb_err then
//	rollback using itr_sqlca;
	li_ret = -1
else
//	commit using itr_sqlca;
	li_ret = 1
end if

return li_ret



end function

on n_cst_tradesrv_process.create
call super::create
TriggerEvent( this, "constructor" )
end on

on n_cst_tradesrv_process.destroy
TriggerEvent( this, "destructor" )
call super::destroy
end on

event constructor;ithw_exception = create throwable
end event

