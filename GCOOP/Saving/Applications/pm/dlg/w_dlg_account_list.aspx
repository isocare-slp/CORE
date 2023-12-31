﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="w_dlg_account_list.aspx.cs"
    Inherits="Saving.Applications.pm.dlg.w_dlg_account_list" %>

<%@ Register Assembly="WebDataWindow" Namespace="Sybase.DataWindow.Web" TagPrefix="dw" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%=postCriteria%>
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function OnDwMainClick(s, r, c, v) {

                    Gcoop.GetEl("HdRow").value = r;
                    account_no = objDwMain.GetItem(r, "account_no");
                    parent.ChooseAcc(account_no);
                    parent.RemoveIFrame();
                }
         function OnDwCriClick(s, r, c, v) {
             if (c == "b_1") {
                 postCriteria();
             }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <dw:WebDataWindowControl ID="DwCriteria" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
            DataWindowObject="d_account_search" LibraryList="~/DataWindow/pm/pm_investment.pbl"
            ClientEventClicked="OnDwCriClick">
        </dw:WebDataWindowControl>
        <dw:WebDataWindowControl ID="DwMain" runat="server" AutoRestoreContext="False" AutoRestoreDataCache="True"
            AutoSaveDataCacheAfterRetrieve="True" ClientScriptable="True" ClientValidation="False"
            DataWindowObject="d_dlg_account_list" LibraryList="~/DataWindow/pm/pm_investment.pbl"
            ClientEventClicked="OnDwMainClick">
        </dw:WebDataWindowControl>
        <asp:HiddenField ID="HdRow" runat="server" Value="" />
        <input type="button" value="ปิดหน้าจอ" onclick="parent.RemoveIFrame();" />
        <asp:HiddenField ID="HdValue" runat="server" Value="" />
    </div>
    </form>
</body>
</html>
