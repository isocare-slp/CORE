<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_sl_fundcoll_payment.aspx.cs" 
Inherits="Saving.Applications.fund.ws_sl_fundcoll_payment_ctrl.ws_sl_fundcoll_payment" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;
        var dsList = new DataSourceTool;

        function Validate() {
//            var moneytype_code = dsMain.GetItem(0, "moneytype_code");
//            var tofrom_accid = dsMain.GetItem(0, "tofrom_accid");
//            if (!moneytype_code || !tofrom_accid) {
//                alert("กรุณาเลือการทำรายการและคู่บัญชี");
//            } else {
//                return confirm("ยืนยันการบันทึก?");
//            }
            return confirm("ยืนยันการบันทึก?");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                JsPostMember();
            }
            else if (c == "slip_date") {
                JsPostCalint();
            }
            else if (c == "moneytype_code") {
                JsPostTofromaccid();
            }
        }
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2Extend(650, 600, 'w_dlg_sl_member_search_tks.aspx', '')
            }
        }
        function GetValueFromDlg(memberno) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            JsPostMember();
        }
        function OnDsListItemChanged(s, r, c, v) {
            if (c == "operate_flag") {
                dsList.SetRowFocus(r);
                PostOperateFlag();
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <p style="color:Red">**ให้บันทึกทีละ 1 กองทุน</p>
    <uc2:DsList ID="dsList" runat="server" />
    <br />
    
</asp:Content>
