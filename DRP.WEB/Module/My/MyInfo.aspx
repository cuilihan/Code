<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="DRP.WEB.Module.My.MyInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div data-options="region:'center'" iconcls="icon-tip" title="企业订购产品服务信息" style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">企业名称：</td>
                    <td>
                        <asp:Literal runat="server" ID="OrgName"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">服务地址：</td>
                    <td>
                        <asp:Literal runat="server" ID="OrgDomain"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">服务期限：</td>
                    <td>
                        <asp:Literal runat="server" ID="DateLimit"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">服务状态：</td>
                    <td>
                        <asp:Literal runat="server" ID="DataStatus"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">用户数：</td>
                    <td>【<asp:Literal runat="server" ID="UserCount"></asp:Literal>】
                        <span style="padding-left: 2em; padding-right: 1em;">如需更多用户，请联系
                            <asp:Literal runat="server" ID="lblCompany"></asp:Literal>
                            购买。</span><a href="http://www.58datu.com/doc/price.aspx" target="_blank">【收费标准】</a>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">可用短信数：</td>
                    <td>【<asp:Literal runat="server" ID="SmsCount"></asp:Literal>】
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">订单：</td>
                    <td style="padding:15px;">购买总价格：【<asp:Literal runat="server" ID="TotalAmt"></asp:Literal>】元 
                            <table class="tblEdit">
                                <tr>
                                    <th style='width: 30px;'>序</th>
                                    <th style='width: 80px;'>购买金额</th>
                                    <th style='width: 80px;'>用户数</th>
                                    <th style='width: 160px;'>有效日期范围</th>
                                    <th style='width: 70px;'>收款人</th>
                                    <th style='width: 110px;'>收款日期</th>
                                    <th>备注</th>
                                </tr>
                                <tbody id="OrderList"></tbody>
                            </table>
                        
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">服务协议：
                    </td>
                    <td>【<a href="http://www.58datu.com/doc/protocol.html" target="_blank">在线阅读服务协议</a>】
                        <span style="width: 1em;"></span>
                        【<a href="http://www.58datu.com/doc/protocolDoc.aspx" target="_blank">服务协议下载</a> 】
                    </td>
                </tr>
            </table>
            <asp:HiddenField runat="server" ID="OrgID" ClientIDMode="Static" />
            <div style="padding: 20px 10px 20px 10px;">
                备注：“旅管家”官方网站为：http://www.58datu.com，谨防上当受骗。
            </div>
            <script type="text/javascript">
                $(function () {
                    var id = $("#OrgID").val();
                    if (id == "") return;
                    var u = "/Service/CommonData.ashx?action=3&id=" + id + "&r=" + getRand();
                    dataService.ajaxGet(u, function (data) {
                        if (data == "") {
                            $("#OrderList").html("<tr><td colspan='7'>未查询到相关的数据</td></tr>");
                        }
                        else {
                            var sb = [];
                            $(eval(data)).each(function (i) {
                                var c = this.UserCount == "-1" ? "不限" : this.UserCount;
                                sb.push("<tr>");
                                sb.push("<td style='text-align:center;'>" + (i + 1) + "</td>");
                                sb.push("<td style='text-align:center;'>" + this.PaidAmt + "</td>");
                                sb.push("<td style='text-align:center;'>" + c + "</td>");
                                sb.push("<td style='text-align:center;'>" + this.sDate + " / " + this.eDate + "</td>");
                                sb.push("<td style='text-align:center;'>" + this.Receiver + "</td>");
                                sb.push("<td style='text-align:center;'>" + this.ReceiveDate + "</td>");
                                sb.push("<td style='text-align:center;'>" + this.Comment + "</td>");
                                sb.push("</tr>");
                            });
                            $("#OrderList").html(sb.join(""));
                        }
                    });
                });
            </script>
        </div>
    </form>
</body>
</html>
