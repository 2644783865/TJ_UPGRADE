<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="CM_SHCLD_TXCLFA.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SHCLD_TXCLFA"
    Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <strong>填写售后质量问题处理单的处理方案</strong>
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
                            <asp:RadioButtonList runat="server" ID="rblRW" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1"></asp:ListItem>
                                <asp:ListItem Text="我的填写任务" Value="2" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
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
                                    <th>
                                        <strong>填写处理方案</strong>
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" />
                                        <asp:Label runat="server" ID="XuHao" Text='<%#Eval("ID_Num")%>'></asp:Label>
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
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplTXFA" NavigateUrl='<%#"CM_SHCLD.aspx?action=writefa&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="Image3" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            填写
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
