<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaidItem.aspx.cs" Inherits="DRP.WEB.Module.Fin.PaidItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>已付款查询</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/PaidItem.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="toolbar">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel">订单名称： 
                    </td>
                    <td style="width: 110px;">
                        <asp:TextBox ID="txtName" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">付款日期： 
                    </td>
                    <td style="width: 200px;">
                        <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                    <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    </td>
                </tr>
            </table>
        </div>
        <table id="tblData" style="margin: 20px 0px;" class="tblEdit">
        </table>
    </form>
</body>
</html>
