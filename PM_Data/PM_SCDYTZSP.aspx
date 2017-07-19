<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PM_SCDYTZSP.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_SCDYTZSP" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产代用通知审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input:hidden").val();
                window.open("PM_SCDYTZD.aspx?action=read&id=" + id);
            });
        });

        function lbtnSolve_OnClientClick() {
            if (confirm("您确定该单据已处理吗？点击处理后单据状态将变为已处理！！！")) {
                return true;
            }
            else {
                return false;
            }
        }
    
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="cbxRW" Text="我的审批任务" AutoPostBack="true" OnCheckedChanged="Query" />
                        </td>
                        <td>
                        </td>
                        <td>
                            代用通知状态：
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblSPZT" OnSelectedIndexChanged="Query" RepeatDirection="Horizontal"
                                AutoPostBack="true">
                                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                                <asp:ListItem Text="未审批" Value="1"></asp:ListItem>
                                <asp:ListItem Text="审批中" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="3"></asp:ListItem>
                                <asp:ListItem Text="未通过" Value="4"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblCLZT" RepeatDirection="Horizontal" OnSelectedIndexChanged="Query"
                                AutoPostBack="true">
                                <asp:ListItem Text="未处理" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已处理" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSX">
                                <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="批号" Value="TZD_PH"></asp:ListItem>
                                <asp:ListItem Text="任务号" Value="TZD_RWH"></asp:ListItem>
                                <asp:ListItem Text="合同名称" Value="TZD_HTMC"></asp:ListItem>
                                <asp:ListItem Text="设备名称" Value="TZD_SBMC"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtItem"></asp:TextBox>
                            <asp:Button runat="server" ID="btnSX" Text="查看" OnClick="Query" />
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
                        <asp:Repeater runat="server" ID="rptDYTZSP" OnItemDataBound="rptDYTZSP_OnItemDataBound">
                            <HeaderTemplate>
                                <th align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    序号
                                </th>
                                <th>
                                    申请人
                                </th>
                                <th>
                                    申请时间
                                </th>
                                <th>
                                    批号
                                </th>
                                <th>
                                    合同
                                </th>
                                <th>
                                    任务号
                                </th>
                                <th>
                                    项目名称
                                </th>
                                <th>
                                    设备名称
                                </th>
                                <th>
                                    编制人
                                </th>
                                <th>
                                    编制日期
                                </th>
                                <th>
                                    修改
                                </th>
                                <th>
                                    审批
                                </th>
                                <th>
                                    处理
                                </th>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息"
                                    ondblclick='trClick(<%#Eval("TZD_SJID") %>)' style="height: 25px">
                                    <td id="td1" runat="server">
                                        <asp:CheckBox runat="server" ID="cbxXuHao" />
                                        <asp:Label ID="rownum" runat="server" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="TZD_ID" Value='<%#Eval("TZD_ID")%>' />
                                        <asp:HiddenField runat="server" ID="TZD_SPZT" Value='<%#Eval("TZD_SPZT")%>' />
                                        <asp:HiddenField runat="server" ID="TZD_SPR1" Value='<%#Eval("TZD_SPR1")%>' />
                                        <asp:HiddenField runat="server" ID="TZD_SPR2" Value='<%#Eval("TZD_SPR2")%>' />
                                        <asp:HiddenField runat="server" ID="TZD_CLZT" Value='<%#Eval("TZD_CLZT")%>' />
                                        <asp:HiddenField runat="server" ID="TZD_ZDR" Value='<%#Eval("TZD_ZDR")%>' />
                                    </td>
                                    <td>
                                        <%#Eval("TZD_ZDR") %>
                                    </td>
                                    <td>
                                        <%#Eval("TZD_ZDSJ") %>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="TZD_PH" Text='<%#Eval("TZD_PH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="TZD_HTMC" Text='<%#Eval("TZD_HTMC")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="TZD_RWH" Text='<%#Eval("TZD_RWH") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="TZD_XMMC" Text='<%#Eval("TZD_XMMC") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="TZD_SBMC" Text='<%#Eval("TZD_SBMC") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="TZD_BZR" Text='<%#Eval("TZD_BZR") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="TZD_BZRQ" Text='<%#Eval("TZD_BZRQ") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplAlter" NavigateUrl='<%#"PM_SCDYTZD.aspx?action=alter&id="+Eval("TZD_ID")%>'>
                                            <img src="../Assets/icons/create.gif"  width="20px" height="20px"/>修改
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplSP" NavigateUrl='<%#"PM_SCDYTZD.aspx?action=check&id="+Eval("TZD_ID")%>'>
                                            <asp:Image runat="server" ID="imgAdd" ImageUrl="~/Assets/images/shenhe.gif" Height="25px" />
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtnSolve" Text="处理" ForeColor="Red" OnClientClick="return  lbtnSolve_OnClientClick()"
                                            CommandArgument='<%#Eval("TZD_ID")%>' OnClick="lbtnSolve_OnClick">
                                            <asp:Image runat="server" ID="imgSolve" ImageUrl="~/Assets/icons/edit.gif" Height="20px" />
                                        </asp:LinkButton>
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
        </div>
        <uc1:UCPaging ID="UCPaging1" runat="server" />
    </div>
</asp:Content>
