<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BizRpt.aspx.cs" Inherits="DRP.WEB.Module.Rpt.BizRpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>综合业务统计报表</title>
    <script src="Script/BizRpt.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabs" class="easyui-panel" title="综合业务统计表" iconcls="icon-search" style="padding: 6px 30px 10px 10px;">
            <table class="tblInfo">
                <tr>
                    <td class="rowlabel">订单年度：
                    </td>
                    <td style="width: 120px;">
                        <asp:DropDownList ID="ddlYear" ClientIDMode="Static" runat="server" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </td>
                    <td class="rowlabel">部门：
                    </td>
                    <td style="width: 220px;">
                        <asp:DropDownList ID="ddlDept" ClientIDMode="Static" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <a class="easyui-linkbutton" iconcls="icon-search" id="btnQuery">统计</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" style="padding: 10px;">
                        <table class="tblPrint" id="tblData"> 
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
