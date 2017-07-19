<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PM_SCDYTZDGL.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_SCDYTZDGL" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产代用通知
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
                var id = $(this).find("span[name*=MS_ID]").html();
                window.open("PM_SCDYTZD.aspx?action=read1&ms_id=" + id);
            });
        });
    
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table>
                    <tr>
                        <td>
                            筛选
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSX">
                                <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="批号" Value="MS_ID"></asp:ListItem>
                                <asp:ListItem Text="合同号" Value="MS_PJID"></asp:ListItem>
                                <asp:ListItem Text="项目名称" Value="MS_PJNAME"></asp:ListItem>
                                <asp:ListItem Text="任务号" Value="MS_ENGID"></asp:ListItem>
                                <asp:ListItem Text="设备名称" Value="MS_ENGNAME"></asp:ListItem>
                                <asp:ListItem Text="子设备名称" Value="MS_CHILDENGNAME"></asp:ListItem>
                                <asp:ListItem Text="图号" Value="MS_MAP"></asp:ListItem>
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
                        <asp:Repeater runat="server" ID="rptDYTZ">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        批号
                                    </th>
                                    <th>
                                        合同
                                    </th>
                                    <th>
                                        项目名称
                                    </th>
                                    <th>
                                        制图员
                                    </th>
                                    <th>
                                        任务号
                                    </th>
                                    <th>
                                        设备
                                    </th>
                                    <th>
                                        子设备名称
                                    </th>
                                    <th>
                                        图号
                                    </th>
                                    <th>
                                        下发时间
                                    </th>
                                    <th>
                                        新增通知
                                    </th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息"
                                    ondblclick='trClick(<%#Eval("MS_ID") %>)'>
                                    <td id="td_1" runat="server">
                                        <asp:CheckBox runat="server" ID="cbxXuHao" />
                                        <asp:Label ID="rownum" runat="server" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_ID" name="MS_ID" runat="server" Text='<%#Eval("MS_ID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_PJID" runat="server" Text='<%#Eval("MS_PJID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_PJNAME" runat="server" Text='<%#Eval("MS_PJNAME")%>'></asp:Label>
                                    </td>
                                    <td id="td_5" runat="server">
                                        <asp:Label ID="MS_PERSON" runat="server" Text='<%#Eval("MS_PERSON")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_ENGID" runat="server" Text='<%#Eval("MS_ENGID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <div style="width: 150px; white-space: normal">
                                            <asp:Label ID="MS_ENGNAME" runat="server" Text='<%#Eval("MS_ENGNAME")%>'></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="width: 150px; white-space: normal">
                                            <asp:Label ID="MS_CHILDENGNAME" runat="server" Text='<%#Eval("MS_CHILDENGNAME")%>'></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_MAP" runat="server" Text='<%#Eval("MS_MAP")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="MS_ADATE" runat="server" Text='<%#Eval("MS_ADATE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink runat="server" ID="hplAdd" Text="新增" ForeColor="Red" NavigateUrl='<%#"PM_SCDYTZD.aspx?action=add&ms_id="+Eval("MS_ID")%>'>
                                            <asp:Image runat="server" ID="imgAdd" ImageUrl="~/Assets/images/create.gif" Height="25px" />
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
