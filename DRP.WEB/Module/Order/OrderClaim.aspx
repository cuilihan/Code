<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderClaim.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderClaim" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单收款认领</title>
    <script src="Script/OrderClaim.js?r=1"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: #E6EDF9; border: 1px solid #9CB8E7; margin: 3px 0px 5px 5px; padding: 5px;">
            <div style="margin-bottom: 5px;">
                订单分类：
                  <asp:DropDownList ID="OrderType" ClientIDMode="Static" runat="server">
                      <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                      <asp:ListItem Text="同行散客订单" Value="1"></asp:ListItem>
                      <asp:ListItem Text="自主班散客订单" Value="2"></asp:ListItem>
                      <asp:ListItem Text="企业团订单" Value="3"></asp:ListItem>
                      <asp:ListItem Text="单项业务订单" Value="5"></asp:ListItem>
                      <asp:ListItem Text="机票订单" Value="6"></asp:ListItem>
                  </asp:DropDownList>
                <span style="padding-left: 1em;">订单名称：</span>
                <asp:TextBox ID="txtOrderName" ClientIDMode="Static" Width="80" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">编号：</span>
                <asp:TextBox ID="txtOrderNo" Width="70" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">客户名称：</span>
                <asp:TextBox ID="txtCustomer" Width="80" ClientIDMode="Static" runat="server"></asp:TextBox>
            </div>
            <div style="margin-bottom: 5px;">
                <span>查询日期：</span>
                <asp:DropDownList ID="ddlDateType" Width="90" runat="server">
                    <asp:ListItem Text="录入日期" Value="1"></asp:ListItem>
                    <asp:ListItem Text="出发日期" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="sDate" Width="84" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                ~
                <asp:TextBox ID="eDate" Width="84" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                <span style="padding-left: 1em;">订单金额：</span>
                <asp:TextBox ID="txtOrderAmt" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
            </div>
            <table id="tblData">
            </table>
            <table class="tblEdit" style="margin-top: 5px;">
                <tr>
                    <td class="rowlabel">已收款金额：</td>
                    <td style="color: red; font-weight: bold; width: 150px;" id="CollectedAmt">0</td>
                    <td class="rowlabel">收据编号：</td>
                    <td>
                        <asp:TextBox ID="BillNo" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">收款备注：</td>
                    <td colspan="3">
                        <asp:TextBox ID="Comment" ClientIDMode="Static" Width="90%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;"> 
                        <a href="javascript:;" id="btnSave" class="easyui-linkbutton" iconcls="icon-save">保存认领收款</a> 
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
