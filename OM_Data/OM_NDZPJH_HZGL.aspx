<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_NDZPJH_HZGL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_NDZPJH_HZGL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    年度招聘计划汇总
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var sjid = $(this).find("input[name*=JH_HZSJ]").val();
                window.open("OM_RYXQJH_HZ.aspx?action=read&sjid=" + sjid);
            });
        })
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnHZ" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onserverclick="btnHZ_onserverclick">招聘汇总</a>
            </div>
        </div>
    </div>
    <div>
        <table width="100%">
            <tr>
                <%--<td>
                    <asp:RadioButtonList runat="server" ID="rblRW" RepeatDirection="Horizontal" RepeatColumns="2"
                        AutoPostBack="true" OnSelectedIndexChanged="Query">
                        <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                        <asp:ListItem Text="我的任务" Value="1" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>--%>
                <td>
                    <asp:RadioButtonList runat="server" ID="rblSPZT" RepeatDirection="Horizontal" RepeatColumns="6"
                        AutoPostBack="true" OnSelectedIndexChanged="Query">
                        <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                        <asp:ListItem Text="初始化" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="未审批" Value="2"></asp:ListItem>
                        <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
                        <asp:ListItem Text="已通过" Value="4"></asp:ListItem>
                        <asp:ListItem Text="已驳回" Value="5"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    部门：<asp:TextBox runat="server" ID="txtBM"></asp:TextBox>
                </td>
                <td>
                    岗位：<asp:TextBox runat="server" ID="txtGW"></asp:TextBox>
                    <asp:Button runat="server" ID="btnQuery" Text="查询" OnClick="Query" />
                </td>
                <td>
                </td>
            </tr>
        </table>
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
                                        <strong>编号</strong>
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
                                    <td rowspan="2">
                                        <strong>修改</strong>
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
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息"
                                    ondblclick='trClick(<%#Eval("JH_HZSJ") %>)'>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" CssClass="checkBoxCss" />
                                        <asp:Label runat="server" ID="lbXuHao" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="JH_ID" Value='<%#Eval("JH_ID")%>' />
                                        <asp:HiddenField runat="server" ID="JH_SFHZ" Value='<%#Eval("JH_SFHZ") %>' />
                                        <asp:HiddenField runat="server" ID="JH_SFTJ" Value='<%#Eval("JH_SFTJ") %>' />
                                        <input type="hidden" runat="server" id="JH_HZSJ" name="JH_HZSJ" value='<%#Eval("JH_HZSJ")%>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_HZBH" Text='<%#Eval("JH_HZBH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_ZPBM" Text='<%#Eval("JH_ZPBM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="JH_GWMC" Text='<%#Eval("JH_GWMC")%>'></asp:Label>
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
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplAlter" NavigateUrl='<%#"OM_RYXQJH_HZ.aspx?action=alter&sjid="+Eval("JH_HZSJ")%>'>
                                            <asp:Image runat="server" ID="Image1" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            修改
                                        </asp:HyperLink>
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
