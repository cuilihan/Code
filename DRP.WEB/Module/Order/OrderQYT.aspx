<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderQYT.aspx.cs" Inherits="DRP.WEB.Module.Order.OrderQYT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>企业团订单管理</title>
    <link href="../../Scripts/Plugin/lhgdialog/skins/default.css" rel="stylesheet" />
    <script src="../../Scripts/Plugin/lhgdialog/lhgdialog.min.js?skin=iblue" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/date_time/WdatePicker.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/Uploadify/jquery.uploadify-3.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/Plugin/Uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="Script/OrderUtility.js" type="text/javascript"></script>
    <script src="../../Scripts/Plugin/kindeditor/kindeditor-min.js"></script>
    <script src="Script/OrderQYT.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="iBread iBreadFive" style="margin: 3px">
            <ul class="iCls">
                <li class="on">
                    <strong class="one">1</strong>
                    填写订单信息 
                    <i></i>
                </li>
                <li>
                    <strong class="two">2</strong>
                    订单预算 
                    <i></i><em></em>
                </li>
                <li>
                    <strong class="three">3</strong>
                    导游报账            
                     <i></i><em></em>
                </li>
                <li>
                    <strong class="four">4</strong>
                    订单决算               
                     <i></i><em></em>
                </li>
                <li class="last">
                    <strong class="five">5</strong>
                    完成订单               
                     <em></em>
                </li>
            </ul>
        </div>
        <asp:Panel runat="server" ID="pnlContainer" Style="padding: 0px 5px 10px 5px;">

            <table class="tblInfo">
                <tr>
                    <td class="rowlabel"><span class="red">*</span>目的地：</td>
                    <td>
                        <asp:HiddenField ID="hfRouteTypeID" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hfDestinationID" ClientIDMode="Static" runat="server" />
                        <input class="easyui-combobox" id="RouteTypeID" style="height: 26px; width: 90px;" />
                        <input class="easyui-combotree" id="DestinationID" style="height: 26px; width: 120px;" />
                    </td>
                    <td class="rowlabel"><span class="red">*</span>订单名称：</td>
                    <td colspan="3">
                        <asp:TextBox ID="OrderName" runat="server" Width="80%" data-options="required:true" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>出团日期：</td>
                    <td>
                        <asp:TextBox ID="TourDate" onclick="WdatePicker()" ClientIDMode="Static" runat="server" data-options="required:true"></asp:TextBox>
                    </td>
                    <td class="rowlabel">行程天数：</td>
                    <td>
                        <asp:TextBox ID="TourDays" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">订单来源：</td>
                    <td>
                        <asp:DropDownList ID="SourceID" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Text="请选择"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">集合时间：</td>
                    <td>
                        <asp:TextBox ID="CollectTime" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                    <td class="rowlabel">上车地点：</td>
                    <td colspan="3">
                        <asp:TextBox ID="VenueName" Width="80%" ClientIDMode="Static" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel"><span class="red">*</span>客户信息：</td>
                    <td colspan="5" style="padding: 10px;">
                        <table class="tblEdit">
                            <tr>
                                <th style="width: 30px;"></th>
                                <th style="width: 100px;">姓名</th>
                                <th style="width: 60px;">性别</th>
                                <th style="width: 110px;">手机号</th>
                                <th style="width: 140px;">身份证号</th>
                                <th style="width: 150px;">公司名称</th>
                                <th style="width: 50px;">联系人</th>
                                <th>备注</th>
                                <th style="width: 40px;"></th>
                            </tr>
                            <tbody id="tblCustomer">
                                <asp:Repeater runat="server" ID="rptCustomer" OnItemDataBound="rptCustomer_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td style='text-align: center;'>
                                                <asp:Literal ID="lblNo" runat="server"></asp:Literal>
                                                <input type='hidden' value="<%# Eval("CustomerID") %>" />
                                            </td>
                                            <td>
                                                <input type='text' class='textbox' style='height: 26px; width: 90px;' value="<%# Eval("Name") %>" /></td>
                                            <td>
                                                <asp:Literal ID="lblSex" runat="server"></asp:Literal></td>
                                            <td>
                                                <input type='text' class='textbox' value="<%# Eval("Mobile") %>" style='height: 26px; width: 100px;' /></td>
                                            <td>
                                                <input type='text' class='textbox' value="<%# Eval("IDNo") %>" style='height: 26px; width: 130px;' /></td>
                                            <td>
                                                <input type='text' class='textbox' value="<%# Eval("Company") %>" style='height: 26px; width: 140px;' /></td>
                                            <td style="text-align: center;">
                                                <asp:Literal ID="lblIsLeader" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <input type='text' class='textbox' value="<%# Eval("Comment") %>" style='height: 26px; width: 98%;' /></td>
                                            <td><a href='javascript:;' onclick="comm.fnDeleteCustomer(this)">删除</a></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="padding: 10px 0px; text-align: right;">
                            <a href="javascript:;" id="btnSelectCustomer" class="easyui-linkbutton" iconcls="icon-ok">选择客户</a>
                            <a href="javascript:;" id="btnAddCustomer" class="easyui-linkbutton" iconcls="icon-add">增加客户</a>
                            <a href="javascript:;" id="btnAddManyCustomer" class="easyui-linkbutton" iconcls="icon-add">批量增加客户</a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">行程及标准：</td>
                    <td colspan="5" style="padding: 10px;">
                        <asp:TextBox ID="Schedule" ClientIDMode="Static" runat="server" TextMode="MultiLine" Width="100%" Height="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">订单备注：</td>
                    <td colspan="5" style="padding: 10px;">
                        <asp:TextBox ID="Remark" ClientIDMode="Static" Width="99%" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="Participant" style="display: none" runat="server">
                    <td class="rowlabel">参与人员：</td>
                    <td colspan="5">
                        <asp:HiddenField ID="hfDept" ClientIDMode="Static" runat="server" />
                        <asp:HiddenField ID="hfEmployee" ClientIDMode="Static" runat="server" />
                        <input class="easyui-combotree" id="Dept" style="height: 26px; width: 120px;" runat="server" />
                        <input class="easyui-combobox" id="Employee" style="height: 26px; width: 120px;" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="rowlabel">上传附件：
                    </td>
                    <td colspan="5" style="padding: 10px;">
                        <div>
                            <input type="file" name="uploadify" id="uploadify" runat="server" />
                        </div>
                        <table class="tblEdit" cellpadding="1" cellspacing="1">
                            <tr>
                                <th>文件名称
                                </th>
                                <th style="width: 90px;">文件类型
                                </th>
                                <th style="width: 90px;">文件大小
                                </th>
                                <th style="width: 120px;">上传日期
                                </th>
                                <th style="width: 80px;">上传人
                                </th>
                                <th style="width: 45px;"></th>
                            </tr>
                            <tbody id="tblFile">
                                <asp:Repeater runat="server" ID="rptFile">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input type="hidden" value='<%# Eval("ID") %>' />
                                                <a href='<%# Eval("FilePath") %>' target='_blank'>
                                                    <%# Eval("FileName") %></a>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("FileType") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("FileSize") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("CreateDate") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <%# Eval("CreateUserName") %>
                                            </td>
                                            <td style="text-align: center;">
                                                <a href="javascript:;" onclick="comm.fnDeleteFile(this)">删除</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: center; padding: 5px;">
                        <div id="btnOpt">
                            <asp:CheckBox ID="OrderStatus" Checked="true" Text="订单已确认" ClientIDMode="Static" runat="server" />
                            <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" id="btnSave">提交订单</a>
                            <a href="javascript:;" class="easyui-linkbutton" iconcls="icon-save" runat="server" id="btnSaveNext">提交订单并编辑预算</a>
                        </div>
                        <div style="color: Red;" id="tips" class="hide">
                            <img src="../../UI/themes/default/images/loading.gif" />
                            正在提交数据，请稍候... 
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
