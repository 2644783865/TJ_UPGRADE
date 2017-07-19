<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_NDPXJJH_HZSP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_NDPXJJH_HZSP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    年度培训计划汇总审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var sjid = $(this).find("input[name*=PX_SJID1]").val();
                window.open("OM_NDPXJH_ZSP.aspx?action=read&id=" + sjid);
            });
        })
        function aa() {
            $("#tab tr:not(:first)").dblclick(function() {
                var sjid = $(this).find("input[name*=PX_SJID1]").val();
                window.open("OM_NDPXJH_ZSP.aspx?action=read&id=" + sjid);
            });
        }
        function btnHZSP_onclick() {
            if (confirm("汇总后将提交给总经理审批，您确定相关部门都已提交了本部门年度培训计划吗？")) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnHZSP" href="#" onclick="return btnHZSP_onclick() " onserverclick="btnHZSP_onserverclick"
                    class="easyui-linkbutton" data-options="iconCls:'icon-add'">年度培训计划汇总</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="btnExport" runat="server" Text="导 出" OnClick="btnExport_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btndelete" runat="server" Text="删 除" OnClick="btndelete_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title" align="right">
                        <table width="100%">
                            <tr>
                                <td>
                                    年份<asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="rblRW" RepeatDirection="Horizontal" AutoPostBack="true"
                                        OnSelectedIndexChanged="Query">
                                        <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="我的任务" Value="1" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    审批状态：<asp:DropDownList runat="server" ID="ddlSPZT" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                        <asp:ListItem Text="-全部-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="初始化" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="未审批" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="已通过" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="未通过" Value="5"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    部门:<asp:DropDownList runat="server" ID="ddlBM" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    培训时间：<asp:DropDownList runat="server" ID="ddlSJ" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                        <asp:ListItem Text="-全部-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="第一季度" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="第二季度" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="第三季度" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="第四季度" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
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
                                <asp:Repeater runat="server" ID="rptPXJH" OnItemDataBound="rptPXJH_OnItemDataBound">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                            <td>
                                                <strong>序号</strong>
                                            </td>
                                            <td>
                                                <strong>申报部门</strong>
                                            </td>
                                            <td>
                                                <strong>培训方式</strong>
                                            </td>
                                            <td>
                                                <strong>项目名称</strong>
                                            </td>
                                            <td>
                                                <strong>培训时间</strong>
                                            </td>
                                            <td>
                                                <strong>培训地点</strong>
                                            </td>
                                            <td>
                                                <strong>主讲人</strong>
                                            </td>
                                            <td>
                                                <strong>培训对象</strong>
                                            </td>
                                            <td>
                                                <strong>培训人数</strong>
                                            </td>
                                            <td>
                                                <strong>学时</strong>
                                            </td>
                                            <td>
                                                <strong>培训费预算</strong>
                                            </td>
                                            <td>
                                                <strong>备注</strong>
                                            </td>
                                            <td>
                                                <strong>审批状态</strong>
                                            </td>
                                            <td>
                                                <strong>审批</strong>
                                            </td>
                                            <td>
                                                <strong>修改</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                            <td>
                                                <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%#Eval("ID_Num")%>' TextAlign="Right"
                                                    CssClass="checkBoxCss" />
                                                <asp:HiddenField runat="server" ID="PX_ID" Value='<%#Eval("PX_ID")%>' />
                                                <input type="hidden" runat="server" id="PX_SJID1" name="PX_SJID1" value='<%#Eval("PX_SJID1")%>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="PX_BM" Text='<%#Eval("PX_BM")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="PX_FS" Text='<%#Eval("PX_FS").ToString()=="n"?"内":"外"%>'></asp:Label>
                                            </td>
                                            <td>
                                                <%#Eval("PX_XMMC")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_SJ")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_DD")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_ZJR")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_DX")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_RS")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_XS")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_FYYS")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_BZ")%>
                                            </td>
                                            <td>
                                                <%#Eval("SPZT").ToString() == "0" ? "初始化" : Eval("SPZT").ToString() == "1" ? "未审批" : Eval("SPZT").ToString() == "y" ? "已通过" : Eval("SPZT").ToString() == "n" ? "未通过" : "审批中"%>
                                            </td>
                                            <td>
                                                <asp:HyperLink runat="server" ID="hplCheck" NavigateUrl='<%#"OM_NDPXJH_ZSP.aspx?action=check&id="+Eval("PX_SJID1")%>'>
                                                    <asp:Image runat="server" ID="Image2" Width="20px" Height="20px" border="0" hspace="2"
                                                        ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                                    审批
                                                </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:HyperLink runat="server" ID="hplAlter" NavigateUrl='<%#"OM_NDPXJH_ZSP.aspx?action=alter&id="+Eval("PX_SJID1")%>'>
                                                    <asp:Image runat="server" ID="Image1" Width="20px" Height="20px" border="0" hspace="2"
                                                        ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                                    修改
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                                没有记录!<br />
                                <br />
                            </asp:Panel>
                        </div>
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
