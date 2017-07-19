<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_JDPXJH_SS.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_JDPXJH_SS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    季度培训计划实施
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

    <script type="text/javascript">
        function cbxRY_onchange(obj) {
            var num = 0;
            $("#<%=cbxRY.ClientID%> input:checkbox:checked").each(function() {
                num++;
            })
            $("#<%=txtPX_SJRS.ClientID%>").val(num);
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title" align="right">
                <a runat="server" id="btnSave" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                    onserverclick="btnSave_onserverclick">保存</a> <a runat="server" id="btnBack" href="#"
                        class="easyui-linkbutton" data-options="iconCls:'icon-add'" onserverclick="btnBack_onserverclick">
                        返回</a>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div style="width: 100%; background-color: #F0F8FF" align="center" id="div1">
                <asp:Panel runat="server" ID="panJBXX">
                    <table width="70%">
                        <tr>
                            <td align="center">
                                <asp:Image runat="server" ID="Image0" ImageUrl="~/Assets/images/OM_ZCTOP.jpg" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-size: x-large;">
                                季度培训计划实施表
                            </td>
                        </tr>
                    </table>
                    <table width="70%" border="2px" cellpadding="0">
                        <tr>
                            <td width="25%" height="25px">
                                实际培训时间
                            </td>
                            <td width="25%">
                                <asp:TextBox runat="server" ID="txtPX_SJSJ" class="easyui-datebox" onfocus="this.blur()"></asp:TextBox>
                            </td>
                            <td width="25%">
                                实际培训地点
                            </td>
                            <td width="25%">
                                <asp:TextBox runat="server" ID="txtPX_SJDD"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%" height="25px">
                                实际学时
                            </td>
                            <td width="25%">
                                <asp:TextBox runat="server" ID="txtPX_SJXS"></asp:TextBox>
                            </td>
                            <td width="25%">
                                培训项目实施编号
                            </td>
                            <td width="25%">
                                <asp:TextBox runat="server" ID="txtPX_BH" onfocus="this.blur()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                实际参训人员：<br />
                                部门：<asp:CheckBoxList runat="server" ID="cbxBM" CellSpacing="15" TextAlign="Right"
                                    AutoPostBack="true" OnSelectedIndexChanged="cbxBM_OnSelectedIndexChanged" RepeatColumns="5"
                                    RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                                人员：<asp:CheckBoxList runat="server" ID="cbxRY" onchange="cbxRY_onchange(this)" CellSpacing="10"
                                    TextAlign="Right" RepeatColumns="10" RepeatDirection="Horizontal">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                实际参训人数
                            </td>
                            <td width="25%">
                                <asp:TextBox runat="server" ID="txtPX_SJRS" onfocus="this.blur()"></asp:TextBox>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                备注：<br />
                                <asp:TextBox runat="server" ID="txtPX_SJBZ" TextMode="MultiLine" Rows="3" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="tab">
                        <tr>
                            <td width="25%" height="25px">
                                制单人
                            </td>
                            <td width="25%">
                                <asp:Label runat="server" ID="lbPX_SSZDR"></asp:Label>
                                <asp:HiddenField runat="server" ID="hidPX_SSZDRID" />
                            </td>
                            <td width="25%">
                                制单时间
                            </td>
                            <td width="25%">
                                <asp:Label runat="server" ID="lbPX_SSZDSJ"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cbxBM" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
