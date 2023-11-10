﻿<%@ Page Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="w_sheet_admin_constant_config.aspx.cs"
    Inherits="Saving.Applications.mbshr.w_sheet_admin_constant_config" Title="Untitled Page" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        var timeout = 500;
        var closetimer = 0;
        var ddmenuitem = 0;

        //        function OnDwMenuListClick(sender, row, name){
        //            window.location = "?uoucf=" + objDwMenuList.GetItem(row, "object_name");
        //            return 1;
        //        }
     
        function mopen(id) {

            mcancelclosetime();
            if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
            ddmenuitem = document.getElementById(id);
            ddmenuitem.style.visibility = 'visible';
        }
        function mclose() {
            if (ddmenuitem) ddmenuitem.style.visibility = 'hidden';
        }

        function mclosetime() {
            closetimer = window.setTimeout(mclose, timeout);
        }

        function mcancelclosetime() {
            if (closetimer) {
                window.clearTimeout(closetimer);
                closetimer = null;
            }
        }

        function clickedMenu(strMenu) {
            var divDetail = document.getElementById(strMenu + "_detail");
            if (divDetail.style.visibility == "visible") {
                divDetail.style.visibility = "hidden";
                divDetail.style.height = "0px";
            } else {
                divDetail.style.visibility = "visible";
                divDetail.style.height = "auto";
            }
        }
    </script>

    <style type="text/css">
        .linkSpan
        {
            font-family: Tahoma, Sans-Serif;
            font-weight: bold;
            font-size: 12px;
            cursor: pointer;
            color: #005599;
            text-align: left;
            text-decoration: underline;
        }
        .menuCursor
        {
            top: 0px;
            left: 0px;
        }
        .menuCursor input
        {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <table>
        <tr>
            <td valign="top">
                <!--สำหรับ แสดงเมนูค่าคงที่ -->
                <table>
                    <asp:Literal ID="LtListMenu" runat="server"></asp:Literal>
                </table>
            </td>
            <td valign="top">
                <!--สำหรับ แสดงค่าคงที่ -->
                <div style="float: right;">
                    <asp:Label ID="lbl_cnstmenu" runat="server" Text="" CssClass="font14px"></asp:Label>
                </div>
                <table width="100%">
                    <tr>
                        <td width="100%" valign="top">
                            <asp:Panel ID="Panel1" runat="server">
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
