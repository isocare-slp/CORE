﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_st_search_catalogdea.aspx.cs"
    Inherits="Saving.Applications.cmd.dlg.w_dlg_st_search_catalogdea" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ค้นหาข้อมูลพัสดุครุภัณฑ์</title>

    <script type="text/javascript">
        // function selectRow จากdatawindow  "objd_sl_product_search_list" 
        // ส่งค่าตาม Request ของ main window ด้วยฟังค์ชั่น--->Getitem_idFromDlg(product_id, product_name);
        // จากนั้นก็จะปิดตัวเองลง
        function OnDetailClick(sender, rowNumber, objectName) {

            var product_id = objdw_detail.GetItem(rowNumber, "product_id");
            var lot_id = objdw_detail.GetItem(rowNumber, "lot_id");
            var isConfirm = confirm("ยืนยันต้องการเลือกข้อมูล " + product_id + ":" + lot_id);
            if (isConfirm) {
                window.opener.Getitem_idFromDlg(product_id, lot_id);
                window.close();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server" Width="540px" Font-Bold="True" Font-Names="tahoma"
        Font-Size="12px" ForeColor="#660033" GroupingText="ค้นหาข้อมูลพัสดุ-ครุภัณฑ์">
    <table style="width: 530px;">
        <tr>
            <td>
                <dw:WebDataWindowControl ID="Dw_main" runat="server" ClientScriptable="True" DataWindowObject="dw_cmd_search_catalogmas"
                    LibraryList="~/DataWindow/Cmd/cmd_catalog.pbl" Width="450px">
                </dw:WebDataWindowControl>
            </td>
            <td>
                <asp:Button ID="b_search" runat="server" Text="ค้นหา" Height="52px" Width="55px"
                    OnClick="b_search_Click" />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <div>
        <asp:Panel ID="Panel2" runat="server" Width="530px" Font-Bold="True" Font-Names="tahoma"
            Font-Size="12px" ForeColor="#660033" GroupingText="รายละเอียดการค้น">
            <dw:WebDataWindowControl ID="dw_detail" runat="server" DataWindowObject="dw_cmd_search_catalogdea"
                LibraryList="~/DataWindow/Cmd/cmd_catalog.pbl"  Width="530px" Style="top: 0px;
                left: 0px" RowsPerPage="10" HorizontalScrollBar="NoneAndClip" VerticalScrollBar="NoneAndClip"
                ClientEventClicked="OnDetailClick" ClientScriptable="True">
                <PageNavigationBarSettings NavigatorType="NumericWithQuickGo" Visible="True">
                </PageNavigationBarSettings>
            </dw:WebDataWindowControl>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HSqlTemp" runat="server" Value="0" Visible="False" />
    </form>
</body>
</html>
