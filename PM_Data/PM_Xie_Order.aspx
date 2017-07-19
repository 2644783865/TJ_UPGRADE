<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_Xie_Order.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.PM_Data.PM_Xie_Order" Title="订单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    外协订单管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        function redirectw(obj) {
            var orderno;
            orderno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            window.open("PM_Xie_IntoOrder.aspx?&orderno=" +orderno + "");
        }
        function mowinopen(ptcode_rcode) {
            var autonum = Math.round(10000 * Math.random());
            window.open('PM_Data_addjs_list.aspx?autonum=' + autonum + '&ptcode_rcode=' + escape(ptcode_rcode) + '', '', "dialogHeight:460px;dialogWidth:780px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
        }
    </script>

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
        .backwhite
        {
            background-color: White;
        }
    </style>

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 2,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 2; i < j; i++) {
                        var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                        var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                        var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                        if (coltext == coltextbef) {
                            for (var k = 2; k <= 6; k++) {
                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            }
                        }
                    }
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
        }
       
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                外协订单列表
                                <asp:RadioButton ID="rad_all" runat="server" Text="全部单据" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true" />
                                &nbsp;
                                <asp:RadioButton ID="rad_mypart" runat="server" Text="我的单据" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_mypart_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                            </td>
                            <td style="text-align: right">
                                外协类型：
                            </td>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="rbl_waixie" runat="server" AutoPostBack="true" RepeatColumns="3"
                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_waixie_OnSelectedIndexChanged">
                                    <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="工序外协" Value="工序外协"></asp:ListItem>
                                    <asp:ListItem Text="成品外协" Value="成品外协"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:Button ID="btn_add" runat="server" Height="25px" Width="70px" BorderColor="Pink"
                                    BorderStyle="Double" Text="追加结算单" OnClick="btn_add_Click" Visible="true" />&nbsp;&nbsp;
                                <asp:Button ID="btn_xiatui" runat="server" Height="25px" Width="70px" BorderColor="Pink"
                                    BorderStyle="Double" Text="生成结算单" OnClick="btn_xiatui_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />&nbsp;
                                <asp:Button ID="btn_Export" runat="server" Text="批量导出" OnClick="btn_Export_Click" />
                                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()"
                                    Visible="false" />&nbsp;
                                <%--  <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                    PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    Y="80">
                                </asp:ModalPopupExtender>--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div>
        <table width="100%">
            <tr>
                <td>
                    时间：
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    年
                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_OnSelectedIndexChanged">
                    </asp:DropDownList>
                    月
                </td>
                <td align="right">
                    单据编号：
                </td>
                <td>
                    <asp:TextBox ID="txt_docnum" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td align="right">
                    供应商：
                </td>
                <td>
                    <asp:TextBox ID="tb_supply" runat="server" OnTextChanged="tb_supply_Textchanged"
                        Width="100px" AutoPostBack="true"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="tb_supply"
                        ServicePath="PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                        CompletionInterval="10" ServiceMethod="GetWaixie" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                    </cc1:AutoCompleteExtender>
                </td>
                <td align="right">
                    计划跟踪号：
                </td>
                <td>
                    <asp:TextBox ID="txt_jhgzh" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    质检结果：
                </td>
                <td>
                    <asp:DropDownList ID="ddl_bjstatus" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                        <asp:ListItem Text="未报检" Value="0"></asp:ListItem>
                        <asp:ListItem Text="待检" Value="1"></asp:ListItem>
                        <asp:ListItem Text="报废" Value="2"></asp:ListItem>
                        <asp:ListItem Text="整改" Value="3"></asp:ListItem>
                        <asp:ListItem Text="待定" Value="4"></asp:ListItem>
                        <asp:ListItem Text="让步接收" Value="5"></asp:ListItem>
                        <asp:ListItem Text="部分合格" Value="6"></asp:ListItem>
                        <asp:ListItem Text="合格" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right">
                    每页显示：
                    <asp:DropDownList ID="DropDownList5" runat="server">
                        <asp:ListItem Text="50条记录" Value="50"></asp:ListItem>
                        <asp:ListItem Text="100条记录" Value="100"></asp:ListItem>
                        <asp:ListItem Text="150条记录" Value="150"></asp:ListItem>
                        <asp:ListItem Text="200条记录" Value="200"></asp:ListItem>
                        <asp:ListItem Text="300条记录" Value="300"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="QueryButton_Click" Text="查询" />
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="height: 475px; overflow: auto; width: 100%">
                <div class="cpbox xscroll">
                    <table id="tab" class="nowrap cptable fullwidth" align="center">
                        <asp:Repeater ID="Purordertotal_list_Repeater" runat="server" OnItemDataBound="Purordertotal_list_Repeater_ItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td>
                                    </td>
                                    <td>
                                        <strong>行号</strong>
                                    </td>
                                    <td>
                                        <strong>单据编号</strong>
                                    </td>
                                    <td>
                                        <strong>制单人</strong>
                                    </td>
                                    <td>
                                        <strong>制单日期</strong>
                                    </td>
                                    <td>
                                        <strong>订单总金额</strong>
                                    </td>
                                    <td>
                                        <strong>供应商</strong>
                                    </td>
                                    <td>
                                        <strong>比价单号</strong>
                                    </td>
                                    <td>
                                        <strong>结算<br />
                                            标志</strong>
                                    </td>
                                    <td>
                                        <strong>加工单号</strong>
                                    </td>
                                    <td>
                                        <strong>是否报检</strong>
                                    </td>
                                    <td>
                                        <strong>计划跟踪号</strong>
                                    </td>
                                    <td>
                                        <strong>批号</strong>
                                    </td>
                                    <td>
                                        <strong>图号</strong>
                                    </td>
                                    <td>
                                        <strong>名称</strong>
                                    </td>
                                    <td>
                                        <strong>数量</strong>
                                    </td>
                                    <td>
                                        <strong>外协类型</strong>
                                    </td>
                                    <td>
                                        <strong>加工工序</strong>
                                    </td>
                                    <td>
                                        <strong>最终报价</strong>
                                    </td>
                                    <td>
                                        <strong>交货日期</strong>
                                    </td>
                                    <td>
                                        <strong>实际到货日期</strong>
                                    </td>
                                    <td>
                                        <strong>备注</strong>
                                    </td>
                                    <td>
                                        <strong>订单备注</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class='<%#(Container.ItemIndex%2==0)?"backwhite":"baseGadget"%>' onmouseover="mover(this)"
                                    onmouseout="mout(this)" ondblclick="redirectw(this)">
                                    <td id="td1" runat="server">
                                        <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                            onclick="checkme(this)"></asp:CheckBox>
                                    </td>
                                    <td id="td2" runat="server">
                                        <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(DropDownList5.SelectedValue))%>'></asp:Label>
                                        <asp:Label ID="TO_ID" runat="server" Text='<%#Eval("TO_ID")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_DOCNUM" runat="server" Text='<%#Eval("TO_DOCNUM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_ZDR" runat="server" Text='<%#Eval("TO_ZDR")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="TO_ZDRNAME" runat="server" Text='<%#Eval("TO_ZDRNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_ZDTIME" runat="server" Text='<%#Eval("TO_ZDTIME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_AMOUNT" runat="server" Text='<%#Eval("TO_AMOUNT")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_SUPPLYID" runat="server" Text='<%#Eval("TO_SUPPLYID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="TO_SUPPLYNAME" runat="server" Text='<%#Eval("TO_SUPPLYNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_BJDOCNUM" runat="server" Text='<%#Get_Int(Eval("TO_BJDOCNUM").ToString())%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_STATE" runat="server" Text='<%#Get_jsstate(Eval("TO_STATE").ToString())%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PIC_JGNUM" runat="server" Text='<%#Eval("PIC_JGNUM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="Hypzlbj" runat="server" Target="_blank" title="点击查看报检信息">
                                            <asp:Label ID="zlbj" runat="server" Text='<%#get_zlbj(Eval("PIC_BJSTATUS").ToString())%>'></asp:Label>
                                        </asp:HyperLink>
                                    </td>
                                    <td style="white-space: normal">
                                        <asp:Label ID="TO_PTC" runat="server" Text='<%#Eval("TO_PTC")%>' Width="300px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="PIC_PTCODE" Text='<%#Eval("PIC_PTCODE") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="PIC_TUHAO" Text='<%#Eval("PIC_TUHAO") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PIC_MNAME" runat="server" Text='<%#Eval("PIC_MNAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PIC_ZXNUM" runat="server" Text='<%#Eval("PIC_ZXNUM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PIC_WXTYPE" runat="server" Text='<%#Eval("PIC_WXTYPE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_PROCESS" runat="server" Text='<%#Eval("TO_PROCESS")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_MONEY" runat="server" Text='<%#Eval("TO_MONEY")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="PIC_SUPPLYTIME" runat="server" Text='<%#Eval("PIC_SUPPLYTIME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="BJSJ" runat="server" Text='<%#Eval("BJSJ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_NOTE" runat="server" Text='<%#Eval("TO_NOTE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TO_TOTALNOTE" runat="server" Text='<%#Eval("TO_TOTALNOTE")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td colspan="22" align="center">
                                <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                    没有记录！</asp:Panel>
                            </td>
                        </tr>
                    </table>

                    <script language="javascript" type="text/javascript">
                                    var myST = new superTable("tab", {
                                        cssSkin: "tDefault",
                                        headerRows: 1,
                                        fixedCols: 0,
                                        //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                                        onStart: function() {
                                            //		        this.start = new Date();
                                        },
                                        onFinish: function() {
                                            for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 2; i < j; i++) {
                                                var dataRow = this.sDataTable.tBodies[0].rows[i + 1];
                                                var coltextbef = this.sDataTable.tBodies[0].rows[i].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                                                var coltext = this.sDataTable.tBodies[0].rows[i + 1].getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
                                                if (coltext == coltextbef) {
                                                    for (var k = 2; k <=6; k++) {
                                                        dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                                                    }
                                                }
                                            }

                                            //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                                        }
                                    });
                    </script>

                </div>
                <div>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                            Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                        <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                        <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                    </td>
                    <td>
                        筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
                        合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            </div> </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="1">
        <ProgressTemplate>
            <div style="position: absolute; top: 50%; right: 40%">
                <table>
                    <tr>
                        <td align="right">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" />
                        </td>
                        <td align="left" style="background-color: Yellow; font-size: medium;">
                            数据处理中，请稍后...
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
