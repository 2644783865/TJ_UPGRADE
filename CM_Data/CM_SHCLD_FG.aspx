<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_SHCLD_FG.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SHCLD_FG" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    售后质量问题处理单分工
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[id*=CLD_ID]").val();
                window.open("CM_SHCLD.aspx?action=read&id=" + id);
            });

            $("#tab tr:not(:first)").click(function() {
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        });

        function xr1() {
            $("#hidPerson").val("person1");
            SelPersons();
        }
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "person1") {

            }
            $('#win').dialog('close');
        }

    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblRW" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="原因分析待分工" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="处理意见待分工" Value="2"></asp:ListItem>
                                <asp:ListItem Text="处理方案待分工" Value="3"></asp:ListItem>
                                <asp:ListItem Text="处理结果待分工" Value="4"></asp:ListItem>
                                <asp:ListItem Text="服务费用待分工" Value="5"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <a runat="server" id="btnFG" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-edit'">
                                分工</a>
                            <asp:ModalPopupExtender runat="server" ID="mpeFG" X="988" Y="80"  TargetControlID="btnFG" PopupControlID="panFG"
                                CancelControlID="close">
                            </asp:ModalPopupExtender>
                            <asp:Panel runat="server" Width="150px" Height="60px" ID="panFG" Style="display: none;
                                border-style: solid; border-width: 1px; border-color: blue; background-color: Menu;">
                                <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                    font-size: 11px; font-weight: bold; position: absolute; top: 5px; right: 10px;">
                                    <a id="close" style="background-color: #6699CC; cursor: pointer; color: #FFFFFF;
                                        text-align: center; text-decoration: none; padding: 5px;" title="关闭">X</a>
                                </div>
                                <table style="width: 100%; background-color: White;">
                                    <tr>
                                        <td>
                                            人员：<asp:DropDownList runat="server" ID="ddlName">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnConfirm" runat="server" Text="确 定" OnClick="btnConfirm_OnClick" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
                        <asp:Repeater runat="server" ID="rptSHFWCLD" OnItemDataBound="rptSHFWCLD_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <th>
                                        <strong>序号</strong>
                                    </th>
                                    <th>
                                        <strong>顾客名称</strong>
                                    </th>
                                    <th>
                                        <strong>编号</strong>
                                    </th>
                                    <th>
                                        <strong>合同号</strong>
                                    </th>
                                    <th>
                                        <strong>任务号</strong>
                                    </th>
                                    <th>
                                        <strong>项目名称</strong>
                                    </th>
                                    <th>
                                        <strong>设备名称</strong>
                                    </th>
                                    <th>
                                        <strong>信息简介</strong>
                                    </th>
                                    <th>
                                        <strong>处理结果</strong>
                                    </th>
                                    <th>
                                        <strong>服务总费用</strong>
                                    </th>
                                    <th>
                                        <strong>制单人</strong>
                                    </th>
                                    <th>
                                        <strong>制单日期</strong>
                                    </th>
                                    <th>
                                        <strong>负责部门</strong>
                                    </th>
                                    <th>
                                        <strong>审批状态</strong>
                                    </th>
                                    <th>
                                        <strong>处理状态</strong>
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%#Eval("ID_Num")%>' />
                                        <asp:HiddenField runat="server" ID="CLD_ID" Value='<%#Eval("CLD_ID") %>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_GKMC" Text='<%#Eval("CLD_GKMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_BH" Text='<%#Eval("CLD_BH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_HTH" Text='<%#Eval("CLD_HTH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_RWH" Text='<%#Eval("CLD_RWH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_XMMC" Text='<%#Eval("CLD_XMMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_SBMC" Text='<%#Eval("CLD_SBMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="CLD_XXJJ" Text='<%#Eval("CLD_XXJJ")%>' ToolTip='<%#Eval("CLD_XXJJ")%>'
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="CLD_FWGC" Text='<%#Eval("CLD_CLJG") %>' ToolTip='<%#Eval("CLD_CLJG") %>'
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_FWZFY" Text='<%#Eval("CLD_FWZFY")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_ZDR" Text='<%#Eval("CLD_ZDR")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_ZDSJ" Text='<%#Eval("CLD_ZDSJ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_FZBM" Text='<%#Eval("CLD_FZBM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_SPZT1" Width="100%" Height="100%" Text=""></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_CLZT" Width="100%" Height="100%" Text=""></asp:Label>
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
    <div id="win" visible="false">
        <div>
            <table>
                <tr>
                    <td>
                        <a href="../PC_Data/PC_TBPC_Purchaseplan_assign_list.aspx"></a><strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        按部门查询：
                    </td>
                    <td>
                        <input id="dep" name="dept" value="" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 430px; height: 230px">
            <table id="dg">
            </table>
        </div>
    </div>
    <div id="buttons" style="text-align: right" visible="false">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
            保存</a> &nbsp;&nbsp;&nbsp; <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a> &nbsp;&nbsp;&nbsp;
        <input id="hidPerson" type="hidden" value="" />
    </div>
</asp:Content>
