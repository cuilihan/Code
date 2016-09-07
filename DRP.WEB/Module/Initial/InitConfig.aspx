<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InitConfig.aspx.cs" Inherits="DRP.WEB.Module.Initial.InitConfig" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统初始化</title>
    <link href="Init.css" rel="stylesheet" />
    <script type="text/javascript" src="Script/Init.js"></script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div title="系统初始化向导" data-options="region:'center',border:true" style="padding: 10px;">
            <div style="margin-bottom:10px; border:1px solid #ffd800; background-color :#ffd800; color:blue; height:26px; line-height:26px; padding-left:10px;">
                使用参数初始化功能时，系统会将已设置的数据清空且不可恢复，强烈建议初次使用系统时使用。
            </div>
            <div id="main_01">
                <table class="tblEdit" style="background-color: none;">
                    <tr>
                        <td colspan="2" style="text-align: center;">

                            <div class="init_title">
                                部门设置
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="rowlabel">公司名称:</td>
                        <td class="datainput">
                            <asp:TextBox ID="OrgName" Width="250" runat="server" ClientIDMode="Static"></asp:TextBox>&nbsp;&nbsp;<span class="red">*</span></td>
                    </tr>
                    <tr>
                        <td class="rowlabel">部门:</td>
                        <td class="datainput">
                            <ul class="list_01">
                                <li>
                                    <asp:TextBox ID="D1" runat="server" ClientIDMode="Static" Text="总经理室">总经理室</asp:TextBox></li>
                                <li>
                                    <asp:TextBox ID="D2" runat="server" ClientIDMode="Static" Text="业务部">业务部</asp:TextBox></li>
                                <li>
                                    <asp:TextBox ID="D3" runat="server" ClientIDMode="Static" Text="计调部">计调部</asp:TextBox></li>
                                <li>
                                    <asp:TextBox ID="D4" runat="server" ClientIDMode="Static" Text="财务部">财务部</asp:TextBox></li>
                            </ul>
                            <div class="btn_01">
                                <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <a href="javascript:;" class="easyui-linkbutton" id="next_01">下一步</a>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;</td>

                    </tr>
                </table> 
            </div>

            <div id="main_02" style="display: none;">
                <table class="tblEdit" style="">
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <div class="init_title">
                                角色设置
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="rowlabel">角色:</td>
                        <td class="datainput">
                            <ul class="list_01">
                                <li>
                                    <asp:TextBox ID="TextBox6" runat="server" ClientIDMode="Static" Text="总经理">总经理</asp:TextBox></li>
                                <li>
                                    <asp:TextBox ID="TextBox7" runat="server" ClientIDMode="Static" Text="业务">业务</asp:TextBox></li>
                                <li>
                                    <asp:TextBox ID="TextBox8" runat="server" ClientIDMode="Static" Text="计调">计调</asp:TextBox></li>
                                <li>
                                    <asp:TextBox ID="TextBox9" runat="server" ClientIDMode="Static" Text="财务">财务</asp:TextBox></li>

                            </ul>
                            <div class="btn_01">
                                <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                            </div>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <a href="javascript:;" class="easyui-linkbutton" id="pre_01" onclick="selectTag('main_01',this);">上一步</a>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            <a href="javascript:;" class="easyui-linkbutton" id="next_02">下一步</a></td>
                    </tr>
                </table> 
            </div>

            <div id="main_03" style="display: none;">
                <table class="tblEdit" style="background-color: none;">
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <div class="init_title">
                                参数设置
                            </div>
                        </td>
                    </tr>
                    <tbody id="param">
                        <%--线路类型--%>
                        <tr>
                            <td class="rowlabel">线路类型:
                                <input type="hidden" value="4" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox5" runat="server" ClientIDMode="Static" Text="周边短线">周边短线</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox14" runat="server" ClientIDMode="Static" Text="国内长线">国内长线</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox15" runat="server" ClientIDMode="Static" Text="出境线路">出境线路</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">收款类型:
                                <input type="hidden" value="6" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox10" runat="server" ClientIDMode="Static" Text="现金">现金</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox23" runat="server" ClientIDMode="Static" Text="转账">转账</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox24" runat="server" ClientIDMode="Static" Text="支付宝">支付宝</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">订单来源:
                                <input type="hidden" value="7" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox19" runat="server" ClientIDMode="Static" Text="平面广告">平面广告</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox20" runat="server" ClientIDMode="Static" Text="百度推广">百度推广</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox21" runat="server" ClientIDMode="Static" Text="老客户">老客户</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox22" runat="server" ClientIDMode="Static" Text="门户网站">门户网站</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">开票项目:
                                <input type="hidden" value="8" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox4" runat="server" ClientIDMode="Static" Text="旅游服务费">旅游服务费</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox11" runat="server" ClientIDMode="Static" Text="会议服务费">会议服务费</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox12" runat="server" ClientIDMode="Static" Text="综合服务费">综合服务费</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">发票领取方式:
                                <input type="hidden" value="9" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox26" runat="server" ClientIDMode="Static" Text="客户自取">客户自取</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox27" runat="server" ClientIDMode="Static" Text="代取">代取</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox28" runat="server" ClientIDMode="Static" Text="寄送">寄送</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">导游领款方式:
                                <input type="hidden" value="13" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox13" runat="server" ClientIDMode="Static" Text="现金">现金</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox16" runat="server" ClientIDMode="Static" Text="转账">转账</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">单项业务类型:
                                <input type="hidden" value="14" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox31" runat="server" ClientIDMode="Static" Text="机票">机票</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox32" runat="server" ClientIDMode="Static" Text="签证">签证</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox33" runat="server" ClientIDMode="Static" Text="单订房">单订房</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox36" runat="server" ClientIDMode="Static" Text="单订车">单订车</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">非订单收入类型:
                                <input type="hidden" value="15" />
                            </td>
                            <td class="datainput">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox17" runat="server" ClientIDMode="Static" Text="返佣">返佣</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox18" runat="server" ClientIDMode="Static" Text="开票">开票</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowlabel">非订单支出类型:
                                <input type="hidden" value="16" />
                            </td>
                            <td class="datainput" colspan="3">
                                <ul class="list_01 list_02">
                                    <li>
                                        <asp:TextBox ID="TextBox37" runat="server" ClientIDMode="Static" Text="办公">办公</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox38" runat="server" ClientIDMode="Static" Text="广告">广告</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox39" runat="server" ClientIDMode="Static" Text="管理">管理</asp:TextBox></li>
                                    <li>
                                        <asp:TextBox ID="TextBox40" runat="server" ClientIDMode="Static" Text="物业">物业</asp:TextBox></li>
                                </ul>
                                <div class="btn_01">
                                    <a href="javascript:;" class="easyui-linkbutton" onclick="addTag(this);">新&nbsp;&nbsp;&nbsp;&nbsp;增</a>
                                    <%--                                <a href="javascript:;" class="easyui-linkbutton" onclick="">删&nbsp;&nbsp;&nbsp;&nbsp;除</a>--%>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <a href="javascript:;" class="easyui-linkbutton pre_02" id="pre_02" onclick="selectTag('main_02',this);">上一步</a>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                            <a href="javascript:;" class="easyui-linkbutton" id="btnSave">下一步</a></td>
                    </tr>
                </table> 
            </div>
        </div>
    </form>
</body>
</html>
