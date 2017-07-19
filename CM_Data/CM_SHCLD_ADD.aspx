<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="CM_SHCLD_ADD.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_SHCLD_ADD" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    售后质量问题处理单管理
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
        
        function btnAdd_OnClick() {
            location.href = "CM_SHCLD.aspx?action=add";
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblRW" AutoPostBack="true" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="Query">
                                <asp:ListItem Text="全部" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="待审批" Value="2"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="3"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="4"></asp:ListItem>
                                <asp:ListItem Text="被驳回" Value="5"></asp:ListItem>
                                <asp:ListItem Text="已处理" Value="6"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            <a runat="server" id="btnAdd" href="#" class="easyui-linkbutton" data-options="iconCls:'icon-add'"
                                onclick="btnAdd_OnClick()">新增质量问题处理单</a>
                            <input type="hidden" runat="server" id="hidTXR" />
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
                                        <strong>修改</strong>
                                    </th>
                                    <th>
                                        <strong>删除</strong>
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%#Eval("ID_Num")%>' />
                                        <asp:HiddenField runat="server" ID="CLD_ID" Value='<%#Eval("CLD_ID")%>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="CLD_GKMC" Text='<%#Eval("CLD_GKMC")%>'>
                                        </asp:Label>
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
                                        <asp:HyperLink runat="server" ID="hplAlter" NavigateUrl='<%#"CM_SHCLD.aspx?action=alter&id="+Eval("CLD_ID")%>'>
                                            <asp:Image runat="server" ID="Image3" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/icons/edit.gif" />
                                            修改
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('确定要删除吗？？？')" Text="删除" ForeColor="Red" CommandArgument='<%#Eval("CLD_ID") %>'
                                            OnClick="lbtnDelete_OnClick"></asp:LinkButton>
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
