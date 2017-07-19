<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_PXBM_SQ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_PXBM_SQ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
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
        function trClick(a) {
            window.open("OM_NDPXJH_SQ.aspx?action=read&id=" + a);
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnSubmit" href="#" class="easyui-linkbutton" onserverclick="btnSubmit_onserverclick"
                    data-options="iconCls:'icon-add'" visible="false">提交</a> <a runat="server" id="btnSave"
                        href="#" class="easyui-linkbutton" onserverclick="btnSave_onserverclick" data-options="iconCls:'icon-add'">
                        保存</a>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
                <asp:Panel runat="server" ID="panJBXX">
                    <table width="70%" border="2px">
                        <tr>
                            <td>
                                填写人：&nbsp;&nbsp;<asp:Label runat="server" ID="lbBM_TXR"></asp:Label>
                                <asp:HiddenField runat="server" ID="hidBM_TXRID" />
                            </td>
                            <td>
                                报名时间：&nbsp;&nbsp;<asp:Label runat="server" ID="lbBM_SJ"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                选择部门：
                                <asp:DropDownList runat="server" ID="ddlBM1" AutoPostBack="true" OnSelectedIndexChanged="ddlBM1_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                勾选报名人员：<br />
                                <asp:CheckBoxList runat="server" ID="cbxRY" RepeatColumns="10" CellSpacing="5" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
                                                <strong>培训方式</strong>
                                            </td>
                                            <td>
                                                <strong>申报部门</strong>
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
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息"
                                            ondblclick='trClick(<%#Eval("PX_ID") %>)'>
                                            <td>
                                                <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%#Convert.ToInt32(Container.ItemIndex +1)%>'
                                                    TextAlign="Right" CssClass="checkBoxCss" />
                                                <asp:HiddenField runat="server" ID="PX_ID" Value='<%#Eval("PX_ID")%>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="PX_FS" Text='<%#Eval("PX_FS").ToString()=="n"?"内":"外"%>'></asp:Label>
                                            </td>
                                            <td>
                                                <%#Eval("PX_BM")%>
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
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:Panel ID="palNoData" runat="server" Visible="false" ForeColor="Red" HorizontalAlign="Center">
                                没有记录!<br />
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlBM1" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
