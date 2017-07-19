<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_PXBM_GL.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_PXBM_GL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    招聘报名管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script src="../JS/OM.js" type="text/javascript"></script>

    <script type="text/javascript">
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnAdd" href="OM_PXBM_SQ.aspx?action=add" class="easyui-linkbutton"
                    data-options="iconCls:'icon-add'">培训报名</a>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table>
                            <tr>
                                <td>
                                    部门:<asp:DropDownList runat="server" ID="ddlBM" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    姓名：<asp:TextBox runat="server" ID="txtBMR"></asp:TextBox>&nbsp;&nbsp; 项目名称：<asp:TextBox
                                        runat="server" ID="txtXMMC"></asp:TextBox>&nbsp;&nbsp; 主讲人：<asp:TextBox runat="server"
                                            ID="txtZJR"></asp:TextBox>&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnQuery" Text="查询" OnClick="Query" />
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
                                <asp:Repeater runat="server" ID="rptPXBM" OnItemDataBound="rptPXBM_OnItemDataBound">
                                    <HeaderTemplate>
                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                            <td>
                                                <strong>序号</strong>
                                            </td>
                                            <td>
                                                <strong>报名人</strong>
                                            </td>
                                            <td>
                                                <strong>申报部门</strong>
                                            </td>
                                            <td>
                                                <strong>报名时间</strong>
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
                                                <strong>制单人</strong>
                                            </td>
                                            <%--<td>
                                        <strong>修改</strong>
                                    </td>--%>
                                            <td>
                                                <strong>删除</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                            <td>
                                                <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%#Eval("ID_Num")%>' TextAlign="Right"
                                                    CssClass="checkBoxCss" />
                                                <asp:HiddenField runat="server" ID="BM_ID" Value='<%#Eval("BM_ID")%>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="BM_BMR" Text='<%#Eval("BM_BMR")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <%#Eval("BM_BM")%>
                                            </td>
                                            <td>
                                                <%#Eval("BM_TXSJ")%>
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
                                                <asp:Label runat="server" ID="BM_TXR" Text='<%#Eval("BM_TXR")%>'></asp:Label>
                                            </td>
                                            <%--<td>
                                        <asp:HyperLink runat="server" ID="hplAlter" NavigateUrl='<%#"OM_NDPXJH_SQ.aspx?action=alter&id="+Eval("PX_ID")%>'>
                                            <asp:Image runat="server" ID="Image1" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            修改
                                        </asp:HyperLink>
                                    </td>--%>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除" OnClientClick="return confirm('确认删除该数据？')"
                                                    CommandArgument='<%#Eval("BM_ID")%>' OnClick="lbtnDelete_OnClick">
                                                    <asp:Image runat="server" ID="imgDelete" Width="20px" Height="20px" ImageAlign="AbsMiddle"
                                                        ImageUrl="~/Assets/images/erase.gif" />
                                                </asp:LinkButton>
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
