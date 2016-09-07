<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="DRP.WEB.Module.Guide.Help" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        ul li
        {
            font-size: 14px;
        }

        li a
        {
            font-size: 17px;
            font-style: italic;
            color: blue;
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel" iconcls="icon-tip" title="初始化向导" style="padding: 10px;">
            欢迎您，【<span style="font-weight: bold;"><asp:Literal ID="lblUserName" runat="server"></asp:Literal></span>】：
         <div style="line-height: 30px; padding-left: 20px;">
             <div style="color: red; padding-top: 20px; font-size: 14px; margin-bottom:10px; font-weight: bold;">
                 第一次使用系统时，我们强烈建议您按如下的步骤设置必要参数：
             </div>

             <ul>
                 <li>1、设置部门，管理入口：系统管理--><a href='/Module/Sys/Dept.aspx'>部门管理</a>，如XX门店
                 </li>
                 <li>2、设置用户，管理入口：系统管理--><a href='/Module/Sys/User.aspx'>用户管理</a>，设置公司员工的账号及密码
                 </li>
                 <li>3、角色管理，管理入口：系统管理--><a href='/Module/Sys/Role.aspx'>角色管理</a>，设置角色为了分配权限，如业务、计调、财务
                 </li>
                 <li>4、权限管理，管理入口：系统管理--><a href='/Module/Sys/Permission.aspx'>权限管理</a>，先设置角色才能分配权限，有先后顺序
                 </li>
                 <li>5、目的地设置，管理入口：系统管理-->参数设置--><a href='/Module/Glo/Destination.aspx'>目的地管理</a>，设置周边短线的目的地，长线与出境系统已内置，可以自行修改
                 </li>
                  <li>6、其他参数，管理入口：系统管理-->参数设置，系统内置一些常用的参数，您可以自行修改。
                 </li>
             </ul>
         </div>
            <div style="color:red; margin:30px 0px 50px 20px; font-size:15px;">
                <img src="../../App_Themes/Default/Images/hand.png" />&nbsp;&nbsp;更多的使用帮助请参考“<a href='http://www.58datu.com/help/doc.aspx' style="font-size:18px; font-style:italic; color:blue; text-decoration:underline;" target="_blank">系统使用手册</a>”
            </div>
        </div>
    </form>
</body>
</html>
