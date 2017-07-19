<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="EQU_GXHT_GL.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_GXHT_GL" Title="设备安全管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    设备安全管理
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
            width: 142px !important;
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
    <link href="FixTable.css" rel="stylesheet" type="text/css" />
    <link href="../PC_Data/PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="../PC_Data/PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script src="../PC_Data/PcJs/rowcolor.js" type="text/javascript"></script>

    <script src="../JS/KeyControlTask.js" type="text/javascript"></script>

    <script src="../JS/DatePicker.js" type="text/javascript"></script>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
      $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=HT_HTBH]").val();
                window.open("EQU_GXHT.aspx?action=read&id=" + id);
            });
        })

//        function trClick(a) { 

//            window.open("EQU_GXHT.aspx?action=read&id=" + a);
//        }      
    </script>

    <script type="text/javascript">
         function reset(){
           document.getElementById("<%=txtHTBH.ClientID %>").value="";
           document.getElementById("<%=txtGYS.ClientID %>").value="";
           document.getElementById("<%=txtQSSJ.ClientID %>").value="";
           document.getElementById("<%=txtJZSJ.ClientID %>").value="";
           
        }
         function seleAll() {
            $(".cpbox.xscroll :checkbox[checked='true']").click();
            if ($("#selectall").attr("checked")) {
                var s = $(".cpbox.xscroll :checkbox").length ;
                $(".cpbox.xscroll :checkbox:lt(" + s + ")").click();
            }
        }

        function seleLian() {
            var a = $(".cpbox.xscroll :checkbox[checked='true']");
            if (a.length == "2") {
                nmin = $(".cpbox.xscroll :checkbox").index(a.eq(0));
                nmax = $(".cpbox.xscroll :checkbox").index(a.eq(1)) - nmin - 1;
                $(".cpbox.xscroll :checkbox:gt(" + nmin + "):lt(" + nmax + ")").click();
            }
        }

        function seleCancel() {
            $(".cpbox.xscroll :checkbox[checked='true']").click();
        }
    </script>

    <asp:ToolkitScriptManager runat="server" ID="ToolkitScriptManager0">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblSP" AutoPostBack="true" OnSelectedIndexChanged="Query"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="全部合同" Value="0"></asp:ListItem>
                            <asp:ListItem Text="待审批" Value="1"></asp:ListItem>
                            <asp:ListItem Text="审批中" Value="2"></asp:ListItem>
                            <asp:ListItem Text="已通过" Value="3"></asp:ListItem>
                            <asp:ListItem Text="已驳回" Value="4"></asp:ListItem>
                            <asp:ListItem Text="我的审批任务" Value="5" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;每页：<asp:DropDownList ID="ddlRowCount"
                            runat="server" Width="45px" AutoPostBack="true" OnSelectedIndexChanged="Query">
                            <asp:ListItem Text="30" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="60" Value="1"></asp:ListItem>
                            <asp:ListItem Text="90" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                        行 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnDaoChu" Text="导出合同" OnClick="btnDaoChu_OnClick"
                            BackColor="#98FB98" Height="25px" Visible="false" />
                        <%--       <a id="hfDaoChu" runat="server" href="#" class="easyui-linkbutton" onclick="hfDaoChu_OnClick">
                            导出合同</a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
                        <asp:HyperLink runat="server" ID="hplAdd" NavigateUrl="EQU_GXHT.aspx?action=add"><img src="../Assets/images/Add_new_img.gif" />新增设备采购合同</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <table width="100%">
                <tr>
                    <td style="width: 80%">
                        &nbsp;&nbsp;合同号：<asp:TextBox runat="server" ID="txtHTBH" Width="120px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtHTBH"
                            ServicePath="EQU_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                            CompletionInterval="10" ServiceMethod="GetHTBH" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </asp:AutoCompleteExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;供应商:
                        <asp:TextBox runat="server" ID="txtGYS" Width="120px"></asp:TextBox>&nbsp;&nbsp;
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtGYS"
                            ServicePath="EQU_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                            CompletionInterval="10" ServiceMethod="GetGYS" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                        </asp:AutoCompleteExtender>
                        &nbsp;&nbsp;&nbsp; 从：<asp:TextBox runat="server" ID="txtQSSJ" class="easyui-datebox"
                            onfocus="this.blur()" Width="100px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;到：<asp:TextBox runat="server" ID="txtJZSJ" class="easyui-datebox"
                            onfocus="this.blur()" Width="100px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnChakan" Text="查询" OnClick="Query" /><%--<input
                            id="btnReset" type="button" value="重置" onclick="reset()" style="background-color: #98FB98" />--%>
                        <asp:Button ID="btn_Reset" runat="server" Text="重置" OnClick="btn_Reset_Click" />
                    </td>
                    <td style="width: 20%" align="right">
                        <asp:Button runat="server" ID="btnBatchDelete" Text="批量删除"
                            OnClientClick="return confirm('确认批量删除所选合同？')" OnClick="btnBatchDelete_OnClick"
                            Visible="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnBackCheck" Text="返审"
                            Width="32px" OnClientClick="return confirm('确认返审该合同？')" OnClick="btnBackCheck_OnClick"
                            Visible="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--                        <asp:LinkButton runat="server" ID="btnBackCheck" Font-Size="14px" ForeColor="Red"
                            Text="返审" OnClientClick="return confirm('确认返审该合同？')" CommandArgument='<%#Eval("HT_HTBH")%>'
                            OnClick="lbtnBackCheck_OnClick" Visible="false">
                        </asp:LinkButton>--%>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="height: auto; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater ID="rptGXHT" runat="server" OnItemDataBound="rptGXHT_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE; height: 30px;">
                                    <td>
                                        <strong>序号</strong>
                                    </td>
                                    <td>
                                        <strong>合同编号</strong>
                                    </td>
                                    <td>
                                        <strong>制单人</strong>
                                    </td>
                                    <td>
                                        <strong>制单时间</strong>
                                    </td>
                                    <td>
                                        <strong>合同总价</strong>
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
                                        <strong>修改</strong>
                                    </td>
                                    <td>
                                        <strong>删除</strong>
                                    </td>
                                    <td>
                                        <strong>审批</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="双击查看详细信息">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" CssClass="checkBoxCss" /><asp:Label runat="server"
                                            ID="lbHT_ID" Text='<%#Eval("HT_ID") %>' Visible="false"></asp:Label><asp:Label ID="lblXuHao"
                                                runat="server" Text="Lable1"></asp:Label>
                                        <input type="hidden" runat="server" id="hidHT_HTBH" value=' <%#Eval("HT_HTBH")%>'
                                            name="HT_HTBH" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_HTBH" Text=' <%#Eval("HT_HTBH")%>'></asp:Label><%--合同编号--%>
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
                                        <asp:Label runat="server" ID="lbHT_GF" Text='<%#Eval("HT_GF")%>'></asp:Label><%--合同供方--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_DH" Text='<%#Eval("HT_DH")%>'></asp:Label><%--联系电话--%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbHT_SPZT" Text='<%#Eval("HT_SPZT").ToString()=="3"?"已通过":Eval("HT_SPZT").ToString()=="0"?"初始化":Eval("HT_SPZT").ToString()=="4"?"已驳回":Eval("HT_SPZT").ToString()=="1"?"待审批":"审批中" %>'></asp:Label>
                                    </td>
                                    <%--审批状态--%>
                                    <td runat="server" id="td1_1">
                                        <asp:HyperLink runat="server" ID="hplXiuGai" CssClass="link" NavigateUrl='<%#"EQU_GXHT.aspx?action=alter&id="+Eval("HT_HTBH") %>'>
                                            <asp:Image runat="server" ID="imgXiuGai" ImageUrl="~/Assets/images/res.gif" Width="20px"
                                                Height="20px" ImageAlign="AbsMiddle" />
                                            修改
                                        </asp:HyperLink>
                                    </td>
                                    <td runat="server" id="td2_2">
                                        <asp:LinkButton runat="server" ID="btnDelete" Text="删除" OnClientClick="return confirm('确认删除该合同？')"
                                            CommandArgument='<%#Eval("HT_HTBH")%>' OnClick="btnDelete_OnClick">
                                            <asp:Image runat="server" ID="imgDelete" Width="20px" Height="20px" ImageAlign="AbsMiddle"
                                                ImageUrl="~/Assets/images/erase.gif" />
                                        </asp:LinkButton>
                                    </td>
                                    <td runat="server" id="td3_3">
                                        <asp:HyperLink runat="server" ID="hplCheck" CssClass="link" NavigateUrl='<%#"EQU_GXHT.aspx?action=check&id="+Eval("HT_HTBH")%>'>
                                            <asp:Image runat="server" ID="imgCheck" Width="20px" Height="20px" border="0" hspace="2"
                                                ImageAlign="AbsMiddle" ImageUrl="~/Assets/images/res.gif" />
                                            审批
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                <tr>
                                    <td colspan="1" align="center">
                                        <asp:Label runat="server" ID="lbHJ" Text="合计：" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td colspan="3" align="center">
                                        <asp:Label runat="server" ID="lbNUM" Text="" ForeColor="Red"></asp:Label>条记录
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lbMONEY" Text="" ForeColor="Red"></asp:Label>元
                                    </td>
                                    <td colspan="6">
                                    </td>
                                </tr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPane" runat="server" ForeColor="Red" HorizontalAlign="Center">
                        没有相关信息!</asp:Panel>
                </div>
                <span visible="true" id="quanxianKJ" runat="server">
                    <input type="checkbox" onclick="seleAll()" id="selectall" /><label for="selectall">&nbsp;全选</label>&nbsp;&nbsp;
                    <input type="button" value="连选" onclick="seleLian()" id="selectlian" />
                    <input type="button" value="取消" onclick="seleCancel()" id="selectcancel" /></span>
            </div>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
