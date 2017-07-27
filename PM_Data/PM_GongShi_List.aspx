<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PM_GongShi_List.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_GongShi_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/Datetime.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/xbjhidden.js" type="text/javascript"></script>
    
   
    <contenttemplate>
            <div class="RightContent">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        生产工时管理
                                    </td>
                                    <td style="width:15%">
                                        <asp:Label ID="gongshiyear" runat="server" >年份：</asp:Label>
                                        <asp:DropDownList ID="ddlgongshiyear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgongshiyear_SelectedIndexChanged" >
                                            <asp:ListItem  Selected="True" Value="%">全部</asp:ListItem>
                                            <asp:ListItem Value="2010">2010</asp:ListItem>
                                            <asp:ListItem Value="2011">2011</asp:ListItem>
                                            <asp:ListItem Value="2012">2012</asp:ListItem>
                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width:15%">
                                    <asp:Label ID="gongshimonth" runat="server">月份：</asp:Label>
                                        <asp:DropDownList ID="ddlgongshimonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgongshimonth_SelectedIndexChanged" >
                                            <asp:ListItem Selected="True" Value="%">全部</asp:ListItem>
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                            <td align="right">
                             <asp:Button ID="btnSC" runat="server" Text="删除该月数据" OnClientClick="javascript:return confirm('将会删除该月数据，确定要删除吗？');" onclick="btnSC_Click"  />
                            </td>
                                    <td align="right">
                                        <asp:Button ID="daoru" runat="server" Text="导入" OnClientClick="window.open('PM_Gongshi_Daoru.aspx')" />
                                    </td>
                                   <td align="center">
                                        <asp:Button ID="daochu" runat="server" Text="导出" OnClick="daochu_Click" />
                                    </td>
                                </tr>
                            </table>
                            <div class="box-wrapper">
                                <div class="box-outer">
                                    <div style="height: 450px; overflow: auto; width: 100%">
                                        <div class="cpbox2 xscroll">
                                            <table id="tab" align="center" class="nowrap cptable fullwidth">
                                                <asp:Repeater ID="PM_GongShi_List_Repeater" runat="server" OnItemDataBound="PM_GongShi_List_Repeater_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                            <td>
                                                                <strong>行号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>统计年月</strong>
                                                            </td>
                                                            <td>
                                                                <strong>顾客名称</strong>
                                                            </td>
                                                            <td>
                                                                <strong>合同号</strong>
                                                            </td>
                                                            <td>
                                                                <strong>任务单号</strong>
                                                            </td>                                                    
                                                            
                                                            <td>
                                                            <strong>项目工时费用</br>(元)</strong>
                                                            </td>
                                                            
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                                        ondblclick="<%# showYg(Eval("GS_CUSNAME").ToString(),Eval("GS_CONTR").ToString(),Eval("GS_TSAID").ToString(),Eval("DATEYEAR").ToString(),Eval("DATEMONTH").ToString()) %>" title="双击查看明细">
                                                            <td runat="server" id="num">
                                                                <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                                                
                                                                <asp:Label ID="check" runat="server" Text='<%#Eval("GS_CHECK")%>' Visible="false"></asp:Label>
                                                            </td> 
                                                            <td>
                                                                <asp:Label ID="yyyy" runat="server" Text='<%#Eval("DATEYEAR")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="mm" runat="server" Text='<%#Eval("DATEMONTH")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="yyyymm" runat="server" Text='<%#get_yyyymm(Convert.ToString(Eval("DATEYEAR")),Convert.ToString(Eval("DATEMONTH")))%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="GS_CUSNAME" runat="server" Text='<%#Eval("GS_CUSNAME")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="GS_CONTR" runat="server" Text='<%#Eval("GS_CONTR")%>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="GS_TSAID" runat="server" Text='<%#Eval("GS_TSAID")%>'></asp:Label>
                                                            </td>
                                                            
                                                            <td>
                                                                <asp:Label ID="GS_MONEY" runat="server" Text='<%#Eval("GS_TSAMONEY")%>'></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <td colspan="9" align="center">
                                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                            没有记录！</asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                                            <hr style="width: 100%; height: 0.1px; color: Blue;" />
                                            没有记录!</asp:Panel>
                                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
</asp:Content>
