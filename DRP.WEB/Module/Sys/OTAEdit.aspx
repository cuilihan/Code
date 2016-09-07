<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OTAEdit.aspx.cs" Inherits="DRP.WEB.Module.Sys.OTAEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="Script/OTASetting.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:Panel ID="pnlContainer" CssClass="easyui-panel" Style="padding: 15px; padding-top: 20px;" iconCls="icon-reload"
                runat="server">
                <table cellspacing="1" cellpadding="1" class="tblEdit" border="0">
                    <tr>
                        <td class="datalabel" style="width: 100px!important;">平台：
                        </td>
                        <td style="padding: 10px;" colspan="3">
                            <asp:DropDownList ID="ddlSupplier" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="datalabel" style="width: 100px!important;">主账号：
                        </td>
                        <td style="padding: 10px;">
                            <asp:TextBox ID="AcctID" runat="server"></asp:TextBox>
                        </td>
                        <td class="datalabel" style="width: 100px!important;">密码：
                        </td>
                        <td style="padding: 10px;">
                            <asp:TextBox ID="AcctPwd" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="datalabel" style="width: 100px!important;">AppID：
                        </td>
                        <td style="padding: 10px;">
                            <asp:TextBox ID="AppId" runat="server"></asp:TextBox>
                        </td>
                        <td class="datalabel" style="width: 100px!important;">AppKey：
                        </td>
                        <td style="padding: 10px;">
                            <asp:TextBox ID="AppKey" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="datalabel" style="width: 100px!important;">名称：
                        </td>
                        <td style="padding: 10px;">
                            <asp:TextBox ID="OTA" runat="server" onblur="toCheckName()" ClientIDMode="Static"></asp:TextBox><span style="color: red"><asp:Label ID="lblError" runat="server" Text="" ClientIDMode="Static"></asp:Label></span>
                        </td>
                        <td class="datalabel" style="width: 100px!important;">ApiUrl：
                        </td>
                        <td style="padding: 10px;">
                            <asp:TextBox ID="OTAServiceUrl" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 10px; text-align: right;" colspan="4">
                            <asp:CheckBox ID="isSynchronize" runat="server" />
                            <label for="isSynchronize" style="color: red; font-size: 16px;">只同步新增订单</label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding: 5px; text-align: center;">
                            <div id="btnOpt">
                                <asp:LinkButton ID="btnSave" ClientIDMode="Static" iconCls="icon-save" runat="server" OnClick="btnSave_Click" CssClass="easyui-linkbutton">保存</asp:LinkButton>
                            </div>
                            <div style="color: Red;" id="tips" class="hide">
                                <img src="../../UI/images/loading.gif" />
                                <div style="padding: 6px;">
                                    正在提交数据，请稍候...
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:HiddenField ID="hfID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hfOTAisHave" runat="server" />
        </div>
    </form>
</body>
</html>
