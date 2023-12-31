﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_account.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="30%">
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ตั้งแต่วันที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="start_date" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงวันที่:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="end_date" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td width="30%">
                    <div>
                        <span>รายการ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="cash_type" runat="server">
                    <asp:ListItem Value =""></asp:ListItem>
                    <asp:ListItem Value ="1">รายการเงินสด</asp:ListItem>
                    <asp:ListItem Value ="2">รายการโอน</asp:ListItem>
                    <asp:ListItem Value ="3">รายการปรับปรุง</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td width="30%">
                    <div>
                        <span>พิมพ์รายการ:</span>
                    </div>
                </td>
                <td>
                <asp:DropDownList ID="print" runat="server">
                <asp:ListItem Value =""></asp:ListItem>
                <asp:ListItem Value ="CR">Credit</asp:ListItem>
                <asp:ListItem Value ="DR">Debit</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
