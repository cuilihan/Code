<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsRpt.aspx.cs" Inherits="DRP.WEB.Module.Om.SmsRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>机构短信统计</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div title="机构短信统计" class="easyui-panel" style="padding:10px;" iconcls="icon-reload">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">日期范围：</td>
                    <td style="width: 300px;">
                        <asp:TextBox ID="sDate" ClientIDMode="Static" onclick="WdatePicker()" runat="server"></asp:TextBox>
                        ~
                 <asp:TextBox ID="eDate" ClientIDMode="Static" onclick="WdatePicker()" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <a class="easyui-linkbutton" iconcls="icon-search">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
