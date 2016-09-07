<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsSetting.aspx.cs" Inherits="DRP.WEB.Module.Om.SmsSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>短信设置</title>
    <script type="text/javascript">
        $(function () {
            $("#btnAdd").click(function () {
                var u = "/Module/Om/SmsSettingEdit.aspx?orgID=" + request("orgID");
                return openWindow({ "title": "短信设置", "width": 520, "height": 320, "url": u }, function () {
                    location.reload();
                });
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" style="padding: 10px;">
        <fieldset>
            <legend style="padding-left: 10px;">短信统计</legend>
            <div style="padding: 15px;">
                <table class="tblInfo">
                    <tr>
                        <td class="rowlabel">机构名称：
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblOrgInfo"></asp:Literal>
                        </td>
                        <td class="rowlabel">总短信数：
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblSmsCount"></asp:Literal>
                        </td>
                        <td class="rowlabel">已发短信数：
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblSmsSendCount"></asp:Literal>
                        </td>
                         <td class="rowlabel">剩余可发短信数：
                        </td>
                        <td style="color:red; font-weight:bold;">
                            <asp:Literal runat="server" ID="lblDeltSms"></asp:Literal>
                        </td>
                        <td class="rowlabel">累计充值金额：
                        </td>
                        <td>
                            <asp:Literal runat="server" ID="lblAmount"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset style="margin-top: 30px;">
            <legend style="padding-left: 10px;">短信充值</legend>
            <div style="padding: 15px;">
                <div style="margin-bottom: 10px;">
                    <a class="easyui-linkbutton" iconcls="icon-add" id="btnAdd">充值记录</a>
                </div>
                <table id="tblData" class="tblInfo">
                    <tr>
                        <th style="width: 30px;">序</th>
                        <th style="width: 80px;">短信条数</th>
                        <th style="width: 80px;">短信单价</th>
                        <th style="width: 80px;">充值金额</th>
                        <th>备注</th>
                        <th style="width: 80px;">创建人
                        </th>
                        <th style="width: 140px;">创建日期
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptData" OnItemDataBound="rptData_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Literal runat="server" ID="lblNo"></asp:Literal>
                                </td>
                                <td style="text-align: center;"><%# Eval("SmsCount") %></td>
                                <td style="text-align: center;"><%# Eval("UnitPrice") %></td>
                                <td style="text-align: center;"><%# Eval("Amount") %></td>
                                <td><%# Eval("Comment") %></td>
                                <td style="text-align: center;"><%# Eval("CreateUserName") %></td>
                                <td style="text-align: center;"><%# Eval("CreateDate") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </fieldset>
    </form>
</body>
</html>
