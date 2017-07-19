<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_JDPXJH.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_JDPXJH" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    季度培训计划
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
            $("#tab tr").click(function() {
                //   console.log($(this).attr("class"));
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });

            $("#tab tr:not(:first)").dblclick(function() {
            window.open("OM_PXJH_MX.aspx?action=read&pxbh=" + $(this).find("[id*='PX_BH']").val());
            })
        })
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnDaoChu" href="#" class="easyui-linkbutton" onserverclick="btnDaoChu_onserverclick"
                    data-options="iconCls:'icon-add'">导出培训</a> &nbsp;&nbsp;&nbsp;&nbsp;<a runat="server"
                        id="btnAdd" href="#" class="easyui-linkbutton" onserverclick="btnAdd_onserverclick"
                        data-options="iconCls:'icon-add'"> 实施培训</a>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <table width="100%">
                    <tr>
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
                        <td style="width: 30%;">
                            <strong>培训实际时间：</strong>
                            <asp:DropDownList ID="dplYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Query">
                            </asp:DropDownList>
                            &nbsp;年&nbsp;
                            <asp:DropDownList ID="dplMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Query">
                            </asp:DropDownList>
                            &nbsp;月&nbsp;
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
                                    <td colspan="13">
                                        <strong>培训计划</strong>
                                    </td>
                                    <td colspan="6">
                                        <strong>培训实施</strong>
                                    </td>
                                    <td rowspan="2">
                                        <strong>修改实施</strong>
                                    </td>
                                    <td rowspan="2">
                                        <strong>编辑培训<br />
                                            详细信息</strong>
                                    </td>
                                </tr>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>申报部门</strong>
                                    </td>
                                    <td>
                                        <strong>实施编号</strong>
                                    </td>
                                    <td>
                                        <strong>计划类型</strong>
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
                                        <strong>培训费<br />
                                            预算</strong>
                                    </td>
                                    <td>
                                        <strong>实际培<br />
                                            训时间</strong>
                                    </td>
                                    <td>
                                        <strong>实际培<br />
                                            训地点</strong>
                                    </td>
                                    <td>
                                        <strong>实际参<br />
                                            训人员</strong>
                                    </td>
                                    <td>
                                        <strong>培训人数</strong>
                                    </td>
                                    <td>
                                        <strong>实际学时</strong>
                                    </td>
                                    <td>
                                        <strong>备注</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%#Eval("ID_Num")%>' TextAlign="Right"
                                            CssClass="checkBoxCss" />
                                        <asp:HiddenField runat="server" ID="PX_ID" Value='<%#Eval("PX_ID")%>' />
                                        <asp:HiddenField runat="server" ID="PX_BH" Value='<%#Eval("PX_BH")%>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="PX_BM" Text='<%#Eval("PX_BM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("PX_BH")%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="SPLX" Text='<%#Eval("SPLX").ToString()=="NDPXJH"?"年度":"临时"%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="PX_FS" Text='<%#Eval("PX_FS").ToString()=="n"?"内":"外"%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("PX_XMMC")%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="PX_SJ" Text='<%#Eval("PX_SJ").ToString()=="1"?"第一季度":Eval("PX_SJ").ToString()=="2"?"第二季度":Eval("PX_SJ").ToString()=="3"?"第三季度":"第四季度"%>'></asp:Label>
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
                                        <%#Eval("PX_SJSJ")%>
                                    </td>
                                    <td>
                                        <%#Eval("PX_SJDD")%>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="PX_SJRY" Text='<%#Eval("PX_SJRY")%>' ToolTip='<%#Eval("PX_SJRY")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <%#Eval("PX_SJRS")%>
                                    </td>
                                    <td>
                                        <%#Eval("PX_SJXS")%>
                                    </td>
                                    <td>
                                        <%#Eval("PX_SJBZ")%>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplAlter" NavigateUrl='<%#"OM_JDPXJH_SS.aspx?action=alter&id="+Eval("PX_ID")%>'>
                                            <asp:Image runat="server" ID="Image1" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            修改
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplEdit" NavigateUrl='<%#"OM_PXJH_MX.aspx?action=edit&pxbh="+Eval("PX_BH")%>'>
                                            <asp:Image runat="server" ID="Image2" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            编辑
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="16" align="center">
                                        <strong>学时合计</strong>
                                    </td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="RSHJ"></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label runat="server" ID="XSHJ"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                    </td>
                                </tr>
                            </FooterTemplate>
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
</asp:Content>
