<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PC_ZHAOBIAOMANAGEMENT.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_ZHAOBIAOMANAGEMENT" Title="招标信息管理" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    招标信息管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>
          <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                询比价清单列表
                                <asp:RadioButton ID="rad_all" runat="server" Text="全部" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_mypart" runat="server" Text="我的任务" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_CheckedChanged" AutoPostBack="true"  Checked="true"/>&nbsp;
                                <asp:RadioButton ID="rad_yiwancheng" runat="server" Text="已完成" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                </div>
                  <div style="height: 470px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="checked_list_Repeater" runat="server" OnItemDataBound="checked_list_Repeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td>
                                                    <strong>单据编号</strong>
                                                </td>
                                                <td>
                                                    <strong>制单日期</strong>
                                                </td>
                                                <td>
                                                    <strong>制单人</strong>
                                                </td>
                                                <td>
                                                    <strong>状态</strong>
                                                </td>
                                                <td>
                                                    <strong>编辑</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server">
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(UCPaging1.PageSize))%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="sheetno" runat="server" Text='<%#Eval("ICL_SHEETNO")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zdtime" runat="server" Text='<%#Eval("ICL_IQRDATE").ToString().Trim()==""?"":Eval("ICL_IQRDATE").ToString().Trim().Substring(0,10)%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zdnm" runat="server" Text='<%#Eval("ST_NAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="totalstate" runat="server" Text='<%#Eval("ICL_STATE").ToString().Trim()=="0"?"未完结":"已完结"%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlContract3" Target="_blank" ToolTip="编辑" NavigateUrl='<%#"http://111.160.8.74:888/PC_Data/ZHAOBIAOEDIT.aspx?sheetno="+Eval("ICL_SHEETNO") %>'
                                                        CssClass="link" runat="server" Visible='<%#Eval("ICL_STATE").ToString().Trim()=="0"?true:false%>'>
                                                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                                        编辑
                                                    </asp:HyperLink>
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
                  <asp:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
