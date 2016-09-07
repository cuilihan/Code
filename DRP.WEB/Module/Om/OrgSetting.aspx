<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrgSetting.aspx.cs" Inherits="DRP.WEB.Module.Om.OrgSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js"></script> 
    <title>机构扩展设置</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel runat="server" ID="pnlContainer">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">机构名称：
                    </td>
                    <td>
                        <asp:Literal ID="OrgName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">自主班订单：
                    </td>
                    <td>
                        <asp:CheckBox ID="OrderAudit" Text="下订单后需要审核确认" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">订单收款流程：
                    </td>
                    <td>
                        <asp:CheckBox ID="OrderCollectedSign" Text="启用：销售收款登记->财务确认" ClientIDMode="Static" runat="server" />
                        <div style="color:red;">配置此功能，建议取消财务"银行收款明细管理"功能</div>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开通微网站：
                    </td>
                    <td>
                        <asp:CheckBox ID="MicroSite" Text="开通" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"> 订单导出使用日期：
                    </td>
                    <td>
                          <asp:Literal ID="cSData" runat="server"></asp:Literal>
                        ~
                        <asp:TextBox ID="cEDate" Width="80" onclick="WdatePicker()" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开通参与人员：
                    </td>
                    <td>
                        <asp:CheckBox ID="Participant" Text="开通" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">开通订单文件上传：
                    </td>
                    <td>
                        <asp:CheckBox ID="UploadFile" Text="开通" ClientIDMode="Static" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 5px; text-align: center;">
                        <asp:LinkButton ID="btnSave" OnClick="btnSave_Click" CssClass="easyui-linkbutton" iconcls="icon-save" runat="server">保存</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
