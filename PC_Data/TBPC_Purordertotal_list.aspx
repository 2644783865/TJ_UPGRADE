<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="TBPC_Purordertotal_list.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.TBPC_Purordertotal_list"
    Title="订单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购订单管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

//        function PLdaochu() {
//            //        window.showModalDialog('PC_TBPC_PLdaochu.aspx','',"dialogHeight:300px;dialogWidth:500px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
//            window.open("PC_TBPC_PLdaochu.aspx");
//            //        window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?shape="+escape(shape)+"&mp_id="+escape(mpid)+"";
//        }

        function openmodewin() {
            var num1;
            var num2;
            radio11 = document.getElementById("ctl00_PrimaryContent_rad_all");
            radio12 = document.getElementById("ctl00_PrimaryContent_rad_mypart");
            radio21 = document.getElementById("ctl00_PrimaryContent_rad_quanbu");
            radio22 = document.getElementById("ctl00_PrimaryContent_rad_weitijiao");
            radio24 = document.getElementById("ctl00_PrimaryContent_rad_yiguanbi");
            radio25 = document.getElementById("ctl00_PrimaryContent_rad_weidaohuo");
            radio26 = document.getElementById("ctl00_PrimaryContent_rad_bfdaohuo");
            radio27 = document.getElementById("ctl00_PrimaryContent_rad_yidaohuo");


            if (radio11.checked == true) {
                num1 = 1;
            }
            else {
                num1 = 2;
            }

            if (radio21.checked == true) {
                num2 = 1;
            }
            else if (radio22.checked == true) {
                num2 = 2;
            }
            else if (radio24.checked == true) {
                num2 = 4;
            }
            else if (radio25.checked == true) {
                num2 = 5;
            }
            else if (radio26.checked == true) {
                num2 = 6;
            }
            else if (radio27.checked == true) {
                num2 = 7;
            }
            var tablenmid_code;
            var autonum = Math.round(10000 * Math.random());
            tablenmid_code = document.getElementById("<%=hid_filter.ClientID %>").value;
            window.showModalDialog('PC_TBPC_Itemsfilter.aspx?autonum=' + autonum + '&tablenmid_code=' + tablenmid_code + '', '', "dialogHeight:460px;dialogWidth:680px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "TBPC_Purordertotal_list.aspx?sf=1&num1=";
        }

        function redirectw(obj) {
            var sheetno;
            var num1;
            var num2;
            radio11 = document.getElementById("ctl00_PrimaryContent_rad_all");
            radio12 = document.getElementById("ctl00_PrimaryContent_rad_mypart");

            radio21 = document.getElementById("ctl00_PrimaryContent_rad_quanbu");
            radio22 = document.getElementById("ctl00_PrimaryContent_rad_weitijiao");
            radio24 = document.getElementById("ctl00_PrimaryContent_rad_yiguanbi");
            radio25 = document.getElementById("ctl00_PrimaryContent_rad_weidaohuo");
            radio26 = document.getElementById("ctl00_PrimaryContent_rad_bfdaohuo");
            radio27 = document.getElementById("ctl00_PrimaryContent_rad_yidaohuo");


            if (radio11.checked == true) {
                num1 = 1;
            }
            else {
                num1 = 2;
            }

            if (radio21.checked == true) {
                num2 = 1;
            }
            else if (radio22.checked == true) {
                num2 = 2;
            }
            else if (radio24.checked == true) {
                num2 = 4;
            }
            else if (radio25.checked == true) {
                num2 = 5;
            }
            else if (radio26.checked == true) {
                num2 = 6;
            }
            else if (radio27.checked == true) {
                num2 = 7;
            }


            sheetno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            window.open("PC_TBPC_PurOrder.aspx?num1=" + num1 + "&num2=" + num2 + "&orderno=" + sheetno + "");
            //window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?mp_id="+sheetno;
        }

        //     self.moveTo(0,0)
        //    self.resizeTo(screen.availWidth,screen.availHeight)

        function Add_HTSP(orderid, ZJE) {
            var autonum = Math.round(10000 * Math.random());
            window.open("../Contract_Data/CM_ContractView_Add.aspx?Action=add&Type=1&autonum=" + autonum + "&orderid=" + orderid + "&ZJE=" + ZJE + "");
        }
        function Add_DDQK(orderid, ZJE, csinfo) {
            var autonum = Math.round(10000 * Math.random());
            window.open("../Contract_Data/CR_NotContractOrder_Add.aspx?Action=add&autonum=" + autonum + "&orderid=" + orderid + "&csinfo=" + csinfo + "&ZJE=" + ZJE + "");
        }
        function Add_DDFP(orderid, ZJE, csinfo) {
            var autonum = Math.round(10000 * Math.random());
            window.open("../Contract_Data/CR_OrderBill_Add.aspx?Action=add&autonum=" + autonum + "&orderid=" + orderid + "&csinfo=" + csinfo + "&ZJE=" + ZJE + "");
        }

        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
    
    </script>

    <script type="text/javascript">
        function seleAll() {
            $(".cpbox.xscroll :checkbox[checked='true']").click();
            if ($("#selectall").attr("checked")) {
                var s = $(".cpbox.xscroll :checkbox").length / 2;
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
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 4,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    for (var i = 1, j = this.sDataTable.tBodies[0].rows.length - 1; i < j; i++) {

                        this.sDataTable.tBodies[0].rows[i].getElementsByTagName("input")[0].onclick = this.sFDataTable.tBodies[0].rows[i].getElementsByTagName("input")[0].onclick = function(i) {
                            var clicked = false;

                            var dataRow = this.sDataTable.tBodies[0].rows[i];
                            var fixedRow = this.sFDataTable.tBodies[0].rows[i];
                            return function() {
                                if (clicked) {
                                    dataRow.style.backgroundColor = "";
                                    fixedRow.style.backgroundColor = "";
                                    clicked = false;
                                }
                                else {
                                    dataRow.style.backgroundColor = "LawnGreen";
                                    fixedRow.style.backgroundColor = "LawnGreen";
                                    clicked = true;
                                }
                            }
                        } .call(this, i);
                    }

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

        $(function() {
            sTable();
        });
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
                            <td align="right">
                                <asp:Button ID="btn_AddHTSP" runat="server" Text="添加采购合同" ForeColor="DodgerBlue"
                                    Font-Bold="true" OnClick="btn_AddHTSP_Click" />&nbsp;
                                <asp:Button ID="btn_AddDDQK" runat="server" Text="添加订单请款" ForeColor="Green" Font-Bold="true"
                                    OnClick="btn_AddDDQK_Click" Visible="false" />&nbsp;
                                <asp:Button ID="btn_AddDDFP" runat="server" Text="添加订单发票" ForeColor="LightSeaGreen"
                                    Font-Bold="true" OnClick="btn_AddDDFP_Click" Visible="false" />&nbsp;
                                <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />&nbsp;
                                <asp:Button ID="btn_PLdaochu" runat="server" Text="批量导出" OnClick="btn_PLdaochu_click" />&nbsp;
                                <asp:Button ID="btn_baojian" runat="server" Text="报检" OnClick="btn_baojian_Click" />&nbsp;
                                <asp:Button ID="btn_mianjian" runat="server" Text="免检" OnClick="btn_mianjian_Click"
                                    OnClientClick="return confirm('你确定提交吗?');" />&nbsp;
                                <asp:Button ID="btn_delete" runat="server" Text="删除" OnClick="btn_delete_Click" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td>
                                采购订单列表
                                <asp:RadioButton ID="rad_all" runat="server" Text="全部单据" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true" />
                                &nbsp;
                                <asp:RadioButton ID="rad_mypart" runat="server" Text="我的任务" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_mypart_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                            </td>
                            <td>
                                <asp:RadioButton ID="rad_quanbu" runat="server" Text="全部" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_quanbu_CheckedChanged" AutoPostBack="true" />
                                &nbsp;
                                <asp:RadioButton ID="rad_weitijiao" runat="server" Text="未提交" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_weitijiao_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_yiguanbi" runat="server" Text="已关闭" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_yiguanbi_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_weidaohuo" runat="server" Text="未入库" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_weidaohuo_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                                <asp:RadioButton ID="rad_yuqi_n" runat="server" Text="逾期未到货" TextAlign="Right" GroupName="select"
                                    Visible="false" OnCheckedChanged="rad_yuqi_n_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_bfdaohuo" runat="server" Text="部分到货" TextAlign="Right" GroupName="select"
                                    Visible="false" OnCheckedChanged="rad_bfdaohuo_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_yidaohuo" runat="server" Text="已到货" TextAlign="Right" GroupName="select"
                                    Visible="false" OnCheckedChanged="rad_yidaohuo_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_yuqi_y" runat="server" Text="逾期到货" TextAlign="Right" GroupName="select"
                                    Visible="false" OnCheckedChanged="rad_yuqi_y_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <%--<asp:RadioButton ID="rad_shanchu" runat="server" Text="已删除" TextAlign="Right" GroupName="select"
                                            OnCheckedChanged="rad_shanchu_CheckedChanged" AutoPostBack="true" />&nbsp; --%>
                            </td>
                            <td>
                                <%--数据范围：<asp:DropDownList ID="DropDownListrange" runat="server">
                                            <asp:ListItem Text="最近10000条数据" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="所有数据" Value="1"></asp:ListItem>
                                          </asp:DropDownList>&nbsp;&nbsp;&nbsp;--%>
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="QueryButton_Click">
                                    <asp:ListItem Text="50条记录" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100条记录" Value="100" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="150条记录" Value="150"></asp:ListItem>
                                    <asp:ListItem Text="200条记录" Value="200"></asp:ListItem>
                                    <asp:ListItem Text="300条记录" Value="300"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnShowPopup" runat="server" Text="筛选" OnClientClick="viewCondition()" />&nbsp;
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                    PopupControlID="PanelCondition" Drag="false" Enabled="True" DynamicServicePath=""
                                    CancelControlID="btnClose" Y="80">
                                </asp:ModalPopupExtender>
                                <input type="button" id="btn_filter" value="过滤" onclick="openmodewin()" />
                                <asp:TextBox ID="hid_filter" runat="server" Style="display: none"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="75%" Style="display: none">
                        <table width="95%" style="background-color: #CCCCFF; border: solid 1px black;">
                            <tr>
                                <td colspan="8" align="center">
                                    <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                                    &nbsp;&nbsp;&nbsp;
                                    <input type="button" id="btnClose" value="取消" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    单据编号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_orderno" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    供应商：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_supply" runat="server" OnTextChanged="tb_supply_Textchanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_supply"
                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetCusupinfo" FirstRowSelected="true"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td align="right">
                                    开始日期：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_StartTime" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_StartTime">
                                    </asp:CalendarExtender>
                                </td>
                                <td align="right">
                                    结束日期：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_EndTime" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                        ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_EndTime">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    名称：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    材质：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_cz" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    规格：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_gg" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    国标：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_gb" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    项目：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_pj" runat="server" OnTextChanged="tb_pj_Textchanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="tb_pj"
                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetPJNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td align="right">
                                    工程：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_eng" runat="server" OnTextChanged="tb_eng_Textchanged"></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="tb_eng"
                                        ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="20" MinimumPrefixLength="1"
                                        CompletionInterval="10" ServiceMethod="GetENGNAME" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                    </cc1:AutoCompleteExtender>
                                </td>
                                <td align="right">
                                    图号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_th" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    制单人：
                                </td>
                                <td>
                                    <asp:DropDownList ID="drp_stu" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    物料类型：
                                </td>
                                <td>
                                    
                                    <asp:TextBox ID="tb_mattype" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    质检结果：
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList3" runat="server">
                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                        <asp:ListItem Text="未报检" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="待检" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="报废" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="整改" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="待定" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="让步接收" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="部分合格" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="合格" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="免检" Value="8"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    计划跟踪号：
                                </td>
                                <td>
                                    <asp:TextBox ID="tb_ptc" runat="server"></asp:TextBox>
                                </td>
                                <td align="right">
                                    是否添加合同:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList4" runat="server">
                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                        <asp:ListItem Text="已添加" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="未添加" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr visible="false">
                                <%--<td align="right">
                                    订单请款:
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownList6" runat="server">
                                        <asp:ListItem Text="-请选择-" Value="%"></asp:ListItem>
                                        <asp:ListItem Text="已添加" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="未添加" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>--%>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-wrapper">
                <div class="box-outer">
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
                                            <td runat="server" visible="false">
                                                <strong>项目</strong>
                                            </td>
                                            <td>
                                                <strong>任务号</strong>
                                            </td>
                                            <td>
                                                <strong>采购合同号</strong>
                                            </td>
                                            <td>
                                                <strong>是否到货</strong>
                                            </td>
                                            <td>
                                                <strong>是否报检</strong>
                                            </td>
                                            <td>
                                                <strong>是否入库</strong>
                                            </td>
                                            <td>
                                                <strong>计划跟踪号</strong>
                                            </td>
                                            <td>
                                                <strong>部件名称</strong>
                                            </td>
                                            <td>
                                                <strong>部件图号</strong>
                                            </td>
                                            <td>
                                                <strong>国标</strong>
                                            </td>
                                            <td>
                                                <strong>图号</strong>
                                            </td>
                                            <td>
                                                <strong>名称</strong>
                                            </td>
                                            <td>
                                                <strong>规格</strong>
                                            </td>
                                            <td>
                                                <strong>材质</strong>
                                            </td>
                                            <td>
                                                <strong>长度</strong>
                                            </td>
                                            <td>
                                                <strong>宽度</strong>
                                            </td>
                                            <td>
                                                <strong>单位</strong>
                                            </td>
                                            <td>
                                                <strong>数量</strong>
                                            </td>
                                            <td>
                                                <strong>辅助单位</strong>
                                            </td>
                                            <td>
                                                <strong>辅助数量</strong>
                                            </td>
                                            <td>
                                                <strong>含税单价</strong>
                                            </td>
                                            <td>
                                                <strong>含税金额</strong>
                                            </td>
                                            <td>
                                                <strong>交货日期</strong>
                                            </td>
                                            <%--                                                <td>
                                                    <strong>备注</strong>
                                                </td>--%>
                                            <td>
                                                <strong>技术备注</strong>
                                            </td>
                                            <td>
                                                <strong>类型</strong>
                                            </td>
                                            <td>
                                                <strong>实际到货日期</strong>
                                            </td>
                                            <td>
                                                <strong>审核标志</strong>
                                            </td>
                                            
                                            <%--<td>
                                                <strong>订单请款单号</strong>
                                            </td>
                                            <td>
                                                <strong>订单发票</strong>
                                            </td>--%>
                                            <td>
                                                <strong>材料编码</strong>
                                            </td>
                                            <%--<td>
                                                <strong>销售合同号</strong>
                                            </td>--%>
                                            <%-- <td id="Td1" runat="server" visible="false">
                                                    <strong>执行数量</strong>
                                                </td>--%>
                                            <td id="Td2" runat="server">
                                                <strong>到货数量</strong>
                                            </td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr id="row" runat="server" ondblclick="redirectw(this)">
                                            <%--class='<%#(Container.ItemIndex%2==0)?"backwhite":"baseGadget"%>'--%>
                                            <%-- onmouseover="mover(this)" onmouseout="mout(this)"--%>
                                            <td id="td1" runat="server">
                                                <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server">
                                                </asp:CheckBox><%--onclick="checkme(this)"--%>
                                                <asp:Label ID="lbjiaji" runat="server" Text="加急" Visible="false"></asp:Label>
                                            </td>
                                            <td id="td2" runat="server">
                                                <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(DropDownList1.SelectedValue))%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_CODE" runat="server" Text='<%#Eval("orderno")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_ZDID" runat="server" Text='<%#Eval("zdrid")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="PO_ZDNM" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_SHTIME" runat="server" Text='<%#Eval("zdtime")==""?Eval("zdtime"):Convert.ToDateTime(Eval("zdtime")).ToString("yyyy-MM-dd")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="zje" runat="server" Text='<%#Eval("PO_ZJE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_SUPPLIERID" runat="server" Text='<%#Eval("supplierid")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="PO_SUPPLIERNM" runat="server" Text='<%#Eval("suppliernm")%>'></asp:Label>
                                            </td>
                                            <td runat="server" visible="false">
                                                <asp:Label ID="pjnm" runat="server" Text='<%#Eval("pjnm")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="engnm" runat="server" Visible="false" Text='<%#Eval("engnm")%>'></asp:Label>
                                                <asp:Label ID="engid" runat="server" Text='<%#Eval("engid")%>'></asp:Label>
                                            </td>
                                            <td id="tdHT">
                                                <asp:HyperLink ID="Hyphth" runat="server" Target="_blank" title="点击查看采购合同信息">
                                                    <asp:Label ID="hetonghao" runat="server"></asp:Label>
                                                </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label ID="daohuoF" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="Hypzlbj" runat="server" Target="_blank" title="点击查看报检信息">
                                                    <asp:Label ID="zlbj" runat="server" Text='<%#get_zlbj(Eval("PO_CGFS").ToString())%>'></asp:Label>
                                                </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Label ID="rukuF" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_ptcode" runat="server" Width="150px" Text='<%#Eval("ptcode")%>'
                                                    ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                    text-align: center" ToolTip='<%#Eval("ptcode")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="PO_CHILDENGNAME" Text='<%#Eval("PO_CHILDENGNAME") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_CHILDENGNAME" runat="server" Text='<%#Eval("PO_CHILDENGNAME")%>'
                                                    BorderStyle="None" Style="background-color: Transparent; text-align: center"
                                                    ForeColor="#1A438E" ToolTip='<%#Eval("PO_CHILDENGNAME")%>' Width="100px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="PO_MAP" Text='<%#Eval("PO_MAP") %>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_MAP" runat="server" Text='<%#Eval("PO_MAP")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("PO_MAP")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="margb" runat="server" Text='<%#Eval("margb")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_margb" runat="server" Text='<%#Eval("margb")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("margb")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_TUHAO" runat="server" Text='<%#Eval("PO_TUHAO")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_TUHAO" runat="server" Text='<%#Eval("PO_TUHAO")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("PO_TUHAO")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="marnm" runat="server" Text='<%#Eval("marnm")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_marnm" runat="server" Text='<%#Eval("marnm")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("marnm")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="margg" runat="server" Text='<%#Eval("margg")%>' Visible="false"></asp:Label>
                                                <asp:TextBox ID="txt_margg" runat="server" Text='<%#Eval("margg")%>' BorderStyle="None"
                                                    ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                    ToolTip='<%#Eval("margg")%>' Width="80px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="marcz" runat="server" Text='<%#Eval("marcz")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="length" runat="server" Text='<%#Eval("length")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="width" runat="server" Text='<%#Eval("width")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="marunit" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="zxnum" runat="server" Text='<%#Eval("zxnum")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_TECUNIT" runat="server" Text='<%#Eval("PO_TECUNIT")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="zxfznum" runat="server" Text='<%#Eval("zxfznum")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ctprice" runat="server" Text='<%#Eval("ctprice")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="ctamount" runat="server" Text='<%#Eval("ctamount")%>'></asp:Label>
                                            </td>
                                            <%-- <td>
                                                    <asp:Label ID="PO_SHID" runat="server" Text='<%#Eval("shrid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PO_SHNM" runat="server" Text='<%#Eval("shrnm")%>'></asp:Label>
                                                </td>--%>
                                            <td>
                                                <asp:Label ID="cgtimerq" runat="server" Text='<%#Eval("cgtimerq")%>'></asp:Label>
                                            </td>
                                            <%--<td>
                                                    <asp:Label ID="PO_gbbz" runat="server" Text='<%#get_ordlistgb_state(Eval("totalcstate").ToString())%>'></asp:Label>
                                                    <asp:Label ID="PO_CSTATE" runat="server" Text='<%#Eval("totalcstate")%>' Visible="false"></asp:Label>
                                                </td>--%>
                                            <%--<td>
                                                    <asp:Label ID="PO_NOTE" runat="server" Text='<%#Eval("totalnote")%>'></asp:Label>
                                                </td>--%>
                                            <td>
                                                <%--<asp:Label ID="detailnote" runat="server" Text='<%#Eval("detailnote")%>'></asp:Label>--%>
                                                <asp:TextBox ID="detailnote" runat="server" Width="150px" Text='<%#Eval("detailnote")%>'
                                                    ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                    text-align: center" ToolTip='<%#Eval("detailnote")%>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_MASHAPE" runat="server" Text='<%#Eval("PO_MASHAPE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="recdate" runat="server" Text='<%#Eval("recdate")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="PO_shbz" runat="server" Text='<%#get_ordlistsh_state(Eval("totalstate").ToString())%>'></asp:Label>
                                                <asp:Label ID="PO_STATE" runat="server" Text='<%#Eval("totalstate")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="PO_cstate" runat="server" Text='<%#Eval("detailcstate")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="detailstate" runat="server" Text='<%#Eval("detailstate")%>' Visible="false"></asp:Label>
                                            </td>
                                            
                                            <%--<td>
                                            </td>
                                            <td>
                                            </td>--%>
                                            <td>
                                                <asp:Label ID="marid" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                            </td>
                                            <%--<td>
                                                <asp:Label ID="salescontract" runat="server" Text='<%#Eval("salescontract")%>'></asp:Label>
                                            </td>--%>
                                            <%--<td runat="server" visible="false">
                                                    <asp:Label ID="zxnum" runat="server" Text='<%#Eval("zxnum")%>'></asp:Label>
                                                </td>--%>
                                            <td runat="server">
                                                <asp:Label ID="recgdnum" runat="server" Text='<%#Eval("recgdnum")%>'></asp:Label>
                                                
                                                <asp:Label ID="PO_IFFAST" runat="server" Text='<%#Eval("PO_IFFAST")%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="37" align="center">
                                        <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                            没有记录！</asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                        </div>
                    </div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <%-- <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                Checked="false" OnCheckedChanged="selectall_CheckedChanged" />--%>
                                <input type="checkbox" onclick="seleAll()" id="selectall" /><label for="selectall">&nbsp;全选</label>&nbsp;&nbsp;
                                <%-- <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                    <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />--%>
                                <input type="button" value="连选" onclick="seleLian()" />
                                <input type="button" value="取消" onclick="seleCancel()" />
                            </td>
                            <td>
                                筛选结果：共<asp:Label ID="lb_select_num" runat="server" Text=""></asp:Label>条记录&nbsp;&nbsp;&nbsp;
                                合计金额：<asp:Label ID="lb_select_money" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                            <td>
                                <span style="color: Red">注：红色代表超出交货日期而未到货，绿色代表已经添加合同审批</span>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
