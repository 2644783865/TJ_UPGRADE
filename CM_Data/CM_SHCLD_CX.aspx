<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_SHCLD_CX.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SHCLD_CX" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    售后质量问题处理单查询
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link href="FixTable.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=CLD_ID]").val();
                window.open("CM_SHCLD.aspx?action=read&id=" + id);
            });

            $("#tab tr:not(:first)").click(function() {
                $(this).removeAttr("style");
                $(this).toggleClass("techBackColor");
            });
        });
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            按审批状态：
                            <asp:DropDownList runat="server" ID="ddlSPZT" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未审批" Value="2"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="4"></asp:ListItem>
                                <asp:ListItem Text="未通过" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                            按处理状态：
                            <asp:DropDownList runat="server" ID="ddlCLZT" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未处理" Value="2"></asp:ListItem>
                                <asp:ListItem Text="处理中" Value="3"></asp:ListItem>
                                <asp:ListItem Text="已处理" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                            按是否填写：
                            <asp:DropDownList runat="server" ID="ddlTXLX" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="原因分析" Value="CLD_YYFX"></asp:ListItem>
                                <asp:ListItem Text="处理意见" Value="CLD_CLYJ"></asp:ListItem>
                                <asp:ListItem Text="处理方案" Value="CLD_CLFA"></asp:ListItem>
                                <asp:ListItem Text="处理结果" Value="CLD_CLJG"></asp:ListItem>
                                <asp:ListItem Text="费用统计" Value="CLD_FWZFY"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="ddlTX" AutoPostBack="true" OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="未填写" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已填写" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button runat="server" ID="btnQTSX" Text="其他筛选" BackColor="#00ffcc" />
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnQTSX"
                                PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                CancelControlID="btnClose" Y="80">
                            </asp:ModalPopupExtender>
                        </td>
                        <td>
                            <a id="btnDaoChu" runat="server" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-search'"
                                onserverclick="btnDaoChu_onclick">导出</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:Panel ID="PanelCondition" runat="server" Width="75%" Style="display: none">
        <table id="tabsx" width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
            <tr>
                <td colspan="5" align="center">
                    <a runat="server" id="btnQuery" onserverclick="Query" href="#" class="easyui-linkbutton"
                        data-options="iconCls:'icon-search'">查询</a> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                    <a id="btnReset" onclick="btnReset_onclick()" href="#" class="easyui-linkbutton"
                        data-options="iconCls:'icon-remove'">重置</a> &nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <input type="button" id="btnClose" value="关闭" />
                </td>
            </tr>
            <tr>
                <td>
                    编号
                </td>
                <td>
                    <input type="text" runat="server" id="txtBH" />
                </td>
                <td>
                    合同号
                </td>
                <td>
                    <input type="text" runat="server" id="txtHTH" />
                </td>
                <td>
                    项目名称
                </td>
                <td>
                    <input type="text" runat="server" id="txtXMMC" />
                </td>
            </tr>
            <tr>
                <td>
                    顾客名称
                </td>
                <td>
                    <input type="text" runat="server" id="txtGKMC" />
                </td>
                <td>
                    任务号
                </td>
                <td>
                    <input type="text" runat="server" id="txtRWH" />
                </td>
                <td>
                    设备名称
                </td>
                <td>
                    <input type="text" runat="server" id="txtSBMC" />
                </td>
            </tr>
        </table>
    </asp:Panel>
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
                                        <input type="hidden" runat="server" id="CLD_ID" value='<%#Eval("CLD_ID") %>' name="CLD_ID" />
                                        <input type="hidden" runat="server" id="CLD_SPZT" value='<%#Eval("CLD_SPZT")%>' />
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
</asp:Content>
