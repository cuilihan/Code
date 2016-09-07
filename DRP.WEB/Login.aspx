<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DRP.WEB.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta property="qc:admins" content="40303076136360416375" />
    <title>旅管家订单业务管理系统用户登录</title>
    <link href="UI/themes/login/Login.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/drp.core.js" type="text/javascript"></script>
    <script src="Scripts/Plugin/drpdialog/drpdialog.js" type="text/javascript"></script>
    <script src="Scripts/Login.js" type="text/javascript"></script>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <div style="background-color: #28A7E1;">
            <div class="logintop">
                <span>旅管家订单业务管理系统</span>
                <ul>
                    <li id="btnCode"><a style="padding: 0px 5px;" href="javascript:;">扫一扫</a><i class="arrow_down"></i>
                        <div class="jq_hidebox">
                            <asp:Literal runat="server" ID="lblQRCode"></asp:Literal>
                            <asp:Image runat="server" ID="QRCode" Visible="false" />
                        </div>
                    </li>
                    <li><a href="Down/IE8-WindowsXP-x86-CHS.exe" title="Windows XP 32位中文版">IE 8 下载</a></li>
                    <li><a href="Down/IE9-Windows7-x86-chs.exe" title="Windows7 32位中文版">IE 9 下载</a></li>
                    <li><a href="Down/Chrome.exe" title="Google 浏览器下载">Google Chrome下载</a></li>
                    <li>
                        <asp:DropDownList ID="OrgID" Visible="false" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Text="选择子系统" Value=""></asp:ListItem>
                        </asp:DropDownList></li>
                </ul>
            </div>
        </div>
        <div class="loginbody">
            <div class="remark">

                <div>
                    <span class="title_label">同频共振 </span>客户360度全方位视图、建立客户生态圈                  
                </div>
                <div>
                    <span class="title_label">有感知力</span>  数字化业务系统建设、感知企业智慧管理
                </div>
                <div>
                    <span class="title_label">凝聚发展</span> 开放式平台发挥集体智慧凝聚合力促发展
                </div>

                <div style="text-align: right; padding-top: 5px; padding-right: 100px; font-weight: bold;">
                    ——旅管家开发团队
                </div>
            </div>
            <div class="loginboxwrap">
                <div class="loginbox">
                    <div class="toolbar">
                        <ul>
                            <li id="1" class="toolbar_on">
                                <a href="javascript:;">用户登录</a>
                            </li>
                            <li id="2"><a href="javascript:;">导游报账登录</a>
                            </li>
                        </ul>
                    </div>
                    <div class="ubox">
                        <ul>
                            <li>
                                <asp:TextBox ID="txtUserID" EnableTheming="false" ClientIDMode="Static" CssClass="loginuser"
                                    Text="" runat="server"></asp:TextBox></li>
                            <li>
                                <asp:TextBox ID="txtPwd" EnableTheming="false" ClientIDMode="Static" CssClass="loginpwd"
                                    TextMode="Password" runat="server"></asp:TextBox>
                            </li>
                            <li>
                                <div style="float: left; width: 100px; height: 40px;">
                                    <asp:TextBox ID="txtCode" EnableTheming="false" ClientIDMode="Static" CssClass="logincode"
                                        runat="server" Text="验证码"></asp:TextBox>
                                </div>
                                <div style="float: left; width: 230px; padding-left: 10px;">
                                    <img id="ImgVerifyCode" src="" style="width: 100px; height: 38px; padding-bottom: 3px;" alt="验证码" title="验证码" />
                                    <span class="r-refresh" id="btnRefresh" title="刷新验证码"></span>
                                    <input type="hidden" value="" id="hfVerifyCode" />
                                </div>
                                <div style="clear: both;"></div>
                            </li>
                            <li style="text-align: center; padding-top: 20px;">
                                <asp:HiddenField ID="hfUserType" runat="server" ClientIDMode="Static" />
                                <asp:Button ID="btnLogin" ClientIDMode="Static" OnClick="btnLogin_Click" runat="server"
                                    Text="用户登录" CssClass="loginbtn" />
                                <div style="margin-top: 8px;">
                                    <asp:Label ID="lblTip" ForeColor="Red" runat="server" Text=""></asp:Label>
                                </div>
                            </li>
                        </ul>
                    </div>

                </div>
            </div>
        </div>
        <div class="loginfooter">
            <div class="loginbm">
                Copyright © 2014. All Rights Reserved 技术支持：<a href="http://www.58datu.com" target="_blank">苏州大途网络科技有限公司</a> <a href="http://www.miitbeian.gov.cn" target="_blank">苏ICP备15063245号</a>
            </div>
        </div>
        <div id="userRolesWrap" style="display: none;">
            <div class="tipbg">
                <div class="roletitle">
                    请选择本次登录的角色
                </div>
                <div class="rolelist">
                    <asp:Literal ID="lblRoles" runat="server"></asp:Literal>
                </div>
                <div class="rolefooter">
                    注：为了能够更好的体验系统的功能，我们建议您选择本次要要登录的角色身份，登录后也可以自由的切换不同的角色。
                </div>
            </div>
        </div>
    </form>
</body>
</html>
