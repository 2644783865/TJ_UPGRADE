<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="PC_CGHTGL.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_CGHTGL" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购合同管理
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

            //  window.location.href = 'PC_CGHT.aspx?action=read&id=' + a;
            window.open("PC_CGHT.aspx?action=read&id=" + a);
        }

        function OpenFuKuan() {
            window.open("PC_CGHT_FKDetail.aspx");
        }
        function OpenFaPiao() {
            window.open("PC_CGHT_FPDetail.aspx");
        }
        
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box_right" align="right">
            <asp:Button runat="server" ID="btnChuLi" Text="处理（标记）" OnClick="btnChuLi_OnClick" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnJiCai" Text="集采（标记）" OnClick="btnJiCai_OnClick"  />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnCancel" Text="取消集采标记" OnClick="btnCancel_OnClick" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnFPDetail" Text="查看发票明细" OnClientClick="OpenFaPiao()" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnFKDetail" Text="查看付款明细" OnClientClick="OpenFuKuan()" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnDaoChu" Text="导出合同" OnClick="btnDaoChu_OnClick" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button runat="server" ID="btnAdd" Text="新增采购合同" OnClick="btnAdd_OnClick" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                    <%--  <td>
                        <asp:RadioButtonList runat="server" ID="rblSX" AutoPostBack="true" OnSelectedIndexChanged="Query"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="全部合同" Value="1"></asp:ListItem>
                            <asp:ListItem Text="我的任务" Value="2" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>--%>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblSP" AutoPostBack="true" OnSelectedIndexChanged="Query"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="全部合同" Value="4"></asp:ListItem>
                            <asp:ListItem Text="待审批" Value="0"></asp:ListItem>
                            <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                            <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                            <asp:ListItem Text="我的审批任务" Value="5" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblCL" AutoPostBack="true" OnSelectedIndexChanged="Query"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="未处理" Value="n"></asp:ListItem>
                            <asp:ListItem Text="已处理" Value="y"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblJC" AutoPostBack="true" OnSelectedIndexChanged="Query"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="非集采" Value="n"></asp:ListItem>
                            <asp:ListItem Text="集采" Value="y"></asp:ListItem>
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
                                    <td runat="server" id="td1">
                                        <strong>修改</strong>
                                    </td>
                                    <td runat="server" id="td2">
                                        <strong>删除</strong>
                                    </td>
                                    <td runat="server" id="td3">
                                        <strong>审批</strong>
                                    </td>
                                    <td runat="server" id="td4">
                                        <strong>反审</strong>
                                    </td>
                                    <%--<td>
                                        <strong>单位</strong>
                                    </td>
                                    <td id="td12" runat="server">
                                        <strong>辅助数量</strong>
                                    </td>
                                    <td>
                                        <strong>辅助单位</strong>
                                    </td>
                                    <td id="td13" runat="server">
                                        <strong>长度</strong>
                                    </td>
                                    <td id="td14" runat="server">
                                        <strong>宽度</strong>
                                    </td>
                                    <td id="td15" runat="server">
                                        <strong>片/支</strong>
                                    </td>
                                    <td id="td16" runat="server">
                                        <strong>含税单价</strong>
                                    </td>
                                    <td>
                                        <strong>含税金额</strong>
                                    </td>
                                    <td>
                                        <strong>交货日期</strong>
                                    </td>
                                    <td>
                                        <strong>备注</strong>
                                    </td>
                                    <td>
                                        <strong>计划跟踪号</strong>
                                    </td>--%>
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
                                    <td id="HT_XFHTBH" runat="server">
                                        <%#Eval("HT_XFHTBH")%>
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
                                    <td runat="server" id="td1_1">
                                        <asp:HyperLink runat="server" ID="hplXiuGai" CssClass="link" NavigateUrl='<%#"PC_CGHT.aspx?action=alter&id="+Eval("HT_ID") %>'>
                                            <asp:Image runat="server" ID="imgXiuGai" ImageUrl="~/Assets/images/create.gif" Width="20px"
                                                Height="20px" ImageAlign="AbsMiddle" />
                                            修改
                                        </asp:HyperLink>
                                    </td>
                                    <td runat="server" id="td2_2">
                                        <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除" OnClientClick="return confirm('确认删除该合同？')"
                                            CommandArgument='<%#Eval("HT_ID")%>' OnClick="lbtnDelete_OnClick">
                                            <asp:Image runat="server" ID="imgDelete" Width="20px" Height="20px" ImageAlign="AbsMiddle"
                                                ImageUrl="~/Assets/images/erase.gif" />
                                        </asp:LinkButton>
                                    </td>
                                    <td runat="server" id="td3_3">
                                        <asp:HyperLink runat="server" ID="hplCheck" CssClass="link" NavigateUrl='<%#"PC_CGHT.aspx?action=check&id="+Eval("HT_ID")%>'>
                                            <asp:Image runat="server" ID="imgCheck" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/images/shenhe.gif" />
                                            审批
                                        </asp:HyperLink>
                                    </td>
                                    <td runat="server" id="td4_4">
                                        <asp:LinkButton runat="server" ID="lbtnBackCheck" Text="返审" OnClientClick="return confirm('确认返审该合同？')"
                                            CommandArgument='<%#Eval("HT_ID")%>' OnClick="lbtnBackCheck_OnClick">
                                            <asp:Image runat="server" ID="Image1" Width="20px" Height="20px" ImageAlign="AbsMiddle"
                                                ImageUrl="~/Assets/images/erase.gif" />
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
