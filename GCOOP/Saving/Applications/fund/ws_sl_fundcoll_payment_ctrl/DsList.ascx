<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" 
Inherits="Saving.Applications.fund.ws_sl_fundcoll_payment_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="3%">
        </th>
        <th width="22%">
            รายละเอียดการจ่าย
        </th>
        <th width="15%">
            วันที่กองทุน
        </th>
        <th width="15%">
            เลขสัญญา
        </th>
       <%-- <th width="15%">
            วงเงินกู้
        </th>--%>
        <th width="15%">
            ยอดกองทุน
        </th>
        <th width="15%">
            รวมจ่ายสุทธิ์
        </th>
    </tr>
</table>
<asp:Panel ID="Panel2" runat="server" Height="200px" Width="750px" ScrollBars="Auto">
    <table class="DataSourceRepeater">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%" align="center">
                        <asp:CheckBox ID="operate_flag" runat="server" />
                    </td>
                    <td width="22%">
                        <asp:TextBox ID="slipitem_desc" runat="server"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="operate_date" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="loancontract_no" runat="server" Style="text-align:center" ReadOnly="true" />
                    </td>
<%--                    <td width="15%">
                        <asp:TextBox ID="loanapprove_amt" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>--%>
                    <td width="15%">
                        <asp:TextBox ID="balance" runat="server" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="itempay_amt" runat="server" BackColor="Violet" Style="text-align: right" ToolTip="#,##0.00" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
