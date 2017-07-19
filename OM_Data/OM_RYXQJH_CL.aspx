<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_RYXQJH_CL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_RYXQJH_CL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    人员需求计划
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style type="text/css">
        .tab
        {
            width: 70%;
            border-right: solid 1px #E5E5E5;
            border-bottom: solid 1px #E5E5E5;
        }
        .tab tr
        {
        }
        .tab tr td
        {
            border-left: solid 1px #E5E5E5;
            border-top: solid 1px #E5E5E5;
            text-align: center;
            font-size: larger;
        }
        .tab tr td input
        {
            font-size: medium;
        }
        .tab tr td input[type="text"]
        {
            width: 90%;
            height: 25px;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblCLZT" RepeatDirection="Horizontal" AutoPostBack="true"
                                OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="待处理" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已处理" Value="2"></asp:ListItem>
                                <asp:ListItem Text="不需处理" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <a runat="server" id="btnChuLi" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                onserverclick="btnChuLi_onserverclick">处理</a> <a runat="server" id="btnDelete" href="#"
                                    class="easyui-linkbutton" data-options="iconCls:'icon-add'" onserverclick="btnDelete_onserverclick">
                                    不需处理</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater runat="server" ID="rptZPJH" OnItemDataBound="rptZPJH_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td rowspan="2">
                                        <strong>序号</strong>
                                    </td>
                                    <td rowspan="2">
                                        <strong>单号</strong>
                                    </td>
                                    <td rowspan="2">
                                        <strong>部门</strong>
                                    </td>
                                    <td colspan="4">
                                        <strong>需求计划</strong>
                                    </td>
                                    <td>
                                        <strong>岗位描述</strong>
                                    </td>
                                    <td colspan="7">
                                        <strong>任职要求</strong>
                                    </td>
                                    <td rowspan="2">
                                        <strong>希望到岗时间</strong>
                                    </td>
                                    <td rowspan="2">
                                        <strong>拟工作地点</strong>
                                    </td>
                                    <td rowspan="2">
                                        <strong>其他</strong>
                                    </td>
                                </tr>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td>
                                        <strong>岗位名称</strong>
                                    </td>
                                    <td>
                                        <strong>需求来源<br />
                                            （校园/招聘）</strong>
                                    </td>
                                    <td>
                                        <strong>招聘方式</strong>
                                    </td>
                                    <td>
                                        <strong>需求人数</strong>
                                    </td>
                                    <td>
                                        <strong>岗位名称及工作职责</strong>
                                    </td>
                                    <td>
                                        <strong>所学专业</strong>
                                    </td>
                                    <td>
                                        <strong>毕业院校要求</strong>
                                    </td>
                                    <td>
                                        <strong>学历</strong>
                                    </td>
                                    <td>
                                        <strong>性别</strong>
                                    </td>
                                    <td>
                                        <strong>年龄</strong>
                                    </td>
                                    <td>
                                        <strong>资格能力要求<br />
                                            （经验、资历、工作技能等）</strong>
                                    </td>
                                    <td>
                                        <strong>其他要求</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr id="trr" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                    title="双击查看详细信息">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" CssClass="checkBoxCss" />
                                        <asp:Label runat="server" ID="lbXuHao" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="JH_ID" Value='<%#Eval("JH_ID")%>' />
                                        <%--<asp:HiddenField runat="server" ID="JH_HZSJ" Value='<%#Eval("JH_HZSJ")%>' />--%>
                                        <input type="hidden" runat="server" id="JH_HZSJ" name="JH_HZSJ" value='<%#Eval("JH_HZSJ")%>' />
                                        <asp:HiddenField runat="server" ID="JH_HZBH" Value='<%#Eval("JH_HZBH")%>' />
                                    </td>
                                    <td id="tdJH_HZBH" runat="server">
                                        <%#Eval("JH_HZBH")%>
                                    </td>
                                    <td id="tdJH_ZPBM" runat="server">
                                        <%#Eval("JH_ZPBM")%>
                                    </td>
                                    <td id="tdJH_GWMC" runat="server">
                                        <%#Eval("JH_GWMC")%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_XQLY" Text='<%#Eval("JH_XQLY")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPFS" Text='<%#Eval("JH_ZPFS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPRS" Text='<%#Eval("JH_ZPRS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPGW" Text='<%#Eval("JH_ZPGW")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPZY" Text='<%#Eval("JH_ZPZY")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPYX" Text='<%#Eval("JH_ZPYX")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPXL" Text='<%#Eval("JH_ZPXL")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPXB" Text='<%#Eval("JH_ZPXB")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPNL" Text='<%#Eval("JH_ZPNL")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPYQ" Text='<%#Eval("JH_ZPYQ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_QTYQ" Text='<%#Eval("JH_QTYQ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_XWDGSJ" Text='<%#Eval("JH_XWDGSJ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_NGZDD" Text='<%#Eval("JH_NGZDD")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_QT" Text='<%#Eval("JH_QT")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                    没有记录!<br />
                    <br />
                </asp:Panel>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
