<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="PM_fayun_list.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_fayun_list" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    询比价管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />

    <script src="PcJs/xbjhidden.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/rowcolor.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <script src="PcJs/pricesearch.js" type="text/javascript" charset="GB2312" language="javascript"></script>

    <link href="PcJs/superTables_compressed.css" rel="stylesheet" type="text/css" />

    <script src="PcJs/superTables_compressed.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function PLdaochu() {
            //        window.showModalDialog('PC_TBPC_PLdaochu.aspx','',"dialogHeight:300px;dialogWidth:500px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
            //        window.location.href="PC_TBPC_Purchaseplan_startcontent.aspx?shape="+escape(shape)+"&mp_id="+escape(mpid)+"";
        }
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
            else if (radio26.checked == true) {
                num2 = 6;
            }
            else if (radio27.checked == true) {
                num2 = 7;
            }
            sheetno = obj.getElementsByTagName("td")[2].getElementsByTagName("span")[0].innerHTML;
            //        window.location.href="TBPC_IQRCMPPRCLST_checked_detail.aspx?num1="+num1+"&num2="+num2+"&sheetno="+escape(sheetno);
            window.open("PM_fayun_check_detail.aspx?sheetno=" + escape(sheetno) + "");
        }

        //    function mowinopen(ptcode_rcode)
        //    {
        //       var autonum=Math.round(10000*Math.random());
        //       window.open('PC_Data_addto_list.aspx?autonum='+autonum+'&ptcode_rcode='+escape(ptcode_rcode)+'','',"dialogHeight:460px;dialogWidth:780px;status:no;scroll:auto;center:yes;toolbar=no;menubar=no");
        //       window.location.href="TBPC_IQRCMPPRCLST_checked.aspx";
        //    }
        //     self.moveTo(0,0)
        //     self.resizeTo(screen.availWidth,screen.availHeight)
    </script>

    <script language="javascript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {

        }
        function EndRequestHandler(sender, args) {
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
                            for (var k = 2; k <= 9; k++) {
                                dataRow.getElementsByTagName("td")[k].getElementsByTagName("span")[0].style.display = "none";
                            }
                        }
                    }
                    //		        alert("Finished... " + ((new Date()) - this.start) + "ms.");
                }
            });
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
    </style>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
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
                                        <asp:RadioButton ID="rad_fpquanbu" runat="server" Text="全部" TextAlign="Right" GroupName="select01"
                                            OnCheckedChanged="rad_yifenpei_CheckedChanged" AutoPostBack="true" Checked="true" />
                                        <asp:RadioButton ID="rad_yifenpei" runat="server" Text="已分配" TextAlign="Right" GroupName="select01"
                                            OnCheckedChanged="rad_yifenpei_CheckedChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="rad_weifenpei" runat="server" Text="未分配" TextAlign="Right" GroupName="select01"
                                            OnCheckedChanged="rad_weifenpei_CheckedChanged" AutoPostBack="true" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rad_quanbu" runat="server" Text="全部" TextAlign="Right" GroupName="select"
                                            OnCheckedChanged="rad_quanbu_CheckedChanged" AutoPostBack="true" />&nbsp;
                                        <asp:RadioButton ID="rad_weitijiao" runat="server" Text="未审核" TextAlign="Right" GroupName="select"
                                            OnCheckedChanged="rad_weitijiao_CheckedChanged" AutoPostBack="true" Checked="true" />&nbsp;
                                        <asp:RadioButton ID="rad_shenhezhong" runat="server" Text="审核中" TextAlign="Right"
                                            GroupName="select" OnCheckedChanged="rad_shenhezhong_CheckedChanged" AutoPostBack="true" />&nbsp;
                                        <asp:RadioButton ID="rad_bohui" runat="server" Text="已驳回" TextAlign="Right" GroupName="select"
                                            OnCheckedChanged="rad_bohui_CheckedChanged" AutoPostBack="true" />
                                        <asp:Label ID="lb_bjdbh" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        <asp:RadioButton ID="rad_tongguo" runat="server" Text="已通过" TextAlign="Right" GroupName="select"
                                            OnCheckedChanged="rad_tongguo_CheckedChanged" AutoPostBack="true" />&nbsp;
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="cb_sp" runat="server" Visible="false" Checked="false" AutoPostBack="true"
                                            Text="只显示我的未审" ForeColor="Red" OnCheckedChanged="CBSP_CheckedChanged" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnJSD" OnClick="btnJSD_OnClick" Text="均摊运费" />
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
                                        <asp:TextBox ID="Tb_pcode" runat="server" Text="" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        发运商：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_Gongyingshang" runat="server" Width="150px" OnTextChanged="tb_Gongyingshang_Textchanged"
                                            AutoPostBack="true"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="tb_Gongyingshang"
                                            ServicePath="~/PM_Data/PM_AutoComplete.asmx" CompletionSetCount="10" MinimumPrefixLength="2"
                                            CompletionInterval="10" ServiceMethod="Getfayun" FirstRowSelected="true" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem">
                                        </cc1:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        任务号：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="tb_tsaid" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        项目名称：<asp:TextBox ID="txtXMMC" runat="server" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        设备名称：<asp:TextBox runat="server" ID="txtSBMC" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        每页显示：
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList5" runat="server">
                                            <asp:ListItem Text="50条记录" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="100条记录" Value="100"></asp:ListItem>
                                            <asp:ListItem Text="150条记录" Value="150"></asp:ListItem>
                                            <asp:ListItem Text="200条记录" Value="200"></asp:ListItem>
                                            <asp:ListItem Text="250条记录" Value="200"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btn_search" runat="server" Text="查询" OnClick="btn_search_click" />&nbsp;
                                        <asp:Button ID="btn_clear" runat="server" Text="清除" OnClick="btn_clear_click" />&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="height: 470px; overflow: auto; width: 100%">
                            <div class="cpbox xscroll">
                                <table id="tab" class="nowrap cptable fullwidth" align="center" title="双击查看详细信息">
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
                                                <td>
                                                    <strong>单据编号</strong>
                                                </td>
                                                <%-- <td>
                                                    <strong>部门<br />
                                                        负责人</strong>
                                                </td>--%>
                                                <td>
                                                    <strong>制单人</strong>
                                                </td>
                                                <td>
                                                    <strong>日期</strong>
                                                </td>
                                                <td>
                                                    <strong>主管<br />
                                                        经理</strong>
                                                </td>
                                                <td>
                                                    <strong>审核<br />
                                                        标志</strong>
                                                </td>
                                                <td>
                                                    <strong>发运商</strong>
                                                </td>
                                                <td>
                                                    <strong>总金额</strong>
                                                </td>
                                                <td>
                                                    <strong>发运数量</strong>
                                                </td>
                                                <td>
                                                    <strong>合同号</strong>
                                                </td>
                                                <td>
                                                    <strong>项目名称</strong>
                                                </td>
                                                <td>
                                                    <strong>任务号</strong>
                                                </td>
                                                <td>
                                                    <strong>设备名称</strong>
                                                </td>
                                                <td>
                                                    <strong>交货期</strong>
                                                </td>
                                                <td>
                                                    <strong>收货单位</strong>
                                                </td>
                                                <td>
                                                    <strong>是否均摊</strong>
                                                </td>
                                                <%--<td>
                                                    <strong>发货内容</strong>
                                                </td>--%>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr id="row" runat="server" class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)"
                                                ondblclick="redirectw(this)">
                                                <%--onclick="rowclick(this)"--%>
                                                <td>
                                                    <asp:CheckBox ID="CKBOX_SELECT" CssClass="checkBoxCss" BorderStyle="None" runat="server"
                                                        Checked="false" onclick="checkme(this)"></asp:CheckBox>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="rownum" runat="server" Text='<%# Container.ItemIndex + 1+(Convert.ToDouble(UCPaging1.CurrentPage)-1)*(Convert.ToDouble(DropDownList5.SelectedValue))%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbdh" runat="server" Text='<%#Eval("ICL_SHEETNO")%>'></asp:Label>
                                                    <asp:Label runat="server" ID="PM_ZONGXU" Text='<%#Eval("PM_ZONGXU")%>' Visible="false"></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="bmfzrid" runat="server" Text='<%#Eval("ICL_FUZRID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="bmfzrnm" runat="server" Text='<%#Eval("iclfzrnm")%>'></asp:Label>
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="zdid" runat="server" Text='<%#Eval("ICL_REVIEWA")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="zdnm" runat="server" Text='<%#Eval("zdrnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zdtime" runat="server" Text='<%#Eval("ICL_IQRDATE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="zgid" runat="server" Text='<%#Eval("ICL_ZHUGUANID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="zgnm" runat="server" Text='<%#Eval("iclzgnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="shstate" runat="server" Text='<%#get_pur_bjdsh(Eval("ICL_STATE").ToString())%>'></asp:Label>
                                                    <asp:Label ID="totalstate" runat="server" Text='<%#Eval("ICL_STATE")%>' Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="supplierid" runat="server" Text='<%#Eval("PM_SUPPLIERRESID")%>' Visible="false"></asp:Label>
                                                    <asp:Label ID="suppliernm" runat="server" Text='<%#Eval("supplierresnm")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="amount" runat="server" Text='<%#Eval("PM_PRICE")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="fayunnum" runat="server" Text='<%#Eval("PM_FYNUM")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="CM_CONTR" runat="server" Text='<%#Eval("TSA_PJID")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="CM_PROJ" runat="server" Text='<%#Eval("CM_PROJ")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID2")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="TSA_ENGNAME" runat="server" Text='<%#Eval("PM_ENGNAME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="CM_JHTIME" runat="server" Text='<%#Eval("CM_JHTIME")%>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="CM_CUSNAME" runat="server" Text='<%#Eval("CM_CUSNAME")%>'></asp:Label>
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="txt_neirong" runat="server" Text='<%#Eval("PM_FHDETAIL")%>'></asp:Label>
                                                </td> --%>
                                                <td>
                                                    <asp:Label ID="lbJT" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="30">
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
                                                    for (var k = 2; k <= 9; k++) {
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
                                <table width="100%">
                                    <tr>
                                        <td width="50%">
                                            共查出<asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red"></asp:Label>批比价单/
                                            <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red"></asp:Label>条记录
                                        </td>
                                        <td align="right" width="50%">
                                            <uc1:UCPaging ID="UCPaging1" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <asp:Panel ID="Pan_select" runat="server">
                            <asp:CheckBox ID="selectall" runat="server" Text="全选" AutoPostBack="true" TextAlign="left"
                                Checked="false" OnCheckedChanged="selectall_CheckedChanged" />
                            <asp:Button ID="btn_LX" runat="server" Text="连选" OnClick="btn_LX_click" />
                            <asp:Button ID="btn_QX" runat="server" Text="取消" OnClick="btn_QX_click" />
                        </asp:Panel>
                    </div>
                </div>
            </div>
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
