<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="TBPC_IQRCMPPRCLST_checked.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.TBPC_IQRCMPPRCLST_checked"
    Title="比价单管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    询比价管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/pricesearch.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function redirectw(obj) {
            var sheetno;
            var num1;
            var num2;
            radio11 = document.getElementById("ctl00_PrimaryContent_rad_all");
            radio12 = document.getElementById("ctl00_PrimaryContent_rad_mypart");
            radio21 = document.getElementById("ctl00_PrimaryContent_rad_quanbu");
            radio22 = document.getElementById("ctl00_PrimaryContent_rad_weitijiao");
            radio23 = document.getElementById("ctl00_PrimaryContent_rad_shenhezhong");
            radio24 = document.getElementById("ctl00_PrimaryContent_rad_bohui");
            radio25 = document.getElementById("ctl00_PrimaryContent_rad_tongguo");
            radio28 = document.getElementById("ctl00_PrimaryContent_rad_tongguodingdan");
            radio26 = document.getElementById("ctl00_PrimaryContent_rad_weizxing");
            radio27 = document.getElementById("ctl00_PrimaryContent_rad_yizhixing");
            
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
            else if (radio23.checked == true) {
                num2 = 3;
            }
            else if (radio24.checked == true) {
                num2 = 4;
            }
            else if (radio25.checked == true) {
                num2 = 5;
            }
            else if (radio28.checked == true) {
                num2 = 8;
            }
            else if (radio26.checked == true) {
                num2 = 6;
            }
            else if (radio27.checked == true) {
                num2 = 7;
            }
            
            sheetno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            //        window.location.href="TBPC_IQRCMPPRCLST_checked_detail.aspx?num1="+num1+"&num2="+num2+"&sheetno="+escape(sheetno);
            window.open("TBPC_IQRCMPPRCLST_checked_detail.aspx?num1=" + num1 + "&num2=" + num2 + "&sheetno=" + escape(sheetno) + "");
        }

        function mowinopen(ptcode_rcode) {
            var autonum = Math.round(10000 * Math.random());
            window.open('PC_Data_addto_list.aspx?autonum=' + autonum + '&ptcode_rcode=' + escape(ptcode_rcode) + '', '', "dialogHeight:460px;dialogWidth:780px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "TBPC_IQRCMPPRCLST_checked.aspx";
        }
        //        self.moveTo(0, 0)
        //        self.resizeTo(screen.availWidth, screen.availHeight)
    </script>

    <script type="text/javascript">
        function PLdaochu() {
            //        window.showModalDialog('PC_TBPC_PLdaochu.aspx','',"dialogHeight:300px;dialogWidth:500px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.open("PC_Data_BJDdaochu.aspx");
            //        window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?shape="+escape(shape)+"&mp_id="+escape(mpid)+"";
        }
    </script>

    <script type="text/javascript">
        function openmodewin() {
            var num1;
            var num2;
            radio11 = document.getElementById("ctl00_PrimaryContent_rad_all");
            radio12 = document.getElementById("ctl00_PrimaryContent_rad_mypart");
            radio21 = document.getElementById("ctl00_PrimaryContent_rad_quanbu");
            radio22 = document.getElementById("ctl00_PrimaryContent_rad_weitijiao");
            radio23 = document.getElementById("ctl00_PrimaryContent_rad_shenhezhong");
            radio24 = document.getElementById("ctl00_PrimaryContent_rad_bohui");
            radio25 = document.getElementById("ctl00_PrimaryContent_rad_tongguo");
            radio28 = document.getElementById("ctl00_PrimaryContent_rad_tongguodingdan");
            radio26 = document.getElementById("ctl00_PrimaryContent_rad_weizxing");
            radio27 = document.getElementById("ctl00_PrimaryContent_rad_yizhixing");
            
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
            else if (radio23.checked == true) {
                num2 = 3;
            }
            else if (radio24.checked == true) {
                num2 = 4;
            }
            else if (radio25.checked == true) {
                num2 = 5;
            }
            else if (radio28.checked == true) {
                num2 = 8;
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
            window.showModalDialog('PC_TBPC_Itemsfilter.aspx?autonum=' + autonum + '&tablenmid_code=' + tablenmid_code + '', '', "dialogHeight:400px;dialogWidth:640px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "TBPC_IQRCMPPRCLST_checked.aspx?sf=1&num1=" + num1 + "&num2=" + num2 + "";
            //window.open("TBPC_IQRCMPPRCLST_checked.aspx?sf=1&num1=" + num1 + "&num2=" + num2 + "");
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
                            var n = 0;
                            if (document.getElementById('<%=Chk_HZ.ClientID %>').checked) {
                                n = 3
                            }
                            else {
                                n = 4;
                            }
                            for (var k = 2; k <= n; k++) {
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
            font-size: small;
        }
        .autocomplete_listItem
        {
            border-style: solid;
            border: #FFEFDB;
            border-width: 1px;
            background-color: #EEDC82;
            color: windowtext;
            font-size: small;
        }
        .autocomplete_highlightedListItem
        {
            background-color: #1C86EE;
            color: black;
            padding: 1px;
            font-size: small;
        }
        .backwhite
        {
            background-color: White;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <div class="RightContent">
       <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btxjddc" runat="server" Text="询价单导出" OnClick="btn_daochu_xjdclick" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />&nbsp;
                                <asp:Button ID="btn_daochu_all" runat="server" Text="批量导出" OnClick="btn_daochu_all_Click" />&nbsp;
                                <input id="btn_PLdaochu" type="hidden"  value="批量导出" onclick="PLdaochu()" />&nbsp;
                                 &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_xiatui" runat="server" Text="生成订单" OnClick="btn_xiatui_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_add" runat="server" Text="追加订单" OnClick="btn_add_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_Click" />&nbsp;&nbsp;
                                <asp:Button ID="btn_delete" runat="server" Text="删除" OnClick="btn_delete_Click" Visible="false" />&nbsp;&nbsp;
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
                                询比价清单列表
                                <asp:RadioButton ID="rad_all" runat="server" Text="全部单据" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_mypart" runat="server" Text="我的任务" TextAlign="Right" GroupName="select00"
                                    OnCheckedChanged="rad_mypart_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                                <asp:TextBox ID="Tb_marrepcode" runat="server" Text="" Visible="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="Chk_HZ" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_HZ_CheckedChanged" />
                                <label for="ctl00_PrimaryContent_Chk_HZ">
                                    汇&nbsp;&nbsp;总</label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rad_quanbu" runat="server" Text="全部" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_quanbu_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_weitijiao" runat="server" Text="未审核" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_weitijiao_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_shenhezhong" runat="server" Text="审核中" TextAlign="Right"
                                    GroupName="select" OnCheckedChanged="rad_shenhezhong_CheckedChanged" AutoPostBack="true"
                                    Checked="true" /><asp:Label runat="server" ID="WarnNum" ForeColor="Red"></asp:Label>&nbsp;
                                <asp:RadioButton ID="rad_bohui" runat="server" Text="已驳回" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_bohui_CheckedChanged" AutoPostBack="true" />
                                <asp:Label ID="lb_bjdbh" runat="server" Text="" ForeColor="Red"></asp:Label>
                                <asp:RadioButton ID="rad_tongguo" runat="server" Text="已通过" TextAlign="Right" GroupName="select"
                                    OnCheckedChanged="rad_tongguo_CheckedChanged" AutoPostBack="true" />
                                <asp:Label ID="lb_bjdtg" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
                                <asp:RadioButton ID="rad_tongguodingdan" runat="server" Text="通过未下订单" TextAlign="Right"
                                    GroupName="select" OnCheckedChanged="rad_tongguo_CheckedChanged" AutoPostBack="true" />&nbsp;
                                    
                                <asp:RadioButton ID="rad_weizxing" runat="server" Text="未执行" TextAlign="Right" GroupName="select"
                                    Visible="false" OnCheckedChanged="rad_weizxing_CheckedChanged" AutoPostBack="true" />&nbsp;
                                <asp:RadioButton ID="rad_yizhixing" runat="server" Text="已执行" TextAlign="Right" GroupName="select"
                                    Visible="false" OnCheckedChanged="rad_yizhixing_CheckedChanged" AutoPostBack="true" />&nbsp;
                            </td>
                            <td>
                                <asp:CheckBox ID="chk_zhaobiao" runat="server" Text="招标物料" AutoPostBack="true" OnCheckedChanged="chk_zhaobiao_CheckedChanged" />
                                <asp:CheckBox ID="cb_sp" runat="server" Visible="false" Checked="false" AutoPostBack="true"
                                    Text="只显示我的未审" ForeColor="Red" OnCheckedChanged="CBSP_CheckedChanged" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                询比价单号：
                            </td>
                            <td>
                                <asp:TextBox ID="Tb_pcode" runat="server" Text="" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                计划跟踪号：
                            </td>
                            <td>
                                <asp:TextBox ID="tb_ptc" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                物料名称：
                            </td>
                            <td>
                                <asp:TextBox ID="tb_marnm" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                采购员：
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_stu" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_click" />&nbsp;
                                &nbsp;
                                <%--<asp:Button ID="btn_filter" runat="server" Text="过滤" OnClientClick="openmodewin()" />--%>
                                <input type="button" id="btn_filter" value="过滤" onclick="openmodewin()" />
                                <asp:TextBox ID="hid_filter" runat="server" Style="display: none"></asp:TextBox>
                                <%--<input id="btn_hidden" type="button" value="隐藏" onclick="hiddenrow(1,9,2)" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                供应商：
                            </td>
                            <td>
                                <asp:TextBox ID="tb_Gongyingshang" runat="server" Width="180px"></asp:TextBox>
                                <%-- <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_Gongyingshang"
                                    ServicePath="PC_Data_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="2"
                                    CompletionInterval="10" ServiceMethod="GetCusupinfo" FirstRowSelected="true"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                </cc1:AutoCompleteExtender>--%>
                            </td>
                            <td colspan="4">
                                &nbsp;&nbsp;开始时间：&nbsp;&nbsp;
                                <asp:TextBox ID="tb_StartTime" runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                    ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_StartTime">
                                </asp:CalendarExtender>&nbsp;&nbsp;
                                结束时间：&nbsp;&nbsp;
                                <asp:TextBox ID="tb_EndTime" runat="server" Width="80px"></asp:TextBox>
                                <asp:CalendarExtender DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy年MM月dd日"
                                    ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="tb_EndTime">
                                </asp:CalendarExtender>
                                物料类型：&nbsp;&nbsp;
                                <asp:TextBox ID="tbwltype" runat="server" Width="60px"></asp:TextBox>&nbsp;&nbsp;
                            </td>
                            <td>
                                每页显示：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_click">
                                    <asp:ListItem Text="50条记录" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100条记录" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="150条记录" Value="150"></asp:ListItem>
                                    <asp:ListItem Text="200条记录" Value="200" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="250条记录" Value="250"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btn_clear" runat="server" Text="重置搜索条件" OnClick="btn_clear_click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="height: 470px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center">
                                    <asp:Repeater ID="checked_list_Repeater" runat="server" OnItemDataBound="checked_list_Repeater_ItemDataBound">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>行<br />
                                                        号</strong>
                                                </td>
                                                <td runat="server" id="dj">
                                                    <strong>单据编号</strong>
                                                </td>
                                                <td runat="server" id="rq">
                                                    <strong>制单日期</strong>
                                                </td>
                                                <td>
                                                    <strong>总金额</strong>
                                                </td>
                                                <td>
                                                    <strong>供应商</strong>
                                                </td>
                                                <td runat="server" id="bm" visible="false">
                                                    <strong>部门<br />
                                                        负责人</strong>
                                                </td>
                                                <td runat="server" id="zd">
                                                    <strong>制单人</strong>
                                                </td>
                                                <td runat="server" id="zg" visible="false">
                                                    <strong>主管<br />
                                                        经理</strong>
                                                </td>
                                                <td>
                                                    <strong>审核<br />
                                                        标志</strong>
                                                </td>
                                                <td runat="server" id="dd">
                                                    <strong>是否生<br />
                                                        成订单</strong>
                                                </td>
                                                <td id="jh" runat="server">
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
                                                    <strong>类型</strong>
                                                </td>
                                                <td>
                                                    <strong>最终报价<br />
                                                        (含税)</strong>
                                                </td>
                                                <td runat="server" id="zx">
                                                    <strong>执行人</strong>
                                                </td>
                                                <td>
                                                    <strong>申请人</strong>
                                                </td>
                                                <td id="td1" runat="server" visible="false">
                                                    <strong>订单编号</strong>
                                                </td>
                                                <td runat="server" id="hg">
                                                    <strong>行关闭<br />
                                                        标志</strong>
                                                </td>
                                                <td runat="server" id="bz">
                                                    <strong>备注</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>关闭<br />
                                                        标志</strong>
                                                </td>
                                                <td runat="server" id="zdj" visible="false">
                                                    <strong>铸锻件<br />
                                                        单价</strong>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <strong>铸锻件<br />
                                                        吨数</strong>
                                                </td>
                                                <td id="Td2" runat="server" visible="false">
                                                    <strong>销售合同号</strong>
                                                </td>
                                                <td>
                                                    <strong>物料编码</strong>
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" ondblclick="redirectw(this)">
                                                <%--class='<%#(Container.ItemIndex%2==0)?"backwhite":"baseGadget"%>'--%>
                                                <%--onclick="rowclick(this)"  onmouseover="mover(this)" onmouseout="mout(this)"--%>
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false"></asp:CheckBox>&nbsp;<%-- onclick="checkme(this)"--%>
                                                    <asp:Label ID="lbjiaji" runat="server" Text="加急" Visible="false" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(DropDownList5.SelectedValue))%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="djr">
                                                    <asp:Label ID="sheetno" runat="server" Text='<%#Eval("picno")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="rqr">
                                                    <asp:Label ID="zdtime" runat="server" Text='<%#Eval("irqdata")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="amount" runat="server" Text='<%#Eval("iclamount")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="supplierid" runat="server" Text='<%#Eval("supplierresid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="bmr" visible="false">
                                                    <asp:Label ID="bmfzrid" runat="server" Text='<%#Eval("iclfzrid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="bmfzrnm" runat="server" Text='<%#Eval("iclfzrnm")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="zdr">
                                                    <asp:Label ID="zdid" runat="server" Text='<%#Eval("zdrid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="zdnm" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="zgr" visible="false">
                                                    <asp:Label ID="zgid" runat="server" Text='<%#Eval("iclzgid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="zgnm" runat="server" Text='<%#Eval("iclzgnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="shstate" runat="server" Text='<%#get_pur_bjdsh(Eval("totalstate").ToString())%>'></asp:Label>
                                                    <asp:Label ID="totalstate" runat="server" Text='<%#Eval("totalstate")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td runat="server" id="ddr">
                                                    <asp:HyperLink ID="Hypdd" runat="server" Target="_blank">
                                                        <asp:Label ID="ddstatetext" runat="server" Text='<%#get_pur_dd(Eval("detailstate").ToString())%>'></asp:Label></asp:HyperLink>
                                                    <asp:Label ID="detailstate" runat="server" Text='<%#Eval("detailstate")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td runat="server" id="jhr">
                                                    <asp:Label ID="ptcode" runat="server" Text='<%#Eval("ptcode")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PIC_ID" runat="server" Text='<%#Eval("PIC_ID")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_ptcode" runat="server" Text='<%#Eval("ptcode")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("ptcode")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_CHILDENGNAME" runat="server" Text='<%#Eval("PIC_CHILDENGNAME")%>'
                                                        Visible="false"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txt_Child" Text='<%#Eval("PIC_CHILDENGNAME")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("PIC_CHILDENGNAME")%>' Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_MAP" runat="server" Text='<%#Eval("PIC_MAP")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_MAP" runat="server" Text='<%#Eval("PIC_MAP")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="100px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("PIC_MAP")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="margb" runat="server" Text='<%#Eval("margb")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_margb" runat="server" Text='<%#Eval("margb")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="100px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("margb")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_TUHAO" runat="server" Text='<%#Eval("PIC_TUHAO")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="100px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("PIC_TUHAO")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="marnm" runat="server" Text='<%#Eval("marnm")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_marnm" runat="server" Text='<%#Eval("marnm")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="100px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("marnm")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="margg" runat="server" Text='<%#Eval("margg")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_margg" runat="server" Text='<%#Eval("margg")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="100px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("margg")%>'></asp:TextBox>
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
                                                    <asp:Label ID="unit" runat="server" Text='<%#Eval("marunit")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="num" runat="server" Text='<%#Eval("marnum")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="marfzunit" runat="server" Text='<%#Eval("marfzunit")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="marfznum" runat="server" Text='<%#Eval("marfznum")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PIC_MASHAPE" runat="server" Text='<%#Eval("PIC_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lasprice" runat="server" Text='<%#Eval("price")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="zxr">
                                                    <asp:Label ID="zxid" runat="server" Text='<%#Eval("zxrid")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="zxnm" runat="server" Text='<%#Eval("zxrnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("sqrnm")%>'></asp:Label>
                                                </td>
                                                <td id="td1" runat="server" visible="false">
                                                    <asp:Label ID="orderno" runat="server" Text='<%#Eval("orderno")%>'></asp:Label>
                                                </td>
                                                <td runat="server" id="hgr">
                                                    <asp:Label ID="hgbstatetext" runat="server" Text='<%#get_pur_hgb(Eval("detailcstate").ToString())%>'></asp:Label>
                                                    <asp:Label ID="hgbstate" runat="server" Text='<%#Eval("detailcstate")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td runat="server" id="bzr">
                                                    <asp:Label ID="note" runat="server" Text='<%#Eval("detailnote")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txt_note" Text='<%#Eval("detailnote")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="120px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("detailnote")%>'></asp:TextBox>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="gbstatetext" runat="server" Text='<%#get_pur_bjdgb(Eval("totalcstate").ToString())%>'></asp:Label>
                                                    <asp:Label ID="gbstate" runat="server" Text='<%#Eval("totalcstate")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td runat="server" id="zdjr" visible="false">
                                                    <asp:Label ID="zdjprice" runat="server" Text='<%#Eval("zdjprice")%>'></asp:Label>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="zdjnumt" runat="server" Text='<%#Eval("zdjnum")%>'></asp:Label>
                                                </td>
                                                <td runat="server" visible="false">
                                                    <asp:Label ID="salescontract" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="marid" runat="server" Text='<%#Eval("marid")%>'></asp:Label>
                                                    
                                                    <asp:Label ID="PIC_IFFAST" runat="server" Text='<%#Eval("PIC_IFFAST")%>' Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="34">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有记录！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td width="50%">
                                            共查出<asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>批比价单/
                                            <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red"></asp:Label>条记录
                                        </td>
                                        <td align="right" width="50%">
                                            <asp:UCPaging ID="UCPaging1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Panel ID="Pan_select" runat="server">
                    <%-- <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                Checked="false" OnCheckedChanged="selectall_CheckedChanged" />--%>
                    <input type="checkbox" onclick="seleAll()" id="selectall" /><label for="selectall">&nbsp;全选</label>&nbsp;&nbsp;
                    <%-- <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                    <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />--%>
                    <input type="button" value="连选" onclick="seleLian()" />
                    <input type="button" value="取消" onclick="seleCancel()" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
