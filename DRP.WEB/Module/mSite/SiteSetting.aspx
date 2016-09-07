<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteSetting.aspx.cs" Inherits="DRP.WEB.Module.mSite.SiteSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>微站设置</title>
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/kindeditor/kindeditor.js"></script>
    <script src="Script/SiteSetting.js?v=1.1" type="text/javascript"></script>
</head>
<body class="easyui-layout">
    <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js?r=<%=(new Random()).Next(0, 999).ToString() %>"></script>
    <form id="form1" runat="server">
        <div title="微站设置" iconcls='icon-ok' data-options="region:'center',border:true" style="padding: 10px;">
            <table class="tblEdit">
                <tr>
                    <td class="rowlabel">微站启用状态：</td>
                    <td style="font-weight: bold; color: red;">
                        <asp:Literal ID="lblStatus" runat="server"></asp:Literal>
                        <asp:Literal ID="lblTips" runat="server"></asp:Literal>
                    </td>
                    <td class="rowlabel">是否显示线路：</td>
                    <td>
                        <asp:RadioButtonList ID="ShowRoute" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal">
                            <asp:ListItem Text="是，适应于无微网" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="否，适应于集成现有微网" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">微站发布地址：</td>
                    <td colspan="3">
                        <asp:HiddenField ID="Domain" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="OrgID" ClientIDMode="Static" runat="server" />
                        <asp:Label ID="lblDomain" ClientIDMode="Static" runat="server"></asp:Label>
                    </td>
                </tr> 
                <tr>
                    <td class="rowlabel"><span class="red">*</span>旅行社名称：</td>
                    <td>
                        <asp:TextBox ID="TravelName" ClientIDMode="Static" Width="200" runat="server"></asp:TextBox>
                    </td>

                    <td class="rowlabel"><span class="red">*</span>线路报名电话：</td>
                    <td>
                        <asp:TextBox ID="Phone" ClientIDMode="Static" Width="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">旅行社Logo：</td>
                    <td colspan="3">
                        <div>
                            <img id="imgLogo" runat="server" src="../../UI/themes/default/images/none.jpg" />
                        </div>
                        <div>
                            <input type="file" name="uploadify" id="uploadify" />
                            <asp:HiddenField ID="LogoUrl" ClientIDMode="Static" runat="server" />
                        </div>

                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding: 10px;">
                        <ul class="ul_tabs" id="ul_tabs">
                            <li><a>关于我们</a></li>
                            <li><a>轮显广告</a></li>
                        </ul>
                        <ul id="tab_conbox">
                            <li class="tab_con">
                                <asp:TextBox ID="AboutUs" runat="server" TextMode="MultiLine" Width="100%" Height="300"></asp:TextBox>

                            </li>
                            <li class="tab_con">
                                <table class="tblEdit">
                                    <tr>
                                        <th style="width:120px;">图片
                                        </th>
                                        <th>链接地址
                                        </th>
                                        <th style="width:40px;"></th>
                                    </tr>
                                    <tbody id="tblAd">
                                        <asp:Literal ID="lblAdInfo" runat="server"></asp:Literal>
                                    </tbody>
                                    <tr>
                                        <td colspan="3" style="text-align: right; padding-right: 15px;">
                                            <input type="file" name="adFile" id="adFile" />
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ul>
                    </td>
                </tr>
            </table>
            <div style="margin: 20px 0px; text-align: center;">
                <a class="easyui-linkbutton" runat="server" iconcls="icon-save" id="btnSave">保存设置</a>
            </div>
        </div>
    </form>
</body>
</html>
