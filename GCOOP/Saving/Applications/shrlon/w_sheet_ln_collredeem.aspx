﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="w_sheet_ln_collredeem.aspx.cs" Inherits="Saving.Applications.shrlon.w_sheet_ln_collredeem" %>

<%@ Register src="w_sheet_ln_collredeem_ctrl/DsMain.ascx" tagname="DsMain" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "collmast_no") {
                PostCollmastNo();
            }
        }

        function OnDsMainClicked(s, r, c) {
            if (c == "b_collmast") {
                Gcoop.OpenIFrame('580', '590', 'w_dlg_loan_collredeem.aspx', '');

            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame('580', '590', 'w_dlg_loan_collredeem.aspx', '');
        }

        function ReceiveMemberNo(member_no, memb_name) {
            dsMain.SetItem(0, "member_no", member_no);
            PostMemberNo();
        }

        function GetCollmastNoIFrame(collmastNo) {
            dsMain.SetItem(0, "collmast_no", collmastNo);
            PostCollmastNo();
        }

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <br />
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    </asp:Content>
