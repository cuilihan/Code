<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Receipt.aspx.cs" Inherits="DRP.WEB.Module.Om.Receipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>收款</title>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>收款金额：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="PaidAmt" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>用户数：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="UserCount" data-options="required:true"></asp:TextBox>
                        <div style="color: red;">
                            注：限制用户数以最后一次更新的为主；<div style="padding-left: 20px;">如增加用户数，则叠加之前的用户数；</div>
                            <div style="padding-left: 20px;">-1表示不限制用户数。</div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">授权日期范围：
                    </td>
                    <td>
                        <asp:TextBox runat="server" Width="90" ID="sDate" onclick="WdatePicker()" ClientIDMode="Static" data-options="required:true"></asp:TextBox>
                        ~
                        <asp:TextBox runat="server" Width="90" ID="eDate" onclick="WdatePicker()" ClientIDMode="Static" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>收款人：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="Receiver" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">收款日期：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="ReceiveDate" onclick="WdatePicker()" ClientIDMode="Static" data-options="required:true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">备注：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="Comment" Width="250" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class="statusbar">
            <asp:LinkButton runat="server" ID="btnSave" CssClass="easyui-linkbutton" iconCls="icon-save" Text="保存" ClientIDMode="Static" OnClick="btnSave_Click" OnClientClick="return $('#form1').form('validate');">
            </asp:LinkButton>
        </div>
    </form>
</body>
</html>
