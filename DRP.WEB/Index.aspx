<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="Index.aspx.cs" Inherits="DRP.WEB.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>旅管家®旅行社综合业务管理系统</title>
    <link href="UI/ad/ad.css" rel="stylesheet" />
    <script src="/Scripts/master.js?v=2.1" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            master.init();
        });
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div id="layout_wrap" data-options="region:'north',border:false">
            <div id="layout_top">
                <div class="layout_top_logo">
                    <asp:Literal ID="lblOrgProName" runat="server"></asp:Literal>
                </div>
                <div class="layout_top_toolbar">
                    <div style="text-align: right; height: 22px; padding-top: 3px;">
                        <ul>
                            <li id="btnSkin">
                                <a style="width: 110px; text-align: center;" href="javascript:;">当前皮肤：<span style="color: red;"><asp:Literal ID="lblCurSkin" runat="server"></asp:Literal></span><i class="arrow_down"></i></a>
                                <div class="skin_hidebox">
                                    <asp:LinkButton ID="btnBlue" Style="font-weight: normal;" runat="server" ToolTip="蓝色" OnClick="btnBlue_Click"><span class="skin_blue_block"></span>切换为蓝色</asp:LinkButton>
                                    <br />
                                    <asp:LinkButton ID="btnGray" Style="font-weight: normal;" runat="server" ToolTip="灰色" OnClick="btnGray_Click"><span class="skin_gray_block"></span>切换为灰色</asp:LinkButton>
                                </div>
                            </li>
                            <li><a href="/Module/Guide/UpdateLog.aspx" target="frmBench">更新日志</a></li>
                            <li><a href="http://www.58datu.com/help/doc.aspx" target="_blank">在线帮助</a></li>
                            <%--<li><a href="javascript:;" id="btnFavorites">收藏本站</a></li>--%>
                            <li><a href='/Logout.aspx'>退出系统</a></li>
                            <li><a  href="/Module/My/MyInfo.aspx" target="frmBench">我的服务</a></li>
                            <li id="btnService"><a style="width: 70px; text-align: center;" href="javascript:;">在线客服<i class="arrow_down"></i></a>
                                <div class="jq_hidebox">
                                    <asp:Literal ID="lblQQ" runat="server"></asp:Literal>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="layout_top_search">
                    <input class="easyui-searchbox" style="width: 200px;" data-options="prompt:'关键字模糊查询',menu:'#mm',searcher:doSearch" />
                    <div id="mm" style="width: 50px;">
                        <div data-options="name:'customer'">客户</div>
                        <div data-options="name:'order'">订单</div>
                        <div data-options="name:'resource'">资源</div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
            <div id="layout_menu_bg">
                <div class="layout_menu_user">
                    <div class="layout_menu_user_info" style="width: 130px; overflow: hidden;">
                        <div style="width: 135px; overflow: hidden;">
                            <a id="btnUserInfo" class="user" title="点击切换角色" href="javascript:;">
                                <asp:Literal runat="server" ID="lblLoginUserName"></asp:Literal>
                                <asp:Literal ID="lblRoleName" runat="server"></asp:Literal>
                                <i class="arrow_down"></i>
                            </a>
                        </div>
                        <div style="margin-top: 5px; text-align: left;">
                            <asp:Literal ID="lblSysDate" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                <div id="layout_menu_right" class="layout_menu_right">
                    <div class="layout_menu_nav_btn_l" title="向左移动菜单" onclick="master.fnMoveToLeft()">
                    </div>
                    <div class="layout_menu_nav" id="layout_menu_nav">
                        <div class="navRollWrap" id="navRollWrap">
                            <ul>
                                <asp:Literal runat="server" ID="lblNavigate"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <div class="layout_menu_nav_btn_r" title="向右移动菜单" onclick="master.fnMoveToRight()">
                    </div>
                </div>
            </div>
        </div>
        <div id="navigate" title="导航菜单" iconcls="icon-dir" data-options="region:'west',border:false,collapsible:false" style="width: 180px; border: 0px; overflow: hidden;">
            <!--导航菜单-->
            <ul id="navTreeMenu" class="ztree"></ul>
        </div>
        <div data-options="region:'center'" style="overflow: hidden; padding: 1px 1px 0px 2px;" id="workbench">
            <iframe frameborder="0" src="Module/Guide/Index.aspx" id="frmBench" name="frmBench" style="width: 100%; height: 100%; z-index: 1;"></iframe>
        </div>
        <div style="overflow: auto;" id="mPopWin">
        </div>
        <div class="pop_notice_panel" isdisplay="">
            <div class="panel_center">
                <!--全局消息（底部弹层）内容-->
                <div id="pop_notice_panel_close" onclick="master.fnPushMessage(true)">我知道了...</div>
                <div id="360ly_notice" style="text-align:center;"></div>
            </div>
        </div>
        <asp:HiddenField ID="UserID" ClientIDMode="Static" runat="server" />
        <!--个人消息提醒时间间隔-->
        <asp:HiddenField ID="PopMessageInterval" ClientIDMode="Static" runat="server" />
        <div id="UserRoleList" style="padding: 10px">
            <div>
                当前正使用的角色：<asp:Label ID="lblCurrentRole" ForeColor="Red" runat="server" Text=""></asp:Label>
            </div>
            <div class="rolelist">
                可切换使用的角色：
                <asp:Literal ID="lblRoles" runat="server"></asp:Literal>
            </div>
        </div>
        <div id="tms-help">
            <div id="help-content" title="本页功能使用手册"><a id="btnJITHelp">即时帮助</a></div>
            <div id="help-close" onclick="$('#tms-help').hide();">关闭</div>
        </div>
        <div id="help-window" class="hide">
            <div style="text-align: right; padding: 5px 10px 10px 0px;">
                更多帮助请参考“<a target="_blank" href='http://www.58datu.com/help/doc.aspx' style="font-size: 13px; color: blue; text-decoration: underline;">旅管家使用说明书</a>”
            </div>
            <div id="helpData">
            </div>
        </div>

        <script type="text/javascript">
            $('#tms-help').draggable();
        </script> 
    </form>
</body>
</html>

