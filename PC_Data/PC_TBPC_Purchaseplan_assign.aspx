<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="PC_TBPC_Purchaseplan_assign.aspx.cs" Inherits="ZCZJ_DPF.PC_Data.PC_TBPC_Purchaseplan_assign"
    Title="采购计划管理" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    采购任务
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="../JS/KeyControl.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function document.onreadystatechange() {
            if (document.readyState == "complete") {
                var ptc = "<%=gloabptc%>";
                if (ptc != "") {
                    var col = 5;
                    othptcfind(ptc, col);
                }
            }
        }
        function redirectw(obj) {
            var sheetno;
            sheetno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            //window.location.href="PC_TBPC_Purchaseplan_assign_detail.aspx?sheetno="+sheetno;
            window.open("PC_TBPC_Purchaseplan_detail.aspx?mp_id=" + escape(sheetno));
        }




        function viewCondition() {
            document.getElementById("<%=PanelCondition.ClientID%>").style.display = 'block';
        }
        //     self.moveTo(0,0)
        //    self.resizeTo(screen.availWidth,screen.availHeight)
    </script>

    <script type="text/javascript">
        function PLdaochu() {
            window.open("PC_TBPC_CGJHdaochu.aspx");
        }
        function mowinopen(ptcode) {
            var autonum = Math.round(10000 * Math.random());
            window.open('PC_Data_zhuijiaBJD.aspx?autonum=' + autonum + '&ptcode=' + escape(ptcode) + '', '', "dialogHeight:460px;dialogWidth:780px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PC_TBPC_Purchaseplan_assign.aspx";
        }
    </script>

    <script type="text/javascript">
        function openmodewin() {
            var num1;
            var num2;
            radio11 = document.getElementById("ctl00_PrimaryContent_rad_all");
            radio12 = document.getElementById("ctl00_PrimaryContent_rad_mypart");

            radio21 = document.getElementById("ctl00_PrimaryContent_rad_stall");
            radio22 = document.getElementById("ctl00_PrimaryContent_rad_stwzx");
            radio23 = document.getElementById("ctl00_PrimaryContent_rad_stxbj");

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

            var tablenmid_code;
            var autonum = Math.round(10000 * Math.random());
            tablenmid_code = document.getElementById("<%=hid_filter.ClientID %>").value;
            window.showModalDialog('PC_TBPC_Itemsfilter.aspx?autonum=' + autonum + '&tablenmid_code=' + tablenmid_code + '', '', "dialogHeight:460px;dialogWidth:680px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            window.location.href = "PC_TBPC_Purchaseplan_assign.aspx?sf=1&num1=" + num1 + "&num2=" + num2 + "";
        }
    </script>

    <%--    招标物料处理
--%>

    <script type="text/javascript">
        function Add_HT(guige, guobiao, pcode, mid) {
            window.showModalDialog("PC_Date_zbqd.aspx?register=" + escape(guige) + "&guobiao=" + escape(guobiao) + "&pc=" + escape(pcode) + "&mid=" + escape(mid), '', "dialogHeight:500px;dialogWidth:700px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");

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
            background-color: #FFFFF0;
        }
    </style>

    <script language="JavaScript" type="text/javascript">
        function xsuse(obj) {
            var marnm;
            var marcz;
            var ptcode;
            var marid;
            ptcode = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            marid = obj.getElementsByTagName("td")[4].getElementsByTagName("span")[0].innerHTML;
            marnm = obj.getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML;
            marcz = obj.getElementsByTagName("td")[7].getElementsByTagName("span")[0].innerHTML;
            windowopen("PC_Date_silimarmarshow.aspx?ptcode=" + escape(ptcode) + "&marid=" + marid + "&marnm=" + marnm + "&marcz=" + escape(marcz));
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
        
        
        function viewCondition() {
            document.getElementById("<%=PanelCondition2.ClientID%>").style.display = 'block';
        }
    </script>

    <script language="javascript" type="text/javascript">
        function sTable() {
            var myST = new superTable("tab", {
                cssSkin: "tDefault",
                headerRows: 1,
                fixedCols: 3,
                //	        colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
                onStart: function() {
                    //		        this.start = new Date();
                },
                onFinish: function() {
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
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
                }
            });
        }

        $(function() {
            sTable();
        });
    </script>

    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_PLdaochu" runat="server" Text="批量导出" OnClientClick="PLdaochu()" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_yc" runat="server" OnClientClick="viewCondition()" Text="隐藏"
                                    Visible="false" />
                                <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btn_yc"
                                    PopupControlID="PanelCondition" Drag="false" Y="80">
                                </asp:ModalPopupExtender>
                                <asp:Button ID="btn_cxkc" runat="server" Text="库存查询" OnClick="btn_cxkc_Click" />
                                <asp:Button ID="btn_hclose" runat="server" Text="行关闭" OnClick="btn_hclose_Click" />
                                <asp:Button ID="btn_marrep" runat="server" Text="代用" OnClick="btn_marrep_Click" />
                                <asp:Button ID="btn_Iqrcmpprc" runat="server" Text="询比价" OnClick="btn_Iqrcmpprc_Click" />
                                <asp:Button ID="btn_zhuijia" runat="server" Text="追加比价单" OnClick="btn_zhuijia_Click" />
                                <asp:Button ID="btn_back" runat="server" Text="返回" OnClick="btn_back_Click" Visible="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button runat="server" ID="CancelFenG" Text="取消分工" OnClientClick="viewCondition()" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="CancelFenG"
                                            PopupControlID="UpdatePanelCondition" Drag="true" Enabled="True" DynamicServicePath=""
                                            Y="100" X="900">
                            </asp:ModalPopupExtender>
                            &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="PanelCondition" runat="server" Width="60%" Style="display: none">
                        <table width="60%" style="background-color: #CCCCFF; border: solid 1px black;" border="1">
                            <tr>
                                <td colspan="8" align="center">
                                    选择需要隐藏的列
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox1" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="批号" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox2" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="计划号" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox3" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="项目" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox4" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="设备" TextAlign="Right"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox5" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="图号" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox6" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="物料编码" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox7" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="材质" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox8" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="国标" TextAlign="Right"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox9" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="分工人" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox10" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="分工日期" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox11" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="采购员" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox12" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="申请人" TextAlign="Right"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="CheckBox13" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="长度" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox14" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="宽度" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox15" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="执行人" TextAlign="Right"></asp:CheckBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBox16" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                        Text="执行日期" TextAlign="Right"></asp:CheckBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8" align="center">
                                    <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="确定" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td>
                            采购任务列表:
                            <asp:RadioButton ID="rad_all" runat="server" Text="全部单据" TextAlign="Right" GroupName="select00"
                                OnCheckedChanged="rad_all_CheckedChanged" AutoPostBack="true" />
                            &nbsp;
                            <asp:RadioButton ID="rad_mypart" runat="server" Text="我的任务" TextAlign="Right" GroupName="select00"
                                OnCheckedChanged="rad_mypart_CheckedChanged" AutoPostBack="true" Checked="true" />
                            &nbsp;
                            <asp:TextBox ID="Tb_marrepcode" runat="server" Text="" Visible="false"></asp:TextBox>
                        </td>
                        <td align="left">
                            状态:
                            <asp:RadioButton ID="rad_stall" runat="server" Text="全部" TextAlign="Right" GroupName="select11"
                                OnCheckedChanged="rad_stall_CheckedChanged" AutoPostBack="true" />
                            <asp:RadioButton ID="rad_allwzx" runat="server" Text="全部未执行" TextAlign="Right" GroupName="select11"
                                OnCheckedChanged="rad_allwzx_CheckedChanged" AutoPostBack="true" Checked="true" />
                            <asp:RadioButton ID="rad_stwzx" runat="server" Text="非招标未执行" TextAlign="Right" GroupName="select11"
                                OnCheckedChanged="rad_stwzx_CheckedChanged" AutoPostBack="true" />
                            <asp:RadioButton ID="rad_stxbj" runat="server" Text="询比价" TextAlign="Right" GroupName="select11"
                                OnCheckedChanged="rad_stxbj_CheckedChanged" AutoPostBack="true" />
                            <asp:RadioButton ID="rad_stxdd" runat="server" Text="下订单" TextAlign="Right" GroupName="select11"
                                Visible="false" OnCheckedChanged="rad_stxdd_CheckedChanged" AutoPostBack="true" />
                            <asp:RadioButton ID="rad_szbwzx" runat="server" Text="未执行招标物料" TextAlign="Right"
                                Visible="false" GroupName="select11" OnCheckedChanged="rad_szbwzx_CheckedChanged"
                                AutoPostBack="true" />
                        </td>
                        <td align="left">
                                采购员：<asp:DropDownList ID="drp_stu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_click">
                                </asp:DropDownList>
                                每页：
                                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_click">
                                    <asp:ListItem Text="50条" Value="50"></asp:ListItem>
                                    <asp:ListItem Text="100条" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="150条" Value="150"></asp:ListItem>
                                    <asp:ListItem Text="200条" Value="200" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="250条" Value="250"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                    </tr>
                </table>
                <asp:Panel ID="PanelCondition2" runat="server" Width="280px" Style="display: none">
                        <asp:UpdatePanel ID="UpdatePanelCondition" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="280px" style="background-color: #CCCCFF; border: solid 1px black;">
                                    <tr>
                                        <td colspan="8" align="center">
                                            <asp:Button ID="cancelquery" runat="server" Text="确认" OnClick="CancelFenG_Click"
                        OnClientClick="Javascript:return confirm('确定取消,执行此操作将取消任务分工?');" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnquixao" runat="server" OnClick="btnquixao_Click" Text="取消" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            理由：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbreason" runat="server" Width="220px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                 </asp:Panel>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                计划跟踪号:
                                <asp:TextBox ID="Tb_ENGNAME" runat="server"></asp:TextBox>
                                类型:
                                <asp:TextBox ID="tbtype" Width="60px" runat="server"></asp:TextBox>
                                批号:
                                <asp:TextBox ID="Tb_pcode" runat="server" Text=""></asp:TextBox>
                                部件名称：<asp:TextBox ID="tb_bjname" runat="server"></asp:TextBox>
                                物料名称：<asp:TextBox ID="tb_marnm" Width="60px" runat="server"></asp:TextBox>
                                物料规格：<asp:TextBox ID="tb_ptc" runat="server" Width="60px"></asp:TextBox>
                                <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_click" />
                                <%--<asp:Button ID="btn_filter" runat="server" Text="过滤" OnClientClick="openmodewin()" />--%>
                                <input type="button" id="btn_filter" value="过滤" onclick="openmodewin()" />
                                <asp:Button ID="btn_zb" runat="server" Text="招标物料" OnClick="btn_zb_Click" Visible="false" />
                                <asp:Button ID="btn_clear" runat="server" Text="重置查询条件" OnClick="btn_clear_click" />
                                <asp:TextBox ID="hid_filter" runat="server" Style="display: none"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="height: 470px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab" width="100%" align="center" class="nowrap cptable fullwidth" border="1">
                                    <asp:Repeater ID="tbpc_purshaseplanassignRepeater" runat="server" OnItemDataBound="tbpc_purshaseplanassignRepeaterbind">
                                        <HeaderTemplate>
                                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE;">
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>行号</strong>
                                                </td>
                                                <td id="jhh" runat="server">
                                                    <strong>计划跟踪号</strong>
                                                </td>
                                                <td id="xm" runat="server" visible="false">
                                                    <strong>项目</strong>
                                                </td>
                                                <td>
                                                    <strong>部件名称</strong>
                                                </td>
                                                <td id="th" runat="server">
                                                    <strong>部件图号</strong>
                                                </td>
                                                <td id="gb" runat="server">
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
                                                <td id="cz" runat="server">
                                                    <strong>材质</strong>
                                                </td>
                                                <td id="cd" runat="server">
                                                    <strong>长度(mm)</strong>
                                                </td>
                                                <td id="kd" runat="server">
                                                    <strong>宽度(mm)</strong>
                                                </td>
                                                <td>
                                                    <strong>单位</strong>
                                                </td>
                                                <td>
                                                    <strong>采购数量</strong>
                                                </td>
                                                <td>
                                                    <strong>辅助单位</strong>
                                                </td>
                                                <td>
                                                    <strong>采购辅助数量</strong>
                                                </td>
                                                <td>
                                                    <strong>上查计划</strong>
                                                </td>
                                                <td>
                                                    <strong>备注</strong>
                                                </td>
                                                <td>
                                                    <strong>类型</strong>
                                                </td>
                                                <td id="fgr" runat="server">
                                                    <strong>分工人</strong>
                                                </td>
                                                <td id="fgrq" runat="server">
                                                    <strong>分工日期</strong>
                                                </td>
                                                <td id="cgy" runat="server">
                                                    <strong>采购员</strong>
                                                </td>
                                                <td id="sqr" runat="server">
                                                    <strong>申请人</strong>
                                                </td>
                                                <td id="clbm" runat="server">
                                                    <strong>材料编码</strong>
                                                </td>
                                                <td id="ph" runat="server">
                                                    <strong>批号</strong>
                                                </td>
                                                <%--<td runat="server" visible="false">
                                                    <strong>查看变更</strong>
                                                </td>--%>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" ondblclick="redirectw(this)" onclick="MouseClick1(this)">
                                                <%--class='<%#(Container.ItemIndex%2==0)?"backwhite":"baseGadget"%>'--%>
                                                <%--onmouseover="mover(this)"
                                                onmouseout="mout(this)"--%>
                                                <td runat="server" id="ch">
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false"></asp:CheckBox>&nbsp;<%--onclick="checkme(this)"--%>
                                                    <asp:Label ID="PUR_ID" runat="server" Text='<%#Eval("PUR_ID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="sqrid" runat="server" Text='<%#Eval("sqrid")%>' Visible="false"> </asp:Label>
                                                    <asp:Label ID="PUR_ENGID" runat="server" Text='<%#Eval("PUR_ENGID")%>' Visible="false">
                                                    </asp:Label>
                                                    <asp:Label ID="PUR_STATE" runat="server" Text='<%#Eval("PUR_STATE")%>' Visible="false"></asp:Label>
                                                     <asp:Label ID="PUR_KEYCOMS" runat="server" Text='<%#Eval("PUR_KEYCOMS")%>' Visible="false"></asp:Label>
                                                     
                                                     <asp:Label ID="lbjiaji" runat="server" Text="加急" Visible="false" ForeColor="Red"></asp:Label>
                                                </td>
                                                <td runat="server" id="ch1">
                                                    <asp:Label ID="ROWSNUM" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(DropDownList5.SelectedValue))%>'></asp:Label>
                                                </td>
                                                <td id="jhh1" runat="server">
                                                    <asp:Label ID="PUR_PTCODE" runat="server" Text='<%#Eval("PUR_PTCODE")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_PUR" runat="server" Width="200px" Text='<%#Eval("PUR_PTCODE")%>'
                                                        ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                        text-align: center" ToolTip='<%#Eval("PUR_PTCODE")%>'></asp:TextBox>
                                                </td>
                                                <td id="xm1" runat="server" visible="false">
                                                    <asp:Label ID="PUR_PJID" runat="server" Text='<%#Eval("PUR_PJID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="PUR_PJNAME" runat="server" Text='<%#Eval("PUR_PJNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PR_CHILDENGNAME" runat="server" Text='<%#Eval("PR_CHILDENGNAME")%>'
                                                        Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_CHILDENGNAME" runat="server" Text='<%#Eval("PR_CHILDENGNAME")%>'
                                                        ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                        text-align: center" ToolTip='<%#Eval("PR_CHILDENGNAME")%>'></asp:TextBox>
                                                </td>
                                                <td id="th1" runat="server">
                                                    <asp:Label ID="PR_MAP" runat="server" Text='<%#Eval("PR_MAP")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_MAP" runat="server" Width="100px" Text='<%#Eval("PR_MAP")%>'
                                                        ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                        text-align: center" ToolTip='<%#Eval("PR_MAP")%>'></asp:TextBox>
                                                </td>
                                                <td id="gb1" runat="server">
                                                    <asp:Label ID="PUR_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_GUOBIAO" runat="server" Text='<%#Eval("PUR_GUOBIAO")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="80px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("PUR_GUOBIAO")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_TUHAO" runat="server" Text='<%#Eval("PUR_TUHAO")%>' BorderStyle="None"
                                                        ForeColor="#1A438E" Width="80px" Style="background-color: Transparent; text-align: center"
                                                        ToolTip='<%#Eval("PUR_TUHAO")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNAME" runat="server" Text='<%#Eval("PUR_MARNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MARNORM" runat="server" Text='<%#Eval("PUR_MARNORM")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_MARNORM" runat="server" Width="80px" Text='<%#Eval("PUR_MARNORM")%>'
                                                        ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                        text-align: center" ToolTip='<%#Eval("PUR_MARNORM")%>'></asp:TextBox>
                                                </td>
                                                <td id="cz1" runat="server">
                                                    <asp:Label ID="PUR_MARTERIAL" runat="server" Text='<%#Eval("PUR_MARTERIAL")%>'></asp:Label>
                                                </td>
                                                <td id="cd1" runat="server">
                                                    <asp:Label ID="PUR_LENGTH" runat="server" Text='<%#Eval("PUR_LENGTH")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="kd1" runat="server">
                                                    <asp:Label ID="PUR_WIDTH" runat="server" Text='<%#Eval("PUR_WIDTH")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NUNIT" runat="server" Text='<%#Eval("PUR_NUNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPNUM" runat="server" Text='<%#Eval("PUR_RPNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="FZUNIT" runat="server" Text='<%#Eval("FZUNIT")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_RPFZNUM" runat="server" Text='<%#Eval("PUR_RPFZNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <%--<asp:HyperLink runat="server" ID="Hyp_Look" NavigateUrl='<%#"~/TM_Data/TM_MP_Require_Audit.aspx?mp_audit_id="+Eval("PUR_PCODE") %>'
                                                        Style="font-family: @宋体; color: #336600; font-weight: normal;" Target="_blank"
                                                        CssClass="link" ForeColor="Black">
                                                        <asp:Image ID="image4" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />&nbsp;上查</asp:HyperLink>--%>
                                                   <asp:HyperLink runat="server" ID="Hyp_Look" NavigateUrl='<%#Eval("PUR_PCODE").ToString().Trim().Contains("XZ")?"~/PC_Data/PC_TBPC_Otherpur_Bill_look.aspx?action=view&mp_id="+Eval("PUR_PCODE"):"~/TM_Data/TM_MP_Require_Audit.aspx?mp_audit_id="+Eval("PUR_PCODE") %>'
                                                        Style="font-family: @宋体; color: #336600; font-weight: normal;" Target="_blank"
                                                        CssClass="link" ForeColor="Black">
                                                        <asp:Image ID="image4" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />&nbsp;上查</asp:HyperLink>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_NOTE" runat="server" Text='<%#Eval("PUR_NOTE")%>' Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txt_NOTE" runat="server" Width="180px" Text='<%#Eval("PUR_NOTE")%>'
                                                        ForeColor="#1A438E" BorderStyle="None" Style="background-color: Transparent;
                                                        text-align: center" ToolTip='<%#Eval("PUR_NOTE")%>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="PUR_MASHAPE" runat="server" Text='<%#Eval("PUR_MASHAPE")%>'></asp:Label>
                                                </td>
                                                <td id="fgr1" runat="server">
                                                    <asp:Label ID="PUR_PTASMAN" runat="server" Text='<%#Eval("PUR_PTASMAN")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="fgrq1" runat="server">
                                                    <asp:Label ID="PUR_PTASTIME" runat="server" Text='<%#Eval("PUR_PTASTIME")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="cgy1" runat="server">
                                                    <asp:Label ID="PUR_CGMAN" runat="server" Text='<%#Eval("PUR_CGMAN")%>' Visible="false"></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="PUR_CGMANNM" runat="server" Text='<%#Eval("PUR_CGMANNM")%>'></asp:Label>
                                                    &nbsp;
                                                </td>
                                                <td id="sqr1" runat="server">
                                                    <asp:Label ID="PUR_SQRNM" runat="server" Text='<%#Eval("sqrnm")%>'></asp:Label>&nbsp;
                                                </td>
                                                <td id="clbm1" runat="server">
                                                    <asp:Label ID="PUR_MARID" runat="server" Text='<%#Eval("PUR_MARID")%>'></asp:Label>
                                                </td>
                                                <td id="ph1" runat="server">
                                                    <asp:Label ID="PUR_PCODE" runat="server" Text='<%#Eval("PUR_PCODE")%>'></asp:Label>
                                                    <asp:Label ID="PUR_IFFAST" runat="server"  Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="32" align="center">
                                            <asp:Panel ID="NoDataPane" runat="server" Visible="false">
                                                没有数据！</asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <uc1:UCPaging ID="UCPaging1" runat="server" />
                            </div>
                        </div>
                        <br />
                        <asp:Panel ID="Panel1" runat="server">
                            <table width="100%">
                                <tr>
                                    <td width="20%" align="left">
                                        <asp:CheckBox ID="chk_selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                            Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                                        <%--<asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                                        <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />--%>
                                        <input type="checkbox" onclick="seleAll()" id="selectall" /><label for="selectall">&nbsp;全选</label>&nbsp;&nbsp;
                                        <input type="button" value="连选" onclick="seleLian()" />
                                        <input type="button" value="取消" onclick="seleCancel()" />
                                    </td>
                                    <td align="center">
                                        注：红色表示提交代用审核中，绿色表示此计划有变更，整行亮黄色表示采购数量为0
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        </div> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
</asp:Content>
