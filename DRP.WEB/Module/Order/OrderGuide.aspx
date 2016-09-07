<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderGuide.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderGuide" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
    <title>安排导游</title> 
    <link href="../../Scripts/Plugin/lhgdialog/skins/default.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/lhgdialog/lhgdialog.min.js?skin=iblue" type="text/javascript"></script>
    <script src="Script/OrderGuide.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 10px;">
        <div style="color: red; margin-bottom: 5px;">
            提醒：导游报账时以手机号与报账密码验证；姓名、手机号、密码均为必填项目。 
        </div>
        <table class="tblEdit">
            <tr>
                <th style="width: 30px;">序</th>
                <th>导游姓名</th>
                <th>导游手机号</th>
                <th>导游报账密码</th>
                <th style="width: 50px;"></th>
            </tr>
            <tbody id="tblData">
                <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                <input type="hidden" value="<%# Eval("GuideID") %>" />
                            </td>
                            <td>
                                <input type="text" class="textbox" style="width: 98%; height: 26px;" value="<%# Eval("GuideName") %>" />
                            </td>
                            <td>
                                <input type="text" class="textbox" style="width: 98%; height: 26px;" value="<%# Eval("Mobile") %>" />
                            </td>
                            <td>
                                <input type="text" class="textbox" style="width: 98%; height: 26px;" value="<%# Eval("AcctPwd") %>" />
                            </td>
                            <td style='text-align: center;'><a href='javascript:;' onclick='g.fnDelete(this)'>删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tr>
                <td colspan="5" style="text-align: right; padding-right: 5px;">
                    <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-ok" id="btnSelect">选择导游</a>
                    <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-add" id="btnAdd">增加导游</a>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: center;">
                    <a href="javascript:;" id="btnSave" class="easyui-linkbutton" iconcls="icon-save">保存</a>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
