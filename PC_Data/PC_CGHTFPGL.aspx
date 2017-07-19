<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="PC_CGHTFPGL.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_CGHTFPGL" Title="采购合同管理页面" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
   <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px;
            background-color: #1C86EE;
            color: windowtext;
            cursor: 'default';
            text-align: left;
            list-style: none;
            padding: 0px;
            border: solid 1px gray;
            width: 400px !important;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
        }
    </style>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        function trClick(a) {
            window.open("PC_CGHT.aspx?action=read&id=" + a);
        }
        
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                    <td style="width:60px">
                         发票状态:
                    </td>
                    <td>
                        
                        <asp:RadioButtonList runat="server" ID="Radiofpzt" AutoPostBack="true" OnSelectedIndexChanged="Query"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="已完结"></asp:ListItem>
                            <asp:ListItem Text="未完结" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width:60px">
                         审批状态:
                    </td>
                    <td>
                        
                        <asp:RadioButtonList runat="server" ID="rblSP" AutoPostBack="true" OnSelectedIndexChanged="Query"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="全部合同" Value="4"></asp:ListItem>
                            <asp:ListItem Text="待审批" Value="0"></asp:ListItem>
                            <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                            <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                    <td>
                        合同号：<asp:TextBox runat="server" ID="txtHTBH"></asp:TextBox>
                        <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtHTBH"
                            ServicePath="PC_ZDPP.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                            ServiceMethod="GetHTBH" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </asp:AutoCompleteExtender>
                        订单号：<asp:TextBox runat="server" ID="txtDDBH"></asp:TextBox>&nbsp;&nbsp;
                        <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txtDDBH"
                            ServicePath="PC_ZDPP.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                            ServiceMethod="GetDDBH1" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </asp:AutoCompleteExtender>
                        供应商:<asp:TextBox runat="server" ID="txtGYS"></asp:TextBox>&nbsp;&nbsp;
                        <asp:AutoCompleteExtender runat="server" ID="AutoCompleteExtender3" TargetControlID="txtGYS"
                            ServicePath="PC_ZDPP.asmx" CompletionSetCount="10" MinimumPrefixLength="1" CompletionInterval="10"
                            ServiceMethod="GetGYS" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </asp:AutoCompleteExtender>
                        从：<asp:TextBox runat="server" ID="txtQSSJ" class="easyui-datebox" onfocus="this.blur()"
                            Width="100px"></asp:TextBox>
                        到：<asp:TextBox runat="server" ID="txtJZSJ" class="easyui-datebox" onfocus="this.blur()"
                            Width="100px"></asp:TextBox>
                        <asp:Button runat="server" ID="btnChakan" Text="查询" OnClick="Query" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater ID="rptCGHTGL" runat="server" OnItemDataBound="rptCGHTGL_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE; height: 30px;">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>合同编号</strong>
                                    </td>
                                    <td>
                                        <strong>订单编号</strong>
                                    </td>
                                    <td>
                                        <strong>制单人</strong>
                                    </td>
                                    <td>
                                        <strong>制单日期</strong>
                                    </td>
                                    <td>
                                        <strong>合同总价</strong>
                                    </td>
                                    <td>
                                        <strong>结算方式</strong>
                                    </td>
                                    <td>
                                        <strong>合同供方（卖方）</strong>
                                    </td>
                                    <td>
                                        <strong>联系电话</strong>
                                    </td>
                                    <td>
                                        <strong>审批状态</strong>
                                    </td>
                                    <td>
                                        <strong>是否集采</strong>
                                    </td>
                                    <td>
                                        <strong>已到发票金额(万元)</strong>
                                    </td>
                                    <td>
                                        <strong>发票备注</strong>
                                    </td>
                                    <td>
                                        <strong>发票完结</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息"
                                    ondblclick='trClick(<%#Eval("HT_ID") %>)'>
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" CssClass="checkBoxCss" />
                                        <asp:Label runat="server" ID="lbXuHao" Text='<%#Eval("ID_Num")%>'></asp:Label>
                                        <asp:Label runat="server" ID="lbHT_ID" Text='<%#Eval("HT_ID") %>' Visible="false"></asp:Label>
                                        <asp:HiddenField runat="server" ID="HT_SPZT" Value='<%#Eval("HT_SPZT") %>' />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_XFHTBH" Text='<%#Eval("HT_XFHTBH")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" BorderStyle="None" TextMode="MultiLine" BorderWidth="1px"
                                            ID="lbHT_DDBH" Text='<%#Eval("HT_DDBH")%>'></asp:TextBox><%--订单编号--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_ZDR" Text='<%#Eval("HT_ZDR")%>'></asp:Label><%--制单人--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_ZDSJ" Text='<%#Eval("HT_ZDSJ")%>'></asp:Label><%--制单时间--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_HTZJ" Text='<%#Eval("HT_HTZJ")%>'></asp:Label><%--合同总价--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_JSFS" Text='<%#Eval("HT_JSFS").ToString()=="1"?"货到付款":Eval("HT_JSFS").ToString()=="2"?"款到发货":"未选择"%>'></asp:Label><%--结算方式--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_GF" Text='<%#Eval("HT_GF")%>'></asp:Label><%--合同供方--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_DH" Text='<%#Eval("HT_DH")%>'></asp:Label><%--联系电话--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_SPZT" Text='<%#Eval("HT_SPZT").ToString()=="0"?"待审批":Eval("HT_SPZT").ToString()=="y"?"已通过":Eval("HT_SPZT").ToString()=="n"?"已驳回":"审批中" %>'></asp:Label>
                                    </td>
                                    <td id="tdJC" runat="server">
                                        <%#Eval("HT_JCZT").ToString()=="y"?"是":"否"%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbydfpje"></asp:Label><%--已到发票金额--%>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="fpnote" Width="150px" runat="server" Text='<%#Eval("HT_FPNOTE")%>'
                                        TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnComplete" runat="server" Visible='<%#Eval("HT_FPZT").ToString().Trim()=="y"?false:true%>' OnClick="btnComplete_OnClick"
                                            CommandArgument='<%# Eval("HT_ID")%>' OnClientClick="return confirm('确认完结吗?')">
                                            <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" runat="server" />
                                            发票完结
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Label runat="server" ID="lbHJ" Text="合计：" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td colspan="2" align="center">
                                        <asp:Label runat="server" ID="lbNUM" Text="" ForeColor="Red"></asp:Label>条记录
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbMONEY" Text="" ForeColor="Red"></asp:Label>元
                                    </td>
                                    <td colspan="9">
                                    </td>
                                </tr>
                            </FooterTemplate>
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
