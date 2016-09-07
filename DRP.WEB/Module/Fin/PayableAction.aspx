<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayableAction.aspx.cs" Inherits="DRP.WEB.Module.Fin.PayableAction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>付款操作</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
    <script src="Script/PayableAction.js" type="text/javascript"></script>
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
                    <td class="rowlabel">订单编号： 
                    </td>
                    <td style="width: 110px;">
                        <asp:TextBox ID="txtOrderNo" ClientIDMode="Static" Width="90" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">出团日期： 
                    </td>
                    <td style="width: 200px;">
                        <asp:TextBox ID="sDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                        ~
                        <asp:TextBox ID="eDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">部门：</td>
                    <td>
                        <asp:DropDownList ID="ddlDept" ClientIDMode="Static" AppendDataBoundItems="true" runat="server" Width="90">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="rowlabel">操作人：</td>
                    <td>
                        <asp:DropDownList ID="ddlCreator" ClientIDMode="Static" AppendDataBoundItems="true"
                            runat="server" Width="90">
                            <asp:ListItem Text="所有" Value=""></asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <a href="javascript:;" id="btnQuery" class="easyui-linkbutton" iconcls="icon-search">查询</a>
                        <a href="javascript:;" id="btnSave" class="easyui-linkbutton" iconcls="icon-save">保存付款信息</a>
                        <asp:LinkButton ID="btnExport" CssClass="easyui-linkbutton" OnClick="btnExport_Click" OnClientClick="return o.fnSetExport()" iconcls="icon-export" runat="server">导出</asp:LinkButton> 
                    </td>
                    <td style="text-align: right; color: red;">注：付款只针对已选择的订单付款，可以批量操作！
                    </td>
                </tr>
            </table>
        </div>
        <table id="tblData">
        </table>

    </form>
</body>
</html>
