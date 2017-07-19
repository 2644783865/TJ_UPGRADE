<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_PXDA.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_PXDA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    员工培训档案
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
        #rdlstSeq td
        {
            border: 0;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="../PC_Data/FixTable.css" rel="stylesheet" type="text/css" />
    <link href="SM_JS/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="SM_JS/rowcolor.js" type="text/javascript"></script>

    <script src="SM_JS/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script src="../JS/OM.js" type="text/javascript"></script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnDaoChu" href="#" class="easyui-linkbutton" onserverclick="btnDaoChu_onserverclick"
                    data-options="iconCls:'icon-add'">导出培训档案</a> &nbsp;&nbsp;&nbsp;&nbsp; <a runat="server"
                        id="btnSave" href="#" onserverclick="btnSave_onserverclick" class="easyui-linkbutton"
                        data-options="iconCls:'icon-add'">保存分数</a>
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
                                    部门:<asp:DropDownList runat="server" ID="ddlBM" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    姓名：<asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                                    项目编号：<asp:TextBox runat="server" ID="txtXMBH"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnQuery" OnClick="Query" Text="查询" Width="30px" BackColor="LightGreen" />
                                </td>
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
                                            <td>
                                                <strong>序号</strong>
                                            </td>
                                            <td>
                                                <strong>姓名</strong>
                                            </td>
                                            <td>
                                                <strong>部门</strong>
                                            </td>
                                            <td>
                                                <strong>项目编号</strong>
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
                                                <strong>培训效果<br />
                                                    （满分100分）</strong>
                                            </td>
                                            <td>
                                                <strong>学时</strong>
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
                                                <asp:HiddenField runat="server" ID="DA_ID" Value='<%#Eval("DA_ID")%>' />
                                                <asp:HiddenField runat="server" ID="PX_ID" Value='<%#Eval("PX_ID")%>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="DA_CXR" Text='<%#Eval("DA_CXR")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <%#Eval("DEP_NAME")%>
                                            </td>
                                            <td>
                                                <%#Eval("DA_XMBH")%>
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
                                                <%#Eval("PX_SJSJ")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_SJDD")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_ZJR")%>
                                            </td>
                                            <td width="120px">
                                                <asp:TextBox runat="server" ID="DA_ZJRDF" onblur="zsxsyz(this)" Text='<%#Eval("DA_ZJRDF")%>'
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <%#Eval("PX_SJXS")%>
                                            </td>
                                            <td>
                                                <%#Eval("PX_SJBZ")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <tr>
                                            <td colspan="13" align="center">
                                                <strong>培训总人数：</strong>
                                                <asp:Label runat="server" ID="RSHJ" ForeColor="Red"></asp:Label>
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <strong>培训总学时：</strong>
                                                <asp:Label runat="server" ID="XSHJ" ForeColor="Red"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
