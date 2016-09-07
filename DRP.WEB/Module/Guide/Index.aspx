<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DRP.WEB.Module.Guide.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>默认首页</title>
    <link href="Index.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/eChart/echarts.js"></script>
    <script src="Script/Index.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div data-options="region:'center'" title="工作台" style="padding: 0px;">
            <table style="width: 100%;" cellspacing="5px" id="tblData">
                <tr>
                    <td valign="top" style="width: 50%;">
                        <div class="easyui-panel" iconcls="icon-tip" title="通知公告【<a style='color:red;' href='/Module/Glo/NoticeList.aspx'>更多</a>】" style="padding: 5px 10px; height: 160px; overflow: hidden;">
                            <ul class="noticelist">
                                <asp:Literal ID="lblNotice" runat="server"></asp:Literal>
                            </ul>
                        </div>
                    </td>
                    <td valign="top" style="width: 50%;">
                        <div class="easyui-panel" title="个人中心" iconcls="icon-user" style="overflow: hidden; padding: 10px; height: 160px;">
                            <div>
                                <span class="myinfo_title">欢迎您，</span>【<asp:Label ID="lblUserName" Font-Bold="true" runat="server" Text=""></asp:Label>】
                                <span class="myinfo_link"><a href="/Module/My/MyPasswrod.aspx">修改密码</a>  <a href="/Logout.aspx" target="_parent">退出系统</a></span>
                            </div>
                            <div class="message">
                                <ul>
                                    <asp:Literal ID="lblMessage" runat="server"></asp:Literal>
                                </ul>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td valign="top" colspan="2">
                        <div class="easyui-panel" iconcls="icon-sum" id="pnlStatistic" title="数据统计" style="padding: 10px;" runat="server">
                            <div id="rptbar" class="rpt_bar">
                                <a id="1" class="rpt_bar_on">同行散客</a>
                                <a id="2">自主班散客</a>
                                <a id="3">企业团</a>
                                <a id="5">单项业务</a>
                                <a id="6">机票订单</a>
                                <span style="position: absolute; right: 15px; font-size: 14px; font-weight: bold; color: red; padding-top: 5px;">
                                    <asp:Literal ID="lblYear" runat="server"></asp:Literal>
                                </span>
                            </div>
                            <div id="rptChart" style="height: 120px; width: 100%; overflow: auto;"></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div data-options="region:'east',collapsed:false,collapsible:false" title="工具栏" style="width: 240px; padding: 10px;">
            <div class="line" style="width: 200px;">
                常用链接
                <span style="position: absolute; right: 2px;">【<a href="/Module/My/Favorites.aspx">设置</a>】</span>
            </div>
            <div class="toolbar_right" style="height: 250px; overflow: auto;">
                <ul class="noticelist">
                    <asp:Literal ID="lblUserFavorites" runat="server"></asp:Literal>
                </ul>
            </div>
            <div class="line" style="width:200px; height:30px;line-height:30px;">
                服务中心<i class="new"></i></div>
            <div class="toolbar_right" style="height: 100px;">
                <ul class="noticelist">
                    <li><i class="red_arrow"></i><a href="/Module/My/MyInfo.aspx">我的服务</a>
                    </li>
                    <li><i class="red_arrow"></i><a href="http://www.58datu.com/doc/protocol.html" target="_blank">服务协议</a>
                    </li>
                    <li><i class="red_arrow"></i><a href="http://www.58datu.com/doc/price.aspx" target="_blank">收费标准</a>
                    </li>
                    <li><i class="red_arrow"></i><a href="http://www.58datu.com/doc/price.aspx#zhengce" target="_blank">优惠政策</a>
                    </li>
                </ul>
            </div>
            <div class="line" style="width: 200px;">工具栏</div>
            <div class="toolbar_right" style="min-height: 100px;">
                <ul class="noticelist">
                    <asp:Literal ID="lblOMPushTools" runat="server"></asp:Literal>
                </ul>
            </div>
        </div>

    </form>
</body>
</html>
